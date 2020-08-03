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




        /// <summary>
        /// 根据用户ID查询对应权限
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        List<xpGrid_Functions> GetFunctionByUserId(int UserId);




        /// <summary>
        /// 获取前台树形列表
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        //List<xpGrid_Functions> GetFunctionForPublic(int UserId);






    }





}
