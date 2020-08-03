namespace MyTiptop.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class QA_Complaint
    {
        public int id { get; set; }

        [StringLength(50)]
        public string data01 { get; set; }

        [StringLength(50)]
        public string data02 { get; set; }

        [StringLength(50)]
        public string data03 { get; set; }

        [StringLength(50)]
        public string data04 { get; set; }

        //[StringLength(50)]
        public string data05 { get; set; }

        public int? data06 { get; set; }

        public int? data07 { get; set; }

        public int? data08 { get; set; }

        public int? data09 { get; set; }

        public int? data10 { get; set; }

        public DateTime? data11 { get; set; }

        public DateTime? data12 { get; set; }

        [StringLength(50)]
        public string data13 { get; set; }

        [StringLength(50)]
        public string data14 { get; set; }

        [StringLength(50)]
        public string data15 { get; set; }
    }
}
