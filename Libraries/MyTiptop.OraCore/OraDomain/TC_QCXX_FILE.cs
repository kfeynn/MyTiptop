namespace MyTiptop.OraCore
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("S30.TC_QCXX_FILE")]
    public partial class TC_QCXX_FILE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TC_QCXX01 { get; set; }

        [Required]
        [StringLength(600)]
        public string TC_QCXX02 { get; set; }

        public int TC_QCXX03 { get; set; }

        public int? TC_QCXX04 { get; set; }

        public int? TC_QCXX05 { get; set; }

        [StringLength(60)]
        public string TC_QCXX06 { get; set; }

        [StringLength(60)]
        public string TC_QCXX07 { get; set; }

        public int? TC_QCXX08 { get; set; }

        [StringLength(60)]
        public string TC_QCXX09 { get; set; }

        public int? TC_QCXX10 { get; set; }

        public int? TC_QCXX11 { get; set; }

        public decimal? TC_QCXX12 { get; set; }

        public decimal? TC_QCXX13 { get; set; }

        [StringLength(60)]
        public string TC_QCXX14 { get; set; }

        [StringLength(60)]
        public string TC_QCXX15 { get; set; }
    }
}
