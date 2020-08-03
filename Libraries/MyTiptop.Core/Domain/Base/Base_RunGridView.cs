namespace MyTiptop.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Base_RunGridView
    {
        [Key]
        public int BaseRunGridViewID { get; set; }

        [StringLength(100)]
        public string GridViewName { get; set; }

        public string strSelect { get; set; }

        [StringLength(50)]
        public string Type { get; set; }

        public int? ViewOrder { get; set; }

        [Required]
        [StringLength(50)]
        public string DBType { get; set; }

        [StringLength(50)]
        public string SqlType { get; set; }

        [StringLength(1000)]
        public string strCondition { get; set; }

        [StringLength(150)]
        public string keyWord { get; set; }
    }
}
