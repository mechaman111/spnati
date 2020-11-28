using Desktop;
using Desktop.Skinning;
using ImagePipeline;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SPNATI_Character_Editor.DataStructures;

namespace SPNATI_Character_Editor.Controls.Pipelines
{
	public partial class GraphEditor : UserControl
	{
		private ISkin _character;
		private PoseStage _stage;
		private PoseSheet _sheet;
		private PipelineGraph _graph;
		private PoseEntry _cell;

		private Point _mousePosition;
		private WorkingConnection _connection;
		private PipelineNodeControl _selectedControl;
		private PipelineNodeControl _previewControl;

		private Dictionary<string, object> _processCache = new Dictionary<string, object>();

		private List<PipelineNode> _pendingPreviews = new List<PipelineNode>();

		private Dictionary<PipelineNode, PipelineNodeControl> _nodeMap = new Dictionary<PipelineNode, PipelineNodeControl>();

		public GraphEditor()
		{
			InitializeComponent();
		}

		public void Destroy()
		{
			_pendingPreviews.Clear();
			_graph.Nodes.CollectionChanged -= Nodes_CollectionChanged;
			_graph.PropertyChanged -= _graph_PropertyChanged;
			foreach (PipelineNodeControl ctl in _nodeMap.Values)
			{
				CleanUpControl(ctl);
			}
			_nodeMap.Clear();
		}

		public void SetData(ISkin character, PoseStage stage, PoseEntry cell, PipelineGraph graph)
		{
			_character = character;
			_cell = cell;
			_stage = stage;
			_sheet = stage.Sheet;
			_graph = graph;
			_graph.Nodes.CollectionChanged += Nodes_CollectionChanged;

			RenderControls();
			_graph.PropertyChanged += _graph_PropertyChanged;
		}

		private void _graph_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			_stage.NotifyPropertyChanged("Pipeline");
			_cell?.NotifyPropertyChanged("Pipeline");
		}

