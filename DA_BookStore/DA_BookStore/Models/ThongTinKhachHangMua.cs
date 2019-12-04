namespace DA_BookStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ThongTinKhachHangMua")]
    public partial class ThongTinKhachHangMua
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ThongTinKhachHangMua()
        {
            HOADONMUAHANGs = new HashSet<HOADONMUAHANG>();
        }

        public int id { get; set; }

        [StringLength(50)]
        public string HoTen { get; set; }

        [Required]
        [StringLength(10)]
        public string Sdt { get; set; }

        [Required]
        [StringLength(100)]
        public string DiaChi { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HOADONMUAHANG> HOADONMUAHANGs { get; set; }
    }
}
