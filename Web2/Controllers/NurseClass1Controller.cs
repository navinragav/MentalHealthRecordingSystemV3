using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Web2.Models;

namespace Web2.Controllers
{
    public class NurseClass1Controller : Controller
    {
        private NurseDB db = new NurseDB();
        private ConsDB db2 = new ConsDB();

        private HospitalDB  db3 = new HospitalDB();

        // GET: NurseClass1
        public ActionResult Index()
        {
            ViewBag.user = Session["uns"];
            ViewBag.role = Session["rol"];
            string user = ViewBag.user;
            if ((Session["uns"] == null) || (Session["rol"].ToString() == "Admin") || (Session["rol"].ToString() == "Secretary") || (Session["rol"].ToString() == "Consultant"))
            {
                return RedirectToAction("LoginPage", "LogClass1");
            }
            else
            {
                string sc = (from s in db3.Nurses where s.nname  == user select s.consultant.cname).First().ToString();
                List<NurseClass1> list1 = new List<NurseClass1>();
                
                IQueryable<NurseClass1> q = from s in db.NurseClass1s where s.Status == 0 && s.cname==sc  select s;
                foreach (var i in q)
                {
                    list1.Add(i);
                }
                return View(list1);
                //return View(db.NurseClass1s.ToList());
            }
        }

        [HttpPost]
        [ActionName("Index")]  //search
        public ActionResult Post_Index()
        {
            List<NurseClass1> list1 = new List<NurseClass1>();
            try
            { 
            ViewBag.user = Session["uns"];
            ViewBag.role = Session["rol"];
            
            string pid = Request["pid"];
            string pname = Request["pname"];
            if (pid !="")
            {
                int pid1 = Convert.ToInt32(Request["pid"].ToString());
                IQueryable<NurseClass1> q = from s in db.NurseClass1s where s.PatientId == pid1 select s;
                foreach (var i in q)
                {
                    list1.Add(i);
                }
                ViewBag.pid = pid;
            }
            else if(pname != "")
            {              
                IQueryable<NurseClass1> q = from s in db.NurseClass1s where s.pname == pname select s;
                foreach (var i in q)
                {
                    list1.Add(i);
                }
                ViewBag.pname = pname;
            }
            else if((pid=="")&&(pname == ""))
            {
                IQueryable<NurseClass1> q = from s in db.NurseClass1s select s;
                foreach (var i in q)
                {
                    list1.Add(i);
                }
                ViewBag.pid = pid;
            }
            }
            catch (Exception e1) { ViewBag.err1 = "Error"; ViewBag.err2 = e1.Message.ToString(); }
            return View(list1);
        }

        // GET: NurseClass1/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.user = Session["uns"];
            ViewBag.role = Session["rol"];
            if ((Session["uns"] == null) || (Session["rol"].ToString() == "Admin") || (Session["rol"].ToString() == "Secretary") || (Session["rol"].ToString() == "Consultant"))
            {
                return RedirectToAction("LoginPage", "LogClass1");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                NurseClass1 nurseClass1 = db.NurseClass1s.Find(id);
                if (nurseClass1 == null)
                {
                    return HttpNotFound();
                }
                return View(nurseClass1);
            }
        }

        // GET: NurseClass1/Create
        //public ActionResult Create()
        //{
        //    List<SecClass1> list1 = new List<SecClass1>();
        //    ViewBag.user = Session["uns"];
        //    ViewBag.role = Session["rol"];
        //    if ((Session["uns"] == null) || (Session["rol"].ToString() == "Admin") || (Session["rol"].ToString() == "Secretary") || (Session["rol"].ToString() == "Consultant"))
        //    {
        //        return RedirectToAction("LoginPage", "LogClass1");
        //    }
        //    else
        //    {
        //        return View();
        //    }
        //}

        // POST: NurseClass1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "npid,PatientId,pname,Appointment,AppointmentTime,DOB,Gender,PastMedicalHistory,FamilyHistory,MentalStatusExamination,CollateralHistory,NursingCarePlan,AlergicSpecificMedication,Status,cname")] NurseClass1 nurseClass1)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.NurseClass1s.Add(nurseClass1);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(nurseClass1);
        //}

        // GET: NurseClass1/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.user = Session["uns"];
            ViewBag.role = Session["rol"];
            if ((Session["uns"] == null) || (Session["rol"].ToString() == "Admin") || (Session["rol"].ToString() == "Secretary") || (Session["rol"].ToString() == "Consultant"))
            {
                return RedirectToAction("LoginPage", "LogClass1");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                NurseClass1 nurseClass1 = db.NurseClass1s.Find(id);
                if (nurseClass1 == null)
                {
                    return HttpNotFound();
                }
                return View(nurseClass1);
            }
        }

        // POST: NurseClass1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "npid,PatientId,pname,Appointment,AppointmentTime,DOB,Gender,PastMedicalHistory,FamilyHistory,MentalStatusExamination,CollateralHistory,NursingCarePlan,AlergicSpecificMedication,Status,cname")] NurseClass1 nurseClass1)
        {
            try
            {
                object o17 = nurseClass1.Status; string oo17 = o17.ToString();
                if (ModelState.IsValid)
                {
                    if (oo17 == "Nurse_Inbox")
                    {
                        db.Entry(nurseClass1).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ConsultantClass1 cc1 = new ConsultantClass1();
                        cc1.PatientId = nurseClass1.PatientId;
                        cc1.pname = nurseClass1.pname;
                        cc1.Appointment = nurseClass1.Appointment;
                        cc1.AppointmentTime = nurseClass1.AppointmentTime;
                        cc1.DOB = nurseClass1.DOB;
                        cc1.Gender = nurseClass1.Gender;
                        cc1.Observation = "Nil";
                        cc1.cname = nurseClass1.cname;

                        db2.ConsultantClass1s.Add(cc1);
                        db2.SaveChanges();              //data send to consultant


                        db.Entry(nurseClass1).State = EntityState.Modified;
                        db.SaveChanges();               // update to nurse

                        return RedirectToAction("Index");                        
                    }

                }
            }
            catch (Exception e1) { ViewBag.err1 = "User Already Exists"; ViewBag.err2 = e1.Message.ToString(); }
            return View(nurseClass1);
        }

        // GET: NurseClass1/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.user = Session["uns"];
            ViewBag.role = Session["rol"];
            if ((Session["uns"] == null) || (Session["rol"].ToString() == "Admin") || (Session["rol"].ToString() == "Secretary") || (Session["rol"].ToString() == "Consultant"))
            {
                return RedirectToAction("LoginPage", "LogClass1");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                NurseClass1 nurseClass1 = db.NurseClass1s.Find(id);
                if (nurseClass1 == null)
                {
                    return HttpNotFound();
                }
                return View(nurseClass1);
            }
        }

        // POST: NurseClass1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NurseClass1 nurseClass1 = db.NurseClass1s.Find(id);
            db.NurseClass1s.Remove(nurseClass1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
