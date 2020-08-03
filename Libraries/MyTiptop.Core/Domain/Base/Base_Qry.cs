namespace MyTiptop.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Base_Qry
    {
        public int id { get; set; }

        [StringLength(50)]
        public string keyVal { get; set; }

        [StringLength(50)]
        public string keyText { get; set; }

        public int? viewOrder { get; set; }

        [StringLength(50)]
        public string viewType { get; set; }

        [StringLength(255)]
        public string Remark { get; set; }
    }
}
