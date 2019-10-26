using Desktop.DataStructures;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace SPNATI_Character_Editor
{
	public class ExpressionTest : BindableObject
	{
		[XmlAttribute("expr")]
		[JsonProperty("expr")]
		public string Expression
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[DefaultValue("==")]
		[XmlAttribute("cmp")]
		[JsonProperty("cmp")]
		public string Operator
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[DefaultValue("")]
		[XmlAttribute("value")]
		[JsonProperty("value")]
		public string Value
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		public static readonly string[] Operators = new string[] { "==", "<=", "<", ">", ">=", "!=" };

		public ExpressionTest() { }

		public ExpressionTest(string expr, string value)
		{
			Expression = expr;
			Value = value;
		}

		public ExpressionTest(string serializedData)
		{
			string[] parts = serializedData.Split(new char[] { ':' });
			if (parts.Length > 0)
			{
				Expression = parts[0];
			}
			if (parts.Length > 1)
			{
				Value = parts[1];
			}
			if (parts.Length > 2 && !string.IsNullOrEmpty(parts[2]))
			{
				Operator = parts[2];
			}
		}

		public ExpressionTest Copy()
		{
			ExpressionTest copy = new ExpressionTest();
			CopyPropertiesInto(copy);
			return copy;
		}

		public string Serialize()
		{
			List<string> pieces = new List<string>();
			pieces.Add(Expression);
			pieces.Add(Value);
			if (!string.IsNullOrEmpty(Operator) && Operator != "==")
			{
				pieces.Add(Operator);
			}
			return string.Join(":", pieces);
		}

		public override bool Equals(object obj)
		{
			ExpressionTest other = obj as ExpressionTest;
			if (other == null) { return false; }
			return Expression.Equals(other.Expression) && (Value ?? "").Equals(other.Value ?? "") && (Operator ?? "").Equals(other.Operator ?? "");
		}

		public override int GetHashCode()
		{
			int hash = (Expression ?? "").GetHashCode();
			hash = (hash * 397) ^ (Value ?? "").GetHashCode();
			hash = (hash * 397) ^ (Operator ?? "").GetHashCode();
			return hash;
		}

		public override string ToString()
		{
			if (string.IsNullOrEmpty(Value))
			{
				return Expression;
			}
			string op = Operator ?? "==";
			return $"{Expression}{op}{Value}";
		}

		public bool RefersTo(Character character, Character self, string target)
		{
			string targetType = GetTarget();
			bool result;
			if (string.IsNullOrEmpty(targetType))
			{
				result = false;
			}
			else
			{
				string id = CharacterDatabase.GetId(character);
				result = (targetType == id || (targetType == "self" && character == self) || (targetType == "target" && character.FolderName == target));
			}
			return result;
		}

		public string GetTarget()
		{
			string expression = Expression;
			string expr = ((expression != null) ? expression.ToLower() : null) ?? "";
			expr = expr.Trim(new char[]
			{
				'~'
			});
			int period = expr.IndexOf('.');
			string targetType = "";
			if (period >= 0)
			{
				targetType = expr.Substring(0, period);
				if (expr.Length > period + 1)
				{
					expr = expr.Substring(period + 1);
				}
			}
			else
			{
				targetType = expr;
			}
			if (targetType != "self" && targetType != "target")
			{
				Variable v = VariableDatabase.Get("self");
				if (v != null)
				{
					if (v.Functions.Find((VariableFunction vv) => vv.Name == targetType) != null)
					{
						return "self";
					}
				}
			}
			return targetType;
		}

		public void ChangeTarget(string newTarget)
		{
			if (Expression != null)
			{
				string current = GetTarget();
				if (Expression.StartsWith(current + "."))
				{
					int replacePoint = Expression.IndexOf('.');
					Expression = newTarget + Expression.Substring(replacePoint);
				}
				else if (Expression.StartsWith("~self."))
				{
					Expression = "~" + newTarget + Expression.Substring(5);
				}
				else
				{
					Expression = newTarget + "." + Expression;
				}
			}
		}
	}
}
