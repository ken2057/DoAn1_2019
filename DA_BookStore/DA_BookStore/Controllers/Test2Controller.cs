using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace DA_BookStore.Controllers
{
    public class Test2Controller : ApiController
    {
        //public IEnumerable<Models.QUANGCAO> Get()
        //{
        //    using (var db = new Models.BookStore())
        //    {
        //        return db.QUANGCAOs.ToList();
        //    }
        //}
        // GET: api/Test2/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Test2
        //public void Post([FromBody]string value){}

        // PUT: api/Test2/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Test2/5
        public void Delete(int id)
        {
        }

        
        public string Get()
        {
            using (var db = new Models.BookStore())
            {
                List<Models.Temp.PromoView> listPromo = new List<Models.Temp.PromoView>();


                db.KHUYENMAIs.ToList()
                        .ForEach(t => listPromo
                                        .Add( new Models.Temp.PromoView(t.TenKhuyenMai, t.NgayBatDau, t.NgayKetThuc,  t.PhanTramKhuyenMai ?? 0) ));

                JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                };

                var grpPromo = new Models.Temp.Group { group = "A", data = listPromo };
                var listGrp = new List<Models.Temp.Group>() { grpPromo };
                //var json = JsonConvert.SerializeObject(listGrp, microsoftDateFormatSettings);
                var json = JsonConvert.SerializeObject(listGrp);
                return json;
            }
        }
    }
}
