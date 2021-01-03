using Desktop;
using SPNATI_Character_Editor.IO;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace SPNATI_Character_Editor.DataStructures
{
	/// <summary>
	/// Manages decks
	/// </summary>
	public static class DeckDatabase
	{
		private static Dictionary<string, Deck> _decks = new Dictionary<string, Deck>();
		
		public static void Load()
		{
			DeckList list = Serialization.ImportConfigFile<DeckList>("cards.xml");
			foreach (Deck deck in list.Decks)
			{
				_decks[deck.Key] = deck;
			}
		}

		public static void Save()
		{
			DeckList list = new DeckList();
			list.Decks = new List<Deck>(Decks);
			Serialization.ExportXml(list, Path.Combine(Config.SpnatiDirectory, "cards.xml"));
		}

		public static IEnumerable<Deck> Decks { get { return _decks.Values; } }

		public static void Add(Deck deck)
		{
			_decks[deck.Key] = deck;
		}
	}

	[XmlRoot("card-decks", Namespace = "")]
	[XmlHeader("This file contains information on all custom card decks within the game.")]
	public class DeckList
	{
		[XmlElement("deck")]
		public List<Deck> Decks = new List<Deck>();
	}

	public class DeckProvider : IRecordProvider<Deck>
	{
		public bool AllowsNew { get { return true; } }

		public bool AllowsDelete { get { return false; } }

		public bool TrackRecent { get { return false; } }

		public IRecord Create(string key)
		{
			Deck deck = new Deck();
			deck.Key = deck.Name = key;
			DeckDatabase.Add(deck);
			return deck;
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
			info.Caption = "Choose a Deck";
			info.Columns = new string[] { "Id", "Title", "Subtitle", "Description" };
		}

		public ListViewItem FormatItem(IRecord record)
		{
			Deck deck = record as Deck;
			ListViewItem item = new ListViewItem(new string[] { deck.Key, deck.Name, deck.SubTitle, deck.Description });
			return item;
		}

		public List<IRecord> GetRecords(string text, LookupArgs args)
		{
			text = text.ToLower();
			List<IRecord> list = new List<IRecord>();
			foreach (Deck record in DeckDatabase.Decks)
			{
				if (record.Key.ToLower().Contains(text) ||
					record.Name.ToLower().Contains(text) ||
					record.SubTitle.ToLower().Contains(text) ||
					(record.Description ?? "").ToLower().Contains(text))
				{
					//partial match
					list.Add(record);
				}
			}
			return list;
		}

		public void SetContext(object context)
		{
			
		}

		public void Sort(List<IRecord> list)
		{
			list.Sort();
		}
	}
}
