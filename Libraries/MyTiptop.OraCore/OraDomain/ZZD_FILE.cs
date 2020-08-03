namespace MyTiptop.OraCore
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    //[Table("S30.ZZD_FILE")]
    public partial class ZZD_FILE
    {
        [Key]
        [StringLength(40)]
        public string ZZD01 { get; set; }

        [StringLength(40)]
        public string ZZD02 { get; set; }

        [StringLength(1)]
        public string ZZDACTI { get; set; }
    }
}
