namespace MyTiptop.OraCore
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    //[Table("S30.SFU_FILE")]
    public partial class SFU_FILE
    {
        [StringLength(1)]
        public string SFU00 { get; set; }

        [Key]
        [StringLength(20)]
        public string SFU01 { get; set; }

        public DateTime? SFU02 { get; set; }

        [StringLength(16)]
        public string SFU03 { get; set; }

        [StringLength(10)]
        public string SFU04 { get; set; }

        [StringLength(10)]
        public string SFU05 { get; set; }

        [StringLength(40)]
        public string SFU06 { get; set; }

        [StringLength(255)]
        public string SFU07 { get; set; }

        [StringLength(20)]
        public string SFU08 { get; set; }

        [StringLength(20)]
        public string SFU09 { get; set; }

        [StringLength(1)]
        public string SFU10 { get; set; }

        [StringLength(1)]
        public string SFU11 { get; set; }

        [StringLength(1)]
        public string SFU12 { get; set; }

        [StringLength(1)]
        public string SFU13 { get; set; }

        [StringLength(1)]
        public string SFUPOST { get; set; }

        [StringLength(10)]
        public string SFUUSER { get; set; }

        [StringLength(10)]
        public string SFUGRUP { get; set; }

        [StringLength(10)]
        public string SFUMODU { get; set; }

        public DateTime? SFUDATE { get; set; }

        [StringLength(1)]
        public string SFUCONF { get; set; }

        [StringLength(255)]
        public string SFUUD01 { get; set; }

        [StringLength(40)]
        public string SFUUD02 { get; set; }

        [StringLength(40)]
        public string SFUUD03 { get; set; }

        [StringLength(40)]
        public string SFUUD04 { get; set; }

        [StringLength(40)]
        public string SFUUD05 { get; set; }

        [StringLength(40)]
        public string SFUUD06 { get; set; }

        public decimal? SFUUD07 { get; set; }

        public decimal? SFUUD08 { get; set; }

        public decimal? SFUUD09 { get; set; }

        public int? SFUUD10 { get; set; }

        public int? SFUUD11 { get; set; }

        public int? SFUUD12 { get; set; }

        public DateTime? SFUUD13 { get; set; }

        public DateTime? SFUUD14 { get; set; }

        public DateTime? SFUUD15 { get; set; }

        public DateTime? SFU14 { get; set; }

        [Required]
        [StringLength(10)]
        public string SFUPLANT { get; set; }

        [Required]
        [StringLength(10)]
        public string SFULEGAL { get; set; }

        [StringLength(10)]
        public string SFUORIU { get; set; }

        [StringLength(10)]
        public string SFUORIG { get; set; }

        [Required]
        [StringLength(1)]
        public string SFU15 { get; set; }

        [StringLength(10)]
        public string SFU16 { get; set; }

        [Required]
        [StringLength(1)]
        public string SFUMKSG { get; set; }
    }
}
