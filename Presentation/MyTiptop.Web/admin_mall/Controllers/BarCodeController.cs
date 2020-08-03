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
    public class BarCodeController : BaseMallAdminController
    {

        #region 结构件

        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="PrcsName">用户（查询用）</param>
        /// <param name="pageNumber">当前页码</param>
        /// <param name="pageSize">页尺寸</param>
        /// <returns></returns>
        public ActionResult BrdList(string DeptName, int pageNumber = 1, int pageSize = 15)
        {
            // 获取参数、分页信息 （Post 传参数传递约定大于配置）
            // 查询所有用户信息
            if (pageSize <= 0)
                //防止除0操作
                pageSize = 1;
            //查询条件
            string filter = String.Empty;
            //if (DeptName != null && DeptName != "")
            //{
            //    filter += " DeptName  like '%" + DeptName + "%'";
            //}
            filter = "where 1=1";

            //返回总页数、总记录数 
            //int totalPage;  //页数 脚标自己会算
            int totalRecord;
            //分页查询 out totalPage,
            List<TC_BRD_FILE> List = new OraRDBSHelper().ExecutePaging<TC_BRD_FILE>("tc_brd_file", "", filter, pageSize, pageNumber, out totalRecord);
            //页脚Model
            PageModel pageModel = new PageModel(pageSize, pageNumber, totalRecord);
            //初始化ViewModel
            BrdListViewModel model = new BrdListViewModel()
            {
                PageModel = pageModel,
                List = List
                //, DeptName = DeptName
            };
            //记录本次访问地址， 用于返回上一层。
            MallUtils.SetAdminRefererCookie(string.Format("{0}?pageNumber={1}&pageSize={2}&DeptName={3}",
                                  Url.Action("DeptList"), pageModel.PageNumber, pageModel.PageSize,
                                  DeptName));
            //返回View
            return View(model);
        }



        #endregion


        #region 生产计划表

        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="PrcsName">用户（查询用）</param>
        /// <param name="pageNumber">当前页码</param>
        /// <param name="pageSize">页尺寸</param>
        /// <returns></returns>
        public ActionResult SfmList(string DeptName, int pageNumber = 1, int pageSize = 15)
        {
            // 获取参数、分页信息 （Post 传参数传递约定大于配置）
            // 查询所有用户信息
            if (pageSize <= 0)
                //防止除0操作
                pageSize = 1;
            //查询条件
            string filter = String.Empty;
            //if (DeptName != null && DeptName != "")
            //{
            //    filter += " DeptName  like '%" + DeptName + "%'";
            //}
            filter = "where 1=1";

            string orderby = " tc_sfm02 desc ";

            //返回总页数、总记录数 
            //int totalPage;  //页数 脚标自己会算
            int totalRecord;
            //分页查询 out totalPage,
            List<TC_SFM_FILE> List = new OraRDBSHelper().ExecutePaging<TC_SFM_FILE>("tc_sfm_file", orderby, filter, pageSize, pageNumber, out totalRecord);
            //页脚Model
            PageModel pageModel = new PageModel(pageSize, pageNumber, totalRecord);
            //初始化ViewModel
            SfmListViewModel model = new SfmListViewModel()
            {
                PageModel = pageModel,
                List = List
                //, DeptName = DeptName
            };
            //记录本次访问地址， 用于返回上一层。
            MallUtils.SetAdminRefererCookie(string.Format("{0}?pageNumber={1}&pageSize={2}&DeptName={3}",
                                  Url.Action("SfmList"), pageModel.PageNumber, pageModel.PageSize,
                                  DeptName));
            //返回View
            return View(model);
        }



        #endregion


        #region 更换条码（气密性等）

        public ActionResult ChangeBarCode(string msg = "")
        {
            //string cmdStr = " select * from  tc_oma_file ";
            //DataTable dt = OraQuery.GetCommonQuery(cmdStr);
            ChangeBarCodeViewModel model = new ChangeBarCodeViewModel
            {
                List = new DataTable(),
                Message = msg
                //PageModel = new PageModel(1, 1, 1)  //不能翻页 ？
            };

            //记录本次访问地址， 用于返回上一层。
            MallUtils.SetAdminRefererCookie(string.Format("{0}",
                                  Url.Action("ChangeBarCode"))
                                  );

            return View(model);
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public ActionResult UploadChangeBarCode(HttpPostedFileBase file)
        {
            string path = "";
            FileController filehelper = new Controllers.FileController();
            if (Request != null && Request.Files.Count > 0 && Request.Files[0].FileName.Length > 1)
            {
                //if (Request.Files.Count > 0 && Request.Files[0].FileName.Length > 1)
                //{
                string ufile = Server.MapPath("~/upload");  //固定上传路径
                //上传文件
                path = filehelper.Upload(file, ufile);
                //}
            }
            else
            {
                //返回显示值 
                return Redirect("ChangeBarCode");
            }

            // 读取文件。NPOI 读取数据 
            DataTable dt = filehelper.Import(path, 1);

            //清条码系统，对数据进行检测。增加检测结果字段。

            dt.Columns.Add("v");
            dt.Columns.Add("v2");

            dt.Columns.Add("flag");

            foreach (DataRow row in dt.Rows)
            {
                string oldbarcode = row[1].ToString();
                string newbarcode = row[2].ToString();

                TC_BRD_FILE brdModel = TcBrdFiles.getModel(newbarcode);

                if (brdModel != null)
                {
                    row[3] = brdModel.TC_BRDACTI.ToString();  // 获取tc_brdacti
                    row[4] = brdModel.TC_BRD20.ToString();
                }
                else
                {
                    row[3] = "-";  // 获取tc_brdacti
                    row[4] = "-";
                }
            }

            //删除文件
            filehelper.DeleteFile(path);
            //重新显示
            ChangeBarCodeViewModel viewModel = new ChangeBarCodeViewModel
            {
                List = dt,
                Message = "显示列表"
            };
            //返回显示值 
            return View("ChangeBarCode", viewModel);
        }


        /// <summary>
        /// 导入数据
        /// </summary>
        /// <returns></returns>
        public ActionResult ImportChangeBarCode(string[] 序号, string[] 旧条码, string[] 新条码,string [] 系统值,string [] chFlag)
        {
            bool flag1 = false;
            bool flag2 = false;
            if (chFlag != null)
            {
                foreach (string s in chFlag)
                {
                    if (s == "1")
                        flag1 = true;
                    if (s == "2")
                        flag2 = true;
                }
            }
            if (新条码 == null || 新条码.Length <= 0)
            {
                return PromptView("导入格式错误，请检查!Error：无编码信息");
            }
            //开启事务管理
            using (TransactionScope sc = new TransactionScope())
            {
                try
                {
                    for (int i = 0; i < 新条码.Length; i++)
                    {
                        //Excel行号 用于提示错误信息
                        int e = i + 2;
                        string newbarcode = 新条码[i].ToString();
                        string oldbarcode = 旧条码[i].ToString();
                        string v = 系统值[i].ToString();

                        if (v == "-")
                        {
                            // 错误
                            return PromptView("导入格式错误，请检查!Error：状态信息不正确 !EXCEL行号:" + e.ToString());
                        }

                        //选中更新第一部分
                        if (flag1)  
                        {
                            if (TcBrdFiles.isExists(newbarcode))
                            {
                                TC_BRD_FILE model = new TC_BRD_FILE
                                {
                                    TC_BRD20 = 0,
                                    TC_BRDACTI = "0"
                                };
                                //修改
                                TcBrdFiles.upDate(model, newbarcode);
                            }
                            else
                            {
                                return PromptView("导入格式错误，请检查!Error：条码信息不正确 !EXCEL行号:" + e.ToString());
                            }
                        }
                        //选中更新第二部分
                        if (flag2)
                        {
                            if (TcBrgFiles.isExists(oldbarcode))
                            {
                                //TC_BRG02 为联合主键 EF不能更新 。改为sql更新方式
                                //TC_BRG_FILE model = new TC_BRG_FILE();
                                //model.TC_BRG02 = newbarcode;
                                TcBrgFiles.upDate(newbarcode, oldbarcode);
                            }

                            if (TcBrsFiles.isExists(oldbarcode))
                            {
                                //TC_BRS_FILE model = new TC_BRS_FILE();
                                //model.TC_BRS02 = newbarcode;
                                TcBrsFiles.upDate(newbarcode, oldbarcode);
                            }
                        }
                    }
                }
                catch (Exception Ex)
                {
                    return PromptView("导入格式错误，请检查!Error:" + Ex.Message);
                }
                //提交
                sc.Complete();
            }
            return RedirectToAction("ChangeBarCode", new { msg = "导入成功！" });
            //return PromptView("导入成功!");
        }


        #endregion 

        #region  清条码

        public ActionResult ClearBarCode(string msg = "")
        {

            string cmdStr = " select * from  tc_oma_file ";

            DataTable dt = OraQuery.GetCommonQuery(cmdStr);



            ClearBarCodeViewModel model = new ClearBarCodeViewModel
            {
                List = new DataTable(),
                Message = msg
                //PageModel = new PageModel(1, 1, 1)  //不能翻页 ？
            };

            //记录本次访问地址， 用于返回上一层。
            MallUtils.SetAdminRefererCookie(string.Format("{0}",
                                  Url.Action("ClearBarCode"))
                                  );

            return View(model);
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public ActionResult UploadClearBarCode(HttpPostedFileBase file)
        {
            string path = "";
            FileController filehelper = new Controllers.FileController();
            if (Request != null && Request.Files.Count > 0 && Request.Files[0].FileName.Length > 1)
            {
                //if (Request.Files.Count > 0 && Request.Files[0].FileName.Length > 1)
                //{
                string ufile = Server.MapPath("~/upload");  //固定上传路径
                //上传文件
                path = filehelper.Upload(file, ufile);
                //}
            }
            else
            {
                //返回显示值 
                return Redirect("ClearBarCode");
                //return View("ClearBarCode");
            }

            // 读取文件。NPOI 读取数据 
            DataTable dt = filehelper.Import(path, 1);

            //清条码系统，对数据进行检测。增加检测结果字段。

            dt.Columns.Add("v");
            dt.Columns.Add("flag");

            foreach (DataRow row in dt.Rows)
            {
                string barcode = row[1].ToString();
                string storecode = row[2].ToString();

                row[3] = BarCodes.GetImgs8(barcode, storecode);

                //string plant = WorkContext.Plant;

                //1.检测当前运营中心有没有此 仓库编号。防止用户登录错营运中心
                //2.制造批次号是否存在。

                if (!BarCodes.StoreIsExists(storecode))
                {
                    row[4] = "1.仓库不存在";
                }
                else if (!BarCodes.BarCodeIsexists(barcode, storecode))
                {
                    row[4] = "2.编码、仓库不存在";
                }
            }

            //删除文件
            filehelper.DeleteFile(path);
            //重新显示
            ClearBarCodeViewModel model = new ClearBarCodeViewModel
            {
                List = dt,
                Message = "显示列表"
            };
            //返回显示值 
            return View("ClearBarCode", model);

        }

        /// <summary>
        /// 导入数据
        /// </summary>
        /// <returns></returns>
        public ActionResult Import(string[] 序号, string[] 制造批号, string[] 仓库编号, string[] 系统值)
        {
            if (制造批号 == null || 制造批号.Length <= 0)
            {
                return PromptView("导入格式错误，请检查!Error：无编码信息");
            }
            //开启事务管理
            using (TransactionScope sc = new TransactionScope())
            {
                try
                {
                    for (int i = 0; i < 制造批号.Length; i++)
                    {
                        //Excel行号 用于提示错误信息
                        int e = i + 2;

                        string barcode = 制造批号[i].ToString();
                        string storecode = 仓库编号[i].ToString();
                        string v = 系统值[i].ToString();

                        if (v == "-")
                        {
                            // 错误
                            return PromptView("导入格式错误，请检查!Error：状态信息不正确 !EXCEL行号:" + e.ToString());
                        }
                        IMGS_FILE model = new IMGS_FILE
                        {
                            IMGS08 = 0
                        };
                        //修改
                        BarCodes.UpdateImgs(model, barcode, storecode);
                    }
                }
                catch (Exception Ex)
                {
                    return PromptView("导入格式错误，请检查!Error:" + Ex.Message);
                }
                //提交
                sc.Complete();
            }

            return RedirectToAction("ClearBarCode", new { msg = "导入成功！" });
            //return PromptView("导入成功!");

        }


        #endregion


        public ActionResult MySfmReport(DateTime? t = null)
        {
            //查询条件
            string filter = String.Empty;

            #region 

            //var list = SfmFiles.GetSfnList(t);

            //DataTable dt = new DataTable();
            //DataColumn dc = null;
            //dc = dt.Columns.Add("1", Type.GetType("System.Int32"));
            //dc = dt.Columns.Add("2", Type.GetType("System.String"));
            //dc = dt.Columns.Add("3", Type.GetType("System.String"));
            //dc = dt.Columns.Add("4", Type.GetType("System.String"));
            //dc = dt.Columns.Add("5", Type.GetType("System.String"));
            //dc = dt.Columns.Add("6", Type.GetType("System.String"));
            //dc = dt.Columns.Add("7", Type.GetType("System.String"));
            //dc = dt.Columns.Add("8", Type.GetType("System.String"));
            //dc = dt.Columns.Add("9", Type.GetType("System.String"));

            //if (list != null && list.Count > 0)
            //{
            //    int sortno = 1;
            //    foreach (TC_SFN_FILE m in list)
            //    {
            //        //根据编码再一次循环

            //        var bmbList = BmbFiles.GetBmbListFor(ObkFiles.GetIma01(m.TC_SFN04));

            //        foreach (BMB_FILE bmbModel in bmbList)
            //        {
            //            DataRow row = dt.NewRow();
            //            row[0] = sortno.ToString();  //序号
            //            row[1] = SflFiles.GetSflName(m.TC_SFN03);  //线别 。序号合并去除重复项
            //            row[2] = m.TC_SFN04;  //编码
            //            row[3] = ImaFiles.GetImaName(bmbModel.BMB03);  //名称  . 指BOM表 子料件编码 6、7 开头的名称
            //            row[4] = ImaFiles.GetIma021(bmbModel.BMB03);  //图号
            //            row[5] = bmbModel.BMB06 / bmbModel.BMB07;  //用量
            //            row[6] = m.TC_SFN05 * (bmbModel.BMB06 / bmbModel.BMB07) ;  //计划数量
            //            row[7] = "";  //实际备货数量
            //            row[8] = "";  //备注

            //            #region 
            //            //row[0] = "0";  //序号
            //            //row[1] = "1";  //线别 。序号合并去除重复项
            //            //row[2] = "2";  //编码
            //            //row[3] = "3";  //名称
            //            //row[4] = "4";  //图号
            //            //row[5] = "5";  //用量
            //            //row[6] = "6";  //计划数量
            //            //row[7] = "7";  //实际备货数量
            //            //row[8] = "8";  //备注

            //            #endregion

            //            dt.Rows.Add(row);
            //            sortno++;
            //        }
            //    } 
            //}

            #endregion 

            var dt = BarCodes.getsfmreport(t);

            //初始化ViewModel
            MySfmReportViewModel model = new MySfmReportViewModel()
            {
                SFMInfo = dt,
                t = t
            };
            //记录本次访问地址， 用于返回上一层。
            MallUtils.SetAdminRefererCookie(string.Format("{0}",
                                  Url.Action("MySfmReport")
                                  ));
            //返回View
            return View(model);
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <returns></returns>
        public FileResult Export(string t = "")
        {
            //创建Excel文件的对象
            //NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();   // XLS
            NPOI.XSSF.UserModel.XSSFWorkbook book = new NPOI.XSSF.UserModel.XSSFWorkbook();     // XLSX
            //添加一个sheet
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");

            //获取list数据
            DateTime? tt = null;

            if (t != null && t != "")
            {
                tt = TypeHelper.StringToDateTime(t);
            }
            var dt = BarCodes.getsfmreport(tt);

            //CellRangeAddress四个参数为：起始行，结束行，起始列，结束列
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, 5));
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 6, 8));

            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
            row1.CreateCell(0).SetCellValue("生产日计划");
            row1.CreateCell(6).SetCellValue(tt.ToString());

            //给sheet1添加第一行的头部标题
            NPOI.SS.UserModel.IRow row2 = sheet1.CreateRow(1);
            row2.CreateCell(0).SetCellValue("序号");
            row2.CreateCell(1).SetCellValue("线别");
            row2.CreateCell(2).SetCellValue("编码");
            row2.CreateCell(3).SetCellValue("名称");
            row2.CreateCell(4).SetCellValue("图号");
            row2.CreateCell(5).SetCellValue("用量");
            row2.CreateCell(6).SetCellValue("计划数量");
            row2.CreateCell(7).SetCellValue("实际备货数量");
            row2.CreateCell(8).SetCellValue("备注");

            if (dt != null && dt.Rows.Count > 0)
            {
                //将数据逐步写入sheet1各个行
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 2);
                    rowtemp.CreateCell(0).SetCellValue(dt.Rows[i][0].ToString());
                    rowtemp.CreateCell(1).SetCellValue(dt.Rows[i][1].ToString());
                    rowtemp.CreateCell(2).SetCellValue(dt.Rows[i][2].ToString());
                    rowtemp.CreateCell(3).SetCellValue(dt.Rows[i][3].ToString());
                    rowtemp.CreateCell(4).SetCellValue(dt.Rows[i][4].ToString());
                    rowtemp.CreateCell(5).SetCellValue(dt.Rows[i][5].ToString());
                    rowtemp.CreateCell(6).SetCellValue(dt.Rows[i][6].ToString());
                    // rowtemp.CreateCell(7).SetCellValue(dt.Rows[i][7].ToString());
                    // rowtemp.CreateCell(8).SetCellValue(dt.Rows[i][8].ToString());
                }
            }
            // 写入到客户端  
            var ms = new NpoiMemoryStream();
            ms.AllowClose = false;
            book.Write(ms);
            ms.Flush();
            ms.Seek(0, SeekOrigin.Begin);
            ms.AllowClose = true;
            return File(ms, "application/vnd.ms-excel", "sfn.xlsx");
        }


    }
}