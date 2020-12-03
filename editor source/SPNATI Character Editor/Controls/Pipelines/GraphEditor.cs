using Desktop;
using Desktop.Skinning;
using ImagePipeline;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SPNATI_Character_Editor.DataStructures;
using System;

namespace SPNATI_Character_Editor.Controls.Pipelines
{
	public partial class GraphEditor : UserControl
	{
		public event EventHandler NameChanged;

		private const int PortExtensionLength = 15;
		private const int VertexThreshold = PipelineNodeControl.PortRadius * 2;
		private const int EndPointThreshold = 20;

		public PipelineGraph Graph;
		private ISkin _character;
		private PoseMatrix _matrix;
		private PoseStage _stage;
		private PoseSheet _sheet;
		private PoseEntry _cell;

		private Point _mousePosition;
		private WorkingConnection _connection;
		private PipelineNodeControl _selectedControl;
		private PipelineNodeControl _previewControl;
		private Point2D _hoverVertex;
		private Connection _hoverConnection;
		private int _hoverVertexIndex = -1;
		private int _hoverVertexInsertionPoint = -1;

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
			Graph.Nodes.CollectionChanged -= Nodes_CollectionChanged;
			Graph.PropertyChanged -= _graph_PropertyChanged;
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
			_matrix = CharacterDatabase.GetPoseMatrix(_character);
			_sheet = stage.Sheet;
			Graph = graph;
			Graph.Nodes.CollectionChanged += Nodes_CollectionChanged;

			RenderControls();
			Graph.PropertyChanged += _graph_PropertyChanged;
		}

		private void _graph_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (_stage.Pipeline == Graph.Key || _cell.Pipeline == Graph.Key)
			{
				_stage.NotifyPropertyChanged("Pipeline");
				_cell?.NotifyPropertyChanged("Pipeline");
			}
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
			if (Graph == null)
			{
				return;
			}
			foreach (PipelineNode node in Graph.NodeMap.Values)
			{
				PipelineNodeControl ctl = AddControl(node, false);
				if (node == Graph.MasterNode)
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
				e.IsValid = Graph.ValidateConnection(_connection.Source, _connection.OutputIndex, node, e.Index);
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
			if (Graph == null)
			{
				return;
			}
			Skin skin = SkinManager.Instance.CurrentSkin;
			Pen pen = skin.PrimaryWidget.GetPen(VisualState.Normal, false, true);
			SolidBrush brush = skin.PrimaryWidget.GetBrush(VisualState.Normal, false, true);
			Graphics g = e.Graphics;

			foreach (Connection connection in Graph.Connections)
			{
				PipelineNode from = Graph.GetNode(connection.From);
				PipelineNode to = Graph.GetNode(connection.To);
				DrawConnection(g, pen, brush, connection, from, connection.FromIndex, to, connection.ToIndex);
			}

			// connection being dragged
			if (_connection != null)
			{
				Pen activePen = skin.SecondaryWidget.GetPen(VisualState.Normal, true, true);
				DrawConnection(g, activePen, brush, null, _connection.Source, _connection.OutputIndex, null, -1);
			}
			else if (_hoverConnection != null)
			{
				const int radius = PipelineNodeControl.PortRadius;
				Rectangle rect = new Rectangle(_hoverVertex.X - radius, _hoverVertex.Y - radius, radius * 2, radius * 2);
				g.FillEllipse(skin.SecondaryWidget.GetBrush(VisualState.Normal, true, true), rect);
			}
		}

