using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyTiptop.Web.Models;
using System.Data.SqlClient;
using MyTiptop.Web.Framework;
using MyTiptop.Data;
using MyTiptop.Core;

namespace MyTiptop.Web.Controllers
{
    public class HomeController : BaseWebController
    {
        #region 
        //[Authorize]
        //public ActionResult Index()
        //{
        //    //测试在数据库里随便取个数
        //    MyTiptop.Core.QASystemDBContext dbContext = new Core.QASystemDBContext();
        //    Bl_Produce_ShopViewModel model = new Bl_Produce_ShopViewModel();
        //    model.name = "555";
        //    model.Bl_Produce_Shop = dbContext.Bl_Produce_Shop.ToList();

        //    //测试添加一条数据
        //    //MyTiptop.Core.Bl_Produce_Shop data = new Core.Bl_Produce_Shop();
        //    //data.ShopName = "测试Test";
        //    //dbContext.Bl_Produce_Shop.Add(data);
        //    //dbContext.SaveChanges();

        //    //测试删除
        //    //using (var db = new EFContext())
        //    //{
        //    //    var person = db.Persons.Where(m => m.PersonId == 4).FirstOrDefault();
        //    //    db.Persons.Remove(person);
        //    //    db.SaveChanges();
        //    //}
        //    //var aa = dbContext.Bl_Produce_Shop.Where(m => m.ShopName == "测试Test").FirstOrDefault();
        //    //if (aa != null)
        //    //{
        //    //    dbContext.Bl_Produce_Shop.Remove(aa);
        //    //    dbContext.SaveChanges();
        //    //}
        //    ////测试修改一条数据
        //    //var bb = dbContext.Bl_Produce_Shop.Where(m => m.ShopName == "01号棚").FirstOrDefault();
        //    //if (bb != null)
        //    //{
        //    //    bb.ShopName = "01号棚555";
        //    //    dbContext.SaveChanges();
        //    //}
        //    //// dbContext.Database.Connection.ConnectionString; 


        //    //// 测试执行存储过程分页
        //    //MyTiptop.Core.RDBSHelper help = new Core.RDBSHelper();

        //    //int totalpage;
        //    //int totalrecord;

        //    //// 返回类型需要有特定的视图 eg：MyTiptop.Core.v_Qa_FirstStepData . 此方法必须取表、视图 所有字段。。。
        //    //List<MyTiptop.Core.v_Qa_FirstStepData> List = help.ExecutePaging<MyTiptop.Core.v_Qa_FirstStepData>("v_Qa_FirstStepData", "*", "RecDataId", "", 10, 1, out totalpage, out totalrecord);

        //    //if (List != null)
        //    //{
        //    //    foreach (MyTiptop.Core.v_Qa_FirstStepData SingleDate in List)
        //    //    {
        //    //        DateTime dt = SingleDate.CheckDate;
        //    //        string aabb = SingleDate.ThreadName;
        //    //    }
        //    //}
        //    ////测试执行 存储过程分页。ADO.NET 方式 可以取部分表、视图。
        //    //System.Data.DataTable dtt = help.GetList("v_Qa_FirstStepData", "*", "RecDataId", "", 10, 1, out totalpage, out totalrecord);
        //    //if (dtt != null)
        //    //{
        //    //    string aaa = dtt.Rows[0][1].ToString();
        //    //    string bbb = dtt.Rows[0][2].ToString();
        //    //}
        //    ////测试
        //    //string aaaaa = MyTiptop.Data.helps.GetProvinceRegionStat(100);
        //    ////int iiii = 0;

        //    return View(model);
        //}
        #endregion

        //整合bootstarp 样式后台。
        public ActionResult Default()
        {

            MenuViewModel model = new Models.MenuViewModel();
            //model.Func = MyTiptop.Core.BMAData.RDBS.GetFunctionByUserId(WorkContext.Uid);
            model.Func = Roles.GetFuncForPublic();

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
            return View();
        }
        //左侧导航页
        public ActionResult menu()
        {
            MenuViewModel model = new Models.MenuViewModel();
           
            model.Func = Roles.GetFuncForPublic(); 

            return View(model);
        }
        //业务提醒功能页
        public ActionResult mallruninfo() 
        { 
            //后台调用一下  Oracle方法。
            MyTiptop.OraData.OraQuery.ThreadMethod();
            //后台调用一下 供应商数据库。
            MyTiptop.SupplierData.Query.ThreadMethod();;
            //后台调用一下  Sqlserver方法。
            MyTiptop.Data.DBQuery.ThreadMethod();

            return View(); 
        } 

    }
}