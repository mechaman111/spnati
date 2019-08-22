using System.Collections.ObjectModel;
using System.ComponentModel;
using Desktop;
using Desktop.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DesktopTests
{
	[TestClass]
	public class BindableObjectTests
	{
		[TestMethod]
		public void SetData()
		{
			TestRoot root = new TestRoot();
			root.PropA = "test";
			Assert.AreEqual("test", root.PropA);
		}

		[TestMethod]
		public void Notify()
		{
			TestRoot root = new TestRoot();
			int raised = 0;
			root.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
			{
				raised++;
			};
			root.PropA = "test";
			Assert.AreEqual(1, raised);
		}

		[TestMethod]
		public void ChangeObject()
		{
			TestRoot root = new TestRoot();
			Nested nested = new Nested();
			root.NestedObject = nested;
			int raised = 0;
			root.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
			{
				raised++;
			};
			nested.PropB = "test";
			Assert.AreEqual(1, raised);
		}

		[TestMethod]
		public void ListNotify_Add()
		{
			TestRoot root = new TestRoot();
			int raised = 0;
			root.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
			{
				raised++;
			};
			root.List.Add("hi");
			Assert.AreEqual(1, raised);
		}

		[TestMethod]
		public void ListNotify_Remove()
		{
			TestRoot root = new TestRoot();
			root.List.Add("hi");
			int raised = 0;
			root.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
			{
				raised++;
			};
			root.List.Remove("hi");
			Assert.AreEqual(1, raised);
		}

		[TestMethod]
		public void NestedList_Notify()
		{
			TestRoot root = new TestRoot();
			Nested nested = new Nested();
			root.NestedList.Add(nested);
			int raised = 0;
			root.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
			{
				raised++;
			};
			nested.PropB = "fun";
			Assert.AreEqual(1, raised);
		}

		[TestMethod]
		public void NestedList_Replace()
		{
			TestRoot root = new TestRoot();
			Nested nested = new Nested();
			root.NestedList.Add(nested);
			Nested newNested = new Nested();
			root.NestedList[0] = newNested;
			int raised = 0;
			root.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
			{
				raised++;
			};
			newNested.PropB = "fun";
			Assert.AreEqual(1, raised);
		}

		[TestMethod]
		public void Dictionary_NestedChange()
		{
			DictionaryObject obj = new DictionaryObject();
			Nested nested = new Nested();
			int raised = 0;
			obj.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
			{
				raised++;
			};
			obj.Dictionary.Add("a", nested);
			Assert.AreEqual(1, raised);
			nested.PropB = "2nd Change";
			Assert.AreEqual(2, raised);
		}
	}

	public class TestRoot : BindableObject
	{
		public string PropA
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		public Nested NestedObject
		{
			get { return Get<Nested>(); }
			set { Set(value); }
		}

		public ObservableCollection<string> List
		{
			get { return Get<ObservableCollection<string>>(); }
			set { Set(value); }
		}

		public ObservableCollection<Nested> NestedList
		{
			get { return Get<ObservableCollection<Nested>>(); }
			set { Set(value); }
		}

		public TestRoot()
		{
			List = new ObservableCollection<string>();
			NestedList = new ObservableCollection<Nested>();
		}
	}

	public class Nested : BindableObject
	{
		public string PropB
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		public ObservableCollection<string> List
		{
			get { return Get<ObservableCollection<string>>(); }
			set { Set(value); }
		}

		public Nested()
		{
			List = new ObservableCollection<string>();
		}
	}

	public class DictionaryObject : BindableObject
	{
		public ObservableDictionary<string, Nested> Dictionary
		{
			get { return Get<ObservableDictionary<string, Nested>>(); }
			set { Set(value); }
		}

		public DictionaryObject()
		{
			Dictionary = new ObservableDictionary<string, Nested>();
		}
	}
}
