using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyApp.Db;
using MyApp.Model;

namespace Management.Controllers
{
    public class RolesController : Controller
    {
        private  EmpManagementEntities db = new EmpManagementEntities();
        // GET: Roles
        public ActionResult Index()
        {
            return View(db.Roles.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(RoleViewModel roleViewModel)
        {
            Role role = new Role();
            role.RoleName = roleViewModel.RoleName;
            role.RoleDescruption = roleViewModel.RoleDescruption;
            role.IsActive = roleViewModel.IsActive;
            role.CreatedDate = DateTime.Now;
            db.Roles.Add(role);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int? id)
        {
            Role role = db.Roles.Find(id);
            RoleViewModel roleViewModel = new RoleViewModel();
            roleViewModel.RoleID = role.RoleID;
            roleViewModel.RoleName = role.RoleName;
            roleViewModel.RoleDescruption = role.RoleDescruption;
            roleViewModel.IsActive = role.IsActive;
            return View(roleViewModel);
        }
        [HttpPost]
        public ActionResult Edit(RoleViewModel roleViewModel)
        {
            Role role = db.Roles.Find(roleViewModel.RoleID);
            role.RoleName = roleViewModel.RoleName;
            role.RoleDescruption = roleViewModel.RoleDescruption;
            role.IsActive = roleViewModel.IsActive;
            role.UpdateDate = DateTime.Now;
            db.Entry(role).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int? id )
        {
            Role role = db.Roles.Find(id);
            db.Roles.Remove(role);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        

        
    }
}