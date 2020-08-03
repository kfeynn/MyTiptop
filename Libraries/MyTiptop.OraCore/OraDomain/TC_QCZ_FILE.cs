namespace MyTiptop.OraCore
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("S30.TC_QCZ_FILE")]
    public partial class TC_QCZ_FILE
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(60)]
        public string TC_QCZ01 { get; set; }

        [Required]
        [StringLength(600)]
        public string TC_QCZ02 { get; set; }

        [StringLength(60)]
        public string TC_QCZ03 { get; set; }

        [StringLength(120)]
        public string TC_QCZ04 { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TC_QCZ05 { get; set; }

        public int TC_QCZ06 { get; set; }

        [StringLength(60)]
        public string TC_QCZ07 { get; set; }

        [StringLength(60)]
        public string TC_QCZ08 { get; set; }

        public int? TC_QCZ09 { get; set; }

        public int? TC_QCZ10 { get; set; }

        public decimal? TC_QCZ11 { get; set; }
    }
}
