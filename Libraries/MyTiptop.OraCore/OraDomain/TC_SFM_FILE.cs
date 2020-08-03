namespace MyTiptop.OraCore
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    //[Table("S10.TC_SFM_FILE")]
    public partial class TC_SFM_FILE
    {
        [Key]
        [StringLength(60)]
        public string TC_SFM01 { get; set; }

        public DateTime TC_SFM02 { get; set; }

        public short TC_SFM03 { get; set; }

        [Required]
        [StringLength(20)]
        public string TC_SFM04 { get; set; }

        public DateTime TC_SFM05 { get; set; }

        [Required]
        [StringLength(60)]
        public string TC_SFM06 { get; set; }

        [StringLength(60)]
        public string TC_SFM07 { get; set; }

        [StringLength(60)]
        public string TC_SFM08 { get; set; }

        [StringLength(60)]
        public string TC_SFM09 { get; set; }

        [StringLength(10)]
        public string TC_SFMPLANT { get; set; }

        [StringLength(10)]
        public string TC_SFMLEGAL { get; set; }

        [StringLength(1)]
        public string TC_SFMCONF { get; set; }
    }
}
