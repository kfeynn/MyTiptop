using System; 
using System.Data; 
using System.Web.Mvc; 
using System.ComponentModel; 
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 
using MyTiptop.Core;
using MyTiptop.Web.Framework;
using MyTiptop.OraCore;

namespace MyTiptop.Web.MallAdmin.Models
{



    #region 类型表

    /// <summary>
    /// 列表视图
    /// </summary>
    public class BrdListViewModel
    {
        /// <summary>
        /// 分页对象
        /// </summary>
        public PageModel PageModel { get; set; }
        /// <summary>
        /// 列表
        /// </summary>
        public List<TC_BRD_FILE> List { get; set; }
        /// <summary>
        /// 名称(用于查询条件)
        /// </summary>
        //public string SortName { get; set; }
    }


    #endregion

    /// <summary>
    /// 列表视图
    /// </summary>
    public class RvvListViewModel
    {
        /// <summary>
        /// 分页对象
        /// </summary>
        public PageModel PageModel { get; set; }
        /// <summary>
        /// 列表
        /// </summary>
        public List<rvvList> List { get; set; }
        /// <summary>
        /// 名称(用于查询条件)
        /// </summary>
        //public string SortName { get; set; }
    }





    public class ClearBarCodeViewModel
    {
        /// <summary>
        /// 列表
        /// </summary>
        public DataTable List { get; set; }
        /// <summary>
        /// 分页对象
        /// </summary>
        public PageModel PageModel { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
    }


    public class ChangeBarCodeViewModel
    {
        /// <summary>
        /// 列表
        /// </summary>
        public DataTable List { get; set; }
        /// <summary>
        /// 分页对象
        /// </summary>
        public PageModel PageModel { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
    }



    /// <summary>
    /// 列表视图
    /// </summary>
    public class SfmListViewModel
    {
        /// <summary>
        /// 分页对象
        /// </summary>
        public PageModel PageModel { get; set; }
        /// <summary>
        /// 列表
        /// </summary>
        public List<TC_SFM_FILE> List { get; set; }
        /// <summary>
        /// 名称(用于查询条件)
        /// </summary>
        //public string SortName { get; set; }
    }


    public class MySfmReportViewModel
    {
        /// <summary>
        /// 报表
        /// </summary>
        public DataTable SFMInfo { get; set; }

        public DateTime? t { get; set; }

    }

}
