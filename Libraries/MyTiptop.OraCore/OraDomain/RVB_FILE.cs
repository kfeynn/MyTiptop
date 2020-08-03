namespace MyTiptop.OraCore
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    //[Table("S10.RVB_FILE")]
    public partial class RVB_FILE
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string RVB01 { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short RVB02 { get; set; }

        public short? RVB03 { get; set; }

        [StringLength(20)]
        public string RVB04 { get; set; }

        [StringLength(40)]
        public string RVB05 { get; set; }

        public decimal RVB06 { get; set; }

        public decimal RVB07 { get; set; }

        public decimal RVB08 { get; set; }

        public decimal RVB09 { get; set; }

        public decimal RVB10 { get; set; }

        public short? RVB11 { get; set; }

        public DateTime? RVB12 { get; set; }

        [StringLength(24)]
        public string RVB13 { get; set; }

        [StringLength(10)]
        public string RVB14 { get; set; }

        public decimal? RVB15 { get; set; }

        public decimal? RVB16 { get; set; }

        [StringLength(24)]
        public string RVB17 { get; set; }

        [StringLength(2)]
        public string RVB18 { get; set; }

        [StringLength(1)]
        public string RVB19 { get; set; }

        [StringLength(10)]
        public string RVB20 { get; set; }

        public short? RVB21 { get; set; }

        [StringLength(20)]
        public string RVB22 { get; set; }

        [StringLength(20)]
        public string RVB25 { get; set; }

        public DateTime? RVB26 { get; set; }

        public decimal? RVB27 { get; set; }

        public decimal? RVB28 { get; set; }

        public decimal RVB29 { get; set; }

        public decimal RVB30 { get; set; }

        public decimal RVB31 { get; set; }

        public decimal? RVB32 { get; set; }

        public decimal? RVB33 { get; set; }

        [StringLength(20)]
        public string RVB34 { get; set; }

        [StringLength(1)]
        public string RVB35 { get; set; }

        [StringLength(10)]
        public string RVB36 { get; set; }

        [StringLength(10)]
        public string RVB37 { get; set; }

        [StringLength(24)]
        public string RVB38 { get; set; }

        [StringLength(1)]
        public string RVB39 { get; set; }

        public DateTime? RVB40 { get; set; }

        [StringLength(40)]
        public string RVB41 { get; set; }

        [StringLength(4)]
        public string RVB80 { get; set; }

        public decimal? RVB81 { get; set; }

        public decimal? RVB82 { get; set; }

        [StringLength(4)]
        public string RVB83 { get; set; }

        public decimal? RVB84 { get; set; }

        public decimal? RVB85 { get; set; }

        [StringLength(4)]
        public string RVB86 { get; set; }

        public decimal? RVB87 { get; set; }

        public decimal? RVB88 { get; set; }

        public decimal? RVB88T { get; set; }

        public decimal? RVB10T { get; set; }

        public decimal? RVB331 { get; set; }

        public decimal? RVB332 { get; set; }

        [StringLength(10)]
        public string RVB930 { get; set; }

        [StringLength(255)]
        public string RVBUD01 { get; set; }

        [StringLength(40)]
        public string RVBUD02 { get; set; }

        [StringLength(40)]
        public string RVBUD03 { get; set; }

        [StringLength(40)]
        public string RVBUD04 { get; set; }

        [StringLength(40)]
        public string RVBUD05 { get; set; }

        [StringLength(40)]
        public string RVBUD06 { get; set; }

        public decimal? RVBUD07 { get; set; }

        public decimal? RVBUD08 { get; set; }

        public decimal? RVBUD09 { get; set; }

        public int? RVBUD10 { get; set; }

        public int? RVBUD11 { get; set; }

        public int? RVBUD12 { get; set; }

        public DateTime? RVBUD13 { get; set; }

        public DateTime? RVBUD14 { get; set; }

        public DateTime? RVBUD15 { get; set; }

        [StringLength(120)]
        public string RVB051 { get; set; }

        [StringLength(1)]
        public string RVB89 { get; set; }

        [StringLength(4)]
        public string RVB90 { get; set; }

        public decimal? RVB90_FAC { get; set; }

        [Required]
        [StringLength(1)]
        public string RVB42 { get; set; }

        [StringLength(20)]
        public string RVB43 { get; set; }

        [StringLength(20)]
        public string RVB44 { get; set; }

        public short? RVB45 { get; set; }

        [Required]
        [StringLength(10)]
        public string RVBPLANT { get; set; }

        [Required]
        [StringLength(10)]
        public string RVBLEGAL { get; set; }

        [StringLength(15)]
        public string RVB93 { get; set; }

        [StringLength(50)]
        public string RVB919 { get; set; }
    }
}
