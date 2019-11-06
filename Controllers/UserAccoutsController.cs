using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VIVOSHOP.Models;

namespace VIVOSHOP.Controllers
{
    public class UserAccoutsController : Controller
    {
        private vivoshopDBEntities db = new vivoshopDBEntities();

        // GET: UserAccouts
        public ActionResult Index(string keyword)
        {
            if (keyword == null || keyword == " ")
            {
                return View(db.UserAccouts.ToList());
            }
            else
            {  
                var A = db.UserAccouts.Where(x => x.User_Name.Contains(keyword) || x.User_Lastname.Contains(keyword) || x.User_Sex.Contains(keyword) || x.User_Tel.Contains(keyword) || x.User_Email.Contains(keyword)).ToList(); 
                return View(A);
            }
        }

        // GET: UserAccouts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAccout userAccout = db.UserAccouts.Find(id);
            if (userAccout == null)
            {
                return HttpNotFound();
            }
            return View(userAccout);
        }

        // GET: UserAccouts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserAccouts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "User_Id,User_Name,User_Lastname,User_Sex,User_Tel,User_Email,User_Address")] UserAccout userAccout)
        {
            if (ModelState.IsValid)
            {
                db.UserAccouts.Add(userAccout);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userAccout);
        }

        // GET: UserAccouts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAccout userAccout = db.UserAccouts.Find(id);
            if (userAccout == null)
            {
                return HttpNotFound();
            }
            return View(userAccout);
        }

        // POST: UserAccouts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "User_Id,User_Name,User_Lastname,User_Sex,User_Tel,User_Email,User_Address")] UserAccout userAccout)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userAccout).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userAccout);
        }

        // GET: UserAccouts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAccout userAccout = db.UserAccouts.Find(id);
            if (userAccout == null)
            {
                return HttpNotFound();
            }
            return View(userAccout);
        }

        // POST: UserAccouts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserAccout userAccout = db.UserAccouts.Find(id);
            db.UserAccouts.Remove(userAccout);
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
