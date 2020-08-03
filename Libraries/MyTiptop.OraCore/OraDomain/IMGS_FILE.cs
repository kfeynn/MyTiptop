namespace MyTiptop.OraCore
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    //[Table("S10.IMGS_FILE")]
    public partial class IMGS_FILE
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(40)]
        public string IMGS01 { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string IMGS02 { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(10)]
        public string IMGS03 { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(24)]
        public string IMGS04 { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(30)]
        public string IMGS05 { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(30)]
        public string IMGS06 { get; set; }

        [StringLength(4)]
        public string IMGS07 { get; set; }

        public decimal? IMGS08 { get; set; }

        public DateTime? IMGS09 { get; set; }

        [StringLength(1)]
        public string IMGS10 { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(20)]
        public string IMGS11 { get; set; }

        [Required]
        [StringLength(10)]
        public string IMGSPLANT { get; set; }

        [Required]
        [StringLength(10)]
        public string IMGSLEGAL { get; set; }
    }
}
