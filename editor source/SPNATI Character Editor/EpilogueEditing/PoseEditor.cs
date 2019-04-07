using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Desktop;
using SPNATI_Character_Editor.Actions;
using SPNATI_Character_Editor.Activities;
using SPNATI_Character_Editor.Controls;

namespace SPNATI_Character_Editor.EpilogueEditor
{
	[Activity(typeof(Character), 210)]
	[Activity(typeof(Costume), 210)]
	public partial class PoseEditor : Activity
	{
		private ISkin _character;
		private LivePose _pose;
		private Pose _sourcePose;
		private UndoManager _history = new UndoManager();
		private float _time;
		private float _playbackTime;
		private bool _playing;
		private ILabel _labelData;
		private ImageLibrary _library;

		public PoseEditor()
		{
			InitializeComponent();
			_history.CommandApplied += _history_CommandApplied;
			timeline.CommandHistory = _history;
			timeline.DataSelected += Timeline_DataSelected;
			timeline.TimeChanged += Timeline_TimeChanged;
			timeline.PlaybackTimeChanged += Timeline_PlaybackTimeChanged;
			timeline.WidgetSelected += Timeline_WidgetSelected;
			timeline.PlaybackChanged += Timeline_PlaybackChanged;
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
			_library = ImageLibrary.Get(_character);
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

		private void Timeline_WidgetSelected(object sender, ITimelineWidget widget)
		{
			canvas.SelectData(widget?.GetData());
		}

		private void Timeline_PlaybackTimeChanged(object sender, float time)
		{
			_playbackTime = time;
			canvas.UpdateTime(_time, _playbackTime);
		}

		private void Timeline_TimeChanged(object sender, float time)
		{
			_time = time;
			canvas.UpdateTime(time, _playbackTime);
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
		}

		protected override void OnDeactivate()
		{
			Workspace.ToggleSidebar(true);
		}

		public override void Save()
		{
			SavePose();
		}

		public override void Destroy()
		{
			LiveImageCache.Clear();
			base.Destroy();
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
			_history.Clear();
			_sourcePose = pose;
			if (pose != null)
			{
				_pose = new LivePose(_character, pose);
			}
			else
			{
				_pose = null;
			}
			timeline.SetData(_pose);
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
				_library.Rename(_sourcePose);
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
			SpriteWidget selectedWidget = timeline.SelectedWidget as SpriteWidget;

			tsRemoveSprite.Enabled = tsAddEndFrame.Enabled = (selectedWidget != null);
			tsAddKeyframe.Enabled = false;
			tsRemoveKeyframe.Enabled = false;
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
				}
			}
		}

		private void AddSprite_Click(object sender, EventArgs e)
		{
			openFileDialog1.UseAbsolutePaths = true;
			if (openFileDialog1.ShowDialog(_character, "") == DialogResult.OK)
			{
				string src = openFileDialog1.FileName;
				timeline.SelectData(timeline.CreateWidget(src)?.GetData());
			}
		}

		private void AddKeyframe_Click(object sender, EventArgs e)
		{
			if (timeline.SelectedWidget == null) { return; }
			SpriteWidget widget = timeline.SelectedWidget as SpriteWidget;
			//TODO: Make this a command
			LiveKeyframe frame = widget.Sprite.AddKeyframe(_time - widget.GetStart());
			widget.SelectKeyframe(frame, null, false);
			UpdateToolbar();
		}

		private void RemoveSprite_Click(object sender, EventArgs e)
		{
			if (timeline.SelectedWidget == null) { return; }
			if (MessageBox.Show($"Are you sure you want to remove {timeline.SelectedWidget.ToString()}?", "Remove Sprite", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
			{
				timeline.RemoveSelectedWidget();
				UpdateToolbar();
			}
		}

		private void RemoveKeyframe_Click(object sender, EventArgs e)
		{
			if (timeline.SelectedWidget == null) { return; }
			SpriteWidget widget = timeline.SelectedWidget as SpriteWidget;
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
			if (timeline.SelectedWidget == null) { return; }
			SpriteWidget widget = timeline.SelectedWidget as SpriteWidget;
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
			_library.Add(pose);
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
			_library.Remove(_sourcePose);
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
	}
}
