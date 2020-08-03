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
    public partial class flow_data_366dao
    {
        static string connectStr = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConStr"].ConnectionString;

        //根据证件号，查询来访纪录
        public static List<flow_data_366> findBy()
        {
            List <flow_data_366>  list = new List<flow_data_366>();
           

            //并没有建立数据库连接
            MySqlConnection conn = new MySqlConnection(connectStr);
            try
            {
                conn.Open();    //建立连接，打开数据库
                //idNumber = idNumber.Replace("'", "‘"); //防一下sql注入 
                string today = DateTime.Today.ToString("yyyy-MM-dd");//日期格式要确保与mysql存储格式一致。 

                //要求：
                //1， 离计划日期前7天开始到计划日期当天每天上午9点 自动生成清单以邮件的方式通知指定人员直至F7节点完成实际试模日期结束
                //指定人员邮箱：工程经理: 陈勇（yong.chen @grand-tec.com）温培钦（peiqin.wen @grand-tec.com）模具工程师：苏树仁（shuren.su @grand-tec.com）采购尹志华（zhihua.yin @grand-tec.com）

                //string sqlstr = "select id,data_6,data_11,data_12,data_235,datediff(date_format(data_235,'%Y-%m-%d'), CURDATE()) diff from TD_OA.flow_data_366 where date_format(data_235, '%Y-%m-%d') > DATE_SUB(CURDATE(), INTERVAL + 7 DAY) and(date_format(data_122, '%Y-%m-%d') is NULL or date_format(data_122, '%Y-%m-%d') > now())  ";
                string sqlstr = "select id,data_6,data_11,data_12,data_235,datediff( date_format(data_235,'%Y-%m-%d'),CURDATE()) diff from TD_OA.flow_data_366 where date_format(data_235, '%Y-%m-%d') > DATE_SUB(CURDATE(), INTERVAL + 7 DAY)  and(date_format(data_122, '%Y-%m-%d') is NULL or date_format(data_122, '%Y-%m-%d') > now())  ";

                MySqlCommand cmd = new MySqlCommand(sqlstr, conn);
               
                MySqlDataReader reader = cmd.ExecuteReader(); 
                
                 
                while (reader.Read())   //遍历表中数据 
                {
                    flow_data_366 model;
                    //Console.WriteLine(reader.GetInt32("id") + reader.GetString("name") + reader.GetString("age")); 
                    //封装属性  id,data_6,data_11,data_12,data_235,datediff(date_format(data_235,'%Y-%m-%d'), CURDATE()) diff 
                    model = new flow_data_366
                    { 
                        #region 封装属性
                        id = reader.GetInt32("id"),
                        data_6 = reader.GetString("data_6"),
                        data_11 = reader.GetString("data_11"),
                        data_12 = reader.GetString("data_12"),
                        data_235 = reader.GetString("data_235"),
                        diff = reader.GetString("diff"),
                 
                        #endregion
                    };

                    list.Add(model);
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
            return list;
        } 
        
    } 
}
