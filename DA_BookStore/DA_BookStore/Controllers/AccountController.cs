using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DA_BookStore.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Index(string id = "", bool edit = false)
        {
            using (var db = new Models.BookStore())
            {
                if (Session["userID"] != null)
                {
                    Models.TAIKHOAN tk = db.TAIKHOANs.Find(id);
                    ViewBag.TK = tk;
                    List<Models.HangTaiKhoan> listHTK = db.HangTaiKhoans.ToList();
                    listHTK.ForEach(t =>
                    {
                        if (t.C_start <= tk.DiemTK && tk.DiemTK <= t.C_end)
                            ViewBag.LoaiTK = t.TenHang;
                    });

                    // when is admin wanna edit user account
                    if (Session["userPrio"] != null && Session["userPrio"].ToString() == "Admin" && edit)
                    {
                        ViewBag.editMode = 1;   
                        Models.NHANVIEN nv = db.NHANVIENs.Find(id);

                        if (nv != null && nv.HienThiNV == true)
                            ViewBag.tkPrio = nv.ChucVuNV;
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
                ViewBag.DsTL = db.THELOAIs.ToList();
            }
            return View();
        }

        [HttpPost]
        public ActionResult UpdateAccountDetail(string id, string pass, string name, string email, string sdt, string sex, string rePass, string prio = "")
        {
            var isAdmin = false;
            using (var db = new Models.BookStore())
            {
                if (rePass != pass && pass != "")
                {
                    ViewBag.error = "Mật khẩu nhập không trùng khớp";
                    return RedirectToAction("Index", "Account");
                }

                Models.TAIKHOAN tk = db.TAIKHOANs.Find(id);

                //kiểm tra quyền user hiện tại
                if (Session["userPrio"] != null && Session["userPrio"].ToString() == "Admin")
                {
                    //kiem tra tk dang su dung co = tk dang thay doi k
                    if (Session["userID"].ToString() != id)
                    {
                        //kiểm tra chức vụ nhân viên đã tòn tại chưa
                        if (db.NHANVIENs.Where(t => t.TenTaiKhoanNV == id).Count() == 0)
                        {
                            if (prio == "Manager")
                                db.NHANVIENs.Add(new Models.NHANVIEN { TenTaiKhoanNV = id, ChucVuNV = "QuanLy", HienThiNV = true });
                            //không set admin
                        }
                        else
                        {
                            Models.NHANVIEN nv = db.NHANVIENs.Find(id);

                            if (prio == "")
                                nv.HienThiNV = false;
                            else if (prio == "Manager")
                            {
                                nv.ChucVuNV = "QuanLy";
                                nv.HienThiNV = true;
                            }
                            //không set admin
                            db.Entry(nv).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                        }
                        isAdmin = true;
                    }
                }
                else
                {
                    if (id != Session["userID"].ToString())
                        return RedirectToAction("Home", "Index");
                }

                if (pass != "")
                    tk.MauKhau = pass;
                tk.HoTen = name;
                tk.Email = email;
                tk.Sdt = sdt;
                tk.GioiTinh = (sex == "Nam") ? true : false;

                db.SaveChanges();

                ViewBag.DsTL = db.THELOAIs.ToList();
                ViewBag.TK = tk;
            }
            if (isAdmin)
                return RedirectToAction("AccountManage", "Account");

            return RedirectToAction("Index", "Account");
        }

        public ActionResult DeleteAccount(string id)
        {
            if (Session["userPrio"] != null && Session["userPrio"].ToString() == "Admin")
            {
                if(Session["userID"].ToString() != id){
                    using (var db = new Models.BookStore())
                    {
                        Models.TAIKHOAN tk = db.TAIKHOANs.Find(id);
                        Models.NHANVIEN nv = db.NHANVIENs.Find(id);

                        tk.HienThiTK = !tk.HienThiTK;
                        db.Entry(tk).State = System.Data.Entity.EntityState.Modified;

                        db.SaveChanges();
                    }
                }
                return RedirectToAction("AccountManage", "Account");
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult AccountManage(int index = 0)
        {
            if (Session["userPrio"] != null && Session["userPrio"].ToString() == "Admin")
            {
                using (var db = new Models.BookStore())
                {
                    ViewBag.DsTL = db.THELOAIs.ToList();

                    List<Models.TAIKHOAN> dsTK = db.TAIKHOANs.ToList();

                    ViewBag.DsTK = dsTK.Skip(15 * index).Take(15);
                    ViewBag.slTK = dsTK.Count();
                    ViewBag.DsNV = db.NHANVIENs.Where(t => t.HienThiNV == true).ToList();
                    List<Models.HangTaiKhoan> listLTK = db.HangTaiKhoans.ToList();

                    ViewBag.LoaiTK = db.HangTaiKhoans.ToList();
                }

                return View();
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult AccountManage(string id, int index = 0)
        {
            if (Session["userPrio"] != null && Session["userPrio"].ToString() == "Admin")
            {
                using (var db = new Models.BookStore())
                {
                    ViewBag.DsTL = db.THELOAIs.ToList();
                    ViewBag.DsNV = db.NHANVIENs.ToList();

                    ViewBag.test = Request.Url.ToString();

                    List<Models.TAIKHOAN> dsTK = db.TAIKHOANs.Where(t => t.HienThiTK == true && t.TenTaiKhoan.Contains(id)).ToList();

                    ViewBag.DsTK = dsTK.Skip(15 * index).Take(15);
                    ViewBag.slTK = dsTK.Count();
                }

                return View();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}