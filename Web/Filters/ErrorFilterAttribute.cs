using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HomeFinance.Filters
{
	public class ErrorFilterAttribute : ExceptionFilterAttribute
	{
		public override void OnException(ExceptionContext context)
		{
			HandleException(context);

			base.OnException(context);
		}

		private void HandleException(ExceptionContext context)
		{
			if (context.Exception is ValidationException)
			{
				HandleValidationException(context);
				return;
			}
			else if (context.Exception is NotFoundException)
			{
				HandleNotFoundException(context);
				return;
			}

			if (!context.ModelState.IsValid)
				HandleInvalidModelStateException(context);
		}

		private void HandleValidationException(ExceptionContext context)
		{
			var exception = (ValidationException)context.Exception;

			var details = new ValidationProblemDetails(exception.Errors)
			{
				Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
			};

			context.Result = new BadRequestObjectResult(details);

			context.ExceptionHandled = true;
		}

		private void HandleInvalidModelStateException(ExceptionContext context)
		{
			var details = new ValidationProblemDetails(context.ModelState)
			{
				Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
			};

			context.Result = new BadRequestObjectResult(details);

			context.ExceptionHandled = true;
		}

		private void HandleNotFoundException(ExceptionContext context)
		{
			var exception = (NotFoundException)context.Exception;

			var details = new ProblemDetails()
			{
				Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
				Title = "The specified resource was not found.",
				Detail = exception.Message
			};

			context.Result = new NotFoundObjectResult(details);

			context.ExceptionHandled = true;
		}
	}
}