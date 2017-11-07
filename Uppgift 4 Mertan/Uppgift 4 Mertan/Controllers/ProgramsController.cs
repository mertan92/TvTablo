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
    public class ProgramsController : Controller
    {
        private TvTabloEntities2 db = new TvTabloEntities2();

        // GET: Programs
        [AuthorizeRolesAttribute("Admin")]
        public ActionResult Index()
        {
            var programs = db.Programs.Include(p => p.Channels);
            return View(programs.ToList());
        }

        [AuthorizeRolesAttribute("Admin")]
        public ActionResult IndexPuffar()
        {
            var programs = db.Programs.Include(p => p.Channels);
            return View(programs.ToList());
        }

        // GET: Programs/Details/5
        [AuthorizeRolesAttribute("Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Programs programs = db.Programs.Find(id);
            if (programs == null)
            {
                return HttpNotFound();
            }
            return View(programs);
        }

        // GET: Programs/Create
        [AuthorizeRolesAttribute("Admin")]
        public ActionResult Create()
        {
            ViewBag.ChannelId = new SelectList(db.Channels, "Id", "Name");
            return View();
        }

        // POST: Programs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ChannelId,Title,Date,Time,Duration,Category,Resume")] Programs programs)
        {
            if (ModelState.IsValid)
            {
              
                try
                {
                    if (programs.Id < 76)
                    {
                        db.Programs.Add(programs);
                        db.SaveChanges();
                        TempData["message"] = "Allt är klart! Programmet är skapat!";
                        TempData["CssClass"] = "alert-success";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        if (DateTime.Now.TimeOfDay < programs.Time)
                        {
                            db.Programs.Add(programs);
                            db.SaveChanges();
                            TempData["message"] = "Allt är klart! Nyhetspuffen är skapad!";
                            TempData["CssClass"] = "alert-success";
                            return RedirectToAction("IndexPuffar");
                        }
                        else
                        {
                            TempData["message"] = "Tiden för den nyheten är passerad. Du måste välja någon tid som är inte passerad än!";
                            TempData["CssClass"] = "alert-danger";
                            return RedirectToAction("IndexPuffar");
                        }
                        

                    }
                    
                }
                catch (Exception)
                {
                    if (programs.Id < 76)
                    {
                        TempData["message"] = "Något gick fel! Troligen den Id som du försöker välja finns redan. Därför kontrollera vilken id nummer från listan med programmer är ledig. Om allt är upptagen" +
                        " prova att redigera någon befintlig program!";
                        TempData["CssClass"] = "alert-danger";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["message"] = "Något gick fel! Troligen den Id som du försöker välja finns redan. Därför kontrollera vilken id nummer från listan med nyhetspuffar är ledig. Om allt är upptagen" +
                        " prova att redigera någon befintlig program!";
                        TempData["CssClass"] = "alert-danger";
                        return RedirectToAction("IndexPuffar");
                    }
                   
                }
                
            }
            else
            {
                TempData["message"] = "Något gick fel! Kontrolera om du har inte glömt att fylla i någon fält eller kontrollera om den id som du försöker sätta nu, finns redan!";
                TempData["CssClass"] = "alert-danger";
                ViewBag.ChannelId = new SelectList(db.Channels, "Id", "Name", programs.ChannelId);
                return View(programs);
            }

            
        }

        // GET: Programs/Edit/5
        [AuthorizeRolesAttribute("Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Programs programs = db.Programs.Find(id);
            if (programs == null)
            {
                return HttpNotFound();
            }
            ViewBag.ChannelId = new SelectList(db.Channels, "Id", "Name", programs.ChannelId);
            return View(programs);
        }

        // POST: Programs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ChannelId,Title,Date,Time,Duration,Category,Resume")] Programs programs)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (programs.Id < 76)
                    {
                        db.Entry(programs).State = EntityState.Modified;
                        db.SaveChanges();
                        TempData["message"] = "Allt är klart! Programmet är redigerat!";
                        TempData["CssClass"] = "alert-success";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        if (DateTime.Now.TimeOfDay < programs.Time)
                        {
                            db.Entry(programs).State = EntityState.Modified;
                            db.SaveChanges();
                            TempData["message"] = "Allt är klart! Nyhetspuffen är redigerad!";
                            TempData["CssClass"] = "alert-success";
                            return RedirectToAction("IndexPuffar");
                        }
                        else
                        {
                            TempData["message"] = "Tiden för den nyheten är passerad. Du måste välja någon tid som är inte passerad än!";
                            TempData["CssClass"] = "alert-danger";
                            return RedirectToAction("IndexPuffar");
                        }
                            
                       
                    }
                    
                }
                catch (Exception)
                {
                    if (programs.Id < 76)
                    {
                        TempData["message"] = "Något gick fel! Problemmet kan det vara så att du har inte rensat ett fällt ordentligt. När sidan öppnas fylls Programmets namn (Title), Kategori (Category) och Beskrivning (Resume) fällten automatisk med intervaller. Prova att rensa fälten" +
                        " från intervallerna så att det inte finns någon tom plats efter ordet som du skriver och prova igen att redigera!";
                        TempData["CssClass"] = "alert-danger";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["message"] = "Något gick fel! Problemmet kan det vara så att du har inte rensat ett fällt ordentligt. När sidan öppnas fylls Programmets namn (Title), Kategori (Category) och Beskrivning (Resume) fällten automatisk med intervaller. Prova att rensa fälten" +
                        " från intervallerna så att det inte finns någon tom plats efter ordet som du skriver och prova igen att redigera!";
                        TempData["CssClass"] = "alert-danger";
                        return RedirectToAction("IndexPuffar");
                    }
                   
                }
                
            }
            else
            {
                TempData["message"] = "Något gick fel! Kontrolera om du har inte glömt att fylla i någon fält!";
                TempData["CssClass"] = "alert-danger";
                ViewBag.ChannelId = new SelectList(db.Channels, "Id", "Name", programs.ChannelId);
                return View(programs);
            }
            
        }

        // GET: Programs/Delete/5
        [AuthorizeRolesAttribute("Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Programs programs = db.Programs.Find(id);
            if (programs == null)
            {
                return HttpNotFound();
            }
            return View(programs);
        }

        // POST: Programs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
           
            Programs programs = db.Programs.Find(id);
            if (programs.Id < 76)
            {
                db.Programs.Remove(programs);
                db.SaveChanges();
                TempData["message"] = "Allt är klart! Programmet är bortagen!";
                TempData["CssClass"] = "alert-success";
                return RedirectToAction("Index");
            }
            else
            {
                db.Programs.Remove(programs);
                db.SaveChanges();
                TempData["message"] = "Allt är klart! nyhetspuffen är bortagen!";
                TempData["CssClass"] = "alert-success";
                return RedirectToAction("IndexPuffar");
            }
            
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
