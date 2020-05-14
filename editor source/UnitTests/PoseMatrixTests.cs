using Microsoft.VisualStudio.TestTools.UnitTesting;
using SPNATI_Character_Editor.DataStructures;
using System.Collections.Generic;

namespace UnitTests
{
	[TestClass]
	public class PoseStageTests
	{
		private static readonly List<string> Order = new List<string>(new string[] { "a", "b", "c" });

		[TestMethod]
		public void InsertPose_AddsToEmpty()
		{
			PoseStage stage = new PoseStage();
			PoseEntry cell = new PoseEntry() { Key = "a" };
			stage.InsertCell(cell, Order);
			Assert.AreEqual(cell, stage.Poses[0]);
		}

		[TestMethod]
		public void InsertPose_AddsAfter_End()
		{
			PoseStage stage = new PoseStage();
			stage.Poses.Add(new PoseEntry() { Key = "a" });

			PoseEntry cell = new PoseEntry() { Key = "c" };
			stage.InsertCell(cell, Order);
			Assert.AreEqual(cell, stage.Poses[1]);
		}

		[TestMethod]
		public void InsertPose_AddsAfter_Middle()
		{
			PoseStage stage = new PoseStage();
			stage.Poses.Add(new PoseEntry() { Key = "a" });
			stage.Poses.Add(new PoseEntry() { Key = "c" });

			PoseEntry cell = new PoseEntry() { Key = "b" };
			stage.InsertCell(cell, Order);
			Assert.AreEqual(cell, stage.Poses[1]);
		}

		[TestMethod]
		public void InsertPose_Adds_NotFound()
		{
			PoseStage stage = new PoseStage();
			stage.Poses.Add(new PoseEntry() { Key = "a" });

			PoseEntry cell = new PoseEntry() { Key = "d" };
			stage.InsertCell(cell, Order);
			Assert.AreEqual(cell, stage.Poses[1]);
		}

		[TestMethod]
		public void InsertPose_AddsBefore_Start()
		{
			PoseStage stage = new PoseStage();
			stage.Poses.Add(new PoseEntry() { Key = "b" });

			PoseEntry cell = new PoseEntry() { Key = "a" };
			stage.InsertCell(cell, Order);
			Assert.AreEqual(cell, stage.Poses[0]);
		}

		[TestMethod]
		public void InsertPose_AddsBefore_Middle()
		{
			PoseStage stage = new PoseStage();
			stage.Poses.Add(new PoseEntry() { Key = "a" });
			stage.Poses.Add(new PoseEntry() { Key = "c" });

			PoseEntry cell = new PoseEntry() { Key = "b" };
			stage.InsertCell(cell, Order);
			Assert.AreEqual(cell, stage.Poses[1]);
		}

		[TestMethod]
		public void Reorder_MatchesInput()
		{
			PoseStage stage = new PoseStage();
			stage.Poses.Add(new PoseEntry() { Key = "c" });
			stage.Poses.Add(new PoseEntry() { Key = "a" });
			stage.Poses.Add(new PoseEntry() { Key = "b" });

			stage.Reorder(Order);
			Assert.AreEqual("a", stage.Poses[0].Key);
			Assert.AreEqual("b", stage.Poses[1].Key);
			Assert.AreEqual("c", stage.Poses[2].Key);
		}

		[TestMethod]
		public void Reorder_PutsNotFoundAtStart()
		{
			PoseStage stage = new PoseStage();
			stage.Poses.Add(new PoseEntry() { Key = "c" });
			stage.Poses.Add(new PoseEntry() { Key = "d" });
			stage.Poses.Add(new PoseEntry() { Key = "b" });

			stage.Reorder(Order);
			Assert.AreEqual("d", stage.Poses[0].Key);
			Assert.AreEqual("b", stage.Poses[1].Key);
			Assert.AreEqual("c", stage.Poses[2].Key);
		}
	}
}
