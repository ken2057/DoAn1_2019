using DA_BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.UI;
using System.Xml.Linq;
using DA_BookStore.Controllers.Paypal;
using PayPal.Api;

namespace DA_BookStore.Controllers
{
    public class PaymentController : Controller
    {
        int tiGiaUSD = 24000;

        [HttpGet]
        public  ActionResult Index()
        {
            if (Session["userID"] != null)
            {
                using (var db = new BookStore())
                {
                    ViewBag.DsSachDeal = db.SACHes.Where(t => t.KHUYENMAI.NgayKetThuc > DateTime.Now && t.HienThiS == true).Take(5).ToList();
                    ViewBag.DsTL = db.THELOAIs.ToList();

                    string temp = Session["userID"].ToString();

                    var query = from gh in db.CTGIOHANGs
                                join s in db.SACHes on gh.MaSach equals s.MaSach
                                where gh.TenTaiKhoan == temp
                                select new { gh.SoLuongGioHang, s.MaSach, s.MaKhuyenMai, s.GiaBan };

                    KHUYENMAI km = new KHUYENMAI();
                    double tongTien = 0;

                    if(!query.Any())
                        return RedirectToAction("Index", "Home");

                    foreach (var item in query)
                    {
                        // lay ma khuyen mai
                        km = db.KHUYENMAIs.Where(t => t.MaKhuyenMai == item.MaKhuyenMai && t.NgayKetThuc >= DateTime.Now).FirstOrDefault();

                        if (km != null)
                            tongTien += item.GiaBan * item.SoLuongGioHang * ((100 - km.PhanTramKhuyenMai) * 0.01) ?? 0;
                        else
                            tongTien += item.GiaBan * item.SoLuongGioHang ?? 0;
                    }
                    
                    // lay ma giam gia thanh vien
                    int diemTK = db.TAIKHOANs.Find(temp).DiemTK ?? 0;
                    Models.HangTaiKhoan giamTK = db.HangTaiKhoans.Where(t => t.C_start <= diemTK && diemTK <= t.C_end).FirstOrDefault();

                    double tamTinh = tongTien * (1 - (double)giamTK.GiamGia / 100);

                    ViewBag.giamTK = giamTK;
                    ViewBag.TamTinh = string.Format("{0:0,0}", tongTien);
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
        public ActionResult Index(string name, string address, string phone, string note, string codePromote, string hinhThucThanhToan)
        {
            string tenTaiKhoan = Session["userID"].ToString();

            using (var db = new BookStore())
            {
                var sql = from c in db.CTGIOHANGs
                          where c.TenTaiKhoan == tenTaiKhoan
                          select c;

                List<CTGIOHANG> cartTemp = sql.ToList();
                // kiểm tra hàng sách đủ
                bool flag = false;
                foreach (var item in cartTemp)
                {
                    var sachTemp = db.SACHes.Find(item.MaSach);
                    string message = '"' + sachTemp.TenSach + '"' + " hiện tại trong kho chỉ còn: " + sachTemp.SoLuongTon;

                    if ((sachTemp.SoLuongTon - item.SoLuongGioHang) < 0)
                    {
                        flag = true;
                        //Response.Write(@"<script language='javascript'>alert('" + message + "');</script>");
                        Response.Write(@"<script language='javascript'>if (confirm('" + message + "')){window.location = '" + Request.Url.ToString() + "'; }</script>");

                        return Index();
                    }
                }

                if (!flag)
                {
                    int diemTK = db.TAIKHOANs.Find(tenTaiKhoan).DiemTK ?? 0;

                    HOADONMUAHANG hd = new HOADONMUAHANG();
                    hd.MaHDMua = Utils.utils.createToken(10);
                    hd.ThoiGianMua = DateTime.Now;
                    hd.TenTaiKhoan = tenTaiKhoan;
                    hd.TinhTrangThanhToan = hinhThucThanhToan == "cod" ? "Doi xac nhan" : "Cho thanh toan";
                    hd.GiamThanhVien = db.HangTaiKhoans.Where(t => t.C_start <= diemTK && diemTK <= t.C_end).FirstOrDefault().GiamGia;
                    hd.GhiChu = note;
                    hd.NguoiNhan = name;
                    hd.DiaChiNhan = address;
                    hd.SdtNhan = phone;

                    double tongTien = 0;
                    foreach (var item in cartTemp)
                    {
                        var sachTemp = db.SACHes.Find(item.MaSach);
                        sachTemp.SoLuongTon -= item.SoLuongGioHang;

                        CTHOADONMUAHANG ctHD = new CTHOADONMUAHANG();
                        ctHD.MaSach = item.MaSach;
                        ctHD.MaHDMua = hd.MaHDMua;
                        ctHD.SoLuongMua = item.SoLuongGioHang;
                        ctHD.GiaHienHanh = sachTemp.GiaBan;

                        // lay khuyen mai cua 1 cuon sach de giam tien
                        Models.KHUYENMAI km = db.KHUYENMAIs.Where(
                            t => t.MaKhuyenMai == sachTemp.MaKhuyenMai
                                && t.NgayKetThuc >= DateTime.Now
                            ).FirstOrDefault();

                        if (km != null)
                        {
                            double gia = sachTemp.GiaBan * ((100 - km.PhanTramKhuyenMai) * 0.01) ?? 0;
                            ctHD.GiaHienHanh = (int)gia;
                        }
                        tongTien += (ctHD.GiaHienHanh * item.SoLuongGioHang) ?? 0;

                        db.CTHOADONMUAHANGs.Add(ctHD);
                    }
                    // kiem tra diem TK de giam tien
                    double giam = (double)hd.GiamThanhVien / 100;
                    tongTien = tongTien * (1 - giam);

                    var queryCode = db.PROMOCODEs.Find(codePromote);
                    // kiem tra ma giam gia
                    if (queryCode != null)
                    {
                        hd.CODE = codePromote;
                        tongTien -= queryCode.SoTienGiam ?? 0;
                        queryCode.SoLuong -= 1;
                        db.Entry(queryCode).State = EntityState.Modified;
                    }

                    hd.TongTien = (int)tongTien;

                    db.HOADONMUAHANGs.Add(hd);

                    foreach (var item in sql)
                    {
                        var ctgh = db.CTGIOHANGs.Find(item.MaSach, item.TenTaiKhoan);
                        db.Entry(ctgh).State = EntityState.Deleted;
                    }
                    db.SaveChanges();

                    if (hinhThucThanhToan == "cod")
                        return RedirectToAction("Index", "HoaDon", new { id = hd.MaHDMua });
                    else if (hinhThucThanhToan == "paypal")
                        return PaymentWithPaypal(hd.MaHDMua, db.CTHOADONMUAHANGs.Where(t => t.MaHDMua == hd.MaHDMua).ToList());
                    //return RedirectToAction("PaymentWithPaypal", "Payment", 
                    //    new { 
                    //            maHDMua = hd.MaHDMua, 
                    //            listCTHDMua = db.CTHOADONMUAHANGs.Where(t => t.MaHDMua == hd.MaHDMua).ToList()
                    //        });

                }

                return RedirectToAction("Charge", "Payment");
            }
        }

        public ActionResult Success(string guid, string orderId, string paymentId, string token, string PayerID, bool Cancel = false)
        {
            using (var db = new BookStore())
            {
                ViewBag.DsTL = db.THELOAIs.ToList();
                var hdMua = db.HOADONMUAHANGs.Find(orderId);
                // kiem tra match cac du lieu
                if (hdMua.paypalGuid != guid)
                    return RedirectToAction("Index", "Home");
                if (Cancel)
                {
                    xoaDonMuaHang(orderId);
                    return RedirectToAction("Index", "Payment");
                }
                //
                hdMua.paymentId = paymentId;
                hdMua.payerID = PayerID;
                hdMua.TinhTrangThanhToan = "Da thanh toan";

                db.Entry(hdMua).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Index", "HoaDon", new { id = orderId });
        }

        public ActionResult PaymentWithPaypal(string maHDMua, List<CTHOADONMUAHANG> listCTHDMua, string Cancel = null)
        {
            var guid = "";
            //getting the apiContext  
            APIContext apiContext = PaypalConfiguration.GetAPIContext();
            try
            {
                //A resource representing a Payer that funds a payment Payment Method as paypal  
                //Payer Id will be returned when payment proceeds or click to pay  
                string payerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {
                    //this section will be executed first because PayerID doesn't exist  
                    //it is returned by the create function call of the payment class  
                    // Creating a payment  
                    // baseURL is the url on which paypal sendsback the data.  
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/Payment/Success?";
                    //here we are generating guid for storing the paymentID received in session  
                    //which will be used in the payment execution  
                    guid = Convert.ToString((new Random()).Next(100000));
                    using (var db = new BookStore())
                    {
                        var hdMua = db.HOADONMUAHANGs.Find(maHDMua);
                        hdMua.paypalGuid = guid;
                        db.Entry(hdMua).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    //CreatePayment function gives us the payment approval url  
                    //on which payer is redirected for paypal account payment  
                    Payment createdPayment = CreatePayment(apiContext, baseURI + "guid=" + guid + "&orderId=" + maHDMua, listCTHDMua);

                    //get links returned from paypal in response to Create function call  
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = null;
                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;
                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment  
                            paypalRedirectUrl = lnk.href;
                        }
                    }
                    // saving the paymentID in the key guid  
                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    // This function exectues after receving all parameters for the payment  
                    guid = Request.Params["guid"];
                    using (var db = new BookStore())
                    {
                        var hdMua = db.HOADONMUAHANGs.Find(maHDMua);
                        hdMua.paypalGuid = guid;
                        db.Entry(hdMua).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
                    //If executed payment failed then we will show payment failure message to user  
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        xoaDonMuaHang(maHDMua);
                        return View("FailureView");
                    }
                }
            }
            catch (Exception ex)
            {
                //
                xoaDonMuaHang(maHDMua);
                throw new Exception(ex.StackTrace);
                return RedirectToAction("Sucess", "Payment", new { id = ex.Message });
                //return View("FailureView");
            }

            //on successful payment show success page to user.
            return View("SuccessView");
        }

        private void xoaDonMuaHang(string maHDMua)
        {
            using (var db = new BookStore())
            {
                var hdMua = db.HOADONMUAHANGs.Find(maHDMua);
                if (hdMua.CODE != null)
                {
                    var code = db.PROMOCODEs.Find(hdMua.CODE);
                    code.SoLuong += 1;
                    db.Entry(code).State = EntityState.Modified;
                }
                List<CTHOADONMUAHANG> listCTHDMua = db.CTHOADONMUAHANGs.Where(t => t.MaHDMua == maHDMua).ToList();

                foreach (var item in listCTHDMua)
                {
                    db.CTGIOHANGs.Add(new CTGIOHANG { 
                        MaSach = item.MaSach,
                        SoLuongGioHang = item.SoLuongMua,
                        TenTaiKhoan = hdMua.TenTaiKhoan
                    });
                    item.SACH.SoLuongTon += item.SoLuongMua;
                    db.Entry(item).State = EntityState.Deleted;
                }

                db.Entry(hdMua).State = EntityState.Deleted;
                db.SaveChanges();
            }
        }

        private PayPal.Api.Payment payment;
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            this.payment = new Payment()
            {
                id = paymentId
            };
            return this.payment.Execute(apiContext, paymentExecution);
        }
        private Payment CreatePayment(APIContext apiContext, string redirectUrl, List<CTHOADONMUAHANG> listCTHDMua)
        {
            double totalPrice = 0;
            //create itemlist and add item objects to it  
            var itemList = new ItemList()
            {
                items = new List<Item>()
            };
            foreach (var ctHDMua in listCTHDMua)
            {
                double gia = (double)(ctHDMua.GiaHienHanh ?? 0) / tiGiaUSD;
                gia = Math.Round(gia, 2);
                totalPrice += gia * (double)ctHDMua.SoLuongMua;
                //Adding Item Details like name, currency, price etc  
                itemList.items.Add(new Item()
                {
                    name = ctHDMua.SACH.TenSach,
                    currency = "USD",
                    price = gia + "",
                    quantity = ctHDMua.SoLuongMua + "",
                    sku = ctHDMua.MaSach
                });
            }
            // giảm giá theo điểm thành viên
            using(var db = new BookStore())
            {
                int diemTK = listCTHDMua[0].HOADONMUAHANG.TAIKHOAN.DiemTK ?? 0;
                var giamThanhVien = db.HangTaiKhoans.Where(t => t.C_start <= diemTK && diemTK <= t.C_end).FirstOrDefault();

                if (giamThanhVien.GiamGia > 0)
                {
                    double giam = (double)giamThanhVien.GiamGia / 100;
                    double tienGiam = -1 * Math.Round(totalPrice * giam, 2);
                    totalPrice += tienGiam;

                    itemList.items.Add(new Item()
                    {
                        name = "Thành viên: " + giamThanhVien.TenHang,
                        currency = "USD",
                        price = tienGiam + "",
                        quantity = "1"
                    });
                }

            }

            // tính mã giảm giá
            if (listCTHDMua[0].HOADONMUAHANG.CODE != null)
            {
                double giam = -1 * (double)(listCTHDMua[0].HOADONMUAHANG.PROMOCODE.SoTienGiam ?? 0) / tiGiaUSD;
                giam = Math.Round(giam, 2);
                totalPrice += giam;
                itemList.items.Add(new Item()
                {
                    name = "Mã giảm giá: "+ listCTHDMua[0].HOADONMUAHANG.CODE,
                    currency = "USD",
                    price =  ""+giam,
                    quantity = "1"
                });
            }

            var payer = new Payer()
            {
                payment_method = "paypal"
            };
            // Configure Redirect Urls here with RedirectUrls object  
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl + "&Cancel=true",
                return_url = redirectUrl
            };
            // Adding Tax, shipping and Subtotal details  
            var details = new Details()
            {
                tax = "0",
                shipping = "0",
                subtotal = totalPrice+""
            };
            //Final amount with details  
            var amount = new Amount()
            {
                currency = "USD",
                total = totalPrice + "", // Total must be equal to sum of tax, shipping and subtotal. 
                details = details
            };
            var transactionList = new List<Transaction>();
            // Adding description about the transaction  
            transactionList.Add(new Transaction()
            {
                description = "Thành toán hoá đơn: " + listCTHDMua[0].MaHDMua,
                invoice_number = listCTHDMua[0].MaHDMua, //Generate an Invoice No  
                amount = amount,
                item_list = itemList
            });
            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };
            // Create a payment using a APIContext  
            return this.payment.Create(apiContext);
        }

    }
}