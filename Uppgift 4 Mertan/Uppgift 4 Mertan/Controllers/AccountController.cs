using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Uppgift_4_Mertan.Data;
using Uppgift_4_Mertan.Models.ViewModels;

namespace Uppgift_4_Mertan.Controllers
{
    public class AccountController : Controller
    {
        private dbOperations dOp = new dbOperations();

        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginVM model, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                if (dOp.CheckUserCredentials(model.Username, model.Password) == true)
                {
                    //ako iskame cookie koito da zapazva druga informaciq osven inlogning pishem true vmesto false (zapiski 5.19)
                    FormsAuthentication.SetAuthCookie(model.Username, false);
                    return Redirect(ReturnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Fel användarnamn eller lösenord!");
                }
            }
            return View();
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        
    }
}