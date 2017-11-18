using KisekaeImporter.ImageImport;
using KisekaeImporter.SubCodes;
using System.Collections.Generic;
using System.IO;

namespace KisekaeImporter.ImageImport
{
	/// <summary>
	/// Template for auto-generating poses
	/// </summary>
	public class PoseTemplate
	{
		/// <summary>
		/// Character's body
		/// </summary>
		public KisekaeCode BaseCode = new KisekaeCode();

		/// <summary>
		/// Clothing per stage
		/// </summary>
		public List<StageTemplate> Stages = new List<StageTemplate>();

		/// <summary>
		/// Poses per emotion
		/// </summary>
		public List<Emotion> Emotions = new List<Emotion>();

		/// <summary>
		/// Creates a code out of a base, a clothing stage, and an emotion/position
		/// </summary>
		/// <param name="baseCode"></param>
		/// <param name="stage"></param>
		/// <param name="emotion"></param>
		/// <returns></returns>
		public static KisekaeCode CreatePose(KisekaeCode baseCode, StageTemplate stage, KisekaeCode poseCode)
		{
			KisekaeCode baseCopy = new KisekaeCode(baseCode);
			KisekaeCode poseCopy = new KisekaeCode(poseCode);

			KisekaeCode output = new KisekaeCode("", true);
			KisekaeHair resultHair = output.GetOrAddComponent<KisekaeHair>();

			KisekaeCode clothingCode = new KisekaeCode(stage.Code);

			//Only apply appearance components to the output
			output.ReplaceComponent(baseCopy.GetComponent<KisekaeAppearance>());
			output.ReplaceComponent(baseCopy.GetComponent<KisekaeFace>());
			output.ReplaceComponent(baseCopy.GetComponent<KisekaeHair>());

			//Bake the clothing into the result
			output.ReplaceComponent(clothingCode.GetComponent<KisekaeClothing>());

			//Manually apply any ahoge that doesn't appear in the base
			KisekaeHair clothingHair = clothingCode.GetComponent<KisekaeHair>();
			if (clothingHair != null)
			{
				foreach (KisekaeAhoge ahoge in clothingHair.GetSubCodeArrayItem<KisekaeAhoge>())
				{
					if (!resultHair.HasSubCode(ahoge.Id, ahoge.Index))
					{
						resultHair.ReplaceSubCode(ahoge);
					}
				}
			}

			//Apply the pose information
			output.ReplaceComponent(poseCopy.GetComponent<KisekaePose>());
			output.ReplaceComponent(poseCopy.GetComponent<KisekaeExpression>());

			//Manually set positions of belts and ahoge that appear in the pose
			KisekaeHair poseHair = poseCopy.GetComponent<KisekaeHair>();
			if (poseHair != null)
			{
				foreach (KisekaeAhoge ahoge in poseHair.GetSubCodeArrayItem<KisekaeAhoge>())
				{
					if (resultHair.HasSubCode(ahoge.Id, ahoge.Index))
					{
						KisekaeAhoge final = resultHair.GetAhoge(ahoge.Index);
						final.CopyPositioningFrom(ahoge);
					}
				}
			}
			KisekaeClothing resultClothing = output.GetOrAddComponent<KisekaeClothing>();
			KisekaeClothing poseClothing = poseCopy.GetComponent<KisekaeClothing>();
			if (poseClothing != null)
			{
				foreach (KisekaeBelt belt in poseClothing.GetSubCodeArrayItem<KisekaeBelt>())
				{
					if (resultClothing.HasSubCode(belt.Id, belt.Index))
					{
						KisekaeBelt final = resultClothing.GetBelt(belt.Index);
						final.CopyPositionFrom(belt);
					}
				}
			}
			KisekaeFace resultFace = output.GetOrAddComponent<KisekaeFace>();
			KisekaeFace poseFace = poseCopy.GetComponent<KisekaeFace>();
			if (poseFace != null)
			{
				foreach (KisekaeFacePaint paint in poseFace.GetSubCodeArrayItem<KisekaeFacePaint>())
				{
					if (resultFace.HasSubCode(paint.Id, paint.Index))
					{
						KisekaeFacePaint final = resultFace.GetFacePaint(paint.Index);
						final.CopyPositionFrom(paint);
					}
				}
			}

			//Add in stage blush, etc.
			KisekaeExpression expression = output.GetOrAddComponent<KisekaeExpression>();
			expression.Blush.Blush += stage.ExtraBlush;
			expression.Blush.Anger += stage.ExtraAnger;
			KisekaeAppearance appearance = output.GetOrAddComponent<KisekaeAppearance>();
			appearance.Vagina.Juice += stage.ExtraJuice;

			return output;
		}

