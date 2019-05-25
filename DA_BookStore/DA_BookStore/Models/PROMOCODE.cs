namespace DA_BookStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PROMOCODE")]
    public partial class PROMOCODE
    {
        [Key]
        [StringLength(10)]
        public string CODE { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayThem { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayHetHan { get; set; }

        public int? SoTienGiam { get; set; }
    }
}
