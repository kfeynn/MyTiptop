using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MyTiptop.Core;
using System.Transactions;

namespace MyTiptop.OraCore.Data
{
    /// <summary>
    ///  
    /// </summary>
    public partial class Tcqcxs
    {

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name=" "></param>
        /// <returns></returns>
        public static bool IsExists(int sid)
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                //精确匹配名称
                var model = dbContext.TC_QCX_FILE.Where(u => u.TC_QCX01 == sid).FirstOrDefault();
                if (model != null)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static TC_QCX_FILE GetModel(int sid)
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                var model = dbContext.TC_QCX_FILE.Where(u => u.TC_QCX01 == sid).FirstOrDefault();
                return model;
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool AddModel(TC_QCX_FILE model)
        {
            bool returnFlag = false;
            using (OraDBContext dbContext = new OraDBContext())
            {
                //增加
                dbContext.TC_QCX_FILE.Add(model);
                dbContext.SaveChanges();
                returnFlag = true;
            }
            return returnFlag;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static bool DeleteModel(int sid)
        {
            bool returnFlag = false;
            using (OraDBContext dbContext = new OraDBContext())
            {
                var model = dbContext.TC_QCX_FILE.Where(u => u.TC_QCX01 == sid).FirstOrDefault();
                if (model != null)
                {
                    dbContext.TC_QCX_FILE.Remove(model);

                    dbContext.SaveChanges();

                    returnFlag = true;
                }
            }
            return returnFlag;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <param name="sid"></param>
        public static void UpdateModel(TC_QCX_FILE model, int sid)
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                var editmodel = dbContext.TC_QCX_FILE.Where(u => u.TC_QCX01 == sid).FirstOrDefault();
                if (editmodel == null)
                {
                    return;//空
                }
                editmodel.TC_QCX02 = model.TC_QCX02;
                editmodel.TC_QCX03 = model.TC_QCX03;
                editmodel.TC_QCX04 = model.TC_QCX04;
                editmodel.TC_QCX05 = model.TC_QCX05;
                editmodel.TC_QCX06 = model.TC_QCX06;
                editmodel.TC_QCX07 = model.TC_QCX07;

                //提交修改 
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public static List<TC_QCX_FILE> GetList()
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                return dbContext.TC_QCX_FILE.ToList();
            }
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public static List<TC_QCX_FILE> GetList(string tc_qcx06,int tc_qcx05)
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                return dbContext.TC_QCX_FILE.Where(u => u.TC_QCX06 == tc_qcx06 && u.TC_QCX05 == tc_qcx05).ToList();
            }
        }

    }

    public partial class Tcqcxxs
    {

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name=" "></param>
        /// <returns></returns>
        public static bool IsExists(int sid)
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                //精确匹配名称
                var model = dbContext.TC_QCXX_FILE.Where(u => u.TC_QCXX01 == sid).FirstOrDefault();
                if (model != null)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static TC_QCXX_FILE GetModel(int sid)
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                var model = dbContext.TC_QCXX_FILE.Where(u => u.TC_QCXX01 == sid).FirstOrDefault();
                return model;
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool AddModel(TC_QCXX_FILE model)
        {
            bool returnFlag = false;
            using (OraDBContext dbContext = new OraDBContext())
            {
                //增加
                dbContext.TC_QCXX_FILE.Add(model);
                dbContext.SaveChanges();
                returnFlag = true;
            }
            return returnFlag;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static bool DeleteModel(int sid)
        {
            bool returnFlag = false;
            using (OraDBContext dbContext = new OraDBContext())
            {
                var model = dbContext.TC_QCXX_FILE.Where(u => u.TC_QCXX01 == sid).FirstOrDefault();
                if (model != null)
                {
                    dbContext.TC_QCXX_FILE.Remove(model);

                    dbContext.SaveChanges();

                    returnFlag = true;
                }
            }
            return returnFlag;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <param name="sid"></param>
        public static void UpdateModel(TC_QCXX_FILE model, int sid)
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                var editmodel = dbContext.TC_QCXX_FILE.Where(u => u.TC_QCXX01 == sid).FirstOrDefault();
                if (editmodel == null)
                {
                    return;//空
                }
                editmodel.TC_QCXX02 = model.TC_QCXX02;
                editmodel.TC_QCXX03 = model.TC_QCXX03;
                editmodel.TC_QCXX04 = model.TC_QCXX04;
                editmodel.TC_QCXX05 = model.TC_QCXX05;
                editmodel.TC_QCXX06 = model.TC_QCXX06;
                editmodel.TC_QCXX07 = model.TC_QCXX07;
                editmodel.TC_QCXX08 = model.TC_QCXX08;
                editmodel.TC_QCXX09 = model.TC_QCXX09;

                //提交修改 
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public static List<TC_QCXX_FILE> GetList()
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                return dbContext.TC_QCXX_FILE.ToList();
            }
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public static List<TC_QCXX_FILE> GetList(string tc_qcxx06, int tc_qcxx05)
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                return dbContext.TC_QCXX_FILE.Where(u => u.TC_QCXX06 == tc_qcxx06 && u.TC_QCXX05 == tc_qcxx05).ToList();
            }
        }

    }



    public partial class Tcqcys
    {

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name=" "></param>
        /// <returns></returns>
        public static bool IsExists(string sid)
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                //精确匹配名称
                var model = dbContext.TC_QCY_FILE.Where(u => u.TC_QCY01 == sid).FirstOrDefault();
                if (model != null)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// 是否已经审核
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static bool IsChecked(string sid)
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                //精确匹配名称
                var model = dbContext.TC_QCY_FILE.Where(u => u.TC_QCY01 == sid).FirstOrDefault();
                if (model != null && model.TC_QCY12 == 2)
                    return true;
                else
                    return false;
            }

        }

        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static TC_QCY_FILE GetModel(string sid)
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                var model = dbContext.TC_QCY_FILE.Where(u => u.TC_QCY01 == sid).FirstOrDefault();
                return model;
            }
        }



        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="prefix">前缀</param>
        /// <returns></returns>
        public static TC_QCY_FILE GetMaxModel(string prefix)
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                //houseList = db.House.Where(x => x.Region.Contains(region)).ToList();
                ////Contains方法是string类型对象的实例方法，在EF中，它会被最终解析为 like‘% %’的样子
                //houseList = db.House.Where(x => x.Region.StartsWith(region)).ToList();
                //houseList = db.House.Where(x => x.Region.EndsWith(region)).ToList();
                ////如果需要实现like‘% ’或者 like ‘ %’则需要使用 startwith，和endwith

                //var ids = new int[] { 1, 2, 3 };
                //houseList = db.House.Where(x => ids.Contains(x.ID)).ToList();
                ////除了like以外，数组的Contains还可以实现 SQL中IN的操作。上面的代码既实现了 select * from house where id in (1,2,3)

                var model = dbContext.TC_QCY_FILE.Where(u => u.TC_QCY01.StartsWith(prefix)).OrderByDescending(u => u.TC_QCY01).FirstOrDefault();
                return model;
            }
        }



        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool AddModel(TC_QCY_FILE model)
        {
            bool returnFlag = false;
            using (OraDBContext dbContext = new OraDBContext())
            {
                //增加
                dbContext.TC_QCY_FILE.Add(model);
                dbContext.SaveChanges();
                returnFlag = true;
            }
            return returnFlag;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static bool DeleteModel(string sid)
        {
            bool returnFlag = false;
            using (OraDBContext dbContext = new OraDBContext())
            {
                var model = dbContext.TC_QCY_FILE.Where(u => u.TC_QCY01 == sid).FirstOrDefault();
                if (model != null)
                {
                    dbContext.TC_QCY_FILE.Remove(model);

                    dbContext.SaveChanges();

                    returnFlag = true;
                }
            }
            return returnFlag;

        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <param name="sid"></param>
        public static void UpdateModel(TC_QCY_FILE model, string sid)
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                var editmodel = dbContext.TC_QCY_FILE.Where(u => u.TC_QCY01 == sid).FirstOrDefault();
                if (editmodel == null)
                {
                    return;//空
                }
                editmodel.TC_QCY02 = model.TC_QCY02;
                editmodel.TC_QCY03 = model.TC_QCY03;
                editmodel.TC_QCY04 = model.TC_QCY04;
                editmodel.TC_QCY05 = model.TC_QCY05;
                editmodel.TC_QCY06 = model.TC_QCY06;
                editmodel.TC_QCY07 = model.TC_QCY07;
                editmodel.TC_QCY08 = model.TC_QCY08;
                editmodel.TC_QCY09 = model.TC_QCY09;
                editmodel.TC_QCY10 = model.TC_QCY10;
                editmodel.TC_QCY11 = model.TC_QCY11;
                editmodel.TC_QCY12 = model.TC_QCY12;
                editmodel.TC_QCY13 = model.TC_QCY13;
                editmodel.TC_QCY14 = model.TC_QCY14;


                //提交修改 
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public static List<TC_QCY_FILE> GetList()
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                return dbContext.TC_QCY_FILE.ToList();
            }
        }

    }

    public partial class Tcqczs
    {

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name=" "></param>
        /// <returns></returns>
        public static bool IsExists(string sid,int qcz05)
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                //精确匹配名称
                var model = dbContext.TC_QCZ_FILE.Where(u => u.TC_QCZ01 == sid && u.TC_QCZ05 == qcz05).FirstOrDefault();
                if (model != null)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// 是否存在前一个
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="qcz05"></param>
        /// <returns></returns>
        public static bool IsExistsPre(string sid, int qcz05)
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                //精确匹配名称
                var model = dbContext.TC_QCZ_FILE.Where(u => u.TC_QCZ01 == sid && u.TC_QCZ05 < qcz05).OrderByDescending(u => u.TC_QCZ05).FirstOrDefault();
                if (model != null)
                    return true;
                else
                    return false;
            }
        }
        /// <summary>
        /// 是否存在未检项
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static bool IsExistsNotChecked(string sid)
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                //精确匹配名称
                var model = dbContext.TC_QCZ_FILE.Where(u => u.TC_QCZ01 == sid && u.TC_QCZ06 != 0 && u.TC_QCZ03 == null ).FirstOrDefault();
                if (model != null)
                    return true;
                else
                    return false;
            }

        }



        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static TC_QCZ_FILE GetModel(string sid, int qcz05)
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                var model = dbContext.TC_QCZ_FILE.Where(u => u.TC_QCZ01 == sid && u.TC_QCZ05 == qcz05).FirstOrDefault();
                return model;
            }
        }

        /// <summary>
        /// 获取上一个
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="qcz05"></param>
        /// <returns></returns>
        public static TC_QCZ_FILE GetModelPre(string sid, int qcz05)
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                var model = dbContext.TC_QCZ_FILE.Where(u => u.TC_QCZ01 == sid && u.TC_QCZ05 < qcz05 && u.TC_QCZ06 != 0).OrderByDescending(u => u.TC_QCZ05).FirstOrDefault();
                return model;
            }
        }



        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool AddModel(TC_QCZ_FILE model)
        {
            bool returnFlag = false;
            using (OraDBContext dbContext = new OraDBContext())
            {
                //增加
                dbContext.TC_QCZ_FILE.Add(model);
                dbContext.SaveChanges();
                returnFlag = true;
            }
            return returnFlag;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static bool DeleteModel(string sid,int qcz05)
        {
            bool returnFlag = false;
            using (OraDBContext dbContext = new OraDBContext())
            {
                var model = dbContext.TC_QCZ_FILE.Where(u => u.TC_QCZ01 == sid && u.TC_QCZ05 == qcz05).FirstOrDefault();
                if (model != null)
                {
                    dbContext.TC_QCZ_FILE.Remove(model);

                    dbContext.SaveChanges();

                    returnFlag = true;
                }
            }
            return returnFlag;
        }


        public static void UpdateModel(TC_QCZ_FILE model, string sid,int qcz05)
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                var editmodel = dbContext.TC_QCZ_FILE.Where(u => u.TC_QCZ01 == sid && u.TC_QCZ05 == qcz05).FirstOrDefault();
                if (editmodel == null)
                {
                    return;//空
                }
                editmodel.TC_QCZ02 = model.TC_QCZ02;
                editmodel.TC_QCZ03 = model.TC_QCZ03;
                editmodel.TC_QCZ04 = model.TC_QCZ04;
                //editmodel.TC_QCZ05 = model.TC_QCY05;
                editmodel.TC_QCZ06 = model.TC_QCZ06;
                editmodel.TC_QCZ07 = model.TC_QCZ07;
                editmodel.TC_QCZ08 = model.TC_QCZ08;
                editmodel.TC_QCZ09 = model.TC_QCZ09;
                editmodel.TC_QCZ10 = model.TC_QCZ10;
                editmodel.TC_QCZ11 = model.TC_QCZ11;

                //提交修改 
                dbContext.SaveChanges();
            }
        }

        public static bool DeleteModel(string sid)
        {
            bool returnFlag = false;
            using (OraDBContext dbContext = new OraDBContext())
            {
                var model = dbContext.TC_QCZ_FILE.Where(u => u.TC_QCZ01 == sid).ToList();
                if (model != null)
                {
                    foreach (TC_QCZ_FILE m in model)
                    {
                        dbContext.TC_QCZ_FILE.Remove(m);

                        dbContext.SaveChanges();
                    }
                    returnFlag = true;
                }
            }
            return returnFlag;
        }


        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public static List<TC_QCZ_FILE> GetList()
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                return dbContext.TC_QCZ_FILE.ToList();
            }
        }


        public static List<TC_QCZ_FILE> GetList(string sid)
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                return dbContext.TC_QCZ_FILE.Where(u => u.TC_QCZ01 == sid).OrderBy(u => u.TC_QCZ05).ToList();
            }
        }



    }

    public partial class Tcqcyys
    {

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name=" "></param>
        /// <returns></returns>
        public static bool IsExists(string sid)
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                //精确匹配名称
                var model = dbContext.TC_QCYY_FILE.Where(u => u.TC_QCYY01 == sid).FirstOrDefault();
                if (model != null)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// 是否已经审核
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static bool IsChecked(string sid)
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                //精确匹配名称
                var model = dbContext.TC_QCYY_FILE.Where(u => u.TC_QCYY01 == sid).FirstOrDefault();
                if (model != null && model.TC_QCYY12 == 2)
                    return true;
                else
                    return false;
            }

        }

        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static TC_QCYY_FILE GetModel(string sid)
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                var model = dbContext.TC_QCYY_FILE.Where(u => u.TC_QCYY01 == sid).FirstOrDefault();
                return model;
            }
        }



        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="prefix">前缀</param>
        /// <returns></returns>
        public static TC_QCYY_FILE GetMaxModel(string prefix)
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                var model = dbContext.TC_QCYY_FILE.Where(u => u.TC_QCYY01.StartsWith(prefix)).OrderByDescending(u => u.TC_QCYY01).FirstOrDefault();
                return model;
            }
        }



        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool AddModel(TC_QCYY_FILE model)
        {
            bool returnFlag = false;
            using (OraDBContext dbContext = new OraDBContext())
            {
                //增加
                dbContext.TC_QCYY_FILE.Add(model);
                dbContext.SaveChanges();
                returnFlag = true;
            }
            return returnFlag;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static bool DeleteModel(string sid)
        {
            bool returnFlag = false;
            using (OraDBContext dbContext = new OraDBContext())
            {
                var model = dbContext.TC_QCYY_FILE.Where(u => u.TC_QCYY01 == sid).FirstOrDefault();
                if (model != null)
                {
                    dbContext.TC_QCYY_FILE.Remove(model);

                    dbContext.SaveChanges();

                    returnFlag = true;
                }
            }
            return returnFlag;

        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <param name="sid"></param>
        public static void UpdateModel(TC_QCYY_FILE model, string sid)
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                var editmodel = dbContext.TC_QCYY_FILE.Where(u => u.TC_QCYY01 == sid).FirstOrDefault();
                if (editmodel == null)
                {
                    return;//空
                }
                editmodel.TC_QCYY02 = model.TC_QCYY02;
                editmodel.TC_QCYY03 = model.TC_QCYY03;
                editmodel.TC_QCYY04 = model.TC_QCYY04;
                editmodel.TC_QCYY05 = model.TC_QCYY05;
                editmodel.TC_QCYY06 = model.TC_QCYY06;
                editmodel.TC_QCYY07 = model.TC_QCYY07;
                editmodel.TC_QCYY08 = model.TC_QCYY08;
                editmodel.TC_QCYY09 = model.TC_QCYY09;
                editmodel.TC_QCYY10 = model.TC_QCYY10;
                editmodel.TC_QCYY11 = model.TC_QCYY11;
                editmodel.TC_QCYY12 = model.TC_QCYY12;
                editmodel.TC_QCYY13 = model.TC_QCYY13;
                editmodel.TC_QCYY14 = model.TC_QCYY14;


                //提交修改 
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public static List<TC_QCYY_FILE> GetList()
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                return dbContext.TC_QCYY_FILE.ToList();
            }
        }

    }

    public partial class Tcqczzs
    {

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name=" "></param>
        /// <returns></returns>
        public static bool IsExists(string sid, int qczz06)
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                //精确匹配名称
                var model = dbContext.TC_QCZZ_FILE.Where(u => u.TC_QCZZ01 == sid && u.TC_QCZZ06 == qczz06).FirstOrDefault();
                if (model != null)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// 是否存在前一个
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="qcz05"></param>
        /// <returns></returns>
        public static bool IsExistsPre(string sid, int qczz06)
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                //精确匹配名称
                var model = dbContext.TC_QCZZ_FILE.Where(u => u.TC_QCZZ01 == sid && u.TC_QCZZ06 < qczz06).OrderByDescending(u => u.TC_QCZZ06).FirstOrDefault();
                if (model != null)
                    return true;
                else
                    return false;
            }
        }
        /// <summary>
        /// 是否存在未检项
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static bool IsExistsNotChecked(string sid)
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                //精确匹配名称
                var model = dbContext.TC_QCZZ_FILE.Where(u => u.TC_QCZZ01 == sid && u.TC_QCZZ06 != 0 && u.TC_QCZZ03 == null).FirstOrDefault();
                if (model != null)
                    return true;
                else
                    return false;
            }

        }



        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static TC_QCZZ_FILE GetModel(string sid, int qczz06)
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                var model = dbContext.TC_QCZZ_FILE.Where(u => u.TC_QCZZ01 == sid && u.TC_QCZZ06 == qczz06).FirstOrDefault();
                return model;
            }
        }

        /// <summary>
        /// 获取上一个
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="qcz05"></param>
        /// <returns></returns>
        public static TC_QCZZ_FILE GetModelPre(string sid, int qczz06)
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                var model = dbContext.TC_QCZZ_FILE.Where(u => u.TC_QCZZ01 == sid && u.TC_QCZZ06 < qczz06 && u.TC_QCZZ06 != 0).OrderByDescending(u => u.TC_QCZZ05).FirstOrDefault();
                return model;
            }
        }



        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool AddModel(TC_QCZZ_FILE model)
        {
            bool returnFlag = false;
            using (OraDBContext dbContext = new OraDBContext())
            {
                //增加
                dbContext.TC_QCZZ_FILE.Add(model);
                dbContext.SaveChanges();
                returnFlag = true;
            }
            return returnFlag;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static bool DeleteModel(string sid, int qczz06)
        {
            bool returnFlag = false;
            using (OraDBContext dbContext = new OraDBContext())
            {
                var model = dbContext.TC_QCZZ_FILE.Where(u => u.TC_QCZZ01 == sid && u.TC_QCZZ06 == qczz06).FirstOrDefault();
                if (model != null)
                {
                    dbContext.TC_QCZZ_FILE.Remove(model);

                    dbContext.SaveChanges();

                    returnFlag = true;
                }
            }
            return returnFlag;
        }


        public static void UpdateModel(TC_QCZZ_FILE model, string sid, int qczz06)
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                var editmodel = dbContext.TC_QCZZ_FILE.Where(u => u.TC_QCZZ01 == sid && u.TC_QCZZ06 == qczz06).FirstOrDefault();
                if (editmodel == null)
                {
                    return;//空
                }
                editmodel.TC_QCZZ02 = model.TC_QCZZ02;
                editmodel.TC_QCZZ03 = model.TC_QCZZ03;
                editmodel.TC_QCZZ04 = model.TC_QCZZ04;
                editmodel.TC_QCZZ05 = model.TC_QCZZ05;
                editmodel.TC_QCZZ06 = model.TC_QCZZ06;
                editmodel.TC_QCZZ07 = model.TC_QCZZ07;
                editmodel.TC_QCZZ08 = model.TC_QCZZ08;
                editmodel.TC_QCZZ09 = model.TC_QCZZ09;
                editmodel.TC_QCZZ10 = model.TC_QCZZ10;
                editmodel.TC_QCZZ11 = model.TC_QCZZ11;

                //提交修改 
                dbContext.SaveChanges();
            }
        }

        public static bool DeleteModel(string sid)
        {
            bool returnFlag = false;
            using (OraDBContext dbContext = new OraDBContext())
            {
                var model = dbContext.TC_QCZZ_FILE.Where(u => u.TC_QCZZ01 == sid).ToList();
                if (model != null)
                {
                    foreach (TC_QCZZ_FILE m in model)
                    {
                        dbContext.TC_QCZZ_FILE.Remove(m);

                        dbContext.SaveChanges();
                    }
                    returnFlag = true;
                }
            }
            return returnFlag;
        }


        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public static List<TC_QCZZ_FILE> GetList()
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                return dbContext.TC_QCZZ_FILE.ToList();
            }
        }


        public static List<TC_QCZZ_FILE> GetList(string sid)
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                return dbContext.TC_QCZZ_FILE.Where(u => u.TC_QCZZ01 == sid).OrderBy(u => u.TC_QCZZ06).ToList();
            }
        }



    }

    public partial class Rvbss
    {

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public static List<RVBS_FILE> GetList()
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                return dbContext.RVBS_FILE.ToList();
            }
        }

        public static List<RVBS_FILE> GetList(string rvbs01)
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                return dbContext.RVBS_FILE.Where(u => u.RVBS01 == rvbs01).ToList();
            }
        }

    }


}
