using System;
using System.Collections;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyTiptop.Web.Framework;
using MyTiptop.Web.MallAdmin.Models;
using MyTiptop.Services;
using MyTiptop.Data;
using MyTiptop.Core;

namespace MyTiptop.Web.MallAdmin.Controllers
{
    public class HomeController : BaseMallAdminController
    {
        //整合bootstarp 样式后台。
        public ActionResult Default()
        {

            MenuViewModel model = new Models.MenuViewModel();
            model.Func = MyTiptop.Core.BMAData.RDBS.GetFunctionByUserId(WorkContext.Uid);

            //后台调用一下 方法.使程序oracle映射
            //MyTiptop.OraData.OraQuery.ThreadMethod();

            return View(model);
        }

        // 后台主页
        public ActionResult Index()
        {
            return View();
        }
        //上部导航页
        public ActionResult navbar()
        {
            //后台调用一下 方法.使程序oracle映射
            //MyTiptop.OraData.OraQuery.ThreadMethod();

            return View();
        }
        //左侧导航页
        public ActionResult menu()
        {
            MenuViewModel model = new Models.MenuViewModel();
            model.Func = MyTiptop.Core.BMAData.RDBS.GetFunctionByUserId(WorkContext.Uid);

            //// 为url 添加配置的子目录
            //foreach (xpGrid_Functions funs in model.Func)
            //{
            //    funs.FuncUrl = WorkContext.ScriptCDN + funs.FuncUrl;

            //}



            return View(model);
        }
        //业务提醒功能页
        public ActionResult mallruninfo()
        {
            // 
            return View(); 
        }

    }
}