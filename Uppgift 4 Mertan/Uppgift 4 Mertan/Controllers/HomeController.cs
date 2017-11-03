using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Uppgift_4_Mertan.Data;
using Uppgift_4_Mertan.Models.db;
using System.Data.Entity;

namespace Uppgift_4_Mertan.Controllers
{
    public class HomeController : Controller
    {
        private dbOperations dOp = new dbOperations();
        private TvTabloEntities2 db2 = new TvTabloEntities2();
       


        public ActionResult Index()
        {
            var prog = dOp.GetProgramsDESC();
            for (int i = 0; i < prog.Count; i++)
            {
                prog[i].Date = dOp.GetDate(null);
            }
            return View(prog);
        }

        public ActionResult Details(int? id, string datum)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Programs prog = db2.Programs.Find(id);
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

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}