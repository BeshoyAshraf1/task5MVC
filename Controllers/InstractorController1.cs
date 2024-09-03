using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using task3.Models;

namespace task3.Controllers
{
    public class InstractorController1 : Controller
    {
        public IActionResult Index()
        {
            var context = new ApplicationDbContext();

            var instructors = context.Instractors
           .Include(i => i.Courses)
           .Include(i => i.Depratments)
           .ToList();
            return View(instructors); 
        }


        public IActionResult Details(int id)
        {
            var context = new ApplicationDbContext();

            var instractors = context.Instractors
           .Include(i => i.Courses)
           .Include(i => i.Depratments)
           .FirstOrDefault(x => x.Id == id);

            Console.WriteLine(instractors.Courses.Name);
            return View(instractors);
        }



        public IActionResult DeleteInstractor(int id)
        {
            var context = new ApplicationDbContext();
            var Instractor = context.Instractors.Include(d => d.Depratments).Include(d=>d.Courses).FirstOrDefault(d => d.Id == id);

            if (Instractor == null)
            {
                return NotFound();
            }

            context.Instractors.Remove(Instractor);
            context.SaveChanges();

            return RedirectToAction("Index");
        }



        [HttpGet]
        public IActionResult Edit(int id)
        {
            var context = new ApplicationDbContext();

            var instractor = context.Instractors.FirstOrDefault(d => d.Id == id);

            if (instractor == null)
            {
                return NotFound();
            }

            return View(instractor);
        }

        [HttpPost]
        public IActionResult Edit(Instractor model)
        {
            var context = new ApplicationDbContext();



            var instractor = context.Instractors.FirstOrDefault(d => d.Id == model.Id);

            if (instractor == null)
            {
                return NotFound();
            }

            instractor.Name = model.Name;
            instractor.address = model.address;
            instractor.Salary = model.Salary;
            instractor.image = model.image;

            context.SaveChanges();

            return RedirectToAction("Index");

        }



        [HttpGet]
        public IActionResult CreateInstractor()
        {
            var context = new ApplicationDbContext();

            var departments = context.Depratments.ToList();
            var courses = context.Courses.ToList();

            ViewBag.Depratments = new SelectList(departments, "id", "name"); 
            ViewBag.Courses = new SelectList(courses, "Id", "Name"); 

            return View("Create");

        }
        [HttpPost]
        public IActionResult CreateInstractor(Instractor model)
        {
            var context = new ApplicationDbContext();

          
                context.Instractors.Add(model);
                context.SaveChanges(); 

                return RedirectToAction("Index");
        

           
         
        }


    }
}
