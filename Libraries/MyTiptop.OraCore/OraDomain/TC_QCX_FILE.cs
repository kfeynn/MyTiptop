namespace MyTiptop.OraCore
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("S30.TC_QCX_FILE")]
    public partial class TC_QCX_FILE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TC_QCX01 { get; set; }

        [Required]
        [StringLength(600)]
        public string TC_QCX02 { get; set; }

        public int TC_QCX03 { get; set; }

        public int? TC_QCX04 { get; set; }

        public int? TC_QCX05 { get; set; }

        [StringLength(60)]
        public string TC_QCX06 { get; set; }

        [StringLength(60)]
        public string TC_QCX07 { get; set; }
    }
}
