namespace MyTiptop.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Base_Sort
    {
        public int id { get; set; }

        [StringLength(50)]
        public string SortName { get; set; }

        [StringLength(50)]
        public string Remark { get; set; }
    }
}
