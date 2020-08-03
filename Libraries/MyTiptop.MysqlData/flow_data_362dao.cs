using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
 
using System.Data.SqlClient;


namespace MyTiptop.MysqlData
{
    public partial class flow_data_362dao
    {
        static string connectStr = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConStr"].ConnectionString;

        //根据证件号，查询来访纪录
        public static flow_data_362 findByIdNumber(string idNumber)
        {
            flow_data_362 model;

            //证件号码最少输入6位 
            if (idNumber.Length < 6)
            {
                return null;
            }

            //并没有建立数据库连接
            MySqlConnection conn = new MySqlConnection(connectStr);
            try
            {
                conn.Open();    //建立连接，打开数据库
                idNumber = idNumber.Replace("'", "‘"); //防一下sql注入 
                string today = DateTime.Today.ToString("yyyy-MM-dd");//日期格式要确保与mysql存储格式一致。

               // string sqlstr = "select * from flow_data_362 where data_11 = '" + idNumber + "' and  left(data_6,10) = '" + today + "'";   //SQL语句
                string sqlstr = "select * from flow_data_362 where data_11 like '%" + idNumber + "%' and  left(data_6,10) = '" + today + "'";   //SQL语句
                MySqlCommand cmd = new MySqlCommand(sqlstr, conn);

                //相当于数据读出流  理解为一本书
                MySqlDataReader reader = cmd.ExecuteReader();
                //reader.Read();  //读取下一页数据 ，读取成功，返回true，下一页没有数据则返回false表示到了最后一页

                if (reader.Read())   //遍历表中数据,只取第一条记录
                {
                    //Console.WriteLine(reader.GetInt32("id") + reader.GetString("name") + reader.GetString("age"));
                    //封装属性
                    model = new flow_data_362
                    {
                        #region 封装属性
                        id = reader.GetInt32("id"),
                        run_id = reader.GetInt32("run_id"),
                        begin_time = reader.GetDateTime("begin_time"),
                        begin_user = reader.GetString("begin_user"),
                        data_10 = reader.GetString("data_10"),
                        data_11 = reader.GetString("data_11"),
                        data_12 = reader.GetString("data_12"),
                        data_13 = reader.GetString("data_13"),
                        data_14 = reader.GetString("data_14"),
                        data_15 = reader.GetString("data_15"),
                        data_16 = reader.GetString("data_16"),
                        data_17 = reader.GetString("data_17"),
                        data_18 = reader.GetString("data_18"),
                        data_19 = reader.GetString("data_19"),
                        data_20 = reader.GetString("data_20"),
                        data_21 = reader.GetString("data_21"),
                        data_22 = reader.GetString("data_22"),
                        data_22_key = reader.GetString("data_22_key"),
                        data_23 = reader.GetString("data_23"),
                        data_24 = reader.GetString("data_24"),
                        data_25 = reader.GetString("data_25"),
                        data_27 = reader.GetString("data_27"),
                        data_29 = reader.GetString("data_29"),
                        data_31 = reader.GetString("data_31"),
                        data_32 = reader.GetString("data_32"),
                        data_33 = reader.GetString("data_33"),
                        data_36 = reader.GetString("data_36"),
                        data_38 = reader.GetString("data_38"),
                        data_4 = reader.GetString("data_4"),
                        data_40 = reader.GetString("data_40"),
                        data_41 = reader.GetString("data_41"),
                        data_43 = reader.GetString("data_43"),
                        data_44 = reader.GetString("data_44"),
                        data_45 = reader.GetString("data_45"),
                        data_5 = reader.GetString("data_5"),
                        data_6 = reader.GetString("data_6"),
                        data_8 = reader.GetString("data_8"),
                        data_9 = reader.GetString("data_9"),
                        flow_auto_num = reader.GetInt32("flow_auto_num"),
                        flow_auto_num_month = reader.GetInt32("flow_auto_num_month"),
                        flow_auto_num_year = reader.GetInt32("flow_auto_num_year"),
                        run_name = reader.GetString("run_name")
                        #endregion
                    };
                }
                else
                {
                    model = null;
                }
                if (!reader.IsClosed)
                {
                    reader.Close();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
            finally
            {
                conn.Close();   //关闭连接
            }
            return model;
        }
        
        /// <summary>
        /// 根据 来访登记卡，查询来访纪录. 用于登记
        /// </summary>
        /// <param name="card_no"></param>
        /// <returns></returns>
        public static flow_data_362 findByCard(string card_no)
        {
            flow_data_362 model;

            //并没有建立数据库连接
            MySqlConnection conn = new MySqlConnection(connectStr);
            try
            {
                conn.Open();    //建立连接，打开数据库
                card_no = card_no.Replace("'", "‘"); //防一下sql注入 

                string sqlstr = "select * from flow_data_362 where data_44 = '" + card_no + "' and LENGTH(data_33) < 1  ";   //SQL语句
                MySqlCommand cmd = new MySqlCommand(sqlstr, conn);

                //相当于数据读出流  理解为一本书
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())   //遍历表中数据,只取第一条记录
                {
                    //封装属性
                    model = new flow_data_362
                    {
                        #region 封装属性
                        id = reader.GetInt32("id"),
                        run_id = reader.GetInt32("run_id"),
                        begin_time = reader.GetDateTime("begin_time"),
                        begin_user = reader.GetString("begin_user"),
                        data_10 = reader.GetString("data_10"),
                        data_11 = reader.GetString("data_11"),
                        data_12 = reader.GetString("data_12"),
                        data_13 = reader.GetString("data_13"),
                        data_14 = reader.GetString("data_14"),
                        data_15 = reader.GetString("data_15"),
                        data_16 = reader.GetString("data_16"),
                        data_17 = reader.GetString("data_17"),
                        data_18 = reader.GetString("data_18"),
                        data_19 = reader.GetString("data_19"),
                        data_20 = reader.GetString("data_20"),
                        data_21 = reader.GetString("data_21"),
                        data_22 = reader.GetString("data_22"),
                        data_22_key = reader.GetString("data_22_key"),
                        data_23 = reader.GetString("data_23"),
                        data_24 = reader.GetString("data_24"),
                        data_25 = reader.GetString("data_25"),
                        data_27 = reader.GetString("data_27"),
                        data_29 = reader.GetString("data_29"),
                        data_31 = reader.GetString("data_31"),
                        data_32 = reader.GetString("data_32"),
                        data_33 = reader.GetString("data_33"),
                        data_36 = reader.GetString("data_36"),
                        data_38 = reader.GetString("data_38"),
                        data_4 = reader.GetString("data_4"),
                        data_40 = reader.GetString("data_40"),
                        data_41 = reader.GetString("data_41"),
                        data_43 = reader.GetString("data_43"),
                        data_44 = reader.GetString("data_44"),
                        data_45 = reader.GetString("data_45"),
                        data_5 = reader.GetString("data_5"),
                        data_6 = reader.GetString("data_6"),
                        data_8 = reader.GetString("data_8"),
                        data_9 = reader.GetString("data_9"),
                        flow_auto_num = reader.GetInt32("flow_auto_num"),
                        flow_auto_num_month = reader.GetInt32("flow_auto_num_month"),
                        flow_auto_num_year = reader.GetInt32("flow_auto_num_year"),
                        run_name = reader.GetString("run_name")
                        #endregion
                    };
                }
                else
                {
                    model = null;
                }
                if (!reader.IsClosed)
                {
                    reader.Close();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
            finally
            {
                conn.Close();   //关闭连接
            }

            return model;

        }

       
        /// <summary>
        /// 根据 来访登记卡，查询来访纪录.  登记完用于显示  
        /// </summary>
        /// <param name="card_no"></param>
        /// <returns></returns>
        public static flow_data_362 findByCardAndDate(string card_no)
        {
            flow_data_362 model;

            //并没有建立数据库连接
            MySqlConnection conn = new MySqlConnection(connectStr);
            try
            {
                conn.Open();    //建立连接，打开数据库
                card_no = card_no.Replace("'", "‘"); //防一下sql注入 

                string today = DateTime.Today.ToString("yyyy-MM-dd");//日期格式要确保与mysql存储格式一致。

                string sqlstr = "select * from flow_data_362 where data_44 = '" + card_no + "' and   left(data_6,10) = '" + today + "'  ORDER BY  run_id desc  ";   //SQL语句
                MySqlCommand cmd = new MySqlCommand(sqlstr, conn);

                //相当于数据读出流  理解为一本书
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())   //遍历表中数据,只取第一条记录
                {
                    //封装属性
                    model = new flow_data_362
                    {
                        #region 封装属性
                        id = reader.GetInt32("id"),
                        run_id = reader.GetInt32("run_id"),
                        begin_time = reader.GetDateTime("begin_time"),
                        begin_user = reader.GetString("begin_user"),
                        data_10 = reader.GetString("data_10"),
                        data_11 = reader.GetString("data_11"),
                        data_12 = reader.GetString("data_12"),
                        data_13 = reader.GetString("data_13"),
                        data_14 = reader.GetString("data_14"),
                        data_15 = reader.GetString("data_15"),
                        data_16 = reader.GetString("data_16"),
                        data_17 = reader.GetString("data_17"),
                        data_18 = reader.GetString("data_18"),
                        data_19 = reader.GetString("data_19"),
                        data_20 = reader.GetString("data_20"),
                        data_21 = reader.GetString("data_21"),
                        data_22 = reader.GetString("data_22"),
                        data_22_key = reader.GetString("data_22_key"),
                        data_23 = reader.GetString("data_23"),
                        data_24 = reader.GetString("data_24"),
                        data_25 = reader.GetString("data_25"),
                        data_27 = reader.GetString("data_27"),
                        data_29 = reader.GetString("data_29"),
                        data_31 = reader.GetString("data_31"),
                        data_32 = reader.GetString("data_32"),
                        data_33 = reader.GetString("data_33"),
                        data_36 = reader.GetString("data_36"),
                        data_38 = reader.GetString("data_38"),
                        data_4 = reader.GetString("data_4"),
                        data_40 = reader.GetString("data_40"),
                        data_41 = reader.GetString("data_41"),
                        data_43 = reader.GetString("data_43"),
                        data_44 = reader.GetString("data_44"),
                        data_45 = reader.GetString("data_45"),
                        data_5 = reader.GetString("data_5"),
                        data_6 = reader.GetString("data_6"),
                        data_8 = reader.GetString("data_8"),
                        data_9 = reader.GetString("data_9"),
                        flow_auto_num = reader.GetInt32("flow_auto_num"),
                        flow_auto_num_month = reader.GetInt32("flow_auto_num_month"),
                        flow_auto_num_year = reader.GetInt32("flow_auto_num_year"),
                        run_name = reader.GetString("run_name")
                        #endregion
                    };
                }
                else
                {
                    model = null;
                }
                if (!reader.IsClosed)
                {
                    reader.Close();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
            finally
            {
                conn.Close();   //关闭连接
            }

            return model;

        }


        /// <summary>
        /// 更新来访实际人数
        /// </summary>
        /// <param name="idNumber"></param>
        /// <param name="data43"></param>
        /// <returns></returns>
        public static int updateData43(string idNumber, int data43)
        {
            //1.查询出数据。
            //2. 符合条件则更新数据
            int returnValue = 0;
            flow_data_362 model = findByIdNumber(idNumber);
            if (model == null)
            {
                return 0;
            }
            //并没有建立数据库连接
            MySqlConnection conn = new MySqlConnection(connectStr);
            try
            {
                conn.Open();    //建立连接，打开数据库
                idNumber = idNumber.Replace("'", "‘"); //防一下sql注入 
                string today = DateTime.Today.ToString("yyyy-MM-dd");//日期格式要确保与mysql存储格式一致。

                //string sqlstr = "update flow_data_362 set  data_43 =" + data43 + " where data_11 = '" + idNumber + "' and  left(data_6,10) = '" + today + "'";   //SQL语句
                string sqlstr = "update flow_data_362 set  data_43 =" + data43 + " where data_11 = '" + model.data_11 + "' and  left(data_6,10) = '" + today + "'";   //SQL语句

                MySqlCommand cmd = new MySqlCommand(sqlstr, conn);

                int i = cmd.ExecuteNonQuery();

                if (i > 0)   //遍历表中数据,只取第一条记录
                {
                    returnValue = data43;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                conn.Close();   //关闭连接
            }
            return returnValue;
        }
    
    
         
        /// <summary>
        /// 更新来访车牌
        /// </summary>
        /// <param name="idNumber"></param>
        /// <param name="data45"></param>
        /// <returns></returns>
        public static string updateData45(string idNumber, string data45)
        {
            //1.查询出数据。
            //2. 符合条件则更新数据
            string returnValue = "";
            flow_data_362 model = findByIdNumber(idNumber);
            if (model == null)
            {
                return "";
            }
            //并没有建立数据库连接
            MySqlConnection conn = new MySqlConnection(connectStr);
            try
            {
                conn.Open();    //建立连接，打开数据库
                idNumber = idNumber.Replace("'", "‘"); //防一下sql注入 
                data45 = data45.Replace("'", "‘");
                string today = DateTime.Today.ToString("yyyy-MM-dd");//日期格式要确保与mysql存储格式一致。


                //string sqlstr = "update flow_data_362 set  data_45 ='" + data45 + "'  where data_11 = '" + idNumber + "' and  left(data_6,10) = '" + today + "'";   //SQL语句
                string sqlstr = "update flow_data_362 set  data_45 ='" + data45 + "'  where data_11 = '" + model.data_11 + "' and  left(data_6,10) = '" + today + "'";   //SQL语句

                MySqlCommand cmd = new MySqlCommand(sqlstr, conn);

                int i = cmd.ExecuteNonQuery();

                if (i > 0)   //遍历表中数据,只取第一条记录
                {
                    returnValue = data45;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                conn.Close();   //关闭连接
            }
            return returnValue;
        }


        //更新离场日期
        public static int updateOutTime(string card_no)
        {
            //1.查询出数据。
            //2. 符合条件则更新数据
            int returnValue = 0;
            flow_data_362 model = findByCard(card_no);
            if (model == null)
            {
                return 0;
            }
            //并没有建立数据库连接
            MySqlConnection conn = new MySqlConnection(connectStr);
            try
            {
                conn.Open();    //建立连接，打开数据库
                card_no = card_no.Replace("'", "‘"); //防一下sql注入 
                string today = DateTime.Today.ToString("yyyy-MM-dd");//日期格式要确保与mysql存储格式一致。

                //离厂时间
                string data33 = DateTime.Now.ToString();

                string sqlstr = "update flow_data_362 set  data_33 ='" + data33 + "' where data_44 = '" + card_no + "' and LENGTH(data_33) < 1 ";   //SQL语句
                MySqlCommand cmd = new MySqlCommand(sqlstr, conn);

                returnValue = cmd.ExecuteNonQuery();              
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                conn.Close();   //关闭连接
            }
            return returnValue;
        }
        
        /// <summary>
        /// 更新入厂 来访卡号、日期
        /// </summary>
        /// <param name="idNumber"></param>
        /// <param name="card_no"></param>
        /// <returns></returns>
        public static int updateInTime(string idNumber,string card_no)
        {
            //1.查询出数据。
            //2. 符合条件则更新数据
            int returnValue = 0;
            flow_data_362 model = findByIdNumber(idNumber);
            if (model == null)
            {
                return 0;
            }
            //要不要先判断此来访申请单的状态、此来访卡的状态？ 、、暂时先不判断
            //并没有建立数据库连接
            MySqlConnection conn = new MySqlConnection(connectStr);
            try
            {
                conn.Open();    //建立连接，打开数据库
                idNumber = idNumber.Replace("'", "‘"); //防一下sql注入 
                string today = DateTime.Today.ToString("yyyy-MM-dd");//日期格式要确保与mysql存储格式一致。

                //进厂时间
                string data40 = DateTime.Now.ToString();

                //string sqlstr = "update flow_data_362 set  data_44 ='" + card_no + "',data_40 = '" + data40 + "'  where data_11 = '" + idNumber + "' and  left(data_6,10) = '" + today + "'";   //SQL语句
                string sqlstr = "update flow_data_362 set  data_44 ='" + card_no + "',data_40 = '" + data40 + "'  where data_11 = '" + model.data_11 + "' and  left(data_6,10) = '" + today + "'";   //SQL语句

                MySqlCommand cmd = new MySqlCommand(sqlstr, conn);

                returnValue = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                conn.Close();   //关闭连接
            }
            return returnValue;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public partial class guest_carddao
    {
        static string connectStr = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConStr"].ConnectionString;
        //获取 来访卡 信息
        public static guest_card GetModel(string card_no)
        {
            guest_card model;
            //并没有建立数据库连接
            MySqlConnection conn = new MySqlConnection(connectStr);
            try
            {
                conn.Open();    //建立连接，打开数据库
                card_no = card_no.Replace("'", "‘"); //防一下sql注入 
                string sqlstr = "select * from guest_card where card_no = '" + card_no + "' ";   //SQL语句
                MySqlCommand cmd = new MySqlCommand(sqlstr, conn);
                //相当于数据读出流  理解为一本书
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())   //遍历表中数据,只取第一条记录
                {
                    //Console.WriteLine(reader.GetInt32("id") + reader.GetString("name") + reader.GetString("age"));
                    //封装属性
                    model = new guest_card
                    {
                        //封装属性
                        card_id = reader.GetInt32("card_id"),
                        card_no = reader.GetString("card_no"),
                        card_flag = reader.GetInt32("card_flag")
                    };
                }
                else
                {
                    model = null;
                }
                if (!reader.IsClosed)
                {
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
            finally
            {
                conn.Close();   //关闭连接
            }
            return model;
        }
       
        /// <summary>
        /// 更新 来访卡 状态
        /// </summary>
        /// <param name="card_no"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static int updateFlag(string card_no, int flag)
        {
            //1.查询出数据。
            //2. 符合条件则更新数据
            int returnValue = 0;
            guest_card model = GetModel(card_no);
            if (model == null)
            {
                return -1;
            }
            //并没有建立数据库连接
            MySqlConnection conn = new MySqlConnection(connectStr);
            try
            {
                conn.Open();    //建立连接，打开数据库
                card_no = card_no.Replace("'", "‘"); //防一下sql注入 
                string sqlstr = "update guest_card set  card_flag ='" + flag + "'  where card_no = '" + card_no + "' ";   //SQL语句
                MySqlCommand cmd = new MySqlCommand(sqlstr, conn);
                returnValue = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                conn.Close();   //关闭连接
            }
            return returnValue;
        }

    }

    /// <summary>
    /// 关联来访车辆信息
    /// </summary>
    public partial class tccdao
    {
        static string connectStr = System.Configuration.ConfigurationManager.ConnectionStrings["TccConStr"].ConnectionString;

        //获取当天进厂车次信息。时间倒序，前5辆车
        public static string getParkInCodeList()
        {
            SqlConnection conn = new SqlConnection(connectStr);
            string ret = "";
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                //输入参数
                //SqlParameter parameter = new SqlParameter { ParameterName = "@RoleId", Value = roleId };
                //cmd.Parameters.Add(parameter);
                //string cmdStr = "select [xpGrid_Functions].* , isnull(CheckedFunc.checked, 0) as checked from[dbo].[xpGrid_Functions] left join (select FuncCode,1 as checked from[dbo].[xpGrid_FuncsInRoles] inner join[xpGrid_Role] on[xpGrid_FuncsInRoles].RoleId = [xpGrid_Role].roleid where[xpGrid_Role].RoleId =@RoleId ) CheckedFunc on[xpGrid_Functions].FuncCode = CheckedFunc.FuncCode";
                string cmdStr = "select top 5  车牌号码,入场时间  from Park_IN  where  DateDiff(dd,入场时间,getdate())=0  order by 入场时间 desc  ";
                cmd.CommandText = cmdStr;
                //执行填充Table
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    ret = "[";
                    foreach (DataRow row in dt.Rows)
                    {
                        if (ret.Length > 1) ret += ",";
                        ret += string.Format("{{\"label\":\"{0}\",\"value\":\"{0}\"}}", row["车牌号码"].ToString().TrimEnd());
                    }
                    ret += "]";
                    //[{"label": "博客园", "value": "cnblogs"}, {"label": "囧月", "value": "囧月"}]
                }
            }
            finally
            {
                //手动关闭连接
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    conn = null;
                }
            }
            return ret;
        }

        /// <summary>
        /// 模糊查询产品编码列表
        /// </summary>
        /// <param name="prdtName"></param>
        /// <param name="top">最多取多少位</param>
        /// <returns></returns>
        //public static string GetPrdtCodeList(string prdtCode, int top)
        //{
        //    using (QASystemDBContext dbContext = new QASystemDBContext())
        //    {
        //        var List = from m in dbContext.Bl_Produce_Prdt
        //                   where m.PrdtCode.Contains(prdtCode)
        //                   select m;
        //        string ret = "";
        //        if (List != null)
        //        {
        //            int i = 0;
        //            ret = "[";
        //            foreach (Bl_Produce_Prdt prdt in List)
        //            {
        //                if (i < top)
        //                {
        //                    if (ret.Length > 1) ret += ",";
        //                    ret += string.Format("{{\"label\":\"{0}\",\"value\":\"{0}\"}}", prdt.PrdtCode.TrimEnd());
        //                    //计数 
        //                    i++;
        //                }
        //            }
        //            ret += "]";
        //        }
        //        //[{"label": "博客园", "value": "cnblogs"}, {"label": "囧月", "value": "囧月"}]
        //        return ret;
        //    }
        //}


    }
}
