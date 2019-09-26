using Microsoft.VisualStudio.TestTools.UnitTesting;
using SPNATI_Character_Editor;

namespace UnitTests
{
	[TestClass]
	public class CaseConversions
	{
		[TestMethod]
		public void TransfersTarget()
		{
			Case c = new Case();
			c.Target = "bob";
			DataConversions.ConvertCase5_2(c);
			Assert.AreEqual(1, c.Conditions.Count);
			Assert.IsNull(c.Target);
			Assert.AreEqual("target", c.Conditions[0].Role);
			Assert.AreEqual("bob", c.Conditions[0].Character);
		}

		[TestMethod]
		public void TransfersTargetStage()
		{
			Case c = new Case();
			c.TargetStage = "0-5";
			DataConversions.ConvertCase5_2(c);
			Assert.AreEqual(1, c.Conditions.Count);
			Assert.IsNull(c.TargetStage);
			Assert.AreEqual("target", c.Conditions[0].Role);
			Assert.AreEqual("0-5", c.Conditions[0].Stage);
		}

		[TestMethod]
		public void TransfersTargetHand()
		{
			Case c = new Case();
			c.TargetHand = "blah";
			DataConversions.ConvertCase5_2(c);
			Assert.AreEqual(1, c.Conditions.Count);
			Assert.IsNull(c.TargetHand);
			Assert.AreEqual("target", c.Conditions[0].Role);
			Assert.AreEqual("blah", c.Conditions[0].Hand);
		}

		[TestMethod]
		public void TransfersTargetLayers()
		{
			Case c = new Case();
			c.TargetLayers = "blah";
			DataConversions.ConvertCase5_2(c);
			Assert.AreEqual(1, c.Conditions.Count);
			Assert.IsNull(c.TargetLayers);
			Assert.AreEqual("target", c.Conditions[0].Role);
			Assert.AreEqual("blah", c.Conditions[0].Layers);
		}

		[TestMethod]
		public void TransfersTargetStartingLayers()
		{
			Case c = new Case();
			c.TargetStartingLayers = "blah";
			DataConversions.ConvertCase5_2(c);
			Assert.AreEqual(1, c.Conditions.Count);
			Assert.IsNull(c.TargetStartingLayers);
			Assert.AreEqual("target", c.Conditions[0].Role);
			Assert.AreEqual("blah", c.Conditions[0].StartingLayers);
		}

		[TestMethod]
		public void TransfersTargetStatus()
		{
			Case c = new Case();
			c.TargetStatus = "blah";
			DataConversions.ConvertCase5_2(c);
			Assert.AreEqual(1, c.Conditions.Count);
			Assert.IsNull(c.TargetStatus);
			Assert.AreEqual("target", c.Conditions[0].Role);
			Assert.AreEqual("blah", c.Conditions[0].Status);
		}

		[TestMethod]
		public void TransfersTargetSaidMarker()
		{
			Case c = new Case();
			c.TargetSaidMarker = "blah";
			DataConversions.ConvertCase5_2(c);
			Assert.AreEqual(1, c.Conditions.Count);
			Assert.IsNull(c.TargetSaidMarker);
			Assert.AreEqual("target", c.Conditions[0].Role);
			Assert.AreEqual("blah", c.Conditions[0].SaidMarker);
		}

		[TestMethod]
		public void TransfersTargetSayingMarker()
		{
			Case c = new Case();
			c.TargetSayingMarker = "blah";
			DataConversions.ConvertCase5_2(c);
			Assert.AreEqual(1, c.Conditions.Count);
			Assert.IsNull(c.TargetSayingMarker);
			Assert.AreEqual("target", c.Conditions[0].Role);
			Assert.AreEqual("blah", c.Conditions[0].SayingMarker);
		}

		[TestMethod]
		public void TransfersTargetNotSaidMarker()
		{
			Case c = new Case();
			c.TargetNotSaidMarker = "blah";
			DataConversions.ConvertCase5_2(c);
			Assert.AreEqual(1, c.Conditions.Count);
			Assert.IsNull(c.TargetNotSaidMarker);
			Assert.AreEqual("target", c.Conditions[0].Role);
			Assert.AreEqual("blah", c.Conditions[0].NotSaidMarker);
		}

