using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HomeFinance.Controllers
{
	public class DashboardController : Controller
	{
		private readonly IMediator _mediator;

		public DashboardController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet("/")]
		public async Task<IActionResult> IndexAsync()
		{
			var accounts = _mediator.Send(new Commands.GetAccountsQuery());
			var categories = _mediator.Send(new Commands.CategorySummaryQuery
			{
				Start = DateOnly.FromDateTime(DateTime.Today.AddYears(-1)),
				End = DateOnly.FromDateTime(DateTime.Today),
			});

			await Task.WhenAll(accounts, categories);

			return View(new Models.DashboardModel
			{
				Accounts = accounts.Result,
				Categories = categories.Result,
			});
		}
	}
}