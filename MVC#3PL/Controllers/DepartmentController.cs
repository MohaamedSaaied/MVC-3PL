using BLL.Interfaces;
using BLL.Repositories;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace MVC_3PL.Controllers
{
    public class DepartmentController : Controller
    {
        //private readonly IDepartmentRepo repo;
        private readonly IUnitOfWork unitOfWork;
        public DepartmentController(/*IDepartmentRepo DeptRepo*/IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var departments = unitOfWork.DepartmentRepo.GetAll();
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
                var res = unitOfWork.DepartmentRepo.Add(model);
                if (res > 0)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }
        [HttpGet]
        public IActionResult Details(int? id,string viewName="Details") 
        {
            if (id is null) return BadRequest();
            var res = unitOfWork.DepartmentRepo.Get(id.Value);
            if (res == null)
            {
                return NotFound();
            }
            return View(viewName,res);
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
            return Details(id,"Edit");
        }
        [HttpPost]
        public IActionResult Edit(int id, Department model)
        {

            if (ModelState.IsValid)
            {
                var res = unitOfWork.DepartmentRepo.Update(model);
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
            var res = unitOfWork.DepartmentRepo.Get(id.Value);
            if (res == null)
            {
                return NotFound();
            }
            unitOfWork.DepartmentRepo.Delete(res);
            return RedirectToAction("Index");
        }

    }
}
