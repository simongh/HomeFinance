using Microsoft.AspNetCore.Mvc;

namespace HomeFinance.Controllers
{
	public class CategoryApiController : ApiControllerBase
	{
		[HttpGet("")]
		public async Task<IActionResult> GetAllAsync()
		{
			var results = await Mediator.Send(new Categories.Commands.GetAllCategoriesQuery());
			return Ok(results);
		}

		[HttpPost("")]
		public async Task<IActionResult> CreateAsync(Categories.Commands.UpdateCategoryCommand command)
		{
			command.Id = null;
			var result = await Mediator.Send(command);

			return StatusCode(StatusCodes.Status201Created, result);
		}

		[HttpPut("{id:int}")]
		public async Task<IActionResult> UpdateAsync(Categories.Commands.UpdateCategoryCommand command)
		{
			var result = await Mediator.Send(command);

			return Ok(result);
		}

		[HttpDelete("{id:int}")]
		public async Task<IActionResult> DeleteAsync(Categories.Commands.DeleteCategoryCommand command)
		{
			await Mediator.Send(command);

			return NoContent();
		}
	}
}