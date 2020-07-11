using System;
using System.Collections.Generic;
using System.Configuration.Internal;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Gproject.Models;

namespace Gproject.Controllers
{
    public class courseController : Controller
    {

        // GET: course
        GprojectEntities db = new GprojectEntities();
        public ActionResult Index()
        {
            return View(db.Courses.ToList());
        }
        [HttpGet]
        public ActionResult add()
        {
            ViewBag.levels = new SelectList(db.Levels, "levelNumber", "levelName");
            ViewBag.day = new SelectList(db.Timetables, "tNumber", "tName");
            ViewBag.labs = new SelectList(db.Labs, "labNumber", "labName");
            ViewBag.Floor = new SelectList(db.Floors, "floorNumber", "floorName");
            return View();
        }
        [HttpPost]
        public JsonResult add(Cours data)
        {
            bool result = true;
            Cours c = new Cours();
            //int a = db.Courses.Max(x => x.courseNumber);
            
            c.courseName = data.courseName;
            c.Description = "Test";
            c.levelNumber = data.levelNumber;
            c.labNumber = data.labNumber;
            c.tNumber = data.tNumber;
            c.floorNumber = data.floorNumber;
            db.Courses.Add(c);
            try
            {
                db.SaveChanges();

            }
            catch (Exception ex)
            {
              string r= ex.Message;
                result = false;

            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.levels = new SelectList(db.Levels, "levelNumber", "levelName");
            ViewBag.day = new SelectList(db.Timetables, "tNumber", "tName");
            ViewBag.labs = new SelectList(db.Labs, "labNumber", "labName");
            ViewBag.Floor = new SelectList(db.Floors, "floorNumber", "floorName");
            Cours cou = db.Courses.Find(id);
            return View(cou);
        }
        [HttpPost]
        public JsonResult Edit(Cours data)
        {
            bool result = true;
            Cours cou = db.Courses.Find(data.courseNumber);
            cou.courseName = data.courseName;
            cou.Description = data.Description;
            cou.levelNumber = data.levelNumber;
            cou.labNumber = data.labNumber;
            cou.tNumber = data.tNumber;
            db.Entry(cou).State = EntityState.Modified;
            try
            {
                db.SaveChanges();

            }
            catch (Exception ex)
            {

               result = false;

            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
         [HttpPost]
         public JsonResult delete(int id)
        {
            bool result = false;
            Cours cou = db.Courses.Find(id);
            db.Courses.Remove(cou);
            if (cou != null) 
            {
                db.SaveChanges();
                 result=true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);


        }
        [HttpGet]
        public ActionResult test()
        {
            return View();
        }

    }
}

