using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Uppgift_4_Mertan.Models.db;

namespace Uppgift_4_Mertan.Controllers
{
    public class Users_Chanels1Controller : Controller
    {
        private TvTabloEntities2 db = new TvTabloEntities2();

        // GET: Users_Chanels1
        public ActionResult Index()
        {
            var users_Chanels = db.Users_Chanels.Include(u => u.Channels).Include(u => u.Users);
            return View(users_Chanels.ToList());
        }

        // GET: Users_Chanels1/Details/5
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

        // GET: Users_Chanels1/Create
        public ActionResult Create()
        {
            ViewBag.ChannelId = new SelectList(db.Channels, "Id", "Name");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Username");
            return View();
        }

        // POST: Users_Chanels1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,ChannelId")] Users_Chanels users_Chanels)
        {
            if (ModelState.IsValid)
            {
                db.Users_Chanels.Add(users_Chanels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ChannelId = new SelectList(db.Channels, "Id", "Name", users_Chanels.ChannelId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Username", users_Chanels.UserId);
            return View(users_Chanels);
        }

        // GET: Users_Chanels1/Edit/5
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
            ViewBag.UserId = new SelectList(db.Users, "Id", "Username", users_Chanels.UserId);
            return View(users_Chanels);
        }

        // POST: Users_Chanels1/Edit/5
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

        // GET: Users_Chanels1/Delete/5
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

        // POST: Users_Chanels1/Delete/5
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
