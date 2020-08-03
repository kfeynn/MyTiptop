using System; 
using System.Data; 
using System.Web.Mvc; 
using System.ComponentModel; 
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 
using MyTiptop.Core;
using MyTiptop.Web.Framework; 
 
namespace MyTiptop.Web.MallAdmin.Models 
{



    #region 类型表

    /// <summary>
    /// 列表视图
    /// </summary>
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
        /// <summary>
        /// 名称(用于查询条件)
        /// </summary>
        public string SortName { get; set; }
    }


    #endregion

    /// <summary>
    /// 导入
    /// </summary>
    public class ImportEquipmentViewModel
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




}
