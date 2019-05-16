using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DA_BookStore.Controllers
{
    public class Test2Controller : ApiController
    {
        public IEnumerable<Models.QUANGCAO> Get()
        {
            using (var db = new Models.BookStore())
            {
                return db.QUANGCAOs.ToList();
            }
        }
        // GET: api/Test2/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Test2
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Test2/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Test2/5
        public void Delete(int id)
        {
        }

       
    }
}
