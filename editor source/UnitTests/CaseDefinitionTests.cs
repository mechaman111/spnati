using Microsoft.VisualStudio.TestTools.UnitTesting;
using SPNATI_Character_Editor;
using SPNATI_Character_Editor.DataStructures;

namespace UnitTests
{
	[TestClass]
	public class CaseDefinitionTests
	{
		private Case foo;

		[TestInitialize]
		public void Initialize()
		{
			foo = new Case("foo");
			CaseDefinitionDatabase.Loader = () =>
			{
				CaseDefinitionDatabase.AddGroup(1, "Fun");
				CaseDefinitionDatabase.AddGroup(2, "Stuff");

				CaseDefinitionDatabase.AddDefinition(foo, "Foo");
			};
		}

		[TestCleanup]
		public void CleanUp()
		{
			CaseDefinitionDatabase.Reset();
		}

		[TestMethod]
		public void GetDefinition_FindsBasicCase()
		{
			CaseDefinition def = CaseDefinitionDatabase.GetDefinition(foo);
			Assert.AreEqual("Foo", def.DisplayName);
		}

		[TestMethod]
		public void GetDefinition_FindsMatchingCase()
		{
			Case clone = new Case("foo");
			CaseDefinition def = CaseDefinitionDatabase.GetDefinition(clone);
			Assert.AreEqual("Foo", def.DisplayName);
		}

		[TestMethod]
		public void UpdateDefinition_UpdatesTag()
		{
			Case c = new Case("bar");
			CaseDefinition def = CaseDefinitionDatabase.AddDefinition(c, "Bar");
			def.Case.Tag = "bar2";
			CaseDefinitionDatabase.UpdateDefinition(def);

			Case c2 = new Case("bar2");
			def = CaseDefinitionDatabase.GetDefinition(c2);
			Assert.AreEqual("Bar", def.DisplayName);
		}

		[TestMethod]
		public void AddDefinition_AssignsUniqueId()
		{
			Case c = new Case("bar");
			CaseDefinition def = CaseDefinitionDatabase.AddDefinition(c, "Bar");
			Case c2 = new Case("bar2");
			CaseDefinition def2 = CaseDefinitionDatabase.AddDefinition(c2, "Bar2");
			Assert.AreNotEqual(def.Id, def2.Id);
		}
	}
}
