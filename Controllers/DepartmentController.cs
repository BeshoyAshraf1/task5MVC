using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using task3.Models;

namespace task4.Controllers
{
    public class DepartmentController : Controller
    {
        public IActionResult Index()
        {
            var context = new ApplicationDbContext();

            var Departments = context.Depratments
           .Include(i => i.Instractors)
           .ToList();
            return View(Departments);
        }


        public IActionResult Details(int id)
        {
            var context = new ApplicationDbContext();

            var instractors = context.Depratments
           .Include(i => i.Instractors)
           .FirstOrDefault(x => x.id == id);

            return View(instractors);
        }
        [HttpPost]
        public IActionResult DeleteDepartment(int id)
        {
            var context = new ApplicationDbContext();
            var department = context.Depratments.Include(d => d.Instractors).FirstOrDefault(d => d.id == id);

            if (department == null)
            {
                return NotFound();
            }

            context.Depratments.Remove(department);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var context = new ApplicationDbContext();

            var department = context.Depratments.FirstOrDefault(d => d.id == id);

            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        [HttpPost]
        public IActionResult Edit(Depratment model)
        {
            var context = new ApplicationDbContext();



            var department = context.Depratments.FirstOrDefault(d => d.id == model.id);

            if (department == null)
            {
                return NotFound();
            }

            department.name = model.name;
            department.managername = model.managername;

            context.SaveChanges();

            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Create()
        {


            return View();

        }

        [HttpPost]
        public IActionResult CreateDepartment(Depratment model)
        {
            var context = new ApplicationDbContext();


            context.Depratments.Add(model);
            context.SaveChanges();

            return RedirectToAction("Index");



       


        }
    }
}
