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
			scene.Directives.Add(new Directive("move")
			{
				Id = "s",
				Looped = true,
				Keyframes = new List<Keyframe>(new Keyframe[] {
				new Keyframe() { Time = "0", Rotation = "10" },
				new Keyframe() { Time = "0.5", X="2001", Rotation = "-38" },
				new Keyframe() { Time = "1.5", X="2000", Rotation = "4" },
				new Keyframe() { Time = "2", X="2003", Rotation = "0" }
			})
			});
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

		[TestMethod]
		public void StopMidSegment()
		{
			Scene scene = new Scene();
			scene.Directives.Add(new Directive("sprite") { Id = "s", X = "100" });
			scene.Directives.Add(new Directive("move") { Id = "s", X = "200", Time = "2", Looped = true });
			scene.Directives.Add(new Directive("stop") { Id = "s", Delay = "6" });

			LiveScene live = new LiveScene(scene, _character);
			Scene result = new Scene();
			result.CreateFrom(live);

			Assert.AreEqual(3, result.Directives.Count);
			Assert.AreEqual("move", result.Directives[1].DirectiveType);
			Assert.IsTrue(result.Directives[1].Looped);
			Assert.AreEqual("stop", result.Directives[2].DirectiveType);
		}

		[TestMethod]
		public void StopStartSegment()
		{
			Scene scene = new Scene();
			scene.Directives.Add(new Directive("sprite") { Id = "s", X = "100" });
			scene.Directives.Add(new Directive("move") { Id = "s", X = "200", Time = "2", Looped = true });
			scene.Directives.Add(new Directive("pause"));
			scene.Directives.Add(new Directive("stop") { Id = "s" });
			scene.Directives.Add(new Directive("pause"));

			LiveScene live = new LiveScene(scene, _character);
			Scene result = new Scene();
			result.CreateFrom(live);

			Assert.AreEqual(5, result.Directives.Count);
			Assert.AreEqual("move", result.Directives[1].DirectiveType);
			Assert.IsTrue(result.Directives[1].Looped);
			Assert.AreEqual("pause", result.Directives[2].DirectiveType);
			Assert.AreEqual("stop", result.Directives[3].DirectiveType);
		}

		[TestMethod]
		public void StopPartialAnimation()
		{
			Scene scene = new Scene();
			scene.Directives.Add(new Directive("sprite") { Id = "s", X = "100" });
			scene.Directives.Add(new Directive("move") { Id = "s", X = "200", Time = "2", Looped = true });
			scene.Directives.Add(new Directive("move") { Id = "s", Y = "300", Time = "2", Looped = true });
			scene.Directives.Add(new Directive("pause"));
			scene.Directives.Add(new Directive("stop") { Id = "s", RawProperties = "x" });
			scene.Directives.Add(new Directive("pause"));

			LiveScene live = new LiveScene(scene, _character);
			Scene result = new Scene();
			result.CreateFrom(live);

			Assert.AreEqual(5, result.Directives.Count);
			Assert.AreEqual("move", result.Directives[1].DirectiveType);
			Assert.IsTrue(result.Directives[1].Looped);
			Assert.AreEqual("pause", result.Directives[2].DirectiveType);
			Assert.AreEqual("stop", result.Directives[3].DirectiveType);
			Assert.AreEqual("x", result.Directives[3].RawProperties);
		}

		[TestMethod]
		public void AnimatesFromDefault()
		{
			Scene scene = new Scene();
			scene.Directives.Add(new Directive("sprite") { Id = "s" });
			scene.Directives.Add(new Directive("move") { Id = "s", X = "200", Time = "2" });

			LiveScene live = new LiveScene(scene, _character);
			Scene result = new Scene();
			result.CreateFrom(live);

			Assert.AreEqual(2, result.Directives.Count);
		}

		[TestMethod]
		public void NoMergingDifferentLoopTypes()
		{
			Scene scene = new Scene();
			scene.Directives.Add(new Directive("sprite") { Id = "s", X = "100" });
			scene.Directives.Add(new Directive("move") { Id = "s", X = "200", Time = "2" });
			scene.Directives.Add(new Directive("move") { Id = "s", Y = "300", Time = "2", EasingMethod = "bounce" });

			LiveScene live = new LiveScene(scene, _character);
			Scene result = new Scene();
			result.CreateFrom(live);

			Assert.AreEqual(3, result.Directives.Count);
			Assert.AreEqual("200", result.Directives[1].X);
			Assert.AreEqual(null, result.Directives[1].EasingMethod);
			Assert.AreEqual("300", result.Directives[2].Y);
			Assert.AreEqual("bounce", result.Directives[2].EasingMethod);
		}

		[TestMethod]
		public void MergesSparseLoop()
		{
			Scene scene = new Scene();
			scene.Directives.Add(new Directive("sprite") { Id = "s" });
			scene.Directives.Add(new Directive("move")
			{
				Id = "s",
				Delay = "2",
				Keyframes = new List<Keyframe>(new Keyframe[] {
				new Keyframe() { X = "5", Time = "0" },
				new Keyframe() { X = "9", Time = "10" },
			})
			});
			scene.Directives.Add(new Directive("move")
			{
				Id = "s",
				Delay = "2",
				Keyframes = new List<Keyframe>(new Keyframe[] {
				new Keyframe() { Y = "4", Time = "5" },
				new Keyframe() { Y = "8", Time = "10" },
			})
			});

			LiveScene live = new LiveScene(scene, _character);
			Scene result = new Scene();
			result.CreateFrom(live);

			Assert.AreEqual(2, result.Directives.Count);
			Directive d = result.Directives[1];
			Assert.AreEqual(3, d.Keyframes.Count);
			Assert.AreEqual(null, d.Keyframes[0].Time);
			Assert.AreEqual("5", d.Keyframes[0].X);
			Assert.AreEqual(null, d.Keyframes[0].Y);

			Assert.AreEqual("5", d.Keyframes[1].Time);
			Assert.AreEqual(null, d.Keyframes[1].X);
			Assert.AreEqual("4", d.Keyframes[1].Y);

			Assert.AreEqual("10", d.Keyframes[2].Time);
			Assert.AreEqual("9", d.Keyframes[2].X);
			Assert.AreEqual("8", d.Keyframes[2].Y);
		}

		[TestMethod]
		public void UsesCameraTypeForCameraAnimation()
		{
			Scene scene = new Scene();
			scene.Directives.Add(new Directive("camera") { Time = "2", X = "5" });

			LiveScene live = new LiveScene(scene, _character);
			Scene result = new Scene();
			result.CreateFrom(live);

			Assert.AreEqual("camera", result.Directives[0].DirectiveType);
		}

		[TestMethod]
		public void UsesFadeTypeForOverlayAnimation()
		{
			Scene scene = new Scene();
			scene.Directives.Add(new Directive("fade") { Time = "2", Alpha = "5" });

			LiveScene live = new LiveScene(scene, _character);
			Scene result = new Scene();
			result.CreateFrom(live);

			Assert.AreEqual("fade", result.Directives[0].DirectiveType);
		}

		[TestMethod]
		public void KeepsCameraAndFadeSeparate()
		{
			Scene scene = new Scene();
			scene.Directives.Add(new Directive("camera") { Time = "2", Alpha = "5", X = "5", Y = "10", Zoom = "5", Color = "#FAFAFA" });

			LiveScene live = new LiveScene(scene, _character);
			Scene result = new Scene();
			result.CreateFrom(live);

			Assert.AreEqual(2, result.Directives.Count);
			Assert.AreEqual("camera", result.Directives[0].DirectiveType);
			Assert.AreEqual("5", result.Directives[0].X);
			Assert.AreEqual("10", result.Directives[0].Y);
			Assert.AreEqual("5", result.Directives[0].Zoom);
			Assert.AreEqual(null, result.Directives[0].Alpha);
			Assert.AreEqual(null, result.Directives[0].Color);

			Assert.AreEqual("fade", result.Directives[1].DirectiveType);
			Assert.AreEqual(null, result.Directives[1].X);
			Assert.AreEqual(null, result.Directives[1].Y);
			Assert.AreEqual(null, result.Directives[1].Zoom);
			Assert.AreEqual("5", result.Directives[1].Alpha);
			Assert.AreEqual("#FAFAFA", result.Directives[1].Color);
		}

		[TestMethod]
		public void DelayedJump()
		{
			Scene scene = new Scene();
			scene.Directives.Add(new Directive("sprite") { Id = "s", Alpha = "0" });
			scene.Directives.Add(new Directive("move")
			{
				Id = "s",
				Delay = "0.5",
				Keyframes = new List<Keyframe>(new Keyframe[] {
				new Keyframe() { Alpha = "100", Time = "0" },
			})
			});

			LiveScene live = new LiveScene(scene, _character);
			Scene result = new Scene();
			result.CreateFrom(live);

			Assert.AreEqual(2, result.Directives.Count);
			Assert.AreEqual("move", result.Directives[1].DirectiveType);
			Assert.AreEqual(null, result.Directives[1].Time);
			Assert.AreEqual("0.5", result.Directives[1].Delay);
		}

		[TestMethod]
		public void MoveIntoLoop()
		{
			Scene scene = new Scene();
			scene.Directives.Add(new Directive("sprite") { Id = "s", X = "20" });
			scene.Directives.Add(new Directive("move") { Id = "s", Time = "1", X = "50", EasingMethod = "bounce" });
			scene.Directives.Add(new Directive("wait"));
			scene.Directives.Add(new Directive("move") { Id = "s", Looped = true, Time = "0.5", X = "100" });
			scene.Directives.Add(new Directive("pause"));

			LiveScene live = new LiveScene(scene, _character);
			Scene result = new Scene();
			result.CreateFrom(live);

			Assert.AreEqual(4, result.Directives.Count);
			Assert.AreEqual("move", result.Directives[1].DirectiveType);
			Assert.AreEqual("1", result.Directives[1].Time);
			Assert.AreEqual("bounce", result.Directives[1].EasingMethod);
			Assert.AreEqual("move", result.Directives[2].DirectiveType);
			Assert.AreEqual("1", result.Directives[2].Delay);
			Assert.AreEqual("0.5", result.Directives[2].Time);
		}

		[TestMethod]
		public void StartIntoMultiPropLoop()
		{
			Scene scene = new Scene();
			scene.Directives.Add(new Directive("sprite") { Id = "s", X = "100", Y = "200" });
			scene.Directives.Add(new Directive("move") { Id = "s", Time = "1", X = "50", Y = "150", EasingMethod = "bounce" });
			scene.Directives.Add(new Directive("move") { Id = "s", Time = "1", Delay = "1", Looped = true, X = "200", Y = "300" });
			scene.Directives.Add(new Directive("pause"));

			LiveScene live = new LiveScene(scene, _character);
			Scene result = new Scene();
			result.CreateFrom(live);

			Assert.AreEqual(4, result.Directives.Count);
			Assert.AreEqual("move", result.Directives[1].DirectiveType);
			Assert.AreEqual("bounce", result.Directives[1].EasingMethod);
			Assert.IsFalse(result.Directives[1].Looped);
			Assert.AreEqual("move", result.Directives[2].DirectiveType);
			Assert.IsTrue(result.Directives[2].Looped);
		}

		[TestMethod]
		public void MoveFromDefault()
		{
			Scene scene = new Scene();
			scene.Directives.Add(new Directive("sprite") { Id = "s" });
			scene.Directives.Add(new Directive("move") { Id = "s", Time = "1", Rotation = "-20" });
			scene.Directives.Add(new Directive("wait"));
			scene.Directives.Add(new Directive("move")
			{
				Id = "s",
				Looped = true,
				Keyframes = new List<Keyframe>(new Keyframe[] {
				new Keyframe() { Time = "0.5", Rotation="-20" },
				new Keyframe() { Time = "1.5", Rotation="-10" },
				new Keyframe() { Time = "2", Rotation="-20" },
			})
			});
			scene.Directives.Add(new Directive("pause"));
			scene.Directives.Add(new Directive("pause"));

			LiveScene live = new LiveScene(scene, _character);
			Scene result = new Scene();
			result.CreateFrom(live);

			Assert.AreEqual(5, result.Directives.Count);
			Directive move = result.Directives[1];
			Assert.AreEqual("move", move.DirectiveType);
			Assert.AreEqual("-20", move.Rotation);
			Assert.AreEqual("1", move.Time);

			Directive loop = result.Directives[2];
			Assert.AreEqual("move", loop.DirectiveType);
			Assert.AreEqual(3, loop.Keyframes.Count);
			Assert.AreEqual("0.5", loop.Keyframes[0].Time);
			Assert.AreEqual("1.5", loop.Keyframes[1].Time);
			Assert.AreEqual("2", loop.Keyframes[2].Time);
			Assert.AreEqual("-20", loop.Keyframes[0].Rotation);
			Assert.AreEqual("-10", loop.Keyframes[1].Rotation);
			Assert.AreEqual("-20", loop.Keyframes[2].Rotation);
		}

		[TestMethod]
		public void MoveFromDefault2()
		{
			Scene scene = new Scene();
			scene.Directives.Add(new Directive("sprite") { Id = "s" });
			scene.Directives.Add(new Directive("pause"));
			scene.Directives.Add(new Directive("move") { Id = "s", Time = "2", X = "2000", Y = "1900" });
			scene.Directives.Add(new Directive("wait"));
			scene.Directives.Add(new Directive("move")
			{
				Id = "s",
				Looped = true,
				Keyframes = new List<Keyframe>(new Keyframe[] {
					new Keyframe() { Time = "0.5", X = "2001", Y="1903", Rotation = "-38" },
					new Keyframe() { Time = "1.5",  X = "2000", Y="1903", Rotation = "4" },
					new Keyframe() { Time = "2",  X = "2003", Y="1919", Rotation = "0" },
				})
			});
			scene.Directives.Add(new Directive("pause"));

			LiveScene live = new LiveScene(scene, _character);
			Scene result = new Scene();
			result.CreateFrom(live);

			Assert.AreEqual(5, result.Directives.Count);
			Directive move = result.Directives[2];
			Assert.AreEqual("move", move.DirectiveType);
			Assert.AreEqual("2000", move.X);
			Assert.AreEqual("1900", move.Y);
			Assert.AreEqual("2", move.Time);

			Directive loop = result.Directives[3];
			Assert.AreEqual("move", loop.DirectiveType);
			Assert.AreEqual(3, loop.Keyframes.Count);
			Assert.AreEqual("0.5", loop.Keyframes[0].Time);
			Assert.AreEqual("1.5", loop.Keyframes[1].Time);
			Assert.AreEqual("2", loop.Keyframes[2].Time);
			Assert.AreEqual("-38", loop.Keyframes[0].Rotation);
			Assert.AreEqual("4", loop.Keyframes[1].Rotation);
			Assert.AreEqual("0", loop.Keyframes[2].Rotation);
			Assert.AreEqual("2001", loop.Keyframes[0].X);
			Assert.AreEqual("2000", loop.Keyframes[1].X);
			Assert.AreEqual("2003", loop.Keyframes[2].X);
			Assert.AreEqual("1903", loop.Keyframes[0].Y);
			Assert.AreEqual("1903", loop.Keyframes[1].Y);
			Assert.AreEqual("1919", loop.Keyframes[2].Y);
		}
	}
}
