using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DA_BookStore.Controllers
{
    public class CodeController : Controller
    {
        // GET: Code
        public ActionResult Index(int index = 0)
        {
            if (Session["userPrio"] == null)
                return RedirectToAction("Index", "Home");

            using (var db = new Models.BookStore())
            {
                ViewBag.DsTL = db.THELOAIs.ToList();
                var listCode = db.PROMOCODEs.OrderBy(t => t.NgayHetHan).ToList();
                ViewBag.slS = listCode.Count();
                ViewBag.DsCode = listCode.Skip(index * 15).Take(15);
            }
            
            return View();
        }

       public ActionResult CreateCode(DateTime startDate, DateTime endDate, int total, int reduce)
        {
            if (Session["userPrio"] == null)
                return RedirectToAction("Index", "Home");

            if(startDate < endDate)
                using (var db = new Models.BookStore())
                {
                    ViewBag.DsTL = db.THELOAIs.ToList();
                    Models.PROMOCODE p = new Models.PROMOCODE()
                    {
                        CODE = Utils.utils.createToken(),
                        NgayThem = startDate,
                        NgayHetHan = endDate,
                        SoLuong = total,
                        SoTienGiam = reduce
                    };
                    db.PROMOCODEs.Add(p);
                    db.SaveChanges();
                }

            return RedirectToAction("Index", "Code");
        }
    }
}