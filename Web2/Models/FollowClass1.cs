using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Web2.Models
{
    public enum FStatus
    {
        Followup_Inbox, Next_Appointment
    }
    public class FollowClass1
    {
        [Key]
        public int fid { get; set; }

        [Display(Name = "Patient ID")]
        public int PatientId { get; set; }

        [Display(Name = "Patient Name")]
        public string pname { get; set; }

        [Display(Name = "Appointment Date")]
        public string Appointment { get; set; }

        [Display(Name = "Appointment Time")]
        public string AppointmentTime { get; set; }

        [Display(Name = "Date of Birth")]
        public string DOB { get; set; }

        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Display(Name = "Status")]
        public FStatus Status { get; set; }

        [Display(Name = "Consultant Name")]
        public string cname { get; set; }
    }
    public class FollowDB : DbContext
    {
        public FollowDB() : base("DefaultConnection")
        {

        }
        public DbSet<FollowClass1> FollowClass1s { get; set; }
    }
}