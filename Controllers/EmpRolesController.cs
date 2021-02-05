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
    public class EmpRolesController : Controller
    {

        private EmpManagementEntities db = new EmpManagementEntities();
        // GET: EmpRoles
        
        public ActionResult Index()
        {          

            var innerjoint = from empRole in db.EmpRoles
                             join emp in db.Employees on empRole.EmpID equals emp.EmpID
                             join role in db.Roles on empRole.RoleID equals role.RoleID
                             select new EmpRoleViewModel
                             {
                                 EmpRolesID = empRole.EmpRolesID,
                                 EmpID = empRole.EmpID,
                                 RoleID = empRole.RoleID,
                                 EmployeeName = emp.EmpName,
                                 RoleName = role.RoleName,
                                 IsActive  = empRole.IsActive
                             };
            return View(innerjoint);
        }

        public ActionResult Create()
        {
            var rolelist1 = db.Roles.ToList();
            ViewBag.Rolelist = new SelectList(rolelist1, "RoleID", "RoleName");

            var employeelist1 = db.Employees.ToList();
            ViewBag.Employeelist = new SelectList(employeelist1, "EmpID", "EmpName");
            return View();
        }
        [HttpPost]
        public ActionResult Create(EmpRoleViewModel empRoleViewModel)
        {
            if (ModelState.IsValid)
            {
                EmpRole empRole = new EmpRole();
                empRole.RoleID = empRoleViewModel.RoleID;
                empRole.EmpID = empRoleViewModel.EmpID;
                empRole.IsActive = empRoleViewModel.IsActive;
                empRole.CreatedDate = DateTime.Now;
                db.EmpRoles.Add(empRole);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(empRoleViewModel);
        }
        public ActionResult Details(int? id)
        {             
            var innerjoint = (from empRole in db.EmpRoles
                             join emp in db.Employees on empRole.EmpID equals emp.EmpID
                             join role in db.Roles on empRole.RoleID equals role.RoleID
                             select new EmpRoleViewModel
                             {
                                 EmpRolesID = empRole.EmpRolesID,
                                 EmpID = empRole.EmpID,
                                 RoleID = empRole.RoleID,
                                 EmployeeName = emp.EmpName,
                                 RoleName = role.RoleName,
                                 IsActive = empRole.IsActive
                             }).FirstOrDefault();
            return View(innerjoint);
        }
        public ActionResult Edit(int? id)
        {
            EmpRole empRole = db.EmpRoles.Find(id);
            EmpRoleViewModel empRoleViewModel = new EmpRoleViewModel();
            empRoleViewModel.EmpRolesID = empRole.EmpRolesID;
            empRoleViewModel.RoleID = empRole.RoleID;
            empRoleViewModel.EmpID = empRole.EmpID;
            empRoleViewModel.IsActive = empRole.IsActive;


            List<Role> rolelist1 = db.Roles.ToList(); 
            ViewBag.Rolelist = new SelectList(rolelist1, "RoleID", "RoleName",empRole.RoleID);

            List<Employee> employeelist1 = db.Employees.ToList();
            ViewBag.Employeelist = new SelectList(employeelist1, "EmpID", "EmpName", empRole.EmpID);
            return View(empRoleViewModel);
        }
        [HttpPost]
        public ActionResult Edit(EmpRoleViewModel empRoleViewModel)
        {

            if (ModelState.IsValid)
            {
                EmpRole empRole = db.EmpRoles.Find(empRoleViewModel.EmpRolesID);
                empRole.RoleID = empRoleViewModel.RoleID;
                empRole.EmpID = empRoleViewModel.EmpID;
                empRole.IsActive = empRoleViewModel.IsActive;
                empRole.CreatedDate = DateTime.Now;
                db.Entry(empRole).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(empRoleViewModel); 

        }

        public ActionResult Delete(int? id)
        {
            EmpRole empRole = db.EmpRoles.Find(id);
            db.EmpRoles.Remove(empRole);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}