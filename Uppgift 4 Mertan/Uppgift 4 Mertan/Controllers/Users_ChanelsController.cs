using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Uppgift_4_Mertan.Data;
using Uppgift_4_Mertan.Models.db;
using Uppgift_4_Mertan.Models.ViewModels;

namespace Uppgift_4_Mertan.Controllers
{
    public class Users_ChanelsController : Controller
    {
        private TvTabloEntities2 db = new TvTabloEntities2();
        private dbOperations dOp = new dbOperations();

        // GET: Users_Chanels
        [Authorize]
        public ActionResult Index()
        {
            var users_Chanels = db.Users_Chanels.Include(u => u.Channels).Include(u => u.Users).Where(x => x.Users.Username == User.Identity.Name);
            return View(users_Chanels.ToList());
        }

        // GET: Users_Chanels/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users_Chanels users_Chanels = db.Users_Chanels.Find(id);
            if (users_Chanels == null)
            {
                return HttpNotFound();
            }
            return View(users_Chanels);
        }

        // GET: Users_Chanels/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.Id = dOp.GetUserChannelsNextId();
            ViewBag.ChannelId = new SelectList(db.Channels, "Id", "Name");
            ViewBag.UserId = new SelectList(db.Users.Where(x => x.Username == User.Identity.Name), "Id", "Username");
            return View();
        }

        // POST: Users_Chanels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,ChannelId")] Users_Chanels users_Chanels)
        {
            if (ModelState.IsValid)
            {
                int UsChNextId = dOp.GetUserChannelsNextId();
               
                db.Users_Chanels.Add(users_Chanels);
                db.SaveChanges();
                return RedirectToAction("Index");

            }

            ViewBag.ChannelId = new SelectList(db.Channels, "Id", "Name", users_Chanels.ChannelId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Username", users_Chanels.UserId);
            return View(users_Chanels);
        }

        // GET: Users_Chanels/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users_Chanels users_Chanels = db.Users_Chanels.Find(id);
            if (users_Chanels == null)
            {
                return HttpNotFound();
            }
            ViewBag.ChannelId = new SelectList(db.Channels, "Id", "Name", users_Chanels.ChannelId);
            ViewBag.UserId = new SelectList(db.Users.Where(x => x.Username == User.Identity.Name), "Id", "Username", users_Chanels.UserId);
            return View(users_Chanels);
        }

        // POST: Users_Chanels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,ChannelId")] Users_Chanels users_Chanels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(users_Chanels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ChannelId = new SelectList(db.Channels, "Id", "Name", users_Chanels.ChannelId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Username", users_Chanels.UserId);
            return View(users_Chanels);
        }

        // GET: Users_Chanels/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users_Chanels users_Chanels = db.Users_Chanels.Find(id);
            if (users_Chanels == null)
            {
                return HttpNotFound();
            }
            return View(users_Chanels);
        }

        // POST: Users_Chanels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Users_Chanels users_Chanels = db.Users_Chanels.Find(id);
            db.Users_Chanels.Remove(users_Chanels);
            db.SaveChanges();
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
