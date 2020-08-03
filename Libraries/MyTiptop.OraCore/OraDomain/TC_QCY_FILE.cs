namespace MyTiptop.OraCore
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("S30.TC_QCY_FILE")]
    public partial class TC_QCY_FILE
    {
        [Key]
        [StringLength(60)]
        public string TC_QCY01 { get; set; }

        [Required]
        [StringLength(120)]
        public string TC_QCY02 { get; set; }

        [Required]
        [StringLength(120)]
        public string TC_QCY03 { get; set; }

        [Required]
        [StringLength(60)]
        public string TC_QCY04 { get; set; }

        [StringLength(120)]
        public string TC_QCY05 { get; set; }

        [StringLength(60)]
        public string TC_QCY06 { get; set; }

        [StringLength(60)]
        public string TC_QCY07 { get; set; }

        public DateTime? TC_QCY08 { get; set; }

        [StringLength(60)]
        public string TC_QCY09 { get; set; }

        public DateTime? TC_QCY10 { get; set; }

        [StringLength(60)]
        public string TC_QCY11 { get; set; }

        public int? TC_QCY12 { get; set; }

        [StringLength(60)]
        public string TC_QCY13 { get; set; }

        public DateTime? TC_QCY14 { get; set; }

        public int? TC_QCY15 { get; set; }

        public int? TC_QCY16 { get; set; }

        [StringLength(60)]
        public string TC_QCY17 { get; set; }

        [StringLength(60)]
        public string TC_QCY18 { get; set; }
    }
}
