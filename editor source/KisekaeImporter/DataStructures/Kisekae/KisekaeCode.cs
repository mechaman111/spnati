using KisekaeImporter.DataStructures.Kisekae;
using KisekaeImporter.SubCodes;
using System;
using System.Collections.Generic;
using System.Text;

namespace KisekaeImporter
{
	/// <summary>
	/// Data representation of a kisekae scene, which contains 1 or more characters + scene data
	/// For a single character without scene data, the format is VersionNumber**Code1_Code2_..._CodeN/#]Asset1/#]Asset2/#].../#]AssetN, where an Asset is a URL or font
	/// For a scene, the format is VersionNumber***Character1*Character2*...*Character9#/]SceneCode1_SceneCode2_..._SceneCodeN/#]SceneAsset1/#]SceneAsset2/#].../#]SceneAssetN, where Character is the format for a single character (minus VersionNumber**)
	/// </summary>
	public class KisekaeCode
	{
		private const string DefaultVersion = "68";

		/// <summary>
		/// Kisekae version this code was generated for
		/// </summary>
		public string Version { get; set; }

		public KisekaeModel[] Models = new KisekaeModel[9];
		
		public KisekaeChunk Scene = null;

		public KisekaeCode()
		{
			Version = DefaultVersion;
		}
		public KisekaeCode(KisekaeCode original)
		{
			string code = original.Serialize();
			Deserialize(code);
		}
		public KisekaeCode(KisekaeCode original, bool resetAll)
		{
			if (resetAll)
			{
				Reset(original.Version);
			}
			Deserialize(original.Serialize());
		}
		public KisekaeCode(string data) : this(data, false)
		{
		}
		public KisekaeCode(string data, bool resetAll)
		{
			if (resetAll)
			{
				Reset(data);
			}
			Deserialize(data);
		}

		private static string DefaultCode54;
		private static string DefaultCode68;
		static KisekaeCode()
		{
			DefaultCode54 = "54**ia_if_ib_id_ic_jc_ie_ja_jb_jd_je_jf_jg_ka_kb_kc_kd_ke_kf_la_lb_oa_os_ob_oc_od_oe_of_lc_og_oh_oo_op_oq_or_om_on_ok_ol_oi_oj_r00_s00_m00_n00_t00";
			DefaultCode68 = "68**ia_if_ib_id_ic_jc_ie_ja_jb_jd_je_jf_jg_ka_kb_kc_kd_ke_kf_la_lb_oa_os_ob_oc_od_oe_of_lc_og_oh_oo_op_oq_or_om_on_ok_ol_oi_oj_r00_s00_m00_n00_t00_f00";
		}

		public override int GetHashCode()
		{
			return Serialize().GetHashCode();
		}

		public void Reset(string data)
		{
			//Fill in empty subcodes to get a blank slate
			Deserialize(data.StartsWith("54") ? DefaultCode54 : DefaultCode68);
		}

		/// <summary>
		/// Merges another code into this one. Any existing subcodes will be replaced
		/// </summary>
		/// <param name="code">Code to merge into this one</param>
		public void MergeIn(KisekaeCode code, bool applyEmpties, bool poseOnly)
		{
			int mergingVersion;
			int version;
			if (int.TryParse(code.Version, out mergingVersion) && int.TryParse(Version, out version))
			{
				if (mergingVersion > version)
				{
					StepUpTransform(this, version, mergingVersion);
					Version = code.Version;
				}
				else if (mergingVersion < version)
				{
					StepUpTransform(code, mergingVersion, version);
				}
			}

			if (code.Scene != null)
			{
				if (Scene == null)
				{
					Scene = new KisekaeChunk("");
				}
				Scene.MergeIn(code.Scene, applyEmpties, poseOnly);
			}

			for (int i = 0; i < Models.Length; i++)
			{
				if (code.Models[i] != null)
				{
					if (Models[i] == null)
					{
						Models[i] = new KisekaeModel("");
					}
					Models[i].MergeIn(code.Models[i], applyEmpties, poseOnly);
				}
			}
		}

