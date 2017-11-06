﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Uppgift_4_Mertan.Models.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Du måste fylla i din användarnamn")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Du måste fylla i din lösenord")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}