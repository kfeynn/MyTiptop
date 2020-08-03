using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace MyTiptop.SupplierData
{
    /// <summary>
    /// 关系数据库帮助类
    /// </summary>
    public class OraRDBSHelper
    {
        /// <summary>
        /// 执行分页查询
        /// </summary>
        /// <typeparam name="T">泛型 需要有对应的实体类</typeparam>
        /// <param name="tablename">表、视图</param>
        /// <param name="orderfield">排序字段、主键</param>
        /// <param name="sqlwhere">查询条件</param>
        /// <param name="pagesize">页面显示数量</param>
        /// <param name="pageindex">页码</param>
        /// <param name="totalrecord">总记录数</param>
        /// <returns></returns>
        public List<T> ExecutePaging<T>(string tablename, string orderfield, string sqlwhere, int pagesize, int pageindex,  out int totalrecord)
        {
            using (DBContext db = new DBContext())
            {
                //Oracle 。不能通过存储过程了。 
                string cmdStr = GetCmdStr(tablename, orderfield, sqlwhere, pagesize, pageindex);
                //执行存储过程.  返回类型用视图实体类 。
                var list = db.Database.SqlQuery<T>(cmdStr).ToList();

                //获取 table、sqlwhere 总记录数
                totalrecord = GetTableSqlwhereCount(tablename, sqlwhere);

                return list;
            }
        }
        /// <summary>
        /// 获取记录总数
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="sqlwhere"></param>
        /// <returns></returns>
        public int GetTableSqlwhereCount(string tablename, string sqlwhere)
        {
            using (DBContext db = new DBContext())
            {
                int count = 0;

                string cmdStr = " select count(0) from " + tablename + " " + sqlwhere ;

                //获取个数。
                var m = db.Database.SqlQuery<int>(cmdStr);

                count = m.FirstOrDefault();
 
                return count;
            }
        }


        /// <summary>
        /// 执行一般查询
        /// </summary>
        /// <param name="cmdStr"></param>
        /// <returns></returns>
        public static List<T> ExecuateSql<T>(string cmdStr)
        {
            using (DBContext db = new DBContext())
            {

                //执行存储过程或语句.  返回类型用视图实体类 。
                var list = db.Database.SqlQuery<T>(cmdStr).ToList();


                return list;
            }
        }

        /// <summary>
        /// 执行一般查询
        /// </summary>
        /// <param name="cmdStr"></param>
        /// <returns></returns>
        public static  DataTable ExecuateSql(string cmdStr)
        {
            using (DBContext db = new DBContext())
            {
                // ADO.NET 方式执行SQL 。 返回DataTable
                Oracle.ManagedDataAccess.Client.OracleConnection conn = new Oracle.ManagedDataAccess.Client.OracleConnection();
                conn = (OracleConnection)db.Database.Connection;
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                Oracle.ManagedDataAccess.Client.OracleCommand cmd = new  OracleCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = cmdStr;
                //执行填充Table
                Oracle.ManagedDataAccess.Client.OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        /// <summary>
        /// 执行一般查询
        /// </summary>
        /// <param name="cmdStr"></param>
        /// <returns></returns>
        public static int ExecuteScalarSql(string cmdStr)
        {
            using (DBContext db = new DBContext())
            {
                Oracle.ManagedDataAccess.Client.OracleConnection conn = new Oracle.ManagedDataAccess.Client.OracleConnection();
                conn = (OracleConnection)db.Database.Connection;
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                Oracle.ManagedDataAccess.Client.OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = cmdStr;
                //执行填充Table

                object o = cmd.ExecuteScalar();

                int count = Convert.ToInt32(o.ToString());//(int)cmd.ExecuteScalar();

                return count;
            }
        }


        public static OracleDataReader ExecuteReaderSql(string cmdStr)
        {
            using (DBContext db = new DBContext())
            {
                Oracle.ManagedDataAccess.Client.OracleConnection conn = new Oracle.ManagedDataAccess.Client.OracleConnection();
                conn = (OracleConnection)db.Database.Connection;
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                Oracle.ManagedDataAccess.Client.OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = cmdStr;
                //执行填充Table
                OracleDataReader o = cmd.ExecuteReader();
               
                return o;
            }
        }



        /// <summary>
        /// 执行语句，不带返回值
        /// </summary>
        /// <param name="cmdStr"></param>
        public static void ExecuteSqlNonQuery(string cmdStr)
        {
            using (DBContext db = new DBContext())
            {
                Oracle.ManagedDataAccess.Client.OracleConnection conn = new Oracle.ManagedDataAccess.Client.OracleConnection();
                conn = (OracleConnection)db.Database.Connection;
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                Oracle.ManagedDataAccess.Client.OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = cmdStr;
                //执行 
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 执行一般查询
        /// </summary>
        /// <param name="cmdStr"></param>
        /// <returns></returns>
        public static string  ExecuteScalarSqlForString(string cmdStr)
        {
            using (DBContext db = new DBContext())
            {
                Oracle.ManagedDataAccess.Client.OracleConnection conn = new Oracle.ManagedDataAccess.Client.OracleConnection();
                conn = (OracleConnection)db.Database.Connection;
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                Oracle.ManagedDataAccess.Client.OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = cmdStr;
                //执行填充Table

                object o = cmd.ExecuteScalar();

                return o.ToString();//(int)cmd.ExecuteScalar();

                 
            }
        }


        /// <summary>
        /// 获取查询分页 字符串
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="orderfield">field desc,field2 asc</param>
        /// <param name="sqlwhere">必须传入查询条件。没有就where 1=1 </param>
        /// <param name="pagesize">传参前先判断总数量</param>
        /// <param name="pageindex">传参前先半盘总页数</param>
        /// <returns></returns>
        public string GetCmdStr(string tablename, string orderfield, string sqlwhere, int pagesize, int pageindex)
        {
            string cmdstr = "";
            //起始条数、截止条数
            int rownum; 
            int rowno; 
            rownum = pagesize * pageindex;
            rowno = pagesize * (pageindex - 1); 
            //sql基础模板
            if (orderfield.Length <= 0)
            {
                //没有orderby
                //cmdstr = "SELECT * FROM(SELECT ROWNUM AS rowno, t.* FROM tc_brd_file t WHERE tc_brd08 BETWEEN TO_DATE('20180501', 'yyyymmdd') AND TO_DATE('20180731', 'yyyymmdd') AND ROWNUM <= 20 * 3) table_alias WHERE table_alias.rowno > 20 * (3 - 1);";
                cmdstr = "SELECT * FROM(SELECT ROWNUM AS rowno, t.* FROM {0} t {1} AND ROWNUM <= {2}) table_alias WHERE table_alias.rowno > {3}";

                cmdstr = String.Format(cmdstr,
                            tablename,
                            sqlwhere,
                            rownum,
                            rowno
                            );
            }
            else
            {
                //有orderby .每页20行。第三页
                //cmdstr = "SELECT * FROM(SELECT ROWNUM AS rowno, r.* FROM( SELECT * FROM tc_brd_file WHERE tc_brd08 BETWEEN TO_DATE('20180501', 'yyyymmdd') AND TO_DATE('20180731', 'yyyymmdd') ORDER BY tc_brd08 desc ) r where ROWNUM <= 20 * 3 ) table_alias WHERE table_alias.rowno > 20 * (3 - 1);";
                cmdstr = "SELECT * FROM(SELECT ROWNUM AS rowno, r.* FROM( SELECT * FROM {0} {1} ORDER BY {2} ) r where ROWNUM <= {3} ) table_alias WHERE table_alias.rowno > {4}";

                cmdstr = String.Format(cmdstr,
                            tablename,
                            sqlwhere,
                            orderfield,
                            rownum,
                            rowno
                            );
            }

            return cmdstr;
        }

    }
}
