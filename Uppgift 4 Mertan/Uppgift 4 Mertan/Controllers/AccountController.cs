using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Uppgift_4_Mertan.Data;
using Uppgift_4_Mertan.Models.db;
using Uppgift_4_Mertan.Models.ViewModels;

namespace Uppgift_4_Mertan.Controllers
{
    public class AccountController : Controller
    {
        private dbOperations dOp = new dbOperations();
        private TvTabloEntities2 db = new TvTabloEntities2();


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
                    FormsAuthentication.SetAuthCookie(model.Username, false);
                    
                    try
                    {
                        return Redirect(ReturnUrl);
                    }
                    catch (Exception)
                    {

                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Fel användarnamn eller lösenord!");
                }
            }

            if (ReturnUrl == "/Home/Admin")
            {
                ViewBag.Admin = "(Inlogningen är bara för administratörer i websidan)";
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