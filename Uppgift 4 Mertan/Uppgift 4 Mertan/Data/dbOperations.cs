using System;
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
                 date = DateTime.Now.Date.ToShortDateString();      
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

        public bool CheckUserCredentials(string username, string password)
        {
            var user = db.Users.Where(x => x.Username.Equals(username) && x.Password.Equals(password));

            if (user.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsUserInRole(string userName, string roleName)
        {
            var role = db.Users
            .Where(x => x.Username.Equals(userName))
            .Include(x => x.Roles).Where(x => x.Roles.Role.Equals(roleName));

            return role.Any();
        }

        public int GetUserChannelsNextId()
        {
            var listUserChannelsId = db.Users_Chanels;
            int userChannelsLastId = 0;
            if (listUserChannelsId.Count() != 0)
            {
                foreach (var item in listUserChannelsId)
                {
                    userChannelsLastId = item.Id;
                }
            }
            
            return userChannelsLastId + 1;
        }


        //public List<Programs> GetProgramsForPersonligTablo()
        //{
        //    var program = db.Programs.Include(x => x.Channels.Users_Chanels);
        //    //.Where(s => s.Users.Username == User.Identity.Name);
        //    return program.ToList();
        //}



    }
}