using System;
using System.Data;
using System.Web.Mvc;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using MyTiptop.OraCore;
using MyTiptop.Web.Framework;

namespace MyTiptop.Web.MallAdmin.Models
{

    public class CheckOutListViewModel
    {
        /// <summary>
        /// check list 表头信息
        /// </summary>
        public TC_QCYY_FILE qcy { get; set; }
        /// <summary> 
        /// 下拉列表
        /// </summary> 
        public List<TC_QCZZ_FILE> qczlist { get; set; }
        /// <summary>
        /// 提示信息
        /// </summary>
        public string message { get; set; }

    }


    public class DCheckOutListViewModel
    {
        /// <summary>
        /// check list 表头信息
        /// </summary>
        public TC_QCYY_FILE qcy { get; set; }
        /// <summary> 
        /// 下拉列表
        /// </summary> 
        public List<TC_QCZZ_FILE> qczlist { get; set; }

        /// <summary>
        /// 提示信息
        /// </summary>
        public string message { get; set; }

    }



    public class QcyyOutListViewModel
    {
        /// <summary>
        /// 分页对象
        /// </summary>
        public PageModel PageModel { get; set; }
        /// <summary>
        /// check list 表头信息
        /// </summary>
        public TC_QCYY_FILE qcy { get; set; }
        /// <summary> 
        /// 下拉列表
        /// </summary> 
        public List<TC_QCYY_FILE> qcylist { get; set; }
        /// <summary>
        /// 提示信息
        /// </summary>
        public string message { get; set; }

    }


}
