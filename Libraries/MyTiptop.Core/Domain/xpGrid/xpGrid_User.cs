namespace MyTiptop.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class xpGrid_User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public xpGrid_User()
        {
            xpGrid_UsersInRoles = new HashSet<xpGrid_UsersInRoles>();
        }

        [Key]
        public int UserID { get; set; }

        [Required]
        [StringLength(20)]
        public string UserName { get; set; }

        [StringLength(20)]
        public string UserCName { get; set; }

        [StringLength(20)]
        public string Password { get; set; }

        public int deleted { get; set; }

        public int? Online { get; set; }

        public int? LastOnlineTime { get; set; }

        public int? AllOnlineTime { get; set; }

        public int? LoginTimes { get; set; }

        public DateTime? CurrentLoginDateTime { get; set; }

        public DateTime? LastOprtnDateTime { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<xpGrid_UsersInRoles> xpGrid_UsersInRoles { get; set; }
    }
}
