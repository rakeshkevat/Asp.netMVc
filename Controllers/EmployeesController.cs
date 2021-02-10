using Management.Customfilter;
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

        [CustomAuthenticationFilter("Admin")]
        public ActionResult Index()
        {
            List<Employee> employees = db.Employees.ToList();
            List<EmployeeViewModel> employeeViewModels = new List<EmployeeViewModel>();
            foreach (var item in employees)
            {
                employeeViewModels.Add(new EmployeeViewModel()
                {
                    EmpID = item.EmpID,
                    EmpName = item.EmpName,
                    EmpEmail = item.EmpEmail,
                    EmpContact = item.EmpContact,
                    IsActive = item.IsActive
                });
            }
            return View(employeeViewModels);
            //return View(db.Employees.Take(3).ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(EmployeeViewModel employeeViewModel)
        {

            if (ModelState.IsValid)
            {
                Employee employee = new Employee();
                employee.EmpID = employeeViewModel.EmpID;
                employee.EmpName = employeeViewModel.EmpName;
                employee.EmpContact = employeeViewModel.EmpContact;
                employee.EmpEmail = employeeViewModel.EmpEmail;
                employee.IsActive = employeeViewModel.IsActive;
                employee.CreatedDate = DateTime.Now;
                employee.Password = employeeViewModel.Password;
                db.Employees.Add(employee);
                db.SaveChanges();
                db.EmpRoles.Add(new EmpRole
                {
                    EmpID = employee.EmpID,
                    RoleID = 3,
                    IsActive = true,
                    CreatedDate = DateTime.Now
                });
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(employeeViewModel);
        }
        public ActionResult Details(int id)
        {
            Employee employee = db.Employees.Find(id);
            EmployeeViewModel employeeViewModel = new EmployeeViewModel();
            employeeViewModel.EmpID = employee.EmpID;
            employeeViewModel.EmpName = employee.EmpName;
            employeeViewModel.EmpContact = employee.EmpContact;
            employeeViewModel.EmpEmail = employee.EmpEmail;
            employeeViewModel.IsActive = employee.IsActive;
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
            employeeViewModel.Password = employee.Password;
            return View(employeeViewModel);

        }
        [HttpPost]
        public ActionResult Edit(EmployeeViewModel employeeViewModel)
        {
            if (ModelState.IsValid)
            {
                Employee employee = db.Employees.Find(employeeViewModel.EmpID);
                employee.EmpName = employeeViewModel.EmpName;
                employee.EmpContact = employeeViewModel.EmpContact;
                employee.EmpEmail = employeeViewModel.EmpEmail;
                employee.IsActive = employeeViewModel.IsActive;
                employee.Password = employeeViewModel.Password;
                employee.UpdateDate = DateTime.Now;
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employeeViewModel);

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