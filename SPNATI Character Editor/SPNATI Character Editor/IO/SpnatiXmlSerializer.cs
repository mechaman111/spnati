using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace SPNATI_Character_Editor.IO
{
	/// <summary>
	/// Basic XML serializer designed specifically for SPNATI files. Output can be deserialized back into C# objects using the .NET XmlSerializer
	/// </summary>
	public class SpnatiXmlSerializer
	{
		private const string IndentString = "    ";

		public void Serialize<T>(string filename, T data)
		{
			IHookSerialization hook = data as IHookSerialization;
			if (hook != null)
			{
				hook.OnBeforeSerialize();
			}

			XmlRootAttribute root = data.GetType().GetCustomAttribute<XmlRootAttribute>(true);
			if (root == null)
			{
				throw new ArgumentException(string.Format("The type {0} cannot be serialized because it is missing an XmlRoot attribute.", data.GetType()));
			}

			var sb = new StringBuilder();
			var settings = new XmlWriterSettings()
			{
				OmitXmlDeclaration = true,
				Indent = true,
				IndentChars = IndentString
			};
			XmlWriter writer = XmlWriter.Create(sb, settings);
			sb.AppendLine("<?xml version='1.0' encoding='UTF-8'?>");

			WriteElement(data, root.ElementName, writer, sb);

			writer.Flush();
			sb.AppendLine();
			string text = sb.ToString();
			text = text.Replace("\r\n>", ">\r\n");
			text = XMLHelper.Encode(text);

			File.WriteAllText(filename, text);
			writer.Dispose();
		}

		private void WriteElement(object data, string name, XmlWriter writer, StringBuilder builder)
		{
			if (data == null)
				return;
			XmlHeaderAttribute header = data.GetType().GetCustomAttribute<XmlHeaderAttribute>();
			if (header != null)
			{
				writer.Flush();
				builder.AppendLine();
				writer.WriteComment(header.Text);
			}

			List<Tuple<FieldInfo, string, string>> subElements = new List<Tuple<FieldInfo, string, string>>();

			bool foundSomething = false;
			writer.WriteStartElement(name);

			if (data.GetType() == typeof(string) && string.IsNullOrEmpty(data.ToString()))
			{
				//Stop empty strings immediately
				writer.WriteEndElement();
				return;
			}

			var fields = data.GetType().GetFields();
			string text = null;

			foreach (var field in fields)
			{
				if (field.GetCustomAttribute<XmlIgnoreAttribute>() != null)
					continue;

				XmlElementAttribute element = field.GetCustomAttribute<XmlElementAttribute>();
				if (element != null)
				{
					//Save elements for later
					subElements.Add(new Tuple<FieldInfo, string, string>(field, element.ElementName, null));
					foundSomething = true;
					continue;
				}

				XmlArrayAttribute arrayAttr = field.GetCustomAttribute<XmlArrayAttribute>();
				if (arrayAttr != null)
				{
					XmlArrayItemAttribute arrayItemAttr = field.GetCustomAttribute<XmlArrayItemAttribute>();
					if (arrayItemAttr != null)
					{
						foundSomething = true;
						subElements.Add(new Tuple<FieldInfo, string, string>(field, arrayAttr.ElementName, arrayItemAttr.ElementName));
					}
					continue;
				}

				XmlTextAttribute textAttr = field.GetCustomAttribute<XmlTextAttribute>();
				if (textAttr != null)
				{
					text = field.GetValue(data)?.ToString();
					foundSomething = true;
					continue;
				}

				XmlAttributeAttribute attribute = field.GetCustomAttribute<XmlAttributeAttribute>();
				if (attribute != null)
				{
					string value = field.GetValue(data)?.ToString();
					foundSomething = true;
					if (value == null)
						continue;
					writer.WriteAttributeString(attribute.AttributeName, value);
					continue;
				}
			}

			// now do elements and arrays
			foreach (var tuple in subElements)
			{
				FieldInfo field = tuple.Item1;

				//Element
				XmlNewLineAttribute newLineAttr = field.GetCustomAttribute<XmlNewLineAttribute>();
				XmlNewLinePosition newLine = XmlNewLinePosition.None;
				if (newLineAttr != null)
				{
					newLine = newLineAttr.Position;
				}
				if (newLine == XmlNewLinePosition.Before || newLine == XmlNewLinePosition.Both)
				{
					writer.Flush();
					builder.AppendLine();
				}

				if (tuple.Item3 == null)
				{
					object subdata = field.GetValue(data);
					IList list = subdata as IList;
					if (list != null)
					{
						XmlSortMethodAttribute sortAttribute = field.GetCustomAttribute<XmlSortMethodAttribute>();
						if (sortAttribute != null)
						{
							MethodInfo sortMethod = data.GetType().GetMethod(sortAttribute.Method, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
							if (sortMethod != null)
							{
								List<object> sortedList = new List<object>();
								foreach (object obj in list)
								{
									sortedList.Add(obj);
								}
								sortedList.Sort((o1, o2) => 
								{
									return (int)sortMethod.Invoke(data, new object[] { o1, o2 }); }
								);
								list = sortedList;
							}
						}

						foreach (object item in list)
						{
							WriteElement(item, tuple.Item2, writer, builder);

							if (newLine == XmlNewLinePosition.After || newLine == XmlNewLinePosition.Both)
							{
								writer.Flush();
								builder.AppendLine();
							}
						}
					}
					else
					{
						WriteElement(subdata, tuple.Item2, writer, builder);
					}
				}
				else
				{
					//Array
					writer.WriteStartElement(tuple.Item2);
					IList array = field.GetValue(data) as IList;
					if (array != null)
					{
						foreach (var obj in array)
						{
							WriteElement(obj, tuple.Item3, writer, builder);
						}
					}

					if (newLine == XmlNewLinePosition.After || newLine == XmlNewLinePosition.Both)
					{
						writer.Flush();
						builder.AppendLine();
					}

					writer.WriteEndElement();
				}
			}

			if (!foundSomething)
			{
				text = data.ToString();
				if (data.GetType() == typeof(bool))
					text = text.ToLower();
			}
			if (text != null)
			{
				writer.WriteString(text);
			}			

			writer.WriteEndElement();
		}
	}

	[AttributeUsage(AttributeTargets.Field)]
	public class XmlNewLineAttribute : Attribute
	{
		public XmlNewLinePosition Position { get; set; }

		public XmlNewLineAttribute(XmlNewLinePosition where)
		{
			Position = where;
		}

		public XmlNewLineAttribute()
		{
			Position = XmlNewLinePosition.Before;
		}
	}

	public enum XmlNewLinePosition
	{
		Before,
		After,
		Both,
		None
	}

	public class XmlHeaderAttribute : Attribute
	{
		public string Text { get; set; }

		public XmlHeaderAttribute(string text)
		{
			Text = text;
		}
	}

	public class XmlSortMethodAttribute : Attribute
	{
		public string Method { get; set; }

		public XmlSortMethodAttribute(string method)
		{
			Method = method;
		}
	}
}
