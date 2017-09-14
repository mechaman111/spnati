using SPNATI_Character_Editor.IO;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace SPNATI_Character_Editor
{
	/// <summary>
	/// Data representation for behaviour.xml and meta.xml
	/// </summary>
	/// <remarks>
	/// PROPERTY ORDER IS IMPORTANT - Order determines the order attributes are placed in the xml files
	/// </remarks>
	[XmlRoot("opponent", Namespace = "")]
	[XmlHeader("This file is machine generated. Please do not edit it directly without preserving your improvements elsewhere or your changes may be lost the next time this file is generated.")]
	public class Character : IHookSerialization
	{
		[XmlIgnore]
		public Metadata Metadata;

		[XmlIgnore]
		public string FolderName;

		[XmlElement("first")]
		public string FirstName;

		[XmlElement("last")]
		public string LastName;

		[XmlElement("label")]
		public string Label;

		[XmlElement("gender")]
		public string Gender;

		[XmlElement("size")]
		public string Size;

		[XmlElement("timer")]
		public int Stamina;

		[XmlNewLine]
		[XmlElement("intelligence")]
		public string Intelligence;

		[XmlArray("tags")]
		[XmlArrayItem("tag")]
		public List<string> Tags;

		[XmlNewLine]
		[XmlArray("start")]
		[XmlArrayItem("state")]
		public List<DialogueLine> StartingLines;

		[XmlNewLine]
		[XmlArray("wardrobe")]
		[XmlArrayItem("clothing")]
		public List<Clothing> Wardrobe = new List<Clothing>();

		[XmlNewLine(XmlNewLinePosition.Both)]
		[XmlElement("behaviour")]
		public Behaviour Behavior = new Behaviour();

		[XmlNewLine(XmlNewLinePosition.After)]
		[XmlElement("epilogue")]
		public List<Epilogue> Endings;

		public Character()
		{
			FirstName = "New";
			LastName = "Character";
			Label = "Opponent";
			Gender = "female";
			Size = "medium";
			Intelligence = "average";
			Stamina = 15;
			Tags = new List<string>();
			Metadata = new Metadata();
			Wardrobe = new List<Clothing>();
			StartingLines = new List<DialogueLine>();
			Endings = new List<Epilogue>();
		}

		/// <summary>
		/// Clears all data from this character
		/// </summary>
		public void Clear()
		{
			FirstName = "";
			LastName = "";
			Label = "";
			Gender = "";
			Size = "";
			Behavior = new Behaviour();
			Intelligence = "average";
			Stamina = 15;
			Tags.Clear();
			Metadata = new Metadata();
			Wardrobe = new List<Clothing>();
			StartingLines = new List<DialogueLine>();
			Endings = new List<Epilogue>();
		}

		public override string ToString()
		{
			return Label;
		}

		/// <summary>
		/// DisplayMember only works with properties, so this is for what to display in the LoadCharacterPrompt
		/// </summary>
		public string DisplayName { get { return FolderName; } }

		#region Outfit
		public int Layers
		{
			get { return Wardrobe.Count; }
		}

		/// <summary>
		/// Converts a layer to a user friendly name based on the wardrobe
		/// </summary>
		/// <param name="layer"></param>
		public StageName LayerToStageName(int layer)
		{
			return LayerToStageName(layer, false);
		}

		/// <summary>
		/// Converts a layer to a user friendly name based on the wardrobe
		/// </summary>
		/// <param name="layer"></param>
		public StageName LayerToStageName(int layer, bool advancingStage)
		{
			if (layer < 0 || layer >= Wardrobe.Count + Clothing.ExtraStages)
				return null;
			string label = layer.ToString();
			if (advancingStage)
			{
				layer++;
				if (layer < Wardrobe.Count)
				{
					Clothing clothes = Wardrobe[Layers - layer];
					label = "losing " + clothes.Name;
				}
				else if (layer == Wardrobe.Count)
				{
					label = "lost all clothing";
				}
				else label = "";
			}
			else
			{
				if (layer == 0)
					label = "Fully Clothed";
				else if (layer < Wardrobe.Count)
				{
					int index = layer - 1;
					Clothing lastClothes = Wardrobe[Layers - 1 - index];
					label = "Lost " + lastClothes.Name;
				}
				else if (layer == Wardrobe.Count)
				{
					label = "Naked";
				}
				else if (layer == Wardrobe.Count + 1)
				{
					label = "Masturbating";
				}
				else if (layer == Wardrobe.Count + 2)
				{
					label = "Finished";
				}
			}
			return new StageName(layer.ToString(), label);
		}

		/// <summary>
		/// Adds a new layer
		/// </summary>
		/// <param name="layer">Layer to add</param>
		/// <returns>Stage of added layer</returns>
		public int AddLayer(Clothing layer)
		{
			Wardrobe.Insert(0, layer);
			return Wardrobe.Count - 1;
		}

		/// <summary>
		/// Removes a layer
		/// </summary>
		/// <param name="layer">Layer to remove</param>
		/// <returns>Stage of removed layer</returns>
		public int RemoveLayer(Clothing layer)
		{
			int index = Wardrobe.IndexOf(layer);
			if(index >= 0)
				Wardrobe.RemoveAt(index);
			return Wardrobe.Count - index;
		}

		/// <summary>
		/// Moves the clothing item at the given index down
		/// </summary>
		/// <param name="layer">Layer to move</param>
		/// <returns>Stage of layer before it was moved</returns>
		public int MoveDown(Clothing layer)
		{
			int index = Wardrobe.IndexOf(layer);
			if (index < 1 || index >= Wardrobe.Count)
				return -1;
			Clothing item = Wardrobe[index];
			Wardrobe.RemoveAt(index);
			Wardrobe.Insert(index - 1, item);
			return Wardrobe.Count - index;
		}

		/// <summary>
		/// Moves the clothing at the given index up
		/// </summary>
		/// <param name="layer">Layer to move</param>
		/// <returns>Stage of layer before it was moved</returns>
		public int MoveUp(Clothing layer)
		{
			int index = Wardrobe.IndexOf(layer);
			if (index < 0 || index >= Wardrobe.Count - 1)
				return -1;
			Clothing item = Wardrobe[index];
			Wardrobe.RemoveAt(index);
			Wardrobe.Insert(index + 1, item);
			return Wardrobe.Count - index;
		}

		/// <summary>
		/// Applies wardrobe changes to the dialogue tree
		/// </summary>
		/// <param name="changes"></param>
		public void ApplyWardrobeChanges(Queue<WardrobeChange> changes)
		{
			Behavior.ApplyWardrobeChanges(this, changes);
		}
		#endregion

		#region Serialization
		public void OnBeforeSerialize()
		{
			string dir = Config.GetRootDirectory(this);
			foreach (var line in StartingLines)
			{
				string image = Path.GetFileNameWithoutExtension(line.Image) + line.ImageExtension;
				if (!string.IsNullOrEmpty(line.Image) && !char.IsNumber(line.Image[0]) && !File.Exists(Path.Combine(dir, image)))
				{
					line.Image = "0-" + line.Image;
				}
			}
			Behavior.OnBeforeSerialize(this);
			Metadata.HasEnding = Endings.Count > 0;
		}

		public void OnAfterDeserialize()
		{
			foreach (var line in StartingLines)
			{
				line.Text = XMLHelper.DecodeEntityReferences(line.Text);
			}
			Behavior.OnAfterDeserialize(this);
			Metadata.HasEnding = Endings.Count > 0;
		}
		#endregion
		
		/// <summary>
		/// Called when editing a character in the editor to make sure working fields are built properly.
		/// Working fields are set up lazily so as to not inflict the performance cost on every single character during startup
		/// </summary>
		public void PrepareForEdit()
		{
			Behavior.PrepareForEdit(this);
		}
	}

	/// <summary>
	/// Change to wardrobe, used for updating dialogue stages
	/// </summary>
	public class WardrobeChange
	{
		/// <summary>
		/// Type of change performed
		/// </summary>
		public WardrobeChangeType Change;
		/// <summary>
		/// Index of item being changed
		/// </summary>
		public int Index;

		public WardrobeChange(WardrobeChangeType type, int index)
		{
			Change = type;
			Index = index;
		}
	}

	public enum WardrobeChangeType
	{
		/// <summary>
		/// Item as added at the given index
		/// </summary>
		Add,
		/// <summary>
		/// Item was removed from the given index
		/// </summary>
		Remove,
		/// <summary>
		/// Item was moved up, originally located at the given index
		/// </summary>
		MoveUp,
		/// <summary>
		/// Item was moved down, originally located at the given index
		/// </summary>
		MoveDown
	}
}