		public void SaveToFile(string filename)
		{
			List<string> lines = new List<string>();
			lines.Add(string.Format("base={0}", BaseCode.Serialize()));
			for (int i = 0; i < Stages.Count; i++)
			{
				lines.Add(string.Format("stage={0}", Stages[i].Serialize()));
			}
			for (int i = 0; i < Emotions.Count; i++)
			{
				var emotion = Emotions[i];
				lines.Add(string.Format("emotion={0},{1}", emotion.Key, emotion.Code.Serialize()));
			}

			File.WriteAllLines(filename, lines);
		}

		public static PoseTemplate LoadFromFile(string filename)
		{
			string[] lines = File.ReadAllLines(filename);
			PoseTemplate template = new PoseTemplate();
			foreach (string line in lines)
			{
				string[] kvp = line.Split('=');
				if (kvp.Length == 1)
					continue;
				string key = kvp[0].ToLower();
				string value = kvp[1].ToLower();
				switch (key)
				{
					case "base":
						template.BaseCode = new KisekaeCode(value);
						break;
					case "stage":
						template.Stages.Add(new StageTemplate(value));
						break;
					case "emotion":
						string[] pieces = value.Split(',');
						if (pieces.Length == 1)
							continue;
						Emotion emotion = new Emotion(pieces[0], new KisekaeCode(pieces[1]));
						template.Emotions.Add(emotion);
						break;
				}
			}
			return template;
		}

		/// <summary>
		/// Creates a complete pose list for each stage+emotion combination
		/// </summary>
		/// <returns></returns>
		public PoseList GeneratePoseList()
		{
			PoseList poses = new PoseList();
			for (int stage = 0; stage < Stages.Count; stage++)
			{
				StageTemplate stageTemplate = Stages[stage];
				foreach (Emotion emotion in Emotions)
				{
					KisekaeCode finalCode = CreatePose(BaseCode, stageTemplate, emotion.Code);
					poses.Poses.Add(new ImageMetadata(string.Format("{0}-{1}", stage.ToString(), emotion.Key), finalCode.Serialize()));
				}
			}
			return poses;
		}
	}

	public class StageTemplate
	{
		/// <summary>
		/// Extra blush to apply to all emotions in this stage
		/// </summary>
		public int ExtraBlush;

		/// <summary>
		/// Extra anger to apply to all emotions in this stage
		/// </summary>
		public int ExtraAnger;

		/// <summary>
		/// Extra juice to apply to all emotions in this stage
		/// </summary>
		public int ExtraJuice;

		/// <summary>
		/// Clothing code for this stage
		/// </summary>
		public KisekaeCode Code;

		public StageTemplate()
		{
		}
		public StageTemplate(string data)
		{
			Deserialize(data);
		}

		public string Serialize()
		{
			return string.Format("blush:{0},anger:{1},juice:{2},{3}", ExtraBlush, ExtraAnger, ExtraJuice, Code.Serialize());
		}

		public void Deserialize(string data)
		{
			string[] pieces = data.Split(',');
			foreach (string piece in pieces)
			{
				string[] kvp = piece.Split(':');
				if (kvp.Length == 1)
				{
					Code = new KisekaeCode(piece);
				}
				else
				{
					string key = kvp[0].ToLower();
					int value;
					if (int.TryParse(kvp[1], out value))
					{
						switch (key)
						{
							case "blush":
								ExtraBlush = value;
								break;
							case "anger":
								ExtraAnger = value;
								break;
							case "juice":
								ExtraJuice = value;
								break;
						}
					}
				}
			}
		}
	}

	public class Emotion
	{
		/// <summary>
		/// Key
		/// </summary>
		public string Key;
		/// <summary>
		/// Pose code
		/// </summary>
		public KisekaeCode Code;

		public Emotion()
		{
		}

		public Emotion(string key, KisekaeCode code)
		{
			Key = key;
			Code = code;
		}
	}
}
