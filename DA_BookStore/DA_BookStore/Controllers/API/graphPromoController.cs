using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace DA_BookStore.Controllers.API
{
    public class graphPromoController : ApiController
    {
        public string Get()
        {
            using (var db = new Models.BookStore())
            {
                List<Models.Temp.PromoView> listPromo = new List<Models.Temp.PromoView>();


                db.KHUYENMAIs.ToList()
                        .ForEach(t => listPromo
                                        .Add(new Models.Temp.PromoView(t.TenKhuyenMai, t.NgayBatDau, t.NgayKetThuc, t.PhanTramKhuyenMai ?? 0)));

                var grpPromo = new Models.Temp.Group { group = "Khuyến mãi", data = listPromo };
                var listGrp = new List<Models.Temp.Group>() { grpPromo };

                var json = JsonConvert.SerializeObject(listGrp);
                return json;
            }
        }
    }
}
