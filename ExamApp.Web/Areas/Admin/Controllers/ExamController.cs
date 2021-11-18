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

namespace ExamApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ExamController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExamController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Exam
        public async Task<IActionResult> Index()
        {
            return View(await _context.Exams.ToListAsync());
        }

        // GET: Admin/Exam/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Admin/Exam/Create
        public IActionResult Create()
        {
            return View(new Exam());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TextTitle,Text,Questions")] Exam exam)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = _context.Users.FirstOrDefault(a => a.UserName == User.Identity.Name);
                    if (user != null)
                    {
                        exam.CreatedById = user.Id;
                    }
                    exam.CreatedAt = DateTime.UtcNow;
                    _context.Add(exam);
                    await _context.SaveChangesAsync();
                    TempData["ResultStatus"] = new ResultStatusViewModel
                    {
                        Message="Sınav başarıyla oluşturuldu",
                        Tag=Tag.Success
                    };
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                   ViewBag.ResultStatus = new ResultStatusViewModel
                    {
                        Message = "Sınav oluşturulurken bir hata oluştu",
                        Tag = Tag.Danger
                    };
                    return View(exam);
                }

            }

            ViewBag.ResultStatus = new ResultStatusViewModel
            {
                Message = "Sınav oluşturma başarısız! Lütfen bütün alanları doldurduğunuzdan emin olun.",
                Tag = Tag.Danger
            };
            return View(exam);
        }

        // GET: Admin/Exam/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exam = await _context.Exams.FindAsync(id);
            if (exam == null)
            {
                return NotFound();
            }
            return View(exam);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TextTitle,Text,CreatedAt,ModifiedAt")] Exam exam)
        {
            if (id != exam.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exam);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExamExists(exam.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(exam);
        }

        // GET: Admin/Exam/Delete/5
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

        // POST: Admin/Exam/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var exam = await _context.Exams.FindAsync(id);
            _context.Exams.Remove(exam);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExamExists(int id)
        {
            return _context.Exams.Any(e => e.Id == id);
        }
    }
}
