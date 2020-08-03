using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTiptop.Core
{
    /// <summary>
    /// 配置管理类
    /// </summary>
    public partial class BMAConfig
    {
        private static object _locker = new object();//锁对象

        private static IConfigStrategy _iconfigstrategy = null;//配置策略
        private static MallConfigInfo _mallconfiginfo = null;//商城基本配置信息
        private static EmailConfigInfo _emailconfiginfo = null;//邮件配置信息
        private static UploadConfigInfo _uploadconfiginfo = null;//上传配置信息



        static BMAConfig()
        {
            try
            {
                string[] fileNameList = Directory.GetFiles(System.Web.HttpRuntime.BinDirectory, "MyTiptop.ConfigStrategy.*.dll", SearchOption.TopDirectoryOnly);
                _iconfigstrategy = (IConfigStrategy)Activator.CreateInstance(Type.GetType(string.Format("MyTiptop.ConfigStrategy.{0}.ConfigStrategy, MyTiptop.ConfigStrategy.{0}", fileNameList[0].Substring(fileNameList[0].LastIndexOf("ConfigStrategy.") + 15).Replace(".dll", "")),
                                                                                         false,
                                                                                         true));
            }
            catch
            {
                throw new Exception("创建'配置策略对象'失败,可能存在的原因:未将'配置策略程序集'添加到bin目录中;'配置策略程序集'文件名不符合'MyTiptop.ConfigStrategy.{策略名称}.dll'格式");
            }
             
            _mallconfiginfo = _iconfigstrategy.GetMallConfig();
        }

        /// <summary>
        /// 商城基本配置信息
        /// </summary>
        public static MallConfigInfo MallConfig
        {
            get { return _mallconfiginfo; }
        }

        /// <summary>
        /// 邮件配置信息
        /// </summary>
        public static EmailConfigInfo EmailConfig
        {
            get
            {
                if (_emailconfiginfo == null)
                {
                    lock (_locker)
                    {
                        if (_emailconfiginfo == null)
                        {
                            _emailconfiginfo = _iconfigstrategy.GetEmailConfig();
                        }
                    }
                }
                return _emailconfiginfo;
            }
        }

        /// <summary>
        /// 上传配置信息
        /// </summary>
        public static UploadConfigInfo UploadConfig
        {
            get
            {
                if (_uploadconfiginfo == null)
                {
                    lock (_locker)
                    {
                        if (_uploadconfiginfo == null)
                        {
                            _uploadconfiginfo = _iconfigstrategy.GetUploadConfig();
                        }
                    }
                }
                return _uploadconfiginfo;
            }
        }




        /// <summary>
        /// 保存商城配置信息
        /// </summary>
        public static void SaveMallConfig(MallConfigInfo mallConfigInfo)
        {
            lock (_locker)
            {
                if (_iconfigstrategy.SaveMallConfig(mallConfigInfo))
                    _mallconfiginfo = mallConfigInfo;
            }
        }

        /// <summary>
        /// 保存邮件配置信息
        /// </summary>
        public static void SaveEmailConfig(EmailConfigInfo emailConfigInfo)
        {
            lock (_locker)
            {
                if (_iconfigstrategy.SaveEmailConfig(emailConfigInfo))
                    _emailconfiginfo = null;
            }
        }

        /// <summary>
        /// 保存上传配置信息
        /// </summary>
        public static void SaveUploadConfig(UploadConfigInfo uploadConfigInfo)
        {
            lock (_locker)
            {
                if (_iconfigstrategy.SaveUploadConfig(uploadConfigInfo))
                {
                    // _creditconfiginfo = null;
                }
            }
        }


    }
}
