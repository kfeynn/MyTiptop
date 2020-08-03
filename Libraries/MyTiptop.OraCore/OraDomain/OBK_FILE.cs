namespace MyTiptop.OraCore
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    //[Table("S10.OBK_FILE")]
    public partial class OBK_FILE
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(40)]
        public string OBK01 { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string OBK02 { get; set; }

        [StringLength(40)]
        public string OBK03 { get; set; }

        public DateTime? OBK04 { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(4)]
        public string OBK05 { get; set; }

        [StringLength(4)]
        public string OBK06 { get; set; }

        [StringLength(4)]
        public string OBK07 { get; set; }

        public decimal? OBK08 { get; set; }

        public decimal? OBK09 { get; set; }

        public decimal? OBK10 { get; set; }

        [StringLength(1)]
        public string OBK11 { get; set; }

        [StringLength(1)]
        public string OBK12 { get; set; }

        [StringLength(1)]
        public string OBK13 { get; set; }

        [StringLength(1)]
        public string OBK14 { get; set; }

        [StringLength(1)]
        public string OBKACTI { get; set; }

        public DateTime? OBKDATE { get; set; }

        [StringLength(10)]
        public string OBKGRUP { get; set; }

        [StringLength(10)]
        public string OBKUSER { get; set; }

        [StringLength(10)]
        public string OBKMODU { get; set; }

        [StringLength(10)]
        public string OBKORIG { get; set; }

        [StringLength(10)]
        public string OBKORIU { get; set; }

        public short? TA_OBKUD01 { get; set; }

        public short? TA_OBKUD02 { get; set; }
    }
}
