using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls
{
	public partial class SceneTree : UserControl
	{
		private Epilogue _epilogue;
		/// <summary>
		/// mapping of tag to node
		/// </summary>
		private Dictionary<object, TreeNode> _nodes = new Dictionary<object, TreeNode>();
		private int _id;

		public event EventHandler<SceneTreeEventArgs> AfterSelect;

		public SceneTree()
		{
			InitializeComponent();

			treeScenes.KeyDown += TreeScenes_KeyDown;
		}

		private void RemoveSelectionHandler()
		{
			treeScenes.AfterSelect -= TreeScenes_AfterSelect;
		}

		private void AddSelectionHandler()
		{
			treeScenes.AfterSelect += TreeScenes_AfterSelect;
		}

		public void SetData(Epilogue epilogue)
		{
			_epilogue = epilogue;
			Enabled = (epilogue != null);

			RemoveSelectionHandler();
			treeScenes.Nodes.Clear();
			if (epilogue == null) { return; }

			treeScenes.BeginUpdate();

			AddSelectionHandler();
			foreach (Scene scene in _epilogue.Scenes)
			{
				TreeNode sceneNode = new TreeNode(scene.ToString());
				_nodes[scene] = sceneNode;
				sceneNode.Tag = scene;
				treeScenes.Nodes.Add(sceneNode);
				foreach (Directive directive in scene.Directives)
				{
					TreeNode dirNode = new TreeNode(directive.ToString());
					_nodes[directive] = dirNode;
					dirNode.Tag = directive;
					sceneNode.Nodes.Add(dirNode);

					foreach (Keyframe kf in directive.Keyframes)
					{
						TreeNode keyNode = new TreeNode(kf.ToString());
						_nodes[kf] = keyNode;
						keyNode.Tag = kf;
						dirNode.Nodes.Add(keyNode);
					}
				}
			}

			treeScenes.ExpandAll();

			treeScenes.EndUpdate();
			if (treeScenes.Nodes.Count > 0)
			{
				treeScenes.SelectedNode = treeScenes.Nodes[0];
			}
		}

		/// <summary>
		/// Selects a node in the tree
		/// </summary>
		/// <param name="id"></param>
		public void SelectNode(Scene scene, Directive directive, Keyframe keyframe)
		{
			TreeNode node = null;
			if (keyframe != null)
			{
				_nodes.TryGetValue(keyframe, out node);
			}
			else if (directive != null)
			{
				_nodes.TryGetValue(directive, out node);
			}
			else if (scene != null)
			{
				_nodes.TryGetValue(scene, out node);
			}
			if (node != null)
			{
				treeScenes.SelectedNode = node;
			}
		}

		private void TreeScenes_AfterSelect(object sender, TreeViewEventArgs e)
		{
			TreeNode node = treeScenes.SelectedNode;
			Scene scene = node.Tag as Scene;
			Directive directive = node.Tag as Directive;
			Keyframe keyframe = node.Tag as Keyframe;

			if (scene != null)
			{
				tsAddDirective.Enabled = true;
				tsAddKeyframe.Enabled = false;
			}
			else if (directive != null)
			{
				tsAddDirective.Enabled = true;
				DirectiveDefinition def = Definitions.Instance.Get<DirectiveDefinition>(directive.DirectiveType);
				tsAddKeyframe.Enabled = (def != null && def.IsAnimatable);
			}
			else if (keyframe != null)
			{
				tsAddDirective.Enabled = false;
				tsAddKeyframe.Enabled = true;
			}

			AfterSelect?.Invoke(this, new SceneTreeEventArgs(node));
		}

		private void TreeScenes_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
			{
				DeleteSelectedNode();
			}
		}

		private void TsAdd_ButtonClick(object sender, EventArgs e)
		{
			AddScene();
		}

		private void TsAddKeyframe_Click(object sender, EventArgs e)
		{
			AddKeyframe();
		}

		private void TsAddDirective_ButtonClick(object sender, EventArgs e)
		{
			tsAddDirective.ShowDropDown();
		}

		private void AddDirectiveToolstripItem_Click(object sender, EventArgs e)
		{
			ToolStripItem ctl = sender as ToolStripItem;
			string tag = ctl.Tag?.ToString();
			if (!string.IsNullOrEmpty(tag))
			{
				AddDirective(tag);
			}
		}

		private void TsRemove_Click(object sender, EventArgs e)
		{
			DeleteSelectedNode();
		}

		public void DeleteSelectedNode()
		{
			TreeNode node = treeScenes.SelectedNode;
			if (node == null) { return; }
			string nodeType = (node.Tag is Scene ? "scene" : node.Tag is Directive ? "directive" : "keyframe");
			if (MessageBox.Show($"Are you sure you want to remove this {nodeType}?", $"Remove {nodeType}", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
			{
				return;
			}
			RemoveNode(treeScenes.SelectedNode);
		}

		private void TsUp_Click(object sender, EventArgs e)
		{
			MoveUp(treeScenes.SelectedNode);
		}

		private void TsDown_Click(object sender, EventArgs e)
		{
			MoveDown(treeScenes.SelectedNode);
		}

		private void TreeScenes_ItemDrag(object sender, ItemDragEventArgs e)
		{
			TreeNode node = e.Item as TreeNode;
			if (node != null && node.Tag is Scene)
			{
				treeScenes.CollapseAll();
			}
			DoDragDrop(e.Item, DragDropEffects.Move);
		}

		private void TreeScenes_DragEnter(object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.Move;
		}

		private void TreeScenes_DragLeave(object sender, EventArgs e)
		{
			lblDragger.Visible = false;
		}

		private void TreeScenes_QueryContinueDrag(object sender, QueryContinueDragEventArgs e)
		{
			if (e.Action == DragAction.Cancel || e.Action == DragAction.Drop)
			{
				lblDragger.Visible = false;
			}
		}

		private void TreeScenes_DragOver(object sender, DragEventArgs e)
		{
			treeScenes.Scroll();

			TreeNode dragNode = (TreeNode)e.Data.GetData(typeof(TreeNode));
			if (dragNode == null) { return; }
			bool draggingDirective = dragNode.Tag is Directive;
			bool draggingKeyframe = dragNode.Tag is Keyframe && !draggingDirective;

			Point targetPoint = treeScenes.PointToClient(new Point(e.X, e.Y));
			TreeNode targetNode = treeScenes.GetNodeAt(targetPoint);
			bool targetDirective = targetNode?.Tag is Directive;


			if (targetNode == null || (draggingKeyframe && (targetNode == null || targetNode.Parent != dragNode.Parent)))
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
				//dragging a directive on top of a scene. always insert below
				Point pt = lblDragger.Location;
				pt.Y = targetNode.Bounds.Y + targetNode.Bounds.Height;
				lblDragger.Location = pt;
				lblDragger.Visible = true;
			}
			else
			{
				if (targetPoint.Y - targetNode.Bounds.Height / 2 < targetNode.Bounds.Y - 2)
				{
					//hovering on the upper half of a node
					Point pt = lblDragger.Location;
					pt.Y = targetNode.Bounds.Y;
					lblDragger.Location = pt;
					lblDragger.Visible = true;
				}
				else
				{
					//hovering on the lower half
					Point pt = lblDragger.Location;
					pt.Y = targetNode.Bounds.Y + targetNode.Bounds.Height;
					lblDragger.Location = pt;
					lblDragger.Visible = true;
				}
			}
			RemoveSelectionHandler();
			treeScenes.SelectedNode = dragNode;
			AddSelectionHandler();
		}

		private void TreeScenes_DragDrop(object sender, DragEventArgs e)
		{
			lblDragger.Visible = false;

			TreeNode dragNode = (TreeNode)e.Data.GetData(typeof(TreeNode));
			if (dragNode == null) { return; }
			bool draggingDirective = dragNode.Tag is Directive;
			bool draggingKeyframe = dragNode.Tag is Keyframe && !draggingDirective;

			Point targetPoint = treeScenes.PointToClient(new Point(e.X, e.Y));
			TreeNode targetNode = treeScenes.GetNodeAt(targetPoint);
			if (targetNode == null) { return; }
			bool targetDirective = targetNode.Tag is Directive;
			bool targetKeyframe = targetNode.Tag is Keyframe && !targetDirective;

			if (!dragNode.Equals(targetNode))
			{
				if (draggingDirective)
				{
					//dragging a directive

					if (targetKeyframe)
					{
						MoveDirectiveNode(targetNode.Parent.Parent, dragNode, targetNode.Parent.Index + 1);
					}
					else if (!targetDirective)
					{
						//insert at start of target scene
						MoveDirectiveNode(targetNode, dragNode, 0);
					}
					else
					{
						if (targetPoint.Y - targetNode.Bounds.Height / 2 < targetNode.Bounds.Y - 2)
						{
							if (targetNode.PrevNode == null)
							{
								//insert at first position in scene
								MoveDirectiveNode(targetNode.Parent, dragNode, 0);
							}
							else
							{
								//insert beneath previous node
								MoveDirectiveNode(targetNode.Parent, dragNode, targetNode.Index);
							}
						}
						else
						{
							//insert beneath target node
							MoveDirectiveNode(targetNode.Parent, dragNode, targetNode.Index + 1);
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
							MoveKeyframeNode(dragNode, targetNode.Index);
						}
						else
						{
							MoveKeyframeNode(dragNode, targetNode.Index + 1);
						}
					}
				}
				else
				{
					//dragging a scene

					if (targetPoint.Y - targetNode.Bounds.Height / 2 < targetNode.Bounds.Y - 2)
					{
						//insert above target scene
						MoveSceneNode(dragNode, targetNode.Index);
					}
					else
					{
						if (targetDirective)
						{
							//this shouldn't happen since the tree collapses directives, but if it does, use the scene node
							targetNode = targetNode.Parent;
						}

						//insert behind target scene
						MoveSceneNode(dragNode, targetNode.Index + 1);
					}
				}
			}
		}

		/// <summary>
		/// Moves an existing scene node to a new position
		/// </summary>
		/// <param name="sceneNode"></param>
		/// <param name="index"></param>
		private void MoveSceneNode(TreeNode sceneNode, int index)
		{
			//if moving higher, adjust the index to account for the scene being removed
			if (sceneNode.Index < index)
			{
				index--;
			}

			//remove the node from its old position
			RemoveNode(sceneNode);

			Scene scene = sceneNode.Tag as Scene;
			_epilogue.Scenes.Insert(index, scene);
			treeScenes.Nodes.Insert(index, sceneNode);

			//auto-select it
			treeScenes.SelectedNode = sceneNode;
		}

		/// <summary>
		/// Moves an existing node to a position under the desired scene
		/// </summary>
		/// <param name="sceneNode">Scene node to host the directive</param>
		/// <param name="directiveNode">Directive node to move</param>
		/// <param name="index">Index beneath the scene in which to insert the directive</param>
		private void MoveDirectiveNode(TreeNode sceneNode, TreeNode directiveNode, int index)
		{
			//if moving higher within the same scene, adjust the index to account for the directive being removed
			if (directiveNode.Parent == sceneNode && directiveNode.Index < index)
			{
				index--;
			}

			//get rid of the directive
			RemoveNode(directiveNode);

			//add it to the appropriate location both within the tree and the data model
			Scene targetScene = sceneNode.Tag as Scene;
			targetScene.Directives.Insert(index, directiveNode.Tag as Directive);
			sceneNode.Nodes.Insert(index, directiveNode);

			//auto-select it
			treeScenes.SelectedNode = directiveNode;
		}

		/// <summary>
		/// Moves a keyframe to a new position under its directive
		/// </summary>
		/// <param name="node"></param>
		/// <param name="index"></param>
		private void MoveKeyframeNode(TreeNode node, int index)
		{
			TreeNode dirNode = node.Parent;
			if (node.Index < index)
			{
				index--;
			}

			RemoveNode(node);

			Directive directive = dirNode.Tag as Directive;
			directive.Keyframes.Insert(index, node.Tag as Keyframe);
			dirNode.Nodes.Insert(index, node);

			//auto-selectit
			treeScenes.SelectedNode = node;
		}

		private Scene GetSelectedScene()
		{
			TreeNode node = treeScenes.SelectedNode;
			while (node != null && !(node.Tag is Scene))
			{
				node = node.Parent;
			}
			if (node != null)
			{
				return node.Tag as Scene;
			}
			return null;
		}

		/// <summary>
		/// Finds the tree node associated with a scene
		/// </summary>
		/// <param name="scene"></param>
		/// <returns></returns>
		private TreeNode FindNode(Scene scene)
		{
			foreach (TreeNode node in treeScenes.Nodes)
			{
				if (node.Tag == scene)
				{
					return node;
				}
			}
			return null;
		}

		private void AddScene()
		{
			Scene scene = GetSelectedScene();

			Scene newScene = new Scene(100, 100);
			TreeNode node = new TreeNode(newScene.ToString());
			_nodes[newScene] = node;
			node.Tag = newScene;

			if (scene != null)
			{
				int index = _epilogue.Scenes.IndexOf(scene);
				treeScenes.Nodes.Insert(index + 1, node);
				_epilogue.Scenes.Insert(index + 1, scene);
			}
			else
			{
				_epilogue.Scenes.Add(scene);
				treeScenes.Nodes.Add(node);
			}
			treeScenes.SelectedNode = node;
		}

		private void AddDirective(string type)
		{
			Scene scene = GetSelectedScene();
			if (scene == null) { return; }

			Directive dir = new Directive(type);
			ApplyDefaults(dir);
			TreeNode node = new TreeNode(dir.ToString());
			_nodes[dir] = node;
			node.Tag = dir;

			TreeNode selected = treeScenes.SelectedNode;
			if (selected != null)
			{
				if (selected.Tag is Scene)
				{
					//add to end of selected scene
					scene.Directives.Add(dir);
					selected.Nodes.Add(node);
				}
				else if (selected.Tag is Directive)
				{
					//add after selected directive
					scene.Directives.Insert(selected.Index + 1, dir);
					selected.Parent.Nodes.Insert(selected.Index + 1, node);
				}

				treeScenes.SelectedNode = node;
			}
		}

		/// <summary>
		/// Applies sensible default values to a directive based on its type
		/// </summary>
		/// <param name="directive"></param>
		private void ApplyDefaults(Directive directive)
		{
			switch (directive.DirectiveType)
			{
				case "sprite":
					directive.Id = $"sprite{++_id}";
					directive.X = "0";
					directive.Y = "0";
					break;
				case "text":
					directive.Id = $"text{++_id}";
					directive.X = "0";
					directive.Y = "0";
					directive.Width = "20%";
					directive.Text = "New text";
					break;
			}
		}

		private void AddKeyframe()
		{
			TreeNode selectedNode = treeScenes.SelectedNode;
			if (selectedNode == null || selectedNode.Tag is Scene) { return; }

			Keyframe frame = new Keyframe();
			frame.Time = "1";
			TreeNode node = new TreeNode(frame.ToString());
			node.Tag = frame;
			_nodes[frame] = node;

			Directive directive = selectedNode.Tag as Directive;
			if (directive != null)
			{
				//adding to a directive, so insert it at the bottom

				if (directive.Keyframes.Count == 0)
				{
					//this is the first keyframe, so convert the directive into a keyframe if it has anything animatable set
					if (directive.HasAnimatableProperties())
					{
						Keyframe kf = new Keyframe();
						kf.TransferPropertiesFrom(directive);
						kf.Directive = directive;
						TreeNode transferNode = new TreeNode(kf.ToString());
						transferNode.Tag = kf;
						_nodes[kf] = transferNode;
						directive.Keyframes.Add(kf);
						selectedNode.Text = directive.ToString();
						selectedNode.Nodes.Add(transferNode);
					}
				}

				frame.Time = directive.Duration ?? frame.Time;
				node.Text = frame.ToString();
				selectedNode.Nodes.Add(node);
				directive.Keyframes.Add(frame);
			}
			else
			{
				//adding to a keyframe, so add next to it
				Keyframe existing = selectedNode.Tag as Keyframe;
				frame.Time = existing.Time;
				node.Text = frame.ToString();
				directive = selectedNode.Parent.Tag as Directive;
				selectedNode.Parent.Nodes.Insert(selectedNode.Index + 1, node);
				directive.Keyframes.Insert(selectedNode.Index + 1, frame);
			}
			frame.Directive = directive;

			treeScenes.SelectedNode = node;
		}


		/// <summary>
		/// Deletes a node
		/// </summary>
		/// <param name="node"></param>
		private void RemoveNode(TreeNode node)
		{
			if (node == null) { return; }
			Scene scene = node.Tag as Scene;
			if (scene != null)
			{
				_epilogue.Scenes.Remove(scene);
				treeScenes.Nodes.Remove(node);
				_nodes.Remove(scene);
				return;
			}

			Directive dir = node.Tag as Directive;
			if (dir != null)
			{
				scene = node.Parent.Tag as Scene;
				scene.Directives.Remove(dir);
				node.Parent.Nodes.Remove(node);
				_nodes.Remove(dir);
				return;
			}

			Keyframe kf = node.Tag as Keyframe;
			if (kf != null)
			{
				dir = node.Parent.Tag as Directive;
				dir.Keyframes.Remove(kf);
				node.Parent.Nodes.Remove(node);
				_nodes.Remove(kf);
			}
		}

		/// <summary>
		/// Moves a node upwards
		/// </summary>
		/// <param name="node"></param>
		private void MoveUp(TreeNode node)
		{
			if (node.Index == 0)
			{
				return;
			}

			Scene scene = node.Tag as Scene;
			if (scene != null)
			{
				MoveSceneNode(node, node.Index - 1);
				return;
			}

			Directive dir = node.Tag as Directive;
			if (dir != null)
			{
				MoveDirectiveNode(node.Parent, node, node.Index - 1);
				return;
			}

			Keyframe keyframe = node.Tag as Keyframe;
			if (keyframe != null)
			{
				MoveKeyframeNode(node, node.Index - 1);
				return;
			}
		}

		/// <summary>
		/// Moves a node upwards
		/// </summary>
		/// <param name="node"></param>
		private void MoveDown(TreeNode node)
		{
			if ((node.Parent == null && node.Index == treeScenes.Nodes.Count - 1) || (node.Parent != null && node.Index == node.Parent.Nodes.Count - 1))
			{
				return;
			}

			Scene scene = node.Tag as Scene;
			if (scene != null)
			{
				MoveSceneNode(node, node.Index + 2);
				return;
			}

			Directive dir = node.Tag as Directive;
			if (dir != null)
			{
				MoveDirectiveNode(node.Parent, node, node.Index + 2);
				return;
			}

			Keyframe keyframe = node.Tag as Keyframe;
			if (keyframe != null)
			{
				MoveKeyframeNode(node, node.Index + 2);
				return;
			}
		}

		/// <summary>
		/// Updates the node text for the node with the given data
		/// </summary>
		/// <param name="data"></param>
		public void UpdateNode(object data)
		{
			if (data == null) { return; }
			TreeNode node;
			if (_nodes.TryGetValue(data, out node))
			{
				node.Text = data.ToString();
			}
		}
	}

	public class SceneTreeEventArgs
	{
		public Scene Scene;
		public Directive Directive;
		public Keyframe Keyframe;

		public SceneTreeEventArgs(TreeNode node)
		{
			if (node == null)
			{
				return;
			}
			Scene = node.Tag as Scene;
			if (Scene == null)
			{
				Directive = node.Tag as Directive;
				if (Directive == null)
				{
					Keyframe = node.Tag as Keyframe;
					Directive = node.Parent.Tag as Directive;
					Scene = node.Parent.Parent.Tag as Scene;
				}
				else
				{
					Scene = node.Parent.Tag as Scene;
				}
			}
		}
	}
}
