using Desktop;
using Desktop.CommonControls.PropertyControls;
using SPNATI_Character_Editor.Controls;
using SPNATI_Character_Editor.EpilogueEditing;
using SPNATI_Character_Editor.Forms;
using SPNATI_Character_Editor.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Activities
{
	[Activity(typeof(Character), 210)]
	[Activity(typeof(Costume), 210)]
	public partial class PoseCreator : Activity
	{
		public const int SelectionLeeway = EpilogueCanvas.SelectionLeeway;
		public const int RotationLeeway = EpilogueCanvas.RotationLeeway;

		private ISkin _character;
		private ImageLibrary _library;

		private Pose _currentPose;
		private Sprite _currentSprite;
		private PoseDirective _currentDirective;
		private Keyframe _currentKeyframe;
		private PoseAnimFrame _currentAnimFrame;
		private Dictionary<object, TreeNode> _nodes = new Dictionary<object, TreeNode>();
		private List<string> _markers = new List<string>();

		private PosePreview _preview;
		private SpritePreview _selectedObject = null;
		private DateTime _lastTick;

		private object _clipboard;

		private bool _populatingPoses;

		private Point _lastMouse;
		private Point _canvasOffset = new Point(0, 0);
		private Point _downPoint = new Point(0, 0);
		private Point _startDragPosition = new Point(0, 0);
		private HoverContext _moveContext;
		private EditMode _mode = EditMode.Edit;
		private CanvasState _state = CanvasState.Normal;
		private Pen _penOuterSelection;
		private Pen _penInnerSelection;
		private Pen _penBoundary;
		private Pen _penKeyframe;
		private Pen _penKeyframeConnection;

		public PoseCreator()
		{
			InitializeComponent();

			_penBoundary = new Pen(Color.Gray, 1);
			_penBoundary.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

			_penOuterSelection = new Pen(Brushes.White, 1);
			_penOuterSelection.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDotDot;
			_penInnerSelection = new Pen(Brushes.Black, 1);
			_penInnerSelection.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDotDot;

			_penKeyframe = new Pen(Color.FromArgb(127, 255, 255, 255));
			_penKeyframe.Width = 2;
			_penKeyframeConnection = new Pen(Color.FromArgb(127, 255, 255, 255), 8);
			_penKeyframeConnection.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
		}

		public override string Caption
		{
			get { return "Pose Maker"; }
		}

		protected override void OnInitialize()
		{
			_character = Record as ISkin;
			_library = ImageLibrary.Get(_character);
		}

		protected override void OnFirstActivate()
		{
			RebuildPoseList();
		}

		protected override void OnActivate()
		{
			Workspace.ToggleSidebar(false);
		}

		private void RebuildPoseList()
		{
			_populatingPoses = true;
			lstPoses.Nodes.Clear();
			foreach (Pose pose in _character.CustomPoses)
			{
				AddNode(pose);
			}
			_populatingPoses = false;
		}

		private TreeNode AddNode(Pose pose)
		{
			TreeNode node = new TreeNode(pose.ToString());
			node.Tag = pose;
			_nodes[pose] = node;
			lstPoses.Nodes.Add(node);

			//subnodes for sprites and directives
			foreach (Sprite sprite in pose.Sprites)
			{
				AddSprite(pose, sprite);
			}

			foreach (PoseDirective directive in pose.Directives)
			{
				AddDirective(pose, directive);
			}
			return node;
		}

		private TreeNode AddSprite(Pose pose, Sprite sprite)
		{
			TreeNode node = new TreeNode(sprite.ToString());
			node.Tag = sprite;
			_nodes[sprite] = node;

			TreeNode parent = _nodes[pose];
			parent.Nodes.Add(node);
			return node;
		}

		private TreeNode AddDirective(Pose pose, PoseDirective directive)
		{
			TreeNode node = new TreeNode(directive.ToString());
			node.Tag = directive;
			_nodes[directive] = node;

			TreeNode parent = _nodes[pose];
			parent.Nodes.Add(node);

			if (directive.DirectiveType == "animation")
			{
				foreach (Keyframe frame in directive.Keyframes)
				{
					AddKeyframe(directive, frame);
				}
			}
			return node;
		}

		private TreeNode AddKeyframe(PoseDirective directive, Keyframe keyframe)
		{
			TreeNode node = new TreeNode(keyframe.ToString());
			node.Tag = keyframe;
			_nodes[keyframe] = node;

			TreeNode parent = _nodes[directive];
			parent.Nodes.Add(node);
			return node;
		}

		protected override void OnDeactivate()
		{
			Workspace.ToggleSidebar(true);
		}

		private void lstPoses_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (_populatingPoses)
			{
				return;
			}

			table.Save();

			TreeNode node = lstPoses.SelectedNode as TreeNode;
			if (node == null)
			{
				table.Data = null;
				return;
			}
			_currentPose = node.Tag as Pose;
			_currentSprite = node.Tag as Sprite;
			_currentKeyframe = node.Tag as Keyframe;
			_currentAnimFrame = node.Tag as PoseAnimFrame;
			_currentDirective = node.Tag as PoseDirective;
			if (_currentKeyframe is PoseDirective || _currentKeyframe is Sprite)
			{
				_currentKeyframe = null;
			}
			if (_currentKeyframe != null || _currentAnimFrame != null)
			{
				_currentDirective = node.Parent.Tag as PoseDirective;
				_currentPose = node.Parent.Parent.Tag as Pose;
			}
			else if (_currentSprite != null || _currentDirective != null)
			{
				_currentPose = node.Parent.Tag as Pose;
			}
			if (_currentSprite != null)
			{
				table.RecordFilter = SpriteFilter;
			}
			else if (_currentKeyframe != null)
			{
				table.RecordFilter = KeyframeFilter;
			}
			else if (_currentAnimFrame != null)
			{
				table.RecordFilter = AnimFrameFilter;
			}
			else if (_currentDirective != null)
			{
				table.RecordFilter = DirectiveFilter;
			}
			else
			{
				table.RecordFilter = null;
			}
			table.Context = new PoseContext(_currentPose, _character, CharacterContext.Pose);
			table.Data = node.Tag;

			tsAddKeyframe.Enabled = (_currentDirective != null);

			BuildPreview();
		}

		private bool SpriteFilter(PropertyRecord record)
		{
			switch (record.Key)
			{
				case "id":
				case "src":
				case "x":
				case "y":
				case "z":
				case "scalex":
				case "scaley":
				case "pivotx":
				case "pivoty":
				case "width":
				case "height":
				case "alpha":
				case "rotation":
				case "marker":
					return true;
				default:
					return false;
			}
		}

		private bool DirectiveFilter(PropertyRecord record)
		{
			switch (record.Key)
			{
				case "id":
				case "tween":
				case "ease":
				case "loop":
				case "delay":
				case "marker":
					return true;
				default:
					return false;
			}
		}

		private bool KeyframeFilter(PropertyRecord record)
		{
			switch (record.Key)
			{
				case "time":
				case "x":
				case "y":
				case "scalex":
				case "scaley":
				case "alpha":
				case "rotation":
					return true;
				default:
					return false;
			}
		}

		private bool AnimFrameFilter(PropertyRecord record)
		{
			switch (record.Key)
			{
				case "time":
				case "src":
					return true;
				default:
					return false;
			}
		}

		private void table_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			UpdateNode(table.Data);
			if (table.Data is Pose && e.PropertyName == "Id")
			{
				_library.Rename(_currentPose);
			}
			else if (table.Data is Keyframe && e.PropertyName == "Time")
			{
				ResortFrames();
			}
			BuildPreview();
		}

		private void ResortFrames()
		{
			if (_currentDirective == null)
			{
				return;
			}

			TreeNode node = _nodes.Get(_currentDirective);
			if (node != null)
			{
				TreeNode selected = _nodes.Get(_currentKeyframe);

				RemoveSelectionHandler();
				//remove the nodes from the tree
				node.Nodes.Clear();

				_currentDirective.Keyframes.Sort((f1, f2) =>
				{
					float t1, t2;
					float.TryParse(f1.Time, NumberStyles.Number, CultureInfo.InvariantCulture, out t1);
					float.TryParse(f2.Time, NumberStyles.Number, CultureInfo.InvariantCulture, out t2);
					return t1.CompareTo(t2);
				});

				//add the nodes back
				foreach (Keyframe kf in _currentDirective.Keyframes)
				{
					TreeNode child = _nodes.Get(kf);
					if (child == null)
					{
						//this shouldn't ever happen, but add a node if it doesn't exist for some reason
						AddKeyframe(_currentDirective, kf);
					}
					else
					{
						node.Nodes.Add(child);
					}
				}

				if (selected != null)
				{
					lstPoses.SelectedNode = selected;
				}
				AddSelectionHandler();
			}
		}

		private void UpdateNode(object data)
		{
			TreeNode node;
			if (_nodes.TryGetValue(data, out node))
			{
				node.Text = data.ToString();
			}
		}

		private void tsRemove_Click(object sender, System.EventArgs e)
		{
			DeleteSelectedNode();
		}

		private void DeleteSelectedNode()
		{
			if (lstPoses.SelectedNode == null) { return; }

			if (MessageBox.Show($"Are you sure you want to remove {lstPoses.SelectedNode.Tag}? This cannot be undone.", "Remove Pose", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
			{
				RemoveNode(lstPoses.SelectedNode);
			}
		}

		private void tsAdd_Click(object sender, System.EventArgs e)
		{
			Pose pose = new Pose();
			pose.Id = "New Pose";
			_library.Add(pose);
			_character.CustomPoses.Add(pose);
			AddNode(pose);
			SelectNode(pose);
		}

		private void SelectNode(object data)
		{
			TreeNode node;
			if (_nodes.TryGetValue(data, out node))
			{
				lstPoses.SelectedNode = node;
			}
		}

		private void lstPoses_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
			{
				DeleteSelectedNode();
			}
			else if (e.KeyCode == Keys.X && e.Modifiers.HasFlag(Keys.Control))
			{
				tsCut_Click(this, EventArgs.Empty);
			}
			else if (e.KeyCode == Keys.C && e.Modifiers.HasFlag(Keys.Control))
			{
				tsCopy_Click(this, EventArgs.Empty);
			}
			else if (e.KeyCode == Keys.D && e.Modifiers.HasFlag(Keys.Control))
			{
				tsDuplicate_Click(this, EventArgs.Empty);
			}
			else if (e.KeyCode == Keys.V && e.Modifiers.HasFlag(Keys.Control))
			{
				tsPaste_Click(this, EventArgs.Empty);
			}
		}

		private void tsCollapseAll_Click(object sender, EventArgs e)
		{
			lstPoses.CollapseAll();
		}

		private void tsExpandAll_Click(object sender, EventArgs e)
		{
			lstPoses.ExpandAll();
		}

		/// <summary>
		/// Deletes a node
		/// </summary>
		/// <param name="node"></param>
		private void RemoveNode(TreeNode node)
		{
			if (node == null) { return; }

			Sprite sprite = node.Tag as Sprite;
			Keyframe frame = node.Tag as Keyframe;
			PoseDirective directive = node.Tag as PoseDirective;
			PoseAnimFrame animFrame = node.Tag as PoseAnimFrame;
			Pose pose = node.Tag as Pose;

			_nodes.Remove(node.Tag);
			if (pose != null)
			{
				_character.CustomPoses.Remove(_currentPose);
				_library.Remove(_currentPose);
			}
			else if (sprite != null)
			{
				_currentPose.Sprites.Remove(sprite);
			}
			else if (animFrame != null)
			{
				_currentDirective.AnimFrames.Remove(animFrame);
			}
			else if (directive != null)
			{
				_currentPose.Directives.Remove(directive);
			}
			else if (frame != null)
			{
				_currentDirective.Keyframes.Remove(frame);
			}
			node.Remove();
		}

		private void tsCut_Click(object sender, EventArgs e)
		{
			TreeNode node = lstPoses.SelectedNode as TreeNode;
			if (node != null)
			{
				_clipboard = node.Tag;
				RemoveNode(node);
			}
		}

		private void tsCopy_Click(object sender, EventArgs e)
		{
			TreeNode node = lstPoses.SelectedNode as TreeNode;
			if (node != null)
			{
				ICloneable cloner = node.Tag as ICloneable;
				_clipboard = cloner?.Clone();
			}
		}

		private void tsPaste_Click(object sender, EventArgs e)
		{
			TreeNode node = lstPoses.SelectedNode as TreeNode;
			if (_clipboard == null || node == null) { return; }
			ICloneable cloner = _clipboard as ICloneable;
			object obj = cloner.Clone();
			Pose pastedPose = obj as Pose;
			Sprite pastedSprite = obj as Sprite;
			PoseDirective pastedDirective = obj as PoseDirective;
			Keyframe pastedKeyframe = obj as Keyframe;

			TreeNode pastedNode = null;
			if (pastedPose != null)
			{
				_character.CustomPoses.Add(pastedPose);
				pastedNode = AddNode(pastedPose);
			}
			else if (pastedSprite != null)
			{
				if (_currentPose != null)
				{
					_currentPose.Sprites.Add(pastedSprite);
					pastedNode = AddSprite(_currentPose, pastedSprite);
				}
			}
			else if (pastedDirective != null)
			{
				if (_currentPose != null)
				{
					_currentPose.Directives.Add(pastedDirective);
					pastedNode = AddDirective(_currentPose, pastedDirective);
				}
			}
			else if (pastedKeyframe != null)
			{
				if (_currentDirective != null)
				{
					_currentDirective.Keyframes.Add(pastedKeyframe);
					pastedNode = AddKeyframe(_currentDirective, pastedKeyframe);
				}
			}

			if (pastedNode != null)
			{
				lstPoses.SelectedNode = pastedNode;
			}
		}

		private void tsDuplicate_Click(object sender, EventArgs e)
		{
			object clipboard = _clipboard;
			tsCopy_Click(sender, e);
			tsPaste_Click(sender, e);
			_clipboard = clipboard;
		}

		private void lstPoses_ItemDrag(object sender, ItemDragEventArgs e)
		{
			TreeNode node = e.Item as TreeNode;
			if (node != null)
			{
				if (node.Tag is Pose)
				{
					lstPoses.CollapseAll();
				}
				else if (node.Tag is Keyframe && !(node.Tag is Sprite) && !(node.Tag is PoseDirective))
				{
					return; //can't drag keyframes
				}
				DoDragDrop(e.Item, DragDropEffects.Move);
			}
		}

		private void lstPoses_DragDrop(object sender, DragEventArgs e)
		{
			lblDragger.Visible = false;

			TreeNode dragNode = (TreeNode)e.Data.GetData(typeof(TreeNode));
			if (dragNode == null) { return; }
			bool draggingDirective = dragNode.Tag is Directive;
			bool draggingSprite = dragNode.Tag is Sprite;
			bool draggingKeyframe = dragNode.Tag is Keyframe && !draggingDirective && !draggingSprite;

			Point targetPoint = lstPoses.PointToClient(new Point(e.X, e.Y));
			TreeNode targetNode = lstPoses.GetNodeAt(targetPoint);
			if (targetNode == null) { return; }
			bool targetSprite = targetNode.Tag is Sprite;
			bool targetDirective = targetNode.Tag is Directive;
			bool targetKeyframe = targetNode.Tag is Keyframe && !targetDirective && !targetSprite;

			if (!dragNode.Equals(targetNode))
			{
				if (draggingDirective)
				{
					//dragging a directive

					if (targetKeyframe)
					{
						MoveNode(dragNode, targetNode.Parent.Index + 1);
					}
					else if (!targetDirective)
					{
						//insert at start of target scene
						MoveNode(dragNode, 0);
					}
					else
					{
						if (targetPoint.Y - targetNode.Bounds.Height / 2 < targetNode.Bounds.Y - 2)
						{
							if (targetNode.PrevNode == null)
							{
								//insert at first position in scene
								MoveNode(dragNode, 0);
							}
							else
							{
								//insert beneath previous node
								MoveNode(dragNode, targetNode.Index);
							}
						}
						else
						{
							//insert beneath target node
							MoveNode(dragNode, targetNode.Index + 1);
						}
					}
				}
				else if (draggingKeyframe)
				{
					//dragging a keyframe

					if (targetKeyframe)
					{
						if (targetPoint.Y - targetNode.Bounds.Height / 2 < targetNode.Bounds.Y - 2)
						{
							MoveNode(dragNode, targetNode.Index);
						}
						else
						{
							MoveNode(dragNode, targetNode.Index + 1);
						}
					}
				}
				else
				{
					//dragging a pose

					if (targetPoint.Y - targetNode.Bounds.Height / 2 < targetNode.Bounds.Y - 2)
					{
						//insert above target scene
						MoveNode(dragNode, targetNode.Index);
					}
					else
					{
						if (targetDirective)
						{
							//this shouldn't happen since the tree collapses directives, but if it does, use the scene node
							targetNode = targetNode.Parent;
						}

						//insert behind target scene
						MoveNode(dragNode, targetNode.Index + 1);
					}
				}
			}
		}

		private void MoveNode(TreeNode node, int index)
		{
			//if moving higher, adjust the index to account for the node being removed
			if (node.Index < index)
			{
				index--;
			}

			TreeNode parent = node.Parent;
			node.Remove();
			if (parent != null)
			{
				parent.Nodes.Insert(index, node);
			}
			else
			{
				lstPoses.Nodes.Insert(index, node);
			}

			lstPoses.SelectedNode = node;
		}

		private void RemoveSelectionHandler()
		{
			lstPoses.AfterSelect -= lstPoses_AfterSelect;
		}

		private void AddSelectionHandler()
		{
			lstPoses.AfterSelect += lstPoses_AfterSelect;
		}

		private void lstPoses_DragEnter(object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.Move;
		}

		private void lstPoses_DragLeave(object sender, EventArgs e)
		{
			lblDragger.Visible = false;
		}

		private void lstPoses_DragOver(object sender, DragEventArgs e)
		{
			lstPoses.Scroll();

			TreeNode dragNode = (TreeNode)e.Data.GetData(typeof(TreeNode));
			if (dragNode == null) { return; }
			bool draggingSprite = dragNode.Tag is Sprite;
			bool draggingDirective = dragNode.Tag is PoseDirective;
			bool draggingKeyframe = dragNode.Tag is Keyframe && !draggingSprite && !draggingDirective;

			Point targetPoint = lstPoses.PointToClient(new Point(e.X, e.Y));
			TreeNode targetNode = lstPoses.GetNodeAt(targetPoint);
			bool targetDirective = targetNode?.Tag is PoseDirective;


			if (targetNode == null || (draggingKeyframe && (targetNode == null || targetNode.Parent != dragNode.Parent))
				|| (draggingSprite && (targetNode == null || targetNode.Parent != dragNode.Parent)))
			{
				//keyframes can only be dragged within their original parent
				e.Effect = DragDropEffects.None;
				lblDragger.Visible = false;
				return;
			}
			else
			{
				e.Effect = DragDropEffects.Move;
			}

			if (draggingDirective && !targetDirective)
			{
				//dragging a directive on top of a pose. always insert below
				Point pt = lblDragger.Location;
				pt.Y = lstPoses.Top + targetNode.Bounds.Y + targetNode.Bounds.Height;
				lblDragger.Location = pt;
				lblDragger.Visible = true;
			}
			else
			{
				if (targetPoint.Y - targetNode.Bounds.Height / 2 < targetNode.Bounds.Y - 2)
				{
					//hovering on the upper half of a node
					Point pt = lblDragger.Location;
					pt.Y = lstPoses.Top + targetNode.Bounds.Y;
					lblDragger.Location = pt;
					lblDragger.Visible = true;
				}
				else
				{
					//hovering on the lower half
					Point pt = lblDragger.Location;
					pt.Y = lstPoses.Top + targetNode.Bounds.Y + targetNode.Bounds.Height;
					lblDragger.Location = pt;
					lblDragger.Visible = true;
				}
			}
			RemoveSelectionHandler();
			lstPoses.SelectedNode = dragNode;
			AddSelectionHandler();
		}

		private void lstPoses_QueryContinueDrag(object sender, QueryContinueDragEventArgs e)
		{
			if (e.Action == DragAction.Cancel || e.Action == DragAction.Drop)
			{
				lblDragger.Visible = false;
			}
		}

		private void addSpriteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (_currentPose == null)
			{
				return;
			}

			openFileDialog1.UseAbsolutePaths = true;
			if (openFileDialog1.ShowDialog(_character, "") == DialogResult.OK)
			{
				Sprite sprite = new Sprite();
				sprite.Src = openFileDialog1.FileName;
				string id = Path.GetFileNameWithoutExtension(sprite.Src);
				int hyphen = id.IndexOf('-');
				if (hyphen == 1 || hyphen == 2)
				{
					id = id.Substring(hyphen + 1);
				}
				sprite.Id = GetUniqueId(id);

				int index = -1;
				if (_currentSprite != null)
				{
					index = _currentPose.Sprites.IndexOf(_currentSprite) + 1;
					_currentPose.Sprites.Insert(index, sprite);
				}
				else
				{
					_currentPose.Sprites.Add(sprite);
				}
				TreeNode node = AddSprite(_currentPose, sprite);
				if (index >= 0)
				{
					MoveNode(node, index);
				}
				lstPoses.SelectedNode = node;
			}
		}

		private string GetUniqueId(string id)
		{
			if (_currentPose == null || _currentPose.Sprites.Find(s => s.Id == id) == null)
			{
				return id;
			}
			int suffix = 0;
			string prefix = id;
			while (_currentPose.Sprites.Find(s => s.Id == id) != null)
			{
				suffix++;
				id = prefix + suffix;
			}

			return id;
		}

		private void addAnimationToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (_currentPose == null)
			{
				return;
			}
			PoseDirective directive = new PoseDirective();
			directive.DirectiveType = "animation";
			directive.InterpolationMethod = "linear"; //engine defaults to none which I don't think is the 99% case, so use linear
			_currentPose.Directives.Add(directive);
			lstPoses.SelectedNode = AddDirective(_currentPose, directive);

			//add a keyframe by default too
			Keyframe startFrame = new Keyframe();
			startFrame.Time = "0";
			directive.Keyframes.Add(startFrame);
			AddKeyframe(directive, startFrame);

			lstPoses.SelectedNode.Expand();
		}

		private void tsAddKeyframe_Click(object sender, EventArgs e)
		{
			if (_currentDirective == null)
			{
				return;
			}
			Keyframe frame;

			if (_currentDirective.Keyframes.Count > 0)
			{
				frame = _currentDirective.Keyframes[_currentDirective.Keyframes.Count - 1].Clone() as Keyframe;
				//add a second to the time just so there's some animation by default
				float time;
				if (!float.TryParse(frame.Time, NumberStyles.Float, CultureInfo.InvariantCulture, out time))
				{
					frame.Time = "1";
				}
				else
				{
					frame.Time = (time + 1).ToString();
				}
			}
			else
			{
				frame = new Keyframe();
				frame.Time = "0";
			}

			_currentDirective.Keyframes.Add(frame);
			lstPoses.SelectedNode = AddKeyframe(_currentDirective, frame);
		}

		private void BuildPreview()
		{
			_preview?.Dispose();
			_selectedObject = null;
			_preview = null;
			if (_currentPose == null) { return; }
			_preview = new PosePreview(_character, _currentPose, _currentDirective, _currentKeyframe, _currentSprite, _markers);
			_lastTick = DateTime.Now;
			tmrTick.Enabled = _preview.IsAnimated;

			//select the object corresponding to the selected object in the tree
			string id = (_currentSprite != null ? _currentSprite.Id : _currentKeyframe != null ? _currentDirective.Id : null);
			if (id != null)
			{
				if (_preview.SelectedObject != null)
				{
					_selectedObject = _preview.SelectedObject;
					_selectedObject.LinkedFrame = _currentKeyframe;
					_selectedObject.LinkedDirective = _currentDirective;
				}
				else
				{
					_selectedObject = _preview.Sprites.Find(obj => obj.Id == id);
				}
			}

			canvas.Invalidate();
		}

		private void tmrTick_Tick(object sender, EventArgs e)
		{
			DateTime now = DateTime.Now;
			TimeSpan elapsed = now - _lastTick;
			float elapsedSec = (float)elapsed.TotalSeconds;
			_lastTick = now;

			if (_preview == null)
			{
				tmrTick.Enabled = false;
				return;
			}

			_preview.Update(elapsedSec);
			canvas.Invalidate();
		}

		private void canvas_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			if (_preview != null)
			{
				//draw the "screen"
				g.FillRectangle(Brushes.LightGray, 0, _canvasOffset.Y, canvas.Width, canvas.Height);

				//center marker
				g.DrawLine(_penBoundary, canvas.Width / 2 + _canvasOffset.X, 0, canvas.Width / 2 + _canvasOffset.X, canvas.Height);

				//draw the pose
				_preview.Draw(g, canvas.Width, canvas.Height, _canvasOffset);

				//selection and gizmos
				if (_selectedObject != null)
				{
					DrawSelection(g, _selectedObject);

					//rotation arrow
					if (_moveContext == HoverContext.Rotate)
					{
						Image arrow = Resources.rotate_arrow;
						Point pt = new Point(_lastMouse.X - arrow.Width / 2, _lastMouse.Y - arrow.Height / 2);

						//rotate to face the object's center
						PointF center = _selectedObject.ToScreenCenter(canvas.Width, canvas.Height, _canvasOffset);

						double angle = Math.Atan2(center.Y - pt.Y, center.X - pt.X);
						angle = angle * (180 / Math.PI) - 90;

						g.TranslateTransform(_lastMouse.X, _lastMouse.Y);
						g.RotateTransform((float)angle);
						g.TranslateTransform(-_lastMouse.X, -_lastMouse.Y);
						g.DrawImage(arrow, pt);
						g.ResetTransform();
					}
				}
			}
		}

		private void DrawSelection(Graphics g, SpritePreview sprite)
		{
			if (_mode == EditMode.Playback) { return; }
			RectangleF bounds = sprite.ToAbsScreenRegion(canvas.Width, canvas.Height, _canvasOffset);
			const int SelectionPadding = 0;
			g.DrawRectangle(_penOuterSelection, bounds.X - 2 - SelectionPadding, bounds.Y - 2 - SelectionPadding, bounds.Width + 4 + SelectionPadding * 2, bounds.Height + 4 + SelectionPadding * 2);
			g.DrawRectangle(_penInnerSelection, bounds.X - 1 - SelectionPadding, bounds.Y - 1 - SelectionPadding, bounds.Width + 2 + SelectionPadding * 2, bounds.Height + 2 + SelectionPadding * 2);

			//pivot point
			if (_selectedObject.LinkedFrame == null)
			{
				bounds = sprite.ToUnscaledScreenRegion(canvas.Width, canvas.Height, _canvasOffset);

				if (_state == CanvasState.MovingPivot || _moveContext == HoverContext.Pivot)
				{
					g.DrawRectangle(_penKeyframe, bounds.X, bounds.Y, bounds.Width, bounds.Height);
				}

				PointF pt = new PointF(bounds.X + sprite.PivotX / sprite.Width * bounds.Width, bounds.Y + sprite.PivotY / sprite.Height * bounds.Height);
				g.FillEllipse(Brushes.White, pt.X - 3, pt.Y - 3, 6, 6);
				g.FillEllipse(Brushes.Black, pt.X - 2, pt.Y - 2, 4, 4);
			}
		}

		private void canvas_MouseDown(object sender, MouseEventArgs e)
		{
			if (_preview == null) { return; }
			switch (_mode)
			{
				case EditMode.Edit:
					_downPoint = new Point(e.X, e.Y);
					if (e.Button == MouseButtons.Left)
					{
						//object selection
						SpritePreview obj = null;
						if (_moveContext == HoverContext.None || _moveContext == HoverContext.Select)
						{
							//1 - Keyframe
							if (_preview.SelectedObject != null)
							{
								List<SpritePreview> keyframes = new List<SpritePreview>();
								keyframes.Add(_preview.SelectedObject);
								obj = GetObjectAtPoint(e.X, e.Y, keyframes);
							}

							//2 - Sprite
							if (obj == null)
							{
								obj = GetObjectAtPoint(e.X, e.Y, _preview.Sprites);
							}

							if (obj != null && (_selectedObject == null || _selectedObject.Sprite != obj.Sprite))
							{
								SelectNode(obj.Sprite);
							}
						}
					}
					else if (e.Button == MouseButtons.Right)
					{
						_lastMouse = new Point(e.X, e.Y);
						_state = CanvasState.Panning;
						canvas.Cursor = Cursors.NoMove2D;
					}
					break;
			}
		}

		private SpritePreview GetObjectAtPoint(int x, int y, List<SpritePreview> objects)
		{
			//search in reverse order because objects are sorted by depth
			for (int i = objects.Count - 1; i >= 0; i--)
			{
				SpritePreview obj = objects[i];
				RectangleF bounds = obj.ToScreenRegion(canvas.Width, canvas.Height, _canvasOffset);
				if (bounds.X <= x && x <= bounds.X + bounds.Width &&
					bounds.Y <= y && y <= bounds.Y + bounds.Height)
				{
					return obj;
				}
			}
			return null;
		}

		/// <summary>
		/// Gets a contextual action based on where the mouse is relative to objects on screen
		/// </summary>
		/// <param name="worldPt"></param>
		private HoverContext GetContext(Point screenPt)
		{
			if (_selectedObject != null)
			{
				bool locked = false;

				RectangleF bounds = _selectedObject.ToAbsScreenRegion(canvas.Width, canvas.Height, _canvasOffset);
				if (true)
				{
					bool allowPivot = _selectedObject.LinkedFrame == null;
					bool allowRotate = true;
					bool allowScale = true;

					float dl = Math.Abs(screenPt.X - bounds.X);
					float dr = Math.Abs(screenPt.X - (bounds.X + bounds.Width));
					float dt = Math.Abs(screenPt.Y - bounds.Y);
					float db = Math.Abs(screenPt.Y - (bounds.Y + bounds.Height));

					//pivot position
					if (allowPivot)
					{
						RectangleF pivotBounds = _selectedObject.ToUnscaledScreenRegion(canvas.Width, canvas.Height, _canvasOffset);
						PointF pivot = new PointF(pivotBounds.X + _selectedObject.PivotX / _selectedObject.Width * pivotBounds.Width, pivotBounds.Y + _selectedObject.PivotY / _selectedObject.Height * pivotBounds.Height);

						//pivoting - hovering over the pivot circle
						float px = Math.Abs(screenPt.X - pivot.X);
						float py = Math.Abs(screenPt.Y - pivot.Y);
						if (px <= SelectionLeeway && py <= SelectionLeeway)
						{
							return HoverContext.Pivot;
						}
					}

					if (allowRotate)
					{
						//rotating - hovering outside a corner
						if (screenPt.X < bounds.X - SelectionLeeway && screenPt.X >= bounds.X - RotationLeeway && dt <= RotationLeeway ||
							screenPt.Y < bounds.Y - SelectionLeeway && screenPt.Y >= bounds.Y - RotationLeeway && dl <= RotationLeeway ||
							screenPt.X > bounds.X + bounds.Width + SelectionLeeway && screenPt.X <= bounds.X + bounds.Width + RotationLeeway && dt <= RotationLeeway ||
							screenPt.Y < bounds.Y - SelectionLeeway && screenPt.Y >= bounds.Y - RotationLeeway && dr <= RotationLeeway ||
							screenPt.X < bounds.X - SelectionLeeway && screenPt.X >= bounds.X - RotationLeeway && db <= RotationLeeway ||
							screenPt.Y > bounds.Y + bounds.Height + SelectionLeeway && screenPt.Y <= bounds.Y + bounds.Height + RotationLeeway && dl <= RotationLeeway ||
							screenPt.X > bounds.X + bounds.Width + SelectionLeeway && screenPt.X <= bounds.X + bounds.Width + RotationLeeway && db <= RotationLeeway ||
							screenPt.Y > bounds.Y + bounds.Height + SelectionLeeway && screenPt.Y <= bounds.Y + bounds.Height + RotationLeeway && dr <= RotationLeeway)
						{
							return locked ? HoverContext.Locked : HoverContext.Rotate;
						}
					}

					if (allowScale)
					{
						//scaling/stretching - grabbing an edge
						if (dl <= SelectionLeeway)
						{
							if (dt <= SelectionLeeway)
							{
								return locked ? HoverContext.Locked : HoverContext.ScaleTop | HoverContext.ScaleLeft;
							}
							else if (db <= SelectionLeeway)
							{
								return locked ? HoverContext.Locked : HoverContext.ScaleBottom | HoverContext.ScaleLeft;
							}
							else if (bounds.Y <= screenPt.Y && screenPt.Y <= bounds.Y + bounds.Height)
							{
								return locked ? HoverContext.Locked : HoverContext.ScaleLeft;
							}
						}

						if (dr <= SelectionLeeway)
						{
							if (dt <= SelectionLeeway)
							{
								return locked ? HoverContext.Locked : HoverContext.ScaleTop | HoverContext.ScaleRight;
							}
							else if (db <= SelectionLeeway)
							{
								return locked ? HoverContext.Locked : HoverContext.ScaleBottom | HoverContext.ScaleRight;
							}
							else if (bounds.Y <= screenPt.Y && screenPt.Y <= bounds.Y + bounds.Height)
							{
								return locked ? HoverContext.Locked : HoverContext.ScaleRight;
							}
						}

						if (dt <= SelectionLeeway && bounds.X <= screenPt.X && screenPt.X <= bounds.X + bounds.Width)
						{
							return locked ? HoverContext.Locked : HoverContext.ScaleTop;
						}

						if (db <= SelectionLeeway && bounds.X <= screenPt.X && screenPt.X <= bounds.X + bounds.Width)
						{
							return locked ? HoverContext.Locked : HoverContext.ScaleBottom;
						}
					}
				}

				if (bounds.X <= screenPt.X && screenPt.X <= bounds.X + bounds.Width &&
					bounds.Y <= screenPt.Y && screenPt.Y <= bounds.Y + bounds.Height)
				{
					return HoverContext.Drag;
				}
			}

			//see if we're on top of an object
			SpritePreview obj = GetObjectAtPoint(screenPt.X, screenPt.Y, _preview.Sprites);
			if (obj != null)
			{
				return HoverContext.Select;
			}

			return HoverContext.None;
		}

		private void canvas_MouseMove(object sender, MouseEventArgs e)
		{
			if (_preview == null) { return; }
			Point screenPt = new Point(e.X, e.Y);

			//lblCoord.Text = $"{_canvasOffset}";
			lblCoord.Text = "";
			switch (_mode)
			{
				case EditMode.Edit:
					switch (_state)
					{
						case CanvasState.Normal:
							HoverContext context = GetContext(screenPt);
							if (_moveContext == HoverContext.Rotate || _moveContext == HoverContext.ArrowRight || _moveContext == HoverContext.ArrowLeft ||
								_moveContext == HoverContext.ArrowDown || _moveContext == HoverContext.ArrowUp || _moveContext == HoverContext.Pivot)
							{
								canvas.Invalidate();
							}
							_moveContext = context;
							switch (context)
							{
								case HoverContext.ScaleTopLeft:
								case HoverContext.ScaleBottomRight:
									canvas.Cursor = Cursors.SizeNWSE;
									break;
								case HoverContext.ScaleTopRight:
								case HoverContext.ScaleBottomLeft:
									canvas.Cursor = Cursors.SizeNESW;
									break;
								case HoverContext.Drag:
									canvas.Cursor = Cursors.SizeAll;
									break;
								case HoverContext.ScaleLeft:
								case HoverContext.ScaleRight:
									canvas.Cursor = Cursors.SizeWE;
									break;
								case HoverContext.ScaleTop:
								case HoverContext.ScaleBottom:
									canvas.Cursor = Cursors.SizeNS;
									break;
								case HoverContext.Select:
									canvas.Cursor = Cursors.Hand;
									break;
								case HoverContext.Pivot:
									canvas.Cursor = Cursors.Cross;
									break;
								default:
									canvas.Cursor = Cursors.Default;
									break;
							}

							if (e.Button == MouseButtons.Left && context != HoverContext.None && context != HoverContext.Locked)
							{
								//start dragging
								if (HoverContext.Object.HasFlag(context))
								{
									switch (context)
									{
										case HoverContext.Drag:
											_startDragPosition = new Point((int)_selectedObject.X, (int)_selectedObject.Y);
											_state = CanvasState.MovingObject;
											break;
										case HoverContext.ScaleTopLeft:
										case HoverContext.ScaleTopRight:
										case HoverContext.ScaleBottomLeft:
										case HoverContext.ScaleBottomRight:
										case HoverContext.ScaleLeft:
										case HoverContext.ScaleTop:
										case HoverContext.ScaleRight:
										case HoverContext.ScaleBottom:
											//flip context according to the current scale
											if (_selectedObject.ScaleX < 0)
											{
												if (context.HasFlag(HoverContext.ScaleLeft))
												{
													context &= ~HoverContext.ScaleLeft;
													context |= HoverContext.ScaleRight;
												}
												else if (context.HasFlag(HoverContext.ScaleRight))
												{
													context &= ~HoverContext.ScaleRight;
													context |= HoverContext.ScaleLeft;
												}
											}
											if (_selectedObject.ScaleY < 0)
											{
												if (context.HasFlag(HoverContext.ScaleTop))
												{
													context &= ~HoverContext.ScaleTop;
													context |= HoverContext.ScaleBottom;
												}
												else if (context.HasFlag(HoverContext.ScaleBottom))
												{
													context &= ~HoverContext.ScaleBottom;
													context |= HoverContext.ScaleTop;
												}
											}
											_moveContext = context;
											_state = CanvasState.Scaling;
											break;
										case HoverContext.Rotate:
											_state = CanvasState.Rotating;
											break;
										case HoverContext.Pivot:
											_state = CanvasState.MovingPivot;
											break;
									}
								}
							}
							break;
						case CanvasState.MovingObject:
							//get difference from screen downPoint
							int offsetX = screenPt.X - _downPoint.X;
							int offsetY = screenPt.Y - _downPoint.Y;

							Rectangle rect = _selectedObject.ToScreenRegion(canvas.Width, canvas.Height, _canvasOffset);

							//convert this to world space
							float screenUnitsPerWorldX = rect.Width / (float)_selectedObject.Width / _selectedObject.ScaleX;
							float screenUnitsPerWorldY = rect.Height / (float)_selectedObject.Height / _selectedObject.ScaleY;
							int x = (int)(offsetX / screenUnitsPerWorldX + _startDragPosition.X);
							int y = (int)(offsetY / screenUnitsPerWorldY + _startDragPosition.Y);

							foreach (object obj in _selectedObject.AdjustPosition(x, y))
							{
								UpdateNode(obj);
								if (table.Data == obj)
								{
									table.UpdateProperty("X");
									table.UpdateProperty("Y");
								}
								canvas.Invalidate();
							}
							break;
						case CanvasState.MovingPivot:
							//figure out where new pivot position is in relation to object bounds
							RectangleF pivotRect = _selectedObject.ToUnscaledScreenRegion(canvas.Width, canvas.Height, _canvasOffset);
							if (_selectedObject.AdjustPivot(screenPt, pivotRect) && _selectedObject.Sprite != null)
							{
								UpdateNode(_selectedObject.Sprite);
								if (_selectedObject.Sprite == table.Data)
								{
									table.UpdateProperty("PivotX");
									table.UpdateProperty("PivotY");
								}
								canvas.Invalidate();
							}
							break;
						case CanvasState.Scaling:
							foreach (object obj in _selectedObject.AdjustScale(screenPt, canvas.Width, canvas.Height, _canvasOffset, _downPoint, _moveContext, ModifierKeys.HasFlag(Keys.Shift)))
							{
								UpdateNode(obj);
								if (obj == table.Data)
								{
									table.UpdateProperty("ScaleX");
									table.UpdateProperty("ScaleY");
								}
								canvas.Invalidate();
							}
							break;
						case CanvasState.Rotating:
							Point center = _selectedObject.ToScreenCenter(canvas.Width, canvas.Height, _canvasOffset);
							foreach (object obj in _selectedObject.AdjustRotation(screenPt, center))
							{
								UpdateNode(obj);
								if (obj == table.Data)
								{
									table.UpdateProperty("Rotation");
								}
								canvas.Invalidate();
							}
							break;
						case CanvasState.Panning:
							int dx = screenPt.X - _lastMouse.X;
							int dy = screenPt.Y - _lastMouse.Y;
							if (dx != 0 || dy != 0)
							{
								if (dx != 0)
								{
									int moveX = dx;
									if (moveX == 0)
									{
										moveX = (dx < 0 ? -1 : 1);
									}
									_canvasOffset.X += moveX;
								}
								if (dy != 0)
								{
									int moveY = dy;
									if (moveY == 0)
									{
										moveY = (dy < 0 ? -1 : 1);
									}
									_canvasOffset.Y += moveY;
								}
								canvas.Invalidate();
							}
							break;
					}
					_lastMouse = screenPt;
					if (_moveContext == HoverContext.Rotate || _moveContext == HoverContext.ArrowRight || _moveContext == HoverContext.ArrowLeft ||
						_moveContext == HoverContext.ArrowDown || _moveContext == HoverContext.ArrowUp || _moveContext == HoverContext.Pivot)
					{
						canvas.Invalidate();
					}
					break;
			}
		}

		private void canvas_MouseUp(object sender, MouseEventArgs e)
		{
			if (_preview == null) { return; }
			_moveContext = HoverContext.None;
			canvas.Cursor = Cursors.Default;
			switch (_state)
			{
				case CanvasState.MovingObject:
				case CanvasState.Resizing:
				case CanvasState.Rotating:
				case CanvasState.Scaling:
				case CanvasState.Panning:
				case CanvasState.MovingPivot:
					_state = CanvasState.Normal;
					canvas.Invalidate();
					break;
			}
		}

		private void cmdFit_Click(object sender, EventArgs e)
		{
			_canvasOffset = new Point(0, 0);
			canvas.Invalidate();
		}

		private void cmdMarkers_Click(object sender, EventArgs e)
		{
			MarkerSetup form = new MarkerSetup();
			form.SetData(_character.Character, _markers);
			if (form.ShowDialog() == DialogResult.OK)
			{
				_markers = form.Markers;
				BuildPreview();
			}
		}
	}

	public class PoseContext : ICharacterContext, IAutoCompleteList
	{
		public Pose Pose { get; }
		public ISkin Character { get; }
		public CharacterContext Context { get; }

		public PoseContext(Pose pose, ISkin character, CharacterContext context)
		{
			Pose = pose;
			Character = character;
			Context = context;
		}

		public string[] GetAutoCompleteList(object data)
		{
			if (data is PoseDirective)
			{
				HashSet<string> items = new HashSet<string>();
				foreach (Sprite sprite in Pose.Sprites)
				{
					items.Add(sprite.Id);
				}
				return items.ToArray();
			}
			return null;
		}
	}
}
