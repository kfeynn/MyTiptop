namespace MyTiptop.OraCore
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    //[Table("S30.TC_SHB_FILE")]
    public partial class TC_SHB_FILE
    {
        [Key]
        [StringLength(20)]
        public string TC_SHB01 { get; set; }

        [StringLength(10)]
        public string TC_SHB02 { get; set; }

        public DateTime? TC_SHB03 { get; set; }

        [StringLength(10)]
        public string TC_SHB04 { get; set; }

        public DateTime? TC_SHB05 { get; set; }

        [StringLength(40)]
        public string TC_SHB06 { get; set; }

        [StringLength(40)]
        public string TC_SHB07 { get; set; }

        [StringLength(10)]
        public string TC_SHBPLANT { get; set; }

        [StringLength(10)]
        public string TC_SHBLEGAL { get; set; }
    }
}
