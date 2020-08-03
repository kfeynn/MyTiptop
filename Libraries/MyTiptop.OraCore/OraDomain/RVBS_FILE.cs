namespace MyTiptop.OraCore
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    //[Table("S30.RVBS_FILE")]
    public partial class RVBS_FILE
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string RVBS00 { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string RVBS01 { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short RVBS02 { get; set; }

        [Required]
        [StringLength(30)]
        public string RVBS03 { get; set; }

        [Required]
        [StringLength(30)]
        public string RVBS04 { get; set; }

        public DateTime? RVBS05 { get; set; }

        public decimal? RVBS06 { get; set; }

        [StringLength(1)]
        public string RVBS07 { get; set; }

        [Required]
        [StringLength(20)]
        public string RVBS08 { get; set; }

        [StringLength(40)]
        public string RVBS021 { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short RVBS022 { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short RVBS09 { get; set; }

        public decimal? RVBS10 { get; set; }

        public decimal? RVBS11 { get; set; }

        public decimal? RVBS12 { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short RVBS13 { get; set; }

        [Required]
        [StringLength(10)]
        public string RVBSPLANT { get; set; }

        [Required]
        [StringLength(10)]
        public string RVBSLEGAL { get; set; }
    }
}
