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
    public class ConsultantsController : Controller
    {
        private HospitalDB db = new HospitalDB();

        // GET: Consultants
        public ActionResult Index()
        {
            ViewBag.user = Session["uns"];
            ViewBag.role = Session["rol"];
            if ((Session["uns"] == null) || (Session["rol"].ToString() == "Secretary") || (Session["rol"].ToString() == "Nurse") || (Session["rol"].ToString() == "Consultant"))
            {
                return RedirectToAction("LoginPage", "LogClass1");
            }
            else
            {
                return View(db.Consultants.ToList());
            }
        }



        // GET: Consultants/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.user = Session["uns"];
            ViewBag.role = Session["rol"];
            if ((Session["uns"] == null) || (Session["rol"].ToString() == "Secretary") || (Session["rol"].ToString() == "Nurse") || (Session["rol"].ToString() == "Consultant"))
            {
                return RedirectToAction("LoginPage", "LogClass1");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Consultant consultant = db.Consultants.Find(id);
                if (consultant == null)
                {
                    return HttpNotFound();
                }
                return View(consultant);
            }
        }

        // GET: Consultants/Create
        public ActionResult Create()
        {
            ViewBag.user = Session["uns"];
            ViewBag.role = Session["rol"];
            if ((Session["uns"] == null) || (Session["rol"].ToString() == "Secretary") || (Session["rol"].ToString() == "Nurse") || (Session["rol"].ToString() == "Consultant"))
            {
                return RedirectToAction("LoginPage", "LogClass1");
            }
            else
            {
                return View();
            }
        }

        // POST: Consultants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cid,cname,psychologist,Phone")] Consultant consultant)
        {
            try
            { 
            if (ModelState.IsValid)
            {
                db.Consultants.Add(consultant);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            }
            catch (Exception e1) { ViewBag.err1 = "User Already Exists"; ViewBag.err2 = e1.Message.ToString(); }
            return View(consultant);
        }

        // GET: Consultants/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.user = Session["uns"];
            ViewBag.role = Session["rol"];
            if ((Session["uns"] == null) || (Session["rol"].ToString() == "Secretary") || (Session["rol"].ToString() == "Nurse") || (Session["rol"].ToString() == "Consultant"))
            {
                return RedirectToAction("LoginPage", "LogClass1");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Consultant consultant = db.Consultants.Find(id);
                if (consultant == null)
                {
                    return HttpNotFound();
                }
                return View(consultant);
            }
        }

        // POST: Consultants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cid,cname,psychologist,Phone")] Consultant consultant)
        {
            try
            { 
            if (ModelState.IsValid)
            {
                db.Entry(consultant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            }
            catch (Exception e1) { ViewBag.err1 = "User Already Exists"; ViewBag.err2 = e1.Message.ToString(); }
            return View(consultant);
        }

        // GET: Consultants/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.user = Session["uns"];
            ViewBag.role = Session["rol"];
            if ((Session["uns"] == null) || (Session["rol"].ToString() == "Secretary") || (Session["rol"].ToString() == "Nurse") || (Session["rol"].ToString() == "Consultant"))
            {
                return RedirectToAction("LoginPage", "LogClass1");
            }
            else
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Consultant consultant = db.Consultants.Find(id);
                if (consultant == null)
                {
                    return HttpNotFound();
                }
                return View(consultant);
            }
        }

        // POST: Consultants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Consultant consultant = db.Consultants.Find(id);
            db.Consultants.Remove(consultant);
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
