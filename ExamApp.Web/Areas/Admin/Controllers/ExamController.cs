using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExamApp.DAL;
using ExamApp.Entity;
using ExamApp.Web.Areas.Admin.ViewModels;
using ExamApp.Web.Areas.Admin.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace ExamApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles ="admin")]
    public class ExamController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public ExamController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var statusMessageHelper = new StatusMessageHelper();
            ViewBag.ResultStatus = statusMessageHelper.GetStatusMessage(HttpContext);
            return View(await _context.Exams.ToListAsync());
        }     
                
        public IActionResult Create()
        {
            var model = new Exam();


            var articlesResult = HtmlAgilityHelper.GetLastFiveArticles("https://www.wired.com/most-recent/");
            if (articlesResult.IsSuccess)
            {
                ViewBag.Articles = articlesResult.Articles;
                //ViewBag.JsonStringArticlesContents = JsonSerializer.Serialize(articlesResult.Articles.Select(a=>a.Content).ToList());

                //first article will be the default
                model.TextTitle = articlesResult.Articles.FirstOrDefault().Title;
                model.Text = articlesResult.Articles.FirstOrDefault().Content;
            }
            else
            {
                ViewBag.ResultStatus = new ResultStatusViewModel
                {
                    Message = articlesResult.ErrorMessage,
                    Tag = Tag.Danger
                };

            }           

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TextTitle,Text,Questions")] Exam exam)
        {
            if (!ModelState.IsValid)
            {
              
                var articlesResult = HtmlAgilityHelper.GetLastFiveArticles("https://www.wired.com/most-recent/");
                if (articlesResult.IsSuccess)
                {
                    ViewBag.Articles = articlesResult.Articles;
                    //ViewBag.JsonStringArticlesContents = JsonSerializer.Serialize(articlesResult.Articles.Select(a=>a.Content).ToList());

                    //first article will be the default
                    exam.TextTitle = articlesResult.Articles.FirstOrDefault().Title;
                    exam.Text = articlesResult.Articles.FirstOrDefault().Content;
                }
                ViewBag.ResultStatus = new ResultStatusViewModel
                {
                    Message = "Sınav oluşturma başarısız! Lütfen bütün alanları doldurduğunuzdan emin olun.",
                    Tag = Tag.Danger
                };
                return View(exam);
            }
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    exam.CreatedById = _userManager.GetUserId(User);
                }               
                exam.CreatedAt = DateTime.UtcNow;
                _context.Add(exam);
                await _context.SaveChangesAsync();

                HttpContext.Session.SetString("message", "Sınav başarıyla oluşturuldu.");
                HttpContext.Session.SetString("tag", Tag.Success.ToString());   
                
                return RedirectToAction(nameof(Index));
            }
            catch
            {
             
                var articlesResult = HtmlAgilityHelper.GetLastFiveArticles("https://www.wired.com/most-recent/");
                if (articlesResult.IsSuccess)
                {
                    ViewBag.Articles = articlesResult.Articles;
                    //ViewBag.JsonStringArticlesContents = JsonSerializer.Serialize(articlesResult.Articles.Select(a=>a.Content).ToList());

                    //first article will be the default
                    exam.TextTitle = articlesResult.Articles.FirstOrDefault().Title;
                    exam.Text = articlesResult.Articles.FirstOrDefault().Content;
                }
                ViewBag.ResultStatus = new ResultStatusViewModel
                {
                    Message = "Sınav oluşturulurken bir hata oluştu",
                    Tag = Tag.Danger
                };
                return View(exam);
            }

        }
              
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exam = await _context.Exams
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exam == null)
            {
                return NotFound();
            }

            return View(exam);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var exam = await _context.Exams.FindAsync(id);
            _context.Exams.Remove(exam);
            await _context.SaveChangesAsync();
            HttpContext.Session.SetString("message", "Sınav başarıyla silindi.");
            HttpContext.Session.SetString("tag", Tag.Success.ToString());
            return RedirectToAction(nameof(Index));
        }

        private bool ExamExists(int id)
        {
            return _context.Exams.Any(e => e.Id == id);
        }
    }
}
