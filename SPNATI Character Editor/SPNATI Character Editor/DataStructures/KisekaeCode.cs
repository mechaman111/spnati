using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPNATI_Character_Editor
{
	/// <summary>
	/// Data representation of a kisekae import/export code
	/// </summary>
	public class KisekaeCode
	{
		public KisekaeCode()
		{
		}
		public KisekaeCode(KisekaeCode original)
		{
			Version = original.Version;
			foreach (var kvp in original._subCodes)
			{
				List<SubCode> list = new List<SubCode>();
				list.AddRange(kvp.Value);
				_subCodes[kvp.Key] = list;
			}
		}
		public KisekaeCode(KisekaeCode code, StageTemplate stage, params KisekaeCode[] merges) : this(code)
		{
			foreach (var extra in merges)
			{
				MergeIn(extra);
			}
			if (stage != null)
			{
				MergeIn(stage);
			}
		}
		public KisekaeCode(string data) : this(data, false)
		{
		}
		public KisekaeCode(string data, bool resetAll)
		{
			if (resetAll)
			{
				//Fill in empty subcodes to get a blank slate
				const string code = "54**ia_if_ib_id_ic_jc_ie_ja_jb_jd_je_jf_jg_ka_kb_kc_kd_ke_kf_la_lb_oa_os_ob_oc_od_oe_of_lc_m00_n00_s00_og_oh_oo_op_oq_or_om_on_ok_ol_oi_oj_ad0.0.0.0.0.0.0.0.0.0";
				Deserialize(code);
			}
			Deserialize(data);
		}

		private const string DefaultVersion = "47";

		/// <summary>
		/// Kisekae version this code was generated for
		/// </summary>
		public string Version { get; private set; }

		/// <summary>
		/// Sub-codes by prefix
		/// </summary>
		private Dictionary<string, List<SubCode>> _subCodes = new Dictionary<string, List<SubCode>>();

		/// <summary>
		/// Removes all key codes outside the provided ones
		/// </summary>
		/// <param name="keys">Keys to keep</param>
		public void RemoveAllBut(params string[] keys)
		{
			HashSet<string> toKeep = new HashSet<string>();
			foreach (string key in keys)
			{
				toKeep.Add(key);
			}

			List<string> toRemove = new List<string>();
			foreach (string key in _subCodes.Keys)
			{
				if (!toKeep.Contains(key))
					toRemove.Add(key);
			}

			foreach (string key in toRemove)
			{
				_subCodes.Remove(key);
			}
		}

		public void MergeIn(StageTemplate stage)
		{
			MergeIn(stage.Code);
			//Apply blush, anger, and juice
			Add("dc", 0, stage.ExtraJuice);
			Add("gc", 0, stage.ExtraBlush);
			Add("gc", 1, stage.ExtraAnger);
		}

		/// <summary>
		/// Adds a value to the given subcode and node. Only works for single item subcodes
		/// </summary>
		/// <param name="subcode"></param>
		/// <param name="node"></param>
		/// <param name="value"></param>
		private void Add(string subcode, int node, int value)
		{
			List<SubCode> list;
			if (!_subCodes.TryGetValue(subcode, out list))
			{
				list = new List<SubCode>();
				SubCode code = new SubCode();
				code.Set(node, value.ToString());
			}
			else
			{
				SubCode code = list[0];
				value += code.GetInt(node);
				code.Set(node, value.ToString());
			}
		}

		/// <summary>
		/// Merges another code into this one. Any existing subcodes will be replaced
		/// </summary>
		/// <param name="code">Code to merge into this one</param>
		public void MergeIn(KisekaeCode code)
		{
			foreach (var kvp in code._subCodes)
			{
				string key = kvp.Key;
				var list = kvp.Value;
				List<SubCode> newList = new List<SubCode>();
				newList.AddRange(list);
				_subCodes[key] = newList;
			}
		}

		/// <summary>
		/// Converts a code into its string representation that Kisekae can import
		/// </summary>
		/// <returns></returns>
		public string Serialize()
		{
			if (string.IsNullOrEmpty(Version))
				return "";
			StringBuilder sb = new StringBuilder();
			sb.Append(Version);
			sb.Append("**");
			bool first = true;
			foreach (var kvp in _subCodes)
			{
				foreach (var subcode in kvp.Value)
				{
					if (!first)
					{
						sb.Append("_");
					}
					else first = false;
					sb.Append(kvp.Key);
					sb.Append(subcode.Serialize());
				}
			}
			return sb.ToString();
		}

		/// <summary>
		/// Deserializes a code into its parts
		/// </summary>
		/// <param name="data"></param>
		public void Deserialize(string data)
		{
			if (string.IsNullOrEmpty(data))
				return;
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
			string[] subcodes = subdata.Split('_');
			foreach (string sub in subcodes)
			{
				string codeData = "";
				string prefix = sub;

				//find the code prefix
				for (int i = 0; i < sub.Length; i++)
				{
					if (!char.IsLetter(sub[i]) || !char.IsLower(sub[i]))
					{
						prefix = sub.Substring(0, i);
						codeData = sub.Substring(i);
						break;
					}
				}
				SubCode subcode = new SubCode(codeData);
				List<SubCode> list;
				if (!_subCodes.TryGetValue(prefix, out list))
				{
					list = new List<SubCode>();
					_subCodes[prefix] = list;
				}
				list.Add(subcode);
			}
		}

		/// <summary>
		/// .-delimited pieces
		/// </summary>
		private class SubCode
		{
			private List<string> _pieces = new List<string>();

			public SubCode()
			{
			}

			public SubCode(string data)
			{
				Deserialize(data);
			}

			public override string ToString()
			{
				return Serialize();
			}

			/// <summary>
			/// Gets an int represenation of a piece
			/// </summary>
			/// <param name="index"></param>
			/// <returns></returns>
			public int GetInt(int index)
			{
				if (index < 0 || index >= _pieces.Count)
					return 0;
				int value;
				int.TryParse(_pieces[index], out value);
				return value;
			}

			/// <summary>
			/// Gets a string representation of a piece
			/// </summary>
			/// <param name="index"></param>
			/// <returns></returns>
			public string GetString(int index)
			{
				if (index < 0 || index >= _pieces.Count)
					return "0";
				return _pieces[index];
			}

			public void Set(int index, string value)
			{
				while (_pieces.Count <= index)
				{
					_pieces.Add("0");
				}
				_pieces[index] = value;
			}

			/// <summary>
			/// Converts this subcode into a string representation
			/// </summary>
			/// <returns></returns>
			public string Serialize()
			{
				return string.Join(".", _pieces);
			}

			/// <summary>
			/// Populates the subcode with data from a string represenation
			/// </summary>
			public void Deserialize(string data)
			{
				_pieces = data.Split('.').ToList();
			}
		}
	}
}
