namespace MyTiptop.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class QA_Complaint_annex
    {
        public int id { get; set; }

        public int? data01 { get; set; }

        [StringLength(100)]
        public string data02 { get; set; }

        [StringLength(500)]
        public string data03 { get; set; }

        [StringLength(500)]
        public string data04 { get; set; }

        public int? data05 { get; set; }

        public int? data06 { get; set; }
    }
}
