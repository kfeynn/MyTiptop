namespace MyTiptop.OraCore
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    //[Table("S10.TC_BRS_FILE")]
    public partial class TC_BRS_FILE
    {
        [StringLength(60)]
        public string TC_BRS01 { get; set; }

        [Key]
        [Column(Order = 0)]
        [StringLength(80)]
        public string TC_BRS02 { get; set; }

        [StringLength(2)]
        public string TC_BRS03 { get; set; }

        public byte? TC_BRS04 { get; set; }

        [StringLength(1)]
        public string TC_BRS05 { get; set; }

        [StringLength(10)]
        public string TC_BRS06 { get; set; }

        [StringLength(1)]
        public string TC_BRS07 { get; set; }

        [StringLength(11)]
        public string TC_BRS08 { get; set; }

        [StringLength(11)]
        public string TC_BRS09 { get; set; }

        [StringLength(11)]
        public string TC_BRS10 { get; set; }

        [StringLength(11)]
        public string TC_BRS11 { get; set; }

        [StringLength(11)]
        public string TC_BRS12 { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime TC_BRS13 { get; set; }

        [StringLength(40)]
        public string TC_BRS14 { get; set; }

        [StringLength(40)]
        public string TC_BRS15 { get; set; }

        public DateTime? TC_BRS16 { get; set; }

        public DateTime? TC_BRS17 { get; set; }

        public short? TC_BRS18 { get; set; }

        public short? TC_BRS19 { get; set; }
    }
}
