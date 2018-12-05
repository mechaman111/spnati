using System;

namespace KisekaeImporter
{
	/// <summary>
	/// Marks minimum version requirements for components/subcodes/pieces
	/// </summary>
	public class KisekaeVersionAttribute : Attribute
	{
		public int MinimumVersion;
	}
}
