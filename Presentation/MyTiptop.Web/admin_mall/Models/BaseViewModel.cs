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
    public class SortListViewModel
    {
        /// <summary>
        /// 分页对象
        /// </summary>
        public PageModel PageModel { get; set; }
        /// <summary>
        /// 列表
        /// </summary>
        public List<Base_Sort> List { get; set; }
        /// <summary>
        /// 名称(用于查询条件)
        /// </summary>
        public string SortName { get; set; }
    }
    /// <summary>
    /// 添加视图
    /// </summary>
    public class SortAddViewModel
    {
        //作业程序信息
        public Base_Sort Single { get; set; }
        //连续添加标志
        public bool AddFlag { get; set; }
        //通知信息
        public string Message { get; set; }
    }
    /// <summary>
    /// 更新视图
    /// </summary>
    public class SortEditViewModel
    {
        //作业程序信息
        public Base_Sort Single { get; set; }
    }

    #endregion


    #region 部门表

    /// <summary>
    /// 列表视图
    /// </summary>
    public class DeptListViewModel
    {
        /// <summary>
        /// 分页对象
        /// </summary>
        public PageModel PageModel { get; set; }
        /// <summary>
        /// 列表
        /// </summary>
        public List<Base_Dept> List { get; set; }
        /// <summary>
        /// 名称(用于查询条件)
        /// </summary>
        public string DeptName { get; set; }
    }
    /// <summary>
    /// 添加视图
    /// </summary>
    public class DeptAddViewModel
    {
        //作业程序信息
        public Base_Dept Single { get; set; }
        //连续添加标志
        public bool AddFlag { get; set; }
        //通知信息
        public string Message { get; set; }
    }
    /// <summary>
    /// 更新视图
    /// </summary>
    public class DeptEditViewModel
    {
        //作业程序信息
        public Base_Dept Single { get; set; }
    }

    #endregion


    #region 状态表

    /// <summary>
    /// 列表视图
    /// </summary>
    public class StatusListViewModel
    {
        /// <summary>
        /// 分页对象
        /// </summary>
        public PageModel PageModel { get; set; }
        /// <summary>
        /// 列表
        /// </summary>
        public List<Base_Status> List { get; set; }
        /// <summary>
        /// 名称(用于查询条件)
        /// </summary>
        public string StatusName { get; set; }
    }
    /// <summary>
    /// 添加视图
    /// </summary>
    public class StatusAddViewModel
    {
        //作业程序信息
        public Base_Status Single { get; set; }
        //连续添加标志
        public bool AddFlag { get; set; }
        //通知信息
        public string Message { get; set; }
    }
    /// <summary>
    /// 更新视图
    /// </summary>
    public class StatusEditViewModel
    {
        //作业程序信息
        public Base_Status Single { get; set; }
    }

    #endregion



}
