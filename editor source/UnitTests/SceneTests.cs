using Microsoft.VisualStudio.TestTools.UnitTesting;
using SPNATI_Character_Editor;
using SPNATI_Character_Editor.EpilogueEditor;
using System.Collections.Generic;

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

		[TestMethod]
		public void MovementIntoLoopWithNewProperty()
		{
			Scene scene = new Scene();
			scene.Directives.Add(new Directive("sprite") { Id = "s", X = "1450" });
			scene.Directives.Add(new Directive("pause"));
			scene.Directives.Add(new Directive("move") { Id = "s", X = "2000", Time = "2" });
			scene.Directives.Add(new Directive("wait"));
			scene.Directives.Add(new Directive("move") { Id= "s", Looped = true, Keyframes = new List<Keyframe>(new Keyframe[] {
				new Keyframe() { Time = "0", Rotation = "0" },
				new Keyframe() { Time = "0.5", X="2001", Rotation = "-38" },
				new Keyframe() { Time = "1.5", X="2000", Rotation = "4" },
				new Keyframe() { Time = "2", X="2003", Rotation = "0" }
			})});
			scene.Directives.Add(new Directive("pause"));

			LiveScene live = new LiveScene(scene, _character);
			Scene result = new Scene();
			result.CreateFrom(live);

			Directive move = result.Directives[2];
			Assert.AreEqual("move", move.DirectiveType);
			Assert.AreEqual(0, move.Keyframes.Count);
			Assert.AreEqual("2", move.Time);
			Assert.IsFalse(move.Looped);

			Directive loop = result.Directives[3];
			Assert.AreEqual("move", loop.DirectiveType);
			Assert.AreEqual(4, loop.Keyframes.Count);
			Assert.IsTrue(loop.Looped);
		}
	}
}
