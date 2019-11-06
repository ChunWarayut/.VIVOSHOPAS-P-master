using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using VIVOSHOP.Models;
namespace VIVOSHOP.Controllers
{
    public class ReportController : Controller
    {
        public vivoshopDBEntities db = new vivoshopDBEntities();
        // GET: Report
        public ActionResult Index()
        {
            return View();
        } 
        
        // GET: Report
        public ActionResult Accout()
        { 
            return View(db.UserAccouts.ToList());
        }
        
        // GET: Report
        public ActionResult Product()
        {
            return View(db.Products.ToList());
        }
        // GET: Report
        public ActionResult ProductGood()
        {
            return View(db.Products.Where(x => x.Pro_Amout > 0).OrderByDescending(x => x.Pro_Amout).Take(10).ToList());
        }

        // GET: Report
        public ActionResult ProductType()
        {
            return View(db.ProductTypes.ToList());
        }
        
        // GET: Report
        public ActionResult Sale(DateTime? gte, DateTime? lte)
        {
            try
            {
                ViewBag.gte = gte.Value.ToString("dd/MM/yyyy");
                ViewBag.lte = lte.Value.ToString("dd/MM/yyyy");
                var sales = db.ProductOrders.Where(x => x.Order_Date >= gte && x.Order_Date <= lte && x.Order_Status == "จัดส่งแล้ว").ToList();
                return View(sales);
            }
            catch
            {
                var sales = db.ProductOrders.Where(x => x.Order_Status == "จัดส่งแล้ว").ToList();
                return View(sales);
            }
        }

    }
}