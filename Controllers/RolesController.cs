﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Management.Customfilter;
using MyApp.Db;
using MyApp.Model;

namespace Management.Controllers
{
    public class RolesController : Controller
    {
        private EmpManagementEntities db = new EmpManagementEntities();
        // GET: Roles
        [CustomAuthenticationFilter("Admin", "staff")]

        public ActionResult Index()
        {
            List<Role> role = db.Roles.Where(y => y.IsActive == true).ToList();
            List<RoleViewModel> roleViewModels = new List<RoleViewModel>();
            foreach (var item in role)
            {
                roleViewModels.Add(new RoleViewModel()
                {
                    RoleID = item.RoleID,
                    RoleName = item.RoleName,
                    RoleDescruption = item.RoleDescruption,
                    IsActive = item.IsActive,
                }
                );
            }

            return View(roleViewModels);
            //return View(db.Roles.ToList());
        }
        [CustomAuthenticationFilter("Admin")]

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
        [CustomAuthenticationFilter("Admin")]

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
        [CustomAuthenticationFilter("Admin")]

        public ActionResult Delete(int? id)
        {
            Role role = db.Roles.Find(id);
            role.IsActive = false;   // only deactive the account not for delete
            //db.Roles.Remove(role);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}