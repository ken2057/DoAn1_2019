namespace DA_BookStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NHANVIEN")]
    public partial class NHANVIEN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NHANVIEN()
        {
            HOADONMUAHANGs = new HashSet<HOADONMUAHANG>();
        }

        [Key]
        [StringLength(50)]
        public string TenTaiKhoanNV { get; set; }

        [StringLength(20)]
        public string ChucVuNV { get; set; }

        public bool? HienThiNV { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HOADONMUAHANG> HOADONMUAHANGs { get; set; }

        public virtual TAIKHOAN TAIKHOAN { get; set; }
    }
}
