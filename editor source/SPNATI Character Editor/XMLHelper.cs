﻿using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace SPNATI_Character_Editor
{
	/// <summary>
	/// Tweaks SPNATI text which is often invalid so that the XML serializer can read it
	/// </summary>
	public static class XMLHelper
	{
		private static Dictionary<string, string> _encodeMap = new Dictionary<string, string>();
		private static Dictionary<string, string> _decodeMap = new Dictionary<string, string>();
		private static Dictionary<string, Regex> _htmlRegex = new Dictionary<string, Regex>();
		private static MatchEvaluator _htmlEncoder;
		private static MatchEvaluator _htmlDecoder;

		static XMLHelper()
		{
			_htmlRegex["script"] = new Regex(@"<script\b[^>]*>[\s\S]*?<\/script>", RegexOptions.IgnoreCase);
			_htmlRegex["div"] = new Regex(@"<div\b[^>]*>[\s\S]*?<\/div>", RegexOptions.IgnoreCase);
			_htmlRegex["span"] = new Regex(@"<span\b[^>]*>[\s\S]*?<\/span>", RegexOptions.IgnoreCase);
			_htmlEncoder = delegate (Match match)
			{
				return match.ToString().Replace("<", "&lt;");
			};
			_htmlDecoder = delegate (Match match)
			{
				return match.ToString().Replace("&lt;", "<");
			};
			Load();
		}

		private static void Load()
		{
			string file = Path.Combine(Config.ExecutableDirectory, "xml_encoding.txt");
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
			text = text.Replace("<br>", "&lt;br&gt;");
			text = text.Replace("<BR>", "&lt;BR&gt;");
			text = text.Replace("</br>", "&lt;/br&gt;");
			text = text.Replace("</BR>", "&lt;/BR&gt;");
			text = text.Replace("<hr>", "&lt;hr&gt;");
			text = text.Replace("<HR>", "&lt;HR&gt;");
			text = text.Replace("</hr>", "&lt;/hr&gt;");
			text = text.Replace("</HR>", "&lt;/HR&gt;");

			//special processing for script/html tags
			//foreach (KeyValuePair<string, Regex> kvp in _htmlRegex)
			//{
			//	string tag = $"<{kvp.Key}>";
			//	if (text.Contains(tag))
			//	{
			//		text = kvp.Value.Replace(text, _htmlEncoder);
			//	}
			//}
			return text;
		}

		/// <summary>
		/// Decodes <, >, and &
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static string DecodeEntityReferences(string text)
		{
			//these are done by the game itself now
			//text = text.Replace("&lt;i&gt;", "<i>");
			//text = text.Replace("&lt;/i&gt;", "</i>");
			//text = text.Replace("&amp;", "&");

			//but these aren't
			//foreach (KeyValuePair<string, Regex> kvp in _htmlRegex)
			//{
			//	string tag = $"&lt;{kvp.Key}";
			//	if (text.Contains(tag))
			//	{
			//		text = text.Replace(tag, $"<{kvp.Key}").Replace($"&lt;/{kvp.Key}", $"</{kvp.Key}");
			//		text = kvp.Value.Replace(text, _htmlDecoder);
			//	}
			//}
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
