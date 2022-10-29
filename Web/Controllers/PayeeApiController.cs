using Microsoft.AspNetCore.Mvc;

namespace HomeFinance.Controllers
{
	public class PayeeApiController : ApiControllerBase
	{
		[HttpGet("")]
		public async Task<IActionResult> GetAllAsync()
		{
			var results = await Mediator.Send(new Payees.Commands.GetAllPayeesQuery());
			return Ok(results);
		}

		[HttpPostAttribute("")]
		public async Task<IActionResult> CreateAsync(Payees.Commands.UpdatePayeeCommand command)
		{
			command.Id = null;
			var result = await Mediator.Send(command);

			return StatusCode(StatusCodes.Status201Created, result);
		}

		[HttpPut("{id:int}")]
		public async Task<IActionResult> UpdateAsync(Payees.Commands.UpdatePayeeCommand command)
		{
			var result = await Mediator.Send(command);

			return Ok(result);
		}

		[HttpDelete("{id:int}")]
		public async Task<IActionResult> DeleteAsync(Payees.Commands.DeletePayeeCommand command)
		{
			await Mediator.Send(command);

			return NoContent();
		}
	}
}