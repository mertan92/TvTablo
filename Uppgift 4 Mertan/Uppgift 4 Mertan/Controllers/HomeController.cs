using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Uppgift_4_Mertan.Data;
using Uppgift_4_Mertan.Models.db;
using System.Data.Entity;
using Uppgift_4_Mertan.Security;

namespace Uppgift_4_Mertan.Controllers
{
    public class HomeController : Controller
    {
        private dbOperations dOp = new dbOperations();
        private TvTabloEntities2 db = new TvTabloEntities2();
       


        public ActionResult Index()
        {
            var prog = dOp.GetProgramsDESC();
            for (int i = 0; i < prog.Count; i++)
            {
                prog[i].Date = dOp.GetDate(null);
            }
            return View(prog.ToList());
        }

        public ActionResult Details(int? id, string datum)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Programs prog = db.Programs.Find(id);
            if (prog == null)
            {
                return HttpNotFound();
            }
            if (datum == null)
            {
                prog.Date = dOp.GetDate(datum);
            }
            else
            {
                prog.Date = datum;
            }
            return View(prog);
        }

        public ActionResult KanalProgram (string LinkText)
        {
            var prog = dOp.GetProgramsASC();
            List<Programs> newProgramList = new List<Programs>();

            if (LinkText == "ProgramSVT1")
            {
                foreach (var item in prog)
                {
                    if (item.ChannelId == 1)
                    {
                        newProgramList.Add(item);
                    }
                }
                return View(newProgramList);
            }
            else if (LinkText == "ProgramTV3")
            {
                foreach (var item in prog)
                {
                    if (item.ChannelId == 2)
                    {
                        newProgramList.Add(item);
                    }
                }
                return View(newProgramList);
            }
            else if (LinkText == "ProgramTV4")
            {
                foreach (var item in prog)
                {
                    if (item.ChannelId == 3)
                    {
                        newProgramList.Add(item);
                    }
                }
                return View(newProgramList);
            }
            else if (LinkText == "ProgramSjaun")
            {
                foreach (var item in prog)
                {
                    if (item.ChannelId == 4)
                    {
                        newProgramList.Add(item);
                    }
                }
                return View(newProgramList);
            }
            else if (LinkText == "ProgramTV8")
            {
                foreach (var item in prog)
                {
                    if (item.ChannelId == 5)
                    {
                        newProgramList.Add(item);
                    }
                }
                return View(newProgramList);
            }
            else
            {
                return HttpNotFound();
            }
        }

        public ActionResult Tablo(string LinkText)
        {
           
            var prog = dOp.GetProgramsASC();
            for (int i = 0; i < prog.Count; i++)
            {
                prog[i].Date = dOp.GetDate(LinkText);
            }
            return View(prog);
        }

        [AuthorizeRolesAttribute("Admin")]
        public ActionResult Admin()
        {
            ViewBag.Message = "Här du kan lägga till, redigera och ta bort användarens konto, programmer och nyhetspuffar ";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize]
        public ActionResult PersonligTablo()
        {
            var programs = dOp.GetProgramsASC();
            List<Programs> choosenPrograms = new List<Programs>();
            var users_Chanels = db.Users_Chanels.Include(u => u.Channels).Include(u => u.Users).Where(x => x.Users.Username == User.Identity.Name);
            string[] chanelArray = new string[5];
            int counter = 0;

            foreach (var prog in programs)
            {
                foreach (var item in users_Chanels)
                {
                    if (prog.Channels.Name == item.Channels.Name)
                    {

                        if (prog.Id < 76)
                        {
                            choosenPrograms.Add(prog);
                        }
                    }
                }
            }

            //foreach (var item in users_Chanels)
            //{
            //    try
            //    {
            //        chanelArray[counter] = item.Channels.Name;
            //        counter++;
            //    }
            //    catch (Exception)
            //    {

            //    }

            //}

            ViewBag.Kanal = users_Chanels;
            return View(choosenPrograms);
        }

        [Authorize]
        public ActionResult LogIn()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}