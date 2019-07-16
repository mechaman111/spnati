using Desktop.DataStructures;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Desktop.Reporting
{
	public class DataSlicer : BindableObject
	{
		public object Context
		{
			get { return Get<object>(); }
			set { Set(value); }
		}

		public ObservableCollection<IDataSlicer> Slicers
		{
			get { return Get<ObservableCollection<IDataSlicer>>(); }
			set { Set(value); }
		}

		public DataSlicer(object context)
		{
			Slicers = new ObservableCollection<IDataSlicer>();
			Context = context;
		}

		public void AddSlicer(IDataSlicer slicer)
		{
			Slicers.Add(slicer);
		}

		public void RemoveSlicer(IDataSlicer slicer)
		{
			Slicers.Remove(slicer);
		}

		public void MoveSlicer(IDataSlicer slicer, int direction)
		{
			direction = Math.Sign(direction);
			int index = Slicers.IndexOf(slicer);
			if (direction == -1 && index > 0)
			{
				Slicers.RemoveAt(index);
				Slicers.Insert(index - 1, slicer);
			}
			else if (direction == 1 && index < Slicers.Count - 1)
			{
				Slicers.RemoveAt(index);
				Slicers.Insert(index + 1, slicer);
			}
		}

		public List<DataBucket> Slice(IEnumerable<ISliceable> unslicedData)
		{
			if (Slicers.Count == 0)
			{
				return new List<DataBucket>(); ;
			}

			return Slice(0, unslicedData);
		}

		private List<DataBucket> Slice(int index, IEnumerable<ISliceable> dataSet)
		{
			List<DataBucket> buckets = Slicers[index].Slice(dataSet);
			if (index < Slicers.Count - 1)
			{
				foreach (DataBucket bucket in buckets)
				{
					bucket.Count = 0;
					List<DataBucket> sublist = Slice(index + 1, bucket.Data);
					foreach (DataBucket sub in sublist)
					{
						bucket.Count = Math.Max(bucket.Count, sub.Count);
						bucket.SubBuckets.Add(sub);
					}
				}
			}
			return buckets;
		}
	}

	public class DataBucket : IComparable<DataBucket>
	{
		public List<ISliceable> Data = new List<ISliceable>();

		public string Name;
		public int Count;

		public DataBucket(string name)
		{
			Name = name;
		}

		public List<DataBucket> SubBuckets = new List<DataBucket>();

		public override string ToString()
		{
			return $"{Name} ({Count})";
		}

		public int CompareTo(DataBucket other)
		{
			return Name.CompareTo(other.Name);
		}
	}

	public interface IDataSlicer : INotifyPropertyChanged
	{
		string DisplayName { get; set; }
		List<DataBucket> Slice(IEnumerable<ISliceable> dataSet);
		ObservableCollection<ISlicerGroup> Groups { get; }
		ISlicerGroup AddGroup(object value);
		void RemoveGroup(ISlicerGroup group);
		void Merge(ISlicerGroup group1, ISlicerGroup group2);
		void Split(ISlicerGroup group);
		void SetContext(object context);
	}

	public interface ISliceable
	{
		int GetSliceCount();
	}
}
