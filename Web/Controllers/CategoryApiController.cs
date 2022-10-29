using Microsoft.AspNetCore.Mvc;

namespace HomeFinance.Controllers
{
	public class CategoryApiController : ApiControllerBase
	{
		[HttpGet("")]
		public async Task<IActionResult> GetAllAsync()
		{
			var results = await Mediator.Send(new Commands.GetAllCategoriesQuery());
			return Ok(results);
		}

		[HttpPost("")]
		public async Task<IActionResult> CreateAsync(Commands.UpdateCategoryCommand command)
		{
			command.Id = null;
			var result = await Mediator.Send(command);

			return StatusCode(StatusCodes.Status201Created, result);
		}

		[HttpPut("{id:int}")]
		public async Task<IActionResult> UpdateAsync(Commands.UpdateCategoryCommand command)
		{
			var result = await Mediator.Send(command);

			return Ok(result);
		}

		[HttpDelete("{id:int}")]
		public async Task<IActionResult> DeleteAsync(Commands.DeleteCategoryCommand command)
		{
			await Mediator.Send(command);

			return NoContent();
		}
	}
}