namespace MyTiptop.OraCore
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    //[Table("S10.TC_IME_FILE")]
    public partial class TC_IME_FILE
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string TC_IME01 { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string TC_IME02 { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(10)]
        public string TC_IME03 { get; set; }
    }
}
