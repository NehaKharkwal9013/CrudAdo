using DAL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class EmployeeController : Controller
    {
        EmployeeDataAccess _db;
        public EmployeeController(EmployeeDataAccess db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var emp = (from employee in _db.GetAllEmployees()
                       where employee.EmployeeId > 0
                            select new EmpModel
                            {
                              EmployeeId= employee.EmployeeId,
                              Name = employee.Name,
                              Gender = employee.Gender,
                              Department = employee.Department
                            }).ToList();
            return View(emp);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee model)
        {
            try
            {
                ModelState.Remove("EmployeeId");
                if (ModelState.IsValid)
                {
                    _db.AddEmployee(model);
                    return RedirectToAction("Index");
                }
            }

            catch (Exception ex)
            {

            }
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            Employee employee = _db.GetEmployeeData(id);
            return View("Create", employee);
        }

        [HttpPost]
        public IActionResult Edit(Employee employee)
        {
            try
            {
                _db.UpdateEmployee(employee);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
               
            }
            return View("Create", employee);

        }

        public IActionResult Delete(int id)
        {

            Employee employee = _db.GetEmployeeData(id);
            if (employee != null)
            {
                _db.DeleteEmployee(id);
            }
            return RedirectToAction("Index");
        }
    }
}
