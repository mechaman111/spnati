using System;
using System.Collections.Generic;

namespace SPNATI_Character_Editor.Analyzers
{
	/// <summary>
	/// Collection of criteria for reporting in the Data Analyzer
	/// </summary>
	public class AnalyzerReportCriteria : IDataCriterion
	{
		public List<IDataCriterion> Criteria = new List<IDataCriterion>();

		/// <summary>
		/// Expression for linking criteria (ex. AND)
		/// </summary>
		public string Expression;

		private AnalyzerReportCriteria _builtTree = null;

		public override string ToString()
		{
			return Expression;
		}

		/// <summary>
		/// Gets whether a character meets all the criteria for this report
		/// </summary>
		/// <param name="character"></param>
		/// <returns></returns>
		public bool MeetsCriteria(Character character)
		{
			string expression = Expression;
			if (string.IsNullOrEmpty(expression))
			{
				expression = "AND";
			}
			expression = expression.ToUpperInvariant();

			if (expression == "AND")
			{
				foreach (IDataCriterion criterion in Criteria)
				{
					if (!criterion.IsMet(character))
					{
						return false;
					}
				}
				return true;
			}
			else if (expression == "OR")
			{
				foreach (IDataCriterion criterion in Criteria)
				{
					if (criterion.IsMet(character))
					{
						return true;
					}
				}
				return false;
			}
			else if (expression == "NOT")
			{
				//should only have one criterion
				foreach (IDataCriterion criterion in Criteria)
				{
					return !criterion.IsMet(character);
				}
			}
			else
			{
				if (_builtTree == null)
				{
					//break down the expression into an expression tree
					expression = expression.Replace("NOT", " NOT ");
					expression = expression.Replace("AND", " AND ");
					expression = expression.Replace("OR", " OR ");
					expression = expression.Replace("&&", " AND ");
					expression = expression.Replace("&", " AND ");
					expression = expression.Replace("||", " OR ");
					expression = expression.Replace("|", " OR ");
					expression = expression.Replace("^", " NOT ");
					expression = expression.Replace("~", " NOT ");
					expression = expression.Replace("!", " NOT ");
					expression = expression.Replace("(", " ( ");
					expression = expression.Replace(")", " ) ");

					string[] pieces = expression.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
					AnalyzerReportCriteria currentLevel = new AnalyzerReportCriteria();
					Stack<AnalyzerReportCriteria> stack = new Stack<AnalyzerReportCriteria>();
					foreach (string piece in pieces)
					{
						string item = piece;
						int criteriaNumber;
						if (int.TryParse(item, out criteriaNumber))
						{
							if (criteriaNumber < 1 || criteriaNumber > Criteria.Count)
							{
								throw new Exception($"Unrecognized criteria: {criteriaNumber}");
							}
							IDataCriterion criterion = Criteria[criteriaNumber - 1];
							currentLevel.Criteria.Add(criterion);
						}
						else if (item == "AND")
						{
							if (currentLevel.Expression == null || currentLevel.Expression == item)
							{
								currentLevel.Expression = "AND";
							}
							else if (currentLevel.Expression == "OR")
							{
								//form X OR Y AND Z, so need to group into X OR (Y AND Z)
								IDataCriterion last = currentLevel.Criteria[currentLevel.Criteria.Count - 1];
								currentLevel.Criteria.RemoveAt(currentLevel.Criteria.Count - 1);
								stack.Push(currentLevel);
								AnalyzerReportCriteria next = new AnalyzerReportCriteria();
								currentLevel.Criteria.Add(next);
								currentLevel = next;
								currentLevel.Expression = "AND";
								currentLevel.Criteria.Add(last);
							}
							else if (currentLevel.Expression == "NOT")
							{
								currentLevel = stack.Pop();
							}
						}
						else if (item == "OR")
						{
							if (currentLevel.Expression == null || currentLevel.Expression == item)
							{
								currentLevel.Expression = "OR";
							}
							else if (currentLevel.Expression == "AND")
							{
								//form X AND Y OR Z, so convert to ((X AND Y) OR Z), setting the outer group to the new current
								AnalyzerReportCriteria copy = new AnalyzerReportCriteria();
								copy.Expression = "AND";
								copy.Criteria.AddRange(currentLevel.Criteria);
								currentLevel.Expression = "OR";
								currentLevel.Criteria = new List<IDataCriterion>();
								currentLevel.Criteria.Add(copy);
							}
							else if (currentLevel.Expression == "NOT")
							{
								currentLevel = stack.Pop();
							}
						}
						else if (item == "NOT")
						{
							stack.Push(currentLevel);
							currentLevel = new AnalyzerReportCriteria();
							stack.Peek().Criteria.Add(currentLevel);
							currentLevel.Expression = "NOT";
						}
						else if (item == ")")
						{
							if (stack.Count == 0)
							{
								throw new Exception("Mismatched parentheses in expression");
							}
							currentLevel = stack.Pop();
						}
						else if (item == "(")
						{
							AnalyzerReportCriteria next = new AnalyzerReportCriteria();
							currentLevel.Criteria.Add(next);
							stack.Push(currentLevel);
							currentLevel = next;
						}
						else
						{
							throw new Exception("Unknown value in expression: " + item);
						}
					}
					while (stack.Count > 0)
					{
						currentLevel = stack.Pop();
					}
					_builtTree = currentLevel;
				}
				return _builtTree.IsMet(character);
			}

			return false;
		}

		public bool IsMet(Character character)
		{
			return MeetsCriteria(character);
		}
	}
}
