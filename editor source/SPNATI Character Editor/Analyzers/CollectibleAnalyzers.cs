using System.Linq;

namespace SPNATI_Character_Editor.Analyzers
{
	public class CollectibleCountAnalyzer : NumericAnalyzer
	{
		public override string Key
		{
			get { return "CollectiblesCount"; }
		}

		public override string Name
		{
			get { return "Count"; }
		}

		public override string FullName
		{
			get { return "Collectibles (Count)"; }
		}

		public override string ParentKey
		{
			get { return "Collectibles"; }
		}

		public override int GetValue(Character character)
		{
			return character.Collectibles.Count;
		}

		public override string[] GetValues()
		{
			return null;
		}
	}

	public class CollectibleNoCounterCountAnalyzer : NumericAnalyzer
	{
		public override string Key
		{
			get { return "CollectibleNoCounters"; }
		}

		public override string Name
		{
			get { return "Count (No Counters)"; }
		}

		public override string FullName
		{
			get { return "Collectibles (w/out Counters)"; }
		}

		public override string ParentKey
		{
			get { return "Collectibles"; }
		}

		public override int GetValue(Character character)
		{
			return character.Collectibles.Collectibles.Count(c => c.Counter == 0);
		}

		public override string[] GetValues()
		{
			return null;
		}
	}

	public class CollectibleCounterCountAnalyzer : NumericAnalyzer
	{
		public override string Key
		{
			get { return "CollectibleCounters"; }
		}

		public override string Name
		{
			get { return "Count (Counters)"; }
		}

		public override string FullName
		{
			get { return "Collectibles (w/ Counters)"; }
		}

		public override string ParentKey
		{
			get { return "Collectibles"; }
		}

		public override int GetValue(Character character)
		{
			return character.Collectibles.Collectibles.Count(c => c.Counter > 0);
		}

		public override string[] GetValues()
		{
			return null;
		}
	}
}
