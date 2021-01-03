using Desktop;
using SPNATI_Character_Editor.DataStructures;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Activities
{
	[Activity(typeof(Deck), 0)]
	public partial class DeckEditor : Activity
	{
		private Deck _deck;
		private CardFront _front;
		private CardBack _back;
		private bool _switchingFront;

		public DeckEditor()
		{
			InitializeComponent();
		}

		protected override void OnInitialize()
		{
			_deck = Record as Deck;
			DeckSetProvider.InvalidateDeckSets();
			SubscribeWorkspace<bool>(WorkspaceMessages.Save, OnSaveWorkspace);

			table.Data = _deck;

			recFront.RecordType = typeof(DeckSet);
			if (_deck.Fronts.Count == 0)
			{
				//add a default front
				CardFront front = _deck.AddFront();
				front.Src = "img/%s%i.jpg";
			}
			foreach (CardFront front in _deck.Fronts)
			{
				AddTab(front);
			}
			SetFront(_deck.Fronts[0]);
			lstBacks.DisplayMember = "Src";
			foreach (CardBack back in _deck.Backs)
			{
				lstBacks.Items.Add(back);
			}
			if (lstBacks.Items.Count > 0)
			{
				lstBacks.SelectedIndex = 0;
			}

			_deck.PropertyChanged += _deck_PropertyChanged;
		}

		private void AddTab(CardFront front)
		{
			TabPage page = new TabPage(front.ToString());
			page.Tag = front;
			tabsFront.TabPages.Add(page);
		}

		private void SetFront(CardFront front)
		{
			if (_front != null)
			{
				_front.PropertyChanged -= _front_PropertyChanged;
			}
			_front = front;
			if (_front != null)
			{
				_front.PropertyChanged += _front_PropertyChanged;
			}
			_switchingFront = true;
			if (front != null && !string.IsNullOrEmpty(front.Src))
			{
				recFront.RecordKey = DeckSet.ToKey(front);
			}
			else
			{
				recFront.RecordKey = null;
			}
			_switchingFront = false;
		}

		private void _front_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "Src")
			{
				//update the tab caption
				foreach (TabPage tab in tabsFront.TabPages)
				{
					if (tab.Tag == _front)
					{
						string name = _front.ToString();
						tab.Text = name;
						return;
					}
				}
			}
		}

		private void SetBack(CardBack back)
		{
			_back = back;
			txtBack.Text = _back?.Src;
			UpdateBackPreview();
		}

		public override bool CanQuit(CloseArgs args)
		{
			if (_deck.IsDirty)
			{
				DialogResult result = MessageBox.Show("Do you want to save changes to this deck?", _deck.Name, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
				if (result == DialogResult.Cancel)
				{
					return false;
				}
				else if (result == DialogResult.Yes)
				{
					Export();
				}
			}
			return true;
		}

		private void _deck_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (!_deck.IsDirty)
			{
				_deck.IsDirty = true;
			}
			if (e.PropertyName == "Backs")
			{
				lstBacks.RefreshListItems();
			}
		}

		private void OnSaveWorkspace(bool auto)
		{
			if (!auto)
			{
				Export();
			}
		}

		public override void Quit()
		{
			picBack.Image?.Dispose();
		}

		private void Export()
		{
			DeckDatabase.Save();
			_deck.IsDirty = false;
		}

		private void cmdBrowseBack_Click(object sender, System.EventArgs e)
		{
			string path = Path.Combine(Config.SpnatiDirectory, _back.Src ?? "");
			if (path == Config.SpnatiDirectory)
			{
				openFileDialog1.FileName = "";
				path = Path.Combine(Config.SpnatiDirectory, "img");
			}
			else
			{
				openFileDialog1.FileName = Path.GetFileName(path);
				path = Path.GetDirectoryName(path);
			}
			openFileDialog1.InitialDirectory = path;

			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				string filename = openFileDialog1.FileName;
				string relPath = filename.Substring(Config.SpnatiDirectory.Length + 1);
				txtBack.Text = _back.Src = relPath.Replace('\\', '/');
				UpdateBackPreview();
			}
		}

		private void UpdateBackPreview()
		{
			picBack.Image?.Dispose();
			string path = Path.Combine(Config.SpnatiDirectory, _back?.Src ?? "");
			if (string.IsNullOrEmpty(path) || !File.Exists(path))
			{
				picBack.Image = null;
			}
			else
			{
				using (Bitmap bmp = new Bitmap(path))
				{
					Bitmap preview = new Bitmap(bmp);
					picBack.Image = preview;
				}
			}
		}

		private void recFront_RecordChanged(object sender, Desktop.CommonControls.RecordEventArgs e)
		{
			DeckSet set = recFront.Record as DeckSet;
			if (!_switchingFront)
			{
				if (set == null)
				{
					set = new DeckSet("img", "%s%i", ".jpg"); //force a default
				}
				_front.Src = set.ToSrc();
				_front.Ranks = set.RankInterval;
				_front.Suits = set.Suits;
			}
			else if (set != null)
			{
				//compare suit+ranks to the stored value and update if changed
				if (_front.Ranks != set.RankInterval || _front.Suits != set.Suits)
				{
					_front.Ranks = set.RankInterval;
					_front.Suits = set.Suits;
					MessageBox.Show("The available images for this set have changed since the last save, so the deck has been automatically updated to match.");
				}
			}
			UpdateDeck(set);
		}

		private void UpdateDeck(DeckSet set)
		{
			UpdateSuit(set, pnlHeart);
			UpdateSuit(set, pnlClub);
			UpdateSuit(set, pnlDiamond);
			UpdateSuit(set, pnlSpade);
		}

		private void DestroySuit(Panel panel)
		{
			foreach (Control ctl in panel.Controls)
			{
				PictureBox box = ctl as PictureBox;
				if (box != null)
				{
					box.Image?.Dispose();
					box.Image = null;
				}
			}
			panel.Controls.Clear();
		}

		private void UpdateSuit(DeckSet set, Panel panel)
		{
			DestroySuit(panel);
			if (recFront.Record == null)
			{
				return;
			}
			string prefix = panel.Tag?.ToString() ?? "";
			string pattern = set.FileName;
			string extension = set.Extension;
			string rootPath = Path.Combine(Config.SpnatiDirectory, set.Folder, pattern + extension).Replace("%s", prefix);

			int x = 0;
			for (int i = 1; i <= 13; i++)
			{
				string file = rootPath.Replace("%i", i.ToString());
				PictureBox box = new PictureBox();
				box.SizeMode = PictureBoxSizeMode.Zoom;
				box.Height = panel.Height - panel.Margin.Top - panel.Margin.Bottom - SystemInformation.HorizontalScrollBarHeight;
				box.Width = (int)(0.75f * box.Height);
				panel.Controls.Add(box);
				box.Left = x;
				x += box.Width + 3;
				if (File.Exists(file))
				{
					using (Bitmap bmp = new Bitmap(file))
					{
						box.Image = new Bitmap(bmp);
					}
				}
				else
				{
					//make up a placeholder
					Bitmap bmp = new Bitmap(300, 400);
					using (Graphics g = Graphics.FromImage(bmp))
					{
						g.FillRectangle(Brushes.LightGray, 0, 0, bmp.Width, bmp.Height);
						using (Pen pen = new Pen(Brushes.Red, 10))
						{
							int size = 160;
							g.DrawEllipse(pen, new Rectangle(bmp.Width / 2 - size / 2, bmp.Height / 2 - size / 2, size, size));
							g.DrawLine(pen, bmp.Width / 2 - size / 2, bmp.Height / 2 + size / 2, bmp.Width / 2 + size / 2, bmp.Height / 2 - size / 2);
						}

						using (StringFormat sf = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
						{
							using (Font font = new Font("Arial", 24, FontStyle.Bold))
							{
								const int height = 40;
								string suit = i.ToString();
								if (i == 11)
								{
									suit = "J";
								}
								else if (i == 12)
								{
									suit = "Q";
								}
								else if (i == 13)
								{
									suit = "K";
								}
								else if (i == 1)
								{
									suit = "A";
								}
								g.DrawString(suit, font, Brushes.Black, new RectangleF(0, 10, bmp.Width, height), sf);
								g.DrawString($"{pattern.Replace("%s", prefix).Replace("%i", i.ToString())}{extension}", font, Brushes.Black, new RectangleF(0, bmp.Height - height - 10, bmp.Width, height), sf);
							}
						}
						box.Image = bmp;
					}
				}
			}
		}

		private void cmdAddBack_Click(object sender, System.EventArgs e)
		{
			CardBack back = _deck.AddBack();
			lstBacks.Items.Add(back);
			lstBacks.SelectedItem = back;

			//auto-open the file selection
			cmdBrowseBack_Click(cmdBrowseBack, e);
		}

		private void cmdRemoveBack_Click(object sender, System.EventArgs e)
		{
			if (_back == null) { return; }
			_deck.RemoveBack(_back);
			lstBacks.Items.Remove(_back);
		}

		private void lstBacks_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			SetBack(lstBacks.SelectedItem as CardBack);
		}

		private void tabStrip_AddButtonClicked(object sender, System.EventArgs e)
		{
			CardFront front = _deck.AddFront();
			AddTab(front);
			tabsFront.SelectedIndex = tabsFront.TabPages.Count - 1;
		}

		private void tabStrip_CloseButtonClicked(object sender, System.EventArgs e)
		{
			if (_front != null && MessageBox.Show("Are you sure you want to delete this front?", "Remove Card Front", MessageBoxButtons.OKCancel) == DialogResult.OK)
			{
				_deck.RemoveFront(_front);
				for (int i = 0; i < tabsFront.TabPages.Count; i++)
				{
					if (tabsFront.TabPages[i].Tag == _front)
					{
						tabsFront.TabPages.RemoveAt(i);
						break;
					}
				}
			}
		}

		private void tabsFront_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			SetFront(tabsFront.SelectedTab.Tag as CardFront);
		}
	}

	public class DeckSet : IRecord
	{
		public string Key { get; set; }
		public string Name { get; set; }
		public string Folder;
		public string FileName;
		public string Extension;

		public string RankInterval;
		public string Suits;

		public string Group { get { return null; } }

		public int CompareTo(IRecord other)
		{
			return Key.CompareTo(other.Key);
		}

		public string ToLookupString()
		{
			return Name;
		}

		public DeckSet()
		{
		}

		public static string ToKey(CardFront front)
		{
			string src = front.Src.Replace('\\', '/').Replace("%s", "").Replace("%i", "");
			string ext = Path.GetExtension(src);
			if (!string.IsNullOrEmpty(ext))
			{
				src = src.Substring(0, src.LastIndexOf(ext));
			}

			return src;
		}

		public string ToSrc()
		{
			string src = Path.Combine(Folder, $"{FileName}{Extension}").Replace('\\', '/');
			if (src.StartsWith(Config.SpnatiDirectory.Replace('\\', '/')))
			{
				src = src.Substring(Config.SpnatiDirectory.Length + 1);
			}
			return src;
		}

		public DeckSet(string folder, string filePattern, string extension)
		{
			Folder = folder.Replace('\\', '/');
			FileName = filePattern;
			string rawName = filePattern.Replace("%s", "").Replace("%i", "");
			string namePath = Folder;
			if (namePath.StartsWith(Config.SpnatiDirectory.Replace('\\', '/')))
			{
				namePath = namePath.Substring(Config.SpnatiDirectory.Length + 1);
			}
			Name = $"{namePath}/{rawName}";
			Extension = extension;
			Key = Name;

			if (folder == "img")
			{
				return;
			}

			string[] suits = new string[] { "heart", "clubs", "spade", "diamo" };
			HashSet<string> usedSuits = new HashSet<string>();
			Dictionary<string, HashSet<int>> missingRanks = new Dictionary<string, HashSet<int>>();

			string src = Path.Combine(Config.SpnatiDirectory, ToSrc());
			Dictionary<string, HashSet<int>> allRanks = new Dictionary<string, HashSet<int>>();

			//Identify the suits and ranks
			foreach (string suit in suits)
			{
				HashSet<int> ranksInSuit = new HashSet<int>();
				allRanks[suit] = ranksInSuit;
				for (int rank = 2; rank <= 14; rank++)
				{
					string filename = src.Replace("%s", suit).Replace("%i", (rank < 14 ? rank : 1).ToString());
					if (File.Exists(filename))
					{
						usedSuits.Add(suit);
						ranksInSuit.Add(rank);
					}
				}
			}

			//keep only the ranks that are in common among all suits
			List<int> remainingRanks = new List<int>();
			for (int rank = 2; rank <= 14; rank++)
			{
				bool used = true;
				foreach (string suit in usedSuits)
				{
					if (!allRanks[suit].Contains(rank))
					{
						used = false;
						break;
					}
				}
				if (used)
				{
					remainingRanks.Add(rank);
				}
			}

			//convert to interval
			if (remainingRanks.Count == 14)
			{
				RankInterval = null; //shortcut for all
			}
			else
			{
				RankInterval = GUIHelper.ListToString(remainingRanks);
			}
			if (usedSuits.Count == 4)
			{
				Suits = null;
			}
			else
			{
				Suits = string.Join(",", usedSuits);
			}
		}
	}

	public class DeckSetProvider : IRecordProvider<DeckSet>
	{
		public bool AllowsNew { get { return false; } }

		public bool AllowsDelete { get { return false; } }

		public bool TrackRecent { get { return false; } }

		public IRecord Create(string key)
		{
			throw new System.NotImplementedException();
		}

		public void Delete(IRecord record)
		{
			throw new System.NotImplementedException();
		}

		public bool FilterFromUI(IRecord record)
		{
			return false;
		}

		public void SetFormatInfo(LookupFormat info)
		{
			info.Caption = "Choose a Folder";
			info.Columns = new string[] { "Folder" };
		}

		public ListViewItem FormatItem(IRecord record)
		{
			ListViewItem item = new ListViewItem(record.Name);
			return item;
		}

		public void SetContext(object context)
		{

		}

		public void Sort(List<IRecord> list)
		{
			list.Sort();
		}

		public List<IRecord> GetRecords(string text, LookupArgs args)
		{
			text = text.ToLower();
			List<IRecord> fullList = GetAllSets();
			List<IRecord> list = new List<IRecord>();
			foreach (IRecord rec in fullList)
			{
				if (rec.Key.ToLower().Contains(text))
				{
					list.Add(rec);
				}
			}
			return list;
		}

		private static List<IRecord> _deckSets = null;
		public static void InvalidateDeckSets()
		{
			_deckSets = null;
		}

		private List<IRecord> GetAllSets()
		{
			if (_deckSets == null)
			{
				//scan the img folder for usable directories
				List<IRecord> list = new List<IRecord>();

				//always add the root as an option for the default
				list.Add(new DeckSet("img", "%s%i", ".jpg"));

				foreach (string directory in Directory.EnumerateDirectories(Path.Combine(Config.SpnatiDirectory, "img")))
				{
					ScanFolder(directory, list);
				}
				_deckSets = list;
			}
			return _deckSets;
		}

		private void ScanFolder(string folder, List<IRecord> list)
		{
			//a folder could feasibly contain different sets with different file patters, so try to identify them all

			string[] suits = new string[] { "clubs", "heart", "diamo", "spade" };
			Regex regex = new Regex(@"^[a-zA-Z_]*(?<rank>[1-9][0-2]?)[a-zA-Z_]*(?<suit>clubs|spade|diamo|heart)[a-zA-Z_]*$|^[a-zA-Z_]*(?<suit>clubs|spade|diamo|heart)[a-zA-Z_]*[a-zA-Z_]*(?<rank>[1-9][0-2]?)[a-zA-Z_]*$");

			HashSet<string> patterns = new HashSet<string>();

			HashSet<string> extensionSet = new HashSet<string>();
			string[] extensions = new string[] { ".png", ".gif", ".bmp", ".jpg", ".svg" };
			extensionSet.AddRange(extensions);
			foreach (string file in Directory.EnumerateFiles(folder).Where(f => extensions.Contains(Path.GetExtension(f))))
			{
				//see if this fits the requirements for a set
				string filename = Path.GetFileNameWithoutExtension(file);

				Match match = regex.Match(filename);
				if (match.Success)
				{
					string suit = match.Groups["suit"].Value;
					string rank = match.Groups["rank"].Value;
					string pattern = filename.Replace(suit, "%s").Replace(rank, "%i");
					if (!patterns.Contains(pattern))
					{
						//new pattern found
						patterns.Add(pattern);
						DeckSet set = new DeckSet(folder, pattern, Path.GetExtension(file));
						list.Add(set);
					}
				}
			}

			// Go deeper
			foreach (string directory in Directory.EnumerateDirectories(folder))
			{
				ScanFolder(folder, list);
			}
		}
	}
}
