using System;
using System.Collections.Generic;

namespace HomeFinance
{
	public class ValidationException : Exception
	{
		public IEnumerable<string> Errors { get; init; }
	}
}