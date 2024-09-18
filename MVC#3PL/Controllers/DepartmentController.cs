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
        public async Task<IActionResult> Index()
        {
            var departments =await unitOfWork.DepartmentRepo.GetAllAsync();
            return View(departments);
        }
        [HttpGet]
        public IActionResult Create() { 
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Department model)
        {
            if (ModelState.IsValid)
            {
                var res =await unitOfWork.DepartmentRepo.AddAsync(model);
                if (res > 0)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id,string viewName="Details") 
        {
            if (id is null) return BadRequest();
            var res = await unitOfWork.DepartmentRepo.GetAsync(id.Value);
            if (res == null)
            {
                return NotFound();
            }
            return View(viewName,res);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            //if (id is null) return BadRequest();
            //var res = repo.Get(id.Value);
            //if (res == null)
            //{
            //    return NotFound();
            //}
            //return View(res);
            return await Details(id,"Edit");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Department model)
        {

            if (ModelState.IsValid)
            {
                var res =await unitOfWork.DepartmentRepo.UpdateAsync(model);
                if (res > 0)
                {
                    return RedirectToAction("Index");
                }
            }

            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id) {

            if (id is null) return BadRequest();
            var res = await unitOfWork.DepartmentRepo.GetAsync(id.Value);
            if (res == null)
            {
                return NotFound();
            }
            await unitOfWork.DepartmentRepo.DeleteAsync(res);
            return RedirectToAction("Index");
        }

    }
}
