using Microsoft.VisualStudio.TestTools.UnitTesting;
using SPNATI_Character_Editor;
using SPNATI_Character_Editor.EpilogueEditor;
using System.Drawing;

namespace UnitTests
{
	[TestClass]
	public class SceneTests
	{
		[TestMethod]
		public void Scene_OverallProperties()
		{
			Scene s = new Scene(2, 5);
			Character c = new Character();
			c.FolderName = "test";
			LiveScene scene = new LiveScene(s, c);
			scene.Name = "foo";
			scene.BackgroundImage = "test/bob.png";
			scene.BackColor = Color.Magenta;
			scene.Width = 200;
			scene.Height = 400;

			s.CreateFrom(scene);
			Assert.AreEqual("foo", s.Name);
			Assert.AreEqual("bob.png", s.Background);
			Assert.AreEqual("200", s.Width);
			Assert.AreEqual("400", s.Height);
			Assert.AreEqual("#FF00FF", s.BackgroundColor);
		}

		[TestMethod]
		public void Scene_ExternalBackground()
		{
			Scene s = new Scene(2, 5);
			Character c = new Character();
			c.FolderName = "test";
			LiveScene scene = new LiveScene(s, c);
			scene.BackgroundImage = "other/file.png";

			s.CreateFrom(scene);
			Assert.AreEqual("/opponents/other/file.png", s.Background);
		}

		[TestMethod]
		public void Camera_FirstFrame_AppliesToScene()
		{
			Scene s = new Scene(2,5);
			Character c = new Character();
			c.FolderName = "test";
			LiveScene scene = new LiveScene(s, c);
			LiveCameraKeyframe kf = scene.Camera.Keyframes[0] as LiveCameraKeyframe;
			kf.X = 5;
			kf.Y = 6;
			kf.Zoom = 7.2f;
			kf.Color = ColorTranslator.FromHtml("#FF0000");
			kf.Opacity = 50;

			s.CreateFrom(scene);

			Assert.AreEqual("5", s.X);
			Assert.AreEqual("6", s.Y);
			Assert.AreEqual("7.2", s.Zoom);
			Assert.AreEqual("#FF0000", s.FadeColor);
			Assert.AreEqual("50", s.FadeOpacity);

			Assert.AreEqual(0, s.Directives.Count);
		}

		[TestMethod]
		public void Camera_SingleFrame_NoKeyframes()
		{
			Scene s = new Scene(2, 5);
			Character c = new Character();
			c.FolderName = "test";
			LiveScene scene = new LiveScene(s, c);
			LiveCameraKeyframe kf = scene.Camera.AddKeyframe(2) as LiveCameraKeyframe;
			kf.Zoom = 0.3f;

			s.CreateFrom(scene);

			Assert.AreEqual(1, s.Directives.Count);
			Directive d = s.Directives[0];
			Assert.AreEqual(0, d.Keyframes.Count);
			Assert.AreEqual("camera", d.DirectiveType);
			Assert.AreEqual("2", d.Time);
			Assert.IsNull(d.X);
			Assert.AreEqual("0.3", d.Zoom);
		}

		[TestMethod]
		public void Camera_MultipleConsecutiveFrames_Keyframes()
		{
			Scene s = new Scene(2, 5);
			Character c = new Character();
			c.FolderName = "test";
			LiveScene scene = new LiveScene(s, c);
			LiveCameraKeyframe lkf0 = scene.Camera.Keyframes[0] as LiveCameraKeyframe;
			lkf0.X = 0;
			lkf0.Y = 10;

			LiveCameraKeyframe lkf1 = scene.Camera.AddKeyframe(2) as LiveCameraKeyframe;
			lkf1.X = 10;
			lkf1.Y = 20;

			LiveCameraKeyframe lkf2 = scene.Camera.AddKeyframe(4) as LiveCameraKeyframe;
			lkf2.X = 20;

			s.CreateFrom(scene);

			Assert.AreEqual("0", s.X);
			Assert.AreEqual("10", s.Y);

			Assert.AreEqual(1, s.Directives.Count);
			Directive d = s.Directives[0];
			Assert.AreEqual(2, d.Keyframes.Count);
			Assert.IsNull(d.Time);
			Keyframe kf0 = d.Keyframes[0];
			Assert.AreEqual("2", kf0.Time);
			Assert.AreEqual("10", kf0.X);
			Assert.AreEqual("20", kf0.Y);

			Keyframe kf1 = d.Keyframes[1];
			Assert.AreEqual("4", kf1.Time);
			Assert.AreEqual("20", kf1.X);
			Assert.IsNull(kf1.Y);
		}
	}
}
