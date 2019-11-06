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
    public class ProductOrdersController : Controller
    {
        private vivoshopDBEntities db = new vivoshopDBEntities();

        // GET: ProductOrders
        public ActionResult Index()
        {
            var ID = int.Parse(Session["User_Id"].ToString());
            var productOrders = db.ProductOrders.Include(p => p.UserAccout);
            if (ID == 50001)
            {
                return View(productOrders.ToList());
            }
            else
            {
                var bankDB = db.ProductOrders.Where(x => x.User_Id == ID);
                return View(bankDB.ToList());
            }
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Order_Id,User_Id,Order_Date,Order_Price,Order_Status,Order_Parcel,Order_Address")] ProductOrder productOrder, string User_Address)
        {

            var user_II = int.Parse(Session["Id"].ToString());
            var user_name = Session["User_Name"];
            var test = productOrder;
            productOrder.Order_Date = DateTime.Now;
            var updateuser = db.UserAccouts.Where(x => x.User_Id == user_II).FirstOrDefault();



            var add = User_Address;
            var add2 = updateuser.User_Address;


            if (ModelState.IsValid)
            {
                if (User_Address.Length > 0)
                {
                    productOrder.Order_Date = DateTime.Now;

                    productOrder.User_Id = user_II;
                    productOrder.Order_Address = User_Address;
                    db.ProductOrders.Add(productOrder);
                    db.SaveChanges();
                    var update = db.OrderDetails.Where(x => x.ProOrderId == 0).ToList();
                    var ProID = db.ProductOrders.OrderByDescending(x => x.Order_Id).Select(x => x.Order_Id).FirstOrDefault();
                    if (update.Count() > 0)
                    {
                        update.ForEach(x => { x.ProOrderId = ProID; });
                        db.SaveChanges();
                    }
                    db.SaveChanges();
                }
                else
                {
                    productOrder.Order_Date = DateTime.Now;

                    productOrder.Order_Address = updateuser.User_Address;
                    productOrder.User_Id = user_II;
                    db.ProductOrders.Add(productOrder);
                    db.SaveChanges();
                    var update = db.OrderDetails.Where(x => x.ProOrderId == 0).ToList();
                    var ProID = db.ProductOrders.OrderByDescending(x => x.Order_Id).Select(x => x.Order_Id).FirstOrDefault();
                    if (update.Count() > 0)
                    {
                        update.ForEach(x => { x.ProOrderId = ProID; });
                        db.SaveChanges();
                    }

                }
                return RedirectToAction("Index", "History");
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
        public ActionResult Edit([Bind(Include = "Order_Id,User_Id,Order_Date,Order_Price,Order_Status,Order_Parcel,Order_img")] ProductOrder productOrder, HttpPostedFileBase Order_img)
        {
            if (ModelState.IsValid)
            {
                var update = db.ProductOrders.Where(x => x.Order_Id == productOrder.Order_Id).ToList();
                if (update.Count() > 0)
                {
                    try
                    {
                        if (Order_img.ContentLength > 0)
                        {
                            string FileName = Path.GetFileName(Order_img.FileName);
                            string FolderPath = Path.Combine(Server.MapPath("~/image"), FileName);
                            Order_img.SaveAs(FolderPath);
                            update.ForEach(x => { x.Order_img = FileName; });
                            db.SaveChanges();
                            return RedirectToAction("Index", "History");
                        }
                        else
                        {
                            try
                            {
                                update.ForEach(x => { x.Order_Status = productOrder.Order_Status; x.Order_Parcel = productOrder.Order_Parcel; });
                                db.SaveChanges();
                            }
                            catch
                            {
                                return RedirectToAction("Index", "History");
                            }
                        }
                    }
                    catch
                    {
                        //update.ForEach(x => { x.Order_Status = productOrder.Order_Status; x.Order_Parcel = productOrder.Order_Parcel; });
                        //db.SaveChanges();
                        return RedirectToAction("Index", "History");
                    }
                    
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.User_Id = new SelectList(db.UserAccouts, "User_Id", "User_Name", productOrder.User_Id);
            return View(productOrder);
        }

        // GET: ProductOrders/Edit/5
        public ActionResult Edits(int? id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edits([Bind(Include = "Order_Id,User_Id,Order_Date,Order_Price,Order_Status,Order_Parcel,Order_img")] ProductOrder productOrder, HttpPostedFileBase Order_img)
        {
            if (ModelState.IsValid)
            {
                var update = db.ProductOrders.Where(x => x.Order_Id == productOrder.Order_Id).ToList();
                if (update.Count() > 0)
                {
                    try
                    {
                        if (Order_img.ContentLength > 0)
                        {
                            string FileName = Path.GetFileName(Order_img.FileName);
                            string FolderPath = Path.Combine(Server.MapPath("~/image"), FileName);
                            Order_img.SaveAs(FolderPath);
                            update.ForEach(x => { x.Order_img = FileName; });
                            db.SaveChanges();
                            return RedirectToAction("Index", "History");
                        }
                        else
                        {
                            try
                            {
                                update.ForEach(x => { x.Order_Status = productOrder.Order_Status; x.Order_Parcel = productOrder.Order_Parcel; });
                                db.SaveChanges();
                            }
                            catch
                            {
                                return RedirectToAction("Index", "History");
                            }
                        }
                    }
                    catch
                    {
                        update.ForEach(x => { x.Order_Status = productOrder.Order_Status; x.Order_Parcel = productOrder.Order_Parcel; });
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }

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
