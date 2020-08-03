using System;
using System.Data;
using System.Web.Mvc;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MyTiptop.Core;
using MyTiptop.Web.Framework;

namespace MyTiptop.Web.Models
{




    public class EquipmentViewModel
    {

        /// <summary>
        /// 分页对象
        /// </summary>
        public PageModel PageModel { get; set; }
        /// <summary>
        /// 列表
        /// </summary>
        public List<Equipment> List { get; set; }
 


    }




}
