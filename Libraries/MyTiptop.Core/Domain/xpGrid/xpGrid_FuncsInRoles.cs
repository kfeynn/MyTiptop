namespace MyTiptop.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class xpGrid_FuncsInRoles
    {
        public int ID { get; set; }

        public int? RoleId { get; set; }

        [StringLength(30)]
        public string FuncCode { get; set; }

        public virtual xpGrid_Functions xpGrid_Functions { get; set; }

        public virtual xpGrid_Role xpGrid_Role { get; set; }
    }
}
