namespace MyTiptop.OraCore
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    //[Table("S10.TC_BRG_FILE")]
    public partial class TC_BRG_FILE
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(4)]
        public string TC_BRG01 { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(80)]
        public string TC_BRG02 { get; set; }

        [Required]
        [StringLength(60)]
        public string TC_BRG03 { get; set; }

        public DateTime TC_BRG04 { get; set; }

        public decimal? TC_BRG05 { get; set; }

        public decimal? TC_BRG06 { get; set; }

        public decimal? TC_BRG07 { get; set; }

        [StringLength(4)]
        public string TC_BRG08 { get; set; }

        public decimal? TC_BRG09 { get; set; }

        public decimal? TC_BRG10 { get; set; }

        [StringLength(10)]
        public string TC_BRG11 { get; set; }

        [StringLength(40)]
        public string TC_BRG12 { get; set; }

        [StringLength(1000)]
        public string TC_BRG13 { get; set; }

        [StringLength(10)]
        public string TC_BRG14 { get; set; }

        [StringLength(10)]
        public string TC_BRG15 { get; set; }

        [StringLength(4)]
        public string TC_BRG16 { get; set; }

        [StringLength(10)]
        public string TC_BRG17 { get; set; }

        public DateTime? TC_BRG18 { get; set; }

        public DateTime? TC_BRG19 { get; set; }

        public decimal? TC_BRG20 { get; set; }

        public decimal? TC_BRG21 { get; set; }

        public decimal? TC_BRG22 { get; set; }

        [Required]
        [StringLength(10)]
        public string TC_BRGPLANT { get; set; }

        [Required]
        [StringLength(10)]
        public string TC_BRGLEGAL { get; set; }

        [StringLength(80)]
        public string TC_BRG23 { get; set; }

        [StringLength(80)]
        public string TC_BRG24 { get; set; }

        [StringLength(100)]
        public string TC_BRG25 { get; set; }

        [StringLength(80)]
        public string TC_BRG26 { get; set; }

        [StringLength(80)]
        public string TC_BRG27 { get; set; }

        public decimal? TC_BRG28 { get; set; }

        public decimal? TC_BRG29 { get; set; }

        public DateTime? TC_BRG30 { get; set; }

        public DateTime? TC_BRG31 { get; set; }

        [Key]
        [Column(Order = 2)]
        public decimal TC_BRGNO { get; set; }
    }
}
