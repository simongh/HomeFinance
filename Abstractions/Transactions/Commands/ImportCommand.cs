using MediatR;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace HomeFinance.Transactions.Commands
{
	public class ImportCommand : IRequest
	{
		public int Account { get; set; }

		public Stream Content { get; set; } = null!;
	}

	internal class ImportCommandHandler : IRequestHandler<ImportCommand, Unit>
	{
		public ImportCommandHandler()
		{ }

		public async Task<Unit> Handle(ImportCommand request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}