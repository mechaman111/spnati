using Desktop;
using SPNATI_Character_Editor.EpilogueEditor;
using System;
using System.Collections.Generic;

namespace SPNATI_Character_Editor.Actions
{
	public class SplitKeyframeCommand : ICommand
	{
		public LiveAnimatedObject Data;
		public LiveKeyframe SourceFrame;
		public float Time;
		public HashSet<string> Properties;

		public SplitKeyframeCommand(LiveAnimatedObject data, float time, LiveKeyframe frame, HashSet<string> properties)
		{
			Data = data;
			SourceFrame = frame;
			Time = time;
			Properties = properties;
		}

		public void Do()
		{
			Dictionary<string, LiveKeyframeMetadata> metadata = new Dictionary<string, LiveKeyframeMetadata>();
			if (SourceFrame == null)
			{
				//need to insert a frame using interpolated properties for anything that appears before and after this point in the block
				foreach (string property in Data.Properties)
				{
					LiveKeyframe before = null;
					LiveKeyframe after = null;
					for (int i = 0; i < Data.Keyframes.Count; i++)
					{
						LiveKeyframe kf = Data.Keyframes[i];
						if (!kf.HasProperty(property))
						{
							continue;
						}
						LiveKeyframeMetadata propMetadata = kf.GetMetadata(property, false);
						if (kf.Time < Time)
						{
							before = kf;
							metadata[property] = propMetadata;
						}
						else
						{
							if (propMetadata.FrameType != KeyframeType.Begin)
							{
								//begin means this point must be before or between blocks, so there's nothing to split
								after = kf;
							}
							break;
						}
					}
					if (before != null && after != null)
					{
						if (after.Time == Time)
						{
							SourceFrame = after;
						}
						Properties.Add(property);
					}
				}
			}
			else if (Properties.Count == 0)
			{
				//add all properties in the frame
				foreach (string property in SourceFrame.TrackedProperties)
				{
					if (SourceFrame.HasProperty(property))
					{
						Properties.Add(property);
					}
				}
			}

			if (Properties.Count > 0)
			{
				LiveKeyframe previewFrame = Data.GetInterpolatedFrame(Time);
				LiveKeyframe destFrame = SourceFrame;
				if (destFrame == null)
				{
					destFrame = Data.AddKeyframe(Time);
				}
				foreach (string prop in Properties)
				{
					destFrame.Set(previewFrame.Get<object>(prop), prop);
					LiveKeyframeMetadata propMetadata = destFrame.GetMetadata(prop, true);
					propMetadata.FrameType = KeyframeType.Split;

					if (metadata.ContainsKey(prop))
					{
						LiveKeyframeMetadata srcMetadata = metadata.Get(prop);
						propMetadata.Interpolation = srcMetadata.Interpolation;
						propMetadata.Ease = srcMetadata.Ease;
					}
				}
			}
		}

		public void Undo()
		{
			throw new NotImplementedException();
		}
	}
}
