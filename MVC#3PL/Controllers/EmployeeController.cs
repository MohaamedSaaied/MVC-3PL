using AutoMapper;
using BLL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
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
         
        public IActionResult Index(string search)
        {
            var employees=Enumerable.Empty<Employee>();
            var EVM = new Collection<EmployeeViewModel>();
            if (string.IsNullOrEmpty(search))
            {
                 employees = unitOfWork.EmployeeReop.GetAll();
            }
            else
            {
                employees= unitOfWork.EmployeeReop.GetByName(search);
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
        public IActionResult Create()
        {
            var depts= unitOfWork.DepartmentRepo.GetAll();
            ViewData["Departments"] = depts;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {

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
                var res = unitOfWork.EmployeeReop.Add(employee);
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
        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (id is null) return BadRequest();
            var res = unitOfWork.EmployeeReop.Get(id.Value);
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
            var depts = unitOfWork.DepartmentRepo.GetAll();
            ViewData["Departments"] = depts;
            return Details(id, "Edit");
        }
        [HttpPost]
        public IActionResult Edit(int id, Employee model)
        {
            if (ModelState.IsValid)
            {
                var res = unitOfWork.EmployeeReop.Update(model);
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
            var res = unitOfWork.EmployeeReop.Get(id.Value);
            if (res == null)
            {
                return NotFound();
            }
            unitOfWork.EmployeeReop.Delete(res);
            return RedirectToAction("Index");
        }

    }
}
