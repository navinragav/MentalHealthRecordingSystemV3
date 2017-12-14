using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Web2.Models
{
    public class testClass1
    {
        [Key]
        public int pid { get; set; }

        [Editable(false)]
        [Required(ErrorMessage = "Enter Patient ID")]
        [Display(Name = "Patient ID")]
        [Index("PatientId", IsUnique = true)]
        public int PatientId { get; set; }

        [Display(Name = "Patient Name")]
        [Required(ErrorMessage = "Enter Patient Name")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Name Must be min 3 to 20 Characters")]
        public string pname { get; set; }

        [Display(Name = "Appointment Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Appointment { get; set; }
    }
    public class testDB : DbContext
    {
        public testDB() : base("DefaultConnection")
        {

        }
        public DbSet<testClass1> testClass1s { get; set; }
    }
}