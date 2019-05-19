using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DA_BookStore.Models.Temp
{
   

    public class PromoView
    {
        public string label { get; set; }
        public List<object> data { get; set; }

        private class tempPromo
        {
            public List<DateTime> timeRange;
            public string var = "1";

            public tempPromo(DateTime? bd, DateTime? kt)
            {
                timeRange = new List<DateTime>();
                timeRange.Add(bd ?? DateTime.Now);
                timeRange.Add(kt ?? DateTime.Now);
            }
        }

        public PromoView(string lb, DateTime? bd, DateTime? kt)
        {
            label = lb;
            data = new List<object>() { new tempPromo(bd,kt) };
        }
    }

    public class Group
    {
        public string group { get; set; }
        public List<PromoView> data { get; set; }
    }
}