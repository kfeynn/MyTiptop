namespace MyTiptop.OraCore
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    //[Table("S10.TC_BRB_FILE")]
    public partial class TC_BRB_FILE
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(60)]
        public string TC_BRB01 { get; set; }

        [StringLength(10)]
        public string TC_BRB02 { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(40)]
        public string TC_BRB03 { get; set; }

        [StringLength(1000)]
        public string TC_BRB06 { get; set; }

        [StringLength(1000)]
        public string TC_BRB07 { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime TC_BRB08 { get; set; }

        [StringLength(6)]
        public string TC_BRB09 { get; set; }

        [StringLength(4)]
        public string TC_BRB10 { get; set; }

        public decimal? TC_BRB11 { get; set; }

        public decimal? TC_BRB12 { get; set; }

        public decimal? TC_BRB13 { get; set; }

        [StringLength(1)]
        public string TC_BRB14 { get; set; }

        [StringLength(2)]
        public string TC_BRB15 { get; set; }

        public short? TC_BRB17 { get; set; }

        public decimal? TC_BRB18 { get; set; }

        public decimal? TC_BRB19 { get; set; }

        public decimal? TC_BRB20 { get; set; }

        [StringLength(80)]
        public string TC_BRB21 { get; set; }

        [StringLength(80)]
        public string TC_BRB22 { get; set; }

        [StringLength(80)]
        public string TC_BRB23 { get; set; }

        [StringLength(80)]
        public string TC_BRB24 { get; set; }

        [StringLength(80)]
        public string TC_BRB25 { get; set; }

        [StringLength(10)]
        public string TC_BRBPLANT { get; set; }

        [StringLength(10)]
        public string TC_BRBLEGAL { get; set; }
    }
}
