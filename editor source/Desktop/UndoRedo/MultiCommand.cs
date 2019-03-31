using System.Collections.Generic;

namespace Desktop
{
	public class MultiCommand : ICommand
	{
		private List<ICommand> _items = new List<ICommand>();

		public int Count { get { return _items.Count; } }

		protected virtual void BeforeDo()
		{
		}

		protected virtual void AfterDo()
		{
		}

		protected virtual void BeforeUndo()
		{
		}

		protected virtual void AfterUndo()
		{
		}

		public ICommand Get(int index)
		{
			if (Count == 0)
				return null;
			return _items[index];
		}

		public void Do()
		{
			BeforeDo();
			for (int i = 0; i < _items.Count; i++)
			{
				_items[i].Do();
			}
			AfterDo();
		}

		public void Undo()
		{
			BeforeUndo();
			for (int i = _items.Count - 1; i >= 0; i--)
			{
				_items[i].Undo();
			}
			AfterUndo();
		}

		public void Record(ICommand item)
		{
			_items.Add(item);
		}

		public void Clear()
		{
			_items.Clear();
		}
	}
}
