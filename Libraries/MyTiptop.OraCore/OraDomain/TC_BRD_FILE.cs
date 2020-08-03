namespace MyTiptop.OraCore
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    //[Table("S10.TC_BRD_FILE")]
    public partial class TC_BRD_FILE
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(60)]
        public string TC_BRD01 { get; set; }

        [StringLength(10)]
        public string TC_BRD02 { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(4)]
        public string TC_BRD03 { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(2)]
        public string TC_BRD04 { get; set; }

        [StringLength(40)]
        public string TC_BRD05 { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(80)]
        public string TC_BRD06 { get; set; }

        [StringLength(1000)]
        public string TC_BRD07 { get; set; }

        public DateTime TC_BRD08 { get; set; }

        [StringLength(6)]
        public string TC_BRD09 { get; set; }

        [StringLength(4)]
        public string TC_BRD10 { get; set; }

        public decimal? TC_BRD11 { get; set; }

        public decimal? TC_BRD12 { get; set; }

        public decimal? TC_BRD13 { get; set; }

        [StringLength(1)]
        public string TC_BRD14 { get; set; }

        [StringLength(1)]
        public string TC_BRD15 { get; set; }

        public short? TC_BRD17 { get; set; }

        public decimal? TC_BRD18 { get; set; }

        public decimal? TC_BRD19 { get; set; }

        public decimal? TC_BRD20 { get; set; }

        [StringLength(80)]
        public string TC_BRD21 { get; set; }

        [StringLength(80)]
        public string TC_BRD22 { get; set; }

        [StringLength(80)]
        public string TC_BRD23 { get; set; }

        [StringLength(80)]
        public string TC_BRD24 { get; set; }

        [StringLength(80)]
        public string TC_BRDACTI { get; set; }

        [StringLength(10)]
        public string TC_BRDPLANT { get; set; }

        [StringLength(10)]
        public string TC_BRDLEGAL { get; set; }
    }
}
