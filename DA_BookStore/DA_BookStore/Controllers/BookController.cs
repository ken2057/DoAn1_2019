﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DA_BookStore.Controllers
{
    public class BookController : Controller
    {
        [HttpGet]
        public ActionResult Detail(string id)
        {
            if (Session["bookEdit"] != null && Session["bookID"].ToString() != id)
                Session["bookEdit"] = null;

            try
            {
                using (var db = new Models.BookStore())
                {
                    ViewBag.DsTL = db.THELOAIs.ToList();

                    Session["bookID"] = id;

                    ViewBag.id = id;
                    Models.SACH s = db.SACHes.Where(t => t.MaSach == id).FirstOrDefault();

                    ViewBag.Sach = s;

                    if (s.HienThiS == false)
                        return RedirectToAction("Home","Home");

                    Models.KHUYENMAI km = db.KHUYENMAIs.Where(t => t.MaKhuyenMai == s.MaKhuyenMai).FirstOrDefault();
                    if (km != null)
                    {
                        ViewBag.KhuyenMai = km;

                        ViewBag.TietKiem = s.GiaBan * (km.PhanTramKhuyenMai * 0.01);
                        ViewBag.GiaBanHienTai = s.GiaBan * ((100 - km.PhanTramKhuyenMai) * 0.01);
                    }

                   
                    if (Session["userID"] != null)
                    {
                        db.CTXEMSACHes.Add(new Models.CTXEMSACH() { MaSach = id, TenTaiKhoan = Session["userID"].ToString(), NgayXemSach = DateTime.Now });
                        db.SaveChanges();
                    }
                }
            }
            catch {}
            return View();
        }
        [HttpPost]
        public ActionResult UpdateBookDetail(string tenSach, string tacGia, string sku, string giaBan, string gioiThieuSach, HttpPostedFileBase hinh)
        {
        	if (Session["userPrio"] != null && Session["userPrio"].ToString() == "Admin")
            {
	            using (var db = new Models.BookStore())
	            {
	                Models.SACH s = db.SACHes.Find(Session["bookID"]);
	                s.TenSach = tenSach;
	                s.TenTacGia = tacGia;
	                s.SKU = sku;
	                s.GiaBan = int.Parse(giaBan);
	                s.GioiThieuSach = gioiThieuSach;
	                if (hinh != null)
	                {
	                    try
	                    {
	                        string _path = "";
	                        if (hinh.ContentLength > 0)
	                        {
	                            string _fileName = System.IO.Path.GetFileName(hinh.FileName);
	                            _path = System.IO.Path.Combine(Server.MapPath("~/Image/Book"), _fileName);
	                            hinh.SaveAs(_path);
	                        }

	                        s.HinhSach = "Image/Book/" + hinh.FileName;
	                    }
	                    catch
	                    {

	                    }
	                }

	                db.Entry(s).State = System.Data.Entity.EntityState.Modified;
	                db.SaveChanges();
	            }
	            Session["bookEdit"] = null;
	            return RedirectToAction("Detail","Book", new { id = Session["BookID"].ToString() });
	        }
	        return RedirectToAction("Home", "Home");
        }

        public ActionResult BookEdit(string id)
        {
            if (Session["userPrio"] != null && Session["userPrio"].ToString() == "Admin")
            {
                Session["bookEdit"] = "sua";
                return RedirectToAction("Detail", "Book", new { id = id });
            }
            return RedirectToAction("Home", "Home");
        }

        public ActionResult BookDelete(string id)
        {
            if (Session["userPrio"] != null && Session["userPrio"].ToString() == "Admin")
            {
                using (var db = new Models.BookStore())
                {
                    Models.SACH s = db.SACHes.Find(id);
                    s.HienThiS = false;
                    db.Entry(s).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("BookManage", "Book");
            }
            return RedirectToAction("Home", "Home");
        }

        [HttpGet]
        public ActionResult BookManage(int index = 0)
        {
            if (Session["userPrio"] != null && Session["userPrio"].ToString() == "Admin")
            {
                using (var db = new Models.BookStore())
                {
                    ViewBag.DsTL = db.THELOAIs.ToList();
                    List<Models.SACH> lst = db.SACHes.Where(t => t.HienThiS == true).ToList();
                    ViewBag.DsS = lst.Skip(15*index).Take(15);
                    ViewBag.slS = lst.Count();
                }

                return View();
            }
            return RedirectToAction("Home","Home");
        }
        [HttpPost]
        public ActionResult BookManage(string id, int index)
        {
            if (Session["userPrio"] != null && Session["userPrio"].ToString() == "Admin")
            {
                using (var db = new Models.BookStore())
                {
                    ViewBag.DsTL = db.THELOAIs.ToList();
                    List<Models.SACH> lst = db.SACHes.Where(t => t.HienThiS == true && t.TenSach.Contains(id)).ToList();
                    ViewBag.DsS = lst.Skip(15*index).Take(15);
                    ViewBag.slS = lst.Count();
                }

                return View();
            }
            return RedirectToAction("Home","Home");
        }

        [HttpGet]
        public ActionResult AddBook()
        {
            if (Session["userPrio"] != null && Session["userPrio"].ToString() == "Admin")
            {
                using (var db = new Models.BookStore())
                {
                    ViewBag.DsTL = db.THELOAIs.ToList();
                }

                return View();
            }
            return RedirectToAction("Home","Home");
        }
        [HttpPost]
        public ActionResult AddBook(string tenSach, string tacGia, string sku, string giaBan, string gioiThieuSach, HttpPostedFileBase hinh)
        {
            if (Session["userPrio"] != null && Session["userPrio"].ToString() == "Admin")
            {
                using (var db = new Models.BookStore())
                {
                    Models.SACH s = new Models.SACH();
                    int slS = db.SACHes.ToList().Count() + 1;

                    var maSach = "S" + slS.ToString().PadLeft(9, '0');

                    s.MaSach = maSach;
                    s.TenSach = tenSach;
                    s.TenTacGia = tacGia;
                    s.SKU = sku;
                    s.GiaBan = int.Parse(giaBan);
                    s.GioiThieuSach = gioiThieuSach;
                    s.MaNhaCungCap = "NCC0000001";
                    s.MaNhaXuatBan = "NXB0000001";
                    s.SoLanTruyCap = 0;
                    s.SoLuongTon = 100;
                    s.NgayXuatBan = DateTime.Today;

                    s.HienThiS = true;
                    if (hinh != null)
                    {
                        try
                        {
                            string _path = "";
                            if (hinh.ContentLength > 0)
                            {
                                string _fileName = System.IO.Path.GetFileName(hinh.FileName);
                                _path = System.IO.Path.Combine(Server.MapPath("~/Image/Book"), _fileName);
                                hinh.SaveAs(_path);
                            }
                            s.HinhSach = "Image/Book/" + hinh.FileName;
                        }
                        catch{}
                    }

                    db.SACHes.Add(s);
                    db.SaveChanges();

                    return RedirectToAction("Detail", "Book", new { id = maSach });
                }
            }
            return RedirectToAction("Home");
        }

    }
}