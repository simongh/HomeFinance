using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HomeFinance.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public abstract class ApiControllerBase : ControllerBase
	{
		private ISender _mediator = null!;

		protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
	}
}