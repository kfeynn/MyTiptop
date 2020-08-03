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
    /// SqlServer策略之用户操作分部类
    /// </summary>
    public partial class RDBSStrategy : IRDBSStrategy
    {
        //获取在线用户数量
        public int GetOnlineUsersCount()
        {
            return 0;
        }

        //设置用户为在线
        public int SetOnlineUsers(int Uid)
        {
            return 0;
        }

        /// <summary>
        /// 获取角色功能列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public DataTable RoleAuthorizationFuncList(int roleId)
        {
            using (DBContext dbContext = new DBContext())
            {
                //准备查询sql语句 ， xpGrid_Functions  + checked  ： 功能权限 + 是否选中
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
                //输入参数
                SqlParameter parameter = new SqlParameter { ParameterName = "@RoleId", Value = roleId };
                cmd.Parameters.Add(parameter);
                string cmdStr = "select [xpGrid_Functions].* , isnull(CheckedFunc.checked, 0) as checked from[dbo].[xpGrid_Functions] left join (select FuncCode,1 as checked from[dbo].[xpGrid_FuncsInRoles] inner join[xpGrid_Role] on[xpGrid_FuncsInRoles].RoleId = [xpGrid_Role].roleid where[xpGrid_Role].RoleId =@RoleId ) CheckedFunc on[xpGrid_Functions].FuncCode = CheckedFunc.FuncCode";
                cmd.CommandText = cmdStr;
                //执行填充Table
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                return dt;
            }
        }

        /// <summary>
        /// 角色更改权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="pmIdList"></param>
        /// <returns></returns>
        public bool RoleAuthorizationChange(int roleId, string[] pmIdList)
        {
            //1.删除原有权限
            //2.添加新的权限
            using (DBContext dbContext = new DBContext())
            {
                using (var dbTransaction = dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        //删除原有权限
                        SqlParameter parameter = new SqlParameter { ParameterName = "@roleId", Value = roleId };
                        string cmdDelStr = "delete  from [xpGrid_FuncsInRoles] where roleId =  @roleId ";
                        dbContext.Database.ExecuteSqlCommand(cmdDelStr, parameter);
                        //添加新权限
                        if (pmIdList != null)
                        {
                            foreach (string pmId in pmIdList)
                            {
                                xpGrid_FuncsInRoles newAdd = new xpGrid_FuncsInRoles()
                                {
                                    RoleId = roleId,
                                    FuncCode = pmId
                                };
                                dbContext.xpGrid_FuncsInRoles.Add(newAdd);
                            }
                        }
                        //添加纪录
                        dbContext.SaveChanges();

                        //提交事务
                        dbTransaction.Commit();

                        return true;
                    }
                    catch (Exception)
                    {
                        //回滚事务操作
                        dbTransaction.Rollback();
                        return false;
                    }
                }
            }
        }


        //获取角色列表 + 是否有权限 checked : 0/1
        public DataTable UserAuthorizationRoleList(int uid)
        {
            using (DBContext dbContext = new DBContext())
            {
                //准备查询sql语句 ， xpGrid_Functions  + checked  ： 功能权限 + 是否选中
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
                //输入参数
                SqlParameter parameter = new SqlParameter { ParameterName = "@UserId", Value = uid };
                cmd.Parameters.Add(parameter);
                //拼凑查询字符串
                string cmdStr = "select [xpGrid_Role].*,isnull(checked,0) as checked from [dbo].[xpGrid_Role] left join (select [xpGrid_UsersInRoles].*,1 as checked from [dbo].[xpGrid_User] inner join [xpGrid_UsersInRoles] on [xpGrid_User].UserID = [xpGrid_UsersInRoles].UserID where [xpGrid_User].userid = @UserId ) CheckedRole  on  [xpGrid_Role].RoleId = CheckedRole.RoleId ";
                cmd.CommandText = cmdStr;
                //执行填充Table
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                return dt;
            }
        }

        /// <summary>
        /// 判断是否有权限访问
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool UserAuthorizationCheck(int uid, string action)
        {
            using (DBContext dbContext = new DBContext())
            {
                SqlConnection conn = new System.Data.SqlClient.SqlConnection();
                conn = (SqlConnection)dbContext.Database.Connection;
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                //拼凑查询字符串
                string cmdStr = "select count(0) from [dbo].[xpGrid_Functions] where funccode in (select funccode from [dbo].[xpGrid_FuncsInRoles] where roleid in (select roleid from  [dbo].[xpGrid_UsersInRoles] where userid = " + uid + " )) and funcUrl like '%" + action + "%'";
                cmd.CommandText = cmdStr;
                int count = (int)cmd.ExecuteScalar();

                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }



        //修改用户权限
        public bool UserAuthorizationChange(int uid, int[] pmIdList)
        {
            //1.删除原有权限
            //2.添加新的权限
            using (DBContext dbContext = new DBContext())
            {
                using (var dbTransaction = dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        //删除原有权限
                        SqlParameter parameter = new SqlParameter { ParameterName = "@userId", Value = uid };
                        string cmdDelStr = "delete from [xpGrid_UsersInRoles] where userId = @userId ";
                        dbContext.Database.ExecuteSqlCommand(cmdDelStr, parameter);
                        //添加新权限
                        if (pmIdList != null)
                        {
                            foreach (int pmId in pmIdList)
                            {
                                xpGrid_UsersInRoles newAdd = new xpGrid_UsersInRoles()
                                {
                                    UserID = uid,
                                    RoleId = pmId
                                };
                                dbContext.xpGrid_UsersInRoles.Add(newAdd);
                            }
                        }
                        //添加纪录
                        dbContext.SaveChanges();

                        //提交事务
                        dbTransaction.Commit();

                        return true;
                    }
                    catch (Exception)
                    {
                        //回滚事务操作
                        dbTransaction.Rollback();
                        return false;
                    }
                }

            }

        }
    }
}
