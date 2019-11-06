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
        public ActionResult ProductType()
        {
            return View(db.ProductTypes.ToList());
        }
        
        // GET: Report
        public ActionResult Sale()
        { 
            return View(db.ProductOrders.ToList());
        }

    }
}