namespace MyTiptop.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Equipment")]
    public partial class Equipment
    {
        public int id { get; set; }

        [StringLength(50)]
        public string Ecode { get; set; }

        public int? SortID { get; set; }

        [StringLength(50)]
        public string IP { get; set; }

        [StringLength(50)]
        public string Place { get; set; }

        public int? DeptID { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }

        public int StatusID { get; set; }

        [StringLength(50)]
        public string Brand { get; set; }

        [StringLength(50)]
        public string Version { get; set; }

        public int? InputUserID { get; set; }

        public DateTime? UpdateTime { get; set; }

        [StringLength(50)]
        public string Remark { get; set; }

        public virtual Base_Status Base_Status { get; set; }
    }
}
