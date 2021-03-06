﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Uppgift_4_Mertan.Models.db
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Programs
    {
        [Required(ErrorMessage = "Du måste fylla i 'Id' (bara siffror) för programmet")]
        public int Id { get; set; }
        public int ChannelId { get; set; }
        [Required(ErrorMessage = "Du måste fylla i 'Namn' för programmet")]
        public string Title { get; set; }
        public string Date { get; set; }
        [Required(ErrorMessage = "Du måste fylla i 'Tid' (bara siffror med : mellan siffrorna) för programmet")]
        public System.TimeSpan Time { get; set; }
        public Nullable<System.TimeSpan> Duration { get; set; }
        public string Category { get; set; }
        public string Resume { get; set; }
    
        public virtual Channels Channels { get; set; }
    }
}
