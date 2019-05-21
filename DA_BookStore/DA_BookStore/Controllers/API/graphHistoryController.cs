using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;

namespace DA_BookStore.Controllers.API
{
    public class graphHistoryController : ApiController
    {
        public string Get()
        {
            return queryData();
        }
        public string Get(int day)
        {
            return queryData(day);
        }

        private string queryData(int day = 30)
        {
            using (var db = new Models.BookStore())
            {
                List<object> grp = new List<object>();
                Dictionary<string, double> dctCT = new Dictionary<string, double>();
                var minDay = DateTime.Now.AddDays(-day);

                var query = from ctxem in db.CTXEMSACHes
                            join s in db.SACHes on ctxem.MaSach equals s.MaSach
                            where ctxem.NgayXemSach >= minDay
                            select new { s.TenSach, s.SoLuongTon, s.MaSach };

                foreach (var item in query)
                {
                    if (!dctCT.Keys.Contains( item.MaSach+" "+item.TenSach+" ("+item.SoLuongTon+")"))
                        dctCT.Add(item.MaSach + " " + item.TenSach + " (" + item.SoLuongTon + ")", 1);
                    else
                        dctCT[item.MaSach + " " + item.TenSach + " (" + item.SoLuongTon + ")"] += 1;
                }

                var test = new classTemp();

                foreach (var item in dctCT)
                {
                    var text = item.Key.Split(' ').ToArray();
                    var sl = text[text.Count() - 1];
                    var id = text[0];
                    var name = item.Key.Replace(" " + sl, "")
                                        .Replace(id + " ", " ");
                    sl = sl.Replace("(", "").Replace(")", "");
                    test.children.Add(new classTemp.temp(name, item.Value, int.Parse(sl), id));
                }

                return JsonConvert.SerializeObject(test);
            }
        }

        private class classTemp
        {
            public List<temp> children { get; set; }

            public classTemp() { children = new List<temp>(); }

            public class temp
            {
                public string Name { get; set; }
                public double Count { get; set; }
                public double SoLuong { get; set; }
                public string id { get; set; }

                public temp(string name, double counts, int sl, string id)
                {
                    Name = name; Count = counts; SoLuong = sl;this.id = id;
                }
            }
        }

        
    }
}
