using System;
using System.Collections.Generic;

namespace Desktop
{
	public class UndoManager
	{
		private static UndoManager _instance;
		public static UndoManager Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new UndoManager();
				}

				return _instance;
			}
		}

		private Stack<ICommand> _undoCommands = new Stack<ICommand>();
		private Stack<ICommand> _redoCommands = new Stack<ICommand>();

		private MultiCommand _bulkAction = null;
		private bool _bulkRecording = false;

		public event Action<UndoAction> CommandApplied;

		private void OnCommandApplied(UndoAction action)
		{
			if (CommandApplied != null)
			{
				CommandApplied(action);
			}
		}

		public void Clear()
		{
			_undoCommands.Clear();
			_redoCommands.Clear();
			_bulkRecording = false;
			OnCommandApplied(UndoAction.Other);
		}

		/// <summary>
		/// Performs and records a command
		/// </summary>
		/// <param name="item"></param>
		public void Commit(ICommand item)
		{
			item.Do();
			Record(item);
		}

		/// <summary>
		/// Records a command to the history but doesn't perform it
		/// </summary>
		/// <param name="item"></param>
		public void Record(ICommand item)
		{
			if (_bulkRecording)
			{
				_bulkAction.Record(item);
				return;
			}
			_undoCommands.Push(item);
			_redoCommands.Clear();
			OnCommandApplied(UndoAction.Do);
		}

		public void StartBulkRecord()
		{
			if (_bulkAction != null)
			{
				EndBulkRecord();
			}
			_bulkRecording = true;
			_bulkAction = new MultiCommand();
		}

		public void EndBulkRecord()
		{
			if (_bulkAction == null)
			{
				return;
			}

			_bulkRecording = false;
			Record(_bulkAction);
			_bulkAction = null;
		}

		public bool CanUndo()
		{
			return _undoCommands.Count > 0;
		}

		public bool CanRedo()
		{
			return _redoCommands.Count > 0;
		}

		public void Undo()
		{
			EndBulkRecord();
			if (_undoCommands.Count > 0)
			{
				ICommand item = _undoCommands.Pop();
				item.Undo();
				_redoCommands.Push(item);
				OnCommandApplied(UndoAction.Undo);
			}
		}

		public ICommand Peek()
		{
			if (_undoCommands.Count > 0)
			{
				return _undoCommands.Peek();
			}

			return null;
		}

		public ICommand PeekRedo()
		{
			if (_redoCommands.Count > 0)
			{
				return _redoCommands.Peek();
			}

			return null;
		}

		public void Redo()
		{
			EndBulkRecord();
			if (_redoCommands.Count > 0)
			{
				ICommand item = _redoCommands.Pop();
				item.Do();
				_undoCommands.Push(item);
				OnCommandApplied(UndoAction.Redo);
			}
		}
	}

	public enum UndoAction
	{
		Other,
		Do,
		Undo,
		Redo,
	}
}
