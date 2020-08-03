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
using MyTiptop.OraCore.Data;
using MyTiptop.OraData;
using System.Transactions;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;


namespace MyTiptop.Web.MallAdmin.Controllers
{
    public class  QcoutlistController : BaseMallAdminController
    {

        public ActionResult QcCheckList()
        {
            return View();
        }


        [HttpGet]
        public ActionResult DCheckList2()
        {
            //初始化ViewModel
            DCheckOutListViewModel model = new DCheckOutListViewModel()
            {
                qcy = new TC_QCYY_FILE(),
                qczlist = new List<TC_QCZZ_FILE>(),
                message = ""
            };

            //记录本次访问地址， 用于返回上一层。
            //MallUtils.SetAdminRefererCookie(string.Format("{0}", Url.Action("CheckList")));

            return View(model);
        }

        [HttpPost]
        public ActionResult DCheckList2(TC_QCYY_FILE req, string _state = "query")
        {
            //1.添加 qcy 、qcz 表。 
            string message = "";
            bool flag = false;
            DCheckOutListViewModel model = new DCheckOutListViewModel()
            {
                qcy = new TC_QCYY_FILE(),
                qczlist = new List<TC_QCZZ_FILE>(),
                message = message
            };
            string qcyy01 = req.TC_QCYY01;
            switch (_state)
            {
                case "query":
                    //重新查询
                    if (Tcqcyys.IsExists(qcyy01))
                    {
                        //单据存在则重新查询
                        flag = true;
                    }
                    else
                    {
                        message = "PN号有误";
                    }
                    break;
                case "add":
                    if (Tcqcyys.IsExists(req.TC_QCYY01))
                    {
                        //已经存在，更新 并 重新显示即可。

                        if (!Tcqcyys.IsChecked(req.TC_QCYY01))  //已经审核不更新
                        {
                            req.TC_QCYY10 = DateTime.Now; //建单时间
                            req.TC_QCYY11 = WorkContext.UserName;  //建单人员
                            req.TC_QCYY12 = 0; //单据状态 开立

                            Tcqcyys.UpdateModel(req, req.TC_QCYY01);
                        }

                        flag = true;
                    }
                    else
                    {
                        List<string> list = new List<string>();
                        list.Add("187132");
                        list.Add("220131");
                        list.Add("206448");
                        list.Add("222660");

                        if (list.Exists(o => o == qcyy01))
                        {
                            req.TC_QCYY01 = QcCheck.getPNOut(qcyy01);

                            if (req.TC_QCYY03 == null)
                            {
                                req.TC_QCYY03 = " "; // PO号
                            }

                            req.TC_QCYY10 = DateTime.Now; //建单时间
                            req.TC_QCYY11 = WorkContext.UserName;  //建单人员
                            req.TC_QCYY12 = 0; //单据状态 开立

                            QcCheck.addQcOutCheckHead(req, qcyy01);
                            message = "添加成功";
                            flag = true;
                        }
                        else
                        {
                            message = "PN输入错误";
                        }
                    }
                    break;
            }

            //重新查询显示
            if (flag)
            {
                //准备显示页面。
                TC_QCYY_FILE qcyModel = Tcqcyys.GetModel(req.TC_QCYY01);
                List<TC_QCZZ_FILE> qcxList = Tcqczzs.GetList(req.TC_QCYY01);
                //初始化ViewModel
                model = new DCheckOutListViewModel()
                {
                    qcy = qcyModel,
                    qczlist = qcxList,
                    message = message
                };
            }
            model.message = message;
            return View(model);

        }

        //[HttpPost]
        //public ActionResult DCheckList(TC_QCY_FILE req, string _state)
        public ActionResult DCheckList(string TC_QCYY01 = "", string _state = "query")
        {
            //1.添加 qcy 、qcz 表。 
            string message = "";
            bool flag = false;
            DCheckOutListViewModel model = new DCheckOutListViewModel()
            {
                qcy = new TC_QCYY_FILE(),
                qczlist = new List<TC_QCZZ_FILE>(),
                message = message
            };
            string qcy01 = TC_QCYY01;
            switch (_state)
            {
                case "query":
                    //重新查询
                    if (Tcqcyys.IsExists(qcy01))
                    {
                        //单据存在则重新查询
                        flag = true;
                    }
                    else if (qcy01 != "")
                    {
                        message = "PN号有误";
                    }
                    else
                    {
                        message = "";
                    }
                    break;
            }

            //重新查询显示
            if (flag)
            {
                //准备显示页面。
                TC_QCYY_FILE qcyModel = Tcqcyys.GetModel(qcy01);
                List<TC_QCZZ_FILE> qcxList = Tcqczzs.GetList(qcy01);
                //初始化ViewModel
                model = new DCheckOutListViewModel()
                {
                    qcy = qcyModel,
                    qczlist = qcxList,
                    message = message
                };
            }
            model.message = message;
            return View(model);

        }


