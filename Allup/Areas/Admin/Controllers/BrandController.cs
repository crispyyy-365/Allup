using Allup.Areas.Admin.ViewModels.Categories;
using Allup.DAL;
using Allup.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Allup.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class BrandController : Controller
	{
		public readonly AppDbContext _context;

		public BrandController(AppDbContext context)
		{
			_context = context;
		}
		public async Task<IActionResult> Index()
		{
			var brands = await _context.Brands
				.Where(c => !c.IsDeleted)
				.Include(c => c.Products)
				.Select(c => new GetBrandAdminVM
				{
					Id = c.Id,
					Name = c.Name,
					ProductCount = c.Products.Count
				}).ToListAsync();

			return View(brands);
		}
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Create(Brand brand)
		{
			if (!ModelState.IsValid) return View();

			bool result = await _context.Brands.AnyAsync(b => b.Name.Trim() == brand.Name.Trim());

			if (result)
			{
				ModelState.AddModelError("Name", "Brand already exists");
				return View();
			}

			brand.CreatedAt = DateTime.Now;
			await _context.SaveChangesAsync();

			return RedirectToAction(nameof(Index));
		}
		public async Task<IActionResult> Update(int? id)
		{
			if (id == null || id < 1) return BadRequest();

			Brand? brand = await _context.Brands.FirstOrDefaultAsync(b => b.Id == id);

			if (brand is null) return NotFound();

			return View(brand);
		}
		[HttpPost]
		public async Task<IActionResult> Update(int? id, Brand brand)
		{
			if (id == null || id < 1) return BadRequest();

			Brand? existed = await _context.Brands.FirstOrDefaultAsync(c => c.Id == id);

			if (existed is null) return NotFound();

			if (!ModelState.IsValid)
			{
				return View();
			}

			bool result = await _context.Categories.AnyAsync(c => c.Name.Trim() == category.Name.Trim() && c.Id != id);

			if (result)
			{
				ModelState.AddModelError(nameof(Category.Name), "Category already exixts");
				return View();
			}

			existed.Name = category.Name;
			await _context.SaveChangesAsync();

			return RedirectToAction(nameof(Index));

		}

		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || id < 1) return BadRequest();

			Category? category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

			if (category is null) return NotFound();


			category.IsDeleted = true;

			await _context.SaveChangesAsync();

			return RedirectToAction(nameof(Index));
		}

	}
}
}
