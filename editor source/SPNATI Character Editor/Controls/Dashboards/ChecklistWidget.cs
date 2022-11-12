using Desktop;
using Desktop.Skinning;
using SPNATI_Character_Editor.Activities;
using SPNATI_Character_Editor.DataStructures;
using SPNATI_Character_Editor.Forms;
using SPNATI_Character_Editor.Services;
using SPNATI_Character_Editor.Workspaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls.Dashboards
{
	public partial class ChecklistWidget : UserControl, IDashboardWidget
	{
		private Character _character;
		private Pen _checkmark = new Pen(Color.Green, 20);

		public ChecklistWidget()
		{
			InitializeComponent();
		}

		public void Initialize(Character character)
		{
			_character = character;
			grpChecklist.Shield();
		}

		public bool IsVisible()
		{
			return Config.DevMode ||
				string.IsNullOrEmpty(_character.Metadata.Writer) ||
				Config.IncludesUserName(_character.Metadata.Writer);
		}

		public IEnumerator DoWork()
		{
			pnlGood.Visible = false;
			bool skipMinor = false;
			grpChecklist.Shield();
			tasks.Clear();
			CheckMetadata();
			if (!CheckWardrobe())
			{
				grpChecklist.Unshield();
				yield break;
			}
			CheckTags();
			yield return 100;

			if (CheckGenericCases())
			{
				yield return 100;
				if (!CheckLineRequirements())
				{
					skipMinor = true;
				}
			}
			else
			{
				skipMinor = true;
			}

			yield return 100;

			CheckImageSizes();

			yield return 100;

			foreach (int delay in CheckLines())
			{
				yield return delay;
			}

			yield return 100;

			if (!skipMinor)
			{
				CheckSituations();
				grpChecklist.Unshield();

				if (Config.EnableDashboardSpellCheck)
				{
					yield return 500;
					grpChecklist.Shield();
					bool result = CheckSpelling();
					grpChecklist.Unshield();
					if (false && !result)
					{
						yield break;
					}
				}
				if (Config.EnableDashboardValidation)
				{
					yield return 2000;
					grpChecklist.Shield();
					bool result = CheckValidation();
					grpChecklist.Unshield();

					if (false && !result)
					{
						yield break;
					}
				}

				foreach (int delay in CheckMustTargets())
				{
					yield return delay;
				}

				foreach (int delay in CheckUntargeted())
				{
					yield return delay;
				}
			}

			if (tasks.Count == 0)
			{
				pnlGood.Visible = true;
			}

			grpChecklist.Unshield();
			yield break;
		}

		private void CheckMetadata()
		{
			if (string.IsNullOrEmpty(_character.Label))
			{
				AddMetadataTask("Character has no label", "The label is what other characters call yours.");
			}
			if (_character.Metadata.Portrait == null || string.IsNullOrEmpty(_character.Metadata.Portrait.Image))
			{
				AddMetadataTask("Assign a starting portrait", "The portrait is the image that appears on the character selection screen.");
			}
			if (string.IsNullOrEmpty(_character.Metadata.Writer))
			{
				AddMetadataTask("Assign a writer", "Ensure proper credit is received in game.");
			}
			if (string.IsNullOrEmpty(_character.Metadata.Artist))
			{
				AddMetadataTask("Assign an artist", "Ensure proper credit is received in game.");
			}
		}

		private void AddMetadataTask(string message, string helpText)
		{
			AddTask(message, helpText, typeof(MetadataEditor));
		}

		private void AddTask(string message, string helpText, Type activityType, params object[] runParameters)
		{
			ChecklistTask task = new ChecklistTask(message);
			task.HelpText = helpText;
			task.LaunchData = new LaunchParameters(_character, activityType, runParameters);
			tasks.AddTask(task);
		}

		private void AddTask(string message, string helpText, Action launchHandler)
		{
			ChecklistTask task = new ChecklistTask(message);
			task.HelpText = helpText;
			task.LaunchHandler = launchHandler;
			tasks.AddTask(task);
		}

		private bool CheckWardrobe()
		{
			if (_character.Layers == 2 && _character.Wardrobe[0].Name == "final layer")
			{
				AddTask("Fill out wardrobe", "Filling out the character's stripping sequence is vital to everything else.", typeof(WardrobeEditor));
				return false;
			}
			return true;
		}

		private void CheckTags()
		{
			if (_character.Tags.Count == 0)
			{
				AddTask("Fill out tags", "Tags describe your character's appearance and help other characters react intelligently to your character.", typeof(TagEditor));
			}
		}

		private bool CheckGenericCases()
		{
			if (Config.SuppressDefaults) { return true; }
			List<Case> cases = _character.Behavior.GetDefaultCases();
			if (cases.Count > 0)
			{
				ChecklistTask task = new ChecklistTask("Replace generic placeholder dialogue");
				task.ProgressBased = true;
				int total = 0;
				foreach (TriggerDefinition trigger in TriggerDatabase.Triggers)
				{
					if (trigger.Tag != "-" && !trigger.Optional && !string.IsNullOrEmpty(trigger.DefaultText))
					{
						total++;
					}
				}

				int finished = total;

				HashSet<string> visitedTags = new HashSet<string>();
				foreach (Case workingCase in cases)
				{
					string tag = workingCase.Tag;
					if (visitedTags.Contains(tag))
					{
						continue;
					}
					visitedTags.Add(tag);
					finished--;
				}

				task.Value = finished;
				task.MaxValue = total;

				task.HelpText = "Replacing placeholders gets your character to the bare minimum of line coverage.";
				ValidationContext context = new ValidationContext(new Stage(cases[0].Stages[0]), cases[0], null);
				task.LaunchData = new LaunchParameters(_character, typeof(DialogueEditor), context);
				tasks.AddTask(task);
				return false;
			}
			return true;
		}

		private bool CheckLineRequirements()
		{
			CharacterHistory history = CharacterHistory.Get(_character, false);
			TestRequirements requirements = TestRequirements.Instance;

			LineWork work = history.Current;
			if (work.TotalLines < requirements.Lines)
			{
				AddLineTask("Write more lines!", $"You need a minimum of {requirements.Lines} lines to be eligible for being added to the game.", work.TotalLines, requirements.Lines);
				return false;
			}
			if (work.TargetedCount < requirements.Targeted)
			{
				AddLineTask("Write lines to other characters", $"Games are more interesting when characters speak directly to each other.\r\n\r\nUse the Also Playing or Target conditions to direct dialogue towards another player.", work.TargetedCount, requirements.Targeted);
				return false;
			}
			if (work.Targets.Count < requirements.UniqueTargets)
			{
				AddLineTask("Write lines to more characters", $"You need to target at least {requirements.UniqueTargets} unique characters to be eligible for being added to the game.", work.Targets.Count, requirements.UniqueTargets);
				return false;
			}
			if (work.FilterCount < requirements.Filtered)
			{
				AddLineTask("Write filtered lines", $"Filtered lines are lines directed towards another character's tags (ex. a line directed towards a blonde).\r\nThese are important because targeting another character's appearance or personality traits ensures that your character will automatically have appropriate contextual dialogue for future characters.\r\n\r\nUse the Target > Target Tag condition to make a filtered line.", work.FilterCount, requirements.Filtered);
				return false;
			}

			return true;
		}

		private void AddLineTask(string message, string helpText, int value, int max)
		{
			ChecklistTask task = new ChecklistTask(message);
			task.HelpText = helpText;
			task.ProgressBased = true;
			task.Value = value;
			task.MaxValue = max;
			task.LaunchData = new LaunchParameters(_character, typeof(DialogueEditor));
			tasks.AddTask(task);
		}

		private void CheckImageSizes()
		{
			CharacterHistory history = CharacterHistory.Get(_character, false);
			TestRequirements requirements = TestRequirements.Instance;
			float size = history.GetTotalFileSize(false);
			if (size > requirements.SizeLimit)
			{
				AddTask("Compress images", $"Characters are allowed to use {requirements.SizeLimit}MB for non-epilogue images. Consider compressing your poses if you haven't yet in order to conserve space.", typeof(ScreenshotTaker));
			}
		}

		private void CheckSituations()
		{
			CharacterEditorData editorData = CharacterDatabase.GetEditorData(_character);
			if (editorData != null)
			{
				if (editorData.NoteworthySituations == null || editorData.NoteworthySituations.Count == 0)
				{
					AddTask("Call out situations", "Use the \"Call Out\" button for dialogue cases where something particularly interesting is happening that other characters should definitely react to.\r\n\r\n" +
						"These situations will appear in other characters' Writing Aid, making it easy to get other characters to interact with yours.", typeof(DialogueEditor));
				}
				else if (!editorData.ReviewedPriorities)
				{
					AddTask("Review situation priorities", "Situations can be given priorities to affect how frequently they appear in other characters' Writing Aid.\r\n\r\n" +
						"Use the Situations tab to update the Priority for any truly \"Must Target\" situation to use \"Must Target\".", typeof(SituationEditor), true);
				}
			}
		}

		private bool CheckSpelling()
		{
			if (!Config.EnableDashboardSpellCheck)
			{
				return true;
			}
			IWorkspace ws = Shell.Instance.GetWorkspace(_character);
			if (ws != null)
			{
				SpellCheckerService spellchecker = ws.GetData<SpellCheckerService>(CharacterWorkspace.SpellCheckerService);
				spellchecker?.Run();
				if (spellchecker.RemainingMisspellings > 0)
				{
					ChecklistTask task = new ChecklistTask($"Run Spell Check ({spellchecker.RemainingMisspellings})");
					task.HelpText = "You have some potentially misspelled words. For false positives (like \"ummmmmmmm\"), considering adding them to the dictionary to prevent this message from appearing again.";
					task.LaunchData = new LaunchParameters(_character, typeof(SpellCheck));
					tasks.AddTask(task);
					return false;
				}
			}
			return true;
		}

		private bool CheckValidation()
		{
			if (!Config.EnableDashboardValidation)
			{
				return true;
			}
			List<ValidationError> warnings;
			CharacterValidator.Validate(_character, out warnings);
			int count = 0;
			foreach (ValidationError error in warnings)
			{
				if (error.Level > ValidationFilterLevel.MissingTargets)
				{
					count++;
				}
			}
			if (count > 0)
			{
				ChecklistTask task = new ChecklistTask($"Resolve validator warnings ({count})");
				task.HelpText = "The Character Validator performs general QA of your character and helps catch problems before they reach the game.";
				task.LaunchData = new LaunchParameters(_character, typeof(ValidateActivity));
				tasks.AddTask(task);
				return false;
			}
			return true;
		}

		private IEnumerable<int> CheckMustTargets()
		{
			if (Config.SafeMode)
			{
				yield break;
			}
			const int Threshold = 20;
			int count = 0;
			List<Character> characters = CharacterDatabase.FilteredCharacters.ToList();
			characters.Shuffle();
			foreach (Character c in characters)
			{
				if (c.FolderName == "human" || c == _character)
				{
					continue;
				}
				CharacterEditorData editorData = CharacterDatabase.GetEditorData(c);
				if (editorData != null && editorData.NoteworthySituations.Count > 0)
				{
					for (int i = 0; i < editorData.NoteworthySituations.Count; i++)
					{
						if (editorData.NoteworthySituations[i].Priority == SituationPriority.MustTarget && !WritingAid.HasResponded(c, _character, editorData.NoteworthySituations[i], true))
						{
							AddTask("Respond to Must Target situations", "There are unanswered \"must targets\" from other characters. Failing to respond to these will make your character appear aloof when they occur.\r\n\r\n" +
								"To respond to Must Target situations, use the Writing Aid and filter the priority to Must Target.", typeof(WritingAid), SituationPriority.MustTarget);
							yield break;
						}
					}
				}
				if (count >= Threshold)
				{
					count = 0;
					yield return 50;
				}
			}
			yield break;
		}

		private IEnumerable<int> CheckUntargeted()
		{
			if (Config.SafeMode)
			{
				yield break;
			}
			const int Threshold = 20;
			CharacterHistory history = CharacterHistory.Get(_character, false);
			LineWork current = history.Current;
			List<Character> characters = CharacterDatabase.FilteredCharacters.ToList();
			characters.Shuffle();
			int count = 0;
			foreach (Character c in characters)
			{
				if (c == _character || c.FolderName == "human") { continue; }
				CharacterEditorData editorData = CharacterDatabase.GetEditorData(c);
				if (editorData.NoteworthySituations.Count > 0 && current.Targets.Find(t => t.Target == c.FolderName) == null)
				{
					string message;
					if (!string.IsNullOrEmpty(c.Metadata.Writer))
					{
						message = $"{c.Metadata.Writer} took the time to call out some important dialogue for {c}";
					}
					else
					{
						message = $"{c} has had some noteworthy situations called out";
					}
					message += $", but you haven't written anything to acknowledge their existence.\r\n\r\n" +
						$"Use the Writing Aid to write a line towards {c}.";
					AddTask($"{c} is feeling neglected", message, typeof(WritingAid), c);
					yield break;
				}
				count++;
				if (count >= Threshold)
				{
					count = 0;
					yield return 20;
				}
			}
		}

		/// <summary>
		/// Checks lines for various fixups
		/// </summary>
		/// <returns></returns>
		private IEnumerable<int> CheckLines()
		{
			foreach (DuplicateCase dupe in _character.EnumerateDuplicates())
			{
				//found a duplicate
				AddTask("Clean up duplicate cases", "The same case was found in each of the \"Hand=(Good/Okay/Bad)\" case types. This can be simplified by using a single \"Hand (Any)\" case.\r\n\r\nClick Go to open the Case Merger Utility.", () =>
				{
					CaseMerger form = new CaseMerger();
					form.SetData(_character);
					form.ShowDialog();
				});
				break;
			}
			yield break;
		}

		private void pnlGood_Paint(object sender, PaintEventArgs e)
		{
			const int CheckSize = 200;

			Graphics g = e.Graphics;
			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
			_checkmark.Color = SkinManager.Instance.CurrentSkin.GoodForeColor;
			g.DrawLines(_checkmark, new PointF[] {
				new PointF(pnlGood.Width / 2 - CheckSize / 2, pnlGood.Height / 2),
				new PointF(pnlGood.Width / 2, pnlGood.Height / 1.5f),
				new PointF(pnlGood.Width / 2 + CheckSize / 2, pnlGood.Height / 2 - CheckSize / 2),
			});

			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
		}
	}
}
