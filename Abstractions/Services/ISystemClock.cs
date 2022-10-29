using System;

namespace HomeFinance.Services
{
	public interface ISystemClock
	{
		public DateOnly Today { get; }
	}
}