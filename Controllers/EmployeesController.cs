using MyApp.Db;
using MyApp.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Management.Controllers
{
    public class EmployeesController : Controller
    {
        // GET: Employees

        private EmpManagementEntities db = new EmpManagementEntities();
        public ActionResult Index()
        {
            return View(db.Employees.Take(3).ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(EmployeeViewModel employeeViewModel)
        {
            Employee employee = new Employee();
            employee.EmpID = employeeViewModel.EmpID;
            employee.EmpName = employeeViewModel.EmpName;
            employee.EmpContact = employeeViewModel.EmpContact;
            employee.EmpEmail = employeeViewModel.EmpEmail;
            employee.IsActive = employeeViewModel.IsActive;
            employee.CreatedDate = employeeViewModel.CreatedDate;
            db.Employees.Add(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            Employee employee = db.Employees.Find(id);
            EmployeeViewModel employeeViewModel = new EmployeeViewModel();
            employee.EmpID = employeeViewModel.EmpID;
            employee.EmpName = employeeViewModel.EmpName;
            employee.EmpContact = employeeViewModel.EmpContact;
            employee.EmpEmail = employeeViewModel.EmpEmail;
            employee.IsActive = employeeViewModel.IsActive;
            employee.CreatedDate = employeeViewModel.CreatedDate;         
             return View(employeeViewModel);
        }

        public ActionResult Edit(int id)
        {
            Employee employee = db.Employees.Find(id);
            EmployeeViewModel employeeViewModel = new EmployeeViewModel();
            employeeViewModel.EmpID = employee.EmpID;
            employeeViewModel.EmpName = employee.EmpName;
            employeeViewModel.EmpContact = employee.EmpContact;
            employeeViewModel.EmpEmail = employee.EmpEmail;
            employeeViewModel.IsActive = employee.IsActive;
            employeeViewModel.CreatedDate = employee.CreatedDate;
            return View(employeeViewModel);

        }
        [HttpPost]
        public ActionResult Edit(EmployeeViewModel employeeViewModel)
        {
            Employee employee = db.Employees.Find(employeeViewModel.EmpID);            
            employee.EmpName = employeeViewModel.EmpName;
            employee.EmpContact = employeeViewModel.EmpContact;
            employee.EmpEmail = employeeViewModel.EmpEmail;
            employee.IsActive = employeeViewModel.IsActive;
            employee.UpdateDate = DateTime.Now;
            db.Entry(employee).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult Delete(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}   