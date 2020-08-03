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

    public class RoleListViewModel
    {
        /// <summary>
        /// 分页对象
        /// </summary>
        public PageModel PageModel { get; set; }
        /// <summary>
        /// 角色列表
        /// </summary>
        public List<xpGrid_Role> Roles { get; set; }
        /// <summary>
        /// 用户名（帐号）
        /// </summary>
        public string RoleName { get; set; }
    }
    /// <summary>
    /// 角色Add信息View视图
    /// </summary>
    public class RoleAddViewModel
    {
        //用户信息
        public xpGrid_Role Role { get; set; }
        //连续添加标志
        public bool AddFlag { get; set; }
        //通知信息
        public string Message { get; set; }
    }

    public class RoleEditViewModel
    {
        public xpGrid_Role Role {get;set;}
    }

    public class FuncListViewModel
    {
        /// <summary>
        /// 分页对象
        /// </summary>
        public PageModel PageModel { get; set; }
        /// <summary>
        /// 功能列表
        /// </summary>
        public List<xpGrid_Functions> FuncList { get; set; }

        public string FuncCode { get; set; }

        public string FuncName { get; set; }

        public string FuncUrl { get; set; }

        public string FuncParent { get; set; }

    }

    public class FuncAddViewModel
    {
        //功能信息
        public xpGrid_Functions Func { get; set; }
        //是否连续添加
        public bool AddFlag { get; set; }
        //通知信息
        public string Message { get; set; }
    }

    public class FuncEditViewModel
    {
        //功能信息
        public xpGrid_Functions Func { get; set; }
       
    }
    /// <summary>
    /// 角色授权
    /// </summary>
    public class RoleAuthorizationViewModel
    {
        //角色
        public xpGrid_Role role { get; set; }

        //功能列表 附带是否选中
        public DataTable FuncList { get; set; }

        //通知信息
        public string Message { get; set; }
    }



}