		[TestMethod]
		public void TransfersTargetSaying()
		{
			Case c = new Case();
			c.TargetSaying = "blah";
			DataConversions.ConvertCase5_2(c);
			Assert.AreEqual(1, c.Conditions.Count);
			Assert.IsNull(c.TargetSaying);
			Assert.AreEqual("target", c.Conditions[0].Role);
			Assert.AreEqual("blah", c.Conditions[0].Saying);
		}

		[TestMethod]
		public void TransfersTargetTimeInStage()
		{
			Case c = new Case();
			c.TargetTimeInStage = "blah";
			DataConversions.ConvertCase5_2(c);
			Assert.AreEqual(1, c.Conditions.Count);
			Assert.IsNull(c.TargetTimeInStage);
			Assert.AreEqual("target", c.Conditions[0].Role);
			Assert.AreEqual("blah", c.Conditions[0].TimeInStage);
		}

		[TestMethod]
		public void TransfersTargetConsecutiveLosses()
		{
			Case c = new Case();
			TriggerDatabase.AddTrigger(new TriggerDefinition("opponent_lost", "opponent_lost") { HasTarget = true });
			c.Tag = "opponent_lost";
			c.ConsecutiveLosses = "blah";
			DataConversions.ConvertCase5_2(c);
			Assert.AreEqual(1, c.Conditions.Count);
			Assert.IsNull(c.ConsecutiveLosses);
			Assert.AreEqual("target", c.Conditions[0].Role);
			Assert.AreEqual("blah", c.Conditions[0].ConsecutiveLosses);
		}

		[TestMethod]
		public void TransfersSelfHand()
		{
			Case c = new Case();
			c.HasHand = "blah";
			DataConversions.ConvertCase5_2(c);
			Assert.AreEqual(1, c.Conditions.Count);
			Assert.IsNull(c.HasHand);
			Assert.AreEqual("self", c.Conditions[0].Role);
			Assert.AreEqual("blah", c.Conditions[0].Hand);
		}

		[TestMethod]
		public void TransfersSelfSaidMarker()
		{
			Case c = new Case();
			c.SaidMarker = "blah";
			DataConversions.ConvertCase5_2(c);
			Assert.AreEqual(1, c.Conditions.Count);
			Assert.IsNull(c.SaidMarker);
			Assert.AreEqual("self", c.Conditions[0].Role);
			Assert.AreEqual("blah", c.Conditions[0].SaidMarker);
		}

		[TestMethod]
		public void TransfersSelfNotSaidMarker()
		{
			Case c = new Case();
			c.NotSaidMarker = "blah";
			DataConversions.ConvertCase5_2(c);
			Assert.AreEqual(1, c.Conditions.Count);
			Assert.IsNull(c.NotSaidMarker);
			Assert.AreEqual("self", c.Conditions[0].Role);
			Assert.AreEqual("blah", c.Conditions[0].NotSaidMarker);
		}

		[TestMethod]
		public void TransfersSelfTimeInStage()
		{
			Case c = new Case();
			c.TimeInStage = "blah";
			DataConversions.ConvertCase5_2(c);
			Assert.AreEqual(1, c.Conditions.Count);
			Assert.IsNull(c.TimeInStage);
			Assert.AreEqual("self", c.Conditions[0].Role);
			Assert.AreEqual("blah", c.Conditions[0].TimeInStage);
		}

		[TestMethod]
		public void TransfersSelfConsecutiveLosses()
		{
			Case c = new Case();
			c.ConsecutiveLosses = "blah";
			DataConversions.ConvertCase5_2(c);
			Assert.AreEqual(1, c.Conditions.Count);
			Assert.IsNull(c.ConsecutiveLosses);
			Assert.AreEqual("self", c.Conditions[0].Role);
			Assert.AreEqual("blah", c.Conditions[0].ConsecutiveLosses);
		}

