using BusinessLayer.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.EmployeeModel;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EmployeePayRoleMVC.Controllers
{
    
    public class EmployeeController1 : Controller
    {
        private readonly IEmployeeBusiness business;
        public EmployeeController1(IEmployeeBusiness business)
        {
            this.business = business;
        }

        [HttpGet("getAll")]
        public IActionResult GetLlEmployye() {

            List<EmployeeEntity> lstEmployee = business.GetAllEmployees().ToList();
            return View(lstEmployee);

        }

        [HttpGet("getEmp")]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost("getEmp")]
        [ValidateAntiForgeryToken]

        public IActionResult Create([Bind] EmployeeModel employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    business.AddEmployee(employee);
                    return RedirectToAction("GetLlEmployye");

                }
                return View(employee);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
                return View(employee);
            }
        }
        




        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("login")]
        public IActionResult Login( [Bind]LoginModel model)
        {
            try
            {

            
           
            if (model.EmployeeId <= 0 || string.IsNullOrEmpty(model.FullName))
            {
                
                return BadRequest($"Invalid input parameters {model.EmployeeId} or {model.FullName}");
            }

            
            EmployeeEntity employee = business.Login(model.EmployeeId, model.FullName);

            if (employee == null)
            {
                
                return BadRequest($"Invalid input parameters {model.EmployeeId} or {model.FullName} Please enter valid Input"); 
            }

            HttpContext.Session.SetInt32("EmployeeId", employee.EmployeeId);
            HttpContext.Session.SetString("FullName", employee.FullName);

            
            return RedirectToAction("Details");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult UserDashBoard()
        {
            if (HttpContext.Session.GetInt32("EmployeeId") != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }


        [HttpGet]
        public IActionResult Details(int? id)
        {

            try {  
                
                //id=HttpContext.Session.GetInt32("EmployeeId");

            if (id == null)
            {
                return NotFound();
            }
            EmployeeEntity employee = business.GetEmployeeData(id);
            if (employee == null)
            {
                return NotFound();

            }

            return View(employee);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
                return View();
            }
        }




        [HttpGet("Edit/{id}")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            EmployeeEntity employee = business.GetEmployeeData(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost("Edit/{id}"), ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind] EmployeeEntity employee)
        {
            try
            {
                if (id != employee.EmployeeId)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    
                    business.UpdateEmployee(employee);
                    return RedirectToAction("GetLlEmployye");
                }
                return View();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
                return View(employee);
            }
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            try
            {
            
            
            if (id == null)
            {
                return NotFound();
            }
            EmployeeEntity employee = business.GetEmployeeData(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return RedirectToAction("GetLlEmployye"); // Redirect to the Index page even if there's an error
            }

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            try
            {
                business.DeleteEmployee(id);
                return RedirectToAction("GetLlEmployye");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return RedirectToAction("GetLlEmployye"); // Redirect to the Index page even if there's an error
            }
        }


        [HttpGet("search")]
        public IActionResult Search()
        {
            return View();
        }

        [HttpPost("search"), ActionName("Search")]
        public IActionResult Search([Bind] EmployeeEntity model)
        {
            

            if (model.FullName != null)
            {
                return RedirectToAction("GetAllEmpByName", "", new { name = model.FullName });
            }
            else
            {
                return BadRequest("Invalid input parameters.");
            }
        }




        [HttpGet("emp/{name}")]
        public IActionResult GetAllEmpByName(string name)
        {

            try
            {

                List<EmployeeEntity> employee = business.GetAllEmployeesByName(name).ToList();
                
                if (employee == null)
                {
                    return NotFound();

                }

                return View(employee);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
                return View();
            }
        }

        //public IActionResult Index()
        //{
        //   return View();
        //}


    }
}
