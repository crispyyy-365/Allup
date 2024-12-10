using Microsoft.AspNetCore.Mvc;

namespace Allup.Areas.Admin.Controllers
{
	public class ProductController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
