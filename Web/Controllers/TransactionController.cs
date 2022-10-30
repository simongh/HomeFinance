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
		public async Task<IActionResult> IndexAsync(Transactions.Commands.GetTransactionsQuery query)
		{
			if (query?.AccountId == null)
				return RedirectToAction(nameof(DashboardController));

			if (query.StartDate == null)
				query.StartDate = DateOnly.FromDateTime(DateTime.Today.AddYears(-1));

			if (query.EndDate == null)
				query.EndDate = DateOnly.FromDateTime(DateTime.Today);

			var items = _mediator.Send(query);
			var categories = _mediator.Send(new Categories.Commands.GetAllCategoriesQuery());
			var payees = _mediator.Send(new Payees.Commands.GetAllPayeesQuery());

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
		public async Task<IActionResult> AddAsync(Transactions.Commands.UpdateTransactionCommand command)
		{
			await _mediator.Send(command);

			return RedirectToAction("index", new
			{
				AccountId = command.Account,
			});
		}

		[HttpGet("{controller}/edit/{id:int}")]
		public async Task<IActionResult> EditAsync([FromRoute] Transactions.Commands.GetTransactionCommand command)
		{
			var result = await _mediator.Send(command);

			return View(result);
		}

		[HttpPost("{controller}/edit/{id:int}")]
		public async Task<IActionResult> UpdateAsync(Transactions.Commands.UpdateTransactionCommand command)
		{
			var result = await _mediator.Send(command);

			return View(result);
		}

		[HttpGet("{controller}/delete/{id:int}")]
		public async Task<IActionResult> DeleteAsync(Transactions.Commands.DeleteTransactionCommand command)
		{
			await _mediator.Send(command);
			return RedirectToAction(nameof(IndexAsync));
		}
	}
}