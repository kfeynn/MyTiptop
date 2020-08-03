using System;
using System.Collections;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyTiptop.Web.Framework;
using MyTiptop.Web.Models;
using MyTiptop.Services;
using MyTiptop.Data;
using MyTiptop.Core;
using MyTiptop.SupplierData;
using MyTiptop.OraData;
using System.Transactions;
using System.IO;

namespace MyTiptop.Web.Controllers
{
    public class  SupplierController : BaseWebController
    {
        

        public ActionResult PnList(PN pn, PNSUB pnSub, int pageNumber = 1, int pageSize = 10, int outExcel = 0)
        {
            if (pn == null) { pn = new PN(); }
            if (pageSize <= 0)
                //防止除0操作
                pageSize = 1;
            //查询条件
            string filter = String.Empty;
            filter = "where 1=1";

            if (pn.DNNUM != null && pn.DNNUM != "")
            {
                if (filter.Length > 1) { filter += " and "; }
                filter += " DNNUM  like '%" + pn.DNNUM + "%'";
            }
            if (pn.SUPID != null && pn.SUPID != "")
            {
                if (filter.Length > 1) { filter += " and "; }
                filter += " SUPID  = '" + pn.SUPID + "'";
            }
            if (pn.NAME != null && pn.NAME != "")
            {
                if (filter.Length > 1) { filter += " and "; }
                filter += " NAME  like '%" + pn.NAME + "%'";
            }
            if (pn.PLANT != null && pn.PLANT != "")
            {
                if (filter.Length > 1) { filter += " and "; }
                filter += " PLANT  like '%" + pn.PLANT + "%'";
            }
            if (pn.PMN33 != null && pn.PMN33 != "")
            {
                if (filter.Length > 1) { filter += " and "; }
                filter += " PMN33  = '" + pn.PMN33 + "'";
            }
            if (pnSub.PMM01 != null && pnSub.PMM01 != "")
            {
                if (filter.Length > 1) { filter += " and "; }
                filter += "  dnnum in (select sdnnum from pnsub where pmm01 like '%" + pnSub.PMM01 + "%' ) ";

                //select* from pn where dnnum in (select sdnnum from pnsub where pmm01 like '%221-S341808220010%' )
            }
            if (pnSub.PMN04 != null && pnSub.PMN04 != "")
            {
                if (filter.Length > 1) { filter += " and "; }
                filter += "  dnnum in (select sdnnum from pnsub where pmn04 like '%" + pnSub.PMN04 + "%' ) ";              
            }



            //返回总页数、总记录数
            int totalRecord;
            //分页查询
            List<PN> pnList = new OraRDBSHelper().ExecutePaging<PN>("PN", " pmn33 desc,dnnum asc ", filter, pageSize, pageNumber, out totalRecord);


            //子表数据

            string subFilter = "";

            foreach (PN p in pnList)
            {
                if (subFilter.Length > 1)
                {
                    subFilter += ",";
                }
                subFilter += "'" + p.DNNUM + "'";
            }

            if (subFilter.Length > 1)
            {
                subFilter = " sdnnum in (" + subFilter + ") ";
            }

            List<PNSUB> pnSubList = new List<PNSUB>();


            if (subFilter != "")
            {
                string cmdStr = "select * from pnsub where 1=1 and  " + subFilter;
                pnSubList = OraRDBSHelper.ExecuateSql<PNSUB>(cmdStr);
            }

            if (outExcel == 1)
            {
                this.TempData["pnSubList"] = pnSubList;
                //导出到Excel
                //return RedirectToAction("Export", new { pnSubList = pnSubList });
                return RedirectToAction("Export");

            }


            //页脚Model
            PageModel pageModel = new PageModel(pageSize, pageNumber, totalRecord);
            //初始化ViewModel
            PnListViewModel model = new PnListViewModel()
            {
                PageModel = pageModel,
                pn = pn,
                pnSub = pnSub,
                pnList = pnList,
                pnSubList = pnSubList,
                message = ""
            };
            //记录本次访问地址， 用于返回上一层。
            MallUtils.SetAdminRefererCookie(string.Format("{0}?pageNumber={1}&pageSize={2}&TC_QCY01={3}",
                      Url.Action("PnList"), pageModel.PageNumber, pageModel.PageSize, pn.DNNUM));
            //返回View
            return View(model);
        }




