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
        //CAI DANH SACH COdE M CO LUU TRONG DB, � l� ?ang test ?�, n� hi?n ra danh s�ch chi ti?t gi? h�ng, c�n list code ch?a l�m, gi? n� hi?n ra cti? h�ng coi nh? listcode � v h ch? cho n� hi?n ct gi? h�ng th�i pk, ? , n?u ???c qua c�i API n�y 
    }
}
