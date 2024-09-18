using AutoMapper;
using BLL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using MVC_3PL.Helper;
using MVC_3PL.ViewModel;
using System.Collections.ObjectModel;

namespace MVC_3PL.Controllers
{
    public class EmployeeController : Controller
    {
        //private IDepartmentRepo DeptRepo;
        //private readonly IEmployeeReop repo;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public EmployeeController(//IEmployeeReop EmpRepo,IDepartmentRepo DeptRepo,
                                  IUnitOfWork unitOfWork,IMapper mapper)
        {
            //repo = EmpRepo;
            //this.DeptRepo = DeptRepo;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index(string search)
        {
            var employees=Enumerable.Empty<Employee>();
            var EVM = new Collection<EmployeeViewModel>();
            if (string.IsNullOrEmpty(search))
            {
                 employees = await unitOfWork.EmployeeReop.GetAllAsync();
            }
            else
            {
                employees= await unitOfWork.EmployeeReop.GetByNameAsync(search);
            }
            //Auto Mapping
            var res =mapper.Map<IEnumerable<EmployeeViewModel>>(employees);
            //string mes = "hello world";

            //// View Dictionary :[Extra Info] Transfer Data from Action to view [One Way]
            ////1. View Data: Property Inherited From Controller - Dictionaty
            //ViewData["Message"] = mes + " from ViewData";
            ////2. View Bag: Property Inherited From Controller - Dynamic
            //ViewBag.Message = mes + " from ViewBag";
            ////3. Temp Date: Property Inherited From Controller - Dictionary
            //TempData["Message01"] = mes + " from TempData";
            
            return View("Index", res);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var depts= await unitOfWork.DepartmentRepo.GetAllAsync();
            ViewData["Departments"] = depts;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.ImageName = FilesSettings.UploadFile(model.Image, "images");

                //Casting :EmployeeViewModel--->Employee
                
                ////Manual Mapping
                //Employee emp = new Employee()
                //{
                //    Id = model.Id,
                //    Name = model.Name,
                //    Age = model.Age,
                //    Adress = model.Adress,
                //    Salary = model.Salary,
                //    Phone = model.Phone,
                //    Email = model.Email,
                //    IsActive = model.IsActive,
                //    IsDeleted = model.IsDeleted,
                //    DateOfCreation = model.DateOfCreation,
                //    HiringDate = model.HiringDate,
                //    WorkFor = model.WorkFor,
                //    WorkForId = model.WorkForId 
                //};

                var employee=mapper.Map<Employee>(model);
                var res = await unitOfWork.EmployeeReop.AddAsync(employee);
                if (res > 0)
                {
                    TempData["Message"] = "Employee is Created Succesfully";

                }
                else
                {
                    TempData["Message"] = "Employee isn't Created Succesfully";
                }
                return RedirectToAction("Index");
            }

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id, string viewName = "Details")
        {
            if (id is null) return BadRequest();
            var res = await unitOfWork.EmployeeReop.GetAsync(id.Value);
            
            if (res == null)
            {
                return NotFound();
            }
            if (viewName == "Edit") { 
                var EVM = mapper.Map<EmployeeViewModel>(res);
                return View(viewName, EVM);
            }
            return View(viewName, res);
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
            var depts = await unitOfWork.DepartmentRepo.GetAllAsync();
            ViewData["Departments"] = depts;
            return await Details(id, "Edit");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                if(model.ImageName is not null) { 
                   FilesSettings.DeleteFile(model.ImageName, "images");
                }
                model.ImageName= FilesSettings.UploadFile(model.Image, "images");
                var employee = mapper.Map<Employee>(model);
                var res = await unitOfWork.EmployeeReop.UpdateAsync(employee); 
                if (res > 0)
                {
                    return RedirectToAction("Index");
                }
            }

            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {

            if (id is null) return BadRequest();
            var res = await unitOfWork.EmployeeReop.GetAsync(id.Value);
            if (res == null)
            {
                return NotFound();
            }
            FilesSettings.DeleteFile(res.ImageName, "images");
            await unitOfWork.EmployeeReop.DeleteAsync(res);         
            return RedirectToAction("Index");
        }

    }
}
