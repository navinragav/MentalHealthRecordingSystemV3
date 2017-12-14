using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Web2.Models
{
    public enum Roles
    {
        Admin,
        Secretary,
        Nurse,
        Consultant
    }
    public class LogClass1
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter User Name")]
        [Display(Name = "User Name")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Name Must be min 4 to 20 Characters")]
        [Index("UserName", IsUnique = true)]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Compare("Password")]
        [Required]
        [DataType(DataType.Password)]
        public string Confirm_Password { get; set; }

        [Display(Name = "Role")]
        [Required]
        public Roles Role { get; set; }
    }
    public class LoginDB : DbContext
    {
        public LoginDB() : base("DefaultConnection")
        {

        }
        public DbSet<LogClass1> LogClass1s { get; set; }

    }
}