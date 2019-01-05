using System.Collections.Generic;
using System.IO;

namespace SPNATI_Character_Editor
{
	/// <summary>
	/// Tweaks SPNATI text which is often invalid so that the XML serializer can read it
	/// </summary>
	public static class XMLHelper
	{
		private static Dictionary<string, string> _encodeMap = new Dictionary<string, string>();
		private static Dictionary<string, string> _decodeMap = new Dictionary<string, string>();

		static XMLHelper()
		{
			Load();
		}

		private static void Load()
		{
			string file = "xml_encoding.txt";
			string[] lines = File.ReadAllLines(file);
			foreach (string kvp in lines)
			{
				string[] pieces = kvp.Split(',');
				if (pieces.Length != 2)
					continue;
				_encodeMap[pieces[0]] = pieces[1];
				_decodeMap[pieces[1]] = pieces[0];
			}
		}

		public static string Encode(string text)
		{
			foreach (var kvp in _encodeMap)
			{
				text = text.Replace(kvp.Key, kvp.Value);
			}
			text = DecodeEntityReferences(text);
			return text;
		}

		public static string Decode(string text)
		{
			foreach (var kvp in _decodeMap)
			{
				text = text.Replace(kvp.Key, kvp.Value);
			}
			return text;
		}

		/// <summary>
		/// Encodes &lt;i>, &lt;/i>, and &amp;
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static string EncodeEntityReferences(string text)
		{
			//text = text.Replace("&", "&amp;");
			text = text.Replace("<i>", "&lt;i&gt;");
			text = text.Replace("<I>", "&lt;i&gt;");
			text = text.Replace("</i>", "&lt;/i&gt;");
			text = text.Replace("</I>", "&lt;/i&gt;");
			return text;
		}

		/// <summary>
		/// Decodes <, >, and &
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static string DecodeEntityReferences(string text)
		{
			//text = text.Replace("&lt;i&gt;", "<i>");
			//text = text.Replace("&lt;/i&gt;", "</i>");
			//text = text.Replace("&amp;", "&");
			return text;
		}

		/// <summary>
		/// Replaces the instance of 
		/// </summary>
		/// <param name="str">String to do replacement on</param>
		/// <param name="index">Starting index</param>
		/// <param name="oldValue">String to replace</param>
		/// <param name="newValue">Replacement string</param>
		/// <returns>String with replacement</returns>
		public static string ReplaceAt(this string str, int index, string oldValue, string newValue)
		{
			string left = str.Substring(0, index);
			int count = oldValue.Length;
			string right = str.Substring(index + count);
			return left + newValue + right;
		}
	}
}
