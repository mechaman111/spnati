using System;
using System.Collections.Generic;
using System.Text;

namespace KisekaeImporter
{
	/// <summary>
	/// Data representation of a kisekae character
	/// </summary>
	public class KisekaeCode
	{
		private const string DefaultVersion = "47";

		/// <summary>
		/// Kisekae version this code was generated for
		/// </summary>
		public string Version { get; private set; }

		/// <summary>
		/// Sub-code components containing one or more groups of subcodes
		/// </summary>
		private Dictionary<Type, KisekaeComponent> _components = new Dictionary<Type, KisekaeComponent>();

		public KisekaeCode()
		{
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
				Reset();
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
				Reset();
			}
			Deserialize(data);
		}

		private static string DefaultCode;
		static KisekaeCode()
		{
			DefaultCode = "54**ia_if_ib_id_ic_jc_ie_ja_jb_jd_je_jf_jg_ka_kb_kc_kd_ke_kf_la_lb_oa_os_ob_oc_od_oe_of_lc_og_oh_oo_op_oq_or_om_on_ok_ol_oi_oj_r000_s000_m000_n000_t000";
		}

		public void Reset()
		{
			//Fill in empty subcodes to get a blank slate
			Deserialize(DefaultCode);
		}
		
		public void ReplaceComponent(KisekaeComponent component)
		{
			if (component == null)
				return;
			_components[component.GetType()] = component;
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
				return "";
			StringBuilder sb = new StringBuilder();
			sb.Append(Version);
			sb.Append("**");
			bool first = true;

			foreach (var component in _components.Values)
			{
				if (!first)
				{
					sb.Append("_");
				}
				string data = component.Serialize();
				if (!string.IsNullOrEmpty(data))
				{
					first = false;
				}
				sb.Append(data);
			}

			return sb.ToString();
		}

		public T GetComponent<T>() where T : KisekaeComponent
		{
			return GetComponent(typeof(T)) as T;
		}
		public KisekaeComponent GetComponent(Type componentType)
		{
			KisekaeComponent component;
			_components.TryGetValue(componentType, out component);
			return component;
		}

		public T GetOrAddComponent<T>() where T : KisekaeComponent
		{
			return GetOrAddComponent(typeof(T)) as T;
		}

		/// <summary>
		/// Gets a component, or creates one if it doesn't exist yet
		/// </summary>
		/// <param name="componentType"></param>
		/// <returns></returns>
		public KisekaeComponent GetOrAddComponent(Type componentType)
		{
			KisekaeComponent component;
			if (!_components.TryGetValue(componentType, out component))
			{
				component = Activator.CreateInstance(componentType) as KisekaeComponent;
				if (component == null)
					return null;
				_components[componentType] = component;
			}
			return component;
		}

		/// <summary>
		/// Removes a component completely
		/// </summary>
		/// <typeparam name="T"></typeparam>
		public void RemoveComponent<T>()
		{
			_components.Remove(typeof(T));
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
				string prefix = sub.Substring(0, 1);
				string id = "";
				if (sub.Length > 1)
				{
					//Current assumption is that a subcode ID is either two alphas or one alpha+2 digits
					if (char.IsDigit(sub[1]))
					{
						id = sub.Substring(0, 1);
						prefix = sub.Substring(0, 3);
						codeData = sub.Substring(3);
					}
					else
					{
						prefix = sub.Substring(0, 2);
						id = prefix;
						codeData = sub.Substring(2);
					}
				}
				else continue;

				Type componentType = KisekaeSubCodeMap.GetComponentType(id);
				if (componentType != null)
				{
					string[] subcodePieces = codeData.Split('.');
					KisekaeComponent component = GetOrAddComponent(componentType);
					component.ApplySubCode(prefix, subcodePieces);
				}
			}
		}
	}
}
