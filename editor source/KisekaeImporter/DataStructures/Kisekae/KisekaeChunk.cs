using System;
using System.Collections.Generic;
using System.Text;

namespace KisekaeImporter.DataStructures.Kisekae
{
	/// <summary>
	/// A chunk is made-up terminology for something that contains components, which are composed of codes like a00.3.5.2_b2.3.5, etc.
	/// </summary>
	public class KisekaeChunk
	{
		public List<string> Assets = new List<string>();

		private Dictionary<Type, KisekaeComponent> _components = new Dictionary<Type, KisekaeComponent>();

		public KisekaeChunk(string data)
		{
			Deserialize(data);
		}
		public KisekaeChunk(KisekaeChunk original)
		{
			string code = original.Serialize();
			Deserialize(code);
		}

		public void Deserialize(string data)
		{
			string[] assets = data.Split(new string[] { "/#]" }, StringSplitOptions.None);

			//codes
			string[] subcodes = assets[0].Split('_');
			foreach (string sub in subcodes)
			{
				if (string.IsNullOrEmpty(sub))
					continue;
				string codeData = "";
				string prefix = sub.Substring(0, 1);
				string id = "";
				if (sub.Length > 1)
				{
					//Current assumption is that a subcode ID is either two alphas or one alpha+2 digits
					if (char.IsDigit(sub[1]))
					{
						id = sub.Substring(0, 1);
						if (sub.Length >= 3)
						{
							prefix = sub.Substring(0, 3);
							codeData = sub.Substring(3);
						}
						else
						{
							prefix = sub.Substring(0, 2);
							codeData = sub.Substring(2);
						}
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

			//assets
			Assets.Clear();
			for (int i = 1; i < assets.Length; i++)
			{
				Assets.Add(assets[i]);
			}
		}

		public string Serialize()
		{
			StringBuilder sb = new StringBuilder();
			List<string> components = new List<string>();
			foreach (var component in _components.Values)
			{
				components.Add(component.Serialize());
			}
			sb.Append(string.Join("_", components));

			if (Assets.Count > 0)
			{
				for (int i = 0; i < Assets.Count; i++)
				{
					sb.Append("/#]");
					sb.Append(Assets[i]);
				}
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


		public void ReplaceComponent(KisekaeComponent component)
		{
			if (component == null)
				return;
			_components[component.GetType()] = component;
		}

		/// <summary>
		/// Merges another chunk into this one
		/// </summary>
		/// <param name="chunk"></param>
		public void MergeIn(KisekaeChunk chunk, bool applyEmpties, bool poseOnly)
		{
			//TODO: Asset merging is very flawed since there's no way to know what image/speech bubble the asset belongs to
			for (int i = 0; i < chunk.Assets.Count; i++)
			{
				if (Assets.Count > i)
				{
					Assets[i] = chunk.Assets[i];
				}
				else
				{
					Assets.Add(chunk.Assets[i]);
				}
			}

			KisekaeChunk copy = new KisekaeChunk(chunk);
			foreach (var kvp in copy._components)
			{
				KisekaeComponent existing = GetOrAddComponent(kvp.Key);
				KisekaeComponent component = kvp.Value;

				foreach (var subcode in component.GetSubCodes())
				{
					existing.ReplaceSubCode(subcode, applyEmpties, poseOnly);
				}
			}
		}

		/// <summary>
		/// Gets all existing subcodes of the given type across all models
		/// </summary>
		/// <returns></returns>
		public IEnumerable<KisekaeSubCode> GetSubCodesOfType<T>()
		{
			foreach (KisekaeComponent component in _components.Values)
			{
				foreach (KisekaeSubCode code in component.GetSubCodesOfType<T>())
				{
					yield return code;
				}
			}
		}

		public void ReplaceAssetPaths(string newRelativePath)
		{
			for (int i = 0; i < Assets.Count; i++)
			{
				string path = Assets[i];
				string filename = System.IO.Path.GetFileName(path);
				bool missingExtension = (filename == System.IO.Path.GetFileNameWithoutExtension(filename));
				string fullpath = System.IO.Path.GetFullPath(System.IO.Path.Combine(newRelativePath, filename));
				fullpath = fullpath.Replace("\\", "/");
				fullpath = fullpath.Replace(" ", "%20");
				Assets[i] = System.IO.Path.Combine(newRelativePath, filename);
			}
		}

		/// <summary>
		/// Shifts all movable pieces by the given offset
		/// </summary>
		/// <param name="offset"></param>
		public void ShiftX(int offset)
		{
			foreach (KisekaeComponent component in _components.Values)
			{
				component.ShiftX(offset);
			}
		}
	}
}
