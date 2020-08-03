using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyTiptop.Core;
using System.Data;
using System.Data.SqlClient;


namespace MyTiptop.RDBSStrategy.SqlServer
{
    /// <summary>
    /// SqlServer策略之分部类
    /// </summary>
    public partial class RDBSStrategy : IRDBSStrategy
    {
        /// <summary>
        /// 根据用户工号，查询用户分配的权限
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public List<xpGrid_Functions> GetFunctionByUserId(int UserId)
        {

            //EF使用

            using (DBContext dbContext = new DBContext())
            {
                //查询语句
                string cmdStr = "select * from dbo.xpGrid_Functions where funccode in (select distinct funccode from dbo.xpGrid_FuncsInRoles where roleid in (select roleid from dbo.xpGrid_UsersInRoles where userid = @UserId))";

                //输入参数
                SqlParameter _userId = new SqlParameter { ParameterName = "@UserId", Value = UserId };

                SqlParameter[] parameter = {
                    _userId
                    };
                //原始SQL语句查询法
                List<xpGrid_Functions> Functions = dbContext.Database.SqlQuery<xpGrid_Functions>(cmdStr, parameter).ToList();

                return Functions;
            }

        }
        /// <summary>
        /// 获取不良树型结构
        /// </summary>
        /// <returns></returns>
        public DataTable GetPqTree()
        {
            //EF使用
            using (DBContext dbContext = new DBContext())
            {
                //准备查询sql语句
                // ADO.NET 方式执行SQL 。 返回DataTable
                SqlConnection conn = new System.Data.SqlClient.SqlConnection();
                conn = (SqlConnection)dbContext.Database.Connection;
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                //查询语句
                //                string cmdStr = @"Select 'StNode' As 'NodeType',Null As 'ParentId',Null As 'ParentName',PqStId As 'NodeId',SortName As 'NodeName' From Qa_PoorQuality_Sort Where PqStId Not In (Select Distinct PqStId From Qa_PqSortPqCode_Refe)  And PqStId Not In (Select PqStId From Qa_PqSortPqSort_Refe)
                //union all
                //Select 'StNode' As 'NodeType',SrcPqStId As 'ParentId',(Select SortName From Qa_PoorQuality_Sort Where PqStId = Qa_PqSortPqSort_Refe.SrcPqStId) As 'ParentName',PqStId As 'NodeId',(Select SortName From Qa_PoorQuality_Sort Where PqStId = Qa_PqSortPqSort_Refe.PqStId) As 'NodeName' From Qa_PqSortPqSort_Refe
                //union all
                //Select 'PqNode' As 'NodeType',PqStId As 'ParentId',(Select SortName From Qa_PoorQuality_Sort Where PqStId = Qa_PqSortPqCode_Refe.PqStId ) As 'ParentName',PqId As 'SonId',(Select PqName From Qa_PoorQuality_Code Where PqId = Qa_PqSortPqCode_Refe.PqId ) As 'SonName' From Qa_PqSortPqCode_Refe
                //";

                //NodeId前 加入 S P 标示，区分分类和不良ID 重号情况
                string cmdStr = @"Select 'StNode' As 'NodeType',Null As 'ParentId',Null As 'ParentName','S'+ cast(PqStId as varchar )  As 'NodeId',SortName As 'NodeName' From Qa_PoorQuality_Sort Where PqStId Not In (Select Distinct PqStId From Qa_PqSortPqCode_Refe)  And PqStId Not In (Select PqStId From Qa_PqSortPqSort_Refe)
union all
Select 'StNode' As 'NodeType','S'+cast(SrcPqStId as varchar) As 'ParentId',(Select SortName From Qa_PoorQuality_Sort Where PqStId = Qa_PqSortPqSort_Refe.SrcPqStId) As 'ParentName','S'+ cast(PqStId as varchar )  As 'NodeId',(Select SortName From Qa_PoorQuality_Sort Where PqStId = Qa_PqSortPqSort_Refe.PqStId) As 'NodeName' From Qa_PqSortPqSort_Refe
union all
Select 'PqNode' As 'NodeType','S'+cast(PqStId as varchar) As 'ParentId',(Select SortName From Qa_PoorQuality_Sort Where PqStId = Qa_PqSortPqCode_Refe.PqStId ) As 'ParentName','P'+ cast(PqId as varchar) As 'SonId',(Select PqName From Qa_PoorQuality_Code Where PqId = Qa_PqSortPqCode_Refe.PqId ) As 'SonName' From Qa_PqSortPqCode_Refe
";
                cmd.CommandText = cmdStr;
                //执行填充Table
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                return dt;
            }
        }
    }
}
