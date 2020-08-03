using System;
using System.Collections;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyTiptop.OraCore;
using System.Data.SqlClient;
//using MyTiptop.Core;
 
using Oracle.ManagedDataAccess;
using Oracle.ManagedDataAccess.Client;

namespace MyTiptop.OraData 
{

    /// <summary>
    /// 料号
    /// </summary>
    public partial class ImaFiles
    {
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="PqName"></param>
        /// <returns></returns>
        public static bool IsExists(string sid)
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                //精确匹配名称
                var model = dbContext.IMA_FILE.Where(u => u.IMA01 == sid).FirstOrDefault();
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
        public static IMA_FILE GetModel(string sid)
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                var model = dbContext.IMA_FILE.Where(u => u.IMA01 == sid).FirstOrDefault();
                return model;
            }
        }

        public static IMA_FILE GetModel(string sid,string DBS)
        {
            using (OraDBContext dbContext = new OraDBContext(DBS))
            {
                var model = dbContext.IMA_FILE.Where(u => u.IMA01 == sid).FirstOrDefault();
                return model;
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool AddModel(IMA_FILE model)
        {
            bool returnFlag = false;
            using (OraDBContext dbContext = new OraDBContext())
            {
                 //增加
                dbContext.IMA_FILE.Add(model);

                dbContext.SaveChanges();

                returnFlag = true;
            }
            return returnFlag;
        }
        /// <summary>
        /// 删除 不许删除ima资料
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        //public static bool DeleteModel(string sid)
        //{
        //    bool returnFlag = false;
        //    using (OraDBContext dbContext = new OraDBContext())
        //    {
        //        IMA_FILE model = dbContext.IMA_FILE.Where(u => u.IMA01 == sid).FirstOrDefault();
        //        if (model != null)
        //        {
        //            dbContext.IMA_FILE.Remove(model);

        //            dbContext.SaveChanges();

        //            returnFlag = true;
        //        }
        //    }
        //    return returnFlag;
        //}
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <param name="sid"></param>
        //public static void UpdateModel(IMA_FILE model, string  sid)
        //{
        //    using (OraDBContext dbContext = new OraDBContext())
        //    {
        //        IMA_FILE editmodel = dbContext.IMA_FILE.Where(u => u.IMA01 == sid).FirstOrDefault();
        //        if (editmodel == null)
        //        {
        //            return;//空
        //        }
        //        editmodel.SortName = model.SortName;
        //        editmodel.Remark = model.Remark;

        //        //提交修改
        //        dbContext.SaveChanges();
        //    }
        //}


        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public static List<IMA_FILE> GetList()
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                return dbContext.IMA_FILE.ToList();
            }

        }

        /// <summary>
        /// 获取料号名称
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static string GetImaName(string  sid)
        {
            string returnFlag = "";
            using (OraDBContext dbContext = new OraDBContext())
            {
                IMA_FILE model = dbContext.IMA_FILE.Where(u => u.IMA01 == sid).FirstOrDefault();
                if (model != null)
                {
                    returnFlag = model.IMA02;
                }
            }
            return returnFlag;
        }

        public static string GetIma021(string sid)
        {
            string returnFlag = "";
            using (OraDBContext dbContext = new OraDBContext())
            {
                IMA_FILE model = dbContext.IMA_FILE.Where(u => u.IMA01 == sid).FirstOrDefault();
                if (model != null)
                {
                    returnFlag = model.IMA021;
                }
            }
            return returnFlag;
        }

    }



    /// <summary>
    /// BOM
    /// </summary>
    public partial class BmbFiles
    {

        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static List<BMB_FILE> GetBmbList(string sid)
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                var model = dbContext.BMB_FILE.Where(u => u.BMB01 == sid).ToList();
                return model;
            }
        }

        /// <summary>
        /// 获取 子料件开头为6、7 开头的 信息
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static List<BMB_FILE> GetBmbListFor(string sid)
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                var model = dbContext.BMB_FILE.Where(u => u.BMB01 == sid && ( u.BMB03.StartsWith("7") || u.BMB03.StartsWith("6")) ).ToList();
                return model;
            }
        }


    }

    /// <summary>
    /// BOM
    /// </summary>
    public partial class ObkFiles
    {

        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static string GetIma01(string sid)
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                string returnValue = "";
                var model = dbContext.OBK_FILE.Where(u => u.OBK03 == sid).FirstOrDefault();
                if (model != null)
                {
                    returnValue = model.OBK01;
                }
                return returnValue;
            }
        }


    }

    /// <summary>
    /// 
    /// </summary>
    public partial class TcBrdFiles
    {
        /// <summary>
        /// 是否存在（在此营运中心）
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static bool isExists(string tc_brd06)
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                //精确匹配名称
                var model = dbContext.TC_BRD_FILE.Where(u => u.TC_BRD06 == tc_brd06).FirstOrDefault();
                if (model != null)
                    return true;
                else
                    return false;
            }
        }
        /// <summary>
        /// 获取Model
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static TC_BRD_FILE getModel(string sid)
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                return dbContext.TC_BRD_FILE.Where(u => u.TC_BRD06 == sid).FirstOrDefault();
            }
        }

        public static int upDate(TC_BRD_FILE model, string sid)
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                TC_BRD_FILE editmodel = dbContext.TC_BRD_FILE.Where(u => u.TC_BRD06 == sid).FirstOrDefault();
                if (editmodel == null)
                {
                    return 0;//空
                }
                editmodel.TC_BRD20 = model.TC_BRD20;  //暂时只改到这两个字段。
                editmodel.TC_BRDACTI = model.TC_BRDACTI;

                //提交修改
                return dbContext.SaveChanges();
            }
        }


    }

    /// <summary>
    /// 
    /// </summary>
    public partial class TcBrgFiles
    {
        /// <summary>
        /// 是否存在（在此营运中心）
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static bool isExists(string tc_brg02)
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                //精确匹配名称
                var model = dbContext.TC_BRG_FILE.Where(u => u.TC_BRG02 == tc_brg02).FirstOrDefault();
                if (model != null)
                    return true;
                else
                    return false;
            }
        }

        public static void upDate(string newbarcode, string oldbarcode)
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                List<TC_BRG_FILE> editmodel = dbContext.TC_BRG_FILE.Where(u => u.TC_BRG02 == oldbarcode).ToList();
                if (editmodel == null)
                {
                    return ;//空
                }
                OracleConnection con = new OracleConnection();
                con = (OracleConnection)dbContext.Database.Connection;
                con.Open();

                string cmdstr = " update tc_brg_file set TC_BRG02 = '"+ newbarcode + "' where TC_BRG02 = '" + oldbarcode + "' ";

                if (editmodel != null)
                {
                    //foreach (TC_BRG_FILE m in editmodel)
                    //{
                    //    m.TC_BRG02 = model.TC_BRG02;
                    //} 
                    OracleCommand cmd = new OracleCommand(cmdstr, con);
                    cmd.ExecuteNonQuery();
                }

                if (con != null)
                {
                    con.Close();//连接需要关闭
                    con.Dispose();
                }

                //提交修改
                //dbContext.SaveChanges();
            } 
        }






    }

    /// <summary>
    /// 
    /// </summary>
    public partial class TcBrsFiles
    {
        /// <summary>
        /// 是否存在（在此营运中心）
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static bool isExists(string tc_brs02)
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                //精确匹配名称
                var model = dbContext.TC_BRS_FILE.Where(u => u.TC_BRS02 == tc_brs02).FirstOrDefault();
                if (model != null)
                    return true;
                else
                    return false;
            }
        }

        public static void upDate(string newbarcode, string oldbarcode)
        {
            // 主键不能通过EF更新
            //using (OraDBContext dbContext = new OraDBContext())
            //{
            //    List<TC_BRS_FILE> editmodel = dbContext.TC_BRS_FILE.Where(u => u.TC_BRS02 == sid).ToList();
            //    if (editmodel == null)
            //    {
            //        return;//空
            //    }
            //    if (editmodel != null)
            //    {
            //        foreach (TC_BRS_FILE m in editmodel)
            //        {
            //            m.TC_BRS02 = model.TC_BRS02;
            //        }
            //    }
            //    //提交修改
            //    dbContext.SaveChanges();
            //}

            using (OraDBContext dbContext = new OraDBContext())
            {
                List<TC_BRS_FILE> editmodel = dbContext.TC_BRS_FILE.Where(u => u.TC_BRS02 == oldbarcode).ToList();
                if (editmodel == null)
                {
                    return;//空
                }
                OracleConnection con = new OracleConnection();
                con = (OracleConnection)dbContext.Database.Connection;
                con.Open();

                string cmdstr = " update tc_brs_file set TC_BRS02 = '" + newbarcode + "' where TC_BRS02 = '" + oldbarcode + "' ";

                if (editmodel != null)
                {
                    //foreach (TC_BRG_FILE m in editmodel)
                    //{
                    //    m.TC_BRG02 = model.TC_BRG02;
                    //} 
                    OracleCommand cmd = new OracleCommand(cmdstr, con);
                    cmd.ExecuteNonQuery();
                }

                if (con != null)
                {
                    con.Close();//连接需要关闭
                    con.Dispose();
                }

                //提交修改
                //dbContext.SaveChanges();
            }
        }
    }


    /// <summary>
    /// 送检单
    /// </summary>
    public partial class QcsFiles
    {
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="PqName"></param>
        /// <returns></returns>
        public static bool IsExists(string sid)
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                //精确匹配名称
                var model = dbContext.QCS_FILE.Where(u => u.QCS01 == sid).FirstOrDefault();
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
        public static QCS_FILE GetModel(string sid)
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                var model = dbContext.QCS_FILE.Where(u => u.QCS01 == sid).FirstOrDefault();
                return model;
            }
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool AddModel(QCS_FILE model)
        {
            bool returnFlag = false;
            using (OraDBContext dbContext = new OraDBContext())
            {
                //增加
                dbContext.QCS_FILE.Add(model);

                dbContext.SaveChanges();

                returnFlag = true;
            }
            return returnFlag;
        }
 


        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public static List<QCS_FILE> GetList()
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                return dbContext.QCS_FILE.ToList();
            }
        }
        public static List<QCS_FILE> GetList(string sid)
        {
            using (OraDBContext dbContext = new OraDBContext())  //传入db
            {
                return dbContext.QCS_FILE.Where(u => u.QCS01 == sid).ToList();
            }
        }

        public static List<QCS_FILE> GetList(string sid,string DBS)
        {
            using (OraDBContext dbContext = new OraDBContext(DBS))  //传入db
            {
                return dbContext.QCS_FILE.Where(u => u.QCS01 == sid ).ToList();
            }
        }


        /// <summary>
        /// 获取要打印的列表
        /// </summary>
        /// <param name="DBS">数据库用户</param>
        /// <param name="plants">营运中心</param>
        /// <param name="startDate">起始日期</param>
        /// <param name="QCSUD10"></param>
        /// <param name="outsourcing">外协标记</param>
        /// <returns></returns>
        public static List<string> GetList(string DBS,string[] plants, string startDate,int QCSUD10 = 0,string outsourcing="0")
        {
            using (OraDBContext dbContext = new OraDBContext(DBS))  //传入db.
            {
                #region 
                ////QCSUD10 = 1;
                //DateTime dt = DateTime.Parse("2019-06-18");
                ////需要排除外协单
                //var aa  = dbContext.QCS_FILE.Where(u => plants.Contains(u.QCSPLANT) && u.QCSUD10 == QCSUD10 && u.QCS04 > dt).GroupBy(u => new { u.QCS01 }).Select(u => new {  u.Key.QCS01 }).ToList();  //按营运中心查,默认条件 未打印过
                //List<string> list = new List<string>();
                //foreach(var a in aa)
                //{
                //    list.Add(a.QCS01);
                //}
                //return list;
                #endregion 

                string s1 = ""; // OR ( rvb36 in ('2201','2204') and rvbplant in ('S32'))     //S32时 ，附加条件 
                string s2 = ""; // OR ( rvb36 in ('2401','2404','2451') and rvbplant in ('S34'))  // S34时 ，附加条件 

                string p = ""; 
                foreach (string s in plants) 
                {
                    if (s == "S32" && !"1".Equals(outsourcing))
                    {
                        s1 = " OR ( rvb36 in ('2201','2204') and rvbplant in ('S32')) ";
                    }
                    else if (s == "S34")
                    {
                        s2 = " OR ( rvb36 in ('2401','2404','2451') and rvbplant in ('S34')) ";
                    }
                    else if (s == "S3A")
                    {
                        s2 = " OR ( rvb36 in ('1112') and rvbplant in ('S3A')) ";
                    }
                    else
                    {
                        if (p.Length > 1)
                        {
                            p += ",";
                        }
                        p += "'" + s + "'";
                    }
                }
                if (p == "")
                {
                    p += "''";
                }
                //外协标记
                string filter = "";
                if ("0".Equals(outsourcing))
                {
                    //不包含外协
                    filter = " and  rvb04 not like '222%' ";
                }
                else if ("1".Equals(outsourcing))
                {
                    //只包含外协
                    filter = " and  rvb04 like '222%' ";
                }

                //string cmdStr = @"select QCS01  from QCS_FILE where QCSPLANT in (" + p + ") and QCSUD10 = "+ QCSUD10 + " and QCS04 >= to_date('"+ startDate + "', 'yyyy/MM/dd') and QCS01 not in (select rvb01 from rvb_file where rvb04 like '222%' and rvb12 >= to_date('"+ startDate + "', 'yyyy/MM/dd') group by rvb01) group by QCS01";
                //string cmdStr = @" select qcs01 from QCS_FILE,rvb_file where qcs01 = rvb01 and ( QCSPLANT in (" + p + ") " + s1 + "  " + s2 + " ) and QCSUD10 = 0  and QCS04 >= to_date('" + startDate + "', 'yyyy/MM/dd') and rvb04 not like '222%' group by QCS01";

                string cmdStr = @" select qcs01 from QCS_FILE,rvb_file where qcs01 = rvb01 and ( QCSPLANT in (" + p + ") " + s1 + "  " + s2 + " ) and QCSUD10 = 0  and QCS04 >= to_date('" + startDate + "', 'yyyy/MM/dd') " + filter + " group by QCS01";

                //执行语句.  
                var list = dbContext.Database.SqlQuery<string>(cmdStr).ToList();

                return list;

            } 
        } 


        public static void UpadteModelUD10(QCS_FILE model, int value, string DBS)
        {
            using (OraDBContext dbContext = new OraDBContext(DBS))  //传入db
            {
                QCS_FILE editmodel = dbContext.QCS_FILE.Where(u => u.QCS01 == model.QCS01 && u.QCS02 == model.QCS02 && u.QCS05 == model.QCS05).FirstOrDefault();
                if (editmodel == null)
                {
                    //空
                }
                editmodel.QCSUD10 = value;
                //提交修改
                dbContext.SaveChanges();
            }
        }

    } 
     
     
    /// <summary>
    /// 送检单
    /// </summary>
    public partial class PmcFiles
    {
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="PqName"></param>
        /// <returns></returns>
        public static bool IsExists(string sid)
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                //精确匹配名称
                var model = dbContext.PMC_FILE.Where(u => u.PMC01 == sid).FirstOrDefault();
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
        public static PMC_FILE GetModel(string sid)
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                var model = dbContext.PMC_FILE.Where(u => u.PMC01 == sid).FirstOrDefault();
                return model;
            }
        }

        public static PMC_FILE GetModel(string sid,string DBS)
        {
            using (OraDBContext dbContext = new OraDBContext(DBS))
            {
                var model = dbContext.PMC_FILE.Where(u => u.PMC01 == sid).FirstOrDefault();
                return model;
            }
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public static List<PMC_FILE> GetList()
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                return dbContext.PMC_FILE.ToList();
            }
        }

    }


    public partial class RvbFiles
    {

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public static List<RVB_FILE> GetList()
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                return dbContext.RVB_FILE.ToList();
            }
        }

        public static List<RVB_FILE> GetList(string rvb01)
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                return dbContext.RVB_FILE.Where(u => u.RVB01 == rvb01).ToList();
            }
        }

        public static RVB_FILE GetModel(string rvb01, int rvb02, string DBS)
        {
            using (OraDBContext dbContext = new OraDBContext(DBS))
            {
                return dbContext.RVB_FILE.Where(u => u.RVB01 == rvb01 && u.RVB02 == rvb02).FirstOrDefault();
            }
        }

    }


}