        /// <summary>
        /// 一览表
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ActionResult QcyList(TC_QCYY_FILE qcy, int pageNumber = 1, int pageSize = 5)
        {
            if (qcy == null)
            {
                qcy = new TC_QCYY_FILE();
            }
            if (pageSize <= 0)
                //防止除0操作
                pageSize = 1;
            //查询条件
            string filter = String.Empty;
            filter = "where 1=1";

            if (qcy.TC_QCYY01 != null && qcy.TC_QCYY01 != "")
            {
                if (filter.Length > 1) { filter += " and "; }
                filter += " TC_QCYY01  like '%" + qcy.TC_QCYY01 + "%'";
            }

            //返回总页数、总记录数
            int totalRecord;
            //分页查询
            List<TC_QCYY_FILE> QcyList = new OraRDBSHelper().ExecutePaging<TC_QCYY_FILE>("TC_QCYY_FILE", "tc_qcyy01", filter, pageSize, pageNumber, out totalRecord);
            //页脚Model
            PageModel pageModel = new PageModel(pageSize, pageNumber, totalRecord);
            //初始化ViewModel
            QcyyOutListViewModel model = new QcyyOutListViewModel()
            {
                PageModel = pageModel,
                qcylist = QcyList,
                message = "",
                qcy = qcy
            };
            //记录本次访问地址， 用于返回上一层。
            MallUtils.SetAdminRefererCookie(string.Format("{0}?pageNumber={1}&pageSize={2}&TC_QCYY01={3}",
                      Url.Action("QcyList"), pageModel.PageNumber, pageModel.PageSize, qcy.TC_QCYY01));
            //返回View
            return View(model);
        }


        /// <summary>
        /// 删除检查项目
        /// </summary>
        /// <param name="mouldId"></param>
        /// <returns></returns>
        public ActionResult deleteRowAjax(string qcz01="" ,int qcz05 = 0)
        {
            string status = "0";
            if (!Tcqcys.IsChecked(qcz01))
            {
                if (Tcqczs.IsExists(qcz01, qcz05))
                {
                    Tcqczs.DeleteModel(qcz01, qcz05);
                    //存在
                    status = "1";
                }
            }
            else
            {
                status = "2";  //已经审核，不能修改
            }
            return AjaxResult("success", status, true);
        }

        public ActionResult addQczzValAjax(string qczz01 = "", string val = "", int qczz06 = 0,int type =1)
        {
            string status = "0";
            if (!Tcqcyys.IsChecked(qczz01))
            {
                var qczzModel = Tcqczzs.GetModel(qczz01, qczz06);
                if (qczzModel != null)
                {

                    //更新
                    if (type == 1)
                        qczzModel.TC_QCZZ03 = val;
                    else if (type == 2)
                        qczzModel.TC_QCZZ04 = val;
                    else if (type == 3)
                        qczzModel.TC_QCZZ05 = val;

                    Tcqczzs.UpdateModel(qczzModel, qczz01, qczz06);
                    status = "1";
                }
            }
            else
            {
                status = "2";//已经审核，不能修改
            }
            return AjaxResult("success", status, true);
        }

        public ActionResult queryandDeleteQcyStatusAjax(string qcy01 = "")
        {
            string status = "0";
            var qcyModel = Tcqcys.GetModel(qcy01);
            if (qcyModel != null)
            {
                //更新
                if (qcyModel.TC_QCY12 != null && qcyModel.TC_QCY12 == 2)
                {
                    //已审核，不能删除 ，返回2
                    status = "2";
                }
                else
                {
                    //成功删除。返回1
                    if (QcCheck.deleteQcy(qcy01))
                    {
                        status = "1";
                    }
                }
            }
            return AjaxResult("success", status, true);
        }

        public ActionResult deleteQcyAjax(string qcy01 = "")
        {
            string status = "0";
            if (QcCheck.deleteQcy(qcy01))
            {
                status = "1";
            }
            return AjaxResult("success", status, true);
        }


  
        /// <summary>
        /// 审核qcy
        /// </summary>
        /// <param name="qcy01"></param>
        /// <returns></returns>
        public ActionResult checkQcyAjax(string qcy01 = "")
        {
            string status = "0";
            if (Tcqczs.IsExistsNotChecked(qcy01))
            { 
                //有未检查项 2 
                status = "2";
            }
            else
            {
                //更新
                TC_QCY_FILE model = Tcqcys.GetModel(qcy01);
                if (model != null)
                {
                    //0:开立 1：填表 2：审核
                    model.TC_QCY12 = 2;
                    model.TC_QCY13 = WorkContext.UserName;
                    model.TC_QCY14 = DateTime.Now;

                    Tcqcys.UpdateModel(model, qcy01);
                    status = "1";
                }
            }
            return AjaxResult("success", status, true);
        }

