namespace DA_BookStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CTGIOHANG")]
    public partial class CTGIOHANG
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string MaSach { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string TenTaiKhoan { get; set; }

        public short? SoLuongGioHang { get; set; }

        public virtual SACH SACH { get; set; }

        public virtual TAIKHOAN TAIKHOAN { get; set; }
        //CAI DANH SACH COdE M CO LUU TRONG DB, ý là ?ang test ?ã, nó hi?n ra danh sách chi ti?t gi? hàng, còn list code ch?a làm, gi? nó hi?n ra cti? hàng coi nh? listcode à v h ch? cho nó hi?n ct gi? hàng thôi pk, ? , n?u ???c qua cái API này 
    }
}
