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
    public class FollowClass1Controller : Controller
    {
        private FollowDB db = new FollowDB();

        private HospitalDB db1 = new HospitalDB();


        private SecDB db2 = new SecDB();


        // GET: FollowClass1
        public ActionResult Index()
        {
            ViewBag.user = Session["uns"];
            ViewBag.role = Session["rol"];
            string user = ViewBag.user;
            string role = ViewBag.role;
            if ((Session["uns"] == null) || (Session["rol"].ToString() == "Admin") || (Session["rol"].ToString() == "Nurse") || (Session["rol"].ToString() == "Consultant"))
            {
                return RedirectToAction("LoginPage", "LogClass1");
            }
            else
            {
                List<FollowClass1> list1 = new List<FollowClass1>();

                IQueryable<FollowClass1> q = from s in db.FollowClass1s where s.Status == 0 select s;
                foreach (var i in q)
                {
                    list1.Add(i);
                }
                return View(list1);
                //return View(db.FollowClass1s.ToList());
            }

        }


        [HttpPost]
        [ActionName("Index")]  //search
        public ActionResult Post_Index()
        {
            List<FollowClass1> list1 = new List<FollowClass1>();
            try
            {

                ViewBag.user = Session["uns"];
                ViewBag.role = Session["rol"];

                string pid = Request["pid"];
                string pname = Request["pname"];
                if (pid != "")
                {
                    int pid1 = Convert.ToInt32(Request["pid"].ToString());
                    IQueryable<FollowClass1> q = from s in db.FollowClass1s where s.PatientId == pid1 select s;
                    foreach (var i in q)
                    {
                        list1.Add(i);
                    }
                    ViewBag.pid = pid;
                }
                else if (pname != "")
                {
                    IQueryable<FollowClass1> q = from s in db.FollowClass1s where s.pname == pname select s;
                    foreach (var i in q)
                    {
                        list1.Add(i);
                    }
                    ViewBag.pname = pname;
                }
                else if ((pid == "") && (pname == ""))
                {
                    IQueryable<FollowClass1> q = from s in db.FollowClass1s select s;
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

        // GET: FollowClass1/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.user = Session["uns"];
            ViewBag.role = Session["rol"];
            string user = ViewBag.user;
            string role = ViewBag.role;
            if ((Session["uns"] == null) || (Session["rol"].ToString() == "Admin") || (Session["rol"].ToString() == "Nurse") || (Session["rol"].ToString() == "Consultant"))
            {
                return RedirectToAction("LoginPage", "LogClass1");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                FollowClass1 followClass1 = db.FollowClass1s.Find(id);
                if (followClass1 == null)
                {
                    return HttpNotFound();
                }
                return View(followClass1);
            }
        }

        // GET: FollowClass1/Create
        public ActionResult Create()
        {
            ViewBag.user = Session["uns"];
            ViewBag.role = Session["rol"];
            string user = ViewBag.user;
            string role = ViewBag.role;
            if ((Session["uns"] == null) || (Session["rol"].ToString() == "Admin") || (Session["rol"].ToString() == "Nurse") || (Session["rol"].ToString() == "Consultant"))
            {
                return RedirectToAction("LoginPage", "LogClass1");
            }
            else
            {
                return View();
            }
        }

        // POST: FollowClass1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "fid,PatientId,pname,Appointment,AppointmentTime,DOB,Gender,Status,cname")] FollowClass1 followClass1)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.FollowClass1s.Add(followClass1);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            catch (Exception e1) { ViewBag.err2 = e1.Message.ToString(); }
            return View(followClass1);
        }

        // GET: FollowClass1/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.user = Session["uns"];
            ViewBag.role = Session["rol"];
            string user = ViewBag.user;
            string role = ViewBag.role;
            if ((Session["uns"] == null) || (Session["rol"].ToString() == "Admin") || (Session["rol"].ToString() == "Nurse") || (Session["rol"].ToString() == "Consultant"))
            {
                return RedirectToAction("LoginPage", "LogClass1");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                FollowClass1 followClass1 = db.FollowClass1s.Find(id);
                if (followClass1 == null)
                {
                    return HttpNotFound();
                }
                return View(followClass1);
            }
        }

        // POST: FollowClass1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "fid,PatientId,pname,Appointment,AppointmentTime,DOB,Gender,Status,cname")] FollowClass1 followClass1)
        {
            // try
            // {
            object o17 = followClass1.Status; string oo17 = o17.ToString();
            if (ModelState.IsValid)
            {
                if (oo17 == "Followup_Inbox")
                {
                    db.Entry(followClass1).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    SecClass1 sc1 = new SecClass1();
                    //int PatientIdx = db2.SecClass1s.Max(u => u.PatientId);

                    sc1.PatientId = followClass1.PatientId;
                    sc1.pname = followClass1.pname;
                    sc1.Appointment = followClass1.Appointment;
                    sc1.AppointmentTime = Convert.ToDateTime(followClass1.AppointmentTime);
                    sc1.DOB = followClass1.DOB;
                    if (followClass1.Gender == "Male") { sc1.Gender = Gender.Male; }
                    if (followClass1.Gender == "Female") { sc1.Gender = Gender.Female; }

                    object o8 = followClass1.cname;
                    string oo8 = o8.ToString();

                    string sc = (from s in db1.Consultants where s.cname == oo8 select s.cid).First().ToString();
                    sc1.cid = Convert.ToInt32(sc);


                    string surname = (from s1 in db2.SecClass1s where s1.PatientId == followClass1.PatientId select s1.SurName).First().ToString();
                    sc1.SurName = surname.ToString();

                    string religion = (from s1 in db2.SecClass1s where s1.PatientId == followClass1.PatientId select s1.Religion).First().ToString();
                    if (religion == "Christian") { sc1.Religion = Religion.Christian; }
                    if (religion == "Hindu") { sc1.Religion = Religion.Hindu; }
                    if (religion == "Muslim") { sc1.Religion = Religion.Muslim; }

                    string ethinic = (from s1 in db2.SecClass1s where s1.PatientId == followClass1.PatientId select s1.EthnicOrigin).First().ToString();
                    if (ethinic == "African") { sc1.EthnicOrigin = EthiniOrigin.African; }
                    if (ethinic == "Asian") { sc1.EthnicOrigin = EthiniOrigin.Asian; }
                    if (ethinic == "European") { sc1.EthnicOrigin = EthiniOrigin.European; }
                    if (ethinic == "Indian") { sc1.EthnicOrigin = EthiniOrigin.Indian; }

                    string marital = (from s1 in db2.SecClass1s where s1.PatientId == followClass1.PatientId select s1.Maritalstatus).First().ToString();
                    if (marital == "Divorce") { sc1.Maritalstatus = Marital.Divorce; }
                    if (marital == "Married") { sc1.Maritalstatus = Marital.Married; }
                    if (marital == "Single") { sc1.Maritalstatus = Marital.Single; }
                    if (marital == "Widow") { sc1.Maritalstatus = Marital.Widow; }

                    string mobile = (from s1 in db2.SecClass1s where s1.PatientId == followClass1.PatientId select s1.ContactNumber).First().ToString();
                    sc1.ContactNumber = mobile.ToString();



                    db2.SecClass1s.Add(sc1);
                    db2.SaveChanges();              //data send to secretary


                    db.Entry(followClass1).State = EntityState.Modified;
                    db.SaveChanges();               // update to followup

                    return RedirectToAction("Index");
                }

            }
            //}
            //catch (Exception e1) { ViewBag.err2 = e1.Message.ToString(); }
            return View(followClass1);
            //if (ModelState.IsValid)
            //{
            //    db.Entry(followClass1).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //return View(followClass1);
        }

        // GET: FollowClass1/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.user = Session["uns"];
            ViewBag.role = Session["rol"];
            string user = ViewBag.user;
            string role = ViewBag.role;
            if ((Session["uns"] == null) || (Session["rol"].ToString() == "Admin") || (Session["rol"].ToString() == "Nurse") || (Session["rol"].ToString() == "Consultant"))
            {
                return RedirectToAction("LoginPage", "LogClass1");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                FollowClass1 followClass1 = db.FollowClass1s.Find(id);
                if (followClass1 == null)
                {
                    return HttpNotFound();
                }
                return View(followClass1);
            }
        }

        // POST: FollowClass1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FollowClass1 followClass1 = db.FollowClass1s.Find(id);
            db.FollowClass1s.Remove(followClass1);
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
