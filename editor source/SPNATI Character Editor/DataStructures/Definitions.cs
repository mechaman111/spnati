using Desktop;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace SPNATI_Character_Editor
{
	public class Definitions
	{
		private static Definitions _instance;
		public static Definitions Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new Definitions();
				}
				return _instance;
			}
			set { _instance = value; }
		}

		public void Destroy()
		{
			_instance = null;
		}

		private DualKeyDictionary<Type, string, IRecord> _definitions = new DualKeyDictionary<Type, string, IRecord>();

		#region General definitions
		public void Clear()
		{
			_definitions.Clear();
		}

		public void Add<T>(T definition) where T : IRecord, new()
		{
			_definitions.Set(typeof(T), definition.Key, definition);
		}

		/// <summary>
		/// Adds a definition to the correct subclass dictionary
		/// </summary>
		/// <param name="definition"></param>
		public void Add(IRecord definition)
		{
			_definitions.Set(definition.GetType(), definition.Key, definition);
		}

		public void Remove<T>(T definition) where T : IRecord
		{
			_definitions.Remove(typeof(T), definition.Key);
		}
		public void Remove(Type type, IRecord definition)
		{
			_definitions.Remove(type, definition.Key);
		}

		/// <summary>
		/// Enumerates through all definitions of a certain type
		/// </summary>
		/// <typeparam name="T">Asset definition type</typeparam>
		/// <returns></returns>
		public IEnumerable<T> Get<T>() where T : IRecord
		{
			Dictionary<string, IRecord> assets = _definitions[typeof(T)];
			if (assets != null)
			{
				foreach (IRecord asset in assets.Values)
				{
					yield return (T)asset;
				}
			}
		}

		public IEnumerable<IRecord> Get(Type type)
		{
			Dictionary<string, IRecord> assets = _definitions[type];
			if (assets != null)
			{
				foreach (IRecord asset in assets.Values)
				{
					yield return asset;
				}
			}
		}

		/// <summary>
		/// Gets a definition with the given key
		/// </summary>
		/// <typeparam name="T">Definition type</typeparam>
		/// <param name="key">Key identifier</param>
		/// <returns></returns>
		public T Get<T>(string key) where T : IRecord
		{
			if (key == null)
			{
				return default(T);
			}

			IRecord asset = _definitions.Get(typeof(T), key);
			return (T)asset;
		}
		#endregion
	}
}