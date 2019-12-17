using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DA_BookStore.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult Index()
        {
            if (Session["userPrio"] != null && Session["userPrio"].ToString() == "Admin")
            {
                using (var db = new Models.BookStore())
                {
                    ViewBag.DsTL = db.THELOAIs.ToList();
                    ViewBag.timeBD = DateTime.Now;
                    ViewBag.timeKT = DateTime.Now;
                    ViewBag.selected = "ngay";
                }
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Search(string phanLoaiRP, DateTime ngayBD, DateTime ngayKT)
        {
            if (Session["userPrio"] != null && Session["userPrio"].ToString() == "Admin")
            {
                using (var db = new Models.BookStore())
                {
                    ViewBag.DsTL = db.THELOAIs.ToList();
                    ViewBag.timeBD = ngayBD;
                    ViewBag.timeKT = ngayKT;
                    ViewBag.selected = phanLoaiRP;

                    // lazy code
                    if (ngayBD > ngayKT)
                        return View("Index");

                    List<string> listTinhTrang = new List<string>();
                    Dictionary<string, Models.Temp.RpDonMua> dictRp = new Dictionary<string, Models.Temp.RpDonMua>();
                    //
                    var listHD = db.HOADONMUAHANGs
                                    .Where(t => ngayBD <= t.ThoiGianMua && t.ThoiGianMua <= ngayKT)
                                    .ToList();

                    // lấy tất cả tình trạng có trong ds hoá đơn
                    foreach (var item in listHD)
                        if (!listTinhTrang.Contains(item.TinhTrangThanhToan))
                            listTinhTrang.Add(item.TinhTrangThanhToan);

                    // group
                    foreach (var item in listHD)
                    {
                        var key = "";
                        if (phanLoaiRP == "ngay")
                            key = string.Format("{0:dd/MM/yyyy}", item.ThoiGianMua);
                        else if (phanLoaiRP == "thang")
                            key = string.Format("{0:MM/yyyy}", item.ThoiGianMua);
                        else // nam
                            key = string.Format("{0:yyyy}", item.ThoiGianMua);

                        // kiểm tra tồn tại để thêm vào dict
                        if(!dictRp.ContainsKey(key))
                            dictRp.Add(key, new Models.Temp.RpDonMua(listTinhTrang));

                        // đếm số lượng trong dict
                        var value = dictRp[key];

                        value.listSLTinhTrang[listTinhTrang.IndexOf(item.TinhTrangThanhToan)] += 1;
                        if (item.TinhTrangThanhToan == "Xong")
                            value.TongTienThu += (long)item.TongTien;

                        dictRp[key] = value;
                    }
                    ViewBag.Report = dictRp;
                    ViewBag.ListTinhTrang = listTinhTrang;
                }
                return View("Index");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}