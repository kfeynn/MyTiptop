using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyTiptop.Core;
 

namespace MyTiptop.Data
{
    public partial class Roles
    {
        /// <summary>
        /// 根据角色名称查询
        /// </summary>
        /// <param name="RoleName"></param>
        /// <returns></returns>
        public static xpGrid_Role GetRoleByRoleName(string RoleName)
        {
            using (DBContext dbContext = new DBContext())
            {
                //xpGrid_User userInfo = null;
                xpGrid_Role roleInfo = dbContext.xpGrid_Role.Where(u => u.RoleName == RoleName).FirstOrDefault();

                return roleInfo;
            }
        }

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="RoleName"></param>
        /// <param name="RoleDes"></param>
        /// <returns></returns>
        public static bool AddRole(string RoleName, string RoleDes)
        {
            //1.为空，返回false
            if (RoleName.Length <= 0)
                return false;
            //2.用户已经存在，返回false
            if (GetUidByRoleName(RoleName) > 0)
                return false;
            using (DBContext dbContext = new DBContext())
            {
                xpGrid_Role role = new xpGrid_Role
                {
                    RoleName = RoleName,
                    RoleDes = RoleDes
                };
                dbContext.xpGrid_Role.Add(role);
                dbContext.SaveChanges();
            }
            return true;
        }

        public static int? GetUidByRoleName(string roleName)
        {
            using (DBContext dbContext = new DBContext())
            {
                xpGrid_Role roleInfo = dbContext.xpGrid_Role.Where(u => u.RoleName == roleName).FirstOrDefault();

                if (roleInfo != null)
                    return roleInfo.RoleId;
                else
                    return null;
            }
        }

        /// <summary>
        /// 根据Id查找角色
        /// </summary>
        /// <param name="rid"></param>
        /// <returns></returns>
        public static xpGrid_Role GetRoleById(int rid)
        {
            using (DBContext dbContext = new DBContext())
            {
                xpGrid_Role roleInfo = dbContext.xpGrid_Role.Where(r => r.RoleId == rid).FirstOrDefault();

                return roleInfo;
            }
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="rid"></param>
        public static void DeleteRole(int rid)
        {
            try
            {
                using (DBContext dbContext = new DBContext())
                {
                    xpGrid_Role roleInfo = dbContext.xpGrid_Role.Where(r => r.RoleId == rid).FirstOrDefault();

                    if (roleInfo != null)
                    {
                        dbContext.xpGrid_Role.Remove(roleInfo);
                        dbContext.SaveChanges();
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="rid"></param>
        /// <param name="role"></param>
        public static void UpdateRole(int rid, xpGrid_Role role)
        {
            using (DBContext dbContext = new DBContext())
            {
                xpGrid_Role roleInfo = dbContext.xpGrid_Role.Where(u => u.RoleId == rid).FirstOrDefault();
                if (roleInfo == null)
                {
                    return;//空
                }
                roleInfo.RoleName = role.RoleName;
                roleInfo.RoleDes = role.RoleDes;
                //提交修改
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// 查找 功能菜单
        /// </summary>
        /// <param name="FuncCode"></param>
        /// <returns></returns>
        public static xpGrid_Functions GetFunctionByFuncCode(string FuncCode)
        {
            using (DBContext dbContext = new DBContext())
            {
                xpGrid_Functions funcInfo = dbContext.xpGrid_Functions.Where(u => u.FuncCode == FuncCode).FirstOrDefault();

                return funcInfo;
            }
        }


        public static bool IsExistFunc(string FuncCode)
        {
            using (DBContext dbContext = new DBContext())
            {
                xpGrid_Functions func = dbContext.xpGrid_Functions.Where(u => u.FuncCode == FuncCode).FirstOrDefault();

                if (func != null)
                    return true ;
                else
                    return false;
            }
        }


        public static List<xpGrid_FunctionsForPublic> GetFuncForPublic()
        {
            using (DBContext dbContext = new DBContext())
            {
                var ModelList = dbContext.xpGrid_FunctionsForPublic.ToList();
                return ModelList;
            }
        }


        /// <summary>
        /// 添加 功能菜单
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public static bool AddFunctions(xpGrid_Functions func)
        {
            //1.为空，返回false
            if (func.FuncCode.Length  <= 0)
                return false;
            //2.用户已经存在，返回false
            if (IsExistFunc(func.FuncCode ))
                return false;
            using (DBContext dbContext = new DBContext())
            {
                //添加纪录
                xpGrid_Functions funcNew = new xpGrid_Functions
                {
                    FuncCode = func.FuncCode,
                    FuncName = func.FuncName,
                    Enable = func.Enable,
                    FuncImg = func.FuncImg,
                    FuncParent = func.FuncParent,
                    DisplayOrder = func.DisplayOrder,
                    FuncUrl = func.FuncUrl
                };
                dbContext.xpGrid_Functions .Add(funcNew);
                dbContext.SaveChanges();
            }
            return true;

        }


        /// <summary>
        /// 删除功能菜单
        /// </summary>
        /// <param name="funcCode"></param>
        public static void DeleteFunc(string  funcCode)
        {
            try
            {
                using (DBContext dbContext = new DBContext())
                {
                    xpGrid_Functions func = dbContext.xpGrid_Functions.Where(u => u.FuncCode == funcCode).FirstOrDefault();

                    if (func != null)
                    {
                        dbContext.xpGrid_Functions.Remove(func);
                        dbContext.SaveChanges();
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// 修改功能菜单
        /// </summary>
        /// <param name="fid"></param>
        /// <param name="func"></param>
        public static void UpdateFunc(string fid, xpGrid_Functions func)
        {
            using (DBContext dbContext = new DBContext())
            {
                xpGrid_Functions Func = dbContext.xpGrid_Functions.Where(u => u.FuncCode == fid).FirstOrDefault();               
                if (Func == null)
                {
                    return;//空
                }
                 
                Func.FuncName = func.FuncName;
                Func.FuncUrl = func.FuncUrl;
                Func.FuncParent = func.FuncParent;
                Func.Enable = func.Enable;
                Func.DisplayOrder = func.DisplayOrder;
                 
                //提交修改
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// 批量删除功能菜单
        /// </summary>
        /// <param name="pmIdList"></param>
        public static void Delfunclist(string[] pmIdList)
        {
            using (DBContext dbContext = new DBContext())
            {
                //循环删除，性能不好 ？
                foreach (string funcCode in pmIdList)
                {
                    xpGrid_Functions Func = dbContext.xpGrid_Functions.Where(u => u.FuncCode == funcCode).FirstOrDefault();
                    if (Func != null)
                    {
                        dbContext.xpGrid_Functions.Remove(Func);
                    }
                }
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// 获取 功能列表（包含是否授权）
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public static System.Data.DataTable RoleAuthorizationFunc(int roleId)
        {
            return BMAData.RDBS.RoleAuthorizationFuncList(roleId);
        }
        /// <summary>
        /// 修改角色功能权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="pmIdList"></param>
        /// <returns></returns>
        public static bool RoleAuthorizationChange(int roleId, string[] pmIdList)
        {
            return BMAData.RDBS.RoleAuthorizationChange(roleId, pmIdList);
        }

    }

    public partial class Trees
    {
        /// <summary>
        /// 获取不良树
        /// </summary>
        /// <returns></returns>
        public static System.Data.DataTable GetPqTree()
        {
            return BMAData.RDBS.GetPqTree();
        }




    }
}
