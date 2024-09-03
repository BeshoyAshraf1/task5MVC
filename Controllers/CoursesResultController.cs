using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using task3.Models;

namespace task5.Controllers
{
    public class CoursesResultController : Controller
    {
        public IActionResult Index()
        {
            var context = new ApplicationDbContext();

            var CoursesResult = context.Courseresults
           .Include(i => i.Courses)
           .Include(i => i.trainees)
           .ToList();
            return View(CoursesResult);
        }
        public IActionResult Details(int id)
        {
            var context = new ApplicationDbContext();

            var CoursesResult = context.Courseresults
           .Include(i => i.Courses)
           .Include(i => i.trainees)
           .FirstOrDefault(x => x.Id == id);

            return View(CoursesResult);
        }




        public IActionResult DeleteCourseResult(int id)
        {
            var context = new ApplicationDbContext();
            var CoursesResult = context.Courseresults.Include(d => d.Courses).Include(d => d.trainees).FirstOrDefault(d => d.Id == id);

            if (CoursesResult == null)
            {
                return NotFound();
            }

            context.Courseresults.Remove(CoursesResult);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var context = new ApplicationDbContext();

            var Courseresults = context.Courseresults.FirstOrDefault(d => d.Id == id);

            if (Courseresults == null)
            {
                return NotFound();
            }

            return View(Courseresults);
        }


        [HttpPost]
        public IActionResult Edit(Courseresult model)
        {
            var context = new ApplicationDbContext();



            var Courseresults = context.Courseresults.FirstOrDefault(d => d.Id == model.Id);

            if (Courseresults == null)
            {
                return NotFound();
            }
            Courseresults.Degree = model.Degree;



            context.SaveChanges();

            return RedirectToAction("Index");

        }





        [HttpGet]
        public IActionResult Create()
        {
            var context = new ApplicationDbContext();

            ViewBag.Courses = new SelectList(context.Courses.ToList(), "Id", "Name");
            ViewBag.Trainees = new SelectList(context.trainees.ToList(), "Id", "Name");

            return View();

        }
        [HttpPost]
        public IActionResult CreateCourseResult(Courseresult model)
        {
            var context = new ApplicationDbContext();


            context.Courseresults.Add(model);
            context.SaveChanges();

            return RedirectToAction("Index");



        }
    }
}
