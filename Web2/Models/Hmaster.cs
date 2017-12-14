using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Web2.Models
{
    public class Hmaster
    {
    }

    public enum Psychologist
    {
        Psychologist
    }
    public enum Grade
    {
        Staff_Nurse,
        Nursing_Manager
    }
    public class Consultant
    {
        [Key]
        public int cid { get; set; }

        // [Index("cname", IsUnique = true)]
        [Display(Name = "Consultant")]
        [Required(ErrorMessage = "Enter Consultant Name")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Name Must be min 3 to 20 Characters")]
        [Index("cname", IsUnique = true)]
        public string cname { get; set; }

        [Display(Name = "Specialist")]
        public Psychologist psychologist { get; set; }

        [Display(Name = "Contact Number")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        public ICollection<Nurse> nurse { get; set; }
        public ICollection<SecClass1> secclass1 { get; set; }
    }
    public class Nurse
    {
        [Key]
        public int nid { get; set; }

        [Required(ErrorMessage = "Enter Nurse Name")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Name Must be min 3 to 20 Characters")]
        //[Index("NurseName", IsUnique = true)]
        [Display(Name = "Nurse Name")]
        [Index("nname", IsUnique = true)]
        public string nname { get; set; }

        [Display(Name = "Grade")]
        public Grade grade { get; set; }

        [Display(Name = "Contact Number")]
        public string Phone { get; set; }

        public int cid { get; set; }
        public Consultant consultant { get; set; }
    }

    public class HospitalDB : DbContext
    {
        public HospitalDB() : base("DefaultConnection")
        {

        }
        public DbSet<Consultant> Consultants { get; set; }
        public DbSet<Nurse> Nurses { get; set; }

    }

}