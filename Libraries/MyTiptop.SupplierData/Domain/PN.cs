namespace MyTiptop.SupplierData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SYSTEM.PN")]
    public partial class PN
    {

        [Key]
        [StringLength(50)]
        public string DNNUM { get; set; }

        [Required]
        [StringLength(20)]
        public string PMN33 { get; set; }

        [Required]
        [StringLength(20)]
        public string SUPID { get; set; }

        [Required]
        [StringLength(120)]
        public string NAME { get; set; }

        public DateTime CREATE_TIME { get; set; }

        [StringLength(20)]
        public string CHANGE_USER { get; set; }

        [StringLength(20)]
        public string CHANGE_TIME { get; set; }

        [Required]
        [StringLength(10)]
        public string PLANT { get; set; }

        public int STATUS { get; set; }


    }
}
