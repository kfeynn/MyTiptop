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
using MyTiptop.OraCore;
using MyTiptop.OraData;
using System.Transactions;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace MyTiptop.Web.MallAdmin.Controllers
{
    public class RunGridViewController : BaseMallAdminController
    {

        #region  通用查询

        /// <summary>
        /// 运行通用查询方法
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult RunGridQuery(string BaseRunGridViewID = "",string msg="")
        {
            //1.BaseRunGridViewID  =""  返回 下拉列表
            //2.BaseRunGridViewID !=""  返回 下拉列表 && 二级查询条件 Condition

            //var selMould = new DBContext().Base_RunGridView.Where(u => u.DBType == "1000").ToList().Select(x => new SelectListItem
            var selMould = new DBContext().Base_RunGridView.Where(u => u.Type == "1000").OrderBy(u => u.ViewOrder).ToList().Select(x => new SelectListItem
            {
                Value = x.BaseRunGridViewID.ToString(),
                Text = x.GridViewName
            });

            //默认选中项
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (SelectListItem item in selMould)
            {
                //默认选中
                if (item.Value == Convert.ToString(BaseRunGridViewID))
                    items.Add(new SelectListItem { Text = item.Text, Value = item.Value, Selected = true });
                else
                    items.Add(new SelectListItem { Text = item.Text, Value = item.Value });
            }

            var condition = new RunGridViewModel().ConditionList;

            if (BaseRunGridViewID != "")
            {
                //[Base_RunGridView_Condition]
                condition = RunGridViewConditions.GetList(int.Parse(BaseRunGridViewID));
            }

            //
            RunGridViewModel model = new RunGridViewModel
            {
                BaseRunGridViewID = items,
                ConditionList = condition,
                msg = msg,
                PageModel =  new PageModel(15,1,1)
            };

            //记录本次访问地址， 用于返回上一层。
            MallUtils.SetAdminRefererCookie(string.Format("{0}", 
                                  Url.Action("RunGridQuery"))
                                  ); 
             
            return View(model);
        }

        [HttpPost]
        public ActionResult RunGridQuery(string gridId = "" , int outExcel= 0 , int pageNumber = 1, int pageSize = 15)
        {
            //1.组织条件.拼接查询字符串
            //2.判断数据库 sql 、oracle 。
            //3.调用对应的查询方法。

            string cmdStr = ""; //  执行语句
            string dbs = ""; // 参数条件{type=0}
            string filter = ""; //  查询条件 

            //回传 阻止查询条件 
            if (gridId == "")
            {
                //跳转
                //返回页面.转向显示  省去为下拉框附值代码
                return RedirectToAction("RunGridQuery", new { msg = "没有找到查询表格名称!" });
            }
            //获取运行表Model
            var gridModel = BaseRunGridViews.GetModel(TypeHelper.StringToInt(gridId));
            if (gridModel == null)
            {
                //返回页面.转向显示  省去为下拉框附值代码
                return RedirectToAction("RunGridQuery", new { msg = "没有找到查询表格名称!" });
            }
            cmdStr = gridModel.strSelect;  //执行语句

            //获取条件配置列表
            var conditionList = RunGridViewConditions.GetList(TypeHelper.StringToInt(gridId));
            if (conditionList != null && conditionList.Count > 0)
            {
                #region  整理组织查询条件
                foreach (var conModel in conditionList)
                {
                    //组织查询条件
                    //判断条件定义中 是否为必要条件。
                    string formName = conModel.formName;
                    string txtFormName = "txt" + conModel.formName;
                    string oper = WebHelper.GetFormString(formName);
                    string value = WebHelper.GetFormString(txtFormName);

                    //保存 用户当次查询 操作符、值
                    conModel.currOperator = oper;
                    conModel.currValue = value;

                    if (conModel.inputtcii != null && conModel.inputtcii == 1)
                    {
                        //必要条件
                        if (value == null || value == "" || oper == null || oper == "")
                        {
                            return RedirectToAction("RunGridQuery", new { msg = "请输入必要参数!" });
                        }
                    }
                    if (oper != null && oper.Length > 0 && value != null && value.Length > 0)
                    {
                        switch (gridModel.SqlType)
                        {
                            case "Sql":   //命令为查询语句  

                                if (conModel.type == 0 && conModel.field == "DBS")   // 是否有选择 dbs的 特殊参数 ？
                                {
                                    dbs = value;
                                }
                                else
                                {
                                    if (gridModel.DBType == "Oracle" && conModel.datetype == "datetime")
                                    {
                                        //Oracle && datetime 
                                        value = "to_date('" + value + "', 'yyyy/mm/dd')";
                                    }
                                    if (conModel.datetype == "varchar" && oper == "like")
                                    {
                                        //like && varchar
                                        value = "'%" + value + "%'";
                                    }
                                    else if (conModel.datetype == "varchar")
                                    {
                                        value = "'" + value + "'";
                                    }
                                    else if (conModel.datetype == "select")
                                    {
                                        if (!TypeHelper.isFloat(value) && !TypeHelper.isNumberic(value))
                                            value = "'" + value + "'";
                                    }

                                    if (filter.Length > 0)
                                    {
                                        filter += " and ";
                                    }
                                    filter += conModel.field + " " + oper + value;
                                }
                                break;

                            case "Proc":  // 命令为存储过程

                                if (!TypeHelper.isFloat(value) && !TypeHelper.isNumberic(value))
                                {
                                    value = "'" + value + "'";
                                }

                                if (filter.Length > 0)
                                {
                                    filter += ",";
                                }
                                filter += value;
                                break;
                        }

                    }
                }

                //有条件 前面加个 and 约定。 proc 不用加
                if (filter != "" && gridModel.SqlType == "Sql")
                {
                    filter = " AND " + filter;
                }

                #endregion
                if (dbs.Length > 0)
                {
                    //带入查询条件 ,有dbs参数的
                    cmdStr = String.Format(cmdStr,dbs,filter);
                }
                else
                {
                    //带入查询条件
                    cmdStr = String.Format(cmdStr,filter);
                }
            }

            string message = "";

            message = "success";
            //run cmdstr 
            DataTable dt = null;

            try
            {

                if (gridModel.DBType == "Oracle")
                {
                    dt = OraQuery.GetCommonQuery(cmdStr);
                }
                else if (gridModel.DBType == "Sql")
                {
                    dt = DBQuery.GetCommonQuery(cmdStr);
                }

            }
            catch(Exception Ex)
            {
                message = "err : " + Ex.Message;
            }


            #region select 
            var selMould = new DBContext().Base_RunGridView.Where(u => u.Type == "1000").OrderBy(u => u.ViewOrder).ToList().Select(x => new SelectListItem
            {
                Value = x.BaseRunGridViewID.ToString(),
                Text = x.GridViewName
            });

            //默认选中项
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (SelectListItem item in selMould)
            {
                //默认选中
                if (item.Value == Convert.ToString(gridId))
                    items.Add(new SelectListItem { Text = item.Text, Value = item.Value, Selected = true });
                else
                    items.Add(new SelectListItem { Text = item.Text, Value = item.Value });
            }
            #endregion

            var condition = new RunGridViewModel().ConditionList;

            //if (gridId != "")
            //{
            //    //[Base_RunGridView_Condition]
            //    condition = RunGridViewConditions.GetList(int.Parse(gridId));
            //    //保存用户当前选择的 操作符、值
            //}

            if (outExcel == 1)
            {
                //导出到Excel
                return RedirectToAction("Export", new { dbType = gridModel.DBType, dbs = dbs,  filter = filter, gridId = gridId });

                //return Redirect(referer);
            }


            /////////////////////////// 页面级分页 //////////////////////////////

            //总纪录数
            int totalRecord = 0;
            totalRecord = dt.Rows.Count;
            DataTable dtnew = dt.Clone();
            dtnew.Clear();
            //起始脚标
            int minindex = (pageNumber - 1) * pageSize;
            int maxindex = pageNumber * pageSize;
            if (dt != null)
            {
                //循环用于生成新表
                for (int i = minindex; i < maxindex && i < dt.Rows.Count; i++)
                {
                    //添加新行
                    dtnew.Rows.Add(dt.Rows[i].ItemArray);
                }
            }
            PageModel pageModel = new PageModel(pageSize, pageNumber, totalRecord);


            /////////////////////////////////////////////////////////////////////////////////


            RunGridViewModel model = new RunGridViewModel 
            { 
                BaseRunGridViewID = items, 
                ConditionList = conditionList,
                msg = message, 
                dt = dtnew,
                PageModel = pageModel
            };
            //记录本次访问地址， 用于返回上一层。
            MallUtils.SetAdminRefererCookie(string.Format("{0}",
                                  Url.Action("RunGridQuery"))
                                  );
            return View(model);
        }









        /// <summary>
        /// 导出
        /// </summary>
        /// <returns></returns>
        /// 允许 危险的参数 带 sql关键字
        [ValidateInput(false)]
        public FileResult Export(string dbType="", string dbs="" , string filter="", string gridId = "")
        {
            //创建Excel文件的对象
            //NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();   // XLS
            NPOI.XSSF.UserModel.XSSFWorkbook book = new NPOI.XSSF.UserModel.XSSFWorkbook();     // XLSX
            //添加一个sheet
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");

            DataTable dt = null;

            var gridModel = BaseRunGridViews.GetModel(TypeHelper.StringToInt(gridId));

            if (gridModel != null)
            {
                string cmdStr = gridModel.strSelect;

                if (dbs.Length > 0)
                {
                    //带入查询条件 ,有dbs参数的
                    cmdStr = String.Format(cmdStr, dbs, filter);
                }
                else
                {
                    //带入查询条件
                    cmdStr = String.Format(cmdStr, filter);
                }
                //cmdStr = String.Format(cmdStr, filter);

                if (dbType == "Oracle")
                {
                    dt = OraQuery.GetCommonQuery(cmdStr);
                }
                else if (dbType == "Sql")
                {
                    dt = DBQuery.GetCommonQuery(cmdStr);
                }
            }

            //给sheet1添加第一行的头部标题
            NPOI.SS.UserModel.IRow row = sheet1.CreateRow(0);

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                row.CreateCell(i).SetCellValue(dt.Columns[i].ColumnName); //标题
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                //将数据逐步写入sheet1各个行
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        rowtemp.CreateCell(j).SetCellValue(dt.Rows[i][j].ToString());
                    }
                }
            }
            // 写入到客户端  
            var ms = new NpoiMemoryStream();
            ms.AllowClose = false;
            book.Write(ms);
            ms.Flush();
            ms.Seek(0, SeekOrigin.Begin);
            ms.AllowClose = true;
            return File(ms, "application/vnd.ms-excel", "Download.xlsx");
        }

        #endregion


        #region 通用查询2(带图表)
        /// <summary>
        /// 运行通用查询方法
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult RunGridQuery2(string BaseRunGridViewID = "", string msg = "")
        {
            //1.BaseRunGridViewID  =""  返回 下拉列表
            //2.BaseRunGridViewID !=""  返回 下拉列表 && 二级查询条件 Condition

            //var selMould = new DBContext().Base_RunGridView.Where(u => u.DBType == "1000").ToList().Select(x => new SelectListItem
            var selMould = new DBContext().Base_RunGridView.Where(u => u.Type == "2000").OrderBy(u => u.ViewOrder).ToList().Select(x => new SelectListItem
            {
                Value = x.BaseRunGridViewID.ToString(),
                Text = x.GridViewName
            });

            //默认选中项
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (SelectListItem item in selMould)
            {
                //默认选中
                if (item.Value == Convert.ToString(BaseRunGridViewID))
                    items.Add(new SelectListItem { Text = item.Text, Value = item.Value, Selected = true });
                else
                    items.Add(new SelectListItem { Text = item.Text, Value = item.Value });
            }

            var condition = new RunGridViewModel().ConditionList;

            if (BaseRunGridViewID != "")
            {
                //[Base_RunGridView_Condition]
                condition = RunGridViewConditions.GetList(int.Parse(BaseRunGridViewID));
            }

            //
            var model = new RunGridViewModel2
            {
                BaseRunGridViewID = items,
                ConditionList = condition,
                msg = msg,
                PageModel = new PageModel(15, 1, 1),
                dtJson = new DataTable().DataTableToJson(),   //没有值得话 给个 json格式空值
                dimensionJson = new DataTable().DataTableToJsonForDimension(),
                barStrJson = new DataTable().DataTableToJsonForBar(),
                width = "50%",
                height = "30px"
            };

            //记录本次访问地址， 用于返回上一层。
            MallUtils.SetAdminRefererCookie(string.Format("{0}",
                                  Url.Action("RunGridQuery2"))
                                  );

            return View(model);
        }

        [HttpPost]
        public ActionResult RunGridQuery2(string gridId = "", int outExcel = 0, int pageNumber = 1, int pageSize = 15)
        {
            //1.组织条件.拼接查询字符串
            //2.判断数据库 sql 、oracle 。
            //3.调用对应的查询方法。

            string cmdStr = ""; //  执行语句
            string dbs = ""; // 参数条件{type=0}
            string filter = ""; //  查询条件 

            //回传 阻止查询条件 
            if (gridId == "")
            {
                //跳转
                //返回页面.转向显示  省去为下拉框附值代码
                return RedirectToAction("RunGridQuery2", new { msg = "没有找到查询表格名称!" });
            }
            //获取运行表Model
            var gridModel = BaseRunGridViews.GetModel(TypeHelper.StringToInt(gridId));
            if (gridModel == null)
            {
                //返回页面.转向显示  省去为下拉框附值代码
                return RedirectToAction("RunGridQuery2", new { msg = "没有找到查询表格名称!" });
            }
            cmdStr = gridModel.strSelect;  //执行语句

            //获取条件配置列表
            var conditionList = RunGridViewConditions.GetList(TypeHelper.StringToInt(gridId));
            if (conditionList != null && conditionList.Count > 0)
            {
                #region  整理组织查询条件
                foreach (var conModel in conditionList)
                {
                    //组织查询条件
                    //判断条件定义中 是否为必要条件。
                    string formName = conModel.formName;
                    string txtFormName = "txt" + conModel.formName;
                    string oper = WebHelper.GetFormString(formName);
                    string value = WebHelper.GetFormString(txtFormName);

                    //保存 用户当次查询 操作符、值
                    conModel.currOperator = oper;
                    conModel.currValue = value;

                    if (conModel.inputtcii != null && conModel.inputtcii == 1)
                    {
                        //必要条件
                        if (value == null || value == "" || oper == null || oper == "")
                        {
                            return RedirectToAction("RunGridQuery2", new { msg = "请输入必要参数!" });
                        }
                    }
                    if (oper != null && oper.Length > 0 && value != null && value.Length > 0)
                    {
                        switch (gridModel.SqlType)
                        {
                            case "Sql":   //命令为查询语句  

                                if (conModel.type == 0 && conModel.field == "DBS")   // 是否有选择 dbs的 特殊参数 ？
                                {
                                    dbs = value;
                                }
                                else
                                {
                                    if (gridModel.DBType == "Oracle" && conModel.datetype == "datetime")
                                    {
                                        //Oracle && datetime 
                                        value = "to_date('" + value + "', 'yyyy/mm/dd')";
                                    }
                                    if (conModel.datetype == "varchar" && oper == "like")
                                    {
                                        //like && varchar
                                        value = "'%" + value + "%'";
                                    }
                                    else if (conModel.datetype == "varchar")
                                    {
                                        value = "'" + value + "'";
                                    }
                                    else if (conModel.datetype == "select")
                                    {
                                        if (!TypeHelper.isFloat(value) && !TypeHelper.isNumberic(value))
                                            value = "'" + value + "'";
                                    }
                                    if (filter.Length > 0)
                                    {
                                        filter += " and ";
                                    }
                                    filter += conModel.field + " " + oper + value;
                                }
                                break;

                            case "Proc":  // 命令为存储过程

                                if (!TypeHelper.isFloat(value) && !TypeHelper.isNumberic(value))
                                {
                                    value = "'" + value + "'";
                                }

                                if (filter.Length > 0)
                                {
                                    filter += ",";
                                }
                                filter += value;
                                break;
                        }

                    }
                }

                //有条件 前面加个 and 约定。 proc 不用加
                if (filter != "" && gridModel.SqlType == "Sql")
                {
                    filter = " AND " + filter;
                }

                #endregion
                if (dbs.Length > 0)
                {
                    //带入查询条件 ,有dbs参数的
                    cmdStr = String.Format(cmdStr, dbs, filter);
                }
                else
                {
                    //带入查询条件
                    cmdStr = String.Format(cmdStr, filter);
                }
            }


            string message = "";

            message = "success";
            //run cmdstr 
            DataTable dt = null;
            try
            {
                if (gridModel.DBType == "Oracle")
                {
                    dt = OraQuery.GetCommonQuery(cmdStr);
                }
                else if (gridModel.DBType == "Sql")
                {
                    dt = DBQuery.GetCommonQuery(cmdStr);
                }

            }
            catch (Exception Ex)
            {
                message = "err : " + Ex.Message;
            }

            #region select 
            var selMould = new DBContext().Base_RunGridView.Where(u => u.Type == "2000").OrderBy(u => u.ViewOrder).ToList().Select(x => new SelectListItem
            {
                Value = x.BaseRunGridViewID.ToString(),
                Text = x.GridViewName
            });

            //默认选中项
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (SelectListItem item in selMould)
            {
                //默认选中
                if (item.Value == Convert.ToString(gridId))
                    items.Add(new SelectListItem { Text = item.Text, Value = item.Value, Selected = true });
                else
                    items.Add(new SelectListItem { Text = item.Text, Value = item.Value });
            }
            #endregion

            var condition = new RunGridViewModel().ConditionList;

            //if (gridId != "")
            //{
            //    //[Base_RunGridView_Condition]
            //    condition = RunGridViewConditions.GetList(int.Parse(gridId));
            //    //保存用户当前选择的 操作符、值
            //}

            if (outExcel == 1)
            {
                //导出到Excel
                return RedirectToAction("Export", new { dbType = gridModel.DBType, dbs = dbs, filter = filter, gridId = gridId });

                //return Redirect(referer);
            }


            /////////////////////////// 页面级分页 //////////////////////////////

            //总纪录数
            int totalRecord = 0;
            totalRecord = dt.Rows.Count;
            DataTable dtnew = dt.Clone();
            dtnew.Clear();
            //起始脚标
            int minindex = (pageNumber - 1) * pageSize;
            int maxindex = pageNumber * pageSize;
            if (dt != null)
            {
                //循环用于生成新表
                for (int i = minindex; i < maxindex && i < dt.Rows.Count; i++)
                {
                    //添加新行
                    dtnew.Rows.Add(dt.Rows[i].ItemArray);
                }
            }
            PageModel pageModel = new PageModel(pageSize, pageNumber, totalRecord);


            /////////////////////////////////////////////////////////////////////////////////
            string width = "50%";

            string height = "400px";

            if (dt != null && dt.Columns.Count < 5 )
            {
                width = "20%";
            }
            else if (dt != null && dt.Columns.Count > 6 && dt.Columns.Count < 9)
            {
                width = "70%";
            }
            else if (dt != null && dt.Columns.Count > 8  )
            {
                width = "90%";
            }

            RunGridViewModel2 model = new RunGridViewModel2
            {
                BaseRunGridViewID = items,
                ConditionList = conditionList,
                msg = message,
                dt = dtnew,
                PageModel = pageModel,
                dtJson = dtnew.DataTableToJsonForEcharts(),  //转成json 传到前台交给js处理柱状图表
                dimensionJson = dtnew.DataTableToJsonForDimension(),
                barStrJson = dtnew.DataTableToJsonForBar(),
                width = width,
                height = height
            };
            //记录本次访问地址， 用于返回上一层。
            MallUtils.SetAdminRefererCookie(string.Format("{0}",
                                  Url.Action("RunGridQuery2"))
                                  );
            return View(model);
        }

        #endregion

        #region  通用查询3

        /// <summary>
        /// 运行通用查询方法
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult RunGridQuery3(string BaseRunGridViewID = "", string msg = "")
        {
            //1.BaseRunGridViewID  =""  返回 下拉列表
            //2.BaseRunGridViewID !=""  返回 下拉列表 && 二级查询条件 Condition

            //var selMould = new DBContext().Base_RunGridView.Where(u => u.DBType == "1000").ToList().Select(x => new SelectListItem
            var selMould = new DBContext().Base_RunGridView.Where(u => u.Type == "3000").OrderBy(u => u.ViewOrder).ToList().Select(x => new SelectListItem
            {
                Value = x.BaseRunGridViewID.ToString(),
                Text = x.GridViewName
            });

            //默认选中项
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (SelectListItem item in selMould)
            {
                //默认选中
                if (item.Value == Convert.ToString(BaseRunGridViewID))
                    items.Add(new SelectListItem { Text = item.Text, Value = item.Value, Selected = true });
                else
                    items.Add(new SelectListItem { Text = item.Text, Value = item.Value });
            }

            var condition = new RunGridViewModel().ConditionList;

            if (BaseRunGridViewID != "")
            {
                //[Base_RunGridView_Condition]
                condition = RunGridViewConditions.GetList(int.Parse(BaseRunGridViewID));
            }

            //
            RunGridViewModel model = new RunGridViewModel
            {
                BaseRunGridViewID = items,
                ConditionList = condition,
                msg = msg,
                PageModel = new PageModel(15, 1, 1)
            };

            //记录本次访问地址， 用于返回上一层。
            MallUtils.SetAdminRefererCookie(string.Format("{0}",
                                  Url.Action("RunGridQuery"))
                                  );

            return View(model);
        }

        [HttpPost]
        public ActionResult RunGridQuery3(string gridId = "", int outExcel = 0, int pageNumber = 1, int pageSize = 15)
        {
            //1.组织条件.拼接查询字符串
            //2.判断数据库 sql 、oracle 。
            //3.调用对应的查询方法。

            string cmdStr = ""; //  执行语句
            string dbs = ""; // 参数条件{type=0}
            string filter = ""; //  查询条件 

            //回传 阻止查询条件 
            if (gridId == "")
            {
                //跳转
                //返回页面.转向显示  省去为下拉框附值代码
                return RedirectToAction("RunGridQuery3", new { msg = "没有找到查询表格名称!" });
            }
            //获取运行表Model
            var gridModel = BaseRunGridViews.GetModel(TypeHelper.StringToInt(gridId));
            if (gridModel == null)
            {
                //返回页面.转向显示  省去为下拉框附值代码
                return RedirectToAction("RunGridQuery3", new { msg = "没有找到查询表格名称!" });
            }
            cmdStr = gridModel.strSelect;  //执行语句

            //获取条件配置列表
            var conditionList = RunGridViewConditions.GetList(TypeHelper.StringToInt(gridId));
            if (conditionList != null && conditionList.Count > 0)
            {
                #region  整理组织查询条件
                foreach (var conModel in conditionList)
                {
                    //组织查询条件
                    //判断条件定义中 是否为必要条件。
                    string formName = conModel.formName;
                    string txtFormName = "txt" + conModel.formName;
                    string oper = WebHelper.GetFormString(formName);
                    string value = WebHelper.GetFormString(txtFormName);

                    //保存 用户当次查询 操作符、值
                    conModel.currOperator = oper;
                    conModel.currValue = value;

                    if (conModel.inputtcii != null && conModel.inputtcii == 1)
                    {
                        //必要条件
                        if (value == null || value == "" || oper == null || oper == "")
                        {
                            return RedirectToAction("RunGridQuery", new { msg = "请输入必要参数!" });
                        }
                    }
                    if (oper != null && oper.Length > 0 && value != null && value.Length > 0)
                    {
                        switch (gridModel.SqlType)
                        {
                            case "Sql":   //命令为查询语句  

                                if (conModel.type == 0 && conModel.field == "DBS")   // 是否有选择 dbs的 特殊参数 ？
                                {
                                    dbs = value;
                                }
                                else
                                {
                                    if (gridModel.DBType == "Oracle" && conModel.datetype == "datetime")
                                    {
                                        //Oracle && datetime 
                                        value = "to_date('" + value + "', 'yyyy/mm/dd')";
                                    }
                                    if (conModel.datetype == "varchar" && oper == "like")
                                    {
                                        //like && varchar
                                        value = "'%" + value + "%'";
                                    }
                                    else if (conModel.datetype == "varchar")
                                    {
                                        value = "'" + value + "'";
                                    }
                                    else if (conModel.datetype == "select")
                                    {
                                        if (!TypeHelper.isFloat(value) && !TypeHelper.isNumberic(value))
                                            value = "'" + value + "'";
                                    }
                                    if (filter.Length > 0)
                                    {
                                        filter += " and ";
                                    }
                                    filter += conModel.field + " " + oper + value;
                                }
                                break;

                            case "Proc":  // 命令为存储过程

                                if (!TypeHelper.isFloat(value) && !TypeHelper.isNumberic(value))
                                {
                                    value = "'" + value + "'";
                                }

                                if (filter.Length > 0)
                                {
                                    filter += ",";
                                }
                                filter += value;
                                break;
                        }

                    }
                }

                //有条件 前面加个 and 约定。 proc 不用加
                if (filter != "" && gridModel.SqlType == "Sql")
                {
                    filter = " AND " + filter;
                }

                #endregion
                if (dbs.Length > 0)
                {
                    //带入查询条件 ,有dbs参数的
                    cmdStr = String.Format(cmdStr, dbs, filter);
                }
                else
                {
                    //带入查询条件
                    cmdStr = String.Format(cmdStr, filter);
                }
            }

            string message = "";

            message = "success";
            //run cmdstr 
            DataTable dt = null;

            try
            {

                if (gridModel.DBType == "Oracle")
                {
                    dt = OraQuery.GetCommonQuery(cmdStr);
                }
                else if (gridModel.DBType == "Sql")
                {
                    dt = DBQuery.GetCommonQuery(cmdStr);
                }

            }
            catch (Exception Ex)
            {
                message = "err : " + Ex.Message;
            }


            #region select 
            var selMould = new DBContext().Base_RunGridView.Where(u => u.Type == "3000").OrderBy(u => u.ViewOrder).ToList().Select(x => new SelectListItem
            {
                Value = x.BaseRunGridViewID.ToString(),
                Text = x.GridViewName
            });

            //默认选中项
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (SelectListItem item in selMould)
            {
                //默认选中
                if (item.Value == Convert.ToString(gridId))
                    items.Add(new SelectListItem { Text = item.Text, Value = item.Value, Selected = true });
                else
                    items.Add(new SelectListItem { Text = item.Text, Value = item.Value });
            }
            #endregion

            var condition = new RunGridViewModel().ConditionList;

            //if (gridId != "")
            //{
            //    //[Base_RunGridView_Condition]
            //    condition = RunGridViewConditions.GetList(int.Parse(gridId));
            //    //保存用户当前选择的 操作符、值
            //}

            if (outExcel == 1)
            {
                //导出到Excel
                return RedirectToAction("Export", new { dbType = gridModel.DBType, dbs = dbs, filter = filter, gridId = gridId });

                //return Redirect(referer);
            }


            /////////////////////////// 页面级分页 //////////////////////////////

            //总纪录数
            int totalRecord = 0;
            totalRecord = dt.Rows.Count;
            DataTable dtnew = dt.Clone();
            dtnew.Clear();
            //起始脚标
            int minindex = (pageNumber - 1) * pageSize;
            int maxindex = pageNumber * pageSize;
            if (dt != null)
            {
                //循环用于生成新表
                for (int i = minindex; i < maxindex && i < dt.Rows.Count; i++)
                {
                    //添加新行
                    dtnew.Rows.Add(dt.Rows[i].ItemArray);
                }
            }
            PageModel pageModel = new PageModel(pageSize, pageNumber, totalRecord);


            /////////////////////////////////////////////////////////////////////////////////


            RunGridViewModel model = new RunGridViewModel
            {
                BaseRunGridViewID = items,
                ConditionList = conditionList,
                msg = message,
                dt = dtnew,
                PageModel = pageModel
            };
            //记录本次访问地址， 用于返回上一层。
            MallUtils.SetAdminRefererCookie(string.Format("{0}",
                                  Url.Action("RunGridQuery"))
                                  );
            return View(model);
        }


        #endregion

        #region  通用查询4

        /// <summary>
        /// 运行通用查询方法
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult RunGridQuery4(string BaseRunGridViewID = "", string msg = "")
        {
            //1.BaseRunGridViewID  =""  返回 下拉列表
            //2.BaseRunGridViewID !=""  返回 下拉列表 && 二级查询条件 Condition

            //var selMould = new DBContext().Base_RunGridView.Where(u => u.DBType == "1000").ToList().Select(x => new SelectListItem
            var selMould = new DBContext().Base_RunGridView.Where(u => u.Type == "4000").OrderBy(u => u.ViewOrder).ToList().Select(x => new SelectListItem
            {
                Value = x.BaseRunGridViewID.ToString(),
                Text = x.GridViewName
            });

            //默认选中项
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (SelectListItem item in selMould)
            {
                //默认选中
                if (item.Value == Convert.ToString(BaseRunGridViewID))
                    items.Add(new SelectListItem { Text = item.Text, Value = item.Value, Selected = true });
                else
                    items.Add(new SelectListItem { Text = item.Text, Value = item.Value });
            }

            var condition = new RunGridViewModel().ConditionList;

            if (BaseRunGridViewID != "")
            {
                //[Base_RunGridView_Condition]
                condition = RunGridViewConditions.GetList(int.Parse(BaseRunGridViewID));
            }

            //
            RunGridViewModel model = new RunGridViewModel
            {
                BaseRunGridViewID = items,
                ConditionList = condition,
                msg = msg,
                PageModel = new PageModel(15, 1, 1)
            };

            //记录本次访问地址， 用于返回上一层。
            MallUtils.SetAdminRefererCookie(string.Format("{0}",
                                  Url.Action("RunGridQuery"))
                                  );

            return View(model);
        }

        [HttpPost]
        public ActionResult RunGridQuery4(string gridId = "", int outExcel = 0, int pageNumber = 1, int pageSize = 15)
        {
            //1.组织条件.拼接查询字符串
            //2.判断数据库 sql 、oracle 。
            //3.调用对应的查询方法。

            string cmdStr = ""; //  执行语句
            string dbs = ""; // 参数条件{type=0}
            string filter = ""; //  查询条件 

            //回传 阻止查询条件 
            if (gridId == "")
            {
                //跳转
                //返回页面.转向显示  省去为下拉框附值代码
                return RedirectToAction("RunGridQuery4", new { msg = "没有找到查询表格名称!" });
            }
            //获取运行表Model
            var gridModel = BaseRunGridViews.GetModel(TypeHelper.StringToInt(gridId));
            if (gridModel == null)
            {
                //返回页面.转向显示  省去为下拉框附值代码
                return RedirectToAction("RunGridQuery4", new { msg = "没有找到查询表格名称!" });
            }
            cmdStr = gridModel.strSelect;  //执行语句

            //获取条件配置列表
            var conditionList = RunGridViewConditions.GetList(TypeHelper.StringToInt(gridId));
            if (conditionList != null && conditionList.Count > 0)
            {
                #region  整理组织查询条件
                foreach (var conModel in conditionList)
                {
                    //组织查询条件
                    //判断条件定义中 是否为必要条件。
                    string formName = conModel.formName;
                    string txtFormName = "txt" + conModel.formName;
                    string oper = WebHelper.GetFormString(formName);
                    string value = WebHelper.GetFormString(txtFormName);

                    //保存 用户当次查询 操作符、值
                    conModel.currOperator = oper;
                    conModel.currValue = value;

                    if (conModel.inputtcii != null && conModel.inputtcii == 1)
                    {
                        //必要条件
                        if (value == null || value == "" || oper == null || oper == "")
                        {
                            return RedirectToAction("RunGridQuery", new { msg = "请输入必要参数!" });
                        }
                    }
                    // if (oper != null && oper.Length > 0 && value != null && value.Length > 0)
                    if (oper != null && oper.Length > 0)
                    {
                        switch (gridModel.SqlType)
                        {
                            case "Sql":   //命令为查询语句  
                                if (value != null && value.Length > 0)
                                {
                                    if (conModel.type == 0 && conModel.field == "DBS")   // 是否有选择 dbs的 特殊参数 ？
                                    {
                                        dbs = value;
                                    }
                                    else
                                    {
                                        if (gridModel.DBType == "Oracle" && conModel.datetype == "datetime")
                                        {
                                            //Oracle && datetime 
                                            value = "to_date('" + value + "', 'yyyy/mm/dd')";
                                        }
                                        if (conModel.datetype == "varchar" && oper == "like")
                                        {
                                            //like && varchar
                                            value = "'%" + value + "%'";
                                        }
                                        else if (conModel.datetype == "varchar")
                                        {
                                            value = "'" + value + "'";
                                        }
                                        else if (conModel.datetype == "select")
                                        {
                                            if (!TypeHelper.isFloat(value) && !TypeHelper.isNumberic(value))
                                                value = "'" + value + "'";
                                        }
                                        if (filter.Length > 0)
                                        {
                                            filter += " and ";
                                        }
                                        filter += conModel.field + " " + oper + value;
                                    }
                                }
                                break;

                            case "Proc":  // 命令为存储过程
                                if (value != null && value.Length > 0)
                                {
                                    if (!TypeHelper.isFloat(value) && !TypeHelper.isNumberic(value))
                                    {
                                        value = "'" + value + "'";
                                    }
                                }
                                else
                                {
                                    value = "null";
                                }

                                if (filter.Length > 0)
                                {
                                    filter += ",";
                                }
                                filter += value;
                                break;
                        }

                    }
                }

                //有条件 前面加个 and 约定。 proc 不用加
                if (filter != "" && gridModel.SqlType == "Sql")
                {
                    filter = " AND " + filter;
                }

                #endregion
                if (dbs.Length > 0)
                {
                    //带入查询条件 ,有dbs参数的
                    cmdStr = String.Format(cmdStr, dbs, filter);
                }
                else
                {
                    //带入查询条件
                    cmdStr = String.Format(cmdStr, filter);
                }
            }

            string message = "";

            message = "success";
            //run cmdstr 
            DataTable dt = null;

            try
            {

                if (gridModel.DBType == "Oracle")
                {
                    dt = OraQuery.GetCommonQuery(cmdStr);
                }
                else if (gridModel.DBType == "Sql")
                {
                    dt = DBQuery.GetCommonQuery(cmdStr);
                }

            }
            catch (Exception Ex)
            {
                message = "err : " + Ex.Message;
            }


            #region select 
            var selMould = new DBContext().Base_RunGridView.Where(u => u.Type == "4000").OrderBy(u => u.ViewOrder).ToList().Select(x => new SelectListItem
            {
                Value = x.BaseRunGridViewID.ToString(),
                Text = x.GridViewName
            });

            //默认选中项
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (SelectListItem item in selMould)
            {
                //默认选中
                if (item.Value == Convert.ToString(gridId))
                    items.Add(new SelectListItem { Text = item.Text, Value = item.Value, Selected = true });
                else
                    items.Add(new SelectListItem { Text = item.Text, Value = item.Value });
            }
            #endregion

            var condition = new RunGridViewModel().ConditionList;

            //if (gridId != "")
            //{
            //    //[Base_RunGridView_Condition]
            //    condition = RunGridViewConditions.GetList(int.Parse(gridId));
            //    //保存用户当前选择的 操作符、值
            //}

            if (outExcel == 1)
            {
                //导出到Excel
                return RedirectToAction("Export", new { dbType = gridModel.DBType, dbs = dbs, filter = filter, gridId = gridId });

                //return Redirect(referer);
            }


            /////////////////////////// 页面级分页 //////////////////////////////

            //总纪录数
            int totalRecord = 0;
            totalRecord = dt.Rows.Count;
            DataTable dtnew = dt.Clone();
            dtnew.Clear();
            //起始脚标
            int minindex = (pageNumber - 1) * pageSize;
            int maxindex = pageNumber * pageSize;
            if (dt != null)
            {
                //循环用于生成新表
                for (int i = minindex; i < maxindex && i < dt.Rows.Count; i++)
                {
                    //添加新行
                    dtnew.Rows.Add(dt.Rows[i].ItemArray);
                }
            }
            PageModel pageModel = new PageModel(pageSize, pageNumber, totalRecord);


            /////////////////////////////////////////////////////////////////////////////////


            RunGridViewModel model = new RunGridViewModel
            {
                BaseRunGridViewID = items,
                ConditionList = conditionList,
                msg = message,
                dt = dtnew,
                PageModel = pageModel
            };
            //记录本次访问地址， 用于返回上一层。
            MallUtils.SetAdminRefererCookie(string.Format("{0}",
                                  Url.Action("RunGridQuery4"))
                                  );
            return View(model);
        }


        #endregion




        #region 条件一览 

        public ActionResult RunGridViewConditionList()
        {
            return View();
        }

        /// <summary>
        /// 实例化下拉框
        /// </summary>
        /// <returns></returns>
        public ActionResult ddlRungrid()
        {
            string List = BaseRunGridViews.GetJsonList(100);
            return Content(List);
        }

        public ActionResult GetRungrid()
        {
            List<Base_RunGridView> list = BaseRunGridViews.GetList();
            string List = list.ListToJson();
            return Content(List);
        }
        
        /// <summary>
        /// 保存修改
        /// </summary>
        public void SaveChanged(string data)
        {
            String json = Request["data"];

            ArrayList rows = (ArrayList)JSON.Decode(json);

            foreach (Hashtable row in rows)
            {
                //根据记录状态，进行不同的增加、删除、修改操作
                String state = row["_state"] != null ? row["_state"].ToString() : "";

                if (state == "added")
                {
                    //添加
                    addNewRow(row);
                }
                else if (state == "removed" || state == "deleted")
                {
                    //删除
                    int id = Convert.ToInt32(row["id"].ToString());
                    RunGridViewConditions.DeleteModel(id);
                }
                else if (state == "modified")
                {
                    //修改
                    editRow(row);
                }
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="row"></param>
        private void addNewRow(Hashtable row)
        {
            var model = new Base_RunGridView_Condition();

            //用于三元表达式赋空值
            int? Null = null;
            //DateTime? DNUll = null;

            //准备数据
            model.type = TypeHelper.StringToInt(row["type"].ToString());
            model.field = row["field"] == null ? null : row["field"].ToString();
            model.fieldName = row["fieldName"] == null ? null : row["fieldName"].ToString();
            model.formName = row["formName"] == null ? null : row["formName"].ToString();
            model.iOperator = row["iOperator"] == null ? null : row["iOperator"].ToString();
            model.datetype = row["datetype"] == null ? null : row["datetype"].ToString();
            model.editFormat = row["editFormat"] == null ? null : row["editFormat"].ToString();
            model.Remark = row["Remark"] == null ? null : row["Remark"].ToString();
            model.inputtcii = row["inputtcii"] == null ? Null : TypeHelper.StringToInt(row["inputtcii"].ToString());
            model.Base_RunGridView_Id = row["Base_RunGridView_Id"] == null ? Null : TypeHelper.StringToInt(row["Base_RunGridView_Id"].ToString());
            
            //添加
            RunGridViewConditions.AddModel(model);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="row"></param>
        private void editRow(Hashtable row)
        {
            int id = Convert.ToInt32(row["id"].ToString());
            var model = new Base_RunGridView_Condition();

            //用于三元表达式赋空值
            int? Null = null;
            //DateTime? DNUll = null;

            //准备数据
            model.type = TypeHelper.StringToInt(row["type"].ToString());
            model.field = row["field"] == null ? null : row["field"].ToString();
            model.fieldName = row["fieldName"] == null ? null : row["fieldName"].ToString();
            model.formName = row["formName"] == null ? null : row["formName"].ToString();
            model.iOperator = row["iOperator"] == null ? null : row["iOperator"].ToString();
            model.datetype = row["datetype"] == null ? null : row["datetype"].ToString();
            model.editFormat = row["editFormat"] == null ? null : row["editFormat"].ToString();
            model.Remark = row["Remark"] == null ? null : row["Remark"].ToString();
            model.inputtcii = row["inputtcii"] == null ? Null : TypeHelper.StringToInt(row["inputtcii"].ToString());
            model.Base_RunGridView_Id = row["Base_RunGridView_Id"] == null ? Null : TypeHelper.StringToInt(row["Base_RunGridView_Id"].ToString());

            //编辑
            RunGridViewConditions.UpdateModel(model, id);

        }

        /// <summary>
        /// 查询
        /// </summary>
        public ActionResult MyDataBing(string key, string gridid = "", string sortField = "", string sortOrder = "", int pageIndex = 0, int pageSize = 15)
        {
            //string key2 = Request["key"];
            //string status = Request["status"];
            //分页
            if (pageSize <= 0)
            {
                pageSize = 1;
            }
            //脚标从 1 开始
            pageIndex += 1;

            if (sortField == "")
            {
                sortField = " ID ";
            }
            if (sortOrder == "")
            {
                sortOrder = " asc ";
            }
            sortField = sortField + " " + sortOrder;

            //查询条件
            string filter = String.Empty;
            if (gridid != null && gridid != "")
            {
                if (filter.Length > 1)
                {
                    filter += " and ";
                }
                filter = " Base_RunGridView_Id = " + gridid;
            }
            //if (key != null && key != "")
            //{
            //    if (filter.Length > 1)
            //    {
            //        filter += " and ";
            //    }
            //    filter += "( Ecode like '%" + key + "%'  or UserName like '%" + key + "%' )";
            //}

            //返回总页数、总记录数 
            int totalPage;
            int totalRecord;
            //分页查询 
            DataTable dt = new RDBSHelper().GetList("Base_RunGridView_Condition", "*", sortField, filter, pageSize, pageIndex, out totalPage, out totalRecord);
            //页脚Model 

            ArrayList dataAll = dt.DataTable2ArrayList();

            Hashtable result = new Hashtable();
            result["data"] = dataAll;
            result["total"] = totalRecord;

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region 报表一览 


        public ActionResult RunGridViewList()
        {
            return View();
        }

        /// <summary>
        /// 查询
        /// </summary>
        public ActionResult MyDataBingForRunGridViews(string key,  string sortField = "", string sortOrder = "", int pageIndex = 0, int pageSize = 15)
        {
            //分页
            if (pageSize <= 0)
            {
                pageSize = 1;
            }
            //脚标从 1 开始
            pageIndex += 1;

            if (sortField == "")
            {
                sortField = " type,ViewOrder ";
            }
            if (sortOrder == "")
            {
                sortOrder = " asc ";
            }
            sortField = sortField + " " + sortOrder;

            //查询条件
            string filter = String.Empty;

            if (key != null && key != "")
            {
                if (filter.Length > 1)
                {
                    filter += " and ";
                }
                filter += " GridViewName like '%" + key + "%' ";
            }

            //返回总页数、总记录数 
            int totalPage;
            int totalRecord;
            //分页查询 
            DataTable dt = new RDBSHelper().GetList("Base_RunGridView", "*", sortField, filter, pageSize, pageIndex, out totalPage, out totalRecord);
            //页脚Model 

            ArrayList dataAll = dt.DataTable2ArrayList();

            Hashtable result = new Hashtable();
            result["data"] = dataAll;
            result["total"] = totalRecord;

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 保存修改
        /// </summary>
        public void SaveChangedForRunGridViews(string data)
        {
            String json = Request["data"];

            ArrayList rows = (ArrayList)JSON.Decode(json);

            foreach (Hashtable row in rows)
            {
                //根据记录状态，进行不同的增加、删除、修改操作
                String state = row["_state"] != null ? row["_state"].ToString() : "";

                if (state == "added")
                {
                    //添加
                    addNewRowForRunGridViews(row);
                }
                else if (state == "removed" || state == "deleted")
                {
                    //删除
                    int id = Convert.ToInt32(row["BaseRunGridViewID"].ToString());
                    BaseRunGridViews.DeleteModel(id);
                }
                else if (state == "modified")
                {
                    //修改
                    editRowForRunGridViews(row);
                }
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="row"></param>
        private void addNewRowForRunGridViews(Hashtable row)
        {
            var model = new Base_RunGridView();
            //用于三元表达式赋空值
            int? Null = null;
            model.GridViewName = row["GridViewName"] == null ? null : row["GridViewName"].ToString();
            model.strSelect = row["strSelect"] == null ? null : row["strSelect"].ToString();
            model.Type = row["Type"] == null ? null : row["Type"].ToString();
            model.ViewOrder = row["ViewOrder"] == null ? Null : TypeHelper.StringToInt(row["ViewOrder"].ToString());
            model.DBType = row["DBType"] == null ? null : row["DBType"].ToString();
            model.SqlType = row["SqlType"] == null ? null : row["SqlType"].ToString();
            //添加
            BaseRunGridViews.AddModel(model);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="row"></param>
        private void editRowForRunGridViews(Hashtable row)
        {
            int id = Convert.ToInt32(row["BaseRunGridViewID"].ToString());
            var model = new Base_RunGridView();
            //用于三元表达式赋空值
            int? Null = null;
            //准备数据
            model.GridViewName = row["GridViewName"] == null ? null : row["GridViewName"].ToString();
            model.strSelect = row["strSelect"] == null ? null : row["strSelect"].ToString();
            model.Type = row["Type"] == null ? null : row["Type"].ToString();
            model.ViewOrder = row["ViewOrder"] == null ? Null : TypeHelper.StringToInt(row["ViewOrder"].ToString());
            model.DBType = row["DBType"] == null ? null : row["DBType"].ToString();
            model.SqlType = row["SqlType"] == null ? null : row["SqlType"].ToString();
            //编辑
            BaseRunGridViews.UpdateModel(model, id);
        }

        #endregion


        #region 基础资料Base_Qry


        public ActionResult BaseQryList()
        {
            return View();
        }

        /// <summary>
        /// 查询
        /// </summary>
        public ActionResult MyDataBingForBaseQry(string key, string sortField = "", string sortOrder = "", int pageIndex = 0, int pageSize = 15)
        {
            //分页
            if (pageSize <= 0)
            {
                pageSize = 1;
            }
            //脚标从 1 开始
            pageIndex += 1;

            if (sortField == "")
            {
                sortField = " id ";
            }
            if (sortOrder == "")
            {
                sortOrder = " asc ";
            }
            sortField = sortField + " " + sortOrder;

            //查询条件
            string filter = String.Empty;

            if (key != null && key != "")
            {
                if (filter.Length > 1)
                {
                    filter += " and ";
                }
                filter += " viewType like '%" + key + "%' ";
            }

            //返回总页数、总记录数 
            int totalPage;
            int totalRecord;
            //分页查询 
            DataTable dt = new RDBSHelper().GetList("Base_Qry", "*", sortField, filter, pageSize, pageIndex, out totalPage, out totalRecord);
            //页脚Model 

            ArrayList dataAll = dt.DataTable2ArrayList();

            Hashtable result = new Hashtable();
            result["data"] = dataAll;
            result["total"] = totalRecord;

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 保存修改
        /// </summary>
        public void SaveChangedForBaseQry(string data)
        {
            String json = Request["data"];

            ArrayList rows = (ArrayList)JSON.Decode(json);

            foreach (Hashtable row in rows)
            {
                //根据记录状态，进行不同的增加、删除、修改操作
                String state = row["_state"] != null ? row["_state"].ToString() : "";

                if (state == "added")
                {
                    //添加
                    addNewRowForBaseQry(row);
                }
                else if (state == "removed" || state == "deleted")
                {
                    //删除
                    int id = Convert.ToInt32(row["id"].ToString());
                    BaseQrys.DeleteModel(id);
                }
                else if (state == "modified")
                {
                    //修改
                    editRowForBaseQry(row);
                }
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="row"></param>
        private void addNewRowForBaseQry(Hashtable row)
        {
            var model = new  Base_Qry();
            //用于三元表达式赋空值
            int? Null = null;
            model.keyVal = row["keyVal"] == null ? null : row["keyVal"].ToString();
            model.keyText = row["keyText"] == null ? null : row["keyText"].ToString();
            model.viewOrder = row["viewOrder"] == null ? Null :  TypeHelper.StringToInt(row["viewOrder"].ToString());
            model.viewType = row["viewType"] == null ? null : row["viewType"].ToString();
            model.Remark = row["Remark"] == null ? null : row["Remark"].ToString();
            //添加
            BaseQrys.AddModel(model);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="row"></param>
        private void editRowForBaseQry(Hashtable row)
        {
            int id = Convert.ToInt32(row["id"].ToString());
            var model = new Base_Qry();
            //用于三元表达式赋空值
            int? Null = null;
            //准备数据
            model.keyVal = row["keyVal"] == null ? null : row["keyVal"].ToString();
            model.keyText = row["keyText"] == null ? null : row["keyText"].ToString();
            model.viewOrder = row["viewOrder"] == null ? Null : TypeHelper.StringToInt(row["viewOrder"].ToString());
            model.viewType = row["viewType"] == null ? null : row["viewType"].ToString();
            model.Remark = row["Remark"] == null ? null : row["Remark"].ToString();
            //编辑
            BaseQrys.UpdateModel(model, id);
        }

        #endregion


        #region 通用查询Map(带图表2)
        /// <summary>
        /// 运行通用查询方法
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult RunMapQuery(string BaseRunGridViewID = "", string msg = "")
        {
            //1.BaseRunGridViewID  =""  返回 下拉列表
            //2.BaseRunGridViewID !=""  返回 下拉列表 && 二级查询条件 Condition

            //var selMould = new DBContext().Base_RunGridView.Where(u => u.DBType == "1000").ToList().Select(x => new SelectListItem
            var selMould = new DBContext().Base_RunGridView.Where(u => u.Type == "5000").OrderBy(u => u.ViewOrder).ToList().Select(x => new SelectListItem
            {
                Value = x.BaseRunGridViewID.ToString(),
                Text = x.GridViewName
            });

            //默认选中项
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (SelectListItem item in selMould)
            {
                //默认选中
                if (item.Value == Convert.ToString(BaseRunGridViewID))
                    items.Add(new SelectListItem { Text = item.Text, Value = item.Value, Selected = true });
                else
                    items.Add(new SelectListItem { Text = item.Text, Value = item.Value });
            }

            var condition = new RunGridViewModel().ConditionList;

            if (BaseRunGridViewID != "")
            {
                //[Base_RunGridView_Condition]
                condition = RunGridViewConditions.GetList(int.Parse(BaseRunGridViewID));
            }

            //
            var model = new RunMapViewModel
            {
                BaseRunGridViewID = items,
                ConditionList = condition,
                msg = msg,
                PageModel = new PageModel(15, 1, 1),
                dtJson = new DataTable().DataTableToJson(),   //没有值得话 给个 json格式空值
                dimensionJson = new DataTable().DataTableToJsonForDimension(),
                barStrJson = new DataTable().DataTableToJsonForBar(),
                width = "50%",
                height = "30px"
            };

            //记录本次访问地址， 用于返回上一层。
            MallUtils.SetAdminRefererCookie(string.Format("{0}",
                                  Url.Action("RunMapQuery"))
                                  );

            return View(model);
        }

        [HttpPost]
        public ActionResult RunMapQuery(string gridId = "", int outExcel = 0, int pageNumber = 1, int pageSize = 15)
        {
            //1.组织条件.拼接查询字符串
            //2.判断数据库 sql 、oracle 。
            //3.调用对应的查询方法。

            string cmdStr = ""; //  执行语句
            string dbs = ""; // 参数条件{type=0}
            string filter = ""; //  查询条件 

            //回传 阻止查询条件 
            if (gridId == "")
            {
                //跳转
                //返回页面.转向显示  省去为下拉框附值代码
                return RedirectToAction("RunMapQuery", new { msg = "没有找到查询表格名称!" });
            }
            //获取运行表Model
            var gridModel = BaseRunGridViews.GetModel(TypeHelper.StringToInt(gridId));
            if (gridModel == null)
            {
                //返回页面.转向显示  省去为下拉框附值代码
                return RedirectToAction("RunMapQuery", new { msg = "没有找到查询表格名称!" });
            }
            cmdStr = gridModel.strSelect;  //执行语句

            //获取条件配置列表
            var conditionList = RunGridViewConditions.GetList(TypeHelper.StringToInt(gridId));
            if (conditionList != null && conditionList.Count > 0)
            {
                #region  整理组织查询条件
                foreach (var conModel in conditionList)
                {
                    //组织查询条件
                    //判断条件定义中 是否为必要条件。
                    string formName = conModel.formName;
                    string txtFormName = "txt" + conModel.formName;
                    string oper = WebHelper.GetFormString(formName);
                    string value = WebHelper.GetFormString(txtFormName);

                    //保存 用户当次查询 操作符、值
                    conModel.currOperator = oper;
                    conModel.currValue = value;

                    if (conModel.inputtcii != null && conModel.inputtcii == 1)
                    {
                        //必要条件
                        if (value == null || value == "" || oper == null || oper == "")
                        {
                            return RedirectToAction("RunMapQuery", new { msg = "请输入必要参数!" });
                        }
                    }
                    if (oper != null && oper.Length > 0 && value != null && value.Length > 0)
                    {
                        switch (gridModel.SqlType)
                        {
                            case "Sql":   //命令为查询语句  

                                if (conModel.type == 0 && conModel.field == "DBS")   // 是否有选择 dbs的 特殊参数 ？
                                {
                                    dbs = value;
                                }
                                else
                                {
                                    if (gridModel.DBType == "Oracle" && conModel.datetype == "datetime")
                                    {
                                        //Oracle && datetime 
                                        value = "to_date('" + value + "', 'yyyy/mm/dd')";
                                    }
                                    if (conModel.datetype == "varchar" && oper == "like")
                                    {
                                        //like && varchar
                                        value = "'%" + value + "%'";
                                    }
                                    else if (conModel.datetype == "varchar")
                                    {
                                        value = "'" + value + "'";
                                    }
                                    else if (conModel.datetype == "select")
                                    {
                                        if (!TypeHelper.isFloat(value) && !TypeHelper.isNumberic(value))
                                            value = "'" + value + "'";
                                    }
                                    if (filter.Length > 0)
                                    {
                                        filter += " and ";
                                    }
                                    filter += conModel.field + " " + oper + value;
                                }
                                break;

                            case "Proc":  // 命令为存储过程

                                if (!TypeHelper.isFloat(value) && !TypeHelper.isNumberic(value))
                                {
                                    value = "'" + value + "'";
                                }

                                if (filter.Length > 0)
                                {
                                    filter += ",";
                                }
                                filter += value;
                                break;
                        }

                    }
                }

                //有条件 前面加个 and 约定。 proc 不用加
                if (filter != "" && gridModel.SqlType == "Sql")
                {
                    filter = " AND " + filter;
                }

                #endregion
                if (dbs.Length > 0)
                {
                    //带入查询条件 ,有dbs参数的
                    cmdStr = String.Format(cmdStr, dbs, filter);
                }
                else
                {
                    //带入查询条件
                    cmdStr = String.Format(cmdStr, filter);
                }
            }


            string message = "";

            message = "success";
            //run cmdstr 
            DataTable dt = null;
            try
            {
                if (gridModel.DBType == "Oracle")
                {
                    dt = OraQuery.GetCommonQuery(cmdStr);
                }
                else if (gridModel.DBType == "Sql")
                {
                    dt = DBQuery.GetCommonQuery(cmdStr);
                }
            }
            catch (Exception Ex)
            {
                message = "err : " + Ex.Message;
            }

            #region select 
            var selMould = new DBContext().Base_RunGridView.Where(u => u.Type == "5000").OrderBy(u => u.ViewOrder).ToList().Select(x => new SelectListItem
            {
                Value = x.BaseRunGridViewID.ToString(),
                Text = x.GridViewName
            });

            //默认选中项
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (SelectListItem item in selMould)
            {
                //默认选中
                if (item.Value == Convert.ToString(gridId))
                    items.Add(new SelectListItem { Text = item.Text, Value = item.Value, Selected = true });
                else
                    items.Add(new SelectListItem { Text = item.Text, Value = item.Value });
            }
            #endregion

            var condition = new RunGridViewModel().ConditionList;

            //if (gridId != "")
            //{
            //    //[Base_RunGridView_Condition]
            //    condition = RunGridViewConditions.GetList(int.Parse(gridId));
            //    //保存用户当前选择的 操作符、值
            //}

            if (outExcel == 1)
            {
                //导出到Excel
                return RedirectToAction("Export", new { dbType = gridModel.DBType, dbs = dbs, filter = filter, gridId = gridId });

                //return Redirect(referer);
            }

            /////////////////////////// 页面级分页 //////////////////////////////

            //总纪录数
            int totalRecord = 0;
            totalRecord = dt.Rows.Count;
            DataTable dtnew = dt.Clone();
            dtnew.Clear();
            //起始脚标
            int minindex = (pageNumber - 1) * pageSize;
            int maxindex = pageNumber * pageSize;
            if (dt != null)
            {
                //循环用于生成新表
                for (int i = minindex; i < maxindex && i < dt.Rows.Count; i++)
                {
                    //添加新行
                    dtnew.Rows.Add(dt.Rows[i].ItemArray);
                }
            }
            PageModel pageModel = new PageModel(pageSize, pageNumber, totalRecord);

            var model = new RunMapViewModel
            {
                BaseRunGridViewID = items,
                ConditionList = conditionList,
                msg = message,
                dt = dtnew,
                PageModel = pageModel,
                dtJson = dtnew.DataTableToJsonForEchartsPie(), // dtnew.DataTableToJsonForEcharts(),  //转成json传到前台交给js处理柱状图表
              
            };
            //记录本次访问地址， 用于返回上一层。
            MallUtils.SetAdminRefererCookie(string.Format("{0}",
                                  Url.Action("RunMapQuery"))
                                  );
            return View(model);
        }

        #endregion


    }

} 