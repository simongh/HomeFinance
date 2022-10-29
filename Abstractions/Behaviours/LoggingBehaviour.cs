using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace HomeFinance.Behaviours
{
	internal class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
	{
		private readonly ILogger _logger;

		public LoggingBehaviour(
			ILogger logger)
		{
			_logger = logger;
		}

		public Task Process(TRequest request, CancellationToken cancellationToken)
		{
			_logger.LogInformation("Request: {@Request}", request);

			return Task.CompletedTask;
		}
	}
}