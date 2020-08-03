using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace MyTiptop.Core
{
    /// <summary>
    /// 关系数据库帮助类
    /// </summary>
    public class RDBSHelper
    {
        /// <summary>
        /// 执行分页存储过程
        /// </summary>
        /// <typeparam name="T">泛型 需要有对应的实体类</typeparam>
        /// <param name="tablename">表、视图</param>
        /// <param name="fileds">需要查询的列</param>
        /// <param name="orderfield">排序字段、主键</param>
        /// <param name="sqlwhere">查询条件</param>
        /// <param name="pagesize">页面显示数量</param>
        /// <param name="pageindex">页码</param>
        /// <param name="totalpage">总页数</param>
        /// <param name="totalrecord">总记录数</param>
        /// <returns></returns>
        public List<T> ExecutePaging<T>(string tablename, string fileds, string orderfield, string sqlwhere, int pagesize, int pageindex, out int totalpage, out int totalrecord) 
        {
            using (DBContext db = new DBContext())
            {
                //准备参数
                totalpage = 0;
                totalrecord = 0;
                //输入参数
                SqlParameter TableName = new SqlParameter { ParameterName = "@TableName", Value = tablename };
                SqlParameter Fields = new SqlParameter { ParameterName = "@Fields", Value = "*" };//fileds //只能取全部字段 需要对应 相应Entity
                SqlParameter OrderField = new SqlParameter { ParameterName = "@OrderField", Value = orderfield };
                SqlParameter sqlWhere = new SqlParameter { ParameterName = "@sqlWhere", Value = sqlwhere };
                SqlParameter pageSize = new SqlParameter { ParameterName = "@pageSize", Value = pagesize };
                SqlParameter pageIndex = new SqlParameter { ParameterName = "@pageIndex", Value = pageindex };
                //返回参数
                SqlParameter TotalPage = new SqlParameter { ParameterName = "@TotalPage", Value = totalpage, Direction = ParameterDirection.Output };
                //返回参数
                SqlParameter TotalRecord = new SqlParameter { ParameterName = "@TotalRecord", Value = totalrecord, Direction = ParameterDirection.Output };

                SqlParameter[] parameter = new SqlParameter[]{
                    TableName,Fields,OrderField,sqlWhere,pageSize,pageIndex,TotalPage,TotalRecord
                };

                //执行存储过程.  返回类型用视图实体类 。
                var list = db.Database.SqlQuery<T>("exec p_Paging_RowCount @TableName,@Fields,@OrderField ,@sqlWhere ,@pageSize ,@pageIndex  ,@TotalPage out ,@TotalRecord out",parameter).ToList();

                //获取返回值
                totalpage = int.Parse(TotalPage.Value.ToString());
                totalrecord = int.Parse(TotalRecord.Value.ToString());

                return list;
            }
        }


        /// <summary>分页获取数据列表的方法ADO.NET</summary>
        /// <param name='tablename'>表或视图</param>
        /// <param name='fileds'>选择的字段</param>
        /// <param name='order'>排序字段</param>
        /// <param name='ordertype'>排序类型desc或者asc</param>
        /// <param name='PageSize'>页大小</param>
        /// <param name='PageIndex'>第几页（索引）</param>
        /// <param name='strWhere'>条件</param>
        /// <returns></returns>
        public DataTable GetList(string tablename, string fileds, string orderfield, string sqlwhere, int pagesize, int pageindex, out int totalpage, out int totalrecord)
        {
            using (DBContext db = new DBContext())
            {

                //先给返回参数赋值
                totalpage = 0;
                totalrecord = 0;

                // ADO.NET 方式执行分页存储过程 。 用于需要只取部分字段的视图或表。
                string cmdStr = "p_Paging_RowCount";
                SqlConnection conn = new System.Data.SqlClient.SqlConnection();
                conn = (SqlConnection)db.Database.Connection;
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;

                //输入参数
                SqlParameter TableName = new SqlParameter { ParameterName = "@TableName", Value = tablename };
                SqlParameter Fields = new SqlParameter { ParameterName = "@Fields", Value = fileds };
                SqlParameter OrderField = new SqlParameter { ParameterName = "@OrderField", Value = orderfield };
                SqlParameter sqlWhere = new SqlParameter { ParameterName = "@sqlWhere", Value = sqlwhere };
                SqlParameter pageSize = new SqlParameter { ParameterName = "@pageSize", Value = pagesize };
                SqlParameter pageIndex = new SqlParameter { ParameterName = "@pageIndex", Value = pageindex };
                //返回参数
                SqlParameter TotalPage = new SqlParameter { ParameterName = "@TotalPage", Value = totalpage, Direction = ParameterDirection.Output };
                //返回参数
                SqlParameter TotalRecord = new SqlParameter { ParameterName = "@TotalRecord", Value = totalrecord, Direction = ParameterDirection.Output };

                cmd.Parameters.Add(TableName);
                cmd.Parameters.Add(Fields);
                cmd.Parameters.Add(OrderField);
                cmd.Parameters.Add(sqlWhere);
                cmd.Parameters.Add(pageSize);
                cmd.Parameters.Add(pageIndex);
                cmd.Parameters.Add(TotalPage);
                cmd.Parameters.Add(TotalRecord);

                cmd.CommandText = cmdStr;
                //执行填充Table
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                adapter.Fill(table);

                //获取返回参数
                totalpage = int.Parse(TotalPage.Value.ToString());  // int.Parse(cmd.Parameters["@TotalPage"].Value.ToString());
                totalrecord = int.Parse(TotalRecord.Value.ToString());  //int.Parse(cmd.Parameters["@TotalRecord"].Value.ToString());

                conn.Close();//连接需要关闭
                conn.Dispose();

                return table;
            }
        }

    }




}
