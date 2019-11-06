using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VIVOSHOP.Models;

namespace VIVOSHOP.Controllers
{
    public class HistoryController : Controller
    {
        private vivoshopDBEntities db = new vivoshopDBEntities();

        // GET: ProductOrders
        public ActionResult Index()
        {
            var ID = int.Parse(Session["User_Id"].ToString());
            var productOrders = db.ProductOrders.Where(x => x.User_Id == ID);
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
            ViewBag.order = db.OrderDetails.Where(x => x.ProOrderId == id).ToList();
            return View(productOrder);
        }

    }
}