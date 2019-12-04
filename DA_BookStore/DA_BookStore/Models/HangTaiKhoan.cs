namespace DA_BookStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HangTaiKhoan")]
    public partial class HangTaiKhoan
    {
        public int id { get; set; }

        [Column("_start")]
        public int? C_start { get; set; }

        [Column("_end")]
        public int? C_end { get; set; }

        [StringLength(20)]
        public string TenHang { get; set; }
    }
}
