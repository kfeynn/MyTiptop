using System;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using System.Collections.Generic;

using MyTiptop.Core;
using MyTiptop.Services;
using MyTiptop.Data;
using System.Configuration;

namespace MyTiptop.Web.Framework
{
    /// <summary>
    /// PC前台基础控制器类
    /// </summary>
    public class BaseWebController : BaseController
    {
        //工作上下文
        public WebWorkContext WorkContext = new WebWorkContext();

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            this.ValidateRequest = false;

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

            ////获得用户id
            int uid = MallUtils.GetUidCookie();
            if (uid < 1)//当用户为游客时
            {
                //创建游客
                user =  Users.CreatePartGuest();
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
                    if (user != null)
                    {
                        //发放登录积分
                        //Credits.SendLoginCredits(ref partUserInfo, DateTime.Now);
                    }
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

            #region 弃用


            //设置用户等级
            //if (UserRanks.IsBanUserRank(partUserInfo.UserRid) && partUserInfo.LiftBanTime <= DateTime.Now)
            //{
            //UserRankInfo userRankInfo = UserRanks.GetUserRankByCredits(partUserInfo.PayCredits);
            //Users.UpdateUserRankByUid(partUserInfo.Uid, userRankInfo.UserRid);
            //    partUserInfo.UserRid = userRankInfo.UserRid;
            //}


            //WorkContext.UserEmail = user.Email;
            //WorkContext.UserMobile = partUserInfo.Mobile;

            //WorkContext.Avatar = user.Avatar;
            //WorkContext.PayCreditName = Credits.PayCreditName;
            //WorkContext.PayCreditCount = partUserInfo.PayCredits;
            //WorkContext.RankCreditName = Credits.RankCreditName;
            //WorkContext.RankCreditCount = partUserInfo.RankCredits;

            //WorkContext.UserRid = partUserInfo.UserRid;
            //WorkContext.UserRankInfo = UserRanks.GetUserRankById(partUserInfo.UserRid);
            //WorkContext.UserRTitle = WorkContext.UserRankInfo.Title;
            //设置用户商城管理员组
            //WorkContext.MallAGid = partUserInfo.MallAGid;
            //WorkContext.MallAdminGroupInfo = MallAdminGroups.GetMallAdminGroupById(partUserInfo.MallAGid);
            //WorkContext.MallAGTitle = WorkContext.MallAdminGroupInfo.Title;
            ////在线游客数
            //WorkContext.OnlineGuestCount = OnlineUsers.GetOnlineGuestCount();
            //在线会员数
            //WorkContext.OnlineMemberCount = WorkContext.OnlineUserCount - WorkContext.OnlineGuestCount;

            ////购物车中商品数量
            //WorkContext.CartProductCount = Carts.GetCartProductCountCookie();

            ////设置导航列表
            //WorkContext.NavList = Navs.GetNavList();
            ////设置友情链接列表
            //WorkContext.FriendLinkList = FriendLinks.GetFriendLinkList();
            ////设置帮助列表
            //WorkContext.HelpList = Helps.GetHelpList();

            #endregion

            //设置当前控制器类名
            WorkContext.Controller = RouteData.Values["controller"].ToString().ToLower();
            ////设置当前动作方法名
            WorkContext.Action = RouteData.Values["action"].ToString().ToLower();
            WorkContext.PageKey = string.Format("/{0}/{1}", WorkContext.Controller, WorkContext.Action);
            WorkContext.ImageCDN = WorkContext.MallConfig.ImageCDN;
            WorkContext.CSSCDN = WorkContext.MallConfig.CSSCDN;
            WorkContext.ScriptCDN = WorkContext.MallConfig.ScriptCDN;


            WorkContext.Dbs = WorkContext.MallConfig.Dbs;

            WorkContext.Plant = WorkContext.MallConfig.Plant;

            WorkContext.Legal = WorkContext.MallConfig.Legal;

            //string subpath = "";
            //subpath = ConfigurationManager.AppSettings["SubPath"].ToString();
            //if (subpath.Length > 0)
            //{
            //    subpath = "/" + subpath;
            //}
            //WorkContext.SubPath = subpath;

            //在线总人数
            WorkContext.OnlineUserCount = Users.GetOnlineUsersCount();
            //搜索词
            WorkContext.SearchWord = string.Empty;
        }

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            //不能应用在子方法上
            if (filterContext.IsChildAction)
                return;

