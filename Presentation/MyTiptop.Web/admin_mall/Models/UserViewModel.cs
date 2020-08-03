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
    /// 用户信息View视图
    /// </summary>
    public class UserViewModel
    {
        //用户信息
        public xpGrid_User User { get; set; }
        //禁用状态
        public bool isdeleted { get; set; }
    }

    /// <summary>
    /// 用户Add信息View视图
    /// </summary>
    public class UserAddViewModel
    {
        //用户信息
        public xpGrid_User User { get; set; }
        //连续添加标志
        public bool AddFlag { get; set; }
        //通知信息
        public string Message { get; set; }
    }
    /// <summary>
    /// 用户列表模型类
    /// </summary>
    public class UserListViewModel
    {
        /// <summary>
        /// 分页对象
        /// </summary>
        public PageModel PageModel { get; set; }
        /// <summary>
        /// 用户列表
        /// </summary>
        public List<xpGrid_User> UserList { get; set; }
        /// <summary>
        /// 用户名（帐号）
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 用户名(姓名)
        /// </summary>
        public string UserCName { get; set; }

    }
    /// <summary>
    /// 重置密码模型类
    /// </summary>
    public class ResetPwdViewModel
    {
        //通知信息
        public string Message { get; set; }
    }

    /// <summary>
    /// 用户授权
    /// </summary>
    public class UserAuthorizationViewModel
    {
        //用户
        public xpGrid_User user { get; set; }
        //角色列表 附带是否选中
        public DataTable RoleList { get; set; }
        //通知信息
        public string Message { get; set; }
    }

}
