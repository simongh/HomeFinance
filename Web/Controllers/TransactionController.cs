using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HomeFinance.Controllers
{
	public class TransactionController : Controller
	{
		private readonly IMediator _mediator;

		public TransactionController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet("transactions")]
		public async Task<IActionResult> IndexAsync(Commands.GetTransactionsQuery query)
		{
			if (query?.AccountId == null)
				return RedirectToAction(nameof(DashboardController));

			if (query.StartDate == null)
				query.StartDate = DateOnly.FromDateTime(DateTime.Today.AddYears(-1));

			if (query.EndDate == null)
				query.EndDate = DateOnly.FromDateTime(DateTime.Today);

			var items = _mediator.Send(query);
			var categories = _mediator.Send(new Commands.GetAllCategoriesQuery());
			var payees = _mediator.Send(new Commands.GetAllPayeesQuery());

			await Task.WhenAll(items, categories, payees);

			return View(new Models.TransactionsModel
			{
				Query = query,
				Results = items.Result,
				Categories = categories.Result,
				Payees = payees.Result,
			});
		}
	}
}