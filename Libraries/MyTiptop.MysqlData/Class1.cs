using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace MyTiptop.MysqlData
{
    public class Class1
    {
        public static DataTable getAllTest()
        {

            //测试连接Mysql数据库。 只有一张页面。直接用原始方式操作mysql数据库，不配置工具或EF。

            // string connectStr="Server=192.168.0.61;Database=TD_OA;User ID=root;Password=myoa888;port=3336;CharSet=utf8;pooling=true;";

            string connectStr = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConStr"].ConnectionString;

            // mysql_connect("localhost:3336/TD_OA ", "root", "myoa888");

            //并没有建立数据库连接
            MySqlConnection conn = new MySqlConnection(connectStr);
            try
            {
                conn.Open();    //建立连接，打开数据库
                Console.WriteLine("打开数据库成功");

                string sqlstr = "select * from flow_data_362";   //SQL语句
                MySqlCommand cmd = new MySqlCommand(sqlstr, conn);
                /* cmd.ExecuteReader();     //执行一些查询
                   cmd.ExecuteScalar();     //执行一些查询，返回一个单个的值
                   cmd.ExecuteNonQuery();   //插入删除   */

                //相当于数据读出流  理解为一本书
                MySqlDataReader reader = cmd.ExecuteReader();
                //reader.Read();  //读取下一页数据 ，读取成功，返回true，下一页没有数据则返回false表示到了最后一页

                while (reader.Read())   //遍历表中数据
                {
                    //读取并打印数据
                    reader.Read();
                    //索引是一行有几个数据
                    Console.WriteLine(reader[0].ToString() + reader[1].ToString() + reader[2].ToString() + reader[3].ToString());
                    //还可以使用Getxxx方式去写 参数（索引）
                    //Console.WriteLine(reader.GetInt32(0) + reader.GetString(1) + reader.GetString(2));
                    //Getxxx方法的重载  参数(列名)
                    //Console.WriteLine(reader.GetInt32("id") + reader.GetString("name") + reader.GetString("age"));
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

            return null;
        }
    }
}
