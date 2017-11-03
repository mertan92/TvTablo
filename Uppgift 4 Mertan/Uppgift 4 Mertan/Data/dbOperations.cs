﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Uppgift_4_Mertan.Models.db;
using System.Data.Entity;

namespace Uppgift_4_Mertan.Data
{
    public class dbOperations
    {
        private TvTabloEntities2 db = new TvTabloEntities2();
        

        public string GetDate(string dag)
        {
            var date = "";
            if (dag == null || dag == "idag")
            {
                 date = DateTime.Now.Date.ToShortDateString();       //Sledvashtata st1pka e da napravq programata za cqlata sedmica s razlini dati. DateTime.Now.Date +1 shte napravq po nqkak1v na4in.
            }
            else if (dag == "imorgon")
            {
                 date = DateTime.Now.Date.AddDays(1).ToShortDateString();
            }
            else if (dag == "3Day")
            {
                date = DateTime.Now.Date.AddDays(2).ToShortDateString();
            }
            else if (dag == "4Day")
            {
                date = DateTime.Now.Date.AddDays(3).ToShortDateString();
            }
            else if (dag == "5Day")
            {
                date = DateTime.Now.Date.AddDays(4).ToShortDateString();
            }
            else if (dag == "6Day")
            {
                date = DateTime.Now.Date.AddDays(5).ToShortDateString();
            }
            else if (dag == "7Day")
            {
                date = DateTime.Now.Date.AddDays(6).ToShortDateString();
            }
            return date;
        }

        public List<Programs> GetProgramsDESC()
        {
            var program = db.Programs.Include(s => s.Channels);
            return program.OrderByDescending(i => i.Id).ToList();
        }

        public List<Programs> GetProgramsASC()
        {
            var program = db.Programs.Include(s => s.Channels);
            return program.ToList();
        }


    }
}