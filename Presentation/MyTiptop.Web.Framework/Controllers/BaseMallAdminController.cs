using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MyTiptop.Core;
using MyTiptop.Services;
using MyTiptop.Data;
using System.Configuration;

namespace MyTiptop.Web.Framework
{
    /// <summary>
    /// PC后台基础控制器类
    /// </summary>
    public class BaseMallAdminController : BaseController
    {
        //工作上下文
        public MallAdminWorkContext WorkContext = new MallAdminWorkContext();

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            WorkContext.IsHttpAjax = WebHelper.IsAjax();
            WorkContext.IP = WebHelper.GetIP();
            WorkContext.Url = WebHelper.GetUrl();
            WorkContext.UrlReferrer = WebHelper.GetUrlReferrer();

            //获得用户唯一标示符sid
            WorkContext.Sid = MallUtils.GetSidCookie();
            if (WorkContext.Sid.Length == 0)
            {
                //生成sid
                WorkContext.Sid = Sessions.GenerateSid();
                //将sid保存到cookie中
                MallUtils.SetSidCookie(WorkContext.Sid);
            }
            //PartUserInfo partUserInfo;
            xpGrid_User user;
            //获得用户id
            int uid = MallUtils.GetUidCookie();
            if (uid < 1)//当用户为游客时
            {
                //创建游客
                user = Users.CreatePartGuest();
            }
            else//当用户为会员时
            {
                //获得保存在cookie中的密码
                string encryptPwd = MallUtils.GetCookiePassword();
                //防止用户密码被篡改为危险字符
                if (encryptPwd.Length == 0 || !SecureHelper.IsBase64String(encryptPwd))
                {
                    //创建游客
                    user = Users.CreatePartGuest();
                    encryptPwd = string.Empty;
                    MallUtils.SetUidCookie(-1);
                    MallUtils.SetCookiePassword("");
                }
                else
                {
                    user = Users.GetPartUserByUidAndPwd(uid, MallUtils.DecryptCookiePassword(encryptPwd));
                    if (user != null){}
                    else//当会员的账号或密码不正确时，将用户置为游客
                    {
                        user = Users.CreatePartGuest();
                        encryptPwd = string.Empty;
                        MallUtils.SetUidCookie(-1);
                        MallUtils.SetCookiePassword("");
                    }
                }
                WorkContext.EncryptPwd = encryptPwd;
            }

            //当用户被禁止访问时重置用户为游客
            if (user.deleted == 1)
            {
                user = Users.CreatePartGuest();
                WorkContext.EncryptPwd = string.Empty;
                MallUtils.SetUidCookie(-1);
                MallUtils.SetCookiePassword("");
            }
            WorkContext.User = user;
            WorkContext.Uid = user.UserID;
            WorkContext.UserName = user.UserName;
            WorkContext.Password = user.Password;
            WorkContext.NickName = user.UserCName;
            ////设置当前控制器类名
            WorkContext.Controller = RouteData.Values["controller"].ToString().ToLower();
            //设置当前动作方法名
            WorkContext.Action = RouteData.Values["action"].ToString().ToLower();
            WorkContext.PageKey = string.Format("/{0}/{1}", WorkContext.Controller, WorkContext.Action);
            //设置图片cdn
            WorkContext.ImageCDN = WorkContext.MallConfig.ImageCDN;
            //设置csscdn
            WorkContext.CSSCDN = WorkContext.MallConfig.CSSCDN;
            //设置脚本cdn
            WorkContext.ScriptCDN = WorkContext.MallConfig.ScriptCDN;

            WorkContext.Dbs = WorkContext.MallConfig.Dbs;

            WorkContext.Plant = WorkContext.MallConfig.Plant;

            WorkContext.Legal = WorkContext.MallConfig.Legal;

            string subpath = "";
            subpath = ConfigurationManager.AppSettings["SubPath"].ToString();
            if (subpath.Length > 0)
            {
                subpath = "/" + subpath;
            }
            WorkContext.SubPath = subpath;
        }


        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            string returnUrl = WorkContext.SubPath + "/"; //返回路径

            //不能应用在子方法上
            if (filterContext.IsChildAction)
                return;

            //当用户ip不在允许的后台访问ip列表时
            if (!string.IsNullOrEmpty(WorkContext.MallConfig.AdminAllowAccessIP) && !ValidateHelper.InIPList(WorkContext.IP, WorkContext.MallConfig.AdminAllowAccessIP))
            {
                if (WorkContext.IsHttpAjax)
                    filterContext.Result = AjaxResult("404", "您访问的网址不存在");
                else
                {
                    //filterContext.Result = new RedirectResult("/");
                    filterContext.Result = new RedirectResult(returnUrl);
                }
                return;
            }

            #region 当用户IP被禁止时
            //if (BannedIPs.CheckIP(WorkContext.IP))
            //{
            //    if (WorkContext.IsHttpAjax)
            //        filterContext.Result = AjaxResult("404", "您访问的网址不存在");
            //    else
            //        filterContext.Result = new RedirectResult("/");
            //    return;
            //}
            #endregion

            //如果当前用户没有登录
            if (WorkContext.Uid < 1)
            {
                if (WorkContext.IsHttpAjax)
                    filterContext.Result = AjaxResult("404", "您访问的网址不存在");
                else
                    //filterContext.Result = new RedirectResult("/");
                    filterContext.Result = new RedirectResult(returnUrl);
                return;
            }

            //判断当前用户是否有访问当前页面的权限  ,很好的功能 (暂时不启用，内部没必要防止盗链，减少一次数据库访问)

            //string controller = WorkContext.Controller;
            //string action = WorkContext.Action;
            //if (WorkContext.Controller != "home")
            //{
            //    string authorityPath = controller + "/" + action;
            //    //验证权限
            //    if (!BMAData.RDBS.UserAuthorizationCheck(WorkContext.Uid, authorityPath)) 
            //    {
            //        if (WorkContext.IsHttpAjax)
            //            filterContext.Result = AjaxResult("nopermit", "您没有当前操作的权限");
            //        else
            //            filterContext.Result =  PromptView("您没有当前操作的权限！");  // 提示器显示不完全？
            //        return;
            //    }
            //}

        }

        /// <summary>
        /// 提示信息视图
        /// </summary>
        /// <param name="message">提示信息</param>
        /// <returns></returns>
        protected ViewResult PromptView(string message)
        {
            return View("prompt", new PromptModel(MallUtils.GetMallAdminRefererCookie(), message));
        }


    }
}
