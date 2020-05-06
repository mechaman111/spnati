using System;
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
			if (data == null) { return; }
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

		public Marker Get(string marker)
		{
			Marker m;
			_markers.TryGetValue(marker, out m);
			return m;
		}

		public bool Contains(string marker)
		{
			return _markers.ContainsKey(marker);
		}

		public void Add(Marker marker)
		{
			Marker existing = _markers.Get(marker.Name);
			_markers[marker.Name] = marker;
			if (existing != null)
			{
				foreach (string value in existing.Values)
				{
					marker.AddValue(value);
				}
			}
		}

		public void Cache(string marker)
		{
			if (string.IsNullOrEmpty(marker))
				return;
			string value;
			bool perTarget;
			string op;
			marker = Marker.ExtractPieces(marker, out value, out perTarget, out op);
			Marker m = _markers.GetOrAddDefault(marker, () => new Marker(marker));
			m.AddValue(value);
		}

		public void RemoveReference(string marker)
		{
			if (string.IsNullOrEmpty(marker))
				return;
			string value;
			bool perTarget;
			string op;
			marker = Marker.ExtractPieces(marker, out value, out perTarget, out op);
			Marker m = _markers.Get(marker);
			if (m != null)
			{
				m.RemoveValue(value);
			}
		}

		public IEnumerable<Marker> Values
		{
			get { return _markers.Values; }
		}

		public void OnAfterDeserialize(string source)
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
			Markers.Sort();
		}
	}
}
