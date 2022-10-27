using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HomeFinance.Controllers
{
	public class CategoryController : Controller
	{
		private readonly IMediator _mediator;
		private readonly IMapper _mapper;

		public CategoryController(
			IMediator mediator,
			IMapper mapper)
		{
			_mediator = mediator;
			_mapper = mapper;
		}

		[HttpGet("{controller}")]
		public async Task<IActionResult> IndexAsync()
		{
			var result = await _mediator.Send(new Commands.GetAllCategoriesQuery());

			return View(result);
		}

		[HttpPost("{controller}")]
		public async Task<IActionResult> AddAsync(Commands.UpdateCategoryCommand command)
		{
			await _mediator.Send(command);

			return RedirectToAction("Index");
		}

		[HttpGet("{controller}/{id}")]
		public async Task<IActionResult> GetAsync([FromRoute] Commands.GetCategoryCommand command)
		{
			var result = await _mediator.Send(command);

			return View(result);
		}

		[HttpGet("{controller}/delete/{id}")]
		public async Task<IActionResult> DeleteAsync([FromRoute] Commands.DeleteCategoryCommand command)
		{
			await _mediator.Send(command);

			return RedirectToAction("Index");
		}
	}
}