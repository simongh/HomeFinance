using Microsoft.AspNetCore.Mvc;

namespace HomeFinance.Controllers
{
	public class HomeController : Controller
	{
		[HttpGet("/")]
		public IActionResult Index()
		{
			return View();
		}
	}
}