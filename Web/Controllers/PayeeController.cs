using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HomeFinance.Controllers
{
	public class PayeeController : Controller
	{
		private readonly IMediator _mediator;

		public PayeeController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet("{controller}")]
		public async Task<IActionResult> IndexAsync()
		{
			var result = await _mediator.Send(new Payees.Commands.GetAllPayeesQuery());

			return View(result);
		}

		[HttpPost("{controller}")]
		public async Task<IActionResult> CreateAsync(Payees.Commands.UpdatePayeeCommand command)
		{
			await _mediator.Send(command);

			return RedirectToAction("index");
		}

		[HttpGet("{controller}/delete/{id}")]
		public async Task<IActionResult> DeleteAsync([FromRoute] Payees.Commands.DeletePayeeCommand command)
		{
			await _mediator.Send(command);

			return RedirectToAction("Index");
		}
	}
}