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

    public partial class Users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Users()
        {
            this.Users_Chanels = new HashSet<Users_Chanels>();
        }
    
        public int Id { get; set; }
        [Required(ErrorMessage = "Du måste fylla i din 'Användarnamn'")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Du måste fylla i din 'Lösenord'")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public int RoleId { get; set; }
    
        public virtual Roles Roles { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Users_Chanels> Users_Chanels { get; set; }
    }
}
