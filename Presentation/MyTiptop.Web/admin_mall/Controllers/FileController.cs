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
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Transactions;


namespace MyTiptop.Web.MallAdmin.Controllers
{

    //新建类 重写Npoi流方法. 解决npoi 导出xlsx问题
    public class NpoiMemoryStream : MemoryStream
    {
        public NpoiMemoryStream()
        {
            AllowClose = true;
        }

        public bool AllowClose { get; set; }

        public override void Close()
        {
            if (AllowClose)
                base.Close();
        }
    }

    public class FileController : BaseMallAdminController
    {
        /// <summary>
        /// 导出
        /// </summary>
        /// <returns></returns>
        public FileResult Export(string key = "", string status = "")
        {
            //查询条件
            string filter = String.Empty;

            if (status != null && status != "")
            {
                if (filter.Length > 1)
                {
                    filter += " and ";
                }
                filter = " StatusID = " + status;
            }
            if (key != null && key != "")
            {
                if (filter.Length > 1)
                {
                    filter += " and ";
                }
                filter += "( Ecode like '%" + key + "%'  or UserName like '%" + key + "%' )";
            }
            filter += " order by Id desc ";

            //创建Excel文件的对象
            //NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();   // XLS
            NPOI.XSSF.UserModel.XSSFWorkbook book = new NPOI.XSSF.UserModel.XSSFWorkbook();     // XLSX
            //添加一个sheet
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");
            //获取list数据
            List<Equipment> List = Equipments.GetList(filter);
            //给sheet1添加第一行的头部标题
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
            row1.CreateCell(0).SetCellValue("编号");
            row1.CreateCell(1).SetCellValue("类型");
            row1.CreateCell(2).SetCellValue("IP");
            row1.CreateCell(3).SetCellValue("存放地址");
            row1.CreateCell(4).SetCellValue("部门");
            row1.CreateCell(5).SetCellValue("使用人");
            row1.CreateCell(6).SetCellValue("状态");
            row1.CreateCell(7).SetCellValue("品牌");
            row1.CreateCell(8).SetCellValue("型号");
            row1.CreateCell(9).SetCellValue("备注");
            row1.CreateCell(10).SetCellValue("录入员");
            row1.CreateCell(11).SetCellValue("更新时间");

            if (List != null && List.Count > 0)
            {
                //将数据逐步写入sheet1各个行
                for (int i = 0; i < List.Count; i++)
                {
                    NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
                    rowtemp.CreateCell(0).SetCellValue(List[i].Ecode.ToString());
                    rowtemp.CreateCell(1).SetCellValue(BaseSorts.GetSortName(List[i].SortID));
                    rowtemp.CreateCell(2).SetCellValue(List[i].IP);
                    rowtemp.CreateCell(3).SetCellValue(List[i].Place);
                    rowtemp.CreateCell(4).SetCellValue(BaseDepts.GetDeptName(List[i].DeptID));
                    rowtemp.CreateCell(5).SetCellValue(List[i].UserName);
                    rowtemp.CreateCell(6).SetCellValue(BaseStatuss.GetStatusName(List[i].StatusID));
                    rowtemp.CreateCell(7).SetCellValue(List[i].Brand);
                    rowtemp.CreateCell(8).SetCellValue(List[i].Version);
                    rowtemp.CreateCell(9).SetCellValue(List[i].Remark);
                    rowtemp.CreateCell(10).SetCellValue(Users.GetUserCNameByUserID(List[i].InputUserID));
                    rowtemp.CreateCell(11).SetCellValue(List[i].UpdateTime.ToString());
                }
            }


            // 写入到客户端  

            #region作废

            //xls 导出方法 
            //System.IO.MemoryStream ms = new System.IO.MemoryStream();
            //book.Write(ms);

            //ms.Seek(0, SeekOrigin.Begin);
            //return File(ms, "application/vnd.ms-excel", "Equipment.xlsx");

            #endregion


            var ms = new NpoiMemoryStream();
            ms.AllowClose = false;
            book.Write(ms);
            ms.Flush();
            ms.Seek(0, SeekOrigin.Begin);
            ms.AllowClose = true;

            return File(ms, "application/vnd.ms-excel", "Equipment.xlsx");




        }


        //public ActionResult Upload(HttpPostedFileBase file)
        public  string  Upload(HttpPostedFileBase file,string ufile)
        {
            string delfileName = "";
            string apath = "";
          
            ////////////   上传文件            
            //if (Request != null)
            //{
            //    if (Request.Files.Count > 0)
            //    {
                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(ufile, fileName);   //固定上传文件位置Server.MapPath("~/upload")

                        if (!UpLoadFile.IsAllowedExtension(fileName))
                        {
                              return "此文件不允许上传!";
                        }
                        //保存
                        file.SaveAs(path);

                        delfileName = fileName;
                        apath = path;
                    }
            //    }
            //}
            return apath;

