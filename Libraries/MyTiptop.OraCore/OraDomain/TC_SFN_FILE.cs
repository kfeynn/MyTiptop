namespace MyTiptop.OraCore
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    //[Table("S10.TC_SFN_FILE")]
    public partial class TC_SFN_FILE
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(60)]
        public string TC_SFN01 { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short TC_SFN02 { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(60)]
        public string TC_SFN03 { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(60)]
        public string TC_SFN04 { get; set; }

        public decimal? TC_SFN05 { get; set; }

        [StringLength(60)]
        public string TC_SFN06 { get; set; }

        [StringLength(60)]
        public string TC_SFN07 { get; set; }

        [StringLength(60)]
        public string TC_SFN08 { get; set; }

        [StringLength(60)]
        public string TC_SFN09 { get; set; }

        [StringLength(10)]
        public string TC_SFNPLANT { get; set; }

        [StringLength(10)]
        public string TC_SFNLEGAL { get; set; }
    }
}
