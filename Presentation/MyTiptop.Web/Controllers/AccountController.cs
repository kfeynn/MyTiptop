using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Text;

using MyTiptop.Web.Models;
using MyTiptop.Web.Framework;
using MyTiptop.Core;
using MyTiptop.Data;
using MyTiptop.Services;

namespace MyTiptop.Web.Controllers
{
    
    public class AccountController : BaseWebController
    {
        //private ApplicationSignInManager _signInManager;
        //private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        /// <summary>
        /// 退出
        /// </summary>
        public ActionResult Logout()
        {
            if (WorkContext.Uid > 0)
            {
                WebHelper.DeleteCookie("mytiptop");
                Sessions.RemoverSession(WorkContext.Sid);
                //取消用户登录状态
                //OnlineUsers.DeleteOnlineUserBySid(WorkContext.Sid);
            }
            //return Redirect("../home/index");
            //string returnUrl = WorkContext.SubPath + "/";  //默认页面
            //return Redirect(WorkContext.SubPath + "/"); //默认页面   .  
            return Redirect(Request.ApplicationPath);
        }

        /// <summary>
        /// 登录
        /// </summary>
        public ActionResult Login()
        {

            string returnUrl = WebHelper.GetQueryString("returnUrl");
            if (returnUrl.Length == 0)
            {
                //returnUrl = WorkContext.SubPath + "/malladmin/home/default";  //默认去后台页面
                string subpath = Request.ApplicationPath;
                if (subpath.Equals("/"))
                    subpath = "";
                returnUrl = subpath + "/malladmin/home/default";  //默认去后台页面 
            }
            if (WorkContext.MallConfig.LoginType == "")
                return PromptView(returnUrl, "系统目前已经关闭登录功能!");
            if (WorkContext.Uid > 0)
                return PromptView(returnUrl, "您已经登录，无须重复登录!");

            //get请求
            if (WebHelper.IsGet())
            {
                LoginViewModel model = new LoginViewModel();

                model.ReturnUrl = returnUrl;
                model.ShadowName = WorkContext.MallConfig.ShadowName;
                model.IsRemember = WorkContext.MallConfig.IsRemember == 1;
                model.IsVerifyCode = CommonHelper.IsInArray(WorkContext.PageKey, WorkContext.MallConfig.VerifyPages);
                //model.OAuthPluginList = Plugins.GetOAuthPluginList();

                model.Random = Randoms.GetRandomInt(0, 5);

                return View(model);
            }

            //ajax请求
            string accountName = WebHelper.GetFormString("shadowName");  //WebHelper.GetFormString(WorkContext.MallConfig.ShadowName);
            string password = WebHelper.GetFormString("password");
            string verifyCode = WebHelper.GetFormString("verifyCode");
            int isRemember = WebHelper.GetFormInt("isRemember");

            StringBuilder errorList = new StringBuilder("[");
            //验证账户名
            if (string.IsNullOrWhiteSpace(accountName))
            {
                errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "accountName", "账户名不能为空", "}");
            }
            else if (accountName.Length < 4 || accountName.Length > 50)
            {
                errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "accountName", "账户名必须大于3且不大于50个字符", "}");
            }
            else if ((!SecureHelper.IsSafeSqlString(accountName, false)))
            {
                errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "accountName", "账户名不存在", "}");
            }

            //验证密码
            if (string.IsNullOrWhiteSpace(password))
            {
                errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "password", "密码不能为空", "}");
            }
            else if (password.Length < 4 || password.Length > 32)
            {
                errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "password", "密码必须大于3且不大于32个字符", "}");
            }

            //验证验证码
            if (CommonHelper.IsInArray(WorkContext.PageKey, WorkContext.MallConfig.VerifyPages))
            {
                if (string.IsNullOrWhiteSpace(verifyCode))
                {
                    errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "verifyCode", "验证码不能为空", "}");
                }
                else if (verifyCode.ToLower() != Sessions.GetValueString(WorkContext.Sid, "verifyCode"))
                {
                    errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "verifyCode", "验证码不正确", "}");
                }
            }

            //当以上验证全部通过时 xpGrid_User PartUserInfo
            xpGrid_User partUserInfo = null;
            if (errorList.Length == 1)
            {
                //用户名登录
                if (!BMAConfig.MallConfig.LoginType.Contains("1"))
                {
                    errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "accountName", "不能使用用户名登录", "}");
                }
                else
                {
                    partUserInfo = Users.GetUserByName(accountName);
                    if (partUserInfo == null)
                        errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "accountName", "用户名不存在", "}");
                }
                if (partUserInfo != null)
                {
                    if (password != partUserInfo.Password)//判断密码是否正确
                    {
                        errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "password", "密码不正确", "}");
                    }
                    else if (partUserInfo.deleted == 1)//当用户等级是禁止访问等级时
                    {
                        errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "accountName", "您的账号当前被锁定,不能访问", "}");
                    }
                }
            }
            if (errorList.Length > 1)//验证失败时
            {
                return AjaxResult("error", errorList.Remove(errorList.Length - 1, 1).Append("]").ToString(), true);
            }
            else//验证成功时
            {
                //将用户信息写入cookie中
                MallUtils.SetUserCookie(partUserInfo, (WorkContext.MallConfig.IsRemember == 1 && isRemember == 1) ? 30 : -1);


                //return Redirect(returnUrl); //登录成功，直接转向
                return AjaxResult("success", returnUrl);
            }
        }





    }
}