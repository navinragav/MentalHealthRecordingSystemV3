using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Web2.Models;

using System.Web.Security;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using System.Data.SqlClient;

namespace Web2.Controllers
{
    public class LogClass1Controller : Controller
    {
        string un,pwd;
       
        private LoginDB db = new LoginDB();

        // GET: LogClass1
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
                return View(db.LogClass1s.ToList());
            }
            //return View(db.LogClass1s.ToList());
        }

        [HttpGet]
        public ActionResult LoginPage()
        {
            return View();
        }

        [HttpPost]
        [ActionName("LoginPage")]
        public ActionResult Loginpage_Post()
        {                       
            un = Request["UserName"];
            pwd = Request["Password"];
            //if(un=="Admin" && pwd=="123")  //static code

            //if (isvalidx(un, pwd))   //ado.net code
            if(IsvalidUser(un, pwd))    //linq query Code
            {                
                string r1 = (from s in db.LogClass1s where s.UserName == un select s.Role).First().ToString();  
                System.Web.HttpContext.Current.Session["rol"] = r1;
                System.Web.HttpContext.Current.Session["uns"] = un;

                if (r1 == "Admin")
                {
                    return RedirectToAction("Create");
                }
                if (r1 == "Secretary")
                {
                    return RedirectToAction("Index","SecClass1");
                }
                if (r1 == "Nurse")
                {
                    return RedirectToAction("Index", "NurseClass1");
                }
                if (r1 == "Consultant")
                {
                    return RedirectToAction("Index", "ConsultantClass1");
                }
                //return RedirectToAction("Index");
            }
            else
            {
                ViewBag.logerr = "Invalid UserName";
            }
            return View();
        }




        private bool IsvalidUser(string User, string Pass)
        {
            var q = from s in db.LogClass1s where s.UserName == User && s.Password == Pass select s;  //linq login
            if (q.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
        public ActionResult signout()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            FormsAuthentication.SignOut();
            Session.Abandon();
            Session["uns"] = "";

            return RedirectToAction("LoginPage", "LogClass1");

        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        //dont use this
        //public bool isvalidx(string user, string pass)
        //{
            //SqlConnection con = new SqlConnection("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=aspnet-Web2-20171121075754;Integrated Security=True");
            //string str = "select * from LogClass1 where UserName=@UserName and Password=@Password";
            //SqlCommand cmd = new SqlCommand(str, con);
            //cmd.Parameters.AddWithValue("@UserName", user);
            //cmd.Parameters.AddWithValue("@Password", pass);
            ////con.Close();
            //con.Open();
            //SqlDataReader sr = cmd.ExecuteReader();
            //if (sr.Read() == true)
            //{
            //    if (sr["UserName"].ToString() == user)
            //    {
            //        string role = sr["Role"].ToString();
            //        if (role == "0") { role = "Admin"; }
            //        if (role == "1") { role = "Secretary"; }
            //        if (role == "2") { role = "Nurse"; }
            //        if (role == "3") { role = "Consultant"; }
            //        System.Web.HttpContext.Current.Session["rol"] = role;
            //        System.Web.HttpContext.Current.Session["uns"] = user;
            //    }
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
            //con.Close();

        //}

        // GET: LogClass1/Details/5
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
                LogClass1 logClass1 = db.LogClass1s.Find(id);
                if (logClass1 == null)
                {
                    return HttpNotFound();
                }
                return View(logClass1);
            }            
        }

        // GET: LogClass1/Create
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

        // POST: LogClass1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserName,Password,Confirm_Password,Role")] LogClass1 logClass1)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.LogClass1s.Add(logClass1);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }catch (Exception e1) { ViewBag.err1 = "User Already Exists"; ViewBag.err2 = e1.Message.ToString(); }
            return View(logClass1);
            
        }

        // GET: LogClass1/Edit/5
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
                LogClass1 logClass1 = db.LogClass1s.Find(id);
                if (logClass1 == null)
                {
                    return HttpNotFound();
                }
                return View(logClass1);
            }
                
        }

        // POST: LogClass1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserName,Password,Confirm_Password,Role")] LogClass1 logClass1)
        {
            try
            { 
            if (ModelState.IsValid)
            {
                db.Entry(logClass1).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }            
            }catch (Exception e1) { ViewBag.err1 = "User Already Exists"; ViewBag.err2 = e1.Message.ToString(); }
            return View(logClass1);
        }

        // GET: LogClass1/Delete/5
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
                LogClass1 logClass1 = db.LogClass1s.Find(id);
                if (logClass1 == null)
                {
                    return HttpNotFound();
                }
                return View(logClass1);
            }
        }

        // POST: LogClass1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LogClass1 logClass1 = db.LogClass1s.Find(id);
            db.LogClass1s.Remove(logClass1);
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
