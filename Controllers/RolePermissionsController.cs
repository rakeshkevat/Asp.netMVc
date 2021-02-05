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
            var innerjoint = from rp in db.RolePermissions
                             join r in db.Roles on rp.RoleID equals r.RoleID
                             join p in db.Pages on rp.PageID equals p.PageID
                             select new RolePermissionViewModel()
                             {   RolePermissingID = rp.RolePermissingID,
                                 RoleID = rp.RoleID,
                                 PageID = rp.PageID,
                                 RoleName =  r.RoleName,
                                 PageName = p.PageName
                             };
            return View(innerjoint);
        }
        public ActionResult Create()   
        {
            var ListofRoles = db.Roles.ToList();
            ViewBag.Roles = new SelectList( ListofRoles, "RoleID", "RoleName");
            var ListofPages = db.Pages.ToList();
            ViewBag.ListofPages = new SelectList(ListofPages, "PageID", "PageName");
            return View();
        }
        [HttpPost]
        public ActionResult Create(RolePermissionViewModel rolePermissionViewModel)
        {
            RolePermission rolePermission = new RolePermission();
            rolePermission.RolePermissingID = rolePermissionViewModel.RolePermissingID;
            rolePermission.RoleID = rolePermissionViewModel.RoleID;
            rolePermission.PageID = rolePermissionViewModel.PageID;
            db.RolePermissions.Add(rolePermission);
            db.SaveChanges();
            return RedirectToAction("index");
        }
        public ActionResult Details(int id)
        {
            var selectFirstRowData = (from rp in db.RolePermissions
                                  join r in db.Roles on rp.RoleID equals r.RoleID
                                  join p in db.Pages on rp.PageID equals p.PageID
                                  select new RolePermissionViewModel()
                                  {
                                      RolePermissingID = rp.RolePermissingID,
                                      RoleID = rp.RoleID,
                                      PageID = rp.PageID,
                                      RoleName = r.RoleName,
                                      PageName = p.PageName
                                  }).FirstOrDefault();
            return View(selectFirstRowData);

        }
        public ActionResult Edit(int id)
        {
            RolePermission rolePermission = db.RolePermissions.Find(id);
            RolePermissionViewModel rolePermissionViewModel = new RolePermissionViewModel();
            rolePermissionViewModel.RolePermissingID = rolePermission.RolePermissingID;
            rolePermissionViewModel.RoleID = rolePermission.RoleID;
            rolePermissionViewModel.PageID = rolePermission.PageID;

            List<Role> ListofRoles = db.Roles.ToList();
            ViewBag.Roles = new SelectList(ListofRoles, "RoleID", "RoleName", rolePermission.RoleID);
            List<Page> ListofPages = db.Pages.ToList();
            ViewBag.ListofPages = new SelectList(ListofPages, "PageID", "PageName", rolePermission.PageID);
            return View(rolePermissionViewModel);
        }
        [HttpPost]
        public ActionResult Edit(RolePermissionViewModel rolePermissionViewModel)
        {

            RolePermission rolePermission = db.RolePermissions.Find(rolePermissionViewModel.RolePermissingID);
            rolePermission.RoleID = rolePermissionViewModel.RoleID;
            rolePermission.PageID = rolePermissionViewModel.PageID;
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