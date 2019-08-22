using System.Collections.Specialized;
using Desktop;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DesktopTests
{
	[TestClass]
	public class ObservableDictionaryTests
	{
		[TestMethod]
		public void Add()
		{
			ObservableDictionary<string, int> dict = new ObservableDictionary<string, int>();
			string key = "fun";
			dict.Add(key, 5);
			Assert.IsTrue(dict.ContainsKey(key));
			Assert.AreEqual(5, dict[key]);
		}

		[TestMethod]
		public void Set()
		{
			ObservableDictionary<string, int> dict = new ObservableDictionary<string, int>();
			string key = "fun";
			dict[key] = 5;
			Assert.IsTrue(dict.ContainsKey(key));
			Assert.AreEqual(5, dict[key]);
		}

		[TestMethod]
		public void Remove()
		{
			ObservableDictionary<string, int> dict = new ObservableDictionary<string, int>();
			string key = "fun";
			dict[key] = 5;
			Assert.IsTrue(dict.Remove(key));
			Assert.IsFalse(dict.ContainsKey(key));
		}

		[TestMethod]
		public void Count()
		{
			ObservableDictionary<string, int> dict = new ObservableDictionary<string, int>();
			dict.Add("a", 1);
			dict.Add("b", 1);
			Assert.AreEqual(2, dict.Count);
		}

		[TestMethod]
		public void Observe_Add()
		{
			ObservableDictionary<string, int> dict = new ObservableDictionary<string, int>();
			int raised = 0;

			dict.CollectionChanged += delegate (object sender, NotifyCollectionChangedEventArgs e)
			{
				raised++;
				Assert.AreEqual(NotifyCollectionChangedAction.Add, e.Action);
				Assert.AreEqual(1, e.NewItems.Count);
				Assert.AreEqual(1, e.NewItems[0]);
			};

			dict.Add("a", 1);
			Assert.AreEqual(1, raised);
		}

		[TestMethod]
		public void Observe_Replace()
		{
			ObservableDictionary<string, int> dict = new ObservableDictionary<string, int>();
			int raised = 0;

			dict.Add("a", 1);

			dict.CollectionChanged += delegate (object sender, NotifyCollectionChangedEventArgs e)
			{
				raised++;
				Assert.AreEqual(NotifyCollectionChangedAction.Replace, e.Action);
				Assert.AreEqual(1, e.OldItems.Count);
				Assert.AreEqual(1, e.OldItems[0]);
				Assert.AreEqual(1, e.NewItems.Count);
				Assert.AreEqual(2, e.NewItems[0]);
			};
			dict["a"] = 2;

			Assert.AreEqual(1, raised);
		}

		[TestMethod]
		public void Observe_Remove()
		{
			ObservableDictionary<string, int> dict = new ObservableDictionary<string, int>();
			int raised = 0;
			dict.Add("a", 1);

			dict.CollectionChanged += delegate (object sender, NotifyCollectionChangedEventArgs e)
			{
				raised++;
				Assert.AreEqual(NotifyCollectionChangedAction.Remove, e.Action);
				Assert.AreEqual(1, e.OldItems.Count);
				Assert.AreEqual(1, e.OldItems[0]);
			};

			dict.Remove("a");
			Assert.AreEqual(1, raised);
		}
	}
}
