using KisekaeImporter.DataStructures.Kisekae;
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
		public static KisekaeCode CreatePose(KisekaeCode baseCode, StageTemplate stage, Emotion emotion)
		{
			KisekaeCode stageCode = new KisekaeCode(stage.Code);
			KisekaeCode poseCode = new KisekaeCode(emotion.Code);

			KisekaeCode output = new KisekaeCode("", true);
			output.MergeIn(baseCode, false, false);
			output.MergeIn(new KisekaeCode(stage.Code), false, false);

			//try to determine which assets from the pose don't apply
			for (int i = 0; i < poseCode.Models.Length; i++)
			{
				int assetIndex = 0;
				KisekaeModel poseModel = poseCode.Models[i];
				KisekaeModel baseModel = (baseCode.Models.Length > i ? baseCode.Models[i] : null);
				KisekaeExternalParts poseParts = poseModel?.GetComponent<KisekaeExternalParts>();
				KisekaeModel stageModel = (stageCode.Models.Length > i ? stageCode.Models[i] : null);
				KisekaeExternalParts baseParts = baseModel?.GetComponent<KisekaeExternalParts>();
				KisekaeExternalParts stageParts = stageModel?.GetComponent<KisekaeExternalParts>();
				if (poseParts != null)
				{
					for (int j = 0; j < 100; j++)
					{
						if (poseParts.HasPart(j))
						{
							SubCodes.KisekaeImage img = poseParts.GetPart(j);
							if (!img.IsEmpty)
							{
								if (baseParts?.HasPart(j) == true || stageParts?.HasPart(j) == true)
								{
									assetIndex++;
								}
								else if (assetIndex < poseModel.Assets.Count)
								{
									poseModel.Assets.RemoveAt(assetIndex);
								}
							}
						}
					}
				}
			}

			//Remove any belts and such that appear in the pose but not in the clothing or base
			foreach (KisekaeSubCode subcode in poseCode.GetSubCodesOfType<IPoseable>())
			{
				if (!baseCode.HasSubCode(subcode.Id, subcode.Index) && !stageCode.HasSubCode(subcode.Id, subcode.Index))
				{
					subcode.Reset();
				}
			}

			output.MergeIn(poseCode, false, true);

			//Add in stage blush, etc.
			KisekaeExpression expression = output.GetOrAddComponent<KisekaeExpression>();
			if (stage.ExtraBlush > 0)
			{
				expression.Blush.Blush += stage.ExtraBlush;
			}
			if (stage.ExtraAnger > 0)
			{
				expression.Blush.Anger += stage.ExtraAnger;
			}
			if (stage.ExtraJuice > 0)
			{
				KisekaeAppearance appearance = output.GetOrAddComponent<KisekaeAppearance>();
				appearance.Vagina.Juice += stage.ExtraJuice;
			}

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
				lines.Add(string.Format("emotion={0},{1},{2}", emotion.Key, emotion.Code, emotion.Crop.Serialize()));
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
						string l = "0";
						string t = "0";
						string r = "600";
						string b = "1400";
						if (pieces.Length >= 6)
						{
							l = pieces[2];
							t = pieces[3];
							r = pieces[4];
							b = pieces[5];
						}
						Emotion emotion = new Emotion(pieces[0], pieces[1], l, t, r, b);
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
				if (string.IsNullOrEmpty(stageTemplate.Code))
				{
					continue;
				}
				foreach (Emotion emotion in Emotions)
				{
					KisekaeCode finalCode = CreatePose(BaseCode, stageTemplate, emotion);
					ImageMetadata data = new ImageMetadata(string.Format("{0}-{1}", stage.ToString(), emotion.Key), finalCode.Serialize());
					data.CropInfo = emotion.Crop;
					poses.Poses.Add(data);
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
		public string Code;

		public StageTemplate()
		{
		}
		public StageTemplate(string data)
		{
			Deserialize(data);
		}

		public string Serialize()
		{
			return string.Format("blush:{0},anger:{1},juice:{2},{3}", ExtraBlush, ExtraAnger, ExtraJuice, Code);
		}

		public void Deserialize(string data)
		{
			string[] pieces = data.Split(',');
			foreach (string piece in pieces)
			{
				string[] kvp = piece.Split(':');
				if (kvp.Length == 1)
				{
					Code = piece;
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
		public string Code;
		/// <summary>
		/// Cropping region
		/// </summary>
		public Rect Crop = new Rect(0, 0, 600, 1400);

		public Emotion()
		{
		}

		public Emotion(string key, string code, string left, string top, string right, string bottom)
		{
			Key = key;
			Code = code;
			Crop = new Rect(left, top, right, bottom);
		}
	}
}
