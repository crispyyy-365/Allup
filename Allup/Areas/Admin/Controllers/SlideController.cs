﻿using Allup.DAL;
using Allup.Models;
using Allup.Utilities.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Allup.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class SlideController : Controller
	{
		private readonly AppDbContext _context;
		private readonly IWebHostEnvironment _env;

		public SlideController(AppDbContext context, IWebHostEnvironment env)
		{
			_context = context;
			_env = env;
		}
		public async Task<IActionResult> Index()
		{
			List<Slide> slides = await _context.Slides.ToListAsync();

			return View(slides);
		}
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(CreateSlideVM slideVM)
		{
			if (!ModelState.IsValid) return View();

			if (!slideVM.Photo.ValidateType("image"))
			{
				ModelState.AddModelError("Photo", "File type is incorrect");

				return View();
			}
			if (slideVM.Photo.ValidateSize(Utilities.Enums.FileSize.MB, 2))
			{
				ModelState.AddModelError("Photo", "File size must be less than 2mb");

				return View();
			}

			Slide slide = new Slide
			{
				Button = slideVM.Button,
				ProductName = slideVM.ProductName,
				Title = slideVM.Title,
				Subtitle = slideVM.SubTitle,
				Description = slideVM.Description,
				Order = slideVM.Order,
				Image = await slideVM.Photo.CreateFileAsync(_env.WebRootPath, "assets", "images", "website-images"),
				IsDeleted = false,
				CreatedAt = DateTime.Now,
			};

			await _context.Slides.AddAsync(slide);
			await _context.SaveChangesAsync();

			return RedirectToAction(nameof(Index));
		}
		public async Task<IActionResult> Update(int? id)
		{
			if (id == null || id < 1) return BadRequest();

			Slide? slide = await _context.Slides.FirstOrDefaultAsync(s => s.Id == id);

			if (slide == null) return NotFound();

			UpdateSlideVM updateSlideVM = new()
			{
				Button = slide.Button,
				ProductName = slide.ProductName,
				Title = slide.Title,
				Subtitle = slide.Subtitle,
				Description = slide.Description,
				Order = slide.Order,
				Image = slide.Image
			};

			return View(slide);
		}
		public async Task<IActionResult> Update(int? id, UpdateSlideVM slideVM)
		{

			if (!ModelState.IsValid) return View(slideVM);

			Slide existed = await _context.Slides.FirstOrDefaultAsync(s => s.Id == id);

			if (existed is null) return NotFound();

			if (slideVM.Photo is not null)
			{
				if (!slideVM.Photo.ValidateType("image/"))
				{
					ModelState.AddModelError(nameof(UpdateSlideVM.Photo), "Type is incorrect");
					return View(slideVM);
				}
				if (!slideVM.Photo.ValidateSize(Utilities.Enums.FileSize.MB, 2))
				{
					ModelState.AddModelError(nameof(UpdateSlideVM.Photo), "Slide is incorrect");
					return View(slideVM);
				}
				string fileName = await slideVM.Photo.CreateFileAsync(_env.WebRootPath, "assets", "images", "website-images");

				existed.Image.DeleteFile(_env.WebRootPath, "assets", "images", "website-images");

				existed.Image = fileName;
			}

			existed.Button = slideVM.Button;
			existed.ProductName = slideVM.ProductName;
			existed.Title = slideVM.Title;
			existed.Description = slideVM.Description;
			existed.Subtitle = slideVM.Subtitle;
			existed.Order = slideVM.Order;

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || id < 1) return BadRequest();

			Slide? slide = await _context.Slides.FirstOrDefaultAsync(s => s.Id == id);

			if (slide == null) return NotFound();

			slide.Image.DeleteFile(_env.WebRootPath, "assets", "images", "website-images");

			_context.Slides.Remove(slide);
			await _context.SaveChangesAsync();

			return RedirectToAction(nameof(Index));
		}
	}
}
