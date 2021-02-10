using MyApp.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;

namespace Management.Customfilter
{
    public class CustomAuthenticationFilter : AuthorizeAttribute
    {
        private readonly string[] allowedroles;
        public CustomAuthenticationFilter(params string[] roles)
        {
            this.allowedroles = roles;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = false;
            var userId = Convert.ToInt32(httpContext.Session["UserId"]);
            if (userId > 0)
                using (var db = new EmpManagementEntities())
                {
                    var empRole = (from eRole in db.EmpRoles
                                  join r in db.Roles on eRole.RoleID equals r.RoleID
                                  where eRole.EmpID == userId
                                  select new {                                  
                                  r.RoleName
                                  }).FirstOrDefault();

                    

                    foreach (var role in allowedroles)
                    {
                        if (role == empRole.RoleName) return true;
                    }
                }


            return authorize;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
               new RouteValueDictionary
               {
                    { "controller", "Account" },
                    { "action", "Login" }
               });
        }
    }
}