using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SPNATI_Character_Editor
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class ImportEdit
	{
		[JsonProperty("type")]
		public string Mode;

		[JsonProperty("intent")]
		public string Intent;

		[JsonProperty("stage")]
		public int Stage;

		[JsonProperty("case")]
		public Case SourceCase;

		[JsonProperty("oldState")]
		public ImportState OriginalState;

		[JsonProperty("state")]
		public ImportState State;

		[JsonProperty("suggestedTag")]
		public string SuggestedTag;

		[JsonProperty("responseTarget")]
		public ImportTarget ResponseTarget;

		public ImportEdit() { }

		/// <summary>
		/// Creates a case for the given character using the information in this edit
		/// </summary>
		/// <param name="character">Character for which to make the case</param>
		/// <returns></returns>
		public Case CreateCase(Character character)
		{
			Case result = null;
			switch (Mode)
			{
				case "edit":
					if (OriginalState != null)
					{
						OriginalState.Text = OriginalState.Text.Replace("&lt;", "<");
						OriginalState.Text = OriginalState.Text.Replace("&gt;", ">");
					}
					Case workingCase = FindMatchingWorkingCase(character);
					if (workingCase != null)
					{
						DialogueLine line = workingCase.Lines.Find(l => l.Text == OriginalState.Text);
						if (line != null)
						{
							line.Text = State.Text;
							if (!string.IsNullOrEmpty(State.Marker?.ToString()))
							{
								line.Marker = State.Marker.ToString();
							}
							line.Image = State.Image;
							line.Pose = character.PoseLibrary.GetPose(line.Image);
							result = workingCase;
						}
						else
						{
							throw new ImportLinesException($"No line matching '{OriginalState.Text}' found.");
						}
					}
					else
					{
						throw new ImportLinesException("Couldn't find an existing case to edit.");
					}
					break;
				case "new":
					if (Intent == "response")
					{
						Character speaker = CharacterDatabase.Get(ResponseTarget.Id);
						if (speaker == null)
						{
							throw new ImportLinesException($"Unrecognized response target: {ResponseTarget.Id}");
						}
						Case response = ResponseTarget.Case.CreateResponse(speaker, character);
						if (response == null)
						{
							throw new ImportLinesException($"Unable to create a response for target: {ResponseTarget.Id}");
						}
						DialogueLine caseLine = new DialogueLine(State.Image, State.Text);
						caseLine.Pose = character.PoseLibrary.GetPose(caseLine.Image);
						response.Lines.Add(caseLine);
						result = response;
					}
					else if (Intent == "generic")
					{
						workingCase = new Case(SuggestedTag);
						workingCase.Stages.Add(Stage);
						DialogueLine caseLine = new DialogueLine(State.Image, State.Text);
						caseLine.Pose = character.PoseLibrary.GetPose(caseLine.Image);
						workingCase.Lines.Add(caseLine);
						result = workingCase;
					}
					else
					{
						throw new ImportLinesException($"Unknown intent: {Intent}");
					}
					character.Behavior.AddWorkingCase(result);
					break;
				default:
					throw new ImportLinesException($"Unrecognized type: {Mode}");
			}
			return result;
		}

		/// <summary>
		/// Finds a working case that matches the conditions of this edit
		/// </summary>
		/// <param name="character"></param>
		/// <returns></returns>
		private Case FindMatchingWorkingCase(Character character)
		{
			foreach (Case workingCase in character.Behavior.GetWorkingCases())
			{
				if (workingCase.MatchesConditions(SourceCase) && workingCase.MatchesStages(SourceCase, false))
				{
					return workingCase;
				}
			}
			return null;
		}
	}

	public class ImportStage
	{
		[JsonProperty("min")]
		public int Min;

		[JsonProperty("max")]
		public int Max;
	}

	public class ImportState
	{
		[JsonProperty("text")]
		public string Text;

		[JsonProperty("image")]
		public string Image;

		[JsonProperty("marker")]
		public ImportMarker Marker;
	}

	public class ImportTarget
	{
		[JsonProperty("id")]
		public string Id;

		[JsonProperty("stage")]
		public int Stage;

		[JsonProperty("case")]
		public Case Case;

		[JsonProperty("state")]
		public ImportState State;
	}

	public class ImportMarker
	{
		[JsonProperty("name")]
		public string Name;

		[JsonProperty("perTarget")]
		public string PerTarget;

		[JsonProperty("Value")]
		public string Value;
	}

	public class ImportLinesException : Exception
	{
		public ImportLinesException(string msg) : base(msg)
		{
		}
	}
}
