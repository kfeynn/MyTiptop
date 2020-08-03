namespace MyTiptop.OraCore
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    //[Table("S10.TC_SFL_FILE")]
    public partial class TC_SFL_FILE
    {
        [Key]
        [StringLength(60)]
        public string TC_SFL01 { get; set; }

        [StringLength(80)]
        public string TC_SFL02 { get; set; }

        [Required]
        [StringLength(20)]
        public string TC_SFL03 { get; set; }

        [Required]
        [StringLength(20)]
        public string TC_SFL04 { get; set; }

        [StringLength(60)]
        public string TC_SFL05 { get; set; }

        [StringLength(60)]
        public string TC_SFL06 { get; set; }

        [StringLength(60)]
        public string TC_SFL07 { get; set; }

        [StringLength(60)]
        public string TC_SFL08 { get; set; }

        [StringLength(60)]
        public string TC_SFL09 { get; set; }

        [StringLength(10)]
        public string TC_SFLPLANT { get; set; }

        [StringLength(10)]
        public string TC_SFLLEGAL { get; set; }
    }
}
