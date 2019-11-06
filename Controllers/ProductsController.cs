using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VIVOSHOP.Models;

namespace VIVOSHOP.Controllers
{
    public class ProductsController : Controller
    {
        private vivoshopDBEntities db = new vivoshopDBEntities();

        // GET: Products
        public ActionResult Index(string keyword)
        {

            var products = db.Products.Include(p => p.ProductType);
            if (keyword == null || keyword == " ")
            {
                return View(products.ToList());
            }
            else
            {
                var c = products.Where(x => x.Pro_Name.Contains(keyword) || x.ProductType.ProType_Name.Contains(keyword)).ToList();  
                return View(c);
            } 
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.ProType_Id = new SelectList(db.ProductTypes, "ProType_Id", "ProType_Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Pro_Id,ProType_Id,Pro_Name,Pro_Details,Pro_Price,Pro_Color,Pro_Picture")] Product product, HttpPostedFileBase Pro_Picture)
        {
            if (Pro_Picture.ContentLength > 0)
            {
                string FileName = Path.GetFileName(Pro_Picture.FileName);
                string FolderPath = Path.Combine(Server.MapPath("~/image"), FileName);
                Pro_Picture.SaveAs(FolderPath);
                product.Pro_Picture = FileName;

                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProType_Id = new SelectList(db.ProductTypes, "ProType_Id", "ProType_Name", product.ProType_Id);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProType_Id = new SelectList(db.ProductTypes, "ProType_Id", "ProType_Name", product.ProType_Id);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Pro_Id,ProType_Id,Pro_Name,Pro_Details,Pro_Price,Pro_Color,Pro_Picture")] Product product, HttpPostedFileBase Pro_Picture)
        {
            if (Pro_Picture.ContentLength > 0)
            {
                string FileName = Path.GetFileName(Pro_Picture.FileName);
                string FolderPath = Path.Combine(Server.MapPath("~/image"), FileName);
                Pro_Picture.SaveAs(FolderPath);
                product.Pro_Picture = FileName;

                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProType_Id = new SelectList(db.ProductTypes, "ProType_Id", "ProType_Name", product.ProType_Id);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
