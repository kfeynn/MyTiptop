namespace MyTiptop.OraCore
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    //[Table("S30.ZO_FILE")]
    public partial class ZO_FILE
    {
        [Key]
        [StringLength(1)]
        public string ZO01 { get; set; }

        [StringLength(80)]
        public string ZO02 { get; set; }

        [StringLength(255)]
        public string ZO041 { get; set; }

        [StringLength(255)]
        public string ZO042 { get; set; }

        [StringLength(40)]
        public string ZO05 { get; set; }

        [StringLength(20)]
        public string ZO06 { get; set; }

        [StringLength(40)]
        public string ZO07 { get; set; }

        [StringLength(1)]
        public string ZO08 { get; set; }

        [StringLength(40)]
        public string ZO09 { get; set; }

        [StringLength(255)]
        public string ZO10 { get; set; }

        [StringLength(10)]
        public string ZO11 { get; set; }

        [StringLength(80)]
        public string ZO12 { get; set; }

        [StringLength(20)]
        public string ZO13 { get; set; }

        [StringLength(1)]
        public string ZO14 { get; set; }

        [StringLength(1)]
        public string ZO15 { get; set; }

        [StringLength(10)]
        public string ZOUSER { get; set; }

        [StringLength(10)]
        public string ZOGRUP { get; set; }

        [StringLength(10)]
        public string ZOMODU { get; set; }

        public DateTime? ZODATE { get; set; }

        [StringLength(10)]
        public string ZOORIU { get; set; }

        [StringLength(10)]
        public string ZOORIG { get; set; }

        [StringLength(6)]
        public string ZO16 { get; set; }
    }
}
