using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HomeFinance.Controllers
{
	public class DashboardController : Controller
	{
		private readonly IMediator _mediator;
		private readonly Services.ISystemClock _systemClock;

		public DashboardController(IMediator mediator, Services.ISystemClock systemClock)
		{
			_mediator = mediator;
			_systemClock = systemClock;
		}

		[HttpGet("/")]
		public async Task<IActionResult> IndexAsync()
		{
			var accounts = _mediator.Send(new Accounts.Commands.GetAccountsQuery());
			var categories = _mediator.Send(new Categories.Commands.CategorySummaryQuery
			{
				Start = _systemClock.Today.AddYears(-1),
				End = _systemClock.Today,
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