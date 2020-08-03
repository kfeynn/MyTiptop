namespace MyTiptop.OraCore
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    //[Table("S10.IMA_FILE")]
    public partial class IMA_FILE
    {
        [Key]
        [StringLength(40)]
        public string IMA01 { get; set; }

        [StringLength(1000)]
        public string IMA02 { get; set; }

        [StringLength(1000)]
        public string IMA021 { get; set; }

        [StringLength(40)]
        public string IMA03 { get; set; }

        [StringLength(255)]
        public string IMA04 { get; set; }

        [StringLength(10)]
        public string IMA05 { get; set; }

        [StringLength(10)]
        public string IMA06 { get; set; }

        [StringLength(1)]
        public string IMA07 { get; set; }

        [StringLength(1)]
        public string IMA08 { get; set; }

        [StringLength(10)]
        public string IMA09 { get; set; }

        [StringLength(10)]
        public string IMA10 { get; set; }

        [StringLength(10)]
        public string IMA11 { get; set; }

        [StringLength(10)]
        public string IMA12 { get; set; }

        [StringLength(40)]
        public string IMA13 { get; set; }

        [StringLength(1)]
        public string IMA14 { get; set; }

        [StringLength(1)]
        public string IMA15 { get; set; }

        public short? IMA16 { get; set; }

        [StringLength(4)]
        public string IMA17 { get; set; }

        public decimal? IMA17_FAC { get; set; }

        public decimal? IMA18 { get; set; }

        [StringLength(4)]
        public string IMA19 { get; set; }

        public decimal? IMA20 { get; set; }

        [StringLength(20)]
        public string IMA21 { get; set; }

        public decimal? IMA22 { get; set; }

        [StringLength(10)]
        public string IMA23 { get; set; }

        [StringLength(1)]
        public string IMA24 { get; set; }

        [StringLength(4)]
        public string IMA25 { get; set; }

        public decimal? IMA26 { get; set; }

        public decimal? IMA261 { get; set; }

        public decimal? IMA262 { get; set; }

        public decimal? IMA27 { get; set; }

        public decimal? IMA271 { get; set; }

        public decimal? IMA28 { get; set; }

        public DateTime? IMA29 { get; set; }

        public DateTime? IMA30 { get; set; }

        [StringLength(4)]
        public string IMA31 { get; set; }

        public decimal? IMA31_FAC { get; set; }

        public decimal? IMA32 { get; set; }

        public decimal? IMA33 { get; set; }

        [StringLength(10)]
        public string IMA34 { get; set; }

        [StringLength(10)]
        public string IMA35 { get; set; }

        [StringLength(10)]
        public string IMA36 { get; set; }

        [StringLength(1)]
        public string IMA37 { get; set; }

        public decimal? IMA38 { get; set; }

        [StringLength(24)]
        public string IMA39 { get; set; }

        public decimal? IMA40 { get; set; }

        public decimal? IMA41 { get; set; }

        [StringLength(1)]
        public string IMA42 { get; set; }

        [StringLength(10)]
        public string IMA43 { get; set; }

        [StringLength(4)]
        public string IMA44 { get; set; }

        public decimal? IMA44_FAC { get; set; }

        public decimal? IMA45 { get; set; }

        public decimal? IMA46 { get; set; }

        public decimal? IMA47 { get; set; }

        public decimal? IMA48 { get; set; }

        public decimal? IMA49 { get; set; }

        public decimal? IMA491 { get; set; }

        public decimal? IMA50 { get; set; }

        public decimal? IMA51 { get; set; }

        public decimal? IMA52 { get; set; }

        public decimal? IMA53 { get; set; }

        public decimal? IMA531 { get; set; }

        public DateTime? IMA532 { get; set; }

        [StringLength(10)]
        public string IMA54 { get; set; }

        [StringLength(4)]
        public string IMA55 { get; set; }

        public decimal? IMA55_FAC { get; set; }

        public decimal? IMA56 { get; set; }

        public decimal? IMA561 { get; set; }

        public decimal? IMA562 { get; set; }

        public short? IMA57 { get; set; }

        [StringLength(40)]
        public string IMA571 { get; set; }

        public decimal? IMA58 { get; set; }

        public decimal? IMA59 { get; set; }

        public decimal? IMA60 { get; set; }

        public decimal? IMA61 { get; set; }

        public decimal? IMA62 { get; set; }

        [StringLength(4)]
        public string IMA63 { get; set; }

        public decimal? IMA63_FAC { get; set; }

        public decimal? IMA64 { get; set; }

        public decimal? IMA641 { get; set; }

        public decimal? IMA65 { get; set; }

        public decimal? IMA66 { get; set; }

        [StringLength(10)]
        public string IMA67 { get; set; }

        public decimal? IMA68 { get; set; }

        public decimal? IMA69 { get; set; }

        [StringLength(1)]
        public string IMA70 { get; set; }

        public short? IMA71 { get; set; }

        public decimal? IMA72 { get; set; }

        public DateTime? IMA73 { get; set; }

        public DateTime? IMA74 { get; set; }

        [StringLength(4)]
        public string IMA86 { get; set; }

        public decimal? IMA86_FAC { get; set; }

        [StringLength(10)]
        public string IMA87 { get; set; }

        public decimal? IMA871 { get; set; }

        [StringLength(10)]
        public string IMA872 { get; set; }

        public decimal? IMA873 { get; set; }

        [StringLength(10)]
        public string IMA874 { get; set; }

        public decimal? IMA88 { get; set; }

        public DateTime? IMA881 { get; set; }

        public short? IMA89 { get; set; }

        public short? IMA90 { get; set; }

        public decimal? IMA91 { get; set; }

        [StringLength(1)]
        public string IMA92 { get; set; }

        [StringLength(8)]
        public string IMA93 { get; set; }

        [StringLength(10)]
        public string IMA94 { get; set; }

        public decimal? IMA95 { get; set; }

        [StringLength(15)]
        public string IMA75 { get; set; }

        [StringLength(10)]
        public string IMA76 { get; set; }

        public decimal? IMA77 { get; set; }

        public short? IMA78 { get; set; }

        public short? IMA79 { get; set; }

        public short? IMA80 { get; set; }

        public short? IMA81 { get; set; }

        public short? IMA82 { get; set; }

        public short? IMA83 { get; set; }

        public short? IMA84 { get; set; }

        public short? IMA85 { get; set; }

        [StringLength(10)]
        public string IMA851 { get; set; }

        [StringLength(1)]
        public string IMA852 { get; set; }

        [StringLength(1)]
        public string IMA853 { get; set; }

        public short? IMA96 { get; set; }

        public short? IMA97 { get; set; }

        public decimal? IMA98 { get; set; }

        public decimal? IMA99 { get; set; }

        [StringLength(1)]
        public string IMA100 { get; set; }

        [StringLength(1)]
        public string IMA101 { get; set; }

        [StringLength(1)]
        public string IMA102 { get; set; }

        [StringLength(1)]
        public string IMA103 { get; set; }

        public decimal? IMA104 { get; set; }

        [StringLength(1)]
        public string IMA105 { get; set; }

        [StringLength(1)]
        public string IMA106 { get; set; }

        [StringLength(1)]
        public string IMA107 { get; set; }

        [StringLength(1)]
        public string IMA108 { get; set; }

        [StringLength(10)]
        public string IMA109 { get; set; }

        [StringLength(1)]
        public string IMA110 { get; set; }

        [StringLength(5)]
        public string IMA111 { get; set; }

        public decimal? IMA121 { get; set; }

        public decimal? IMA122 { get; set; }

        public decimal? IMA123 { get; set; }

        public decimal? IMA124 { get; set; }

        public decimal? IMA125 { get; set; }

        public decimal? IMA126 { get; set; }

        public decimal? IMA127 { get; set; }

        public decimal? IMA128 { get; set; }

        public decimal? IMA129 { get; set; }

        [StringLength(1)]
        public string IMA130 { get; set; }

        [StringLength(10)]
        public string IMA131 { get; set; }

        [StringLength(24)]
        public string IMA132 { get; set; }

        [StringLength(40)]
        public string IMA133 { get; set; }

        [StringLength(15)]
        public string IMA134 { get; set; }

        [StringLength(40)]
        public string IMA135 { get; set; }

        [StringLength(10)]
        public string IMA136 { get; set; }

        [StringLength(10)]
        public string IMA137 { get; set; }

        [StringLength(40)]
        public string IMA138 { get; set; }

        [StringLength(1)]
        public string IMA139 { get; set; }

        [StringLength(1)]
        public string IMA140 { get; set; }

        [StringLength(1)]
        public string IMA141 { get; set; }

        public short? IMA142 { get; set; }

        public short? IMA143 { get; set; }

        [StringLength(10)]
        public string IMA144 { get; set; }

        [StringLength(1)]
        public string IMA145 { get; set; }

        [StringLength(1)]
        public string IMA146 { get; set; }

        [StringLength(1)]
        public string IMA147 { get; set; }

        public short? IMA148 { get; set; }

        public DateTime? IMA901 { get; set; }

        public DateTime? IMA902 { get; set; }

        [StringLength(1)]
        public string IMA903 { get; set; }

        [StringLength(1)]
        public string IMA904 { get; set; }

        [StringLength(1)]
        public string IMA905 { get; set; }

        [StringLength(1)]
        public string IMA906 { get; set; }

        [StringLength(4)]
        public string IMA907 { get; set; }

        [StringLength(4)]
        public string IMA908 { get; set; }

        public short? IMA909 { get; set; }

        [Required]
        [StringLength(20)]
        public string IMA910 { get; set; }

        [StringLength(1)]
        public string IMAACTI { get; set; }

        [StringLength(10)]
        public string IMAUSER { get; set; }

        [StringLength(10)]
        public string IMAGRUP { get; set; }

        [StringLength(10)]
        public string IMAMODU { get; set; }

        public DateTime? IMADATE { get; set; }

        [StringLength(10)]
        public string IMAAG { get; set; }

        [StringLength(10)]
        public string IMAAG1 { get; set; }

        [StringLength(255)]
        public string IMAUD01 { get; set; }

        [StringLength(40)]
        public string IMAUD02 { get; set; }

        [StringLength(255)]
        public string IMAUD03 { get; set; }

        [StringLength(255)]
        public string IMAUD04 { get; set; }

        [StringLength(255)]
        public string IMAUD05 { get; set; }

        [StringLength(255)]
        public string IMAUD06 { get; set; }

        public decimal? IMAUD07 { get; set; }

        public decimal? IMAUD08 { get; set; }

        public decimal? IMAUD09 { get; set; }

        public int? IMAUD10 { get; set; }

        public int? IMAUD11 { get; set; }

        public int? IMAUD12 { get; set; }

        public DateTime? IMAUD13 { get; set; }

        public DateTime? IMAUD14 { get; set; }

        public DateTime? IMAUD15 { get; set; }

        [StringLength(100)]
        public string IMA1001 { get; set; }

        [StringLength(1000)]
        public string IMA1002 { get; set; }

        [StringLength(20)]
        public string IMA1003 { get; set; }

        [StringLength(10)]
        public string IMA1004 { get; set; }

        [StringLength(10)]
        public string IMA1005 { get; set; }

        [StringLength(10)]
        public string IMA1006 { get; set; }

        [StringLength(10)]
        public string IMA1007 { get; set; }

        [StringLength(10)]
        public string IMA1008 { get; set; }

        [StringLength(10)]
        public string IMA1009 { get; set; }

        [StringLength(1)]
        public string IMA1010 { get; set; }

        public decimal? IMA1011 { get; set; }

        public DateTime? IMA1012 { get; set; }

        public DateTime? IMA1013 { get; set; }

        [StringLength(1)]
        public string IMA1014 { get; set; }

        public DateTime? IMA1015 { get; set; }

        [StringLength(40)]
        public string IMA1016 { get; set; }

        public decimal? IMA1017 { get; set; }

        public decimal? IMA1018 { get; set; }

        public decimal? IMA1019 { get; set; }

        public decimal? IMA1020 { get; set; }

        public decimal? IMA1021 { get; set; }

        public decimal? IMA1022 { get; set; }

        public decimal? IMA1023 { get; set; }

        public decimal? IMA1024 { get; set; }

        public decimal? IMA1025 { get; set; }

        public decimal? IMA1026 { get; set; }

        public decimal? IMA1027 { get; set; }

        public decimal? IMA1028 { get; set; }

        [StringLength(1)]
        public string IMA1029 { get; set; }

        [StringLength(1)]
        public string IMA911 { get; set; }

        public decimal? IMA912 { get; set; }

        [StringLength(1)]
        public string IMA913 { get; set; }

        [StringLength(10)]
        public string IMA914 { get; set; }

        [StringLength(24)]
        public string IMA391 { get; set; }

        [StringLength(24)]
        public string IMA1321 { get; set; }

        [StringLength(1)]
        public string IMA1911 { get; set; }

        [StringLength(40)]
        public string IMA1912 { get; set; }

        public decimal? IMA1913 { get; set; }

        [StringLength(40)]
        public string IMA1914 { get; set; }

        [StringLength(40)]
        public string IMA1915 { get; set; }

        [StringLength(40)]
        public string IMA1916 { get; set; }

        [StringLength(1)]
        public string IMA1919 { get; set; }

        public DateTime? IMA1401 { get; set; }

        [Required]
        [StringLength(1)]
        public string IMA915 { get; set; }

        [Required]
        [StringLength(10)]
        public string IMA916 { get; set; }

        public int? IMA917 { get; set; }

        [Required]
        [StringLength(1)]
        public string IMA150 { get; set; }

        [Required]
        [StringLength(1)]
        public string IMA151 { get; set; }

        [Required]
        [StringLength(1)]
        public string IMA152 { get; set; }

        [StringLength(1)]
        public string IMA918 { get; set; }

        [StringLength(1)]
        public string IMA919 { get; set; }

        [StringLength(10)]
        public string IMA920 { get; set; }

        [StringLength(1)]
        public string IMA921 { get; set; }

        [StringLength(1)]
        public string IMA922 { get; set; }

        [StringLength(10)]
        public string IMA923 { get; set; }

        [StringLength(1)]
        public string IMA924 { get; set; }

        [StringLength(1)]
        public string IMA925 { get; set; }

        public decimal? IMA601 { get; set; }

        public decimal? IMA153 { get; set; }

        [Required]
        [StringLength(1)]
        public string IMA926 { get; set; }

        [StringLength(1)]
        public string IMA154 { get; set; }

        [StringLength(1)]
        public string IMA155 { get; set; }

        [StringLength(24)]
        public string IMA149 { get; set; }

        [StringLength(24)]
        public string IMA1491 { get; set; }

        [StringLength(10)]
        public string IMAORIU { get; set; }

        [StringLength(10)]
        public string IMAORIG { get; set; }

        public DateTime? IMA9021 { get; set; }

        public decimal IMA022 { get; set; }

        [StringLength(4)]
        public string IMA251 { get; set; }

        [StringLength(10)]
        public string IMA940 { get; set; }

        [StringLength(10)]
        public string IMA941 { get; set; }

        [Required]
        [StringLength(1)]
        public string IMA156 { get; set; }

        [StringLength(10)]
        public string IMA157 { get; set; }

        [Required]
        [StringLength(1)]
        public string IMA158 { get; set; }

        [Required]
        [StringLength(1)]
        public string IMA927 { get; set; }

        [Required]
        [StringLength(1)]
        public string IMA120 { get; set; }

        [Required]
        [StringLength(1)]
        public string IMA159 { get; set; }

        [StringLength(1)]
        public string IMA930 { get; set; }

        [StringLength(1)]
        public string IMA931 { get; set; }

        [StringLength(1)]
        public string IMA932 { get; set; }

        [StringLength(10)]
        public string IMA933 { get; set; }

        [Required]
        [StringLength(1)]
        public string IMA934 { get; set; }
    }
}
