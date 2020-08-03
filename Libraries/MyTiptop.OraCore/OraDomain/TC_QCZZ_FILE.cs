namespace MyTiptop.OraCore
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("S30.TC_QCZZ_FILE")]
    public partial class TC_QCZZ_FILE
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(60)]
        public string TC_QCZZ01 { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(600)]
        public string TC_QCZZ02 { get; set; }

        [StringLength(120)]
        public string TC_QCZZ03 { get; set; }

        [StringLength(120)]
        public string TC_QCZZ04 { get; set; }

        [StringLength(120)]
        public string TC_QCZZ05 { get; set; }

        public int? TC_QCZZ06 { get; set; }

        public int? TC_QCZZ07 { get; set; }

        [StringLength(60)]
        public string TC_QCZZ08 { get; set; }

        [StringLength(60)]
        public string TC_QCZZ09 { get; set; }

        public int? TC_QCZZ10 { get; set; }

        public int? TC_QCZZ11 { get; set; }

        public decimal? TC_QCZZ12 { get; set; }
    }
}
