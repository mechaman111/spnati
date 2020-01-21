using Desktop.Skinning;
using SPNATI_Character_Editor.Providers;
using System;
using System.Drawing;
using System.Xml.Serialization;

namespace SPNATI_Character_Editor
{
	public class CaseLabel
	{
		[XmlAttribute("id")]
		public int Id;

		[XmlAttribute("label")]
		public string Text;

		[XmlAttribute("color")]
		public string ColorCode;

		[XmlAttribute("folder")]
		public string Folder;

		[XmlAttribute("sort")]
		public int SortId;

		public override string ToString()
		{
			return Text;
		}
	}

	public class ColorCode : Definition
	{
		public int Code;
		private Func<Skin, Color> _mapper;

		public ColorCode()
		{
		}

		public ColorCode(string name, int code, Func<Skin, Color> colorMapper)
		{
			Key = code.ToString();
			Name = name;
			Code = code;
			_mapper = colorMapper;
		}

		public override string ToString()
		{
			return Name;
		}

		public Color GetColor()
		{
			if (_mapper == null)
			{
				return SkinManager.Instance.CurrentSkin.Surface.ForeColor;
			}
			return _mapper(SkinManager.Instance.CurrentSkin);
		}
	}

	public class ColorCodeProvider : DefinitionProvider<ColorCode>
	{
		public override void ApplyDefaults(ColorCode definition)
		{
		}

		public override string GetLookupCaption()
		{
			return "Choose a Color";
		}
	}
}
