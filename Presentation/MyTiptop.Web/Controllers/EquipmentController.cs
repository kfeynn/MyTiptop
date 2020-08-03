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
using System.Collections.Generic;

using MyTiptop.Web.Models;
using MyTiptop.Web.Framework;
using MyTiptop.Core;
using MyTiptop.Data;
using MyTiptop.Services;
using MyTiptop.OraCore;

namespace MyTiptop.Web.Controllers
{
    
    public class EquipmentController : BaseWebController
    {
        //private ApplicationSignInManager _signInManager;
        //private ApplicationUserManager _userManager;

        public EquipmentController()
        {
        }

        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="PrcsName">用户（查询用）</param>
        /// <param name="pageNumber">当前页码</param>
        /// <param name="pageSize">页尺寸</param>
        /// <returns></returns>
        public ActionResult EquipmentList(string Ecode, int pageNumber = 1, int pageSize = 15)
        {

            //test
           // Class1.UserIsExists("", "");



            // 获取参数、分页信息 （Post 传参数传递约定大于配置）
            // 查询所有用户信息
            if (pageSize <= 0)
                //防止除0操作
                pageSize = 1;
            //查询条件
            string filter = String.Empty;
            if (Ecode != null && Ecode != "")
            {
                filter += " Ecode  like '%" + Ecode + "%'";
            }
            
            //返回总页数、总记录数
            int totalPage;
            int totalRecord;
            //分页查询
            List<Equipment> list = new RDBSHelper().ExecutePaging<Equipment>("Equipment", "*", "  id asc", filter, pageSize, pageNumber, out totalPage, out totalRecord);
            //页脚Model
            PageModel pageModel = new PageModel(pageSize, pageNumber, totalRecord);
            //初始化ViewModel
            EquipmentViewModel model = new EquipmentViewModel()
            {
                PageModel = pageModel,
                List = list
                 
            };

            ////记录本次访问地址， 用于返回上一层。
            //MallUtils.SetAdminRefererCookie(string.Format("{0}?pageNumber={1}&pageSize={2}",
            //                      Url.Action("SortList"), pageModel.PageNumber, pageModel.PageSize
            //                      ));

            //返回View
            return View(model);
        }






    }
}