		public override string ToString()
		{
			return Serialize();
		}

		/// <summary>
		/// Converts a code into its string representation that Kisekae can import
		/// </summary>
		/// <returns></returns>
		public string Serialize()
		{
			if (string.IsNullOrEmpty(Version))
			{
				return "";
			}
			StringBuilder sb = new StringBuilder();
			sb.Append(Version);
			sb.Append("**");
			if (Scene != null)
			{
				for (int i = 0; i < Models.Length; i++)
				{
					sb.Append("*");
					KisekaeModel model = Models[i];
					if (model != null)
					{
						sb.Append(model.Serialize());
					}
					else
					{
						sb.Append("0");
					}
				}

				if (Scene != null)
				{
					sb.Append("#/]");
				}
				sb.Append(Scene.Serialize());
			}
			else
			{
				if (Models[0] != null)
				{
					sb.Append(Models[0].Serialize());
				}
			}

			return sb.ToString();
		}

		/// <summary>
		/// Gets whether a subcode with the given prefix exists in either the scene or any model
		/// </summary>
		/// <param name="prefix"></param>
		/// <returns></returns>
		public bool HasSubCode(string id, int index)
		{
			Type componentType = KisekaeSubCodeMap.GetComponentType(id);

			for (int i = 0; i < Models.Length; i++)
			{
				KisekaeModel model = Models[i];
				if (model != null)
				{
					KisekaeComponent component = model.GetComponent(componentType);
					if (component != null && component.HasSubCode(id, index))
					{
						return true;
					}
				}
			}
			if (Scene != null)
			{
				KisekaeComponent component = Scene.GetComponent(componentType);
				if (component != null && component.HasSubCode(id, index))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Gets all existing subcodes of the given type across all models
		/// </summary>
		/// <returns></returns>
		public IEnumerable<KisekaeSubCode> GetSubCodesOfType<T>()
		{
			for (int i = 0; i < Models.Length; i++)
			{
				KisekaeChunk model = Models[i];
				if (model != null)
				{
					foreach (KisekaeSubCode code in model.GetSubCodesOfType<T>())
					{
						yield return code;
					}
				}
			}

			if (Scene != null)
			{
				foreach (KisekaeSubCode code in Scene.GetSubCodesOfType<T>())
				{
					yield return code;
				}
			}
		}

		public T GetComponent<T>() where T : KisekaeComponent
		{
			if (Models[0] != null)
			{
				return Models[0].GetComponent(typeof(T)) as T;
			}
			return null;
		}

		public T GetOrAddComponent<T>() where T : KisekaeComponent
		{
			if (Models[0] == null)
			{
				Models[0] = new KisekaeModel("");
			}
			return Models[0].GetOrAddComponent<T>();
		}

		public void ReplaceComponent<T>(T component) where T : KisekaeComponent
		{
			if (Models[0] != null)
			{
				Models[0].ReplaceComponent(component);
			}
		}

		/// <summary>
		/// Deserializes a code into its parts
		/// </summary>
		/// <param name="data"></param>
		public void Deserialize(string data)
		{
			if (string.IsNullOrEmpty(data))
				return;

			//Extract the version and whether this is a scene or not
			string[] versionSplit = data.Split(new string[] { "**" }, StringSplitOptions.None);
			string subdata = "";
			if (versionSplit.Length == 1)
			{
				Version = DefaultVersion;
				subdata = data;
			}
			else
			{
				Version = versionSplit[0];
				subdata = versionSplit[1];
			}
			if (subdata.StartsWith("*"))
			{
				subdata = subdata.Substring(1);
				Scene = new KisekaeChunk("");
			}

			//split out the scene data
			string[] modelSceneSplit = subdata.Split(new string[] { "#/]" }, StringSplitOptions.None);

			//process models
			string[] models = modelSceneSplit[0].Split('*');
			for (int i = 0; i < models.Length; i++)
			{
				if (i >= Models.Length)
				{
					continue; //too many characters. This can't be valid.
				}
				string modelData = models[i];
				if (modelData == "0")
				{
					continue;
				}
				
				Models[i] = new KisekaeModel(models[i]);
			}

			//process scene
			if (modelSceneSplit.Length > 1)
			{
				Scene.Deserialize(modelSceneSplit[1]);
			}
		}

		public int TotalAssets
		{
			get
			{
				int count = 0;
				for (int i = 0; i < Models.Length; i++)
				{
					if (Models[i] != null)
					{
						count += Models[i].Assets.Count;
					}
				}
				if (Scene != null)
				{
					count += Scene.Assets.Count;
				}
				return count;
			}
		}

		/// <summary>
		/// Performs code transformations so merging separate versions doesn't screw things up
		/// </summary>
		/// <param name="code"></param>
		/// <param name="oldVersion"></param>
		/// <param name="newVersion"></param>
		private static void StepUpTransform(KisekaeCode code, int oldVersion, int newVersion)
		{
			for (int i = 0; i < code.Models.Length; i++)
			{
				KisekaeModel model = code.Models[i];
				if (model != null)
				{
					if (oldVersion < 83 && newVersion >= 83)
					{
						//mouth shapes
						KisekaeExpression expression = model.GetComponent<KisekaeExpression>();
						if (expression != null)
						{
							expression.Mouth.OffsetX = 50;
							expression.Mouth.OffsetY = 50;
							expression.Mouth.Rotation = 50;
						}
						KisekaeClothing clothing = model.GetComponent<KisekaeClothing>();
						if (clothing != null)
						{
							ConvertShoe84(clothing.LeftShoe);
							ConvertShoe84(clothing.RightShoe);
						}
					}
				}
			}
		}

		private static void ConvertShoe84(KisekaeShoe shoe)
		{
			switch (shoe.Type)
			{
				case 0:
					shoe.Top = 1;
					shoe.TopColor1 = shoe.Color2;
					shoe.Color2 = shoe.Color1;
					break;
				case 1:
					shoe.Top = 2;
					shoe.TopColor1 = shoe.Color2;
					shoe.Color2 = shoe.Color3;
					break;
				case 10:
					shoe.Type = 1;
					shoe.Top = 3;
					shoe.TopColor1 = shoe.Color2;
					shoe.Color2 = shoe.Color3;
					break;
				case 11:
					shoe.Type = 1;
					shoe.Top = 4;
					shoe.TopColor1 = shoe.Color2;
					shoe.Color2 = shoe.Color3;
					break;
				case 15:
					shoe.Top = 6;
					shoe.TopColor1 = shoe.Color2;
					shoe.Color2 = shoe.Color1;
					break;
				case 16:
					shoe.Type = 1;
					shoe.Top = 7;
					shoe.TopColor1 = shoe.Color2;
					shoe.Color2 = shoe.Color3;
					break;
				case 17:
					shoe.Type = 14;
					shoe.Top = 8;
					shoe.TopColor1 = shoe.Color1;
					shoe.TopColor2 = shoe.Color2;
					shoe.Color2 = shoe.Color3;
					break;
				case 18:
					shoe.Type = 14;
					shoe.Top = 9;
					shoe.TopColor1 = shoe.Color1;
					shoe.TopColor2 = shoe.Color2;
					shoe.Color2 = shoe.Color3;
					break;
				case 19:
					shoe.Type = 15;
					shoe.Top = 10;
					shoe.TopColor1 = shoe.Color2;
					shoe.Color2 = shoe.Color3;
					break;
				case 20:
					shoe.Type = 16;
					shoe.Top = 11;
					shoe.TopColor1 = shoe.Color2;
					break;
			}
		}
	}
}
