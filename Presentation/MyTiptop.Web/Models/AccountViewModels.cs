using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using MyTiptop.Web.Models;
using MyTiptop.Web.Framework;
using MyTiptop.Core;
using MyTiptop.Data;
using MyTiptop.Services;

namespace MyTiptop.Web.Models
{




    public class LoginViewModel 
    {
        /// <summary>
        /// 返回地址
        /// </summary>
        public string ReturnUrl { get; set; }
        /// <summary>
        /// 影子账号名
        /// </summary>
        public string ShadowName { get; set; }
        /// <summary>
        /// 是否允许记住用户
        /// </summary>
        public bool IsRemember { get; set; }
        /// <summary>
        /// 是否启用验证码
        /// </summary>
        public bool IsVerifyCode { get; set; }
        ///// <summary>
        ///// 开放授权插件
        ///// </summary>
        ////public List<PluginInfo> OAuthPluginList { get; set; }

        /// <summary>
        /// 1-5 随即数
        /// </summary>
        public int Random { get; set; }
    }

    /// <summary>
    /// 菜单View视图
    /// </summary>
    public class MenuViewModel
    {
        public List<xpGrid_FunctionsForPublic> Func { get; set; }

    }


}
