using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 
namespace MyTiptop.MysqlData
{ 
    public partial class guest_card
    {
        public int card_id { get; set; }
               
        [StringLength(255)]
        public string card_no { get; set; }

        public int card_flag { get; set; }
    } 
} 
