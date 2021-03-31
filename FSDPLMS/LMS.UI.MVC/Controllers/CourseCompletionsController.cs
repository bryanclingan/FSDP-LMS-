using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LMS.DATA.EF;

namespace LMS.UI.MVC.Controllers
{
    public class CourseCompletionsController : Controller
    {
        private FSDPLMSEntities db = new FSDPLMSEntities();

        // GET: CourseCompletions
        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "Manager")]
        [Authorize(Roles = "HRAdmin")]
        public ActionResult Index()
        {
            var courseCompletions = db.CourseCompletions.Include(c => c.Cours).Include(c => c.UserDetail);
            return View(courseCompletions.ToList());
        }

        // GET: CourseCompletions/Details/5
        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "HRAdmin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseCompletion courseCompletion = db.CourseCompletions.Find(id);
            if (courseCompletion == null)
            {
                return HttpNotFound();
            }
            return View(courseCompletion);
        }

        // GET: CourseCompletions/Create
        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "HRAdmin")]
        public ActionResult Create()
        {
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName");
            ViewBag.UserID = new SelectList(db.UserDetails, "UserID", "FirstName");
            return View();
        }

        // POST: CourseCompletions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "HRAdmin")]
        public ActionResult Create([Bind(Include = "CourseCompletionID,UserID,CourseID,DateCompleted")] CourseCompletion courseCompletion)
        {
            if (ModelState.IsValid)
            {
                db.CourseCompletions.Add(courseCompletion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", courseCompletion.CourseID);
            ViewBag.UserID = new SelectList(db.UserDetails, "UserID", "FirstName", courseCompletion.UserID);
            return View(courseCompletion);
        }

        // GET: CourseCompletions/Edit/5
        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "HRAdmin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseCompletion courseCompletion = db.CourseCompletions.Find(id);
            if (courseCompletion == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", courseCompletion.CourseID);
            ViewBag.UserID = new SelectList(db.UserDetails, "UserID", "FirstName", courseCompletion.UserID);
            return View(courseCompletion);
        }

        // POST: CourseCompletions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "HRAdmin")]
        public ActionResult Edit([Bind(Include = "CourseCompletionID,UserID,CourseID,DateCompleted")] CourseCompletion courseCompletion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(courseCompletion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", courseCompletion.CourseID);
            ViewBag.UserID = new SelectList(db.UserDetails, "UserID", "FirstName", courseCompletion.UserID);
            return View(courseCompletion);
        }

        // GET: CourseCompletions/Delete/5
        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "HRAdmin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseCompletion courseCompletion = db.CourseCompletions.Find(id);
            if (courseCompletion == null)
            {
                return HttpNotFound();
            }
            return View(courseCompletion);
        }

        // POST: CourseCompletions/Delete/5
        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "HRAdmin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CourseCompletion courseCompletion = db.CourseCompletions.Find(id);
            db.CourseCompletions.Remove(courseCompletion);
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
