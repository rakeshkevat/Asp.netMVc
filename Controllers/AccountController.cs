using MyApp.Db;
using MyApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Management.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        


        private EmpManagementEntities db = new EmpManagementEntities();


        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            var employee = db.Employees.Where(x => x.EmpName.Equals(loginViewModel.EmpName) && x.Password.Equals(loginViewModel.Password)).SingleOrDefault();

            if (employee != null)
            {
                var empRole = db.EmpRoles.FirstOrDefault(x=>x.EmpID == employee.EmpID);
               Session["RoleId"] = empRole.RoleID;
                Session["UserId"] = employee.EmpID;
                Session["EmpName"] = employee.EmpName;

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public ActionResult Logout(LoginViewModel loginViewModel)
        {
            Session["RoleId"] = null;

            return RedirectToAction("Login");
        }



    }
}