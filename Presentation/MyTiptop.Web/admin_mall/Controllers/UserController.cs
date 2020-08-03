using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using MyTiptop.Web.Framework;
using MyTiptop.Web.MallAdmin.Models;
using MyTiptop.Data;
using MyTiptop.Services;
using MyTiptop.Core;
using System.Data;

namespace MyTiptop.Web.MallAdmin.Controllers
{
    public class UserController : BaseMallAdminController
    {
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="userName">用户（查询用）</param>
        /// <param name="pageNumber">当前页码</param>
        /// <param name="pageSize">页尺寸</param>
        /// <returns></returns>
        public ActionResult List(string userName, string userCName, int pageNumber = 1, int pageSize = 15)
        {
            // 获取参数、分页信息 （Post 传参数传递约定大于配置）
            // string returnUrl = WebHelper.GetFormString("UserName");
            // 查询所有用户信息
            if (pageSize <= 0)
                //防止除0操作
                pageSize = 1;
            //查询条件
            string filter = String.Empty;
            if (userName!=null && userName != "")
            {
                filter += " userName  like '%" + userName + "%'";
            }
            if (userCName!=null && userCName != "")
            {
                if (filter.Length > 0)
                {
                    filter = filter + " and ";
                }
                filter += " userCName  like '%" + userCName + "%'";
            }
            //返回总页数、总记录数
            int totalPage;
            int totalRecord;
            //分页查询
            List<xpGrid_User> userList = new RDBSHelper().ExecutePaging<xpGrid_User>("xpGrid_User", "*", " deleted asc, userid asc", filter, pageSize, pageNumber, out totalPage, out totalRecord);
            //页脚Model
            PageModel pageModel = new PageModel(pageSize, pageNumber, totalRecord);
            //初始化ViewModel
            UserListViewModel model = new UserListViewModel()
            {
                PageModel = pageModel,
                UserList = userList,
                UserName = userName,
                UserCName = userCName
            };
            //记录本次访问地址， 用于返回上一层。
            MallUtils.SetAdminRefererCookie(string.Format("{0}?pageNumber={1}&pageSize={2}&userName={3}&userCName={4}",
                                  Url.Action("list"), pageModel.PageNumber, pageModel.PageSize,
                                  userName, userCName));
            //返回View
            return View(model);
        }

        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit()
        {
            int uid = WebHelper.GetQueryInt("uid");

            xpGrid_User userInfo = Users.GetUserById(uid);

            if (userInfo == null)
            {
                return PromptView("用户不存在");
            }
            UserViewModel model = new UserViewModel()
            {
                User = userInfo,
                isdeleted = Convert.ToBoolean(userInfo.deleted)
            };
            //获取返回上一页链接信息，比 history.go(-1) 多了刷新功能
            ViewBag.referer = MallUtils.GetMallAdminRefererCookie();
            return View(model);
        }

