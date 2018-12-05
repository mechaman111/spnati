using System;

namespace Desktop
{
	[AttributeUsage(AttributeTargets.Class)]
	public class WorkspaceAttribute : Attribute
	{
		public Type RecordType { get; set; }

		public WorkspaceAttribute(Type recordType)
		{
			RecordType = recordType;
		}
	}
}
