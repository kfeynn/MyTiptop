namespace MyTiptop.OraCore
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("S30.TC_QCYY_FILE")]
    public partial class TC_QCYY_FILE
    {
        [Key]
        [StringLength(60)]
        public string TC_QCYY01 { get; set; }

        [Required]
        [StringLength(120)]
        public string TC_QCYY02 { get; set; }

        //[Required]
        [StringLength(120)]
        public string TC_QCYY03 { get; set; }

        [StringLength(60)]
        public string TC_QCYY04 { get; set; }

        [StringLength(120)]
        public string TC_QCYY05 { get; set; }

        [StringLength(60)]
        public string TC_QCYY06 { get; set; }

        [StringLength(60)]
        public string TC_QCYY07 { get; set; }

        public DateTime? TC_QCYY08 { get; set; }

        [StringLength(60)]
        public string TC_QCYY09 { get; set; }

        public DateTime? TC_QCYY10 { get; set; }

        [StringLength(60)]
        public string TC_QCYY11 { get; set; }

        public short? TC_QCYY12 { get; set; }

        [StringLength(60)]
        public string TC_QCYY13 { get; set; }

        public DateTime? TC_QCYY14 { get; set; }

        public short? TC_QCYY15 { get; set; }

        public short? TC_QCYY16 { get; set; }

        [StringLength(60)]
        public string TC_QCYY17 { get; set; }

        [StringLength(60)]
        public string TC_QCYY18 { get; set; }
    }
}
