using BLL.Interfaces;
using BLL.Repositories;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace MVC_3PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepo repo;
        public DepartmentController(IDepartmentRepo DeptRepo)
        {
            repo= DeptRepo;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var departments = repo.GetAll();
            return View(departments);
        }
        [HttpGet]
        public IActionResult Create() { 
            return View();
        }
        [HttpPost]
        public IActionResult Create(Department model)
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
        public IActionResult Details(int? id) 
        {
            if (id is null) return BadRequest();

            var res =repo.Get(id.Value);
            if (res == null)
            {
                return NotFound();
            }
            return View(res);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is null) return BadRequest();
            var res = repo.Get(id.Value);
            if (res == null)
            {
                return NotFound();
            }
            return View(res);
        }
        [HttpPost]
        public IActionResult Edit(int id, Department model)
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
        public IActionResult Delete(int? id) {

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
