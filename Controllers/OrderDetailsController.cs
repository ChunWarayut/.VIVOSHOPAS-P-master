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
    public class OrderDetailsController : Controller
    {
        private vivoshopDBEntities db = new vivoshopDBEntities();

        // GET: OrderDetails
        public ActionResult Index()
        {
            var pro_0 = db.OrderDetails.Where(x => x.ProOrderId == 0).ToList();
            if (pro_0.Count() > 0)
            {
                ViewBag.Prosum = db.OrderDetails.Where(x => x.ProOrderId == 0).Select(x => x.Pro_Price).Sum();
            }
            else
            {
                ViewBag.Prosum = 0; 
            }
            
            var orderDetails = db.OrderDetails.OrderBy(x=>x.ProOrderId).Include(o => o.Product);
            return View(orderDetails.Where(x=>x.ProOrderId == 0).ToList());
        }
         

        // GET: OrderDetails/Create
        public ActionResult Create(decimal? price, int? id)
        {
            ViewBag.Price = price;
            ViewBag.Pro_Id = id;
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

        // POST: OrderDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Order_Id,Pro_Id,OrderDetails_Number,Pro_Price,ProOrderId")] OrderDetail orderDetail, decimal OrderDetails_Number, decimal Price)
        {
            if (ModelState.IsValid)
            {
                orderDetail.Pro_Price = Price * OrderDetails_Number;
                orderDetail.ProOrderId = 0;
                db.OrderDetails.Add(orderDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Pro_Id = new SelectList(db.Products, "Pro_Id", "Pro_Name", orderDetail.Pro_Id);
            return View(orderDetail);
        }
         
        // GET: OrderDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // POST: OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            db.OrderDetails.Remove(orderDetail);
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
