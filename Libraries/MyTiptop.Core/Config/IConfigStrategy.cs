using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTiptop.Core
{
    /// <summary>
    /// BrnMall配置策略接口
    /// </summary>
    public partial interface IConfigStrategy
    {


        /// <summary>
        /// 保存商城基本配置
        /// </summary>
        /// <param name="configInfo">商城基本配置信息</param>
        /// <returns>是否保存成功</returns>
        bool SaveMallConfig(MallConfigInfo configInfo);

        /// <summary>
        /// 获得商城基本配置
        /// </summary>
        MallConfigInfo GetMallConfig();

        /// <summary>
        /// 保存邮件配置
        /// </summary>
        /// <param name="configInfo">邮件配置信息</param>
        /// <returns>是否保存成功</returns>
        bool SaveEmailConfig(EmailConfigInfo configInfo);

        /// <summary>
        /// 获得邮件配置
        /// </summary>
        EmailConfigInfo GetEmailConfig();

        /// <summary>
        /// 保存上传配置
        /// </summary>
        /// <param name="configInfo">上传配置信息</param>
        /// <returns>是否保存成功</returns>
        bool SaveUploadConfig(UploadConfigInfo configInfo);

        /// <summary>
        /// 获得上传配置
        /// </summary>
        UploadConfigInfo GetUploadConfig();

     
    }
}
