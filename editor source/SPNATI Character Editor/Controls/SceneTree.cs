using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls
{
	public partial class SceneTree : UserControl
	{
		private Character _character;
		private Epilogue _epilogue;
		/// <summary>
		/// mapping of tag to node
		/// </summary>
		private Dictionary<object, TreeNode> _nodes = new Dictionary<object, TreeNode>();
		private int _id;
		private object _clipboard;

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

		public void SetData(Epilogue epilogue, Character character)
		{
			_character = character;
			_epilogue = epilogue;
			Enabled = (epilogue != null);

			RemoveSelectionHandler();
			treeScenes.Nodes.Clear();
			if (epilogue == null) { return; }

			treeScenes.BeginUpdate();

			AddSelectionHandler();
			foreach (Scene scene in _epilogue.Scenes)
			{
				BuildSceneNode(scene);
			}

			if (!Config.CollapseEpilogueScenes)
			{
				treeScenes.ExpandAll();
			}

			treeScenes.EndUpdate();
			if (treeScenes.Nodes.Count > 0)
			{
				treeScenes.SelectedNode = treeScenes.Nodes[0];
			}
		}

		private TreeNode BuildSceneNode(Scene scene)
		{
			TreeNode sceneNode = new TreeNode(scene.ToString());
			_nodes[scene] = sceneNode;
			sceneNode.Tag = scene;
			treeScenes.Nodes.Add(sceneNode);
			foreach (Directive directive in scene.Directives)
			{
				BuildDirectiveNode(sceneNode, directive);
			}
			return sceneNode;
		}

		private TreeNode BuildDirectiveNode(TreeNode sceneNode, Directive directive)
		{
			TreeNode dirNode = new TreeNode(directive.ToString());
			_nodes[directive] = dirNode;
			dirNode.Tag = directive;
			sceneNode.Nodes.Add(dirNode);

			foreach (Keyframe kf in directive.Keyframes)
			{
				BuildKeyframeNode(dirNode, kf);
			}
			foreach (Choice c in directive.Choices)
			{
				BuildChoiceNode(dirNode, c);
			}
			return dirNode;
		}

		private TreeNode BuildKeyframeNode(TreeNode dirNode, Keyframe kf)
		{
			TreeNode keyNode = new TreeNode(kf.ToString());
			_nodes[kf] = keyNode;
			keyNode.Tag = kf;
			dirNode.Nodes.Add(keyNode);
			return keyNode;
		}

		private TreeNode BuildChoiceNode(TreeNode dirNode, Choice choice)
		{
			TreeNode choiceNode = new TreeNode(choice.ToString());
			_nodes[choice] = choiceNode;
			choiceNode.Tag = choice;
			dirNode.Nodes.Add(choiceNode);
			return choiceNode;
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
			Choice choice = node.Tag as Choice;

			if (scene != null)
			{
				EnableMenu(tsAddDirective, !scene.Transition);
				EnableMenu(tsAddKeyframe, false);
			}
			else if (directive != null)
			{
				EnableMenu(tsAddDirective, true);
				DirectiveDefinition def = Definitions.Instance.Get<DirectiveDefinition>(directive.DirectiveType);
				EnableMenu(tsAddKeyframe, def != null && (def.IsAnimatable || directive.DirectiveType == "prompt"));
			}
			else if (keyframe != null)
			{
				EnableMenu(tsAddDirective, false);
				EnableMenu(tsAddKeyframe, true);
			}
			else if (choice != null)
			{
				EnableMenu(tsAddDirective, false);
				EnableMenu(tsAddKeyframe, true);
			}

			AfterSelect?.Invoke(this, new SceneTreeEventArgs(node));
		}

		/// <summary>
		/// Enables a menu and all items within since disabling the top-level menu doesn't disable shortcuts of sub-items.
		/// </summary>
		/// <param name="menu"></param>
		/// <param name="enabled"></param>
		private void EnableMenu(ToolStripItem menu, bool enabled)
		{
			menu.Enabled = enabled;
			ToolStripDropDownItem dropDownItem = menu as ToolStripDropDownItem;
			if (dropDownItem != null)
			{
				foreach (ToolStripItem item in dropDownItem.DropDownItems)
				{
					EnableMenu(item, enabled);
				}
			}
		}

		private void TreeScenes_KeyDown(object sender, KeyEventArgs e)
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

		private void TsAdd_ButtonClick(object sender, EventArgs e)
		{
			AddScene();
		}

		private void tsAddTransition_Click(object sender, EventArgs e)
		{
			AddSceneTransition();
		}

		private void TsAddKeyframe_Click(object sender, EventArgs e)
		{
			TreeNode selectedNode = treeScenes.SelectedNode;
			Directive directive = selectedNode?.Tag as Directive;
			if (directive == null)
			{
				directive = selectedNode.Parent?.Tag as Directive;
				if (directive == null)
				{
					return;
				}
			}
			if (directive.DirectiveType == "prompt")
			{
				AddChoice();
			}
			else
			{
				AddKeyframe();
			}
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
				AddDirective(tag, true);
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
			string nodeType = (node.Tag is Scene ? (((Scene)node.Tag).Transition ? "transition" : "scene") : node.Tag is Choice ? "choice" : node.Tag is Directive ? "directive" : "keyframe");
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
			bool draggingChoice = dragNode.Tag is Choice;

			Point targetPoint = treeScenes.PointToClient(new Point(e.X, e.Y));
			TreeNode targetNode = treeScenes.GetNodeAt(targetPoint);
			bool targetDirective = targetNode?.Tag is Directive;


			if (targetNode == null ||
				(draggingKeyframe && (targetNode == null || targetNode.Parent != dragNode.Parent)) ||
				 (draggingChoice && (targetNode == null || targetNode.Parent != dragNode.Parent)))
			{
				//keyframes and choices can only be dragged within their original parent
				e.Effect = DragDropEffects.None;
				lblDragger.Visible = false;
				return;
			}
			else
			{
				Scene scene = targetNode.Tag as Scene;
				if (scene != null && scene.Transition && draggingDirective)
				{
					//can't drag things into a transition
					e.Effect = DragDropEffects.None;
					lblDragger.Visible = false;
					return;
				}
				e.Effect = DragDropEffects.Move;
			}

			if (draggingDirective && !targetDirective)
			{
				//dragging a directive on top of a scene. always insert below
				Point pt = lblDragger.Location;
				pt.Y = treeScenes.Top + targetNode.Bounds.Y + targetNode.Bounds.Height;
				lblDragger.Location = pt;
				lblDragger.Visible = true;
			}
			else
			{
				if (targetPoint.Y - targetNode.Bounds.Height / 2 < targetNode.Bounds.Y - 2)
				{
					//hovering on the upper half of a node
					Point pt = lblDragger.Location;
					pt.Y = treeScenes.Top + targetNode.Bounds.Y;
					lblDragger.Location = pt;
					lblDragger.Visible = true;
				}
				else
				{
					//hovering on the lower half
					Point pt = lblDragger.Location;
					pt.Y = treeScenes.Top + targetNode.Bounds.Y + targetNode.Bounds.Height;
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
			bool draggingChoice = dragNode.Tag is Choice;

			Point targetPoint = treeScenes.PointToClient(new Point(e.X, e.Y));
			TreeNode targetNode = treeScenes.GetNodeAt(targetPoint);
			if (targetNode == null) { return; }
			bool targetDirective = targetNode.Tag is Directive;
			bool targetKeyframe = targetNode.Tag is Keyframe && !targetDirective;
			bool targetChoice = targetNode.Tag is Choice;

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
						Scene scene = targetNode.Tag as Scene;
						if (scene.Transition) { return; }

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
				else if (draggingChoice)
				{
					//dragging a choice
					if (targetChoice)
					{
						if (targetPoint.Y - targetNode.Bounds.Height / 2 < targetNode.Bounds.Y - 2)
						{
							MoveChoiceNode(dragNode, targetNode.Index);
						}
						else
						{
							MoveChoiceNode(dragNode, targetNode.Index + 1);
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

			InsertScene(sceneNode, index);
		}

		private void InsertScene(TreeNode sceneNode, int index)
		{
			//remove the node from its old position (which might be nowhere)
			RemoveNode(sceneNode);

			_nodes[sceneNode.Tag] = sceneNode;
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

			InsertDirective(sceneNode, directiveNode, index);
		}

		private void InsertDirective(TreeNode sceneNode, TreeNode directiveNode, int index)
		{
			//remove the node from its old position (which might be nowhere)
			RemoveNode(directiveNode);
			_nodes[directiveNode.Tag] = directiveNode;

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

			InsertKeyframe(node, index, dirNode);
		}

		private void InsertKeyframe(TreeNode node, int index, TreeNode dirNode)
		{
			//remove the node from its old position (which might be nowhere)
			RemoveNode(node);
			_nodes[node.Tag] = node;

			Directive directive = dirNode.Tag as Directive;
			Keyframe frame = node.Tag as Keyframe;
			frame.Directive = directive;
			directive.Keyframes.Insert(index, frame);
			dirNode.Nodes.Insert(index, node);

			//auto-select it
			treeScenes.SelectedNode = node;
		}

		/// <summary>
		/// Moves a choice to a new position under its directive
		/// </summary>
		/// <param name="node"></param>
		/// <param name="index"></param>
		private void MoveChoiceNode(TreeNode node, int index)
		{
			TreeNode dirNode = node.Parent;
			if (node.Index < index)
			{
				index--;
			}

			InsertChoice(node, index, dirNode);
		}

		private void InsertChoice(TreeNode node, int index, TreeNode dirNode)
		{
			//remove the node from its old position (which might be nowhere)
			RemoveNode(node);
			_nodes[node.Tag] = node;

			Directive directive = dirNode.Tag as Directive;
			Choice choice = node.Tag as Choice;
			choice.Directive = directive;
			directive.Choices.Insert(index, choice);
			dirNode.Nodes.Insert(index, node);

			//auto-select it
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
				_epilogue.Scenes.Insert(index + 1, newScene);
			}
			else
			{
				_epilogue.Scenes.Add(newScene);
				treeScenes.Nodes.Add(node);
			}
			treeScenes.SelectedNode = node;
		}

		private void AddSceneTransition()
		{
			Scene scene = GetSelectedScene();

			Scene transition = new Scene(true)
			{
				Transition = true
			};
			TreeNode node = new TreeNode(transition.ToString());
			_nodes[transition] = node;
			node.Tag = transition;

			if (scene != null)
			{
				int index = _epilogue.Scenes.IndexOf(scene);
				treeScenes.Nodes.Insert(index + 1, node);
				_epilogue.Scenes.Insert(index + 1, transition);
			}
			else
			{
				_epilogue.Scenes.Add(transition);
				treeScenes.Nodes.Add(node);
			}
			treeScenes.SelectedNode = node;
		}

		private void AddDirective(string type, bool autoSelect)
		{
			Scene scene = GetSelectedScene();
			if (scene == null) { return; }

			string src = null;
			if (type == "sprite")
			{
				if (openFileDialog.ShowDialog(_character, "") == DialogResult.OK)
				{
					src = openFileDialog.FileName;
				}
			}

			Directive dir = new Directive(type);
			ApplyDefaults(dir);
			if (src != null)
			{
				dir.Src = src;
			}
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

				if (autoSelect)
				{
					treeScenes.SelectedNode = node;
				}
			}

			if (Config.AutoPauseDirectives.Contains(type))
			{
				AddDirective("pause", false);
			}
		}

		/// <summary>
		/// Applies sensible default values to a directive based on its type
		/// </summary>
		/// <param name="directive"></param>
		private void ApplyDefaults(Directive directive)
		{
			string id = $"{directive.DirectiveType}{++_id}";
			while (_epilogue.Scenes.Find(s => s.Directives.Find(d => d.Id == id) != null) != null)
			{
				id = $"{directive.DirectiveType}{++_id}";
			}

			switch (directive.DirectiveType)
			{
				case "sprite":
					directive.Id = id;
					directive.X = "0";
					directive.Y = "0";
					directive.PivotX = "50%";
					directive.PivotY = "50%";
					break;
				case "text":
					directive.Id = id;
					directive.X = "0";
					directive.Y = "0";
					directive.Width = "20%";
					directive.Text = "New text";
					break;
				case "emitter":
					directive.Id = id;
					directive.X = "0";
					directive.Y = "0";
					directive.PivotX = "50%";
					directive.PivotY = "50%";
					directive.Rate = "1";
					directive.StartAlpha = "100";
					directive.EndAlpha = "0";
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
						//force selecting an empty node to update the host so that it doesn't save after the fact and overwrite the values we're changing immediately below
						treeScenes.SelectedNode = null;
						AfterSelect?.Invoke(this, new SceneTreeEventArgs(null));

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

		private void AddChoice()
		{
			TreeNode selectedNode = treeScenes.SelectedNode;
			if (selectedNode == null || selectedNode.Tag is Scene) { return; }

			Choice choice = new Choice();
			choice.Caption = "Button Caption";
			TreeNode node = new TreeNode(choice.ToString());
			node.Tag = choice;
			_nodes[choice] = node;

			Directive directive = selectedNode.Tag as Directive;
			if (directive != null)
			{
				//adding to a directive, so insert it at the bottom
				node.Text = choice.ToString();
				selectedNode.Nodes.Add(node);
				directive.Choices.Add(choice);
			}
			else
			{
				//adding to a choice, so add next to it
				node.Text = choice.ToString();
				directive = selectedNode.Parent.Tag as Directive;
				selectedNode.Parent.Nodes.Insert(selectedNode.Index + 1, node);
				directive.Choices.Insert(selectedNode.Index + 1, choice);
			}
			choice.Directive = directive;

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

			Choice choice = node.Tag as Choice;
			if (choice != null)
			{
				dir = node.Parent.Tag as Directive;
				dir.Choices.Remove(choice);
				node.Parent.Nodes.Remove(node);
				_nodes.Remove(choice);
			}
		}

		/// <summary>
		/// Moves a node upwards
		/// </summary>
		/// <param name="node"></param>
		private void MoveUp(TreeNode node)
		{
			if (node == null) { return; }
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

			Choice choice = node.Tag as Choice;
			if (choice != null)
			{
				MoveChoiceNode(node, node.Index - 1);
			}
		}

		/// <summary>
		/// Moves a node upwards
		/// </summary>
		/// <param name="node"></param>
		private void MoveDown(TreeNode node)
		{
			if (node == null) { return; }
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

			Choice choice = node.Tag as Choice;
			if (choice != null)
			{
				MoveChoiceNode(node, node.Index + 2);
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

		private void tsCut_Click(object sender, EventArgs e)
		{
			TreeNode node = treeScenes.SelectedNode as TreeNode;
			if (node != null)
			{
				_clipboard = node.Tag;
				RemoveNode(node);
			}
		}

		private void tsCopy_Click(object sender, EventArgs e)
		{
			TreeNode node = treeScenes.SelectedNode as TreeNode;
			if (node != null)
			{
				ICloneable cloner = node.Tag as ICloneable;
				_clipboard = cloner?.Clone();
			}
		}

		private void tsPaste_Click(object sender, EventArgs e)
		{
			TreeNode node = treeScenes.SelectedNode as TreeNode;
			if (_clipboard == null || node == null) { return; }
			ICloneable cloner = _clipboard as ICloneable;
			object obj = cloner.Clone();
			Scene pastedScene = obj as Scene;
			Directive pastedDirective = obj as Directive;
			Keyframe pastedFrame = obj as Keyframe;
			Choice pastedChoice = obj as Choice;

			Directive selectedDirective = node.Tag as Directive;
			Keyframe selectedFrame = node.Tag as Keyframe;
			Choice selectedChoice = node.Tag as Choice;

			TreeNode sceneNode = null;
			TreeNode dirNode = null;
			TreeNode frameNode = null;
			TreeNode choiceNode = null;

			if (selectedDirective != null)
			{
				sceneNode = node.Parent;
				dirNode = node;
			}
			else if (selectedFrame != null)
			{
				selectedDirective = node.Parent.Tag as Directive;
				sceneNode = node.Parent.Parent;
				dirNode = node.Parent;
				frameNode = node;
			}
			else if (selectedChoice != null)
			{
				selectedDirective = node.Parent.Tag as Directive;
				sceneNode = node.Parent.Parent;
				dirNode = node.Parent;
				choiceNode = node;
			}
			else
			{
				sceneNode = node;
			}

			//create the node hierachy
			TreeNode newNode = null;
			if (pastedScene != null)
			{
				newNode = BuildSceneNode(pastedScene);
			}
			else if (pastedDirective != null)
			{
				newNode = BuildDirectiveNode(sceneNode, pastedDirective);
			}
			else if (pastedFrame != null && dirNode != null)
			{
				newNode = BuildKeyframeNode(dirNode, pastedFrame);
			}
			else if (pastedChoice != null && dirNode != null)
			{
				newNode = BuildChoiceNode(dirNode, pastedChoice);
			}

			//insert the node into the correct location
			if (pastedScene != null)
			{
				InsertScene(newNode, sceneNode.Index + 1);
			}
			else if (pastedDirective != null)
			{
				if (dirNode == null)
				{
					//add to the end of the selected scene
					InsertDirective(sceneNode, newNode, sceneNode.Nodes.Count - 1);
				}
				else
				{
					//add after the selected directive
					InsertDirective(sceneNode, newNode, dirNode.Index + 1);
				}
			}
			else if (pastedFrame != null)
			{
				if (dirNode != null)
				{
					if (frameNode != null)
					{
						InsertKeyframe(newNode, frameNode.Index + 1, dirNode);
					}
					else
					{
						InsertKeyframe(newNode, dirNode.Nodes.Count - 1, dirNode);
					}
				}
			}
			else if (pastedChoice != null)
			{
				if (dirNode != null)
				{
					if (choiceNode != null)
					{
						InsertChoice(newNode, choiceNode.Index + 1, dirNode);
					}
					else
					{
						InsertChoice(newNode, dirNode.Nodes.Count - 1, dirNode);
					}
				}
			}
		}

		private void tsDuplicate_Click(object sender, EventArgs e)
		{
			object clipboard = _clipboard;
			tsCopy_Click(sender, e);
			tsPaste_Click(sender, e);
			_clipboard = clipboard;
		}

		private void tsLock_Click(object sender, EventArgs e)
		{
			TreeNode selectedNode = treeScenes.SelectedNode;
			if (selectedNode == null) { return; }
			Scene scene = selectedNode.Tag as Scene;
			if (scene != null)
			{
				if (!scene.Transition)
				{
					scene.Locked = !scene.Locked;
					UpdateNode(scene);
				}
				return;
			}
			Directive directive = selectedNode.Tag as Directive;
			if (directive != null)
			{
				if (directive.DirectiveType == "sprite" || directive.DirectiveType == "emitter" || directive.DirectiveType == "text")
				{
					directive.Locked = !directive.Locked;
					UpdateNode(directive);
				}
			}
		}

		private void tsCollapse_Click(object sender, EventArgs e)
		{
			Config.CollapseEpilogueScenes = true;
			treeScenes.CollapseAll();
		}

		private void tsExpandAll_Click(object sender, EventArgs e)
		{
			Config.CollapseEpilogueScenes = false;
			treeScenes.ExpandAll();
		}
	}

	public class SceneTreeEventArgs
	{
		public Scene Scene;
		public Directive Directive;
		public Keyframe Keyframe;
		public Choice Choice;

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
					Choice = node.Tag as Choice;
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
