using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using task3.Models;

namespace task5.Controllers
{
    public class CoursesController : Controller
    {
        public IActionResult Index()
        {
            var context = new ApplicationDbContext();

            var Courses = context.Courses
           .Include(i => i.Instractors)
           .Include(i=>i.Courseresults)
           .ToList();
            return View(Courses);
        }
        public IActionResult Details(int id)
        {
            var context = new ApplicationDbContext();

            var Courses = context.Courses
           .Include(i => i.Instractors)
           .Include(i => i.Courseresults)
           .FirstOrDefault(x => x.Id == id);

            return View(Courses);
        }




        public IActionResult DeleteCourse(int id)
        {
            var context = new ApplicationDbContext();
            var Courses = context.Courses.Include(d => d.Instractors).Include(d => d.Courseresults).FirstOrDefault(d => d.Id == id);

            if (Courses == null)
            {
                return NotFound();
            }

            context.Courses.Remove(Courses);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var context = new ApplicationDbContext();

            var Course = context.Courses.FirstOrDefault(d => d.Id == id);

            if (Course == null)
            {
                return NotFound();
            }

            return View(Course);
        }


        [HttpPost]
        public IActionResult Edit(Course model)
        {
            var context = new ApplicationDbContext();



            var Course = context.Courses.FirstOrDefault(d => d.Id == model.Id);

            if (Course == null)
            {
                return NotFound();
            }

            Course.Name = model.Name;
            Course.Degree = model.Degree;
            Course.MinDegree = model.Degree;

            context.SaveChanges();

            return RedirectToAction("Index");

        }





        [HttpGet]
        public IActionResult Create()
        {

            return View();

        }
        [HttpPost]
        public IActionResult CreateCourse(Course model)
        {
            var context = new ApplicationDbContext();
            if (!ModelState.IsValid) 
            {
              
                return View("Create", model);
            }

            context.Courses.Add(model);
            context.SaveChanges();

            return RedirectToAction("Index");



        }



        [HttpPost]
        public IActionResult ValidateMinDegree(double minDegree, double degree)
        {
            if (minDegree > degree)
            {
                return Json("Minimum Degree cannot be greater than the Degree.");
            }

            return Json(true); 
        }
    }
}
