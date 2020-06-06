using System.Collections;
using Desktop.CommonControls;
using System.Reflection;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Desktop.Skinning
{
	public class SkinnedDataGridView : DataGridView, ISkinControl
	{
		const int HeaderPadding = 5;

		private Dictionary<DataGridViewRow, object> _boundItems = new Dictionary<DataGridViewRow, object>();
		private Type _itemType;
		private IList _dataSource;
		public IList Data
		{
			get { return _dataSource; }
			set
			{
				_dataSource = value;
				_itemType = null;
				if (_dataSource != null)
				{
					Type type = _dataSource.GetType();
					if (type.IsGenericType)
					{
						_itemType = type.GenericTypeArguments[0];
					}
				}
				PopulateData();
			}
		}

		public SkinnedDataGridView()
		{
			OnUpdateSkin(new Skin());
			DoubleBuffered = true;
			this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
		}

		public void OnUpdateSkin(Skin skin)
		{
			if (skin != null)
			{
				if (Parent != null && Parent.BackColor != System.Drawing.Color.Transparent)
				{
					BackgroundColor = Parent.BackColor;
				}
				else
				{
					BackgroundColor = skin.Background.Normal;
				}
				
				BorderStyle = BorderStyle.None;
				GridColor = skin.PrimaryColor.Border;
				EnableHeadersVisualStyles = false;

				ColumnHeadersBorderStyle = RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

				DefaultCellStyle.BackColor = skin.FieldBackColor;
				Font = Skin.TextFont;
				ColumnHeadersDefaultCellStyle.BackColor = RowHeadersDefaultCellStyle.BackColor = skin.Surface.Normal;
				ColumnHeadersDefaultCellStyle.ForeColor = RowHeadersDefaultCellStyle.ForeColor = skin.Surface.ForeColor;
				ColumnHeadersDefaultCellStyle.Padding = new Padding(0, HeaderPadding, 0, HeaderPadding);

				foreach (DataGridViewColumn column in Columns)
				{
					column.DefaultCellStyle.BackColor = skin.FieldBackColor;
					column.DefaultCellStyle.ForeColor = skin.Surface.ForeColor;
				}

				foreach (DataGridViewRow row in Rows)
				{
					foreach (DataGridViewCell cell in row.Cells)
					{
						cell.Style.BackColor = skin.FieldBackColor;
						cell.Style.ForeColor = skin.Surface.ForeColor;
					}
				}

				Invalidate(true);
			}
		}

		private void PopulateData()
		{
			Rows.Clear();
			if (_dataSource != null)
			{
				foreach (object obj in _dataSource)
				{
					Type type = obj.GetType();

					DataGridViewRow row = Rows[Rows.Add()];
					_boundItems[row] = obj;
					foreach (DataGridViewColumn col in Columns)
					{
						string propName = col.DataPropertyName;
						MemberInfo mi = PropertyTypeInfo.GetMemberInfo(type, propName);
						object value = mi?.GetValue(obj);
						row.Cells[col.Index].Value = value;
					}
				}
			}
		}

		public void Save(params DataGridViewColumn[] primaryCols)
		{
			if (_itemType == null) { return; }
			EndEdit();

			for (int i = 0; i < Rows.Count; i++)
			{
				DataGridViewRow row = Rows[i];
				if (row.IsNewRow)
				{
					continue;
				}
				bool primaryCellsFilled = true;
				foreach (DataGridViewColumn col in primaryCols)
				{
					object value = row.Cells[col.Index].Value;
					if (value == null)
					{
						primaryCellsFilled = false;
						break;
					}
				}
				if (primaryCellsFilled)
				{
					//update the item
					object item;
					if (!_boundItems.TryGetValue(row, out item))
					{
						//new row
						item = Activator.CreateInstance(_itemType);
						_boundItems[row] = item;
						_dataSource.Add(item);
					}
					foreach (DataGridViewColumn col in Columns)
					{
						string value = row.Cells[col.Index].Value?.ToString() ?? "";
						string propName = col.DataPropertyName;
						MemberInfo mi = PropertyTypeInfo.GetMemberInfo(_itemType, propName);
						Type memberType = mi.GetDataType();
						if (memberType == typeof(string))
						{
							mi.SetValue(item, value);
						}
						else if (memberType == typeof(int))
						{
							int numVal;
							int.TryParse(value, out numVal);
							mi.SetValue(item, numVal);
						}
						else if (memberType == typeof(float))
						{
							float numVal;
							float.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out numVal);
							mi.SetValue(item, numVal);
						}
						else
						{
							//need a special converter
						}
					}
				}
				else
				{
					//delete the item from the collection
					_boundItems.Remove(row);
					_dataSource.RemoveAt(i);
					Rows.RemoveAt(i--);
				}
			}
		}

		protected override void OnCellMouseDown(DataGridViewCellMouseEventArgs e)
		{
			if (e.RowIndex == -1 && e.ColumnIndex == -1)
			{
				TopLeftHeaderMouseDown?.Invoke(this, EventArgs.Empty);
			}
			else
			{
				base.OnCellMouseDown(e);
			}
		}
		public event EventHandler<EventArgs> TopLeftHeaderMouseDown;
	}
}