		[TestMethod]
		public void TransfersAlsoPlaying()
		{
			Case c = new Case();
			c.AlsoPlaying = "bob";
			DataConversions.ConvertCase5_2(c);
			Assert.AreEqual(1, c.Conditions.Count);
			Assert.IsNull(c.AlsoPlaying);
			Assert.AreEqual("other", c.Conditions[0].Role);
			Assert.AreEqual("bob", c.Conditions[0].Character);
		}

		[TestMethod]
		public void TransfersAlsoPlayingStage()
		{
			Case c = new Case();
			c.AlsoPlayingStage = "0-5";
			DataConversions.ConvertCase5_2(c);
			Assert.AreEqual(1, c.Conditions.Count);
			Assert.IsNull(c.AlsoPlayingStage);
			Assert.AreEqual("other", c.Conditions[0].Role);
			Assert.AreEqual("0-5", c.Conditions[0].Stage);
		}

		[TestMethod]
		public void TransfersAlsoPlayingHand()
		{
			Case c = new Case();
			c.AlsoPlayingHand = "blah";
			DataConversions.ConvertCase5_2(c);
			Assert.AreEqual(1, c.Conditions.Count);
			Assert.IsNull(c.AlsoPlayingHand);
			Assert.AreEqual("other", c.Conditions[0].Role);
			Assert.AreEqual("blah", c.Conditions[0].Hand);
		}

		[TestMethod]
		public void TransfersAlsoPlayingSaidMarker()
		{
			Case c = new Case();
			c.AlsoPlayingSaidMarker = "blah";
			DataConversions.ConvertCase5_2(c);
			Assert.AreEqual(1, c.Conditions.Count);
			Assert.IsNull(c.AlsoPlayingSaidMarker);
			Assert.AreEqual("other", c.Conditions[0].Role);
			Assert.AreEqual("blah", c.Conditions[0].SaidMarker);
		}

		[TestMethod]
		public void TransfersAlsoPlayingSayingMarker()
		{
			Case c = new Case();
			c.AlsoPlayingSayingMarker = "blah";
			DataConversions.ConvertCase5_2(c);
			Assert.AreEqual(1, c.Conditions.Count);
			Assert.IsNull(c.AlsoPlayingSayingMarker);
			Assert.AreEqual("other", c.Conditions[0].Role);
			Assert.AreEqual("blah", c.Conditions[0].SayingMarker);
		}

		[TestMethod]
		public void TransfersAlsoPlayingNotSaidMarker()
		{
			Case c = new Case();
			c.AlsoPlayingNotSaidMarker = "blah";
			DataConversions.ConvertCase5_2(c);
			Assert.AreEqual(1, c.Conditions.Count);
			Assert.IsNull(c.AlsoPlayingNotSaidMarker);
			Assert.AreEqual("other", c.Conditions[0].Role);
			Assert.AreEqual("blah", c.Conditions[0].NotSaidMarker);
		}

		[TestMethod]
		public void TransfersAlsoPlayingSaying()
		{
			Case c = new Case();
			c.AlsoPlayingSaying = "blah";
			DataConversions.ConvertCase5_2(c);
			Assert.AreEqual(1, c.Conditions.Count);
			Assert.IsNull(c.AlsoPlayingSaying);
			Assert.AreEqual("other", c.Conditions[0].Role);
			Assert.AreEqual("blah", c.Conditions[0].Saying);
		}

		[TestMethod]
		public void TransfersAlsoPlayingTimeInStage()
		{
			Case c = new Case();
			c.AlsoPlayingTimeInStage = "blah";
			DataConversions.ConvertCase5_2(c);
			Assert.AreEqual(1, c.Conditions.Count);
			Assert.IsNull(c.AlsoPlayingTimeInStage);
			Assert.AreEqual("other", c.Conditions[0].Role);
			Assert.AreEqual("blah", c.Conditions[0].TimeInStage);
		}
	}
}
