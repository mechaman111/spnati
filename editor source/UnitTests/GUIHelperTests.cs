using Microsoft.VisualStudio.TestTools.UnitTesting;
using SPNATI_Character_Editor;
using System.Collections.ObjectModel;

namespace UnitTests
{
	[TestClass]
	public class GUIHelperTests
	{
		[TestMethod]
		public void EmptyList()
		{
			ObservableCollection<int> list = new ObservableCollection<int>();
			string output = GUIHelper.ListToString(list);
			Assert.AreEqual("", output);
		}

		[TestMethod]
		public void SingleStage()
		{
			string output = GUIHelper.ListToString(new ObservableCollection<int>
			{
				5
			});
			Assert.AreEqual("5", output);
		}

		[TestMethod]
		public void SingleRange()
		{
			string output = GUIHelper.ListToString(new ObservableCollection<int>
			{
				5,
				6,
				7
			});
			Assert.AreEqual("5-7", output);
		}

		[TestMethod]
		public void MultipleRange()
		{
			string output = GUIHelper.ListToString(new ObservableCollection<int>
			{
				1,
				2,
				4,
				5,
				6
			});
			Assert.AreEqual("1-2 4-6", output);
		}

		[TestMethod]
		public void RangeStage()
		{
			string output = GUIHelper.ListToString(new ObservableCollection<int>
			{
				1,
				2,
				3,
				6
			});
			Assert.AreEqual("1-3 6", output);
		}

		[TestMethod]
		public void StageRange()
		{
			string output = GUIHelper.ListToString(new ObservableCollection<int>
			{
				1,
				4,
				5,
				6
			});
			Assert.AreEqual("1 4-6", output);
		}

		[TestMethod]
		public void SortedRange()
		{
			string output = GUIHelper.ListToString(new ObservableCollection<int>
			{
				6,
				5,
				2,
				7
			});
			Assert.AreEqual("2 5-7", output);
		}

		[TestMethod]
		public void StageRange_ToList()
		{
			ObservableCollection<int> list = GUIHelper.StringToList("1 4-6");
			Assert.AreEqual(4, list.Count);
			Assert.AreEqual(1, list[0]);
			Assert.AreEqual(4, list[1]);
			Assert.AreEqual(5, list[2]);
			Assert.AreEqual(6, list[3]);
		}

		[TestMethod]
		public void RangeStage_ToList()
		{
			ObservableCollection<int> list = GUIHelper.StringToList("1-3 6");
			Assert.AreEqual(4, list.Count);
			Assert.AreEqual(1, list[0]);
			Assert.AreEqual(2, list[1]);
			Assert.AreEqual(3, list[2]);
			Assert.AreEqual(6, list[3]);
		}

		[TestMethod]
		public void MultipleRange_ToList()
		{
			ObservableCollection<int> list = GUIHelper.StringToList("1-2 4-6");
			Assert.AreEqual(5, list.Count);
			Assert.AreEqual(1, list[0]);
			Assert.AreEqual(2, list[1]);
			Assert.AreEqual(4, list[2]);
			Assert.AreEqual(5, list[3]);
			Assert.AreEqual(6, list[4]);
		}

		[TestMethod]
		public void SingleRange_ToList()
		{
			ObservableCollection<int> list = GUIHelper.StringToList("5-7");
			Assert.AreEqual(3, list.Count);
			Assert.AreEqual(5, list[0]);
			Assert.AreEqual(6, list[1]);
			Assert.AreEqual(7, list[2]);
		}

		[TestMethod]
		public void SingleStage_ToList()
		{
			ObservableCollection<int> list = GUIHelper.StringToList("5");
			Assert.AreEqual(1, list.Count);
			Assert.AreEqual(5, list[0]);
		}

		[TestMethod]
		public void Empty_ToList()
		{
			ObservableCollection<int> list = GUIHelper.StringToList("");
			Assert.AreEqual(0, list.Count);
		}

		[TestMethod]
		public void UnsortedRange_ToList()
		{
			ObservableCollection<int> list = GUIHelper.StringToList("4-6 2");
			Assert.AreEqual(4, list.Count);
			Assert.AreEqual(2, list[0]);
			Assert.AreEqual(4, list[1]);
			Assert.AreEqual(5, list[2]);
			Assert.AreEqual(6, list[3]);
		}
	}
}
