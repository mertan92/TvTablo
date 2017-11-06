using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Uppgift_4_Mertan.Data;

namespace Uppgift_4_Mertan.Security
{
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        private readonly string[] userAssignedRole;
        private dbOperations dOp = new dbOperations();

        public AuthorizeRolesAttribute(params string[] roles)
        {
            this.userAssignedRole = roles;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = false;
            foreach (var roles in userAssignedRole)
            {
                authorize = dOp.IsUserInRole(httpContext.User.Identity.Name, roles);
                if (authorize)
                {
                    return authorize;
                }
            }
            return authorize;
        }
    }
}