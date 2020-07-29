using Desktop;
using Desktop.CommonControls.PropertyControls;
using SPNATI_Character_Editor.Actions;
using SPNATI_Character_Editor.Controls;
using SPNATI_Character_Editor.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.EpilogueEditor
{
	[Activity(typeof(Character), 210)]
	[Activity(typeof(Costume), 210)]
	public partial class PoseEditor : Activity
	{
		private ISkin _character;
		private LivePose _pose;
		private Pose _sourcePose;
		private int _stage;
		private UndoManager _history = new UndoManager();
		private float _time;
		private float _elapsedTime;
		private float _playbackTime;
		private bool _playing;
		private ILabel _labelData;

		public PoseEditor()
		{
			InitializeComponent();
			_history.CommandApplied += _history_CommandApplied;
			timeline.CommandHistory = _history;
			timeline.DataSelected += Timeline_DataSelected;
			timeline.TimeChanged += Timeline_TimeChanged;
			timeline.PlaybackTimeChanged += Timeline_PlaybackTimeChanged;
			timeline.ElapsedTimeChanged += Timeline_ElapsedTimeChanged;
			timeline.WidgetSelected += Timeline_WidgetSelected;
			timeline.PlaybackChanged += Timeline_PlaybackChanged;
			timeline.UIRequested += Timeline_UIRequested;
			//table.UndoManager = _history;
			table.RecordFilter = RecordFilter;

			canvas.UndoManager = _history;
			canvas.ObjectSelected += Canvas_ObjectSelected;
		}

		public override string Caption
		{
			get
			{
				return "Pose Maker";
			}
		}

		protected override void OnInitialize()
		{
			_character = Record as ISkin;
		}

		private void _history_CommandApplied(object sender, CommandEventArgs obj)
		{
			UpdateToolbar();
		}

		private void Timeline_PlaybackChanged(object sender, bool enabled)
		{
			_playing = enabled;
			canvas.SetPlayback(enabled);
			if (!_playing)
			{
				UpdateToolbar();
			}
		}

		private void Canvas_ObjectSelected(object sender, CanvasSelectionArgs args)
		{
			timeline.SelectWidgetWithData(args.Object);
		}

		private void Timeline_WidgetSelected(object sender, ITimelineObject widget)
		{
			canvas.SelectData(widget?.GetData());
		}

		private void Timeline_ElapsedTimeChanged(object sender, float time)
		{
			_elapsedTime = time;
			canvas.UpdateTime(_time, _playbackTime, _elapsedTime);
		}

		private void Timeline_PlaybackTimeChanged(object sender, float time)
		{
			_playbackTime = time;
			canvas.UpdateTime(_time, _playbackTime, _elapsedTime);
		}

		private void Timeline_TimeChanged(object sender, float time)
		{
			_time = time;
			canvas.UpdateTime(time, _playbackTime, _elapsedTime);
			UpdateToolbar();
		}

		protected override void OnFirstActivate()
		{
			UpdateToolbar();

			RebuildPoseList();
		}

		protected override void OnActivate()
		{
			Workspace.ToggleSidebar(false);
			_character.IsDirty = true;
		}

		protected override void OnDeactivate()
		{
			Workspace.ToggleSidebar(true);
		}

		public override void Save()
		{
			SavePose();
		}

		private void RebuildPoseList()
		{
			lstPoses.Items.Clear();
			foreach (Pose pose in _character.CustomPoses)
			{
				lstPoses.Items.Add(pose);
			}
			lstPoses.Sorted = true;

			if (lstPoses.Items.Count > 0)
			{
				lstPoses.SelectedIndex = 0;
			}
		}

		private void lstPoses_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			Pose newPose = lstPoses.SelectedItem as Pose;
			if (newPose == _sourcePose)
			{
				SetTableData(_pose, null);
			}
			else
			{
				if (_pose != null)
				{
					SavePose();
				}
				SetPose(lstPoses.SelectedItem as Pose);
			}
		}

		/// <summary>
		/// Saves a LivePose back into a definition
		/// </summary>
		private void SavePose()
		{
			if (_sourcePose == null || _pose == null)
			{
				return;
			}
			table.Save();

			//put the pose back into _sourcePose
			_sourcePose.CreateFrom(_pose);
		}

		private void SetPose(Pose pose)
		{
			HashSet<string> hiddenSprites = new HashSet<string>();
			HashSet<string> collapsedSprites = new HashSet<string>();
			if (_pose != null)
			{
				foreach (LiveSprite sprite in _pose.Sprites)
				{
					if (!string.IsNullOrEmpty(sprite.Id))
					{
						if (sprite.Hidden)
						{
							hiddenSprites.Add(sprite.Id);
						}
						if (sprite.Widget != null && sprite.Widget.IsCollapsed)
						{
							collapsedSprites.Add(sprite.Id);
						}
					}
				}
			}

			_history.Clear();
			_sourcePose = pose;
			if (pose != null)
			{
				_pose = new LivePose(_character, pose, _stage);
				_pose.CurrentStage = _stage;
			}
			else
			{
				_pose = null;
			}
			timeline.SetData(_pose);

			//restore collapsed and hidden states for sprites that have the same ID as previous pose
			if (_pose != null)
			{
				foreach (LiveSprite sprite in _pose.Sprites)
				{
					if (!string.IsNullOrEmpty(sprite.Id))
					{
						if (hiddenSprites.Contains(sprite.Id))
						{
							sprite.Hidden = true;
						}
						if (collapsedSprites.Contains(sprite.Id))
						{
							sprite.Widget.IsCollapsed = true;
						}
					}
				}

			}
			table.Context = new LivePoseContext(_pose, _character, CharacterContext.Pose);
			SetTableData(_pose, null);
			canvas.SetData(_character, _pose);
			timeline.CurrentTime = 0;
			UpdateEnabledFields();
		}

		private void Timeline_DataSelected(object sender, DataSelectionArgs data)
		{
			SetTableData(data.Data, data.PreviewData);
			UpdateToolbar();
		}

		private void SetTableData(object data, object previewData)
		{
			if (_labelData != null)
			{
				_labelData.LabelChanged -= _labelData_LabelChanged;
				_labelData = null;
			}
			table.SetDataAsync(data, previewData);
			_labelData = data as ILabel;
			if (_labelData != null)
			{
				_labelData.LabelChanged += _labelData_LabelChanged;
				lblDataCaption.Text = _labelData.GetLabel();
			}
			else
			{
				lblDataCaption.Text = data?.ToString();
			}
		}

		private void _labelData_LabelChanged(object sender, System.EventArgs e)
		{
			lblDataCaption.Text = _labelData.GetLabel();
			if (_labelData == _pose)
			{
				_sourcePose.Id = _pose.Id;
				_character.Character.PoseLibrary.Rename(_sourcePose);
				lstPoses.RefreshListItems();
			}
		}

		private bool RecordFilter(PropertyRecord record)
		{
			if (table.Data is LiveKeyframe)
			{
				return FilterKeyframeProperty(record);
			}
			return true;
		}

		private bool FilterKeyframeProperty(PropertyRecord record)
		{
			LiveKeyframe keyframe = table.Data as LiveKeyframe;
			if (record.Key == "time" && keyframe.Time == 0)
			{
				return false;
			}
			return true;
		}

		private void UpdateToolbar()
		{
			if (_playing) { return; }
			SpriteWidget selectedWidget = timeline.SelectedObject as SpriteWidget;

			tsRemoveSprite.Enabled = tsAddEndFrame.Enabled = (selectedWidget != null);
			tsCreateSequence.Enabled = true;
			tsAddKeyframe.Enabled = false;
			tsRemoveKeyframe.Enabled = false;
			tsTypeBegin.Enabled = tsTypeNormal.Enabled = tsTypeSplit.Enabled = false;
			if (selectedWidget != null)
			{
				tsRemoveSprite.Enabled = true;
				LiveKeyframe kf = selectedWidget.Sprite.Keyframes.Find(k => k.Time == _time);
				if (kf == null)
				{
					float start = selectedWidget.GetStart();
					if (_time > start)
					{
						tsAddKeyframe.Enabled = true;
					}
				}
				if (selectedWidget.SelectedFrame != null && selectedWidget.SelectedFrame.Time != 0)
				{
					tsRemoveKeyframe.Enabled = true;
					tsTypeBegin.Enabled = tsTypeNormal.Enabled = tsTypeSplit.Enabled = true;
				}
			}
		}

		private void AddSprite_Click(object sender, EventArgs e)
		{
			openFileDialog1.UseAbsolutePaths = true;
			if (openFileDialog1.ShowDialog(_character, "") == DialogResult.OK)
			{
				string src = openFileDialog1.FileName;
				timeline.SelectObject(timeline.CreateWidget(src));
			}
		}

		private void AddKeyframe_Click(object sender, EventArgs e)
		{
			if (timeline.SelectedObject == null) { return; }
			SpriteWidget widget = timeline.SelectedObject as SpriteWidget;
			//TODO: Make this a command
			LiveKeyframe frame = widget.Sprite.AddKeyframe(_time - widget.GetStart());
			widget.SelectKeyframe(frame, null, false);
			UpdateToolbar();
		}

		private void RemoveSprite_Click(object sender, EventArgs e)
		{
			if (timeline.SelectedObject == null) { return; }
			if (MessageBox.Show($"Are you sure you want to remove {timeline.SelectedObject.ToString()}?", "Remove Sprite", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
			{
				timeline.RemoveSelectedWidget();
				UpdateToolbar();
			}
		}

		private void RemoveKeyframe_Click(object sender, EventArgs e)
		{
			if (timeline.SelectedObject == null) { return; }
			SpriteWidget widget = timeline.SelectedObject as SpriteWidget;
			LiveKeyframe frame = widget.SelectedFrame;
			if (frame != null)
			{
				DeleteKeyframeCommand command = new DeleteKeyframeCommand(widget.Sprite, frame);
				_history.Commit(command);
			}
			UpdateToolbar();
		}

		private void CopyFirstFrame_Click(object sender, EventArgs e)
		{
			if (timeline.SelectedObject == null) { return; }
			SpriteWidget widget = timeline.SelectedObject as SpriteWidget;
			LiveSprite sprite = widget.Sprite;
			if (sprite.Keyframes.Count > 0)
			{
				LiveKeyframe kf = widget.Sprite.Keyframes[0];
				kf = sprite.CopyKeyframe(kf, new HashSet<string>());
				PasteKeyframeCommand command = new PasteKeyframeCommand(sprite, kf, widget.GetEnd(timeline.Duration));
				_history.Commit(command);
				timeline.CurrentTime = command.NewKeyframe.Time;
			}
			UpdateToolbar();
		}

		private void UpdateEnabledFields()
		{
			bool enabled = (_pose != null);
			timeline.Enabled = enabled;
			canvas.Enabled = enabled;
			table.Enabled = enabled;
			tsRemovePose.Enabled = enabled;
			tsMainMenu.Enabled = enabled;
			tsCut.Enabled = enabled;
			tsCopy.Enabled = enabled;
			tsPaste.Enabled = Clipboards.Has<Pose>();
			tsDuplicate.Enabled = enabled;
		}

		private void tsAddPose_Click(object sender, System.EventArgs e)
		{
			Pose pose = new Pose();
			pose.Id = "new_pose";
			lstPoses.Items.Add(pose);
			lstPoses.SelectedItem = pose;
			_character.CustomPoses.Add(pose);
			_character.CustomPoses.Sort();
			_character.Character.PoseLibrary.Add(pose);
		}

		private void tsRemovePose_Click(object sender, System.EventArgs e)
		{
			if (_pose == null ||
				MessageBox.Show($"Are you sure you want to permanently delete {_pose}? This operation cannot be undone.",
						"Remove Pose", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
			{
				return;
			}
			_character.CustomPoses.Remove(_sourcePose);
			_character.Character.PoseLibrary.Remove(_sourcePose);
			lstPoses.Items.Remove(_sourcePose);
			if (lstPoses.Items.Count > 0)
			{
				lstPoses.SelectedIndex = 0;
			}
		}

		private void lstPoses_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control)
			{
				if (e.KeyCode == Keys.X)
				{
					tsCut_Click(sender, EventArgs.Empty);
					e.SuppressKeyPress = true;
				}
				else if (e.KeyCode == Keys.C)
				{
					tsCopy_Click(sender, EventArgs.Empty);
					e.SuppressKeyPress = true;
				}
				else if (e.KeyCode == Keys.V)
				{
					tsPaste_Click(sender, EventArgs.Empty);
					e.SuppressKeyPress = true;
				}
				else if (e.KeyCode == Keys.D)
				{
					tsDuplicate_Click(sender, EventArgs.Empty);
					e.SuppressKeyPress = true;
				}
			}
		}


		private void tsCut_Click(object sender, System.EventArgs e)
		{
			if (_pose == null) { return; }
			SavePose();

			Clipboards.Set<Pose>(_sourcePose);
			lstPoses.Items.Remove(_sourcePose);
			_character.CustomPoses.Remove(_sourcePose);
		}

		private void tsCopy_Click(object sender, EventArgs e)
		{
			if (_pose == null) { return; }
			SavePose();

			Clipboards.Set<Pose>(_sourcePose);
			UpdateEnabledFields();
		}

		private void tsPaste_Click(object sender, EventArgs e)
		{
			Pose sourcePose = Clipboards.Get<Pose, Pose>();
			if (sourcePose == null) { return; }
			Pose copy = sourcePose.Clone() as Pose;
			lstPoses.Items.Add(copy);
			lstPoses.SelectedItem = copy;
			_character.CustomPoses.Add(copy);
		}

		private void tsDuplicate_Click(object sender, EventArgs e)
		{
			if (_pose == null) { return; }
			SavePose();

			Pose copy = _sourcePose.Clone() as Pose;
			lstPoses.Items.Add(copy);
			lstPoses.SelectedItem = copy;
			_character.CustomPoses.Add(copy);
		}

		private void tsCreateSequence_Click(object sender, EventArgs e)
		{
			SpriteWidget widget = timeline.SelectedObject as SpriteWidget;
			LiveSprite sprite = widget?.Sprite;
			if (!CanBeSequenced(widget))
			{
				sprite = null;
			}
			CreateSequenceForm form = new CreateSequenceForm(_character, sprite);
			if (form.ShowDialog() == DialogResult.OK)
			{
				List<string> frames = form.Frames;
				if (frames.Count == 0)
				{
					return;
				}
				if (form.Sprite == null)
				{
					sprite = timeline.CreateWidget(form.Frames[0])?.GetData() as LiveSprite;
					sprite.Id = form.SequenceName;
				}
				else
				{
					while (sprite.Keyframes.Count > 1)
					{
						sprite.Keyframes.RemoveAt(1);
					}
				}
				sprite.GetBlockMetadata("Src", 0).Ease = "linear";
				LiveSpriteKeyframe keyframe = sprite.Keyframes[0] as LiveSpriteKeyframe;
				keyframe.Src = frames[0];
				for (int i = 1; i < frames.Count; i++)
				{
					LiveSpriteKeyframe kf = sprite.AddKeyframe(i * form.Duration) as LiveSpriteKeyframe;
					kf.Src = frames[i];
				}
			}
		}

		private bool CanBeSequenced(SpriteWidget widget)
		{
			if (widget == null)
			{
				return false;
			}
			LiveSprite sprite = widget.Sprite;
			if (sprite.AnimatedProperties.Count <= 3) //only "X", "Y", and "Src" are allowed
			{
				if (!sprite.AnimatedProperties.Contains("Src"))
				{
					return false;
				}
				if (sprite.AnimatedProperties.Count == 2 && !sprite.AnimatedProperties.Contains("X") && !sprite.AnimatedProperties.Contains("Y"))
				{
					return false;
				}
				if (sprite.AnimatedProperties.Count == 3 && (!sprite.AnimatedProperties.Contains("X") || !sprite.AnimatedProperties.Contains("Y")))
				{
					return false;
				}
				return true;
			}

			return false;
		}

		private void tsRefresh_Click(object sender, EventArgs e)
		{
			LiveImageCache.Refresh();
			foreach (LiveSprite sprite in _pose.Sprites)
			{
				if (!string.IsNullOrEmpty(sprite.Src))
				{
					sprite.Image = LiveImageCache.Get(sprite.Src);
					if (sprite.Image != null)
					{
						sprite.Width = sprite.Image.Width;
						sprite.Height = sprite.Image.Height;
					}
					else
					{
						sprite.Width = 100;
						sprite.Height = 100;
					}
				}
			}
			canvas.InvalidateCanvas();
		}


		private void Timeline_UIRequested(object sender, object data)
		{
			LiveSpriteKeyframe frame = data as LiveSpriteKeyframe;
			if (frame != null)
			{
				openFileDialog1.UseAbsolutePaths = true;
				if (openFileDialog1.ShowDialog(_character, frame.Src ?? "") == DialogResult.OK)
				{
					string src = openFileDialog1.FileName;
					frame.Src = src;
				}
			}
		}

		private void tsExport_Click(object sender, EventArgs e)
		{
			if (_pose == null) { return; }
			SavePose();
			PoseExporter exporter = new PoseExporter();
			exporter.SetPose(_character, _sourcePose);
			exporter.ShowDialog();
		}

		private void tsTypeNormal_Click(object sender, EventArgs e)
		{
			ToggleKeyframeType(KeyframeType.Normal);
		}

		private void tsTypeSplit_Click(object sender, EventArgs e)
		{
			ToggleKeyframeType(KeyframeType.Split);
		}

		private void tsTypeBegin_Click(object sender, EventArgs e)
		{
			ToggleKeyframeType(KeyframeType.Begin);
		}

		private void ToggleKeyframeType(KeyframeType type)
		{
			if (timeline.SelectedObject == null) { return; }
			KeyframedWidget widget = timeline.SelectedObject as KeyframedWidget;
			LiveKeyframe frame = widget.SelectedFrame;
			if (frame != null)
			{
				HashSet<string> props = new HashSet<string>();
				foreach (string p in widget.SelectedProperties)
				{
					props.Add(p);
				}
				ToggleKeyframeTypeCommand command = new ToggleKeyframeTypeCommand(widget.Data, frame, props, type);
				_history.Commit(command);
			}
			UpdateToolbar();
		}

		private void tsStageSelect_Click(object sender, EventArgs e)
		{
			StageSelect form = new StageSelect();
			form.SetData(_character.Character, null, "Choose a Stage", "Assets prefixed with # will be retrieved from this stage."); 
			if (form.ShowDialog() == DialogResult.OK)
			{
				_stage = form.Stage;
				_pose.CurrentStage = _stage;
				canvas.Refresh();
			}
		}
	}

	public class LivePoseContext : ICharacterContext, IAutoCompleteList
	{
		public LivePose Pose { get; }
		public ISkin Character { get; }
		public CharacterContext Context { get; }

		public LivePoseContext(LivePose pose, ISkin character, CharacterContext context)
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
				foreach (LiveSprite sprite in Pose.Sprites)
				{
					items.Add(sprite.Id);
				}
				return items.ToArray();
			}
			return null;
		}
	}
}
