using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DA_BookStore.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Test()
        {
            using (var db = new Models.BookStore())
            {
                ViewBag.DsSach = db.SACHes.ToList();
            }
            return View();
        }

        public string GraphPromo()
        {
            using (var db = new Models.BookStore())
            {
                List<Models.Temp.PromoView> listPromo = new List<Models.Temp.PromoView>();


                db.KHUYENMAIs.ToList()
                        .ForEach(t => listPromo
                                        .Add(new Models.Temp.PromoView(t.TenKhuyenMai, t.NgayBatDau, t.NgayKetThuc, t.PhanTramKhuyenMai ?? 0)));

                var grpPromo = new Models.Temp.Group { group = "Khuyến mãi", data = listPromo };
                var listGrp = new List<Models.Temp.Group>() { grpPromo };

                return JsonConvert.SerializeObject(listGrp);

            }
        }

        public string GraphHistory(int day = 30)
        {
            using (var db = new Models.BookStore())
            {
                Dictionary<string, double> dctCT = new Dictionary<string, double>();
                var minDay = DateTime.Now.AddDays(-day);

                var query = from ctxem in db.CTXEMSACHes
                            join s in db.SACHes on ctxem.MaSach equals s.MaSach
                            where ctxem.NgayXemSach >= minDay
                            select new { s.TenSach, s.SoLuongTon, s.MaSach };

                foreach (var item in query)
                {
                    if (!dctCT.Keys.Contains(item.MaSach + " " + item.TenSach + " (" + item.SoLuongTon + ")"))
                        dctCT.Add(item.MaSach + " " + item.TenSach + " (" + item.SoLuongTon + ")", 1);
                    else
                        dctCT[item.MaSach + " " + item.TenSach + " (" + item.SoLuongTon + ")"] += 1;
                }

                var test = new Models.Temp.HistoryView();

                foreach (var item in dctCT)
                {
                    var text = item.Key.Split(' ').ToArray();
                    var sl = text[text.Count() - 1];
                    var id = text[0];
                    var name = item.Key.Replace(" " + sl, "")
                                        .Replace(id + " ", " ");
                    sl = sl.Replace("(", "").Replace(")", "");
                    test.children.Add(new Models.Temp.HistoryView.temp(name, item.Value, int.Parse(sl), id));
                }
                test.children.Sort();

                return JsonConvert.SerializeObject(test);
            }
        }


    }
}