        #region  qclist基础列表
        /// <summary>
        /// 查询
        /// </summary>
        public ActionResult MyDataBing(string key, string sortField = "", string sortOrder = "", int pageIndex = 0, int pageSize = 20)
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
                sortField = " tc_qcxx01 ";
            }
            if (sortOrder == "")
            {
                sortOrder = " asc ";
            }
            sortField = sortField + " " + sortOrder;

            //查询条件
            string filter = String.Empty;

            filter = " where 1=1 ";

            if (key != null && key != "")
            {
                if (filter.Length > 1)
                {
                    filter += " and ";
                }
                filter += " tc_qcxx02 like '%" + key + "%' or tc_qcxx06 = '" + key + "' ";
            }

            //返回总页数、总记录数 
            //int totalPage;
            int totalRecord;
            //分页查询 

            var dt = new OraRDBSHelper().ExecutePaging<TC_QCXX_FILE>("TC_QCXX_FILE", sortField, filter, pageSize, pageIndex,  out totalRecord);
            //页脚Model 

            Hashtable result = new Hashtable();
            result["data"] = dt;
            result["total"] = totalRecord;

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 保存修改
        /// </summary>
        /// 允许html脚本 （危险字符）
        [ValidateInput(false)]
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
                    int sid = Convert.ToInt32(row["TC_QCXX01"].ToString());
                    Tcqcxxs.DeleteModel(sid);
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
            var model = new TC_QCXX_FILE();
             
            model.TC_QCXX01 = Convert.ToInt32(row["TC_QCXX01"].ToString());
            model.TC_QCXX02 = row["TC_QCXX02"] == null ? null : row["TC_QCXX02"].ToString();
            model.TC_QCXX03 = TypeHelper.StringToInt(row["TC_QCXX03"].ToString());
            model.TC_QCXX04 = row["TC_QCXX04"] == null ? 0 : TypeHelper.StringToInt(row["TC_QCXX04"].ToString());
            model.TC_QCXX05 = 1;
            model.TC_QCXX06 = row["TC_QCXX06"] == null ? null : row["TC_QCXX06"].ToString();
            model.TC_QCXX07 = row["TC_QCXX07"] == null ? null : row["TC_QCXX07"].ToString();
            model.TC_QCXX08 = row["TC_QCXX08"] == null ? 0 : TypeHelper.StringToInt(row["TC_QCXX08"].ToString());
            model.TC_QCXX09 = row["TC_QCXX09"] == null ? null : row["TC_QCXX09"].ToString();
             
            Tcqcxxs.AddModel(model);
        } 

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="row"></param>
        private void editRow(Hashtable row)
        {
            int sid = Convert.ToInt32(row["TC_QCXX01"].ToString());
            var model = new TC_QCXX_FILE();

            model.TC_QCXX02 = row["TC_QCXX02"] == null ? null : row["TC_QCXX02"].ToString();
            model.TC_QCXX03 = TypeHelper.StringToInt(row["TC_QCXX03"].ToString());
            model.TC_QCXX04 = row["TC_QCXX04"] == null ? 0 : TypeHelper.StringToInt(row["TC_QCXX04"].ToString());
            model.TC_QCXX06 = row["TC_QCXX06"] == null ? null : row["TC_QCXX06"].ToString();
            model.TC_QCXX05 = 1;
            model.TC_QCXX07 = row["TC_QCXX07"] == null ? null : row["TC_QCXX07"].ToString();
            model.TC_QCXX08 = row["TC_QCXX08"] == null ? 0 : TypeHelper.StringToInt(row["TC_QCXX08"].ToString());
            model.TC_QCXX09 = row["TC_QCXX09"] == null ? null : row["TC_QCXX09"].ToString();

            Tcqcxxs.UpdateModel(model, sid);
        }


        /// <summary>
        /// 是否已经存在
        /// </summary>
        /// <param name="mouldId"></param>
        /// <returns></returns>
        public ActionResult IsExistsCheckListAjax(int sid = 0)
        {
            string status = "0";
            if (Tcqcxxs.IsExists(sid))
            {
                //存在
                status = "1";
            }
            return AjaxResult("success", status, true);
        }



        #endregion 



    }
}   
