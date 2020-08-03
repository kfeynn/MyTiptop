namespace MyTiptop.OraCore
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    //[Table("S10.IMD_FILE")]
    public partial class IMD_FILE
    {
        [Key]
        [StringLength(10)]
        public string IMD01 { get; set; }

        [StringLength(80)]
        public string IMD02 { get; set; }

        [StringLength(255)]
        public string IMD03 { get; set; }

        [StringLength(255)]
        public string IMD04 { get; set; }

        [StringLength(255)]
        public string IMD05 { get; set; }

        [StringLength(40)]
        public string IMD06 { get; set; }

        [StringLength(40)]
        public string IMD07 { get; set; }

        [StringLength(24)]
        public string IMD08 { get; set; }

        [StringLength(1)]
        public string IMD09 { get; set; }

        [StringLength(1)]
        public string IMD10 { get; set; }

        [StringLength(1)]
        public string IMD11 { get; set; }

        [StringLength(1)]
        public string IMD12 { get; set; }

        [StringLength(1)]
        public string IMD13 { get; set; }

        public short? IMD14 { get; set; }

        public short? IMD15 { get; set; }

        [StringLength(1)]
        public string IMDACTI { get; set; }

        [StringLength(10)]
        public string IMDUSER { get; set; }

        [StringLength(10)]
        public string IMDGRUP { get; set; }

        [StringLength(10)]
        public string IMDMODU { get; set; }

        public DateTime? IMDDATE { get; set; }

        [StringLength(10)]
        public string IMD16 { get; set; }

        [StringLength(24)]
        public string IMD081 { get; set; }

        [Required]
        [StringLength(1)]
        public string IMD17 { get; set; }

        [Required]
        [StringLength(1)]
        public string IMD18 { get; set; }

        [Required]
        [StringLength(1)]
        public string IMD19 { get; set; }

        [StringLength(10)]
        public string IMD20 { get; set; }

        [StringLength(24)]
        public string IMD21 { get; set; }

        [StringLength(24)]
        public string IMD211 { get; set; }

        [Required]
        [StringLength(1)]
        public string IMDPOS { get; set; }

        [StringLength(10)]
        public string IMDORIU { get; set; }

        [StringLength(10)]
        public string IMDORIG { get; set; }

        [Required]
        [StringLength(1)]
        public string IMD22 { get; set; }
    }
}
