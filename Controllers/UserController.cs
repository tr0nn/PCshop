using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PCshop_.Models;

namespace PCshop_.Controllers
{
    
    public class UserController : Controller
    {
        MyConnectionDataContext db;
        void CheckConnection()
        {
            if (db == null) db = new MyConnectionDataContext();
        }
        
        public ActionResult Index(int id)
        {
            CheckConnection();

            registration US = (registration)Session["user"];
            if (US == null)
            {
                ViewBag.error = "please Register";
                return RedirectToAction("Login", "Auth");
            }

            return View(db.registrations.Where(x => x.Id == id).FirstOrDefault());
        }
    }
}