using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SPNATI_Character_Editor.ImageImport;

namespace UnitTests
{
	[TestClass]
	public class ImageMetadataTests
	{
		[TestMethod]
		public void CorrectFormat()
		{
			ImageMetadata image = new ImageMetadata("", "12**agesg");
			Assert.IsTrue(image.StartsWithVersion());
		}

		[TestMethod]
		public void MissingStars()
		{
			ImageMetadata image = new ImageMetadata("", "12agesg");
			Assert.IsFalse(image.StartsWithVersion());
		}

		[TestMethod]
		public void MissingNumber()
		{
			ImageMetadata image = new ImageMetadata("", "agesg");
			Assert.IsFalse(image.StartsWithVersion());
		}
	}
}
