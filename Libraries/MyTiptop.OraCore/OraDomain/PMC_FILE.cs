namespace MyTiptop.OraCore
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("S10.PMC_FILE")]
    public partial class PMC_FILE
    {
        [Key]
        [StringLength(10)]
        public string PMC01 { get; set; }

        [StringLength(10)]
        public string PMC02 { get; set; }

        [StringLength(40)]
        public string PMC03 { get; set; }

        [StringLength(10)]
        public string PMC04 { get; set; }

        [StringLength(1)]
        public string PMC05 { get; set; }

        [StringLength(10)]
        public string PMC06 { get; set; }

        [StringLength(10)]
        public string PMC07 { get; set; }

        [StringLength(80)]
        public string PMC081 { get; set; }

        [StringLength(80)]
        public string PMC082 { get; set; }

        [StringLength(255)]
        public string PMC091 { get; set; }

        [StringLength(255)]
        public string PMC092 { get; set; }

        [StringLength(255)]
        public string PMC093 { get; set; }

        [StringLength(255)]
        public string PMC094 { get; set; }

        [StringLength(255)]
        public string PMC095 { get; set; }

        [StringLength(40)]
        public string PMC10 { get; set; }

        [StringLength(40)]
        public string PMC11 { get; set; }

        [StringLength(40)]
        public string PMC12 { get; set; }

        [StringLength(1)]
        public string PMC13 { get; set; }

        [StringLength(1)]
        public string PMC14 { get; set; }

        [StringLength(10)]
        public string PMC15 { get; set; }

        [StringLength(10)]
        public string PMC16 { get; set; }

        [StringLength(10)]
        public string PMC17 { get; set; }

        [StringLength(1)]
        public string PMC18 { get; set; }

        [StringLength(1)]
        public string PMC19 { get; set; }

        [StringLength(1)]
        public string PMC20 { get; set; }

        [StringLength(1)]
        public string PMC21 { get; set; }

        [StringLength(4)]
        public string PMC22 { get; set; }

        [StringLength(1)]
        public string PMC23 { get; set; }

        [StringLength(20)]
        public string PMC24 { get; set; }

        [StringLength(2)]
        public string PMC25 { get; set; }

        [StringLength(24)]
        public string PMC26 { get; set; }

        [StringLength(1)]
        public string PMC27 { get; set; }

        public short? PMC28 { get; set; }

        [StringLength(1)]
        public string PMC30 { get; set; }

        public DateTime? PMC40 { get; set; }

        public DateTime? PMC41 { get; set; }

        public DateTime? PMC42 { get; set; }

        public DateTime? PMC43 { get; set; }

        public DateTime? PMC44 { get; set; }

        public decimal? PMC45 { get; set; }

        public decimal? PMC46 { get; set; }

        [StringLength(4)]
        public string PMC47 { get; set; }

        [StringLength(1)]
        public string PMC48 { get; set; }

        [StringLength(20)]
        public string PMC49 { get; set; }

        public short? PMC50 { get; set; }

        public short? PMC51 { get; set; }

        [StringLength(255)]
        public string PMC52 { get; set; }

        [StringLength(255)]
        public string PMC53 { get; set; }

        [StringLength(10)]
        public string PMC54 { get; set; }

        [StringLength(20)]
        public string PMC55 { get; set; }

        [StringLength(30)]
        public string PMC56 { get; set; }

        [StringLength(10)]
        public string PMC901 { get; set; }

        [StringLength(1)]
        public string PMC902 { get; set; }

        [StringLength(1)]
        public string PMC903 { get; set; }

        [StringLength(10)]
        public string PMC904 { get; set; }

        [StringLength(1)]
        public string PMC905 { get; set; }

        [StringLength(1)]
        public string PMC906 { get; set; }

        [StringLength(1)]
        public string PMC907 { get; set; }

        [StringLength(10)]
        public string PMC908 { get; set; }

        [StringLength(30)]
        public string PMC909 { get; set; }

        [StringLength(30)]
        public string PMC910 { get; set; }

        [StringLength(1)]
        public string PMC911 { get; set; }

        [StringLength(1)]
        public string PMCACTI { get; set; }

        [StringLength(10)]
        public string PMCUSER { get; set; }

        [StringLength(10)]
        public string PMCGRUP { get; set; }

        [StringLength(10)]
        public string PMCMODU { get; set; }

        public DateTime? PMCDATE { get; set; }

        [StringLength(255)]
        public string PMCUD01 { get; set; }

        [StringLength(80)]
        public string PMCUD02 { get; set; }

        [StringLength(40)]
        public string PMCUD03 { get; set; }

        [StringLength(40)]
        public string PMCUD04 { get; set; }

        [StringLength(40)]
        public string PMCUD05 { get; set; }

        [StringLength(40)]
        public string PMCUD06 { get; set; }

        public decimal? PMCUD07 { get; set; }

        public decimal? PMCUD08 { get; set; }

        public decimal? PMCUD09 { get; set; }

        public int? PMCUD10 { get; set; }

        public int? PMCUD11 { get; set; }

        public int? PMCUD12 { get; set; }

        public DateTime? PMCUD13 { get; set; }

        public DateTime? PMCUD14 { get; set; }

        public DateTime? PMCUD15 { get; set; }

        [StringLength(1)]
        public string PMC912 { get; set; }

        [StringLength(10)]
        public string PMC1912 { get; set; }

        [StringLength(10)]
        public string PMC1913 { get; set; }

        [StringLength(10)]
        public string PMC1914 { get; set; }

        [StringLength(10)]
        public string PMC1915 { get; set; }

        [StringLength(10)]
        public string PMC1916 { get; set; }

        [StringLength(6)]
        public string PMC1917 { get; set; }

        public decimal? PMC1918 { get; set; }

        [StringLength(1)]
        public string PMC1919 { get; set; }

        [Required]
        [StringLength(10)]
        public string PMC1920 { get; set; }

        public int? PMC1921 { get; set; }

        [StringLength(1)]
        public string PMC913 { get; set; }

        [StringLength(1)]
        public string PMC281 { get; set; }

        [StringLength(1)]
        public string PMC914 { get; set; }

        [StringLength(10)]
        public string PMC915 { get; set; }

        [StringLength(10)]
        public string PMC916 { get; set; }

        [StringLength(10)]
        public string PMC917 { get; set; }

        [StringLength(10)]
        public string PMC918 { get; set; }

        [StringLength(5)]
        public string PMC919 { get; set; }

        [StringLength(5)]
        public string PMC920 { get; set; }

        [StringLength(5)]
        public string PMC921 { get; set; }

        [StringLength(5)]
        public string PMC922 { get; set; }

        [StringLength(5)]
        public string PMC923 { get; set; }

        [StringLength(2)]
        public string PMC57 { get; set; }

        public short? PMC58 { get; set; }

        [StringLength(2)]
        public string PMC59 { get; set; }

        [StringLength(8)]
        public string PMC60 { get; set; }

        [StringLength(10)]
        public string PMC930 { get; set; }

        public DateTime? PMCCRAT { get; set; }

        [StringLength(10)]
        public string PMCORIU { get; set; }

        [StringLength(10)]
        public string PMCORIG { get; set; }

        public short? PMC29 { get; set; }

        [StringLength(1)]
        public string PMCUD16 { get; set; }

        [StringLength(1)]
        public string PMCUD17 { get; set; }

        [StringLength(1)]
        public string PMCUD18 { get; set; }

        [StringLength(40)]
        public string PMCUD19 { get; set; }

        [StringLength(40)]
        public string PMCUD20 { get; set; }
    }
}
