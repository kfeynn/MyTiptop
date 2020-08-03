using System;
using System.Collections;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyTiptop.Core;
using System.Data.SqlClient;


namespace MyTiptop.Data
{
    /// <summary>
    ///  
    /// </summary>
    public partial class RunGridViewConditions
    {

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name=" "></param>
        /// <returns></returns>
        public static bool IsExists(int sid)
        {
            using (DBContext dbContext = new DBContext())
            {
                //精确匹配名称
                var model = dbContext.Base_RunGridView_Condition.Where(u => u.id == sid).FirstOrDefault();
                if (model != null)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static Base_RunGridView_Condition GetModel(int sid)
        {
            using (DBContext dbContext = new DBContext())
            {
                var model = dbContext.Base_RunGridView_Condition.Where(u => u.id == sid).FirstOrDefault();
                return model;
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool AddModel(Base_RunGridView_Condition model)
        {
            bool returnFlag = false;
            using (DBContext dbContext = new DBContext())
            {
                //增加
                dbContext.Base_RunGridView_Condition.Add(model);
                dbContext.SaveChanges();
                returnFlag = true;
            }
            return returnFlag;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static bool DeleteModel(int sid)
        {
            bool returnFlag = false;
            using (DBContext dbContext = new DBContext())
            {
                var model = dbContext.Base_RunGridView_Condition.Where(u => u.id == sid).FirstOrDefault();
                if (model != null)
                {
                    dbContext.Base_RunGridView_Condition.Remove(model);

                    dbContext.SaveChanges();

                    returnFlag = true;
                }
            }
            return returnFlag;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <param name="sid"></param>
        public static void UpdateModel(Base_RunGridView_Condition model, int sid)
        {
            using (DBContext dbContext = new DBContext())
            {
                var editmodel = dbContext.Base_RunGridView_Condition.Where(u => u.id == sid).FirstOrDefault();
                if (editmodel == null)
                {
                    return;//空
                }
                editmodel.Base_RunGridView_Id = model.Base_RunGridView_Id;
                editmodel.inputtcii = model.inputtcii;
                editmodel.datetype = model.datetype;
                editmodel.iOperator = model.iOperator;
                editmodel.type = model.type;
                editmodel.editFormat = model.editFormat;
                editmodel.field = model.field;
                editmodel.fieldName = model.fieldName;
                editmodel.Remark = model.Remark;
                editmodel.formName = model.formName;
                

                //提交修改 
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public static List<Base_RunGridView_Condition> GetList()
        {
            using (DBContext dbContext = new DBContext())
            {
                return dbContext.Base_RunGridView_Condition.ToList();
            }
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public static List<Base_RunGridView_Condition> GetList(int? gridId)
        {
            using (DBContext dbContext = new DBContext())
            {
                return dbContext.Base_RunGridView_Condition.Where(u => u.Base_RunGridView_Id == gridId).ToList();
            }
        }


        /// <summary>
        /// 根据operator，返回下拉操作符 html
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetHtmlByOperator(string type, string formName, string oper, string currOper)
        {
            // key : name 
            // oper : operator   split ','

            string htmlStr = "<select id = \"" + formName + "\" name = \"" + formName + "\"  style=\"width: 100px;\">";
            //htmlStr += "<option value = \"\"></option>";  //不要默认项

            if (oper.Length > 0)  //type == "1"  && 
            {
                string[] list = oper.Split(',');

                foreach (string o in list)
                {
                    if (currOper != null && o == currOper)
                    {
                        //选中值
                        htmlStr += "<option selected = \"selected\"   value = \"" + o + "\">" + o + "</option>";
                    }
                    else
                    {
                        htmlStr += "<option  value = \"" + o + "\">" + o + "</option>";
                    }
                }
            }
            htmlStr += "</ select >";

            //<select id = "gridId" name = "gridId" >
            //   < option value = "1000" ></ option >
            //   < option selected = "selected" value = "1001" > 精密销售额数据 </ option >
            //</select >

            return htmlStr;
        }


        #region 
        /// <summary>
        /// 根据datetype，返回输入框text的html代码
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetTextByDatatype(string type, string formName, string datetype, int? inputtcii, string currValue)
        {
            formName = "txt" + formName;    // textbox  固定前缀 .解析的时候也要考虑这个


            string request = "";

            if (inputtcii != null && inputtcii == 1)
            {
                request = "  required  ";
            }

            if (currValue == null)
                currValue = "";

            string htmlStr = "";

            //if (type == "1")
            //{
            switch (datetype)
            {
                case "datetime":
                    htmlStr = "<input type=\"text\" id=\"" + formName + "\" name=\"" + formName + "\"" + request + " onfocus=\"WdatePicker({ dateFmt: 'yyyy-MM-dd'})\" class=\"Wdate\" value=\"" + currValue + "\" />";
                    break;

                case "varchar":
                    htmlStr = "<input type=\"text\" id=\"" + formName + "\" name=\"" + formName + "\"" + request + "  size=\"35\" value=\"" + currValue + "\" />";
                    break;

                case "select":
                    //下拉菜单 。 currValue


                    break;

                case "check":
                    //单选框  currValue

                    break;

                default:
                    break;
            }
            //}

            //<input height="18" id="roleName" name="roleName" type="text" value="" />
            //<input type="text" id="t" name="t" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})" class="Wdate" value="" />
            //<input class="input"  id="Single_productName"  name="Single.productName" required="True" size="35" type="text" value="" /> 

            return htmlStr;
        }

        #endregion 


        public static string GetTextByConditionId(int sid, string currValue)
        {
            var model = GetModel(sid);
            if (model == null)
                return "";
            string formName = "txt" + model.formName;    // textbox  固定前缀 .解析的时候也要考虑这个
            string request = "";
            if (model.inputtcii != null && model.inputtcii == 1)
            {
                request = "  required  ";
            }
            if (currValue == null)    //当前值
                currValue = "";
            string htmlStr = "";
            switch (model.datetype)
            {
                case "datetime":
                    htmlStr = "<input type=\"text\" id=\"" + formName + "\" name=\"" + formName + "\"" + request + " onfocus=\"WdatePicker({ dateFmt: 'yyyy-MM-dd'})\" class=\"Wdate\" value=\"" + currValue + "\" />";
                    break;
                case "varchar":
                    htmlStr = "<input type=\"text\" id=\"" + formName + "\" name=\"" + formName + "\"" + request + "  size=\"35\" value=\"" + currValue + "\" />";
                    break;
                case "select":
                    #region 下拉菜单
                    htmlStr = "<select id = \"" + formName + "\" name = \"" + formName + "\"  style=\"width: 100px;\">";
                    if (model.inputtcii != 1)
                    {
                        htmlStr += "<option value = \"\"></option>";
                    }
                    // 查询得到table   model.editFormat  
                    DataTable dt = DBQuery.GetCommonQuery(model.editFormat);   //此table是固定 keyval ，keytext 两列
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow o in dt.Rows)
                        {
                            if (currValue != null && o[0].ToString() == currValue)
                            {
                                //选中值
                                htmlStr += "<option selected = \"selected\"   value = \"" + o[0] + "\">" + o[1] + "</option>";
                            }
                            else
                            {
                                htmlStr += "<option  value = \"" + o[0] + "\">" + o[1] + "</option>";
                            }
                        }
                    }
                    htmlStr += "</ select >";
                    #endregion 
                    break;

                case "check":
                    //单选框  currValue
                    break;

                default:
                    break;
            }
            return htmlStr;
        }

        /// <summary>
        /// 根据datetype，返回输入框text的html代码
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetTextByinputtcii(int? inputtcii)
        {
            string htmlStr = "";

            if (inputtcii != null && inputtcii == 1)
            {
                htmlStr = "<span><font color='red'>*必输项*</font></span>";
            }


            return htmlStr;
        }


    }

    public partial class BaseRunGridViews
    {

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name=" "></param>
        /// <returns></returns>
        public static bool IsExists(int sid)
        {
            using (DBContext dbContext = new DBContext())
            {
                //精确匹配名称
                var model = dbContext.Base_RunGridView.Where(u => u.BaseRunGridViewID == sid).FirstOrDefault();
                if (model != null)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static Base_RunGridView GetModel(int sid)
        {
            using (DBContext dbContext = new DBContext())
            {
                var model = dbContext.Base_RunGridView.Where(u => u.BaseRunGridViewID == sid).FirstOrDefault();
                return model;
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool AddModel(Base_RunGridView model)
        {
            bool returnFlag = false;
            using (DBContext dbContext = new DBContext())
            {
                //增加
                dbContext.Base_RunGridView.Add(model);
                dbContext.SaveChanges();
                returnFlag = true;
            }
            return returnFlag;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static bool DeleteModel(int sid)
        {
            bool returnFlag = false;
            using (DBContext dbContext = new DBContext())
            {
                var model = dbContext.Base_RunGridView.Where(u => u.BaseRunGridViewID == sid).FirstOrDefault();
                if (model != null)
                {
                    dbContext.Base_RunGridView.Remove(model);

                    dbContext.SaveChanges();

                    returnFlag = true;
                }
            }
            return returnFlag;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <param name="sid"></param>
        public static void UpdateModel(Base_RunGridView model, int sid)
        {
            using (DBContext dbContext = new DBContext())
            {
                var editmodel = dbContext.Base_RunGridView.Where(u => u.BaseRunGridViewID == sid).FirstOrDefault();
                if (editmodel == null)
                {
                    return;//空
                }
                editmodel.DBType = model.DBType;
                editmodel.GridViewName = model.GridViewName;
                editmodel.keyWord = model.keyWord;
                editmodel.strCondition = model.strCondition;
                editmodel.strSelect = model.strSelect;
                editmodel.Type = model.Type;
                editmodel.ViewOrder = model.ViewOrder;
                editmodel.SqlType = model.SqlType;

                //提交修改 
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public static List<Base_RunGridView> GetList()
        {
            using (DBContext dbContext = new DBContext())
            {
                return dbContext.Base_RunGridView.ToList();
            }
        }

        /// <summary>
        /// 获取状态列表。JSon 格式
        /// </summary>
        /// <param name="top">最多取多少位</param>
        /// <returns></returns>
        public static string GetJsonList(int top)
        {
            using (DBContext dbContext = new DBContext())
            {
                var List = from m in dbContext.Base_RunGridView select m;
                string ret = "";
                if (List != null)
                {
                    int i = 0;
                    ret = "[";
                    foreach (Base_RunGridView s in List)
                    {
                        if (i < top)
                        {
                            if (ret.Length > 1) ret += ",";
                            ret += string.Format("{{\"id\":\"{0}\",\"text\":\"{1}\"}}", s.BaseRunGridViewID, s.GridViewName);
                            //计数 
                            i++;
                        }
                    }
                    ret += "]";
                }
                #region 
                /*
                    [
                        { "id": "usa", "text": "美国" },
                        { "id": "cn", "text": "中国" },
                        { "id": "jp", "text": "日本" }
                    ]
                */
                #endregion 
                return ret;
            }
        }


        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public static List<Base_RunGridView_Condition> GetList(int? gridId)
        {
            using (DBContext dbContext = new DBContext())
            {
                return dbContext.Base_RunGridView_Condition.Where(u => u.Base_RunGridView_Id == gridId).ToList();
            }
        }


        /// <summary>
        /// 根据operator，返回下拉操作符 html
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetHtmlByOperator(string type, string formName, string oper)
        {
            // key : name 
            // oper : operator   split ','

            string htmlStr = "<select id = \"" + formName + "\" name = \"" + formName + "\"  style=\"width: 100px;\">";
            htmlStr += "<option value = \"\"></option>";

            if (type == "1" && oper.Length > 0)
            {
                string[] list = oper.Split(',');

                foreach (string o in list)
                {
                    htmlStr += "<option  value = \"" + o + "\">" + o + "</option>";
                }
            }
            htmlStr += "</ select >";

            //<select id = "gridId" name = "gridId" >
            //   < option value = "1000" ></ option >
            //   < option selected = "selected" value = "1001" > 精密销售额数据 </ option >
            //</select >

            return htmlStr;
        }


        /// <summary>
        /// 根据datetype，返回输入框text的html代码
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetTextByDatatype(string type, string formName, string datetype, int? inputtcii)
        {
            formName = "txt" + formName;    // textbox  固定前缀 .解析的时候也要考虑这个


            string request = "";

            if (inputtcii != null && inputtcii == 1)
            {
                request = "  required  ";
            }

            string htmlStr = "";

            if (type == "1")
            {
                switch (datetype)
                {
                    case "datetime":
                        htmlStr = "<input type=\"text\" id=\"" + formName + "\" name=\"" + formName + "\"" + request + " onfocus=\"WdatePicker({ dateFmt: 'yyyy-MM-dd'})\" class=\"Wdate\" value=\"\" />";
                        break;

                    case "varchar":
                        htmlStr = " <input type=\"text\" id=\"" + formName + "\" name=\"" + formName + "\"" + request + "  size=\"35\" value=\"\" /> ";
                        break;
                    default:
                        break;
                }
            }

            //<input height="18" id="roleName" name="roleName" type="text" value="" />
            //<input type="text" id="t" name="t" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})" class="Wdate" value="" />
            //<input class="input"  id="Single_productName"  name="Single.productName" required="True" size="35" type="text" value="" /> 

            return htmlStr;
        }
    }

    public partial class BaseQrys
    {

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name=" "></param>
        /// <returns></returns>
        public static bool IsExists(int sid)
        {
            using (DBContext dbContext = new DBContext())
            {
                //精确匹配名称
                var model = dbContext.Base_Qry.Where(u => u.id == sid).FirstOrDefault();
                if (model != null)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static Base_Qry GetModel(int sid)
        {
            using (DBContext dbContext = new DBContext())
            {
                var model = dbContext.Base_Qry.Where(u => u.id == sid).FirstOrDefault();
                return model;
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool AddModel(Base_Qry model)
        {
            bool returnFlag = false;
            using (DBContext dbContext = new DBContext())
            {
                //增加
                dbContext.Base_Qry.Add(model);
                dbContext.SaveChanges();
                returnFlag = true;
            }
            return returnFlag;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static bool DeleteModel(int sid)
        {
            bool returnFlag = false;
            using (DBContext dbContext = new DBContext())
            {
                var model = dbContext.Base_Qry.Where(u => u.id == sid).FirstOrDefault();
                if (model != null)
                {
                    dbContext.Base_Qry.Remove(model);
                    dbContext.SaveChanges();
                    returnFlag = true;
                }
            }
            return returnFlag;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <param name="sid"></param>
        public static void UpdateModel(Base_Qry model, int sid)
        {
            using (DBContext dbContext = new DBContext())
            {
                var editmodel = dbContext.Base_Qry.Where(u => u.id == sid).FirstOrDefault();
                if (editmodel == null)
                {
                    return;//空
                }
                editmodel.keyVal = model.keyVal;
                editmodel.keyText = model.keyText;
                editmodel.viewOrder = model.viewOrder;
                editmodel.viewType = model.viewType;
                editmodel.Remark = model.Remark;
                //提交修改 
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public static List<Base_Qry> GetList()
        {
            using (DBContext dbContext = new DBContext())
            {
                return dbContext.Base_Qry.ToList();
            }
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public static List<Base_Qry> GetList(string viewType)
        {
            using (DBContext dbContext = new DBContext())
            {
                return dbContext.Base_Qry.Where(u => u.viewType == viewType).ToList();
            }
        }

    }

    public partial class Complaints
    {
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name=" "></param>
        /// <returns></returns>
        public static bool IsExists(int sid)
        {
            using (DBContext dbContext = new DBContext())
            {
                //精确匹配名称
                var model = dbContext.QA_Complaint.Where(u => u.id == sid).FirstOrDefault();
                if (model != null)
                    return true;
                else
                    return false;
            }
        }
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static QA_Complaint GetModel(int sid)
        {
            using (DBContext dbContext = new DBContext())
            {
                var model = dbContext.QA_Complaint.Where(u => u.id == sid).FirstOrDefault();
                return model;
            }
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int AddModel(QA_Complaint model)
        {
            int returnSid = 0;
            using (DBContext dbContext = new DBContext())
            {
                //增加
                dbContext.QA_Complaint.Add(model);
                dbContext.SaveChanges();
                returnSid = model.id;  // 返回自增ID
            }
            return returnSid;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static bool DeleteModel(int sid)
        {
            bool returnFlag = false;
            using (DBContext dbContext = new DBContext())
            {
                var model = dbContext.QA_Complaint.Where(u => u.id == sid).FirstOrDefault();
                if (model != null)
                {
                    dbContext.QA_Complaint.Remove(model);
                    dbContext.SaveChanges();
                    returnFlag = true;
                }
            }
            return returnFlag;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <param name="sid"></param>
        public static void UpdateModel(QA_Complaint model, int sid)
        {
            using (DBContext dbContext = new DBContext())
            {
                var editmodel = dbContext.QA_Complaint.Where(u => u.id == sid).FirstOrDefault();
                if (editmodel == null)
                {
                    return;//空
                }
                editmodel.data01 = model.data01;
                editmodel.data02 = model.data02;
                editmodel.data03 = model.data03;
                editmodel.data04 = model.data04;
                editmodel.data05 = model.data05;
                editmodel.data06 = model.data06;
                editmodel.data07 = model.data07;
                editmodel.data08 = model.data08;
                editmodel.data09 = model.data09;
                editmodel.data10 = model.data10;
                editmodel.data11 = model.data11;
                editmodel.data12 = model.data12;
                editmodel.data13 = model.data13;
                editmodel.data14 = model.data14;
                editmodel.data15 = model.data15;
                //提交修改 
                dbContext.SaveChanges();
            }
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public static List<QA_Complaint> GetList()
        {
            using (DBContext dbContext = new DBContext())
            {
                return dbContext.QA_Complaint.ToList();
            }
        }
      
    }


    public partial class ComplaintAnnexs
    {
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name=" "></param>
        /// <returns></returns>
        public static bool IsExists(int sid)
        {
            using (DBContext dbContext = new DBContext())
            {
                //精确匹配名称
                var model = dbContext.QA_Complaint_annex.Where(u => u.id == sid).FirstOrDefault();
                if (model != null)
                    return true;
                else
                    return false;
            }
        }

        public static int IsExists(int data01,string data02)
        {
            using (DBContext dbContext = new DBContext())
            {
                //精确匹配名称
                var model = dbContext.QA_Complaint_annex.Where(u => u.data01 == data01 && u.data02 == data02).FirstOrDefault();
                if (model != null)
                    return model.id;
                else
                    return 0;
            }
        }
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static QA_Complaint_annex GetModel(int sid)
        {
            using (DBContext dbContext = new DBContext())
            {
                var model = dbContext.QA_Complaint_annex.Where(u => u.id == sid).FirstOrDefault();
                return model;
            }
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int AddModel(QA_Complaint_annex model)
        {
            int returnSid = 0;
            using (DBContext dbContext = new DBContext())
            {
                try
                {
                    //增加
                    dbContext.QA_Complaint_annex.Add(model);
                    dbContext.SaveChanges();
                    returnSid = model.id;  // 返回自增ID
                }
                catch (Exception ex)
                {
                    string aa = ex.Message;
                }
            }
            return returnSid;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static bool DeleteModel(int sid)
        {
            bool returnFlag = false;
            using (DBContext dbContext = new DBContext())
            {
                var model = dbContext.QA_Complaint_annex.Where(u => u.id == sid).FirstOrDefault();
                if (model != null)
                {
                    dbContext.QA_Complaint_annex.Remove(model);
                    dbContext.SaveChanges();
                    returnFlag = true;
                }
            }
            return returnFlag;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <param name="sid"></param>
        public static void UpdateModel(QA_Complaint_annex model, int sid)
        {
            using (DBContext dbContext = new DBContext())
            {
                var editmodel = dbContext.QA_Complaint_annex.Where(u => u.id == sid).FirstOrDefault();
                if (editmodel == null)
                {
                    return;//空
                }
                editmodel.data01 = model.data01;
                editmodel.data02 = model.data02;
                editmodel.data03 = model.data03;
                editmodel.data04 = model.data04;
                editmodel.data05 = model.data05;
                editmodel.data06 = model.data06;
                //提交修改 
                dbContext.SaveChanges();
            }
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public static List<QA_Complaint_annex> GetList()
        {
            using (DBContext dbContext = new DBContext())
            {
                return dbContext.QA_Complaint_annex.ToList();
            }
        }

        public static List<QA_Complaint_annex> GetList(int data01)
        {
            using (DBContext dbContext = new DBContext())
            {
                return dbContext.QA_Complaint_annex.Where(u => u.data01 == data01).ToList();
            }
        }

    }


    public partial class Logs
    {
 
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static Log GetModel(int sid)
        {
            using (DBContext dbContext = new DBContext())
            {
                var model = dbContext.Log.Where(u => u.id == sid).FirstOrDefault();
                return model;
            }
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int AddModel(Log model)
        {
            int returnSid = 0;
            using (DBContext dbContext = new DBContext())
            {
                //增加
                dbContext.Log.Add(model);
                dbContext.SaveChanges();
                returnSid = model.id;  // 返回自增ID
            }
            return returnSid;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static bool DeleteModel(int sid)
        {
            bool returnFlag = false;
            using (DBContext dbContext = new DBContext())
            {
                var model = dbContext.Log.Where(u => u.id == sid).FirstOrDefault();
                if (model != null)
                {
                    dbContext.Log.Remove(model);
                    dbContext.SaveChanges();
                    returnFlag = true;
                }
            }
            return returnFlag;
        }
        
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public static List<Log> GetList()
        {
            using (DBContext dbContext = new DBContext())
            {
                return dbContext.Log.ToList();
            }
        }

    }


}
