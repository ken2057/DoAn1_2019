﻿using System;
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
                    ViewBag.DsTL = db.THELOAIs.ToList();

                    var username = Session["userID"].ToString();
                    Models.HOADONMUAHANG t = db.HOADONMUAHANGs.Find(id);
                    if (t.TenTaiKhoan == Session["userID"].ToString() || Session["userPrio"].ToString() == "Admin")
                    {
                        t.TinhTrangThanhToan = "Huy";

                        db.Entry(t).State = System.Data.Entity.EntityState.Modified;
                        // tra lai sach khi huy

                        List<Models.CTHOADONMUAHANG> lstCTHD = db.CTHOADONMUAHANGs.Where(t => t.MaHDMua == id).ToList();
                        foreach (var ctHD in lstCTHD)
                        {
                            Models.SACH b = db.SACHes.Find(ctHD.MaSach);
                            b.SoLuongTon += ctHD.SoLuongMua;
                            db.Entry(b).State = System.Data.Entity.EntityState.Modified;
                        }

                        db.SaveChanges();
                    }
                }
                if(Session["userPrio"].ToString() == "Admin")
                    return RedirectToAction("Admin", "HoaDon");
                return RedirectToAction("AccountHistory", "HoaDon");
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult XuLy(string id)
        {
            if (Session["userPrio"].ToString() == "Admin")
            {
                using (var db = new Models.BookStore())
                {
                    ViewBag.DsTL = db.THELOAIs.ToList();

                    var username = Session["userID"].ToString();
                    Models.HOADONMUAHANG t = db.HOADONMUAHANGs.Find(id);
                    t.TinhTrangThanhToan = "Van chuyen";
                    t.TenTaiKhoanNV = Session["userID"].ToString();

                    db.Entry(t).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                }
                return RedirectToAction("Admin", "HoaDon");
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Xong(string id)
        {
            if (Session["userPrio"].ToString() == "Admin")
            {
                using (var db = new Models.BookStore())
                {
                    ViewBag.DsTL = db.THELOAIs.ToList();

                    var username = Session["userID"].ToString();
                    Models.HOADONMUAHANG t = db.HOADONMUAHANGs.Find(id);
                    t.TinhTrangThanhToan = "Xong";

                    db.Entry(t).State = System.Data.Entity.EntityState.Modified;

                    //cong diem cho taikhoan mua hang
                    if (t.TenTaiKhoan != null)
                    {
                        var tk = db.TAIKHOANs.Find(t.TenTaiKhoan);
                        tk.DiemTK += (t.TongTien / 1000);
                        db.Entry(tk).State = System.Data.Entity.EntityState.Modified;
                    }

                    db.SaveChanges();
                }
                return RedirectToAction("Admin", "HoaDon");
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Admin(int index = 0)
        {
            if (Session["userPrio"] != null && Session["userPrio"].ToString() == "Admin")
            {
                using (var db = new Models.BookStore())
                {
                    ViewBag.DsTL = db.THELOAIs.ToList();
                    List<Models.HOADONMUAHANG> lstDonMua = db.HOADONMUAHANGs.ToList();
                    ViewBag.DsDonMua = lstDonMua.Skip(index * 15).Take(15);
                    ViewBag.slS = lstDonMua.Count();
                }

                return View();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}