		private void DrawConnection(Graphics g, Pen pen, SolidBrush brush, Connection connection, PipelineNode from, int fromIndex, PipelineNode to, int index)
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
				Point startPt = fromCtl.GetPortPosition(fromIndex, false);
				startPt = new Point(fromCtl.Left + startPt.X, fromCtl.Top + startPt.Y);
				Point endPt = _mousePosition;
				if (connection != null)
				{
					if (toCtl != null)
					{
						startPt = new Point(startPt.X + PortExtensionLength, startPt.Y);
						g.DrawLine(pen, new Point(startPt.X - PortExtensionLength, startPt.Y), startPt); //straight line extending from output
						endPt = toCtl.GetPortPosition(index, true);
						endPt = new Point(toCtl.Left + endPt.X - PortExtensionLength, toCtl.Top + endPt.Y);
						g.DrawLine(pen, endPt, new Point(endPt.X + PortExtensionLength, endPt.Y)); //straight line extending from input
					}

					Point lastPt = startPt;
					for (int i = 0; i <= connection.Vertices.Count; i++)
					{
						Point pt = i < connection.Vertices.Count ? (Point)connection.Vertices[i] : endPt;
						if (i < connection.Vertices.Count)
						{
							pt.X -= panel.HorizontalScroll.Value;
							pt.Y -= panel.VerticalScroll.Value;
						}
						g.DrawLine(pen, lastPt, pt); //connect previous vertex
						if (pt != endPt)
						{
							g.FillEllipse(brush, new Rectangle(pt.X - 2, pt.Y - 2, 5, 5)); //draw the vertex
						}
						lastPt = pt;
					}
				}
				else
				{
					g.DrawLine(pen, startPt, endPt);
				}
			}
		}

		private async void tmrPreview_Tick(object sender, System.EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			tmrPreview.Stop();

			PipelineSettings settings = new PipelineSettings() { PreviewMode = true, Cache = _processCache };

			if (_pendingPreviews.Count > 0)
			{
				await Graph.Process(_cell, settings);
				for (int i = 0; i < _pendingPreviews.Count; i++)
				{
					PipelineNode nextNode = _pendingPreviews[i];
					PipelineNodeControl ctl;
					if (_nodeMap.TryGetValue(nextNode, out ctl))
					{
						if (!Graph.HasNodeOutput(nextNode.Id))
						{
							await Graph.Process(_cell, settings, nextNode);
						}
						PipelineResult result = Graph.GetNodeOutput(nextNode.Id);
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

			PortConnection from = Graph.GetInput(node, input);
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
				Graph.Disconnect(ctl.Node, e.Index);
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
			if (Graph.Connect(_connection.Source, _connection.OutputIndex, target, e.Index))
			{
				InvalidatePreviews(target);
			}
			else
			{
				if (_connection.Source.Definition.Outputs[_connection.OutputIndex].Type != target.Definition.Inputs[e.Index].Type)
				{
					MessageBox.Show($"Incompatible ports. Trying to connect an output of type \"{_connection.Source.Definition.Outputs[_connection.OutputIndex].Type}\" to an input of type \"{target.Definition.Inputs[e.Index].Type}\".", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
					foreach (PortConnection output in Graph.GetOutputs(node, i))
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
				Graph.RemoveNode(ctl.Node);
				panel.Invalidate();
			}
		}

		private void tsAddNode_Click(object sender, System.EventArgs e)
		{
			IPipelineNode record = RecordLookup.DoLookup(typeof(IPipelineNode), "", false, FilterNodes, null) as IPipelineNode;
			if (record != null)
			{
				Graph.AddNode(record.Key);
			}
		}

		private bool FilterNodes(IRecord record)
		{
			return record.Key != "root";
		}

		private void tsSaveAs_Click(object sender, System.EventArgs e)
		{
			string name = InputBox.Show("Choose a new name for the pipeline:", "Save As");
			if (name == null)
			{
				return;
			}
			PipelineGraph existing = _matrix.Pipelines.Find(p => p.Name == name);
			if (existing != null)
			{
				MessageBox.Show("A pipeline with that name already exists.");
				tsSaveAs_Click(sender, e);
				return;
			}

			PipelineGraph copy = new PipelineGraph();
			Graph.CopyPropertiesInto(copy);
			copy.Key = copy.Name = name;
			copy.Character = Graph.Character;
			copy.OnAfterDeserialize("");
			_matrix.Pipelines.Add(copy);
			Destroy();
			SetData(_character, _stage, _cell, copy);
			NameChanged?.Invoke(this, EventArgs.Empty);
		}

		private void panel_MouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.None)
			{
				//check for hovering over a connection
				foreach (Connection connection in Graph.Connections)
				{
					Point start = GetConnectionPoint(connection.From, connection.FromIndex, false);
					Point end = GetConnectionPoint(connection.To, connection.ToIndex, true);

					//make sure it's not near an end point
					float distStart = e.Location.Distance(start);
					float distEnd = e.Location.Distance(end);
					if (distStart < EndPointThreshold || distEnd < EndPointThreshold)
					{
						break; //if we're near an end point, don't allow vertex dragging for any connections
					}

					Point lastPt = start;
					for (int i = 0; i <= connection.Vertices.Count; i++)
					{
						Point pt = i < connection.Vertices.Count ? (Point)connection.Vertices[i] : end;
						if (i < connection.Vertices.Count)
						{
							pt.X -= panel.HorizontalScroll.Value;
							pt.Y -= panel.VerticalScroll.Value;
						}

						Point intersection = e.Location.GetClosestPointOnLineSegment(lastPt, pt);
						float dist = e.Location.Distance(intersection);

						if (dist <= VertexThreshold)
						{
							//see if we're near the vertex
							float vertDist = intersection.Distance(pt);
							float startDist = intersection.Distance(lastPt);
							if (vertDist <= VertexThreshold)
							{
								_hoverVertexIndex = i;
								_hoverVertex = pt;
							}
							else if (startDist <= VertexThreshold && i > 0)
							{
								_hoverVertexIndex = i - 1;
								_hoverVertex = lastPt;
							}
							else
							{
								_hoverVertexInsertionPoint = i;
								_hoverVertexIndex = -1;
								_hoverVertex = intersection;
							}
							_hoverConnection = connection;
							_hoverConnection.VerticesChanged += _hoverConnection_VerticesChanged;
							panel.Invalidate();
							return;
						}
						lastPt = pt;
					}
				}

				if (_hoverConnection != null)
				{
					ClearHoverConnection();
				}
			}
			else if (e.Button == MouseButtons.Left)
			{
				if (_hoverVertexIndex >= 0)
				{
					_hoverVertex = e.Location;
					_hoverConnection.ReplaceVertex(_hoverVertexIndex, new Point2D(e.Location.X + panel.HorizontalScroll.Value, e.Location.Y + panel.VerticalScroll.Value));
				}
			}
		}

		private void panel_MouseDown(object sender, MouseEventArgs e)
		{
			if (_hoverConnection != null)
			{
				if (e.Button == MouseButtons.Right && _hoverVertexIndex >= 0)
				{
					//delete this vertex
					_hoverConnection.RemoveVertex(_hoverVertexIndex);
					ClearHoverConnection();
				}
				else if (e.Button == MouseButtons.Left && _hoverConnection != null)
				{
					if (_hoverVertexIndex == -1)
					{
						//add a new vertex
						_hoverVertexIndex = _hoverConnection.InsertVertex(_hoverVertexInsertionPoint, new Point2D(_hoverVertex.X + panel.HorizontalScroll.Value, _hoverVertex.Y + panel.VerticalScroll.Value));
					}
				}
			}
		}

		private void panel_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left && _hoverConnection != null)
			{
				ClearHoverConnection();
			}
		}

		private void ClearHoverConnection()
		{
			_hoverConnection.VerticesChanged -= _hoverConnection_VerticesChanged;
			_hoverConnection = null;
			_hoverVertexIndex = -1;
			_hoverVertexInsertionPoint = -1;
			panel.Invalidate();
		}

		private void _hoverConnection_VerticesChanged(object sender, EventArgs e)
		{
			panel.Invalidate();
		}

		/// <summary>
		/// Gets the point where a connection hooks into a port
		/// </summary>
		/// <param name="nodeId"></param>
		/// <param name="input"></param>
		/// <returns></returns>
		private Point GetConnectionPoint(int nodeId, int index, bool input)
		{
			PipelineNodeControl ctl;
			PipelineNode node = Graph.GetNode(nodeId);
			if (node != null)
			{
				if (_nodeMap.TryGetValue(node, out ctl))
				{
					Point pt = ctl.GetPortPosition(index, input);
					pt.X += ctl.Left;
					pt.Y += ctl.Top;
					if (input)
					{
						pt.X -= PortExtensionLength;
					}
					else
					{
						pt.X += PortExtensionLength;
					}
					return pt;
				}
			}
			return new Point(0, 0);
		}

		private void panel_Scroll(object sender, ScrollEventArgs e)
		{
			panel.Invalidate();
		}
	}
}
