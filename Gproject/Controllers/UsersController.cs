using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using Gproject.Models;
using Microsoft.Ajax.Utilities;

namespace Gproject.Controllers
{
    public class UsersController : Controller
    {
        GprojectEntities db = new GprojectEntities();
        getdata d = new getdata();
        

        // GET: Users
        public ActionResult Index()
        {
            ViewBag.Name = System.Web.HttpContext.Current.Session["Name"].ToString();
            int lab =(int)System.Web.HttpContext.Current.Session["level"];
            List<getdata> Data = (db.Courses.Where(a => a.labNumber == lab).Select(
              x => new getdata
              {
                 course = x.courseName,
                  DayName = x.Timetable.tName,
                  LabName = x.Lab.labName,
                  floor = x.Floor.floorName
              }
              )).ToList();
            //var dta = d.getData(lab);
            //int i = Data.Count();
            if (lab != 1) 
            {
                return RedirectToAction("Admin");
            }
            else
            {
                
                return View(Data);
            }
            

        }
        [HttpGet]
        public ActionResult Login()
        {
           
            return View();
        }
        [HttpPost]
        public JsonResult Login(string username,string pass)
        {
            string result;
            var data = db.Users.Where(x => x.userName == username && x.password == pass).FirstOrDefault();
            if (data != null) 
            {
                Session["role"] = data.roleNumber;
                Session["User"] = data.userName;
                Session["name"] = data.Name;
                Session["level"] = data.levelNumber;
                int lab = (int)System.Web.HttpContext.Current.Session["Role"];
                if (lab==1)
                {
                    result = "student";
                }
                else
                {
                    result = "doctor";
                }
             
            }
            else
            {
                result = "error";
                
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Register()
        {
            ViewBag.levels = new SelectList(db.Levels, "levelNumber", "levelName");
            ViewBag.Roles = new SelectList(db.Roles, "roleNumber", "roleName");
            ViewBag.Types = new SelectList(db.Types, "typeNumber", "typeName");
            ViewBag.Governate = new SelectList(db.Governates, "governateNumber", "governateName");
            return View();
        }
        [HttpPost]
        public JsonResult Register(User data)
        {
            bool result = true;
            User us = new User();
            if (db.Users.Any(o => o.userName.ToLower() == data.userName.ToLower()))
            {
                result = false;
            }
            else
            {
                us.userName = data.userName.ToLower();
                us.Name = data.Name;
                us.levelNumber = data.levelNumber;
                us.password = data.password;
                us.roleNumber = 1;
                us.typeNumber = data.typeNumber;
                us.governateNumber = data.governateNumber;
                db.Users.Add(us);
                db.SaveChanges();
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Admin()
        {
            
            return View();

        }

        public ActionResult AllUsers()
        {
            int lab;
            try
            {
                 lab = (int)System.Web.HttpContext.Current.Session["Role"];
                if (lab == 1 || lab == 0)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    return View(db.Users.ToList());
                }
              
            }
            catch (Exception)
            {
                return RedirectToAction("Login");
            }
            

        }

        [HttpGet]
        public ActionResult AddTeachingMember()
        {
            ViewBag.levels = new SelectList(db.Levels, "levelNumber", "levelName");
            ViewBag.Roles = new SelectList(db.Roles, "roleNumber", "roleName");
            ViewBag.Types = new SelectList(db.Types, "typeNumber", "typeName");
            ViewBag.Governate = new SelectList(db.Governates, "governateNumber", "governateName");
            return View();
        }
        [HttpPost]
        public JsonResult AddTeachingMember(User data)
        {
            bool result = true;
            User us = new User();
            if (db.Users.Any(o => o.userName.ToLower() == data.userName.ToLower()))
            {
                result = false;
            }
            else
            {
                us.userName = data.userName.ToLower();
                us.Name = data.Name;
                us.levelNumber = data.levelNumber;
                us.password = data.password;
                us.roleNumber =data.roleNumber;
                us.typeNumber = data.typeNumber;
                us.governateNumber = data.governateNumber;
                db.Users.Add(us);
                db.SaveChanges();
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult EditUser(string id)
        {
            int role;
            try
            {
                role = (int)System.Web.HttpContext.Current.Session["Role"];
                if (role == 1 || role == 0)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.levels = new SelectList(db.Levels, "levelNumber", "levelName");
                    ViewBag.Roles = new SelectList(db.Roles, "roleNumber", "roleName");
                    ViewBag.Types = new SelectList(db.Types, "typeNumber", "typeName");
                    ViewBag.Governate = new SelectList(db.Governates, "governateNumber", "governateName");
                    User us = db.Users.Find(id);
                    return View(us);
                }
            }
            catch(Exception)
            {
                return RedirectToAction("Login");
            }
             
        }
         public ActionResult logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

    }
   
}