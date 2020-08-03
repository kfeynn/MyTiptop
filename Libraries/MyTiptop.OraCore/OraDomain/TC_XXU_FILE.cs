namespace MyTiptop.OraCore
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    //[Table("S30.TC_XXU_FILE")]
    public partial class TC_XXU_FILE
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(1)]
        public string TC_XXU001 { get; set; }

        [StringLength(1)]
        public string TC_XXU002 { get; set; }

        [StringLength(1)]
        public string TC_XXU003 { get; set; }

        [StringLength(1)]
        public string TC_XXU004 { get; set; }

        [StringLength(1)]
        public string TC_XXU005 { get; set; }

        [StringLength(1)]
        public string TC_XXU006 { get; set; }

        [StringLength(1)]
        public string TC_XXU007 { get; set; }

        public short? TC_XXU008 { get; set; }

        [StringLength(120)]
        public string TC_XXU009 { get; set; }

        [StringLength(1)]
        public string TC_XXU010 { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(1)]
        public string TC_XXU011 { get; set; }
    }
}
