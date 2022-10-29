using System;

namespace HomeFinance.Services
{
	internal class SystemClock : ISystemClock
	{
		public DateOnly Today => DateOnly.FromDateTime(DateTime.Today);
	}
}