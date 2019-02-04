using Desktop;
using SPNATI_Character_Editor.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Activities
{
	[Activity(typeof(Character), 210)]
	[Activity(typeof(Costume), 210)]
	public partial class PoseCreator : Activity
	{
		private CharacterEditorData _editorData;
		private ISkin _character;
		private ImageLibrary _library;

		private Pose _currentPose;
		private Sprite _currentSprite;
		private PoseDirective _currentDirective;
		private Keyframe _currentKeyframe;
		private PoseAnimFrame _currentAnimFrame;
		private Dictionary<object, TreeNode> _nodes = new Dictionary<object, TreeNode>();

		private object _clipboard;

		private bool _populatingPoses;

		public PoseCreator()
		{
			InitializeComponent();
		}

		public override string Caption
		{
			get { return "Pose Maker"; }
		}

		protected override void OnInitialize()
		{
			_character = Record as ISkin;
			_library = ImageLibrary.Get(_character);
			_editorData = CharacterDatabase.GetEditorData(_character.Character);
			table.Context = new PoseContext(_character, CharacterContext.Pose);
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
			table.Data = node.Tag;

			tsAddKeyframe.Enabled = (_currentDirective != null);

			preview.SetImage(new CharacterImage(_currentPose));
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
				case "delay":
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
			preview.SetImage(new CharacterImage(_currentPose));
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

		private void SelectNode(Pose pose)
		{
			TreeNode node;
			if (_nodes.TryGetValue(pose, out node))
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
			PoseAnimFrame pastedAnim = obj as PoseAnimFrame;

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
			if (node != null && node.Tag is Pose)
			{
				lstPoses.CollapseAll();
			}
			DoDragDrop(e.Item, DragDropEffects.Move);
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
				Pose pose = targetNode.Tag as Pose;
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
			Sprite sprite = new Sprite();
			_currentPose.Sprites.Add(sprite);
			lstPoses.SelectedNode = AddSprite(_currentPose, sprite);
		}

		private void addAnimationToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (_currentPose == null)
			{
				return;
			}
			PoseDirective directive = new PoseDirective();
			directive.DirectiveType = "animation";
			_currentPose.Directives.Add(directive);
			lstPoses.SelectedNode = AddDirective(_currentPose, directive);
		}

		private void tsAddKeyframe_Click(object sender, EventArgs e)
		{
			if (_currentDirective == null)
			{
				return;
			}
			Keyframe frame = new Keyframe();
			_currentDirective.Keyframes.Add(frame);
			lstPoses.SelectedNode = AddKeyframe(_currentDirective, frame);
		}
	}

	public class PoseContext : ICharacterContext
	{
		public ISkin Character { get; }
		public CharacterContext Context { get; }

		public PoseContext(ISkin character, CharacterContext context)
		{
			Character = character;
			Context = context;
		}
	}
}
