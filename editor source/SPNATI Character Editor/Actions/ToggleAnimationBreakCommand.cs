using Desktop;
using SPNATI_Character_Editor.EpilogueEditor;
using System.Collections.Generic;
using System;

namespace SPNATI_Character_Editor.Actions
{
	/// <summary>
	/// Toggles one or more properties in a keyframe to use an anim break
	/// </summary>
	public class ToggleAnimationBreakCommand : ICommand
	{
		private HashSet<string> _properties = new HashSet<string>();
		private Dictionary<string, bool> _oldSettings = new Dictionary<string, bool>();
		private LiveKeyframe _keyframe;
		private bool _enable;

		public ToggleAnimationBreakCommand(LiveKeyframe frame, HashSet<string> properties)
		{
			_keyframe = frame;
			if (properties.Count == 0)
			{
				foreach (string property in LiveKeyframe.TrackedProperties)
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

			bool anyOff = false; //turn on for everything unless all selected properties were already on
			foreach (string property in _properties)
			{
				bool isBreak = _keyframe.InterpolationBreaks.ContainsKey(property);
				_oldSettings[property] = isBreak;
				anyOff = anyOff || !isBreak;
			}
			if (anyOff)
			{
				_enable = true;
			}
			_properties = properties;
		}

		public void Do()
		{
			_oldSettings.Clear();
			foreach (string property in _properties)
			{
				_oldSettings[property] = _keyframe.InterpolationBreaks.ContainsKey(property);
				if (_enable)
				{
					_keyframe.InterpolationBreaks[property] = true;
				}
				else
				{
					_keyframe.InterpolationBreaks.Remove(property);
				}
			}
		}

		public void Undo()
		{
			foreach (KeyValuePair<string, bool> kvp in _oldSettings)
			{
				if (kvp.Value)
				{
					_keyframe.InterpolationBreaks[kvp.Key] = true;
				}
				else
				{
					_keyframe.InterpolationBreaks.Remove(kvp.Key);
				}
			}
		}
	}
}
