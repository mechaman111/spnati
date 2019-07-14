using Desktop.DataStructures;
using System.ComponentModel;

namespace Desktop.Reporting
{
	public interface ISlicerGroup : INotifyPropertyChanged
	{
		string Key { get; set; }
		string Label { get; set; }
		bool Groupable { get; set; }
		ObservableSet<object> Values { get; }
		bool Active { get; set; }
	}

	public class SlicerGroup<T> : BindableObject, ISlicerGroup
	{
		public bool Groupable
		{
			get { return Get<bool>(); }
			set { Set(value); }
		}

		public string Key
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		public string Label
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		public ObservableSet<object> Values
		{
			get { return Get<ObservableSet<object>>(); }
			set { Set(value); }
		}

		public bool Active
		{
			get { return Get<bool>(); }
			set { Set(value); }
		}

		public SlicerGroup(object value) : this()
		{
			Label = value.ToString();
			Key = value.ToString();
			Values.Add(value);
			Groupable = true;
		}

		public SlicerGroup()
		{
			Values = new ObservableSet<object>();
			Active = true;
			Groupable = false;
		}

		public override string ToString()
		{
			return Label;
		}
	}
}
