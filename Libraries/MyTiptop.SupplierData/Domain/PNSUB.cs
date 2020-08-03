namespace MyTiptop.SupplierData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SYSTEM.PNSUB")]
    public partial class PNSUB
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string PMM01 { get; set; }

        public int PMN02 { get; set; }

        [Required]
        [StringLength(50)]
        public string PMN04 { get; set; }

        [Required]
        [StringLength(255)]
        public string PMN041 { get; set; }

        [StringLength(512)]
        public string IMA021 { get; set; }

        [Required]
        [StringLength(50)]
        public string PMN07 { get; set; }

        [Column(TypeName = "float")]
        public decimal PMN20 { get; set; }

        [Required]
        [StringLength(50)]
        public string PMN86 { get; set; }

        [Column(TypeName = "float")]
        public decimal PMN87 { get; set; }

        public int STATUS { get; set; }

        [StringLength(50)]
        public string SDNNUM { get; set; }


    }
}
