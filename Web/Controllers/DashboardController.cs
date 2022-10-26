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
			var accounts = await _mediator.Send(new Commands.GetAccountsQuery());
			return View(new Models.DashboardModel
			{
				Accounts = accounts,
			});
		}
	}
}