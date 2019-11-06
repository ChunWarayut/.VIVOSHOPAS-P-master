using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VIVOSHOP.Models;

namespace VIVOSHOP.Controllers
{
    public class LoginController : Controller
    {
        public vivoshopDBEntities db = new vivoshopDBEntities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Autherize(VIVOSHOP.Models.UserAccout userModel)
        {
            var userDetail = db.UserAccouts.Where(x => x.User_Email == userModel.User_Email && x.User_Tel == userModel.User_Tel).FirstOrDefault();
            if (userDetail == null)
            {
                userModel.LoginErrorMessage = "Email หรือ เบอร์โทรศัพท์ไม่ถูกต้อง";
                return View("Index" , userModel);
            }
            else
            {
                Session["id"] = userDetail.User_Id;
                Session["User_Email"] = userDetail.User_Email;
                Session["User_Id"] = userDetail.User_Id;
                Session["User_Name"] = userDetail.User_Name;
                Session["User_Lastname"] = userDetail.User_Lastname;
                Session["User_Tel"] = userDetail.User_Tel;
                Session["User_Address"] = userDetail.User_Address;
                return RedirectToAction("Index","Home");
            }
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
        public ActionResult Create([Bind(Include = "User_Id,User_Name,User_Lastname,User_Sex,User_Tel,User_Email,User_Address")] UserAccout userAccout, VIVOSHOP.Models.UserAccout userModel)
        {
            try
            {

                var emailChecked = new System.Net.Mail.MailAddress(userAccout.User_Email);                
                if (userAccout.User_Tel.Length < 10)
                {
                    var phoneChecked = new System.Net.Mail.MailAddress(userAccout.User_Tel);
                }
                if (ModelState.IsValid)
                {
                    Session["id"] = userAccout.User_Id;
                    Session["User_Email"] = userAccout.User_Email;
                    Session["User_Id"] = userAccout.User_Id;
                    Session["User_Name"] = userAccout.User_Name;
                    Session["User_Lastname"] = userAccout.User_Lastname;
                    Session["User_Tel"] = userAccout.User_Tel;
                    Session["User_Address"] = userAccout.User_Address;
                    db.UserAccouts.Add(userAccout);
                    db.SaveChanges();
                    var user = db.UserAccouts.OrderByDescending(x => x.User_Id).FirstOrDefault();
                    Session["id"] = user.User_Id;
                    Session["User_Email"] = user.User_Email;
                    Session["User_Id"] = user.User_Id;
                    Session["User_Name"] = user.User_Name;
                    Session["User_Lastname"] = user.User_Lastname;
                    Session["User_Tel"] = user.User_Tel;
                    Session["User_Address"] = user.User_Address;
                }

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                ViewBag.ErrorCHK = "True";
                userModel.LoginErrorMessageTEL = "เบอร์โทรต้องมี 10 หลัก";
                userModel.LoginErrorMessageEMAIL = "กรุณาตรวจสอบ Email";
                return View(userModel);
            }
        }



        public ActionResult Logout()
        {
            Session.Abandon();
            return View();
        }
    }
}