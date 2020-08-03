using System; 
using System.Collections; 
using System.Data; 
using System.Collections.Generic; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
using System.Data.SqlClient;
using MyTiptop.Core;
using System.Threading;

namespace MyTiptop.Data
{

    public partial class DBQuery
    {
        /// <summary> 
        /// 执行oracle 数据库普通查询 ，返回table 
        /// </summary> 
        /// <param name="oraquery"></param> 
        /// <returns></returns> 
        public static DataTable GetCommonQuery(string cmdStr) 
        { 
            using (DBContext db = new DBContext()) 
            {
                DataTable table = new DataTable();
                if (cmdStr != null && cmdStr.Length > 0)
                {
                    SqlConnection conn = new System.Data.SqlClient.SqlConnection();
                    conn = (SqlConnection)db.Database.Connection;
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = cmdStr;
                    //执行填充Table
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(table);
                    conn.Close();//连接需要关闭
                    conn.Dispose();
                }
                return table;
            }
        }

        /// <summary>
        /// 新开线程，后台运行
        /// </summary>
        public static void ThreadMethod()
        {
            try
            {
                ThreadStart myThreadDelegate = new ThreadStart(DoWork);
                Thread myThread = null;
                myThread = new Thread(myThreadDelegate);
                myThread.Start();
            }
            catch
            {
                //有错不管
            }
        }

        private static void DoWork()
        {
            using (DBContext db = new DBContext())
            {
                //随便执行一个小表，使保持映射
                var model = db.xpGrid_User.ToList().FirstOrDefault();
            }
        }
    }
}
