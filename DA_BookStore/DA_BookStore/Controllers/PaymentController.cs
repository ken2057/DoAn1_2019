using DA_BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DA_BookStore.Controllers
{
    public class PaymentController : Controller
    {
        [HttpGet]
        public ActionResult Payment()
        {
            if (Session["userID"] != null)
            {
                using (var db = new Models.BookStore())
                {
                    ViewBag.DsSachDeal = db.SACHes.Where(t => t.KHUYENMAI.NgayKetThuc > DateTime.Now && t.HienThiS == true).Take(5).ToList();
                    ViewBag.DsTL = db.THELOAIs.ToList();

                    string temp = Session["userID"].ToString();

                    var query = from gh in db.CTGIOHANGs
                                join s in db.SACHes on gh.MaSach equals s.MaSach
                                where gh.TenTaiKhoan == temp
                                select new {gh.SoLuongGioHang, s.MaSach, s.MaKhuyenMai, s.GiaBan };

                    Models.KHUYENMAI km = new Models.KHUYENMAI();
                    double? tamTinh = 0;

                    foreach (var item in query)
                    {
                        km = db.KHUYENMAIs.Where(t => t.MaKhuyenMai == item.MaKhuyenMai).FirstOrDefault();
                        tamTinh += item.GiaBan * item.SoLuongGioHang * ((100 - km.PhanTramKhuyenMai) * 0.01);
                    }

                    ViewBag.TamTinh = string.Format("{0:0,0}", tamTinh);
                    ViewBag.ThanhTien = string.Format("{0:0,0}", tamTinh);

                }
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }
        [HttpPost]
        public string Payment(string name, string address, string phonenumber, string email, string note, string giftcode)
        {
            ViewBag.HoTen = name;
            ViewBag.DiaChi = address;
            ViewBag.SDT = phonenumber;
            ViewBag.Email = email;
            ViewBag.Note = note;
            ViewBag.PromoteCode = giftcode;
            ViewBag.TT= ViewBag.HoTen+" "+ViewBag.DiaChi+" " + ViewBag.SDT+" "+ViewBag.Email+" "+ViewBag.Note;
            return ViewBag.HoTen + " " + ViewBag.DiaChi + " " + ViewBag.SDT + " " + ViewBag.Email + " " + ViewBag.Note;
        }

       
    }
}