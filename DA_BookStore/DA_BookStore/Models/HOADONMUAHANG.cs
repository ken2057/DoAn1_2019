namespace DA_BookStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HOADONMUAHANG")]
    public partial class HOADONMUAHANG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HOADONMUAHANG()
        {
            CTHOADONMUAHANGs = new HashSet<CTHOADONMUAHANG>();
        }

        [Key]
        [StringLength(10)]
        public string MaHDMua { get; set; }

        [StringLength(20)]
        public string TinhTrangThanhToan { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ThoiGianMua { get; set; }

        [StringLength(50)]
        public string TenTaiKhoan { get; set; }

        public int? ThongTinKH { get; set; }

        public int? TongTien { get; set; }

        [StringLength(10)]
        public string CODE { get; set; }

        [StringLength(50)]
        public string TenTaiKhoanNV { get; set; }

        [StringLength(100)]
        public string MaVanChuyen { get; set; }

        public short? GiamThanhVien { get; set; }

        public string GhiChu { get; set; }

        public string NguoiNhan { get; set; }

        public string DiaChiNhan { get; set; }

        [StringLength(20)]
        public string SdtNhan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTHOADONMUAHANG> CTHOADONMUAHANGs { get; set; }

        public virtual TAIKHOAN TAIKHOAN { get; set; }

        public virtual NHANVIEN NHANVIEN { get; set; }

        public virtual ThongTinKhachHangMua ThongTinKhachHangMua { get; set; }

        public virtual PROMOCODE PROMOCODE { get; set; }
    }
}
