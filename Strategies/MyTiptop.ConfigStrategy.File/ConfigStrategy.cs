using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyTiptop.Core;

using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Configuration;

namespace MyTiptop.ConfigStrategy.File
{
    /// <summary>
    /// 基于文件的配置策略
    /// </summary>
    public partial class ConfigStrategy : IConfigStrategy
    {
        #region 私有字段  

        private readonly string _mallconfigfilepath = "/App_Data/mall.config";//商城基本配置信息文件路径
        private readonly string _emailconfigfilepath = "/App_Data/email.config";//邮件配置信息文件路径
        private readonly string _uploadconfigfilepath = "/App_Data/upload.config";//上传配置信息文件路径

        public ConfigStrategy()
        {
            //改造构造函数 ，重新指定文件路径 到 子目录下
            string subpath = "";
            subpath = ConfigurationManager.AppSettings["SubPath"].ToString();
            //subpath = Controller.Server.MapPath("~");   //? 获取不到站点目录
            if (subpath.Length > 0)
            {
                subpath = "/" + subpath;
            }

            _mallconfigfilepath = subpath + _mallconfigfilepath;
            _emailconfigfilepath = subpath + _emailconfigfilepath;
            _uploadconfigfilepath = subpath + _uploadconfigfilepath;

            //_mallconfigfilepath = dbs + _mallconfigfilepath;
            //_emailconfigfilepath = dbs + _emailconfigfilepath; WebWorkContext
            //_uploadconfigfilepath = dbs + _uploadconfigfilepath;

        }
        #endregion

        #region 帮助方法

        /// <summary>
        /// 从文件中加载配置信息
        /// </summary>
        /// <param name="configInfoType">配置信息类型</param>
        /// <param name="configInfoFile">配置信息文件路径</param>
        /// <returns>配置信息</returns>
        private IConfigInfo LoadConfigInfo(Type configInfoType, string configInfoFile)
        {
            return (IConfigInfo)IOHelper.DeserializeFromXML(configInfoType, configInfoFile);
        }

        /// <summary>
        /// 将配置信息保存到文件中
        /// </summary>
        /// <param name="configInfo">配置信息</param>
        /// <param name="configInfoFile">保存路径</param>
        /// <returns>是否保存成功</returns>
        private bool SaveConfigInfo(IConfigInfo configInfo, string configInfoFile)
        {
            return IOHelper.SerializeToXml(configInfo, configInfoFile);
        }

        #endregion

        /// <summary>
        /// 保存商城基本配置
        /// </summary>
        /// <param name="configInfo">商城基本配置信息</param>
        /// <returns>是否保存结果</returns>
        public bool SaveMallConfig(MallConfigInfo configInfo)
        {
            return SaveConfigInfo(configInfo, IOHelper.GetMapPath(_mallconfigfilepath));
        }

        /// <summary>
        /// 获得商城基本配置
        /// </summary>
        public MallConfigInfo GetMallConfig()
        {
            
            return (MallConfigInfo)LoadConfigInfo(typeof(MallConfigInfo), IOHelper.GetMapPath(_mallconfigfilepath));
        }

        /// <summary>
        /// 保存邮件配置
        /// </summary>
        /// <param name="configInfo">邮件配置信息</param>
        /// <returns>是否保存结果</returns>
        public bool SaveEmailConfig(EmailConfigInfo configInfo)
        {
            return SaveConfigInfo(configInfo, IOHelper.GetMapPath(_emailconfigfilepath));
        }

        /// <summary>
        /// 获得邮件配置
        /// </summary>
        public EmailConfigInfo GetEmailConfig()
        {
            return (EmailConfigInfo)LoadConfigInfo(typeof(EmailConfigInfo), IOHelper.GetMapPath(_emailconfigfilepath));
        }
                
        /// <summary>
        /// 保存上传配置
        /// </summary>
        /// <param name="configInfo">上传配置信息</param>
        /// <returns>是否保存成功</returns>
        public bool SaveUploadConfig(UploadConfigInfo configInfo)
        {
            return SaveConfigInfo(configInfo, IOHelper.GetMapPath(_uploadconfigfilepath));
        }

        /// <summary>
        /// 获得上传配置
        /// </summary>
        public UploadConfigInfo GetUploadConfig()
        {
            return (UploadConfigInfo)LoadConfigInfo(typeof(UploadConfigInfo), IOHelper.GetMapPath(_uploadconfigfilepath));
        }

    }
}
