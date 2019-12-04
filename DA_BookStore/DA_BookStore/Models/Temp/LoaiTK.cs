using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DA_BookStore.Models.Temp
{
    public class LoaiTK
    {
        public int start { get; set; }
        public int end { get; set; }
        public string Loai { get; set; }

        public LoaiTK(int start, int end, string loai)
        {
            this.start = start;
            this.end = end;
            this.Loai = loai;
        }
    }
}