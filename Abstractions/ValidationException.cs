using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HomeFinance
{
	public class ValidationException : Exception
	{
		public IDictionary<string, string[]> Errors { get; init; }

		public ValidationException()
			: base("One or more validation errors have occurred")
		{ }

		public ValidationException(IEnumerable<ValidationFailure> failures)
			: this()
		{
			Errors = failures
				.GroupBy(e => e.PropertyName, e => e.ErrorMessage)
				.ToDictionary(e => e.Key, e => e.ToArray());
		}

		public ValidationException(string property, string error)
			: this()
		{
			Errors = new Dictionary<string, string[]>
			{
				{ property, new[] { error } }
			};
		}
	}
}