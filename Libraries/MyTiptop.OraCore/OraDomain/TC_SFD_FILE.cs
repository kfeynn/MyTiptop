namespace MyTiptop.OraCore
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    //[Table("S30.TC_SFD_FILE")]
    public partial class TC_SFD_FILE
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(40)]
        public string TC_SFD01 { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short TC_SFD02 { get; set; }

        [StringLength(40)]
        public string TC_SFD03 { get; set; }

        public short? TC_SFD04 { get; set; }

        [StringLength(40)]
        public string TC_SFD05 { get; set; }

        public decimal? TC_SFD06 { get; set; }

        public decimal? TC_SFD07 { get; set; }

        public DateTime? TC_SFD08 { get; set; }

        [StringLength(255)]
        public string TC_SFD09 { get; set; }

        [StringLength(255)]
        public string TC_SFDUD01 { get; set; }

        [StringLength(40)]
        public string TC_SFDUD02 { get; set; }

        [StringLength(40)]
        public string TC_SFDUD03 { get; set; }

        [StringLength(40)]
        public string TC_SFDUD04 { get; set; }

        [StringLength(40)]
        public string TC_SFDUD05 { get; set; }

        [StringLength(40)]
        public string TC_SFDUD06 { get; set; }

        public decimal? TC_SFDUD07 { get; set; }

        public decimal? TC_SFDUD08 { get; set; }

        public decimal? TC_SFDUD09 { get; set; }

        public int? TC_SFDUD10 { get; set; }

        public int? TC_SFDUD11 { get; set; }

        public int? TC_SFDUD12 { get; set; }

        public DateTime? TC_SFDUD13 { get; set; }

        public DateTime? TC_SFDUD14 { get; set; }

        public DateTime? TC_SFDUD15 { get; set; }

        [StringLength(10)]
        public string TC_SFDORIU { get; set; }

        [StringLength(10)]
        public string TC_SFDORIG { get; set; }
    }
}
