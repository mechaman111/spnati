using Microsoft.VisualStudio.TestTools.UnitTesting;
using SPNATI_Character_Editor.Providers;
using System;

namespace UnitTests
{
	[TestClass]
	public class DateTests
	{
		[TestMethod]
		public void ToTime_Future()
		{
			DateTime last = new DateTime(2000, 1, 1);
			DateTime now = new DateTime(1900, 1, 1, 0, 0, 0);
			Assert.AreEqual("In the future", CharacterProvider.GetTimeSince(last, now));
		}

		[TestMethod]
		public void ToTime_Now()
		{
			DateTime last = new DateTime(2000, 1, 1);
			DateTime now = new DateTime(2000, 1, 1);
			Assert.AreEqual("Just now", CharacterProvider.GetTimeSince(last, now));
		}

		[TestMethod]
		public void ToTime_UnderMinute()
		{
			DateTime last = new DateTime(2000, 1, 1, 0, 0, 0);
			DateTime now = new DateTime(2000, 1, 1, 0, 0, 30);
			Assert.AreEqual("Just now", CharacterProvider.GetTimeSince(last, now));
		}

		[TestMethod]
		public void ToTime_OneMinute()
		{
			DateTime last = new DateTime(2000, 1, 1, 0, 0, 0);
			DateTime now = new DateTime(2000, 1, 1, 0, 1, 0);
			Assert.AreEqual("1 minute ago", CharacterProvider.GetTimeSince(last, now));
		}

		[TestMethod]
		public void ToTime_TwoMinutes()
		{
			DateTime last = new DateTime(2000, 1, 1, 0, 0, 0);
			DateTime now = new DateTime(2000, 1, 1, 0, 2, 0);
			Assert.AreEqual("2 minutes ago", CharacterProvider.GetTimeSince(last, now));
		}

		[TestMethod]
		public void ToTime_OneHour()
		{
			DateTime last = new DateTime(2000, 1, 1, 0, 0, 0);
			DateTime now = new DateTime(2000, 1, 1, 1, 0, 0);
			Assert.AreEqual("1 hour ago", CharacterProvider.GetTimeSince(last, now));
		}

		[TestMethod]
		public void ToTime_TwoHours()
		{
			DateTime last = new DateTime(2000, 1, 1, 0, 0, 0);
			DateTime now = new DateTime(2000, 1, 1, 2, 0, 0);
			Assert.AreEqual("2 hours ago", CharacterProvider.GetTimeSince(last, now));
		}

		[TestMethod]
		public void ToTime_OneDay()
		{
			DateTime last = new DateTime(2000, 1, 1, 0, 0, 0);
			DateTime now = new DateTime(2000, 1, 2, 0, 0, 0);
			Assert.AreEqual("1 day ago", CharacterProvider.GetTimeSince(last, now));
		}

		[TestMethod]
		public void ToTime_TwoDays()
		{
			DateTime last = new DateTime(2000, 1, 1, 0, 0, 0);
			DateTime now = new DateTime(2000, 1, 3, 0, 0, 0);
			Assert.AreEqual("2 days ago", CharacterProvider.GetTimeSince(last, now));
		}

		[TestMethod]
		public void ToTime_OneWeek()
		{
			DateTime last = new DateTime(2000, 1, 1, 0, 0, 0);
			DateTime now = new DateTime(2000, 1, 8, 0, 0, 0);
			Assert.AreEqual("1 week ago", CharacterProvider.GetTimeSince(last, now));
		}

		[TestMethod]
		public void ToTime_TwoWeeks()
		{
			DateTime last = new DateTime(2000, 1, 1, 0, 0, 0);
			DateTime now = new DateTime(2000, 1, 15, 0, 0, 0);
			Assert.AreEqual("2 weeks ago", CharacterProvider.GetTimeSince(last, now));
		}

		[TestMethod]
		public void ToTime_OneMonth()
		{
			DateTime last = new DateTime(2000, 1, 1, 0, 0, 0);
			DateTime now = new DateTime(2000, 2, 1, 0, 0, 0);
			Assert.AreEqual("1 month ago", CharacterProvider.GetTimeSince(last, now));
		}

		[TestMethod]
		public void ToTime_TwoMonths()
		{
			DateTime last = new DateTime(2000, 1, 1, 0, 0, 0);
			DateTime now = new DateTime(2000, 3, 1, 0, 0, 0);
			Assert.AreEqual("2 months ago", CharacterProvider.GetTimeSince(last, now));
		}

		[TestMethod]
		public void ToTime_ElevenMonths()
		{
			DateTime last = new DateTime(2000, 1, 1, 0, 0, 0);
			DateTime now = new DateTime(2000, 12, 1, 0, 0, 0);
			Assert.AreEqual("11 months ago", CharacterProvider.GetTimeSince(last, now));
		}

		[TestMethod]
		public void ToTime_Year()
		{
			DateTime last = new DateTime(2000, 1, 1, 0, 0, 0);
			DateTime now = new DateTime(2001, 1, 1, 0, 0, 0);
			Assert.AreEqual("Over a year ago", CharacterProvider.GetTimeSince(last, now));
		}
	}
}
