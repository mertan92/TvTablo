using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Uppgift_4_Mertan.Models.db;
using Uppgift_4_Mertan.Security;

namespace Uppgift_4_Mertan.Controllers
{
    public class UsersController : Controller
    {
        private TvTabloEntities2 db = new TvTabloEntities2();

        // GET: Users
        [AuthorizeRolesAttribute("Admin")]
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.Roles);
            return View(users.ToList());
        }

        // GET: Users/Details/5
        [AuthorizeRolesAttribute("Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // GET: Users/Create
        [AuthorizeRolesAttribute("Admin")]
        public ActionResult Create()
        {
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "Role");
            return View();
        }

        // Creat only users
        public ActionResult CreateUsers()
        {
            ViewBag.RoleId = new SelectList(db.Roles.Where(x => x.Role == "User"), "Id", "Role");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Username,Password,RoleId")] Users users)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(users);
                db.SaveChanges();
                TempData["message"] = "Allt är klart! Användaren är registrerad!";
                TempData["CssClass"] = "alert-success";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["message"] = "Något gick fel! Kontrolera om du har inte glömt att fylla i någon fält!";
                TempData["CssClass"] = "alert-danger";
                ViewBag.RoleId = new SelectList(db.Roles, "Id", "Role", users.RoleId);
                return View(users);
            }

           
        }

        //Only for users
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUsers([Bind(Include = "Id,Username,Password,RoleId")] Users users)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(users);
                db.SaveChanges();
                TempData["message"] = "Du är registrerad! Nu kan du logga in med dina uppgifter";
                TempData["CssClass"] = "alert-success";
                return RedirectToAction("Login", "Account");
            }
            else
            {
                TempData["message"] = "Något gick fel! Kontrolera om du har inte glömt att fylla i någon fält!";
                TempData["CssClass"] = "alert-danger";
                ViewBag.RoleId = new SelectList(db.Roles, "Id", "Role", users.RoleId);
                return View(users);
            }
        }

        // GET: Users/Edit/5
        [AuthorizeRolesAttribute("Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "Role", users.RoleId);
            return View(users);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Username,Password,RoleId")] Users users)
        {
            if (ModelState.IsValid)
            {
                db.Entry(users).State = EntityState.Modified;
                db.SaveChanges();
                TempData["message"] = "Allt är klart! Användare är uppdaterad!";
                TempData["CssClass"] = "alert-success";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["message"] = "Något gick fel! Kontrolera om du har inte glömt att fylla i någon fält!";
                TempData["CssClass"] = "alert-danger";
                ViewBag.RoleId = new SelectList(db.Roles, "Id", "Role", users.RoleId);
                return View(users);
            }
           
        }

        // GET: Users/Delete/5
        [AuthorizeRolesAttribute("Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Users users = db.Users.Find(id);
            db.Users.Remove(users);
            db.SaveChanges();
            TempData["message"] = "Allt är klart! Användare är borttagen!";
            TempData["CssClass"] = "alert-success";
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
