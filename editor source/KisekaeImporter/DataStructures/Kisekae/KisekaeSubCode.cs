using System.Collections.Generic;

namespace KisekaeImporter
{
	/// <summary>
	/// .-delimited pieces
	/// </summary>
	public class KisekaeSubCode
	{
		public string Id { get; internal set; }
		public int Index { get; set; }
		private List<string> _pieces = new List<string>();

		public KisekaeSubCode()
		{
			Index = -1;
		}

		public KisekaeSubCode(string id)
		{
			Index = -1;
			Id = id;
		}

		public KisekaeSubCode(string id, string[] data)
		{
			_pieces = new List<string>(data.Length);
			foreach (string piece in data)
			{
				_pieces.Add(piece);
			}
		}

		public void Reset()
		{
			_pieces.Clear();
		}

		public bool IsEmpty
		{
			get { return _pieces.Count == 0 || _pieces.Count == 1 && _pieces[0] == ""; }
		}

		public override string ToString()
		{
			string id = Id;
			if (Index >= 0)
			{
				if (id == "u")
				{
					//generalize this if more than u ever gets a single digit
					id += Index.ToString();
				}
				else
				{
					id += Index.ToString("00");
				}
			}
			if (_pieces.Count > 0 && _pieces[0] == "")
				return id;
			return id + string.Join(".", _pieces);
		}

		/// <summary>
		/// Gets an int representation of a piece
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public int GetInt(int index)
		{
			if (index < 0 || index >= _pieces.Count)
				return 0;
			int value;
			int.TryParse(_pieces[index], out value);
			return value;
		}

		/// <summary>
		/// Gets a string representation of a piece
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public string GetString(int index)
		{
			if (index < 0 || index >= _pieces.Count)
				return "0";
			return _pieces[index];
		}

		/// <summary>
		/// Gets a bool representation of a piece
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public bool GetBool(int index)
		{
			if (index < 0 || index >= _pieces.Count)
				return false;
			int value;
			int.TryParse(_pieces[index], out value);
			return value > 0;
		}

		public void Set(int index, string value)
		{
			while (_pieces.Count <= index)
			{
				_pieces.Add("0");
			}
			_pieces[index] = value;
		}

		public void Set(int index, int value)
		{
			Set(index, value.ToString());
		}

		public void Set(int index, bool value)
		{
			Set(index, value ? "1" : "0");
		}

		public string[] GetData()
		{
			return _pieces.ToArray();
		}

		/// <summary>
		/// Populates the subcode with data from a string represenation
		/// </summary>
		public void Deserialize(string[] data)
		{
			for (int i = 0; i < data.Length; i++)
			{
				Set(i, data[i]);
			}
		}

		public void Disable()
		{
			_pieces = new List<string>();
			_pieces.Add("");
		}
	}

	public class KisekaeSubCodeArray
	{
		private SortedList<int, KisekaeSubCode> _array = new SortedList<int, KisekaeSubCode>();

		public void Add(int index, KisekaeSubCode code)
		{
			code.Index = index;
			_array[index] = code;
		}

		public KisekaeSubCode Get(int index)
		{
			KisekaeSubCode code;
			_array.TryGetValue(index, out code);
			return code;
		}

		public IEnumerable<KisekaeSubCode> SubCodes
		{
			get
			{
				foreach (var code in _array.Values)
				{
					yield return code;
				}
			}
		}
	}
}
