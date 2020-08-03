using System; 
using System.Collections; 
using System.Data; 
using System.Collections.Generic; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
using MyTiptop.OraCore; 
using Oracle.ManagedDataAccess;
using Oracle.ManagedDataAccess.Client;
using System.Threading;

namespace MyTiptop.OraData
{

    public partial class OraQuery
    {

        /// <summary>
        /// 执行oracle 数据库普通查询 ，返回table
        /// </summary>
        /// <param name="oraquery"></param>
        /// <returns></returns>
        public static DataTable GetCommonQuery(string oraquery)
        {
            using (OraDBContext db = new OraDBContext())
            {
                OracleConnection con = new OracleConnection();
                con = (OracleConnection)db.Database.Connection;
                con.Open();

                OracleCommand cmd = new OracleCommand(oraquery, con);
                OracleDataAdapter oda = new OracleDataAdapter();
                oda.SelectCommand = cmd;

                DataTable table = new DataTable();
                oda.Fill(table);

                con.Close();//连接需要关闭
                con.Dispose();

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
                //加载邮件主体
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
            using (OraDBContext db = new OraDBContext())
            {
                //随便执行一个小表，使保持映射
                var model = db.TC_XXU_FILE.ToList().FirstOrDefault();
            }
        }
    }

}
