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
using System.Drawing;
using QRCoder;



namespace MyTiptop.Web.MallAdmin.Controllers
{
    public class RvvBarCodeController : BaseMallAdminController
    {
        #region 结构件 
         





        /// <summary> 
        /// 用户列表 
        /// </summary>
        /// <param name="PrcsName">用户（查询用）</param>
        /// <param name="pageNumber">当前页码</param>
        /// <param name="pageSize">页尺寸</param>
        /// <returns></returns>
        public ActionResult RvvList(string DeptName, int outExcel = 0, int pageNumber = 1, int pageSize = 15)
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
            // filter = "where 1=1";








            //返回总页数、总记录数 
            int totalPage;  //页数 脚标自己会算
            int totalRecord;
            //分页查询 out totalPage,
            List<rvvList> List = new RDBSHelper().ExecutePaging<rvvList>("rvvList", "*", "  id asc", filter, pageSize, pageNumber, out totalPage, out totalRecord);



            


            foreach(rvvList r in List)
            {
                string ipath = Server.MapPath("~/upload/ewm/");  //二维码图片固定位置

                r.qrcode2 = "";  //保存二维码图片并 记录下路径

                ipath = ipath + r.id + ".png";

                try
                {
                    //Bitmap bmp = Encoder.code("test", 1, 100, ipath, 100, 1, true);
                    Bitmap bmp = Encoder.code2("test");
                    r.qrcode2 = "upload/ewm/"+ r.id + ".png";
                }
                catch(Exception Ex)
                {
                   string aa =  Ex.Message;
                }
            }

            //页脚Model
            PageModel pageModel = new PageModel(pageSize, pageNumber, totalRecord);
            //初始化ViewModel
            RvvListViewModel model = new RvvListViewModel()
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




        /// <summary>
        /// 导出到pdf
        /// </summary>
        /// <returns></returns>
        public FileResult ExportToPdf(string t = "")
        { 
          
            
            //获取list数据 
            DateTime? tt = null; 
             
            if (t != null && t != "") 
            { 
                tt = TypeHelper.StringToDateTime(t); 
            } 
            var dt = BarCodes.getsfmreport(tt); 

            return null;


            
        } 





        /// <summary>
        /// 导出到pdf
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




    public static class Encoder
    {
        /// <summary>
        ///生成二维码
        ///</summary>
        ///<param name="msg">信息</param>
        ///<param name="version">版本 1 ~ 40</param>
        ///<param name="pixel">像素点大小</param>
        ///<param name="icon_path">图标路径</param>
        ///<param name="icon_size">图标尺寸</param>
        ///<param name="icon_border">图标边框厚度</param>
        ///<param name="white_edge">二维码白边</param>
        ///<returns>位图</returns>
        public static  Bitmap code(string msg, int version, int pixel, string icon_path, int icon_size, int icon_border, bool white_edge)
        {

           

            QRCoder.QRCodeGenerator code_generator = new QRCoder.QRCodeGenerator();
            QRCoder.QRCodeData code_data = code_generator.CreateQrCode(msg, QRCoder.QRCodeGenerator.ECCLevel.M/* 这里设置容错率的一个级别 */, true, true, QRCoder.QRCodeGenerator.EciMode.Utf8, version);
            QRCoder.QRCode code = new QRCoder.QRCode(code_data);
            Bitmap icon = new Bitmap(icon_path);
            //Bitmap i = new Bitmap("");
            Bitmap bmp = code.GetGraphic(pixel, Color.Black, Color.White, icon, icon_size, icon_border, white_edge);
            return bmp;
        }

        public static Bitmap code2(string msg)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(msg, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);

            return qrCodeImage;

        }
    }
}