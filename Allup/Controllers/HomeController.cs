using Allup.DAL;
using Allup.Models;
using Allup.ViewModels;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Allup.Controllers
{
	public class HomeController : Controller
	{
		public readonly AppDbContext _context;
		public HomeController(AppDbContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			HomeVM homeVM = new()
			{
				Slides = _context.Slides
				.OrderBy(s => s.Order)
				.Where(s => s.IsDeleted != false)
				.ToList(),

				Categories = _context.Categories
				.Take(2)
				.Where(s => s.IsDeleted != false)
				.ToList(),
			};
			return View(homeVM);
		}
	}
}