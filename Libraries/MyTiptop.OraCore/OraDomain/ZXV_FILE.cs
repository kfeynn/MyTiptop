namespace MyTiptop.OraCore
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    //[Table("S30.ZXV_FILE")]
    public partial class ZXV_FILE
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string ZXV01 { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(32)]
        public string ZXV02 { get; set; }

        public DateTime? ZXV03 { get; set; }

        public DateTime? ZXV04 { get; set; }

        [StringLength(10)]
        public string ZXV05 { get; set; }
    }
}