        /// <summary>
        /// 导出
        /// </summary>
        /// <returns></returns>
        /// 允许 危险的参数 带 sql关键字
        //[ValidateInput(false)]
        public FileResult Export()
        {
            List<PNSUB> pnSubList = this.TempData["pnSubList"] as List<PNSUB>;

            //创建Excel文件的对象
            NPOI.XSSF.UserModel.XSSFWorkbook book = new NPOI.XSSF.UserModel.XSSFWorkbook();     // XLSX
            //添加一个sheet
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");

            //给sheet1添加第一行的头部标题
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);

            row1.CreateCell(0).SetCellValue("送货单号");
            row1.CreateCell(1).SetCellValue("订单号");
            row1.CreateCell(2).SetCellValue("项次");
            row1.CreateCell(3).SetCellValue("料号");
            row1.CreateCell(4).SetCellValue("名称");
            row1.CreateCell(5).SetCellValue("规格");
            row1.CreateCell(6).SetCellValue("单位");
            row1.CreateCell(7).SetCellValue("数量");

            if (pnSubList != null && pnSubList.Count > 0)
            {
                //将数据逐步写入sheet1各个行
                for (int i = 0; i < pnSubList.Count; i++)
                {
                    NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
                    rowtemp.CreateCell(0).SetCellValue(pnSubList[i].SDNNUM.ToString());
                    rowtemp.CreateCell(1).SetCellValue(pnSubList[i].PMM01.ToString());
                    rowtemp.CreateCell(2).SetCellValue(pnSubList[i].PMN02.ToString());
                    rowtemp.CreateCell(3).SetCellValue(pnSubList[i].PMN04.ToString());
                    rowtemp.CreateCell(4).SetCellValue(pnSubList[i].PMN041.ToString());
                    rowtemp.CreateCell(5).SetCellValue(pnSubList[i].IMA021.ToString());
                    rowtemp.CreateCell(6).SetCellValue(pnSubList[i].PMN07.ToString());
                    rowtemp.CreateCell(7).SetCellValue(pnSubList[i].PMN20.ToString());
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



        ///// <summary>
        ///// 删除检查项目
        ///// </summary>
        ///// <param name="mouldId"></param>
        ///// <returns></returns>
        //public ActionResult deleteRowAjax(string qcz01="" ,int qcz05 = 0)
        //{
        //    string status = "0";
        //    if (!Tcqcys.IsChecked(qcz01))
        //    {
        //        if (Tcqczs.IsExists(qcz01, qcz05))
        //        {
        //            Tcqczs.DeleteModel(qcz01, qcz05);
        //            //存在
        //            status = "1";
        //        }
        //    }
        //    else
        //    {
        //        status = "2";  //已经审核，不能修改
        //    }
        //    return AjaxResult("success", status, true);
        //}

        ///// <summary>
        ///// 检查是否可以选择（初检）
        ///// </summary>
        ///// <param name="qcz01"></param>
        ///// <param name="qcz05"></param>
        ///// <returns></returns>
        //public ActionResult checkingAjax(string qcz01 = "", int qcz05 = 0)
        //{
        //    string status = "0";
        //    var qczModelPre = Tcqczs.GetModelPre(qcz01, qcz05);
        //    if (qczModelPre == null)
        //    {
        //        //不存在前一项
        //        status = "1";
        //    }
        //    else if (qczModelPre != null && qczModelPre.TC_QCZ03 != null)
        //    {
        //        //存在并且有值
        //        status = "1";
        //    }

        //    return AjaxResult("success", status, true);
        //}

        //public ActionResult updateQcz03Ajax(string qcz01 = "",string qcz03 ="", int qcz05 = 0)
        //{
        //    string status = "0";

        //    if (!Tcqcys.IsChecked(qcz01))
        //    {


        //        var qczModel = Tcqczs.GetModel(qcz01, qcz05);
        //        if (qczModel != null)
        //        {
        //            //更新
        //            qczModel.TC_QCZ03 = qcz03;

        //            Tcqczs.UpdateModel(qczModel, qcz01, qcz05);
        //            status = "1";
        //        }
        //    }
        //    else
        //    {
        //        status = "2";  //已经审核，不能修改
        //    }

        //    return AjaxResult("success", status, true);
        //}

        //public ActionResult addQcz04Ajax(string qcz01 = "", string qcz04 = "", int qcz05 = 0)
        //{
        //    string status = "0";
        //    if (!Tcqcys.IsChecked(qcz01))
        //    {
        //        var qczModel = Tcqczs.GetModel(qcz01, qcz05);
        //        if (qczModel != null)
        //        {
        //            //更新
        //            qczModel.TC_QCZ04 = qcz04;
        //            Tcqczs.UpdateModel(qczModel, qcz01, qcz05);
        //            status = "1";
        //        }
        //    }
        //    else
        //    {
        //        status = "2";//已经审核，不能修改
        //    }
        //    return AjaxResult("success", status, true);
        //}

        //public ActionResult queryandDeleteQcyStatusAjax(string qcy01 = "")
        //{
        //    string status = "0";
        //    var qcyModel = Tcqcys.GetModel(qcy01);
        //    if (qcyModel != null)
        //    {
        //        //更新
        //        if (qcyModel.TC_QCY12 != null && qcyModel.TC_QCY12 == 2)
        //        {
        //            //已审核，不能删除 ，返回2
        //            status = "2";
        //        }
        //        else
        //        {
        //            //成功删除。返回1
        //            if (QcCheck.deleteQcy(qcy01))
        //            {
        //                status = "1";
        //            }
        //        }
        //    }
        //    return AjaxResult("success", status, true);
        //}

        //public ActionResult deleteQcyAjax(string qcy01 = "")
        //{
        //    string status = "0";
        //    if (QcCheck.deleteQcy(qcy01))
        //    {
        //        status = "1";
        //    }
        //    return AjaxResult("success", status, true);
        //}


        ///// <summary>
        ///// 检查是否可以选择（复检）
        ///// </summary>
        ///// <param name="qcz01"></param>
        ///// <param name="qcz05"></param>
        ///// <returns></returns>
        //public ActionResult ccheckingAjax(string qcz01 = "", int qcz05 = 0)
        //{
        //    string status = "0";
        //    var qczModelPre = Tcqczs.GetModelPre(qcz01, qcz05);
        //    if (qczModelPre == null)
        //    {
        //        //不存在前一项
        //        status = "1";
        //    }
        //    else if (qczModelPre != null && qczModelPre.TC_QCZ08 != null)  // 复检 TC_QCZ08
        //    {
        //        //存在并且有值
        //        status = "1";
        //    }

        //    return AjaxResult("success", status, true);
        //}

        //public ActionResult updateQcz08Ajax(string qcz01 = "", string qcz08 = "", int qcz05 = 0)
        //{
        //    string status = "0";

        //    if (!Tcqcys.IsChecked(qcz01))
        //    {
        //        var qczModel = Tcqczs.GetModel(qcz01, qcz05);
        //        if (qczModel != null)
        //        {
        //            //更新
        //            qczModel.TC_QCZ08 = qcz08;

        //            Tcqczs.UpdateModel(qczModel, qcz01, qcz05);
        //            status = "1";
        //        }
        //    }
        //    else
        //    {
        //        status = "2";  //已经审核，不能修改
        //    }

        //    return AjaxResult("success", status, true);
        //}

        ///// <summary>
        ///// 审核qcy
        ///// </summary>
        ///// <param name="qcy01"></param>
        ///// <returns></returns>
        //public ActionResult checkQcyAjax(string qcy01 = "")
        //{
        //    string status = "0";
        //    if (Tcqczs.IsExistsNotChecked(qcy01))
        //    { 
        //        //有未检查项 2 
        //        status = "2";
        //    }
        //    else
        //    {
        //        //更新
        //        TC_QCY_FILE model = Tcqcys.GetModel(qcy01);
        //        if (model != null)
        //        {
        //            //0:开立 1：填表 2：审核
        //            model.TC_QCY12 = 2;
        //            model.TC_QCY13 = WorkContext.UserName;
        //            model.TC_QCY14 = DateTime.Now;

        //            Tcqcys.UpdateModel(model, qcy01);
        //            status = "1";
        //        }
        //    }
        //    return AjaxResult("success", status, true);
        //}


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
