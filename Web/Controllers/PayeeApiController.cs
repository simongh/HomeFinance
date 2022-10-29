using Microsoft.AspNetCore.Mvc;

namespace HomeFinance.Controllers
{
	public class PayeeApiController : ApiControllerBase
	{
		[HttpGet("")]
		public async Task<IActionResult> GetAllAsync()
		{
			var results = await Mediator.Send(new Commands.GetAllPayeesQuery());
			return Ok(results);
		}

		[HttpPostAttribute("")]
		public async Task<IActionResult> CreateAsync(Commands.UpdatePayeeCommand command)
		{
			command.Id = null;
			var result = await Mediator.Send(command);

			return StatusCode(StatusCodes.Status201Created, result);
		}

		[HttpPut("{id:int}")]
		public async Task<IActionResult> UpdateAsync(Commands.UpdatePayeeCommand command)
		{
			var result = await Mediator.Send(command);

			return Ok(result);
		}

		[HttpDelete("{id:int}")]
		public async Task<IActionResult> DeleteAsync(Commands.DeletePayeeCommand command)
		{
			await Mediator.Send(command);

			return NoContent();
		}
	}
}