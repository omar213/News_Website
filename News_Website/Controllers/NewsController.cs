﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using News_Website.Models;

using Microsoft.AspNetCore.Hosting;

namespace News_Website.Controllers
{
    public class NewsController : Controller
    {
        private readonly NewsContext _context;



        public NewsController(NewsContext context, IWebHostEnvironment hostEnv)
        {
            _context = context;
            host = hostEnv;


        }

        // GET: News
        public async Task<IActionResult> Index()
        {
            var newsContext = _context.News.Include(n => n.Category);
            return View(await newsContext.ToListAsync());
        }
        private IWebHostEnvironment host;

       

        void uploadphoto(News model)
        {
            if (model.File != null)
            {
                string uploadsfolder = Path.Combine(host.WebRootPath, "images/News");
                string uniqueFileName =  Guid.NewGuid() + ".jpg";
                string filepath = Path.Combine(uploadsfolder, uniqueFileName);

                using (var filestream = new FileStream(filepath, FileMode.Create))
                {

                    model.File.CopyTo(filestream);
                }
                model.Image = uniqueFileName;


            }
        }
        // GET: News/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.News == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .Include(n => n.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // GET: News/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        // POST: News/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( News news)
        {
            if (ModelState.IsValid)
            {
                uploadphoto(news);
                _context.Add(news);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", news.CategoryId);
            return View(news);
        }

        // GET: News/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.News == null)
            {
                return NotFound();
            }

            var news = await _context.News.FindAsync(id);
            if (news == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", news.CategoryId);
            return View(news);
        }

        // POST: News/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  News news)
        {
            if (id != news.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    uploadphotoedit(news);
                    _context.Update(news);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                   // if (!NewsExists(news.Id))
                   ///// {
                   //     return NotFound();
                    //}
                    //
                    //
                      //  throw;
                    //}
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", news.CategoryId);
            return View(news);
        }

         void uploadphotoedit(News model)
        {
            if (model.File != null)
            {
                string uploadsfolder = Path.Combine(host.WebRootPath, "images/News");
                string uniqueFileName = Guid.NewGuid() + ".jpg";
                string filepath = Path.Combine(uploadsfolder, uniqueFileName);

                using (var filestream = new FileStream(filepath, FileMode.Create))
                {

                    model.File.CopyTo(filestream);
                }
                model.Image = uniqueFileName;


            }
        }

        // GET: News/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.News == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .Include(n => n.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.News == null)
            {
                return Problem("Entity set 'NewsContext.News'  is null.");
            }
            var news = await _context.News.FindAsync(id);
            if (news != null)
            {
                _context.News.Remove(news);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewsExists(int id)
        {
          return (_context.News?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
