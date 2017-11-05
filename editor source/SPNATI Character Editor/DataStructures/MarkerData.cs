using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SPNATI_Character_Editor
{
	[XmlRoot("markers")]
	[Serializable]
	public class MarkerData : IHookSerialization
	{
		[XmlElement("marker")]
		public List<Marker> Markers = new List<Marker>();

		[XmlIgnore]
		private Dictionary<string, Marker> _markers = new Dictionary<string, Marker>();

		/// <summary>
		/// Merges another marker data into this one
		/// </summary>
		/// <param name="data"></param>
		public void Merge(MarkerData data)
		{
			foreach (var marker in data.Markers)
			{
				Add(marker);
			}
		}

		public int Count
		{
			get
			{
				return _markers.Count;
			}
		}

		public void Clear()
		{
			_markers.Clear();
		}

		public bool Contains(string marker)
		{
			return _markers.ContainsKey(marker);
		}

		public void Add(Marker marker)
		{
			_markers[marker.Name] = marker;
		}

		public void Cache(string marker)
		{
			if (string.IsNullOrEmpty(marker))
				return;
			if (_markers.ContainsKey(marker))
				return;
			_markers[marker] = new Marker(marker);
		}

		public IEnumerable<Marker> Values
		{
			get { return _markers.Values; }
		}

		public void OnAfterDeserialize()
		{
			_markers.Clear();
			foreach (var marker in Markers)
			{
				_markers[marker.Name] = marker;
			}
		}

		public void OnBeforeSerialize()
		{
			Markers.Clear();
			foreach (var kvp in _markers)
			{
				Markers.Add(kvp.Value);
			}
		}
	}
}
