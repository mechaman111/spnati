using Microsoft.VisualStudio.TestTools.UnitTesting;
using SPNATI_Character_Editor;

namespace UnitTests
{
	[TestClass]
	public class ConfigTests
	{
		[TestMethod]
		public void ExactUser()
		{
			Config.UserName = "Bobby";
			Assert.IsTrue(Config.IncludesUserName("Bobby"));
		}

		[TestMethod]
		public void SubUser()
		{
			Config.UserName = "Bob";
			Assert.IsFalse(Config.IncludesUserName("Bobby"));
		}

		[TestMethod]
		public void Ampersand()
		{
			Config.UserName = "Joe";
			Assert.IsTrue(Config.IncludesUserName("Joe & Mac"));
		}

		[TestMethod]
		public void AmpersandSecond()
		{
			Config.UserName = "Mac";
			Assert.IsTrue(Config.IncludesUserName("Joe & Mac"));
		}

		[TestMethod]
		public void Comma()
		{
			Config.UserName = "Joe";
			Assert.IsTrue(Config.IncludesUserName("Joe, Mac"));
		}

		[TestMethod]
		public void CommaSecond()
		{
			Config.UserName = "Mac";
			Assert.IsTrue(Config.IncludesUserName("Joe, Mac"));
		}

		[TestMethod]
		public void DropsWhitespace()
		{
			Config.UserName = "Joe";
			Assert.IsTrue(Config.IncludesUserName(" Joe "));
		}
	}
}
