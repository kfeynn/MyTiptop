namespace MyTiptop.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Log")]
    public partial class Log
    {
        [Key]
        public int id { get; set; }

        public DateTime? Date { get; set; }

        [StringLength(32)]
        public string Thread { get; set; }

        [StringLength(50)]
        public string ErrLevel { get; set; }

        [StringLength(50)]
        public string Logger { get; set; }

        [StringLength(500)]
        public string Message { get; set; }

        [StringLength(500)]
        public string Exception { get; set; }

        [StringLength(100)]
        public string Context { get; set; }
    }
}
