using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyTiptop.Core;

namespace MyTiptop.Data
{
    /// <summary>
    /// 用户数据访问类
    /// </summary>
    public partial class Users
    {

  
        /// <summary>
        /// 获得用户
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public static xpGrid_User GetUserById(int uid)
        {
            using (DBContext dbContext = new DBContext())
            {
                xpGrid_User userInfo = dbContext.xpGrid_User.Where(u => u.UserID == uid).FirstOrDefault();

                return userInfo;
            }
        }

        /// <summary>
        /// 获得用户id
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public static int? GetUidByUserName(string userName)
        {
            using (DBContext dbContext = new DBContext())
            {
                xpGrid_User userInfo = dbContext.xpGrid_User.Where(u => u.UserName == userName).FirstOrDefault();

                if (userInfo != null)
                    return userInfo.UserID;
                else
                    return null;
            }
        }

        /// <summary>
        /// 获取所有用户信息
        /// </summary>
        /// <returns></returns>
        public static List<xpGrid_User> GetUsers()
        {
            //需要分页处理。。。。。 待完成

            using (DBContext dbContext = new DBContext())
            {
                List<xpGrid_User> users = null;  //.Where(c => c.deleted == 0)
                users = dbContext.xpGrid_User.OrderBy(c => c.deleted).ThenBy(c => c.UserID).ToList();
                return users;
            }

        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public static List<xpGrid_User> GetUser()
        {
            using (DBContext dbContext = new DBContext())
            {
                return dbContext.xpGrid_User.ToList();
            }
        }


        /// <summary>
        /// 获得用户
        /// </summary>
        /// <param name="userName">用户UserName</param>
        /// <returns></returns>
        public static xpGrid_User GetUserByName(string userName)
        {
            using (DBContext dbContext = new DBContext())
            {
                xpGrid_User userInfo = dbContext.xpGrid_User.Where(u => u.UserName == userName).FirstOrDefault();
                return userInfo;
            }
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public static bool AddUser(string UserName,string UserCName)
        {
            //1.为空，返回false
            if (UserName.Length <= 0)
                return false;
            //2.用户已经存在，返回false
            if (GetUidByUserName(UserName) > 0)
                return false;
            //3.查询用户姓名
             

            using (DBContext dbContext = new DBContext())
            {
 
                xpGrid_User user = new xpGrid_User
                {
                    UserName = UserName,
                    UserCName = UserCName,
                    deleted = 0,
                    AllOnlineTime = 0,
                    LoginTimes = 0,
                    Online = 0,
                    Password = BMAConfig.MallConfig.Password   //配置的默认用户密码
                };
                dbContext.xpGrid_User.Add(user);
                dbContext.SaveChanges();
            }
            return true;
        }

        /// <summary>
        /// 创建部分用户
        /// </summary>
        /// <returns></returns>
        public static xpGrid_User CreatePartGuest()
        {
            return new xpGrid_User
            {
                UserID = -1,
                UserName = "guest",
                UserCName = "",
                Password = "",
                deleted = 0,
                Online = 1,
                LastOnlineTime = 0,
                AllOnlineTime = 0,
                LoginTimes = 0,
                CurrentLoginDateTime = new DateTime(1900, 1, 1),
                LastOprtnDateTime = new DateTime(1900, 1, 1)
            };
        }

        /// <summary>
        /// 获得部分用户,检验帐号密码
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public static xpGrid_User GetPartUserByUidAndPwd(int uid, string password)
        {
            xpGrid_User partUserInfo = GetUserById(uid);
            if (partUserInfo != null && partUserInfo.Password == password)
                return partUserInfo;
            return null;
        }

        //获取在线用户数量
        public static int GetOnlineUsersCount()
        {
            return MyTiptop.Core.BMAData.RDBS.GetOnlineUsersCount();
        }

        //设置用户为在线
        public static int SetOnlineUsers(int Uid)
        {
            return MyTiptop.Core.BMAData.RDBS.SetOnlineUsers(Uid);
        }


        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <param name="uid"></param>
        public static void UpdateUser(xpGrid_User user,int uid)
        {
            using (DBContext dbContext = new DBContext())
            {
                xpGrid_User userInfo = dbContext.xpGrid_User.Where(u => u.UserID == uid).FirstOrDefault();

                if (userInfo == null)
                {
                    return;//空
                }
                userInfo.UserName = user.UserName;
                userInfo.UserCName = user.UserCName;
                userInfo.deleted = user.deleted;
                //提交修改
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="uid"></param>
        public static void DeleteUser(int uid)
        {
            try
            {
                using (DBContext dbContext = new DBContext())
                {
                    xpGrid_User userInfo = dbContext.xpGrid_User.Where(u => u.UserID == uid).FirstOrDefault();
                    if (userInfo != null)
                    {
                        dbContext.xpGrid_User.Remove(userInfo);
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
        /// 根据工号查找姓名
        /// </summary>
        /// <param name="UserName">工号</param>
        /// <returns></returns>
        public static string GetUserCNameByUserName(string UserName)
        {
            string rStr = "";
            using (DBContext dbContext = new DBContext())
            {
                //Bl_Personnel_All person = dbContext.Bl_Personnel_All.Where(u => u.nameno == UserName).FirstOrDefault();
                //if (person != null)
                //{
                //    rStr = person.name;
                //}
            }
            return rStr;
        }
        /// <summary>
        /// 根据ID查询姓名
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static string GetUserCNameByUserID(int? sid)
        {
            string returnFlag = "";
            using (DBContext dbContext = new DBContext())
            {
                //if (sid == null)
                //{
                //    return "";
                //}
                xpGrid_User model = dbContext.xpGrid_User.Where(u => u.UserID == sid).FirstOrDefault();
                if (model != null)
                {
                    returnFlag = model.UserCName;
                }
            }
            return returnFlag;
        }

        /// <summary>
        /// 重新设置密码
        /// </summary>
        /// <param name="uid">uid</param>
        /// <param name="pwd">pwd</param>
        /// <returns></returns>
        public static bool ResetPassword(int uid, string pwd)
        {
            //1.不验证原密码
            bool returnFlag = false;
            using (DBContext dbContext = new DBContext())
            {
                xpGrid_User user = dbContext.xpGrid_User.Where(u => u.UserID == uid).FirstOrDefault();
                if (user != null)
                {
                    user.Password = pwd;
                    dbContext.SaveChanges();
                    //设置修改成功状态
                    returnFlag = true;
                }
            }
            return returnFlag;
        }
        /// <summary>
        /// 获取用户所属的角色列表
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static System.Data.DataTable UserAuthorizationRole(int uid)
        {
            return BMAData.RDBS.UserAuthorizationRoleList(uid);
        }

        public static bool UserAuthorizationChange(int uid, int[] pmIdList)
        {
            return BMAData.RDBS.UserAuthorizationChange(uid, pmIdList);
        }













    }
}
