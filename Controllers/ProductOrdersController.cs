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
    public class ProductOrdersController : Controller
    {
        private vivoshopDBEntities db = new vivoshopDBEntities();

        // GET: ProductOrders
        public ActionResult Index()
        {
            var productOrders = db.ProductOrders.Include(p => p.UserAccout);
            return View(productOrders.ToList());
        }

        // GET: ProductOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductOrder productOrder = db.ProductOrders.Find(id);
            if (productOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.order = db.OrderDetails.Where(x=>x.ProOrderId == id).ToList();
            return View(productOrder);
        }

        // GET: ProductOrders/Create
        public ActionResult Create(decimal? price)
        {
            @ViewBag.Prosum = price;
            @ViewBag.sum = db.ProductOrders.OrderByDescending(x => x.Order_Id).Select(x => x.Order_Id).FirstOrDefault();

            ViewBag.User_Id = new SelectList(db.UserAccouts, "User_Id", "User_Name");
            return View();
        }

        // POST: ProductOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Order_Id,User_Id,Order_Date,Order_Price,Order_Status,Order_Parcel")] ProductOrder productOrder)
        {
            if (ModelState.IsValid)
            { 
                productOrder.Order_Date = DateTime.Now;
                db.ProductOrders.Add(productOrder);
                db.SaveChanges();
                var update = db.OrderDetails.Where(x => x.ProOrderId == 0).ToList();
                var ProID = db.ProductOrders.OrderByDescending(x => x.Order_Id).Select(x => x.Order_Id).FirstOrDefault();
                if (update.Count() > 0)
                {
                    update.ForEach(x => { x.ProOrderId = ProID; }); 
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }

            ViewBag.User_Id = new SelectList(db.UserAccouts, "User_Id", "User_Name", productOrder.User_Id);
            return View(productOrder);
        }

        // GET: ProductOrders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductOrder productOrder = db.ProductOrders.Find(id);
            if (productOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.order = db.OrderDetails.Where(x => x.ProOrderId == id).ToList();
            ViewBag.User_Id = new SelectList(db.UserAccouts, "User_Id", "User_Name", productOrder.User_Id);
            return View(productOrder);
        }

        // POST: ProductOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Order_Id,User_Id,Order_Date,Order_Price,Order_Status,Order_Parcel")] ProductOrder productOrder)
        {
            if (ModelState.IsValid)
            {
                var update = db.ProductOrders.Where(x => x.Order_Id == productOrder.Order_Id).ToList();
                if (update.Count() > 0)
                {
                    update.ForEach(x => { x.Order_Status = productOrder.Order_Status; x.Order_Parcel = productOrder.Order_Parcel; });
                    db.SaveChanges();
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.User_Id = new SelectList(db.UserAccouts, "User_Id", "User_Name", productOrder.User_Id);
            return View(productOrder);
        }

        // GET: ProductOrders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductOrder productOrder = db.ProductOrders.Find(id);
            if (productOrder == null)
            {
                return HttpNotFound();
            }
            return View(productOrder);
        }

        // POST: ProductOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductOrder productOrder = db.ProductOrders.Find(id);
            db.ProductOrders.Remove(productOrder);
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
