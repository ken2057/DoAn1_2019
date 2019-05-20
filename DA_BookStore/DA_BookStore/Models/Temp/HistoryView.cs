using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DA_BookStore.Models.Temp
{
    public class HistoryView
    {
        public List<object> data;

        public HistoryView(string word, double count)
        {
            data = new List<object>() { word, count };
        }

        public void fixCount(double num)
        {
            data[1] = (object)((double)data[1]/num);
            if ((double)data[1] < 0.01)
                data[1] = 0.01;

        }
    }
}