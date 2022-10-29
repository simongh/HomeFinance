using Microsoft.AspNetCore.Mvc;

namespace HomeFinance.Controllers
{
	public class TransactionApiController : ApiControllerBase
	{
		[HttpGet("")]
		public async Task<IActionResult> GetAllAsync(Transactions.Commands.GetTransactionsQuery query)
		{
			var results = await Mediator.Send(query);
			return Ok(results);
		}

		[HttpPostAttribute("")]
		public async Task<IActionResult> CreateAsync(Transactions.Commands.UpdateTransactionCommand command)
		{
			command.Id = null;
			var result = await Mediator.Send(command);

			return StatusCode(StatusCodes.Status201Created, result);
		}

		[HttpPut("{id:int}")]
		public async Task<IActionResult> UpdateAsync(Transactions.Commands.UpdateTransactionCommand command)
		{
			var result = await Mediator.Send(command);

			return Ok(result);
		}

		[HttpDelete("{id:int}")]
		public async Task<IActionResult> DeleteAsync(Transactions.Commands.DeleteTransactionCommand command)
		{
			await Mediator.Send(command);

			return NoContent();
		}
	}
}