		private void Nodes_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			switch (e.Action)
			{
				case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
					foreach (PipelineNode node in e.NewItems)
					{
						AddControl(node, true);
					}
					break;
				case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
					foreach (PipelineNode node in e.OldItems)
					{
						RemoveControl(node);
					}
					break;
				case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
				case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
					RenderControls();
					break;
			}
		}

		/// <summary>
		/// Completely re-renders the node controls
		/// </summary>
		private void RenderControls()
		{
			panel.Controls.Clear();
			foreach (PipelineNodeControl ctl in _nodeMap.Values)
			{
				CleanUpControl(ctl);
			}
			_nodeMap.Clear();
			if (_graph == null)
			{
				return;
			}
			foreach (PipelineNode node in _graph.NodeMap.Values)
			{
				PipelineNodeControl ctl = AddControl(node, false);
				if (node == _graph.MasterNode)
				{
					SetPreviewControl(ctl);
				}
			}
		}

		/// <summary>
		/// Creates and adds a control for a node
		/// </summary>
		/// <param name="node"></param>
		private PipelineNodeControl AddControl(PipelineNode node, bool focus)
		{
			PipelineNodeControl ctl = new PipelineNodeControl();
			ctl.OnUpdateSkin(SkinManager.Instance.CurrentSkin);
			ctl.PreviewInvalidated += Node_PreviewInvalidated;
			ctl.InputPortGrabbed += Ctl_InputPortGrabbed;
			ctl.OutputPortGrabbed += Ctl_OutputPortGrabbed;
			ctl.InputPortReleased += Ctl_InputPortReleased;
			ctl.HeaderClicked += Ctl_HeaderClicked;
			ctl.DoubleClick += Ctl_DoubleClick;
			ctl.InputHovered += Ctl_InputHovered;
			ctl.Deleted += Ctl_Deleted;
			node.PropertyChanged += Node_PropertyChanged;
			panel.Controls.Add(ctl);
			ctl.Location = new System.Drawing.Point(node.X, node.Y);
			ctl.Node = node;
			_nodeMap[node] = ctl;

			if (focus)
			{
				ctl.BringToFront();
				ctl.Focus();
			}
			return ctl;
		}

		private void CleanUpControl(PipelineNodeControl ctl)
		{
			ctl.Node.PropertyChanged -= Node_PropertyChanged;
			ctl.PreviewInvalidated -= Node_PreviewInvalidated;
			ctl.InputPortGrabbed -= Ctl_InputPortGrabbed;
			ctl.OutputPortGrabbed -= Ctl_OutputPortGrabbed;
			ctl.InputPortReleased -= Ctl_InputPortReleased;
			ctl.HeaderClicked -= Ctl_HeaderClicked;
			ctl.DoubleClick -= Ctl_DoubleClick;
			ctl.InputHovered -= Ctl_InputHovered;
			ctl.Deleted -= Ctl_Deleted;
			ctl.Destroy();
		}

		private void Ctl_HeaderClicked(object sender, System.EventArgs e)
		{
			PipelineNodeControl ctl = sender as PipelineNodeControl;
			_selectedControl = ctl;
		}

		private void Ctl_DoubleClick(object sender, System.EventArgs e)
		{
			PipelineNodeControl ctl = sender as PipelineNodeControl;
			SetPreviewControl(ctl);
		}

		private void Ctl_InputHovered(object sender, PortEventArgs e)
		{
			if (_connection != null)
			{
				PipelineNodeControl ctl = sender as PipelineNodeControl;
				PipelineNode node = ctl.Node;
				e.IsValid = _graph.ValidateConnection(_connection.Source, _connection.OutputIndex, node, e.Index);
			}
		}

		private void SetPreviewControl(PipelineNodeControl ctl)
		{
			if (_previewControl != null)
			{
				_previewControl.PreviewChanged -= _previewControl_PreviewChanged;
			}
			picPreview.SetImage(ctl?.Preview, false);
			_previewControl = ctl;
			if (ctl != null)
			{
				grpPreview.Text = ctl.Node.Definition.Name;
				_previewControl.PreviewChanged += _previewControl_PreviewChanged;
			}
			else
			{
				grpPreview.Text = "Preview";
			}
		}

		private void _previewControl_PreviewChanged(object sender, System.EventArgs e)
		{
			picPreview.SetImage(_previewControl.Preview, false);
		}

		private void Node_PreviewInvalidated(object sender, System.EventArgs e)
		{
			InvalidatePreviews((sender as PipelineNodeControl).Node);
		}

		private void InvalidatePreview(PipelineNode node)
		{
			PipelineNodeControl ctl;
			if (_nodeMap.TryGetValue(node, out ctl))
			{
				ctl.Shield();
			}
			_pendingPreviews.Add(node);
			tmrPreview.Start();
		}

		/// <summary>
		/// Removes a node's control
		/// </summary>
		/// <param name="node"></param>
		private void RemoveControl(PipelineNode node)
		{
			PipelineNodeControl ctl;
			if (_nodeMap.TryGetValue(node, out ctl))
			{
				node.PropertyChanged -= Node_PropertyChanged;
				panel.Controls.Remove(ctl);
				ctl.Destroy();
				_nodeMap.Remove(node);
				if (_selectedControl == ctl)
				{
					_selectedControl = null;
				}
				if (_previewControl == ctl)
				{
					SetPreviewControl(null);
				}
			}
		}

		private void Node_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "X" || e.PropertyName == "Y")
			{
				panel.Invalidate();
				panel.Update();
			}
		}

		private void panel_Paint(object sender, PaintEventArgs e)
		{
			if (_graph == null)
			{
				return;
			}
			Skin skin = SkinManager.Instance.CurrentSkin;
			Pen pen = skin.PrimaryWidget.GetPen(VisualState.Normal, false, true);
			Graphics g = e.Graphics;

			foreach (Connection connection in _graph.Connections)
			{
				PipelineNode from = _graph.GetNode(connection.From);
				PipelineNode to = _graph.GetNode(connection.To);
				DrawConnection(g, pen, from, connection.FromIndex, to, connection.ToIndex);
			}

			// connection being dragged
			if (_connection != null)
			{
				Pen activePen = skin.SecondaryWidget.GetPen(VisualState.Normal, true, true);
				DrawConnection(g, activePen, _connection.Source, _connection.OutputIndex, null, -1);
			}
		}

		private void DrawConnection(Graphics g, Pen pen, PipelineNode from, int fromIndex, PipelineNode to, int index)
		{
			PipelineNodeControl fromCtl;
			PipelineNodeControl toCtl = null;
			_nodeMap.TryGetValue(from, out fromCtl);
			if (to != null)
			{
				_nodeMap.TryGetValue(to, out toCtl);
			}

			if (fromCtl != null)
			{
				Point pt1 = fromCtl.GetPortPosition(fromIndex, false);
				pt1 = new Point(fromCtl.Left + pt1.X, fromCtl.Top + pt1.Y);
				Point pt2 = _mousePosition;
				if (toCtl != null)
				{
					pt2 = toCtl.GetPortPosition(index, true);
					pt2 = new Point(toCtl.Left + pt2.X, toCtl.Top + pt2.Y);
				}
				g.DrawLine(pen, pt1, pt2);
			}
		}

		private async void tmrPreview_Tick(object sender, System.EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			tmrPreview.Stop();

			PipelineSettings settings = new PipelineSettings() { PreviewMode = true, Cache = _processCache };

			if (_pendingPreviews.Count > 0)
			{
				await _graph.Process(_cell, settings);
				for (int i = 0; i < _pendingPreviews.Count; i++)
				{
					PipelineNode nextNode = _pendingPreviews[i];
					PipelineNodeControl ctl;
					if (_nodeMap.TryGetValue(nextNode, out ctl))
					{
						if (!_graph.HasNodeOutput(nextNode.Id))
						{
							await _graph.Process(_cell, settings, nextNode);
						}
						PipelineResult result = _graph.GetNodeOutput(nextNode.Id);
						ctl.SetPreview(result);
					}
				}
			}
			_pendingPreviews.Clear();
			Cursor.Current = Cursors.Default;
		}

		private class WorkingConnection
		{
			public PipelineNodeControl Control;
			public PipelineNode Source;
			public int OutputIndex;
			public PipelineNode OldTarget;
			public int OldInput;
		}

		private void Ctl_OutputPortGrabbed(object sender, PortEventArgs e)
		{
			PipelineNodeControl ctl = sender as PipelineNodeControl;
			PipelineNode node = ctl.Node;

			_connection = new WorkingConnection()
			{
				Source = node,
				Control = ctl,
				OutputIndex = e.Index,
			};
			_mousePosition = panel.PointToClient(ctl.PointToScreen(e.MousePosition));

			ctl.MouseMove += Ctl_MouseMove;
			ctl.MouseUp += Ctl_MouseUp;
		}

		private void Ctl_InputPortGrabbed(object sender, PortEventArgs e)
		{
			PipelineNodeControl ctl = sender as PipelineNodeControl;
			PipelineNode node = ctl.Node;
			int input = e.Index;

			PortConnection from = _graph.GetInput(node, input);
			if (from != null)
			{
				_connection = new WorkingConnection()
				{
					Source = from.Node,
					Control = ctl,
					OutputIndex = from.Index,
					OldTarget = node,
					OldInput = input
				};
				_mousePosition = panel.PointToClient(ctl.PointToScreen(e.MousePosition));

				ctl.MouseMove += Ctl_MouseMove;
				ctl.MouseUp += Ctl_MouseUp;

				//disconnect this input
				_graph.Disconnect(ctl.Node, e.Index);
				panel.Invalidate();
			}
		}

		private void Ctl_InputPortReleased(object sender, PortEventArgs e)
		{
			if (_connection == null)
			{
				return;
			}

			PipelineNode target = (sender as PipelineNodeControl).Node;

			//make the connection
			if (_graph.Connect(_connection.Source, _connection.OutputIndex, target, e.Index))
			{
				InvalidatePreviews(target);
			}
			else
			{
				if (_connection.Source.Definition.Outputs[0].Type == target.Definition.Inputs[e.Index].Type)
				{
					MessageBox.Show("This is not the right type of input for the port.", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else
				{
					MessageBox.Show("This connection would a node to feed into itself, which is invalid.", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void Ctl_MouseUp(object sender, MouseEventArgs e)
		{
			if (_connection == null) { return; }

			Point screenPos = _connection.Control.PointToScreen(e.Location);
			//need to manually tell all the controls about this event because they won't receive it otherwise due to how WinForms works while dragging
			foreach (PipelineNodeControl ctl in _nodeMap.Values)
			{
				if (ctl != sender)
				{
					ctl.ParentMouseUp(e, screenPos);
				}
			}

			if (_connection.OldTarget != null)
			{
				InvalidatePreviews(_connection.OldTarget);
			}

			_connection = null;
			panel.Invalidate();
		}

		/// <summary>
		/// Invalidates the previews of a node and all its children
		/// </summary>
		/// <param name="node"></param>
		private void InvalidatePreviews(PipelineNode node)
		{
			InvalidatePreview(node);
			if (node.Definition.Outputs != null)
			{
				int outputCount = node.Definition.Outputs.Length;
				for (int i = 0; i < outputCount; i++)
				{
					foreach (PortConnection output in _graph.GetOutputs(node, i))
					{
						InvalidatePreviews(output.Node);
					}
				}
			}
		}

		private void Ctl_MouseMove(object sender, MouseEventArgs e)
		{
			if (_connection == null) { return; }
			Point screenPos = _connection.Control.PointToScreen(e.Location);
			//need to manually tell all the controls about this MouseMove because they won't receive it otherwise due to how WinForms works while dragging
			foreach (PipelineNodeControl ctl in _nodeMap.Values)
			{
				if (ctl != sender)
				{
					ctl.ParentMouseMove(e, screenPos);
				}
			}

			_mousePosition = panel.PointToClient(screenPos);
			panel.Invalidate();
			panel.Update();
		}

		private void tsRemoveNode_Click(object sender, System.EventArgs e)
		{
			DeleteNode(_selectedControl);
		}

		private void Ctl_Deleted(object sender, System.EventArgs e)
		{
			DeleteNode(sender as PipelineNodeControl);
		}

		private void DeleteNode(PipelineNodeControl ctl)
		{
			if (ctl != null)
			{
				if (ctl.IsMasterNode)
				{
					MessageBox.Show("You cannot delete the Result node.");
					return;
				}
				_graph.RemoveNode(ctl.Node);
				panel.Invalidate();
			}
		}

		private void tsAddNode_Click(object sender, System.EventArgs e)
		{
			IPipelineNode record = RecordLookup.DoLookup(typeof(IPipelineNode), "", false, FilterNodes, null) as IPipelineNode;
			if (record != null)
			{
				_graph.AddNode(record.Key);
			}
		}

		private bool FilterNodes(IRecord record)
		{
			return record.Key != "root";
		}
	}
}
