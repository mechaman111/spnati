using Desktop.CommonControls.PropertyControls;
using SPNATI_Character_Editor.Controls;
using System;
using System.IO;

namespace SPNATI_Character_Editor.EpilogueEditor
{
	public class LiveSpriteKeyframe : LiveKeyframe
	{
		public LiveSpriteKeyframe() : base()
		{
			TrackedProperties.Add("Src");
			TrackedProperties.Add("ScaleX");
			TrackedProperties.Add("ScaleY");
			TrackedProperties.Add("Alpha");
			TrackedProperties.Add("Rotation");
			TrackedProperties.Add("SkewX");
			TrackedProperties.Add("SkewY");
		}

		[FileSelect(DisplayName = "Source", GroupOrder = 10, Key = "src", Description = "Sprite source image")]
		public string Src
		{
			get { return Get<string>(); }
			set
			{
				if (value == Src)
				{
					return;
				}
				if (Data.AllowsCrossStageImages)
				{
					string filename = Path.GetFileName(value);
					int stage;
					string id;
					PoseMap.ParseImage(filename, out stage, out id);
					if (stage >= 0)
					{
						value = value.Replace($"{stage}-", "#-");
					}
				}
				Set(value);
			}
		}

		[Float(DisplayName = "Scale X", GroupOrder = 40, Key = "scalex", Increment = 0.1f, Minimum = -1000, Maximum = 1000)]
		public float? ScaleX
		{
			get { return Get<float?>(); }
			set { Set(value); }
		}

		[Float(DisplayName = "Scale Y", GroupOrder = 45, Key = "scaley", Increment = 0.1f, Minimum = -1000, Maximum = 1000)]
		public float? ScaleY
		{
			get { return Get<float?>(); }
			set { Set(value); }
		}

		[Slider(DisplayName = "Opacity (0-100)", GroupOrder = 30, Key = "alpha", Description = "Opacity/transparency level")]
		public float? Alpha
		{
			get { return Get<float?>(); }
			set { Set(value); }
		}

		[Float(DisplayName = "Rotation (deg)", GroupOrder = 50, Key = "rotation", Description = "Sprite rotation", DecimalPlaces = 0, Minimum = -7020, Maximum = 7020)]
		public float? Rotation
		{
			get { return Get<float?>(); }
			set { Set(value); }
		}

		[Float(DisplayName = "Skew X", GroupOrder = 60, Key = "skewx", Description = "Sprite shearing factor horizontally", DecimalPlaces = 2, Minimum = -89, Maximum = 89, Increment = 1f)]
		public float? SkewX
		{
			get { return Get<float?>(); }
			set { Set(value); }
		}

		[Float(DisplayName = "Skew Y", GroupOrder = 65, Key = "skewx", Description = "Sprite shearing factor vertically", DecimalPlaces = 2, Minimum = -89, Maximum = 89, Increment = 1f)]
		public float? SkewY
		{
			get { return Get<float?>(); }
			set { Set(value); }
		}

		protected override object GetDefaultValue(string property)
		{
			switch (property)
			{
				case "ScaleX":
				case "ScaleY":
					return 1.0f;
				case "Alpha":
					return 100f;
				default: return base.GetDefaultValue(property);
			}
		}

		/// <summary>
		/// Get the actual source path for this keyframe, relative to opponents/
		/// </summary>
		/// <param name="character"></param>
		/// <returns></returns>
		public string GetActualSrc(ISkin character)
		{
			if (String.IsNullOrEmpty(Src))
			{
				return null;
			}

			if (Src.StartsWith("/opponents/"))
			{
				return Src.Substring("/opponents/".Length);
			}
			else if (Src.StartsWith("opponents/"))
			{
				return Src.Substring("opponents/".Length);
			}

			if (character == null)
			{
				return Src;
			}

			string curFolderName = character.FolderName;
			if (curFolderName.StartsWith("opponents/"))
			{
				curFolderName = curFolderName.Substring("opponents/".Length);
			}

			if (Src.StartsWith(curFolderName + "/") || Src.StartsWith("reskins/"))
			{
				return Src;
			}
			else
			{
				foreach (Character c in CharacterDatabase.Characters)
				{
					if (Src.StartsWith(c.FolderName + "/"))
					{
						return Src;
					}
				}

				return curFolderName + "/" + Src;
			}
		}

		/// <summary>
		/// Corrects this sprite's source to the type of path used by scenes, which is relative to character's folder, or / if not in the character's folder
		/// </summary>
		/// <param name="scene"></param>
		/// <param name="character"></param>
		/// <returns></returns>
		public string GetSceneSrc(ISkin character)
		{
			if (String.IsNullOrEmpty(Src))
			{
				return null;
			}

			if (Src.StartsWith("/opponents/"))
			{
				return Src;
			}
			else if (Src.StartsWith("opponents/"))
			{
				return "/" + Src;
			}

			if (character == null)
			{
				return Src;
			}

			if (Src.StartsWith(character.FolderName + "/"))
            {
				return Src.Substring(character.FolderName.Length + 1);
            }
			else
            {
				return "/opponents/" + Src;
            }
		}
	}
}
