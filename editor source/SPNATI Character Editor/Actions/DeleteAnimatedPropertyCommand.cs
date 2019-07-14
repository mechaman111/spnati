using System.Collections.Generic;
using Desktop;
using SPNATI_Character_Editor.EpilogueEditor;

namespace SPNATI_Character_Editor.Actions
{
	/// <summary>
	/// Deletes a property from an entire animation
	/// </summary>
	public class DeleteAnimatedPropertyCommand : ICommand
	{
		private LiveAnimatedObject _object;
		private string _property;
		private List<ICommand> _commands = new List<ICommand>();

		public DeleteAnimatedPropertyCommand(LiveAnimatedObject obj, string property)
		{
			_object = obj;
			_property = property;
		}

		public void Do()
		{
			_commands.Clear();
			for (int i = _object.Keyframes.Count - 1; i >= 0; i--)
			{
				if (_object.Keyframes[i].HasProperty(_property))
				{
					DeletePropertyCommand command = new DeletePropertyCommand(_object, _object.Keyframes[i], _property);
					command.Do();
					_commands.Add(command);
				}
			}
		}

		public void Undo()
		{
			for (int i = _commands.Count; i >= 0; i--)
			{
				_commands[i].Undo();
			}
		}
	}
}
