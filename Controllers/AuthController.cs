using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PCshop_.Models;
using System.Text;
using System.Security.Cryptography;
using System.ComponentModel.DataAnnotations;
using PCshop_.Helpers;


namespace PCshop_.Controllers
{
    public class AuthController : Controller
    {
        string AuthSecret = "poiuytrewqasdfghjklmnbvcxz145789";
        MyConnectionDataContext db;  // მონაცემთა ბაზის ტიპის ობიექტი

        void CheckConnection()   // შემოწმება დაკავშირებულია თუ არა
        {
            if (db == null) db = new MyConnectionDataContext();
        }
        //---------------------------------------Login--------------------
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LogModel model)
        {
            CheckConnection();
            //registration არის dbის tableს სახელი
            registration us = db.registrations.Where(x => x.Username == model.UserName &&
            x.Password == MainHelper.MD5hash(model.Password + AuthSecret)).FirstOrDefault();

            if (us == null)
            {
                //-----------------------------აქამდე მოდის კომპილატორი. 
                return Content("view()");
            }
            else
            {
                Session["user"] = us;
                return RedirectToAction("index", "User", new { @id = us.Id });
            }  
        }


        //----------------------------------------------Register----------------------
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel m)
        {

            MyConnectionDataContext db = new MyConnectionDataContext();
            // registration არის db-ს table სახელი
            registration c = new registration();

            c.Username = m.Username;
            c.Email = m.Email;
            if (m.Password != m.Repeat_Password)
            {
                ViewBag.error = "passwords is not the same";
                return View();
            }
            else if ( m.Password.Length < 8 && m.Repeat_Password.Length<8)
            {

                ViewBag.error = " The passwords should be longer than 8 characters";
                return View();
            }
            else
            {
                c.Password = MainHelper.MD5hash(m.Password + AuthSecret);
                c.CreatDate = DateTime.Now;

                db.registrations.InsertOnSubmit(c);
                db.SubmitChanges();
            }
                return RedirectToAction("Login");
            
        }
        public ActionResult ForgPassword()
        {
            ViewBag.error = "Email is not corett form";
            return View();
        }
    }
}