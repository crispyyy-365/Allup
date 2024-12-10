using Allup.DAL;
using Allup.Models;
using Allup.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Allup.Controllers
{
	public class ShopController : Controller
	{
		public AppDbContext _context { get; set; }
		public ShopController(AppDbContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			return View();
		}
		public async Task<IActionResult> Details(int? id)
		{
			if (id is null || id == 0) return BadRequest();
			Product? product = await _context.Products
				.Include(p => p.ProductImages.OrderByDescending(pi => pi.IsPrimary))
				.Include(p => p.Category)
				.FirstOrDefaultAsync(p => p.Id == id);
			if (product == null) return NotFound();
			DetailVM detailVm = new()
			{
				Product = product,
				RelatedProducts = await _context.Products
				.Where(p => p.CategoryId == product.CategoryId && p.Id != id)
				.Include(p => p.ProductImages.Where(pi => pi != null))
				.Take(8)
				.ToListAsync(),
			};
			return View(detailVm);
		}
	}
}