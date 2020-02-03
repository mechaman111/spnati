using Desktop;
using Desktop.Providers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace SPNATI_Character_Editor.DataStructures
{
	[XmlRoot("clothing")]
	public class ClothingDefinitions
	{
		private static ClothingDefinitions _instance;
		public static ClothingDefinitions Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = LoadFunction();
				}
				return _instance;
			}
		}

		public static Func<ClothingDefinitions> LoadFunction = LoadFromFile;

		private static ClothingDefinitions LoadFromFile()
		{
			string filepath = Path.Combine(Config.SpnatiDirectory, "opponents", "clothing.xml");
			return Serialization.Import<ClothingDefinitions>(filepath);
		}

		[XmlArray("categories")]
		[XmlArrayItem("category")]
		public List<ClothingCategoryItem> Categories = new List<ClothingCategoryItem>();
	}

	public class ClothingCategoryItem
	{
		[XmlAttribute("key")]
		public string Key;

		[XmlText]
		public string Name;
	}

	public class ClothingCategory : Category
	{
		public ClothingCategory(string key, string value) : base(key, value)
		{
		}
	}

	public class ClothingCategoryProvider : CategoryProvider<ClothingCategory>
	{
		public override string GetLookupCaption()
		{
			return "Choose a category";
		}

		protected override ClothingCategory[] GetCategoryValues()
		{
			ClothingDefinitions defs = ClothingDefinitions.Instance;
			return defs.Categories.Select(item => new ClothingCategory(item.Key, item.Name)).ToArray();
		}

		public override bool AllowsNew
		{
			get { return true; }
		}

		public override IRecord Create(string key)
		{
			ClothingCategory category = new ClothingCategory(key, key);
			Add(category);
			return category;
		}
	}
}
