namespace MyTiptop.OraCore
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    //[Table("S30.QCS_FILE")]
    public partial class QCS_FILE
    {
        [StringLength(1)]
        public string QCS00 { get; set; }

        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string QCS01 { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short QCS02 { get; set; }

        [StringLength(40)]
        public string QCS021 { get; set; }

        [StringLength(10)]
        public string QCS03 { get; set; }

        public DateTime? QCS04 { get; set; }

        [StringLength(8)]
        public string QCS041 { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short QCS05 { get; set; }

        public decimal? QCS06 { get; set; }

        public decimal? QCS061 { get; set; }

        [StringLength(4)]
        public string QCS062 { get; set; }

        public decimal? QCS071 { get; set; }

        [StringLength(4)]
        public string QCS072 { get; set; }

        public decimal? QCS081 { get; set; }

        [StringLength(4)]
        public string QCS082 { get; set; }

        [StringLength(1)]
        public string QCS09 { get; set; }

        public decimal? QCS091 { get; set; }

        [StringLength(10)]
        public string QCS10 { get; set; }

        public short? QCS101 { get; set; }

        [StringLength(1)]
        public string QCS11 { get; set; }

        [StringLength(255)]
        public string QCS12 { get; set; }

        [StringLength(10)]
        public string QCS13 { get; set; }

        [StringLength(1)]
        public string QCS14 { get; set; }

        public DateTime? QCS15 { get; set; }

        [StringLength(1)]
        public string QCS16 { get; set; }

        [StringLength(1)]
        public string QCS17 { get; set; }

        public DateTime? QCS18 { get; set; }

        [StringLength(8)]
        public string QCS19 { get; set; }

        [StringLength(30)]
        public string QCS20 { get; set; }

        [StringLength(1)]
        public string QCS21 { get; set; }

        public decimal? QCS22 { get; set; }

        public short? QCSPRNO { get; set; }

        [StringLength(1)]
        public string QCSACTI { get; set; }

        [StringLength(10)]
        public string QCSUSER { get; set; }

        [StringLength(10)]
        public string QCSGRUP { get; set; }

        [StringLength(10)]
        public string QCSMODU { get; set; }

        public DateTime? QCSDATE { get; set; }

        [StringLength(4)]
        public string QCS30 { get; set; }

        public decimal? QCS31 { get; set; }

        public decimal? QCS32 { get; set; }

        [StringLength(4)]
        public string QCS33 { get; set; }

        public decimal? QCS34 { get; set; }

        public decimal? QCS35 { get; set; }

        [StringLength(4)]
        public string QCS36 { get; set; }

        public decimal? QCS37 { get; set; }

        public decimal? QCS38 { get; set; }

        [StringLength(4)]
        public string QCS39 { get; set; }

        public decimal? QCS40 { get; set; }

        public decimal? QCS41 { get; set; }

        [StringLength(1)]
        public string QCSSPC { get; set; }

        [StringLength(255)]
        public string QCSUD01 { get; set; }

        [StringLength(40)]
        public string QCSUD02 { get; set; }

        [StringLength(40)]
        public string QCSUD03 { get; set; }

        [StringLength(40)]
        public string QCSUD04 { get; set; }

        [StringLength(40)]
        public string QCSUD05 { get; set; }

        [StringLength(40)]
        public string QCSUD06 { get; set; }

        public decimal? QCSUD07 { get; set; }

        public decimal? QCSUD08 { get; set; }

        public decimal? QCSUD09 { get; set; }

        public int? QCSUD10 { get; set; }

        public int? QCSUD11 { get; set; }

        public int? QCSUD12 { get; set; }

        public DateTime? QCSUD13 { get; set; }

        public DateTime? QCSUD14 { get; set; }

        public DateTime? QCSUD15 { get; set; }

        [Required]
        [StringLength(10)]
        public string QCSPLANT { get; set; }

        [Required]
        [StringLength(10)]
        public string QCSLEGAL { get; set; }

        [StringLength(10)]
        public string QCSORIU { get; set; }

        [StringLength(10)]
        public string QCSORIG { get; set; }
    }
}
