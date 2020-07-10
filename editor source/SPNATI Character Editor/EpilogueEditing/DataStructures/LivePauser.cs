using SPNATI_Character_Editor.EpilogueEditing.Widgets;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SPNATI_Character_Editor.EpilogueEditor
{
	/// <summary>
	/// Manages all pauses in the sequence
	/// </summary>
	public class LivePauser : LiveObject
	{
		public override bool IsVisible
		{
			get { return false; }
		}

		public ObservableCollection<LiveBreak> Pauses
		{
			get { return Get<ObservableCollection<LiveBreak>>(); }
			set { Set(value); }
		}

		public LivePauser(LiveScene data)
		{
			Data = data;
			Pauses = new ObservableCollection<LiveBreak>();
		}

		/// <summary>
		/// Adds a pause at a time
		/// </summary>
		/// <param name="time"></param>
		public void Add(LiveBreak brk)
		{
			float time = brk.Time;
			for (int i = 0; i < Pauses.Count; i++)
			{
				if (Pauses[i].Time > time)
				{
					Pauses.Insert(i, brk);
					return;
				}
				if (Pauses[i].Time == time)
				{
					return; //already there
				}
			}
			Pauses.Add(brk);
		}

		public override LiveObject CreateLivePreview(float time)
		{
			return null;
		}

		public override ITimelineWidget CreateWidget(Timeline timeline)
		{
			return new PauseWidget(this, timeline);
		}

		public override void DestroyLivePreview()
		{

		}

		public override void Draw(Graphics g, Matrix sceneTransform, List<string> markers, bool inPlayback)
		{

		}

		public override void Update(float time, float elapsedTime, bool inPlayback)
		{

		}

		public override bool UpdateRealTime(float deltaTime, bool inPlayback)
		{
			return false;
		}

		/// <summary>
		/// Moves a break to a new point in time
		/// </summary>
		/// <param name="index"></param>
		/// <param name="newTime"></param>
		/// <param name="resort"></param>
		public void MoveBreak(int index, float newTime, bool resort)
		{
			if (index < 0 || index >= Pauses.Count)
			{
				return;
			}
			bool newExists = Pauses.Find(b => b.Time == newTime) != null;
			if (newExists)
			{
				return; //Can't move onto another one
			}
			Pauses[index].Time = newTime;

			if (resort)
			{
				Pauses.Sort((b1, b2) => b1.Time.CompareTo(b2.Time));
			}
		}

		public HashSet<float> GetValidBreaks()
		{
			HashSet<Tuple<float, float>> intervals = new HashSet<Tuple<float, float>>();
			HashSet<float> potentials = new HashSet<float>();
			LiveScene scene = Data as LiveScene;
			float maxTime = 0;

			//1st pass: map out all the animation block intervals
			foreach (LiveObject obj in scene.Tracks)
			{
				IFixedLength l = obj as IFixedLength;
				if (l != null)
				{
					maxTime = Math.Max(maxTime, obj.Start + l.Length);
				}
				LiveAnimatedObject anim = obj as LiveAnimatedObject;
				if (anim == null)
				{
					continue; //only tracks with keyframes are relevant for placing breaks
				}
				foreach (string property in anim.Properties)
				{
					LiveKeyframe blockStart = null;
					LiveKeyframe blockEnd = null;
					bool inLoop = false;
					foreach (LiveKeyframe kf in anim.Keyframes)
					{
						if (kf.HasProperty(property))
						{
							maxTime = Math.Max(obj.Start + kf.Time, maxTime);
							LiveKeyframeMetadata md = kf.GetMetadata(property, false);
							if (blockStart == null)
							{
								if (md.Iterations > 0 || md.Looped)
								{
									inLoop = true;
								}
								blockStart = kf;
								blockEnd = blockStart;
							}
							else
							{
								switch (md.FrameType)
								{
									case KeyframeType.Normal:
										blockEnd = kf;
										break;
									case KeyframeType.Begin:
									case KeyframeType.Split:
										if (md.FrameType == KeyframeType.Split)
										{
											//this is the end of a block and the start of the next
											blockEnd = kf;
										}
										if (!inLoop)
										{
											intervals.Add(new Tuple<float, float>(blockStart.Time + obj.Start, blockEnd.Time + obj.Start));
											potentials.Add(blockStart.Time + obj.Start);
											potentials.Add(blockEnd.Time + obj.Start);
										}

										inLoop = false;
										blockEnd = null;
										blockStart = kf;
										break;
								}
							}
						}
					}
					if (blockStart != null && !inLoop)
					{
						if (blockEnd == null)
						{
							blockEnd = blockStart;
						}
						intervals.Add(new Tuple<float, float>(blockStart.Time + obj.Start, blockEnd.Time + obj.Start));
						potentials.Add(blockStart.Time + obj.Start);
						potentials.Add(blockEnd.Time + obj.Start);
					}
				}
			}

			potentials.Remove(0);

			if (potentials.Count == 0)
			{
				//if there is nothing valid, then allow a pause after whatever was the max time
				potentials.Add(maxTime);
			}

			//2nd pass: filter out any edges intersecting another interval
			HashSet<float> times = new HashSet<float>();
			foreach (float time in potentials)
			{
				bool allowed = true;
				foreach (Tuple<float, float> other in intervals)
				{
					if (other.Item1 < time && other.Item2 > time)
					{
						allowed = false;
						break;
					}
				}
				if (allowed)
				{
					times.Add(time);
				}
			}

			return times;
		}
	}
}
