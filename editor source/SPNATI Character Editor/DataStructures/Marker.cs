using System;
using System.Xml.Serialization;

namespace SPNATI_Character_Editor
{
	[XmlRoot("marker")]
	[Serializable]
	/// <summary>
	/// Contains descriptive information about a marker. This doesn't actually control anything in game, it's just for documentation
	/// </summary>
	public class Marker : IComparable<Marker>
	{
		[XmlAttribute("name")]
		/// <summary>
		/// Marker name, what's used in game
		/// </summary>
		public string Name;

		[XmlAttribute("scope")]
		/// <summary>
		/// Controls whether the marker will be visible to other charaters in the editor. Does not control anything in game
		/// </summary>
		public MarkerScope Scope;

		[XmlText]
		/// <summary>
		/// Description about what this marker controls
		/// </summary>
		public string Description;

		public Marker()
		{
			Scope = MarkerScope.Public;
		}

		public Marker(string name) : this()
		{
			Name = name;
		}

		public override string ToString()
		{
			return Name;
		}

		public int CompareTo(Marker other)
		{
			return Name.CompareTo(other.Name);
		}
	}

	public enum MarkerScope
	{
		Private,
		Public
	}
}
