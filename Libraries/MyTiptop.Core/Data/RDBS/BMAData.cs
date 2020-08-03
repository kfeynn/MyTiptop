using System;
using System.IO;


namespace MyTiptop.Core
{
    //反射获取接口方法
    public partial class BMAData
    {
        private static IRDBSStrategy _irdbsstrategy = null;//关系型数据库策略

        static BMAData()
        {
            try
            {
                //反射 找到 实现类 RDBSStrategy.* 
                string[] fileNameList = Directory.GetFiles(System.Web.HttpRuntime.BinDirectory, "MyTiptop.RDBSStrategy.*.dll", SearchOption.TopDirectoryOnly);
                _irdbsstrategy = (IRDBSStrategy)Activator.CreateInstance(Type.GetType(string.Format("MyTiptop.RDBSStrategy.{0}.RDBSStrategy, MyTiptop.RDBSStrategy.{0}", fileNameList[0].Substring(fileNameList[0].LastIndexOf("RDBSStrategy.") + 13).Replace(".dll", "")), false, true));
            }
            catch
            {
                throw new Exception("创建'关系数据库策略对象'失败,可能存在的原因:未将'关系数据库策略程序集'添加到bin目录中;'关系数据库策略程序集'文件名不符合'MyTiptop.RDBSStrategy.{策略名称}.dll'格式");
            }
        }
        /// <summary>
        /// 关系型数据库
        /// </summary>
        public static IRDBSStrategy RDBS
        {
            get { return _irdbsstrategy; }
        }
    }
}