            //系统已经关闭
            if (WorkContext.MallConfig.IsClosed == 1 && WorkContext.PageKey != "/account/login" && WorkContext.PageKey != "/account/logout")
            {
                filterContext.Result = PromptView(WorkContext.MallConfig.CloseReason);
                return;
            }

            //当前时间为禁止访问时间
            if (ValidateHelper.BetweenPeriod(WorkContext.MallConfig.BanAccessTime)  && WorkContext.PageKey != "/account/login" && WorkContext.PageKey != "/account/logout")
            {
                filterContext.Result = PromptView("当前时间不能访问本系统");
                return;
            }

            //当用户ip在被禁止的ip列表时
            if (ValidateHelper.InIPList(WorkContext.IP, WorkContext.MallConfig.BanAccessIP))
            {
                filterContext.Result = PromptView("您的IP被禁止访问本系统");
                return;
            }

            //当用户ip不在允许的ip列表时
            if (!string.IsNullOrEmpty(WorkContext.MallConfig.AllowAccessIP) && !ValidateHelper.InIPList(WorkContext.IP, WorkContext.MallConfig.AllowAccessIP))
            {
                filterContext.Result = PromptView("您的IP被禁止访问本系统");
                return;
            }

            ////当用户IP被禁止时 （不使用此功能）
            //if (BannedIPs.CheckIP(WorkContext.IP))
            //{
            //    filterContext.Result = PromptView("您的IP被禁止访问本系统");
            //    return;
            //}

            //判断目前访问人数是否达到允许的最大人数
            if (WorkContext.OnlineUserCount > WorkContext.MallConfig.MaxOnlineCount  && (WorkContext.Controller != "account" && (WorkContext.Action != "login" || WorkContext.Action != "logout")))
            {
                filterContext.Result = PromptView("系统人数达到访问上限, 请稍等一会再访问！");
                return;
            }

            //强行显示一下（测试）
            //filterContext.Result = PromptView("系统人数达到访问上限, 请稍等一会再访问！");


        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //不能应用在子方法上
            if (filterContext.IsChildAction)
                return;

            ////清空执行的sql语句数目
            //RDBSHelper.ExecuteCount = 0;
            ////清空执行的sql语句细节
            //RDBSHelper.ExecuteDetail = string.Empty;

            //页面开始执行时间
            WorkContext.StartExecuteTime = DateTime.Now;

            ////当用户为会员时,更新用户的在线时间
            if (WorkContext.Uid > 0)
            {
                Users.SetOnlineUsers(WorkContext.Uid);
            }

            ////更新在线用户
            //Asyn.UpdateOnlineUser(WorkContext.Uid, WorkContext.Sid, WorkContext.NickName, WorkContext.IP, WorkContext.RegionId);
            ////更新PV统计
            //Asyn.UpdatePVStat(WorkContext.StoreId, WorkContext.Uid, WorkContext.RegionId, WebHelper.GetBrowserType(), WebHelper.GetOSType());
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //不能应用在子方法上
            if (filterContext.IsChildAction)
                return;
#if DEBUG
            ////执行的sql语句数目
            //WorkContext.ExecuteCount = RDBSHelper.ExecuteCount;

            ////执行的sql语句细节
            //if (RDBSHelper.ExecuteDetail == string.Empty)
            //    WorkContext.ExecuteDetail = "<div style=\"display:block;clear:both;text-align:center;width:100%;margin:5px 0px;\">当前页面没有和数据库的任何交互</div>";
            //else
            //    WorkContext.ExecuteDetail = "<div style=\"display:block;clear:both;text-align:center;width:100%;margin:5px 0px;\">数据查询分析:</div>" + RDBSHelper.ExecuteDetail;
#endif
            //页面执行时间
            WorkContext.ExecuteTime = DateTime.Now.Subtract(WorkContext.StartExecuteTime).TotalMilliseconds / 1000;
        }

        /// <summary>
        /// 提示信息视图
        /// </summary>
        /// <param name="message">提示信息</param>
        /// <returns></returns>
        protected ViewResult PromptView(string message)
        {
            return View("prompt", new PromptModel(message));
        }
    }
}

