using Microsoft.AspNetCore.Mvc;

namespace HomeFinance.Controllers
{
	public class AccountApiController : ApiControllerBase
	{
		[HttpGet("")]
		public async Task<IActionResult> GetAllAsync()
		{
			var results = await Mediator.Send(new Accounts.Commands.GetAccountsQuery());
			return Ok(results);
		}

		[HttpPost("")]
		public async Task<IActionResult> CreateAsync(Accounts.Commands.UpdateAccountCommand command)
		{
			command.Id = null;
			var result = await Mediator.Send(command);

			return StatusCode(StatusCodes.Status201Created, result);
		}

		[HttpPut("{id:int}")]
		public async Task<IActionResult> UpdateAsync(Accounts.Commands.UpdateAccountCommand command)
		{
			var result = await Mediator.Send(command);

			return Ok(result);
		}

		[HttpDelete("{id:int}")]
		public async Task<IActionResult> DeleteAsync(Accounts.Commands.DeleteAccountCommand command)
		{
			await Mediator.Send(command);

			return NoContent();
		}
	}
}