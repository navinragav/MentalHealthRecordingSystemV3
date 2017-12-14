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
    public class NursesController : Controller
    {
        private HospitalDB db = new HospitalDB();

        // GET: Nurses
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
                var nurses = db.Nurses.Include(n => n.consultant);
                return View(nurses.ToList());
            }
        }


        // GET: Nurses/Details/5
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
                Nurse nurse = db.Nurses.Find(id);
                if (nurse == null)
                {
                    return HttpNotFound();
                }
                return View(nurse);
            }
        }

        // GET: Nurses/Create
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
                ViewBag.cid = new SelectList(db.Consultants, "cid", "cname");
                return View();
            }
        }

        // POST: Nurses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "nid,nname,grade,Phone,cid")] Nurse nurse)
        {
            try
            { 
            if (ModelState.IsValid)
            {
                db.Nurses.Add(nurse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cid = new SelectList(db.Consultants, "cid", "cname", nurse.cid);
            }catch (Exception e1) { ViewBag.err1 = "User Already Exists"; ViewBag.err2 = e1.Message.ToString(); }
            return View(nurse);
        }

        // GET: Nurses/Edit/5
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
                Nurse nurse = db.Nurses.Find(id);
                if (nurse == null)
                {
                    return HttpNotFound();
                }
                ViewBag.cid = new SelectList(db.Consultants, "cid", "cname", nurse.cid);
                return View(nurse);
            }
        }

        // POST: Nurses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "nid,nname,grade,Phone,cid")] Nurse nurse)
        {
            try
            { 
            if (ModelState.IsValid)
            {
                db.Entry(nurse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cid = new SelectList(db.Consultants, "cid", "cname", nurse.cid);
            }
            catch (Exception e1) { ViewBag.err1 = "User Already Exists"; ViewBag.err2 = e1.Message.ToString(); }
            return View(nurse);
        }

        // GET: Nurses/Delete/5
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
                Nurse nurse = db.Nurses.Find(id);
                if (nurse == null)
                {
                    return HttpNotFound();
                }
                return View(nurse);
            }
        }

        // POST: Nurses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Nurse nurse = db.Nurses.Find(id);
            db.Nurses.Remove(nurse);
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
