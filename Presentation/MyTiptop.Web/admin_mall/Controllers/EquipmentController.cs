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
    //public class NpoiMemoryStream : MemoryStream
    //{
    //    public NpoiMemoryStream()
    //    {
    //        AllowClose = true;
    //    }

    //    public bool AllowClose { get; set; }

    //    public override void Close()
    //    {
    //        if (AllowClose)
    //            base.Close();
    //    }
    //}

    public class EquipmentController : BaseMallAdminController
    {
        public ActionResult EquipmentList()
        {
            // 
            return View(); 
        }

        /// <summary>
        /// 实例化下拉框
        /// </summary>
        /// <returns></returns>
        public ActionResult ddlStatus()
        {
            string List = BaseStatuss.GetStatusList(10);
            //json
            //[{ "id": "usa", "text": "美国" },{ "id": "cn", "text": "中国" }]
            //直接返回文本Json
            return Content(List);
        }

        /// <summary>
        /// 获取Status列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetStatus()
        {
            List<Base_Status> list = BaseStatuss.GetList();
            //List 转化为 Json
            string List = list.ListToJson();
            //直接返回文本Json
            return Content(List);
        }

        /// <summary>
        /// 获取类型
        /// </summary>
        /// <returns></returns>
        public ActionResult GetSort()
        {
            List<Base_Sort> list = BaseSorts.GetList();
            //List 转化为 Json
            string List = list.ListToJson();
            //直接返回文本Json
            return Content(List);
        }

        /// <summary>
        /// 获取类型
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDept()
        {
            List<Base_Dept> list = BaseDepts.GetList();
            //List 转化为 Json
            string List = list.ListToJson();
            //直接返回文本Json
            return Content(List);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult GetUser()
        {
            List<xpGrid_User> list = Users.GetUser();
            //List 转化为 Json
            string List = list.ListToJson();
            //直接返回文本Json
            return Content(List);
        }


        /// <summary>
        /// 保存修改
        /// </summary>
        public void SaveChangedEmployees(string data)
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
                    Equipments.DeleteModel(id);
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
            Equipment model = new Equipment();
            //状态不能为空
            if(row["StatusID"] == null)
            {
                return;
            }
            //用于三元表达式赋空值
            int? Null = null;
            //DateTime? DNUll = null;

            //准备数据
            model.StatusID = TypeHelper.StringToInt(row["StatusID"].ToString());

            model.Ecode = row["Ecode"] == null ? null : row["Ecode"].ToString();
            model.DeptID = row["DeptID"] == null ? Null : TypeHelper.StringToInt(row["DeptID"].ToString());
            model.Brand = row["Brand"] == null ? null : row["Brand"].ToString();
            model.IP = row["IP"] == null ? null : row["IP"].ToString();
            model.Place = row["Place"] == null ? null : row["Place"].ToString();
            model.Remark = row["Remark"] == null ? null : row["Remark"].ToString();
            model.SortID = row["SortID"] == null ? Null : TypeHelper.StringToInt(row["SortID"].ToString());
            model.UserName = row["UserName"] == null ? null : row["UserName"].ToString();
            model.Version = row["Version"] == null ? null : row["Version"].ToString();

            //自动完成

            model.UpdateTime = DateTime.Now;
            model.InputUserID = WorkContext.Uid;

            //添加
            Equipments.AddModel(model);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="row"></param>
        private void editRow(Hashtable row)
        {
            int id = Convert.ToInt32(row["id"].ToString());
            Equipment model = new Equipment();

            //需要修改的值 

            //状态不能为空
            if (row["StatusID"] == null)
            {
                return;
            }
            //用于三元表达式赋空值
            int? Null = null;
            //DateTime? DNUll = null;

            //准备数据
            model.StatusID = TypeHelper.StringToInt(row["StatusID"].ToString());

            model.Ecode = row["Ecode"] == null ? null : row["Ecode"].ToString();
            model.DeptID = row["DeptID"] == null ? Null : TypeHelper.StringToInt(row["DeptID"].ToString());
            model.Brand = row["Brand"] == null ? null : row["Brand"].ToString();
            model.IP = row["IP"] == null ? null : row["IP"].ToString();
            model.Place = row["Place"] == null ? null : row["Place"].ToString();
            model.Remark = row["Remark"] == null ? null : row["Remark"].ToString();
            model.SortID = row["SortID"] == null ? Null : TypeHelper.StringToInt(row["SortID"].ToString());
            model.UserName = row["UserName"] == null ? null : row["UserName"].ToString();
            model.Version = row["Version"] == null ? null : row["Version"].ToString();

            //自动完成

            model.UpdateTime = DateTime.Now;
            model.InputUserID = WorkContext.Uid;

            //编辑
    
            Equipments.UpdateModel(model,id);

        }


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



        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="key"></param>
        /// <param name="status"></param>
        /// <param name="sortField"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ActionResult MyDataBing(string key,string status="",string sortField="", string sortOrder="", int pageIndex = 0, int pageSize = 15)
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
                sortOrder = " desc ";
            }
            sortField = sortField + " "+ sortOrder;

            //查询条件
            string filter = String.Empty;

            if (status !=null && status != "")
            {
                if (filter.Length > 1)
                {
                    filter += " and ";
                }
                filter = " StatusID = " + status ; 
            }
            if (key !=null && key != "")
            {
                if (filter.Length > 1)
                {
                    filter += " and ";
                }
                filter += "( Ecode like '%" + key + "%'  or UserName like '%" + key + "%' )";
            }
     
            //返回总页数、总记录数 
            int totalPage; 
            int totalRecord; 
            //分页查询 
            DataTable dt = new RDBSHelper().GetList("Equipment", "*", sortField , filter, pageSize, pageIndex, out totalPage, out totalRecord);
            //页脚Model 
      
            ArrayList dataAll = dt.DataTable2ArrayList(); 
            
            Hashtable result = new Hashtable(); 
            result["data"] = dataAll; 
            result["total"] = totalRecord; 
             
            return Json(result, JsonRequestBehavior.AllowGet); 
        }


        public ActionResult ImportEquipment()
        {

            ImportEquipmentViewModel model = new ImportEquipmentViewModel
            {
                List = new DataTable(),
                Message = "显示列表",
                PageModel = new PageModel(1, 1, 1)  //不能翻页 ？
            };


            return View(model);
        }


        public ActionResult Upload(HttpPostedFileBase file)
        {

            string delfileName = "";
            string apath = "";
          
            ////////////   上传文件            
            if (Request != null)
            {
                if (Request.Files.Count > 0)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/upload"), fileName);   //固定上传文件位置

                        if (!UpLoadFile.IsAllowedExtension(fileName))
                        {
                              return PromptView("此文件不允许上传!");
                        }
                        //保存
                        file.SaveAs(path);

                        delfileName = fileName;
                        apath = path;
                    }
                }
            }
            // 读取文件。NPOI 读取数据 
            DataTable dt = Import(apath, 1);
          
            //利用完文件  、删除文件  ////////////
            //UpLoadFile.DeleteFile(Server.MapPath("~/upload"), delfileName);
            System.IO.File.Delete(apath);//每次上传完毕删除文件

            //重新显示
            ImportEquipmentViewModel model = new ImportEquipmentViewModel
            {
                List = dt,
                Message = "显示列表"
            };

            //返回显示值 
            return View("ImportEquipment",model);
        }


        /// <summary>读取excel
        /// 默认第一行为表头
        /// </summary>
        /// <param name="strFileName">excel文档绝对路径</param>
        /// <param name="rowIndex">内容行偏移量，第一行为表头，内容行从第二行开始则为1</param>
        /// <returns></returns>
        public static DataTable Import(string strFileName, int rowIndex)
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



        /// <summary>
        /// 导入数据
        /// </summary>
        /// <returns></returns>
        public ActionResult Import(string[] Ecode, string[] SortID, string[] IP, string[] Place, string[] DeptID, string[] UserName, string[] StatusID, string[] Brand, string[] Version, string[] Remark)
        {

            if (Ecode == null || Ecode.Length <= 0)
            {
                return PromptView("导入格式错误，请检查!Error：无编码信息" );
            }

            //开启事务管理
            using (TransactionScope sc = new TransactionScope())
            {
                try
                {
                    for (int i = 0; i < Ecode.Length; i++)
                    {
                        //Excel行号 用于提示错误信息
                        int e = i + 2;

                        int? sortId = BaseSorts.GetSortId(SortID[i].ToString());
                        int? deptId = BaseDepts.GetDeptId(DeptID[i].ToString());
                        int statusId = BaseStatuss.GetStatusId(StatusID[i].ToString());

                        if (statusId == 0)
                        {
                            // 错误

                            return PromptView("导入格式错误，请检查!Error：状态信息不正确 !EXCEL行号:" + e.ToString());
                        }

                        Equipment model = new Equipment
                        {
                            Ecode = Ecode[i].ToString(),
                            SortID = sortId,
                            DeptID = deptId,
                            IP = IP[i].ToString(),
                            Place = Place[i].ToString(),
                            UserName = UserName[i].ToString(),
                            StatusID = statusId,
                            Brand = Brand[i].ToString(),
                            Version = Version[i].ToString(),
                            InputUserID = WorkContext.Uid,
                            UpdateTime = DateTime.Now,
                            Remark = Remark[i].ToString()
                        };

                        //检验数据 ，有则更新 无则插入
                        bool IsExist = false;

                        int sid = 0;

                        IsExist = Equipments.IsExist(Ecode[i].ToString());

                        if (IsExist)
                        {
                            //修改
                            sid = Equipments.GetEquipmentId(Ecode[i].ToString());

                            Equipments.UpdateModel(model, sid);
                        }
                        else
                        {
                            //新增
                            Equipments.AddModel(model);
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
            
            






 

            return PromptView("导入成功!");

        }


  

    } 
} 