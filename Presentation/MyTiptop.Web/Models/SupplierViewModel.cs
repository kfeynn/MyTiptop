using System;
using System.Data;
using System.Web.Mvc;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MyTiptop.OraCore;
using MyTiptop.Web.Framework;
using MyTiptop.SupplierData;

namespace MyTiptop.Web.Models
{

    public class PnssListViewModel
    {
        /// <summary>
        /// 分页对象
        /// </summary>
        public PageModel PageModel { get; set; }
        /// <summary>
        /// check list 表头信息
        /// </summary>
        public TC_QCY_FILE qcy { get; set; }
        /// <summary> 
        /// 下拉列表
        /// </summary> 
        public List<TC_QCY_FILE> qcylist { get; set; }
        /// <summary>
        /// 提示信息
        /// </summary>
        public string message { get; set; }

    }



    public class PnListViewModel
    {
        /// <summary>
        /// 分页对象
        /// </summary>
        public PageModel PageModel { get; set; }

        public PN pn { get; set; }

        public PNSUB pnSub { get; set; }

        public List<PN> pnList { get; set; }

        /// <summary> 
        /// 下拉列表
        /// </summary> 
        public List<PNSUB> pnSubList { get; set; }
        /// <summary>
        /// 提示信息
        /// </summary>
        public string message { get; set; }

    }


}
