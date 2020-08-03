namespace MyTiptop.OraCore
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    //[Table("S30.ZYW_FILE")]
    public partial class ZYW_FILE
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(6)]
        public string ZYW01 { get; set; }

        [StringLength(80)]
        public string ZYW02 { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string ZYW03 { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(10)]
        public string ZYW04 { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(1)]
        public string ZYW05 { get; set; }

        [StringLength(1)]
        public string ZYW06 { get; set; }

        [StringLength(1)]
        public string ZYW07 { get; set; }

        [StringLength(1)]
        public string ZYW08 { get; set; }

        [StringLength(1)]
        public string ZYWACTI { get; set; }

        [StringLength(10)]
        public string ZYWUSER { get; set; }

        [StringLength(10)]
        public string ZYWGRUP { get; set; }

        [StringLength(10)]
        public string ZYWMODU { get; set; }

        public DateTime? ZYWDATE { get; set; }

        [StringLength(10)]
        public string ZYWORIG { get; set; }

        [StringLength(10)]
        public string ZYWORIU { get; set; }
    }
}
