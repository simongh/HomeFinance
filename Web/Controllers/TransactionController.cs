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

		[HttpPost("{controller}/add")]
		public async Task<IActionResult> AddAsync(Commands.UpdateTransactionCommand command)
		{
			await _mediator.Send(command);

			return RedirectToAction(nameof(IndexAsync));
		}

		[HttpGet("{controller}/edit/{id:int}")]
		public async Task<IActionResult> EditAsync([FromRoute] Commands.GetTransactionCommand command)
		{
			var result = await _mediator.Send(command);

			return View(result);
		}

		[HttpPost("{controller}/edit/{id:int}")]
		public async Task<IActionResult> UpdateAsync(Commands.UpdateTransactionCommand command)
		{
			var result = await _mediator.Send(command);

			return View(result);
		}

		[HttpGet("{controller}/delete/{id:int")]
		public async Task<IActionResult> DeleteAsync(Commands.DeleteTransactionCommand command)
		{
			await _mediator.Send(command);
			return RedirectToAction(nameof(IndexAsync));
		}
	}
}