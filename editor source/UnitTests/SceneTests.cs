using Microsoft.VisualStudio.TestTools.UnitTesting;
using SPNATI_Character_Editor;
using SPNATI_Character_Editor.EpilogueEditor;

namespace UnitTests
{
	/// <summary>
	/// These are mostly cases that came up while testing existing epilogues rather preemptively unit testing
	/// </summary>
	[TestClass]
	public class SceneTests
	{
		private Character _character;

		[TestInitialize]
		public void Initialize()
		{
			_character = new Character();
			_character.FolderName = "bob";

			ShellLogic.BuildDirectiveTypes();
		}

		[TestMethod]
		public void BasicSpriteAndText()
		{
			Scene scene = new Scene();
			scene.Directives.Add(new Directive("sprite") { Id = "sprite1", Src = "test.png" });
			scene.Directives.Add(new Directive("text") { Id = "text1", Text = "Hi" });

			LiveScene live = new LiveScene(scene, _character);
			Scene result = new Scene();
			result.CreateFrom(live);

			Assert.AreEqual(2, result.Directives.Count);
			Assert.AreEqual("sprite", result.Directives[0].DirectiveType);
			Assert.AreEqual("sprite1", result.Directives[0].Id);
			Assert.AreEqual("test.png", result.Directives[0].Src);
			Assert.AreEqual("text", result.Directives[1].DirectiveType);
			Assert.AreEqual("text1", result.Directives[1].Id);
			Assert.AreEqual("Hi", result.Directives[1].Text);
		}

		[TestMethod]
		public void MidTextChange()
		{
			Scene scene = new Scene();
			scene.Directives.Add(new Directive("text") { Text = "Text that lasts until halfway through the next segment" });
			scene.Directives.Add(new Directive("pause"));
			scene.Directives.Add(new Directive("clear-all") { Delay = "0.5" });
			scene.Directives.Add(new Directive("text") { Text = "TEST", Delay = "0.5" });
			scene.Directives.Add(new Directive("pause"));
			scene.Directives.Add(new Directive("clear-all"));

			LiveScene live = new LiveScene(scene, _character);
			Scene result = new Scene();
			result.CreateFrom(live);
			
			Assert.AreEqual("clear-all", result.Directives[2].DirectiveType);
			Assert.AreEqual("text", result.Directives[3].DirectiveType);
		}
	}
}
