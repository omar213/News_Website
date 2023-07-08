using Microsoft.AspNetCore.Mvc;
using News_Website.Models;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace News_Website.Controllers
{
    public class HomeController : Controller
    {
        NewsContext db;
        public HomeController(NewsContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {

            var result = db.Categories.ToList();
            return View(result);
        }

        public IActionResult Contact()
        {

            return View();
        }


        [HttpPost]
        public IActionResult SaveContact(Contactus model)
        {
            
                db.Contacts.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");




           

        }
        [Authorize]
        public IActionResult News(int id)
        {
            Category c = db.Categories.Find(id);
            ViewBag.cat = c.Description;

            var result = db.News.Where(y => y.CategoryId == id).OrderByDescending(x => x.Date).ToList();
            return View(result);
        }

        public IActionResult DeleteNews(int id)
        {
            var news = db.News.Find(id);
            db.News.Remove(news);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult Teammember()
        {

            return View(db.TeamMembers.ToList() );
        }
    }
}