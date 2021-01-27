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
    public class RolePermissionsController : Controller
    {
        private EmpManagementEntities db = new EmpManagementEntities();
        // GET: RolePermissions
        public ActionResult Index()
        {
            return View(db.RolePermissions.ToList());
        }
        public ActionResult Create()    
        {
             
            return View();
        }
        [HttpPost]
        public ActionResult Create(RolePermissionViewModel rolePermissionViewModel)
        {
            RolePermission rolePermission = new RolePermission();
            rolePermission.RolePermissingID = rolePermissionViewModel.RolePermissingID;
            rolePermission.RoleID = rolePermissionViewModel.RoleID;
            rolePermission.PageName = rolePermissionViewModel.PageName;
            db.RolePermissions.Add(rolePermission);
            db.SaveChanges();
            return RedirectToAction("index");
        }
        public ActionResult Details(int id)
        {
            RolePermission rolePermission = db.RolePermissions.Find(id);
            RolePermissionViewModel rolePermissionViewModel = new RolePermissionViewModel();
            rolePermissionViewModel.RolePermissingID = rolePermission.RolePermissingID;
            rolePermissionViewModel.RoleID = rolePermission.RoleID;
            rolePermissionViewModel.PageName = rolePermission.PageName;            
            return View(rolePermissionViewModel); 
        }
        public ActionResult Edit(int id)
        {
            RolePermission rolePermission = db.RolePermissions.Find(id);
            RolePermissionViewModel rolePermissionViewModel = new RolePermissionViewModel();
            rolePermissionViewModel.RolePermissingID = rolePermission.RolePermissingID;
            rolePermissionViewModel.RoleID = rolePermission.RoleID;
            rolePermissionViewModel.PageName = rolePermission.PageName;
            return View(rolePermissionViewModel);
        }
        [HttpPost]
        public ActionResult Edit(RolePermissionViewModel rolePermissionViewModel)
        {

            RolePermission rolePermission = db.RolePermissions.Find(rolePermissionViewModel.RolePermissingID);
            rolePermission.RoleID = rolePermissionViewModel.RoleID;
            rolePermission.PageName = rolePermissionViewModel.PageName;
            db.Entry(rolePermission).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("index");
        }

        public ActionResult Delete(int? id)
        {
            RolePermission rolePermission = db.RolePermissions.Find(id);
            db.RolePermissions.Remove(rolePermission);
            db.SaveChanges();
            return RedirectToAction("index");
        }
    }
}