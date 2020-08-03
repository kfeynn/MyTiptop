namespace MyTiptop.OraCore
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    //[Table("S30.TC_SFC_FILE")]
    public partial class TC_SFC_FILE
    {
        [Key]
        [StringLength(20)]
        public string TC_SFC01 { get; set; }

        [StringLength(10)]
        public string TC_SFC02 { get; set; }

        [StringLength(10)]
        public string TC_SFC03 { get; set; }

        public DateTime? TC_SFC04 { get; set; }

        [StringLength(10)]
        public string TC_SFC05 { get; set; }

        [StringLength(10)]
        public string TC_SFC06 { get; set; }

        [StringLength(1)]
        public string TC_SFC07 { get; set; }

        [StringLength(255)]
        public string TC_SFC08 { get; set; }

        [StringLength(1)]
        public string TC_SFC09 { get; set; }

        public DateTime? TC_SFC10 { get; set; }

        [StringLength(1)]
        public string TC_SFC11 { get; set; }

        [StringLength(10)]
        public string TC_SFC12 { get; set; }

        public DateTime? TC_SFC13 { get; set; }

        [StringLength(1)]
        public string TC_SFCACTI { get; set; }

        [StringLength(10)]
        public string TC_SFCUSER { get; set; }

        [StringLength(10)]
        public string TC_SFCGRUP { get; set; }

        [StringLength(10)]
        public string TC_SFCMODU { get; set; }

        public DateTime? TC_SFCDATE { get; set; }

        [StringLength(255)]
        public string TC_SFCUD01 { get; set; }

        [StringLength(40)]
        public string TC_SFCUD02 { get; set; }

        [StringLength(40)]
        public string TC_SFCUD03 { get; set; }

        [StringLength(40)]
        public string TC_SFCUD04 { get; set; }

        [StringLength(40)]
        public string TC_SFCUD05 { get; set; }

        [StringLength(40)]
        public string TC_SFCUD06 { get; set; }

        public decimal? TC_SFCUD07 { get; set; }

        public decimal? TC_SFCUD08 { get; set; }

        public decimal? TC_SFCUD09 { get; set; }

        public int? TC_SFCUD10 { get; set; }

        public int? TC_SFCUD11 { get; set; }

        public int? TC_SFCUD12 { get; set; }

        public DateTime? TC_SFCUD13 { get; set; }

        public DateTime? TC_SFCUD14 { get; set; }

        public DateTime? TC_SFCUD15 { get; set; }

        [StringLength(10)]
        public string TC_SFCORIU { get; set; }

        [StringLength(10)]
        public string TC_SFCORIG { get; set; }
    }
}