        /// <summary>
        /// 编辑用户（确认修改）
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(UserViewModel Model, int uid = -1)
        {
            //可以不取
            //uid = WebHelper.GetFormInt("uid");
            xpGrid_User userInfo = Users.GetUserById(uid);
            if (userInfo == null)
            {
                return PromptView("用户不存在");
            }
            //xpGrid_User u2 = Users.GetUserByName(Model.User.UserName);
            //if (u2 == null)
            //{
            //    return PromptView("用户不存在");
            //}
            userInfo.UserName = Model.User.UserName;
            userInfo.UserCName = Model.User.UserCName;
            userInfo.deleted = Convert.ToInt16(Model.isdeleted);

            if (ModelState.IsValid)
            {
                Users.UpdateUser(userInfo, uid);
                //获取返回上一页链接信息，比 history.go(-1) 多了刷新功能
                ViewBag.referer = MallUtils.GetMallAdminRefererCookie();

                return PromptView("用户修改成功");
            }
            //Model.User.UserCName = u2.UserCName;
            return View(Model);
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Add()
        {
            UserAddViewModel model = new UserAddViewModel();
            //返回上一级目录
            ViewBag.referer = MallUtils.GetMallAdminRefererCookie();
            return View(model);
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(UserAddViewModel model)
        {
            string userName = model.User.UserName;
            string userCName = model.User.UserCName;
            //model.AddFlag;
            xpGrid_User userInfo = Users.GetUserByName(userName);
            if (userInfo != null)
            {
                return PromptView("已经存在用户");
            }


            ViewBag.referer = MallUtils.GetMallAdminRefererCookie();
            //启用模型验证 eg: @Html.ValidationMessageFor 
            if (ModelState.IsValid)
            {
                //添加用户
                if (!Users.AddUser(userName, userCName))
                {
                    return PromptView("用户名错误");
                }
                if (model.AddFlag)
                {
                    model.Message = "添加成功！";
                    //连续添加
                    return View(model);
                }
                //整理跳转链接，到上一级目录    malladmin/user
                string subreferer = GetAdminRefererStr(ViewBag.referer);
                return Redirect(subreferer);
            }
            return View(model);
        }


        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="uid">用户ID</param>
        /// <returns></returns>
        public ActionResult delete(int uid = -1)
        {
            xpGrid_User userInfo = Users.GetUserById(uid);
            if (userInfo == null)
            {
                return PromptView("用户不存在");
            }
            try
            {
                Users.DeleteUser(uid);
            }
            catch (Exception Ex)
            {
                return PromptView("删除错误："+Ex.Message);
            }
            return PromptView("用户已经删除！");
        }
  
        
        /// <summary>
        /// 根据工号找姓名 AJAX 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public ActionResult GetUserNameAjax(string userName)
        {
            string UserCName = Users.GetUserCNameByUserName(userName);

            return AjaxResult("success", UserCName);
        }
                
        /// <summary>
        /// 获取上一级访问地址，用于转向Redirect
        /// </summary>
        /// <param name="referer"></param>
        /// <returns></returns>
        public string GetAdminRefererStr(string referer)
        {
            string subreferer = referer.Substring(referer.LastIndexOf("?") + 1);

            subreferer = string.Format("{0}?{1}", Url.Action("list"), subreferer);

            return subreferer;
        }

        [HttpGet]
        public ActionResult resetpwd()
        {
            //当前登录ID
            ResetPwdViewModel model = new ResetPwdViewModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult resetpwd(string password, string confirmPwd)
        {
            if (password != confirmPwd)
            {
                return PromptView("2次输入的密码不一致");
            }
            string Message = String.Empty;
            //重新设置密码 
            if (Users.ResetPassword(WorkContext.Uid, password))
            {
                //重新设置Cookie防止被T出系统
                MallUtils.SetCookiePassword(password);
                Message = "密码修改成功！";
            }
            ResetPwdViewModel model = new ResetPwdViewModel
            {
                Message = Message
            };
            //当前登录ID 
            return View(model);
        }

        [HttpGet]
        public ActionResult changepassword() 
        {
            //记录本次访问地址， 用于返回上一层。
            MallUtils.SetAdminRefererCookie(string.Format("{0}",Url.Action("changepassword")));
            //当前登录ID 
            ResetPwdViewModel model = new ResetPwdViewModel();
            return View(model); 
        }

        [HttpPost]
        public ActionResult changepassword(string password, string confirmPassword)
        {
            if (password != confirmPassword)
            {
                return PromptView("2次输入的密码不一致");
            }
            string Message = String.Empty;
            //重新设置密码 
            if (Users.ResetPassword(WorkContext.Uid, password))
            {
                //重新设置Cookie防止被T出系统
                MallUtils.SetCookiePassword(password);
                Message = "修改成功！";
            }
            ResetPwdViewModel model = new ResetPwdViewModel
            {
                Message = Message
            };
            //当前登录ID 
            return View(model);
        }


        //[HttpGet]
        public ActionResult UserAuthorization(int uid = -1)
        {
            xpGrid_User userInfo = Users.GetUserById(uid);
            if (userInfo == null)
            {
                return PromptView("用户不存在");
            }
            //返回上一级目录
            ViewBag.referer = MallUtils.GetMallAdminRefererCookie();

            DataTable roleList = Users.UserAuthorizationRole(uid);

            UserAuthorizationViewModel Model = new UserAuthorizationViewModel
            {
                user = userInfo,
                Message = "",
                RoleList = roleList
            };
            return View(Model);
        }


        public ActionResult UserAuthorizationChange(int[] pmIdList, int uid = -1)
        {
            xpGrid_User userInfo = Users.GetUserById(uid);
            if (userInfo == null)
            {
                return PromptView("用户不存在");
            }
            string Message = "";
            //修改授权
            if (Users.UserAuthorizationChange(uid, pmIdList))
                Message = "修改成功";
            //返回上一级目录
            ViewBag.referer = MallUtils.GetMallAdminRefererCookie();

            DataTable roleList = Users.UserAuthorizationRole(uid);

            UserAuthorizationViewModel Model = new UserAuthorizationViewModel
            {
                user = userInfo,
                Message = Message,
                RoleList = roleList
            };
            return View("UserAuthorization", Model);
        }








    }

}
