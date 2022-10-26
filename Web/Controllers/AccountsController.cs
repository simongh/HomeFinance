using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HomeFinance.Controllers
{
	public class AccountsController : Controller
	{
		private readonly IMediator _mediator;

		public AccountsController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet("{controller}/add")]
		public IActionResult GetAdd()
		{
			return View("Update", new ResultModels.AccountResult());
		}

		[HttpGet("{controller}/{id}")]
		public async Task<IActionResult> GetAsync([FromRoute] Commands.GetAccountsQuery query)
		{
			var result = await _mediator.Send(query);
			return View("Update", result.First());
		}

		[HttpPost("{controller}/update")]
		public async Task<IActionResult> UpdateAsync(Commands.UpdateAccountCommand command)
		{
			var result = await _mediator.Send(command);

			return View("Update", result);
		}

		[HttpGet("{controller}/delete/{id}")]
		public async Task<IActionResult> DeleteAsync([FromRoute] Commands.DeleteAccountCommand command)
		{
			await _mediator.Send(command);

			return RedirectToAction("index", "dashboard");
		}
	}
}