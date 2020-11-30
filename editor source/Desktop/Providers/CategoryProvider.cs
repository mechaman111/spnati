using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Desktop.Providers
{
	public abstract class CategoryProvider<T> : IRecordProvider<T> where T : Category
	{
		private List<T> _categoryValues;

		public virtual bool AllowsNew
		{
			get { return false; }
		}
		public bool AllowsDelete { get { return false; } }

		public bool TrackRecent
		{
			get { return false; }
		}

		public virtual IRecord Create(string key)
		{
			throw new NotImplementedException();
		}

		public void Delete(IRecord record)
		{
			throw new NotImplementedException();
		}

		public ListViewItem FormatItem(IRecord record)
		{
			ListViewItem item = new ListViewItem(new string[] { record.Key, record.Name });
			return item;
		}

		public void SetFormatInfo(LookupFormat info)
		{
			info.Columns = new string[] { "Key", "Value" };
			info.Caption = GetLookupCaption();
		}

		public virtual bool FilterFromUI(IRecord record)
		{
			return false;
		}

		public abstract string GetLookupCaption();

		protected abstract T[] GetCategoryValues();

		public List<IRecord> GetRecords(string text, LookupArgs args)
		{
			if (_categoryValues == null)
			{
				_categoryValues = GetCategoryValues().ToList();
			}

			text = text.ToLower();
			List<IRecord> list = new List<IRecord>();
			foreach (T category in _categoryValues)
			{
				if (category.Key.ToLower().Contains(text) || category.Name.ToLower().Contains(text))
				{
					list.Add(category);
				}
			}
			return list;
		}

		protected void Add(T category)
		{
			_categoryValues.Add(category);
		}

		public void SetContext(object context)
		{
		}

		public void Sort(List<IRecord> list)
		{
			list.Sort();
		}
	}
}
