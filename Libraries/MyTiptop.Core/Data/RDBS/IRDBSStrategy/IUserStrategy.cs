using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace MyTiptop.Core
{
    /// <summary>
    /// 关系数据库策略基础信息分部接口
    /// </summary>
    public partial interface IRDBSStrategy
    {
        //获取在线用户数量
        int GetOnlineUsersCount();
        //设置用户为在线
        int SetOnlineUsers(int Uid);
        //获取功能菜单 + 是否有权限 checked： 0/1 
        DataTable RoleAuthorizationFuncList(int roleId);
        //修改角色权限
        bool RoleAuthorizationChange(int roleId, string[] pmIdList);
        //获取角色列表 + 是否有权限 checked : 0/1
        DataTable UserAuthorizationRoleList(int uid);
        //修改用户权限
        bool UserAuthorizationChange(int roleId, int[] pmIdList);

        //获取 不良树型结构
        DataTable GetPqTree();

        bool UserAuthorizationCheck(int uid, string action);

    }
}
