using System.Collections.Generic;
using Desktop;
using SPNATI_Character_Editor.EpilogueEditor;

namespace SPNATI_Character_Editor.Actions
{
	/// <summary>
	/// Toggles one or more properties in a keyframe to use an anim break
	/// </summary>
	public class ToggleKeyframeTypeCommand : ICommand
	{
		private HashSet<string> _properties = new HashSet<string>();
		private Dictionary<string, KeyframeType> _oldSettings = new Dictionary<string, KeyframeType>();
		private Dictionary<string, KeyframeType> _newSettings = new Dictionary<string, KeyframeType>();
		private LiveKeyframe _keyframe;

		public ToggleKeyframeTypeCommand(LiveAnimatedObject data, LiveKeyframe frame, HashSet<string> properties) : this(data, frame, properties, null)
		{
		}
		public ToggleKeyframeTypeCommand(LiveAnimatedObject data, LiveKeyframe frame, HashSet<string> properties, KeyframeType? newType)
		{
			_keyframe = frame;
			if (properties.Count == 0)
			{
				foreach (string property in frame.TrackedProperties)
				{
					if (frame.HasProperty(property))
					{
						properties.Add(property);
					}
				}
			}
			foreach (string prop in properties)
			{
				_properties.Add(prop);
			}

			foreach (string property in _properties)
			{
				if (newType.HasValue)
				{
					_newSettings[property] = newType.Value;
				}
				else
				{
					KeyframeType type = _keyframe.GetFrameType(property);
					_oldSettings[property] = type;
					switch (type)
					{
						case KeyframeType.Begin:
							type = KeyframeType.Split;
							break;
						case KeyframeType.Split:
							type = KeyframeType.Normal;
							break;
						default:
							type = KeyframeType.Begin;
							break;
					}
					_newSettings[property] = type;
				}
			}
			_properties = properties;
		}

		public void Do()
		{
			_oldSettings.Clear();
			foreach (string property in _properties)
			{
				KeyframeType type = _newSettings[property];
				LiveKeyframeMetadata metadata = _keyframe.GetMetadata(property, true);
				metadata.FrameType = type;
			}
		}

		public void Undo()
		{
			foreach (KeyValuePair<string, KeyframeType> kvp in _oldSettings)
			{
				KeyframeType type = kvp.Value;
				LiveKeyframeMetadata metadata = _keyframe.GetMetadata(kvp.Key, true);
				metadata.FrameType = type;
			}
		}
	}
}
