namespace MyTiptop.OraCore
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    //[Table("S30.SFV_FILE")]
    public partial class SFV_FILE
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string SFV01 { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short SFV03 { get; set; }

        [StringLength(40)]
        public string SFV04 { get; set; }

        [StringLength(10)]
        public string SFV05 { get; set; }

        [StringLength(10)]
        public string SFV06 { get; set; }

        [StringLength(24)]
        public string SFV07 { get; set; }

        [StringLength(4)]
        public string SFV08 { get; set; }

        public decimal? SFV09 { get; set; }

        [StringLength(40)]
        public string SFV11 { get; set; }

        [StringLength(255)]
        public string SFV12 { get; set; }

        public decimal? SFV13 { get; set; }

        public short? SFV14 { get; set; }

        public short? SFV15 { get; set; }

        [StringLength(1)]
        public string SFV16 { get; set; }

        [StringLength(20)]
        public string SFV17 { get; set; }

        [StringLength(1)]
        public string SFV18 { get; set; }

        [StringLength(1)]
        public string SFV19 { get; set; }

        [StringLength(23)]
        public string SFV20 { get; set; }

        [StringLength(4)]
        public string SFV30 { get; set; }

        public decimal? SFV31 { get; set; }

        public decimal? SFV32 { get; set; }

        [StringLength(4)]
        public string SFV33 { get; set; }

        public decimal? SFV34 { get; set; }

        public decimal? SFV35 { get; set; }

        [StringLength(10)]
        public string SFV930 { get; set; }

        [StringLength(40)]
        public string SFV41 { get; set; }

        [StringLength(30)]
        public string SFV42 { get; set; }

        [StringLength(4)]
        public string SFV43 { get; set; }

        [StringLength(10)]
        public string SFV44 { get; set; }

        [StringLength(255)]
        public string SFVUD01 { get; set; }

        [StringLength(40)]
        public string SFVUD02 { get; set; }

        [StringLength(40)]
        public string SFVUD03 { get; set; }

        [StringLength(40)]
        public string SFVUD04 { get; set; }

        [StringLength(40)]
        public string SFVUD05 { get; set; }

        [StringLength(40)]
        public string SFVUD06 { get; set; }

        public decimal? SFVUD07 { get; set; }

        public decimal? SFVUD08 { get; set; }

        public decimal? SFVUD09 { get; set; }

        public int? SFVUD10 { get; set; }

        public int? SFVUD11 { get; set; }

        public int? SFVUD12 { get; set; }

        public DateTime? SFVUD13 { get; set; }

        public DateTime? SFVUD14 { get; set; }

        public DateTime? SFVUD15 { get; set; }

        [Required]
        [StringLength(10)]
        public string SFVPLANT { get; set; }

        [Required]
        [StringLength(10)]
        public string SFVLEGAL { get; set; }

        [StringLength(20)]
        public string SFV45 { get; set; }
    }
}
