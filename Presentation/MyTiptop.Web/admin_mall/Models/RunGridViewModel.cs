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
    /// <summary> 
    /// 
    /// </summary>
    public class RunGridViewModel 
    {

        //下拉列表
        public IEnumerable<SelectListItem> BaseRunGridViewID { get; set; }

        /// <summary>
        /// </summary> 
        //public List<Base_RunGridView> RunGridViewList { get; set; } 
        /// <summary> 
        /// 查询条件  
        /// </summary> 
        public List<Base_RunGridView_Condition> ConditionList { get; set; }


        public DataTable dt { get; set; }

        /// <summary>
        /// message
        /// </summary>
        public string msg { get; set; }

        /// <summary>
        /// 分页对象
        /// </summary>
        public PageModel PageModel { get; set; }

    }

    /// <summary> 
    /// 
    /// </summary>
    public class RunGridViewModel2
    {

        //下拉列表
        public IEnumerable<SelectListItem> BaseRunGridViewID { get; set; }

        /// <summary> 
        /// 查询条件  
        /// </summary> 
        public List<Base_RunGridView_Condition> ConditionList { get; set; }


        public DataTable dt { get; set; }

        /// <summary>
        /// message
        /// </summary>
        public string msg { get; set; }

        /// <summary>
        /// 分页对象
        /// </summary>
        public PageModel PageModel { get; set; }

        /// <summary>
        /// 将dt转化为json格式字符。传输到前台交给js处理图形用。
        /// </summary>
        public string dtJson { get; set; }

        public string dimensionJson { get; set; }

        public string barStrJson { get; set; }


        public string width { get; set; }   //显示控件的长度

        public string height { get; set; } //显示控件的高度

    }


    /// <summary> 
    /// 
    /// </summary>
    public class RunMapViewModel
    {

        //下拉列表
        public IEnumerable<SelectListItem> BaseRunGridViewID { get; set; }

        /// <summary> 
        /// 查询条件  
        /// </summary> 
        public List<Base_RunGridView_Condition> ConditionList { get; set; }


        public DataTable dt { get; set; }

        /// <summary>
        /// message
        /// </summary>
        public string msg { get; set; }

        /// <summary>
        /// 分页对象
        /// </summary>
        public PageModel PageModel { get; set; }

        /// <summary>
        /// 将dt转化为json格式字符。传输到前台交给js处理图形用。
        /// </summary>
        public string dtJson { get; set; }

        public string dimensionJson { get; set; }

        public string barStrJson { get; set; }


        public string width { get; set; }   //显示控件的长度

        public string height { get; set; } //显示控件的高度

    }
} 

