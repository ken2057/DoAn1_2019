using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DA_BookStore.Controllers.API
{
    public class graphHistoryController : ApiController
    {
        public string Get()
        {
            using(var db = new Models.BookStore())
            {
                List<object> grp = new List<object>();
                Dictionary<string, double> dctCT = new Dictionary<string, double>();

                foreach (var item in db.CTXEMSACHes.ToList())
                    if (!dctCT.Keys.Contains(item.MaSach))
                        dctCT.Add(item.MaSach, 1);
                    else
                        dctCT[item.MaSach] += 1;

                foreach (var item in dctCT)
                    grp.Add(new List<object> { item.Key, item.Value/dctCT.Values.Max() });


                var json = JsonConvert.SerializeObject(grp);
                return json;
            }

           
        }
    }
}
