using System.Linq;
using System.Net;
using System.Web.Mvc;
using VIVOSHOP.Models;

namespace VIVOSHOP.Controllers
{
    public class HomeController : Controller
    {
        public vivoshopDBEntities db = new vivoshopDBEntities();
        public ActionResult Index(string keyword)
        {
            if (keyword == " " || keyword == null)
            {
                return View(db.Products.ToList());
            }
            else
            {
                return View(db.Products.Where(x => x.Pro_Name.Contains(keyword)).ToList());
            }

        }


        // GET: Products/Edit/5
        public ActionResult Create(int? id)
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
            ViewBag.id = id;
            return View(product);
        }


        [HttpPost]
        public ActionResult Create([Bind(Include = "Order_Id,Pro_Id,OrderDetails_Number,Pro_Price,ProOrderId")]OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                db.OrderDetails.Add(orderDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(orderDetail);
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