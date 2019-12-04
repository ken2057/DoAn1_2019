using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DA_BookStore.Controllers
{
    public class HoaDonController : Controller
    {
        // GET: HoaDon
        public ActionResult Index(string id)
        {
            if (Session["userID"] != null)
            {
                using (var db = new Models.BookStore())
                { 
                    ViewBag.DsCTHD = db.CTHOADONMUAHANGs.Where(t => t.MaHDMua == id).ToList();
                    ViewBag.HD = db.HOADONMUAHANGs.Find(id);

                    ViewBag.DsTL = db.THELOAIs.ToList();
                }

                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult AccountHistory()
        {
            if (Session["userID"] != null)
            {
                using (var db = new Models.BookStore())
                {
                    var username = Session["userID"].ToString();
                    var t = db.HOADONMUAHANGs.Where(t => t.TenTaiKhoan == username).ToList();
                    ViewBag.DsMua = t;
                    ViewBag.slTK = t.Count();

                    ViewBag.DsTL = db.THELOAIs.ToList();
                }

                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Huy(string id)
        {
            if (Session["userID"] != null)
            {
                using (var db = new Models.BookStore())
                {
                    var username = Session["userID"].ToString();
                    var t = db.HOADONMUAHANGs.Find(id);
                    t.TinhTrangThanhToan = "Huy";

                    db.Entry(t).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    ViewBag.DsTL = db.THELOAIs.ToList();
                }
                return RedirectToAction("AccountHistory", "Account");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}