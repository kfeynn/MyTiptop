namespace MyTiptop.OraCore
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    //[Table("S30.TC_BRE_FILE")]
    public partial class TC_BRE_FILE
    {
        [Key]
        [StringLength(60)]
        public string TC_BRE01 { get; set; }

        [StringLength(20)]
        public string TC_BRE02 { get; set; }

        [StringLength(20)]
        public string TC_BRE03 { get; set; }

        [StringLength(20)]
        public string TC_BRE04 { get; set; }

        [StringLength(20)]
        public string TC_BRE05 { get; set; }

        [StringLength(20)]
        public string TC_BREUSER { get; set; }

        [StringLength(10)]
        public string TC_BREGRUP { get; set; }

        [StringLength(20)]
        public string TC_BREMODU { get; set; }

        public DateTime? TC_BREDATE { get; set; }

        [StringLength(80)]
        public string TC_BREUD01 { get; set; }

        [StringLength(80)]
        public string TC_BREUD02 { get; set; }

        public decimal? TC_BREUD03 { get; set; }

        public decimal? TC_BREUD04 { get; set; }

        public DateTime? TC_BREUD05 { get; set; }

        public DateTime? TC_BREUD06 { get; set; }

        [StringLength(10)]
        public string TC_BREPLANT { get; set; }

        [StringLength(10)]
        public string TC_BRELEGAL { get; set; }
    }
}
