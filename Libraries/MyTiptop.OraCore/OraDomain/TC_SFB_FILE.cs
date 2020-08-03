namespace MyTiptop.OraCore
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    //[Table("S30.TC_SFB_FILE")]
    public partial class TC_SFB_FILE
    {
        [Key]
        [StringLength(40)]
        public string TC_SFB01 { get; set; }

        [StringLength(40)]
        public string TC_SFB02 { get; set; }

        [StringLength(30)]
        public string TC_SFB03 { get; set; }

        [StringLength(4)]
        public string TC_SFB04 { get; set; }

        [StringLength(10)]
        public string TC_SFB05 { get; set; }

        [StringLength(10)]
        public string TC_SFB06 { get; set; }

        [StringLength(80)]
        public string TC_SFB07 { get; set; }

        public decimal? TC_SFB08 { get; set; }

        [StringLength(255)]
        public string TC_SFB09 { get; set; }

        [StringLength(1)]
        public string TC_SFB10 { get; set; }

        public DateTime? TC_SFB11 { get; set; }

        [StringLength(1)]
        public string TC_SFB12 { get; set; }

        public decimal? TC_SFB13 { get; set; }

        [StringLength(10)]
        public string TC_SFB14 { get; set; }

        public DateTime? TC_SFB15 { get; set; }

        [StringLength(255)]
        public string TC_SFB16 { get; set; }

        public DateTime? TC_SFB17 { get; set; }

        public DateTime? TC_SFB18 { get; set; }

        [StringLength(10)]
        public string TC_SFBUSER { get; set; }

        [StringLength(10)]
        public string TC_SFBGRUP { get; set; }

        [StringLength(10)]
        public string TC_SFBMODU { get; set; }

        public DateTime? TC_SFBDATE { get; set; }

        [StringLength(255)]
        public string TC_SFBUD01 { get; set; }

        [StringLength(40)]
        public string TC_SFBUD02 { get; set; }

        [StringLength(40)]
        public string TC_SFBUD03 { get; set; }

        [StringLength(40)]
        public string TC_SFBUD04 { get; set; }

        [StringLength(40)]
        public string TC_SFBUD05 { get; set; }

        [StringLength(40)]
        public string TC_SFBUD06 { get; set; }

        public decimal? TC_SFBUD07 { get; set; }

        public decimal? TC_SFBUD08 { get; set; }

        public decimal? TC_SFBUD09 { get; set; }

        public int? TC_SFBUD10 { get; set; }

        public int? TC_SFBUD11 { get; set; }

        public int? TC_SFBUD12 { get; set; }

        public DateTime? TC_SFBUD13 { get; set; }

        public DateTime? TC_SFBUD14 { get; set; }

        public DateTime? TC_SFBUD15 { get; set; }
    }
}
