using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Desktop;

namespace SPNATI_Character_Editor.Providers
{
	public abstract class DefinitionProvider<T> : IRecordProvider<T> where T : Definition, new()
	{
		public bool AllowsNew { get { return false; } }
		public bool AllowsDelete { get { return false; } }
		protected bool SearchDescription { get; set; }
		protected object Context { get; set; }

		public bool TrackRecent
		{
			get { return false; }
		}

		public IRecord Create(string key)
		{
			T definition = CreateRecord(key);
			ApplyDefaults(definition);
			Definitions.Instance.Add(definition);
			return definition;
		}
		protected virtual T CreateRecord(string key)
		{
			T definition = Activator.CreateInstance<T>();
			definition.Key = key;
			definition.Name = key;
			return definition;
		}
		public abstract void ApplyDefaults(T definition);

		public void Delete(IRecord record) { }
		public virtual ListViewItem FormatItem(IRecord record)
		{
			Definition def = record as Definition;
			return new ListViewItem(new string[] { def.Name, def.Description });
		}

		public virtual void SetFormatInfo(LookupFormat info)
		{
			info.Caption = GetLookupCaption();
			info.Columns = new string[] { "Name", "Description" };
		}

		public abstract string GetLookupCaption();

		public List<IRecord> GetRecords(string text, LookupArgs args)
		{
			text = text.ToLower();
			var list = new List<IRecord>();
			foreach (T record in Definitions.Instance.Get<T>())
			{
				if (record.Key.ToLower().Contains(text) || record.Name.ToLower().Contains(text) || (SearchDescription && (record.Description ?? "").ToLower().Contains(text)))
				{
					//partial match
					list.Add(record);
				}
			}
			return list;
		}

		public void SetContext(object context)
		{
			Context = context;
		}

		public void Sort(List<IRecord> list)
		{
			list.Sort();
		}

		public virtual bool FilterFromUI(IRecord record)
		{
			return false;
		}
	}
}
