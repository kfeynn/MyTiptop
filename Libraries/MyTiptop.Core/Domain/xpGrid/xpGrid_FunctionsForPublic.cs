namespace MyTiptop.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class xpGrid_FunctionsForPublic
    {
        [Key]
        [StringLength(30)]
        public string FuncCode { get; set; }

        [StringLength(100)]
        public string FuncName { get; set; }

        [StringLength(200)]
        public string FuncUrl { get; set; }

        [StringLength(10)]
        public string FuncParent { get; set; }

        [StringLength(50)]
        public string FuncImg { get; set; }

        public int? Enable { get; set; }

        public int? DisplayOrder { get; set; }
    }
}
