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
    public class  QclistController : BaseMallAdminController
    {
        [HttpGet]
        public ActionResult CheckList()
        {
            //初始化ViewModel
            CheckListViewModel model = new CheckListViewModel()
            {
                qcy = new TC_QCY_FILE(),
                qczlist = new List<TC_QCZ_FILE>()
            };

            //记录本次访问地址， 用于返回上一层。
            //MallUtils.SetAdminRefererCookie(string.Format("{0}", Url.Action("CheckList")));

            return View(model);
        }

        [HttpPost]
        public ActionResult CheckList(TC_QCY_FILE req,string _state)
        {
            //1.添加 qcy 、qcz 表。 

            string message = "";

            bool flag = false;

            CheckListViewModel model = new CheckListViewModel()
            {
                qcy = new TC_QCY_FILE(),
                qczlist = new List<TC_QCZ_FILE>(),
                message = message
            };
            string qcy01 = req.TC_QCY01;
            switch (_state) 
            {
                case "query":
                    //重新查询
                    if (Tcqcys.IsExists(req.TC_QCY01))
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
                    if (Tcqcys.IsExists(req.TC_QCY01)) 
                    {
                        //已经存在，更新 并 重新显示即可。

                        if (!Tcqcys.IsChecked(req.TC_QCY01))  //已经审核不更新
                        {
                            req.TC_QCY10 = DateTime.Now; //建单时间
                            req.TC_QCY11 = WorkContext.UserName;  //建单人员
                            req.TC_QCY12 = 0; //单据状态 开立

                            Tcqcys.UpdateModel(req, req.TC_QCY01);
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

                        if (list.Exists(o => o == qcy01))
                        {
                            req.TC_QCY01 = QcCheck.getPN(qcy01);

                            req.TC_QCY10 = DateTime.Now; //建单时间
                            req.TC_QCY11 = WorkContext.UserName;  //建单人员
                            req.TC_QCY12 = 0; //单据状态 开立

                            QcCheck.addQcCheckHead(req, qcy01);
                            message = "添加成功";
                            flag = true;
                        }
                        else
                        {
                            message = "PN输入错误";
                        }
                    }
                    break; 
                case "reload": 
                    //删除 qcz 资料，重新生成 qcz列表
                    if (qcy01 !=null && qcy01.Length > 0 && qcy01.IndexOf('-') > 0)
                    {
                        QcCheck.reloadqcz(req.TC_QCY01);
                        message = "重置检查项目列表成功";

                        flag = true;
                    }
                    else
                    {
                        message = "单号为空";
                    }
                    break; 
            }

            //重新查询显示
            if (flag)
            {
                //准备显示页面。
                TC_QCY_FILE qcyModel = Tcqcys.GetModel(req.TC_QCY01);

                List<TC_QCZ_FILE> qcxList = Tcqczs.GetList(req.TC_QCY01);

                //初始化ViewModel
                model = new CheckListViewModel()
                {
                    qcy = qcyModel,
                    qczlist = qcxList,
                    message = message
                };
            }
            model.message = message;

            //处理角标的时候，行号 - 父节点行号  就是显示号码


            return View(model); 
        }

        [HttpGet]
        public ActionResult DCheckList2()
        {
            //初始化ViewModel
            DCheckListViewModel model = new DCheckListViewModel()
            {
                qcy = new TC_QCY_FILE(),
                qczlist = new List<TC_QCZ_FILE>(),
                message=""
            };

            //记录本次访问地址， 用于返回上一层。
            //MallUtils.SetAdminRefererCookie(string.Format("{0}", Url.Action("CheckList")));

            return View(model);
        }
        [HttpPost]
        public ActionResult DCheckList2(TC_QCY_FILE req, string _state = "query")
        {
            //1.添加 qcy 、qcz 表。 
            string message = "";
            bool flag = false;
            DCheckListViewModel model = new DCheckListViewModel()
            {
                qcy = new TC_QCY_FILE(),
                qczlist = new List<TC_QCZ_FILE>(),
                message = message
            };
            string qcy01 = req.TC_QCY01;
            switch (_state)
            {
                case "query":
                    //重新查询
                    if (Tcqcys.IsExists(qcy01))
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
                    if (Tcqcys.IsExists(req.TC_QCY01))
                    {
                        //已经存在，更新 并 重新显示即可。

                        if (!Tcqcys.IsChecked(req.TC_QCY01))  //已经审核不更新
                        {
                            req.TC_QCY10 = DateTime.Now; //建单时间
                            req.TC_QCY11 = WorkContext.UserName;  //建单人员
                            req.TC_QCY12 = 0; //单据状态 开立

                            Tcqcys.UpdateModel(req, req.TC_QCY01);
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

                        if (list.Exists(o => o == qcy01))
                        {
                            req.TC_QCY01 = QcCheck.getPN(qcy01);

                            req.TC_QCY10 = DateTime.Now; //建单时间
                            req.TC_QCY11 = WorkContext.UserName;  //建单人员
                            req.TC_QCY12 = 0; //单据状态 开立

                            QcCheck.addQcCheckHead(req, qcy01);
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
                TC_QCY_FILE qcyModel = Tcqcys.GetModel(req.TC_QCY01);
                List<TC_QCZ_FILE> qcxList = Tcqczs.GetList(req.TC_QCY01);
                //初始化ViewModel
                model = new DCheckListViewModel()
                {
                    qcy = qcyModel,
                    qczlist = qcxList,
                    message = message
                };
            }
            model.message = message;
            return View(model);

        }


        //[HttpGet]
        //public ActionResult DCheckList()
        //{
        //    //初始化ViewModel
        //    DCheckListViewModel model = new DCheckListViewModel()
        //    {
        //        qcy = new TC_QCY_FILE(),
        //        qczlist = new List<TC_QCZ_FILE>()
        //    };

        //    return View(model);
        //}

        //[HttpPost]
        //public ActionResult DCheckList(TC_QCY_FILE req, string _state)
        public ActionResult DCheckList(string TC_QCY01 = "", string _state = "query")
        {
            //1.添加 qcy 、qcz 表。 
            string message = "";
            bool flag = false;
            DCheckListViewModel model = new DCheckListViewModel()
            {
                qcy = new TC_QCY_FILE(),
                qczlist = new List<TC_QCZ_FILE>(),
                message = message
            };
            string qcy01 = TC_QCY01;
            switch (_state)
            {
                case "query":
                    //重新查询
                    if (Tcqcys.IsExists(qcy01))
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
                TC_QCY_FILE qcyModel = Tcqcys.GetModel(qcy01);
                List<TC_QCZ_FILE> qcxList = Tcqczs.GetList(qcy01);
                //初始化ViewModel
                model = new DCheckListViewModel()
                {
                    qcy = qcyModel,
                    qczlist = qcxList,
                    message = message
                };
            }
            model.message = message;
            return View(model);

        }

        //[HttpGet]
        public ActionResult DCheckQcy(string _state,string TC_QCY01 ="")
        {
            string message = "";
            //初始化ViewModel
            DCheckListViewModel model = new DCheckListViewModel()
            {
                qcy = new TC_QCY_FILE(),
                qczlist = new List<TC_QCZ_FILE>(),
                message = message
            };

            string qcy01 = TC_QCY01;

            if (TC_QCY01 != "")
            {
                switch (_state)
                {
                    case "checked":
                        if (Tcqczs.IsExistsNotChecked(qcy01))
                        {
                            //有未检查项 2 
                            message = "有未检查项";
                        }
                        else
                        {
                            if (Tcqcys.IsChecked(qcy01))
                            {
                                //有未检查项 2 
                                message = "单据已经审核,不要重复点。";
                            }
                            else
                            {
                                //更新
                                TC_QCY_FILE m = Tcqcys.GetModel(qcy01);
                                if (model != null)
                                {
                                    //0:开立 1：填表 2：审核
                                    m.TC_QCY12 = 2;
                                    m.TC_QCY13 = WorkContext.UserName;
                                    m.TC_QCY14 = DateTime.Now;

                                    Tcqcys.UpdateModel(m, qcy01);
                                    message = "审核成功";
                                }
                            }
                        }
                        break;

                    case "unchecked":
                        if (!Tcqcys.IsChecked(qcy01))
                        {
                            //有未检查项 2 
                            message = "单据未审核";
                        }
                        else
                        {
                            //更新
                            TC_QCY_FILE m = Tcqcys.GetModel(qcy01);
                            if (model != null)
                            {
                                //0:开立 1：填表 2：审核
                                m.TC_QCY12 = 0;
                                m.TC_QCY13 = WorkContext.UserName;
                                m.TC_QCY14 = DateTime.Now;

                                Tcqcys.UpdateModel(m, qcy01);
                                message = "审核成功";
                            }
                        }
                        break;
                }

                //准备显示页面。
                TC_QCY_FILE qcyModel = Tcqcys.GetModel(TC_QCY01);
                 
                List<TC_QCZ_FILE> qcxList = Tcqczs.GetList(TC_QCY01);
                 
                //初始化ViewModel
                model = new DCheckListViewModel()
                {
                    qcy = qcyModel,
                    qczlist = qcxList,
                    message = message
                };
            }

            //返回上一级目录
            ViewBag.referer = MallUtils.GetMallAdminRefererCookie();
            return View(model);
        }


        public ActionResult QcCheckList()
        {
            return View();
        }

        /// <summary>
        /// 一览表
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ActionResult QcyList(TC_QCY_FILE qcy, int pageNumber = 1, int pageSize = 5)
        {
            if (qcy == null)
            {
                qcy = new TC_QCY_FILE();
            }
            if (pageSize <= 0)
                //防止除0操作
                pageSize = 1;
            //查询条件
            string filter = String.Empty;
            filter = "where 1=1";

            if (qcy.TC_QCY01 != null && qcy.TC_QCY01 != "")
            {
                if (filter.Length > 1) { filter += " and "; }
                filter += " TC_QCY01  like '%" + qcy.TC_QCY01 + "%'";
            }

            //返回总页数、总记录数
            int totalRecord;
            //分页查询
            List<TC_QCY_FILE> QcyList = new OraRDBSHelper().ExecutePaging<TC_QCY_FILE>("TC_QCY_FILE", "tc_qcy01", filter, pageSize, pageNumber, out totalRecord);
            //页脚Model
            PageModel pageModel = new PageModel(pageSize, pageNumber, totalRecord);
            //初始化ViewModel
            QcyListViewModel model = new QcyListViewModel()
            {
                PageModel = pageModel,
                qcylist = QcyList,
                message = "",
                qcy = qcy
            };
            //记录本次访问地址， 用于返回上一层。
            MallUtils.SetAdminRefererCookie(string.Format("{0}?pageNumber={1}&pageSize={2}&TC_QCY01={3}",
                      Url.Action("QcyList"), pageModel.PageNumber, pageModel.PageSize,qcy.TC_QCY01));
            //返回View
            return View(model);
        }


        /// <summary>
        /// 一览表,无审核，删除权限
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ActionResult QcyList2(TC_QCY_FILE qcy, int pageNumber = 1, int pageSize = 5)
        {
            if (qcy == null)
            {
                qcy = new TC_QCY_FILE();
            }
            if (pageSize <= 0)
                //防止除0操作
                pageSize = 1;
            //查询条件
            string filter = String.Empty;
            filter = "where 1=1";

            if (qcy.TC_QCY01 != null && qcy.TC_QCY01 != "")
            {
                if (filter.Length > 1) { filter += " and "; }
                filter += " TC_QCY01  like '%" + qcy.TC_QCY01 + "%'";
            }

            //返回总页数、总记录数
            int totalRecord;
            //分页查询
            List<TC_QCY_FILE> QcyList = new OraRDBSHelper().ExecutePaging<TC_QCY_FILE>("TC_QCY_FILE", "tc_qcy01", filter, pageSize, pageNumber, out totalRecord);
            //页脚Model
            PageModel pageModel = new PageModel(pageSize, pageNumber, totalRecord);
            //初始化ViewModel
            QcyListViewModel model = new QcyListViewModel()
            {
                PageModel = pageModel,
                qcylist = QcyList,
                message = "",
                qcy = qcy
            };
            //记录本次访问地址， 用于返回上一层。
            MallUtils.SetAdminRefererCookie(string.Format("{0}?pageNumber={1}&pageSize={2}&TC_QCY01={3}",
                      Url.Action("QcyList2"), pageModel.PageNumber, pageModel.PageSize, qcy.TC_QCY01));
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

        /// <summary>
        /// 检查是否可以选择（初检）
        /// </summary>
        /// <param name="qcz01"></param>
        /// <param name="qcz05"></param>
        /// <returns></returns>
        public ActionResult checkingAjax(string qcz01 = "", int qcz05 = 0)
        {
            string status = "0";
            var qczModelPre = Tcqczs.GetModelPre(qcz01, qcz05);
            if (qczModelPre == null)
            {
                //不存在前一项
                status = "1";
            }
            else if (qczModelPre != null && qczModelPre.TC_QCZ03 != null)
            {
                //存在并且有值
                status = "1";
            }

            return AjaxResult("success", status, true);
        }

        public ActionResult updateQcz03Ajax(string qcz01 = "",string qcz03 ="", int qcz05 = 0)
        {
            string status = "0";

            if (!Tcqcys.IsChecked(qcz01))
            {


                var qczModel = Tcqczs.GetModel(qcz01, qcz05);
                if (qczModel != null)
                {
                    //更新
                    qczModel.TC_QCZ03 = qcz03;

                    Tcqczs.UpdateModel(qczModel, qcz01, qcz05);
                    status = "1";
                }
            }
            else
            {
                status = "2";  //已经审核，不能修改
            }

            return AjaxResult("success", status, true);
        }

        public ActionResult addQcz04Ajax(string qcz01 = "", string qcz04 = "", int qcz05 = 0)
        {
            string status = "0";
            if (!Tcqcys.IsChecked(qcz01))
            {
                var qczModel = Tcqczs.GetModel(qcz01, qcz05);
                if (qczModel != null)
                {
                    //更新
                    qczModel.TC_QCZ04 = qcz04;
                    Tcqczs.UpdateModel(qczModel, qcz01, qcz05);
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
        /// 检查是否可以选择（复检）
        /// </summary>
        /// <param name="qcz01"></param>
        /// <param name="qcz05"></param>
        /// <returns></returns>
        public ActionResult ccheckingAjax(string qcz01 = "", int qcz05 = 0)
        {
            string status = "0";
            var qczModelPre = Tcqczs.GetModelPre(qcz01, qcz05);
            if (qczModelPre == null)
            {
                //不存在前一项
                status = "1";
            }
            else if (qczModelPre != null && qczModelPre.TC_QCZ08 != null)  // 复检 TC_QCZ08
            {
                //存在并且有值
                status = "1";
            }

            return AjaxResult("success", status, true);
        }

        public ActionResult updateQcz08Ajax(string qcz01 = "", string qcz08 = "", int qcz05 = 0)
        {
            string status = "0";

            if (!Tcqcys.IsChecked(qcz01))
            {
                var qczModel = Tcqczs.GetModel(qcz01, qcz05);
                if (qczModel != null)
                {
                    //更新
                    qczModel.TC_QCZ08 = qcz08;

                    Tcqczs.UpdateModel(qczModel, qcz01, qcz05);
                    status = "1";
                }
            }
            else
            {
                status = "2";  //已经审核，不能修改
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
                sortField = " tc_qcx01 ";
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
                filter += " tc_qcx02 like '%" + key + "%' or tc_qcx06 = '" + key + "' ";
            }

            //返回总页数、总记录数 
            //int totalPage;
            int totalRecord;
            //分页查询 

            var dt = new OraRDBSHelper().ExecutePaging<TC_QCX_FILE>("TC_QCX_FILE", sortField, filter, pageSize, pageIndex,  out totalRecord);
            //页脚Model 

            Hashtable result = new Hashtable();
            result["data"] = dt;
            result["total"] = totalRecord;

            return Json(result, JsonRequestBehavior.AllowGet);
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
                    int sid = Convert.ToInt32(row["TC_QCX01"].ToString());
                    Tcqcxs.DeleteModel(sid);
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
            var model = new TC_QCX_FILE();

            model.TC_QCX01 = Convert.ToInt32(row["TC_QCX01"].ToString());
            model.TC_QCX02 = row["TC_QCX02"] == null ? null : row["TC_QCX02"].ToString();
            model.TC_QCX03 = TypeHelper.StringToInt(row["TC_QCX03"].ToString());
            model.TC_QCX04 = row["TC_QCX04"] == null ? 0 : TypeHelper.StringToInt(row["TC_QCX04"].ToString());
            model.TC_QCX05 = 0;
            model.TC_QCX06 = row["TC_QCX06"] == null ? null : row["TC_QCX06"].ToString();
            model.TC_QCX07 = row["TC_QCX07"] == null ? null : row["TC_QCX07"].ToString();

            Tcqcxs.AddModel(model);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="row"></param>
        private void editRow(Hashtable row)
        {
            int sid = Convert.ToInt32(row["TC_QCX01"].ToString());
            var model = new TC_QCX_FILE();

            model.TC_QCX02 = row["TC_QCX02"] == null ? null : row["TC_QCX02"].ToString();
            model.TC_QCX03 = TypeHelper.StringToInt(row["TC_QCX03"].ToString());
            //model.TC_QCX04 = TypeHelper.StringToInt(row["TC_QCX04"].ToString());
            model.TC_QCX04 = row["TC_QCX04"] == null ? 0 : TypeHelper.StringToInt(row["TC_QCX04"].ToString());
            model.TC_QCX06 = row["TC_QCX06"] == null ? null : row["TC_QCX06"].ToString();
            model.TC_QCX05 = 0;
            model.TC_QCX07 = row["TC_QCX07"] == null ? null : row["TC_QCX07"].ToString();

            Tcqcxs.UpdateModel(model, sid);
        }


        /// <summary>
        /// 是否已经存在
        /// </summary>
        /// <param name="mouldId"></param>
        /// <returns></returns>
        public ActionResult IsExistsCheckListAjax(int sid = 0)
        {
            string status = "0";
            if (Tcqcxs.IsExists(sid))
            {
                //存在
                status = "1";
            }
            return AjaxResult("success", status, true);
        }



        #endregion 

        ////开启事务管理1.添加记录。2.更新模具使用次数信息 。
        //using (TransactionScope sc = new TransactionScope())
        //{
        //    try
        //    {
        //        //添加 
        //        Emfds.AddModel(model);
        //        //更新mould基础表的已经使用 usedCount信息
        //        bmoulds.UpdateModel(bModel, model.mouldId);
        //        //事务提交
        //        sc.Complete();
        //    }
        //    catch(Exception Ex)
        //    {
        //        throw Ex;
        //        //return PromptView("错误，请检查!Error:" + Ex.Message);
        //    }
        //}

    }
}   
