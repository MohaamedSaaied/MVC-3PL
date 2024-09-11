using BLL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace MVC_3PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeReop repo;
        public EmployeeController(IEmployeeReop EmpRepo)
        {
            repo = EmpRepo;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var employees = repo.GetAll();
            return View("Index", employees);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee model)
        {
            if (ModelState.IsValid)
            {
                var res = repo.Add(model);
                if (res > 0)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }
        [HttpGet]
        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (id is null) return BadRequest();
            var res = repo.Get(id.Value);
            if (res == null)
            {
                return NotFound();
            }
            return View(viewName, res);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            //if (id is null) return BadRequest();
            //var res = repo.Get(id.Value);
            //if (res == null)
            //{
            //    return NotFound();
            //}
            //return View(res);
            return Details(id, "Edit");
        }
        [HttpPost]
        public IActionResult Edit(int id, Employee model)
        {

            if (ModelState.IsValid)
            {
                var res = repo.Update(model);
                if (res > 0)
                {
                    return RedirectToAction("Index");
                }
            }

            return NotFound();
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {

            if (id is null) return BadRequest();
            var res = repo.Get(id.Value);
            if (res == null)
            {
                return NotFound();
            }
            repo.Delete(res);
            return RedirectToAction("Index");
        }

    }
}
