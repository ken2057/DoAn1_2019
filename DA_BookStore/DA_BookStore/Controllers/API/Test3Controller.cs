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
    public class Test3Controller : ApiController
    {
        public string Get()
        {
            var json = new JavaScriptSerializer().Serialize(DateTime.Today);
            
            var json3 = JsonConvert.SerializeObject(DateTime.Now);


            JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
            };
            var jsonDateTime = JsonConvert.SerializeObject(DateTime.Now, microsoftDateFormatSettings);
            //jsonDateTime = jsonDateTime.Replace("\"\\/Date(", "").Replace(")\\/\"", "");

            return jsonDateTime;
        }
    }
}
