namespace MyTiptop.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("rvvList")]
    public partial class rvvList
    {
        public int id { get; set; }

        [StringLength(50)]
        public string rvv32 { get; set; }

        [StringLength(50)]
        public string rvv33 { get; set; }

        [StringLength(100)]
        public string qrcode { get; set; }

        [StringLength(100)]
        public string qrcode2 { get; set; }
    }
}
