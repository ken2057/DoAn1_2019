using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DA_BookStore.Models.Temp
{
    public class RpDonMua
    {
        public List<string> listTinhTrang { get; set; }
        public List<int> listSLTinhTrang { get; set; }
        public long TongTienThu { get; set; }

        public RpDonMua(List<string> listTinhTrang)
        {
            this.listTinhTrang = listTinhTrang;
            this.listSLTinhTrang = createZeroList(listTinhTrang.Count);
            TongTienThu = 0;
        }

        private List<int> createZeroList(int length)
        {
            List<int> list = new List<int>();
            for (int i = 0; i < length; i++)
                list.Add(0);
            return list;
        }
    }
}