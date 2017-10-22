using SPNATI_Character_Editor.ImageImport;
using System.Collections.Generic;
using System.IO;

namespace SPNATI_Character_Editor
{
	/// <summary>
	/// Template for auto-generating poses
	/// </summary>
	public class PoseTemplate
	{
		/// <summary>
		/// In advanced mode, codes won't be culled when combining the base, stage and emotion
		/// </summary>
		public bool AdvancedMode { get; set; }

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

		public void SaveToFile(string filename)
		{
			List<string> lines = new List<string>();
			lines.Add(string.Format("advanced={0}", AdvancedMode ? "1" : "0"));
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
					case "advanced":
						template.AdvancedMode = (value == "1");
						break;
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
				foreach (Emotion emotion in Emotions)
				{
					KisekaeCode baseCode = new KisekaeCode("", true);
					baseCode.MergeIn(BaseCode);
					KisekaeCode finalCode = new KisekaeCode(baseCode, Stages[stage], emotion.Code);
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
