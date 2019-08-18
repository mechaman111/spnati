using System;

namespace Desktop
{
	public interface IDirtiable
	{
		event EventHandler<bool> OnDirtyChanged;
	}
}
