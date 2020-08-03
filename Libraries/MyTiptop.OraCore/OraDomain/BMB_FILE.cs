namespace MyTiptop.OraCore
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    //[Table("S10.BMB_FILE")]
    public partial class BMB_FILE
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(40)]
        public string BMB01 { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short BMB02 { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(40)]
        public string BMB03 { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime BMB04 { get; set; }

        public DateTime? BMB05 { get; set; }

        public decimal? BMB06 { get; set; }

        public decimal? BMB07 { get; set; }

        public decimal? BMB08 { get; set; }

        [StringLength(6)]
        public string BMB09 { get; set; }

        [StringLength(4)]
        public string BMB10 { get; set; }

        public decimal? BMB10_FAC { get; set; }

        public decimal? BMB10_FAC2 { get; set; }

        [StringLength(20)]
        public string BMB11 { get; set; }

        [StringLength(10)]
        public string BMB13 { get; set; }

        [StringLength(1)]
        public string BMB14 { get; set; }

        [StringLength(1)]
        public string BMB15 { get; set; }

        [StringLength(1)]
        public string BMB16 { get; set; }

        [StringLength(1)]
        public string BMB17 { get; set; }

        public short? BMB18 { get; set; }

        [StringLength(1)]
        public string BMB19 { get; set; }

        public short? BMB20 { get; set; }

        [StringLength(1)]
        public string BMB21 { get; set; }

        [StringLength(1)]
        public string BMB22 { get; set; }

        public decimal? BMB23 { get; set; }

        [StringLength(20)]
        public string BMB24 { get; set; }

        [StringLength(10)]
        public string BMB25 { get; set; }

        [StringLength(10)]
        public string BMB26 { get; set; }

        [StringLength(1)]
        public string BMB27 { get; set; }

        public decimal? BMB28 { get; set; }

        [StringLength(10)]
        public string BMBMODU { get; set; }

        public DateTime? BMBDATE { get; set; }

        [StringLength(10)]
        public string BMBCOMM { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(20)]
        public string BMB29 { get; set; }

        [StringLength(1)]
        public string BMB30 { get; set; }

        [StringLength(1)]
        public string BMB31 { get; set; }

        public int BMB33 { get; set; }

        [StringLength(255)]
        public string BMBUD01 { get; set; }

        [StringLength(40)]
        public string BMBUD02 { get; set; }

        [StringLength(40)]
        public string BMBUD03 { get; set; }

        [StringLength(40)]
        public string BMBUD04 { get; set; }

        [StringLength(40)]
        public string BMBUD05 { get; set; }

        [StringLength(40)]
        public string BMBUD06 { get; set; }

        public decimal? BMBUD07 { get; set; }

        public decimal? BMBUD08 { get; set; }

        public decimal? BMBUD09 { get; set; }

        public int? BMBUD10 { get; set; }

        public int? BMBUD11 { get; set; }

        public int? BMBUD12 { get; set; }

        public DateTime? BMBUD13 { get; set; }

        public DateTime? BMBUD14 { get; set; }

        public DateTime? BMBUD15 { get; set; }

        public decimal? BMB081 { get; set; }

        public decimal? BMB082 { get; set; }

        [StringLength(10)]
        public string BMB36 { get; set; }

        [StringLength(40)]
        public string BMB37 { get; set; }
    }
}
