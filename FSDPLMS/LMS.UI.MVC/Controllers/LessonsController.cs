using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using LMS.DATA.EF;
using Microsoft.AspNet.Identity;

namespace LMS.UI.MVC.Controllers
{
    public class LessonsController : Controller
    {
        private FSDPLMSEntities db = new FSDPLMSEntities();

        // GET: Lessons
        public ActionResult Index()
        {
            var lessons = db.Lessons.Include(l => l.Cours);
            return View(lessons.ToList());
        }

        // GET: Lessons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lesson lesson = db.Lessons.Find(id);
            if (lesson == null)
            {
                return HttpNotFound();
            }
            var lessonViews = db.LessonViews.Where(x => x.LessonID == lesson.LessonId).FirstOrDefault();
            if (lessonViews ==null)
            {
                LessonView lsv = new LessonView();

                lsv.LessonID = lesson.LessonId;
                lsv.DateViewed = DateTime.Now;
                lsv.UserID = User.Identity.GetUserId();
                db.LessonViews.Add(lsv);
                db.SaveChanges();
            }

            
            
            var courseLessons = db.Lessons.Where(x => x.CourseID == lesson.CourseID).Count();
            var completedLessons = db.LessonViews.Where(x => x.Lesson.CourseID == lesson.CourseID).Count();
            if (completedLessons == courseLessons)
            {
                CourseCompletion cc = new CourseCompletion();
                cc.CourseID = lesson.CourseID;
                cc.DateCompleted = DateTime.Now;
                cc.UserID = User.Identity.GetUserId();
                db.CourseCompletions.Add(cc);
                db.SaveChanges();
                
                var course = db.Courses.Where(x => x.CourseID == lesson.CourseID).FirstOrDefault();

                string body = $"{User.Identity.Name} has completed the following course: {course.CourseName}.";

                MailMessage mm = new MailMessage("webadmin@bryandoescode.com", "bryanclingan@outlook.com", "New Course Completion", body);

                mm.IsBodyHtml = true;

                mm.Priority = MailPriority.High;

                mm.ReplyToList.Add("bryanclingan@outlook.com");

                SmtpClient client = new SmtpClient("mail.bryandoescode.com");

                client.Credentials = new NetworkCredential("webadmin@bryandoescode.com", "P@ssw0rd");

                client.Port = 587;

                try
                {
                    client.Send(mm);
                }
                catch (Exception e)
                {
                    ViewBag.Message = e.StackTrace;
                }

            }
            return View(lesson);
        }

        // GET: Lessons/Create
        public ActionResult Create()
        {
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName");
            return View();
        }

        // POST: Lessons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LessonId,LessonTitle,CourseID,Introduction,VideoURL,PdfFileName,IsActive")] Lesson lesson,HttpPostedFileBase pdfFile)
        {

            if (ModelState.IsValid)
            {
                if (lesson.VideoURL !=null)
                {
                    lesson.VideoURL = "https://www.youtube.com/embed/" + lesson.VideoURL.Substring(32);
                }
                #region File Upload Utility                
                

                if (pdfFile != null)
                {

                    string pdfName = pdfFile.FileName;

                    string ext = pdfName.Substring(pdfName.LastIndexOf('.'));

                    string[] goodExts = { ".pdf" };

                    if (goodExts.Contains(ext.ToLower()) )
                    {
                        pdfName = Guid.NewGuid() + ext.ToLower();
                        string savePath = Server.MapPath("~/Content/img/");
                        pdfFile.SaveAs(savePath + pdfName);
                        lesson.PdfFileName = pdfName;
                    }
                    else
                    {
                        pdfName = null;
                        lesson.PdfFileName = null;
                    }
                }
                #endregion
                db.Lessons.Add(lesson);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", lesson.CourseID);
            return View(lesson);
        }

        // GET: Lessons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lesson lesson = db.Lessons.Find(id);
            if (lesson == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", lesson.CourseID);
            return View(lesson);
        }

        // POST: Lessons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LessonId,LessonTitle,CourseID,Introduction,VideoURL,PdfFileName,IsActive")] Lesson lesson,HttpPostedFileBase pdfFile)
        {
            if (ModelState.IsValid)
            {
                if (lesson.VideoURL != null&& !lesson.VideoURL.Contains("embed"))
                {
                    lesson.VideoURL = "https://www.youtube.com/embed/" + lesson.VideoURL.Substring(32);
                }
            #region Image Upload
            
                if (pdfFile != null)
                {
                    string pdfName = pdfFile.FileName;

                    string ext = pdfName.Substring(pdfName.LastIndexOf("."));
                    string[] exts = new string[] { ".pdf" };
                    if (exts.Contains(ext.ToLower()))
                    {
                        pdfName = Guid.NewGuid() + ext.ToLower();
                        
                        string savepath = Server.MapPath("~/Content/img/");
                        pdfFile.SaveAs(savepath + pdfName);

                        if ( lesson.PdfFileName != null)
                        {
                            System.IO.File.Delete(Server.MapPath("~/Content/img/" + lesson.PdfFileName));
                            string path = Server.MapPath("~/Content/img/");
                            

                        }
                        lesson.PdfFileName = pdfName;
                    }

                }

                #endregion
                db.Entry(lesson).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", lesson.CourseID);
            return View(lesson);
        }

        // GET: Lessons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lesson lesson = db.Lessons.Find(id);
            if (lesson == null)
            {
                return HttpNotFound();
            }
            return View(lesson);
        }

        // POST: Lessons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Lesson lesson = db.Lessons.Find(id);
            db.Lessons.Remove(lesson);
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
