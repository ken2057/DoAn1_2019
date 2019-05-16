using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DA_BookStore.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Home()
        {
            using (var db = new Models.BookStore())
            {
                ViewBag.DsSachDeal = db.SACHes.Where(t => t.KHUYENMAI.NgayKetThuc > DateTime.Now && t.HienThiS == true).Take(5).ToList();
                ViewBag.DsTL = db.THELOAIs.ToList();
            }
            
            return View();
        }

        public ActionResult Search(string search, int index = 0, bool tl = false)
        {
            using (var db = new Models.BookStore())
            {
                List<Models.SACH> lst = new List<Models.SACH>();
                if (!tl)
                {
                    lst = db.SACHes.Where(t => t.TenSach.Contains(search) && t.HienThiS == true).ToList();
                }
                else
                {
                    lst = db.SACHes.Where(t => t.MaTL1 == search || t.MaTL2 == search || t.MaTL3 == search).ToList();
                    ViewBag.TenTL = db.THELOAIs.Find(search).TenTheLoai;
                }

                ViewBag.SoLuong = lst.Count;
                ViewBag.DsSach = lst.Skip(12 * ((index == 0)? 0 : index - 1)).Take(12);
                ViewBag.DsTL = db.THELOAIs.ToList();
            }

            ViewBag.TenSach = search;
            ViewBag.Index = index;
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Home");
        }

        [HttpGet]
        public ActionResult Purchase()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Purchase(string idSach )
        {
            //string idKhach = Session["userID"].ToString();
            //using (var db = new Models.BookStore())
            //{
            //    Models.CTGIOHANG ct = new Models.CTGIOHANG();
            //    ct = db.CTGIOHANGs.Where(t => t.MaSach == idSach && t.TenTaiKhoan == idKhach).FirstOrDefault();
            //    ViewBag.cccc = idSach;
            //    db.Entry(ct).State = EntityState.Deleted;
            //    db.SaveChanges();
            //}
            return RedirectToAction("Home");
        }
    }
}