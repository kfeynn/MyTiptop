namespace MyTiptop.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class xpGrid_Role
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public xpGrid_Role()
        {
            xpGrid_FuncsInRoles = new HashSet<xpGrid_FuncsInRoles>();
            xpGrid_UsersInRoles = new HashSet<xpGrid_UsersInRoles>();
        }

        [Key]
        public int RoleId { get; set; }

        [Required]
        [StringLength(40)]
        public string RoleName { get; set; }

        [StringLength(255)]
        public string RoleDes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<xpGrid_FuncsInRoles> xpGrid_FuncsInRoles { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<xpGrid_UsersInRoles> xpGrid_UsersInRoles { get; set; }
    }
}