            // 读取文件。NPOI 读取数据 
            //DataTable dt = Import(apath, 1);

            //利用完文件  、删除文件  ////////////
            //UpLoadFile.DeleteFile(Server.MapPath("~/upload"), delfileName);

            //System.IO.File.Delete(apath);//每次上传完毕删除文件

            ////重新显示
            //ImportEquipmentViewModel model = new ImportEquipmentViewModel
            //{
            //    List = dt,
            //    Message = "显示列表"
            //};

            //返回显示值 
            //return View("ImportEquipment",model);
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        public  void DeleteFile(string filepath)
        {   
            //存在文件 
            if (System.IO.File.Exists(filepath)) //Server.MapPath("~/upimg/Data.html")))
            {
                System.IO.File.Delete(filepath);//每次上传完毕删除文件
            }
        }

        /// <summary>读取excel
        /// 默认第一行为表头
        /// </summary>
        /// <param name="strFileName">excel文档绝对路径</param>
        /// <param name="rowIndex">内容行偏移量，第一行为表头，内容行从第二行开始则为1</param>
        /// <returns></returns>
        public  DataTable Import(string strFileName, int rowIndex)
        {
            DataTable dt = new DataTable();

            IWorkbook hssfworkbook;
            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                hssfworkbook = WorkbookFactory.Create(file);
            }
            ISheet sheet = hssfworkbook.GetSheetAt(0);

            IRow headRow = sheet.GetRow(0);
            if (headRow != null)
            {
                int colCount = headRow.LastCellNum;
                for (int i = 0; i < colCount; i++)
                {
                    dt.Columns.Add("COL_" + i);
                }
            }
            for (int i = (sheet.FirstRowNum + rowIndex); i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                bool emptyRow = true;
                object[] itemArray = null;

                if (row != null)
                {
                    itemArray = new object[row.LastCellNum];

                    for (int j = row.FirstCellNum; j < row.LastCellNum; j++)
                    {

                        if (row.GetCell(j) != null)
                        {

                            switch (row.GetCell(j).CellType)
                            {
                                case CellType.Numeric:
                                    if (HSSFDateUtil.IsCellDateFormatted(row.GetCell(j)))//日期类型
                                    {
                                        itemArray[j] = row.GetCell(j).DateCellValue.ToString("yyyy-MM-dd");
                                    }
                                    else//其他数字类型
                                    {
                                        itemArray[j] = row.GetCell(j).NumericCellValue;
                                    }
                                    break;
                                case CellType.Blank:
                                    itemArray[j] = string.Empty;
                                    break;
                                case CellType.Formula:
                                    if (Path.GetExtension(strFileName).ToLower().Trim() == ".xlsx")
                                    {
                                        XSSFFormulaEvaluator eva = new XSSFFormulaEvaluator(hssfworkbook);
                                        if (eva.Evaluate(row.GetCell(j)).CellType == CellType.Numeric)
                                        {
                                            if (HSSFDateUtil.IsCellDateFormatted(row.GetCell(j)))//日期类型
                                            {
                                                itemArray[j] = row.GetCell(j).DateCellValue.ToString("yyyy-MM-dd");
                                            }
                                            else//其他数字类型
                                            {
                                                itemArray[j] = row.GetCell(j).NumericCellValue;
                                            }
                                        }
                                        else
                                        {
                                            itemArray[j] = eva.Evaluate(row.GetCell(j)).StringValue;
                                        }
                                    }
                                    else
                                    {
                                        HSSFFormulaEvaluator eva = new HSSFFormulaEvaluator(hssfworkbook);
                                        if (eva.Evaluate(row.GetCell(j)).CellType == CellType.Numeric)
                                        {
                                            if (HSSFDateUtil.IsCellDateFormatted(row.GetCell(j)))//日期类型
                                            {
                                                itemArray[j] = row.GetCell(j).DateCellValue.ToString("yyyy-MM-dd");
                                            }
                                            else//其他数字类型
                                            {
                                                itemArray[j] = row.GetCell(j).NumericCellValue;
                                            }
                                        }
                                        else
                                        {
                                            itemArray[j] = eva.Evaluate(row.GetCell(j)).StringValue;
                                        }
                                    }
                                    break;
                                default:
                                    itemArray[j] = row.GetCell(j).StringCellValue;
                                    break;

                            }

                            if (itemArray[j] != null && !string.IsNullOrEmpty(itemArray[j].ToString().Trim()))
                            {
                                emptyRow = false;
                            }
                        }
                    }
                }

                //非空数据行数据添加到DataTable
                if (!emptyRow)
                {
                    dt.Rows.Add(itemArray);
                }
            }
            return dt;
        }
   
    } 
} 