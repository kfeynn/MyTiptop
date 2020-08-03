namespace MyTiptop.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Base_RunGridView_Condition
    {
        public int id { get; set; }

        public int? type { get; set; }

        [StringLength(50)]
        public string field { get; set; }

        [StringLength(50)]
        public string fieldName { get; set; }

        [StringLength(50)]
        public string formName { get; set; }

        [StringLength(50)]
        public string iOperator { get; set; }

        [StringLength(50)]
        public string currOperator { get; set; }

        [StringLength(50)]
        public string currValue { get; set; }

        [StringLength(50)]
        public string datetype { get; set; }

        [StringLength(255)]
        public string editFormat { get; set; }

        [StringLength(200)]
        public string Remark { get; set; }

        public int? inputtcii { get; set; }

        public int? Base_RunGridView_Id { get; set; }

    }
}
