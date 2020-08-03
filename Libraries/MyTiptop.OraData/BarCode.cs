using System;
using System.Collections;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyTiptop.OraCore;
using System.Data.SqlClient;
using MyTiptop.Core;
using Oracle.ManagedDataAccess.Client;

namespace MyTiptop.OraData
{
    /// <summary>
    /// 
    /// </summary>
    public partial class BarCodes
    {
        /// <summary>
        /// 仓库是否存在（在此营运中心）
        /// </summary>
        /// <param name="PqName"></param>
        /// <returns></returns>
        public static bool StoreIsExists(string storeCode)
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                //精确匹配名称
                var model = dbContext.IMD_FILE.Where(u => u.IMD01 == storeCode).FirstOrDefault();
                if (model != null)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// 批号、仓库是否存在
        /// </summary>
        /// <param name="barcode"></param>
        /// <param name="storecode"></param>
        /// <returns></returns>
        public static bool BarCodeIsexists(string barcode, string storecode)
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                //精确匹配名称
                var model = dbContext.IMGS_FILE.Where(u => u.IMGS02 == storecode && u.IMGS06 == barcode).FirstOrDefault();
                if (model != null)
                    return true;
                else
                    return false;
            }
        }

        public static string GetImgs8(string barcode, string storecode)
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                string returnValue = "-";

                //精确匹配名称
                var model = dbContext.IMGS_FILE.Where(u => u.IMGS02 == storecode && u.IMGS06 == barcode).FirstOrDefault();
                if (model != null)
                    returnValue = model.IMGS08.ToString();

                return returnValue;

            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <param name="sid"></param>
        public static void UpdateImgs(IMGS_FILE model, string barcode, string storecode)
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                var editmodel = dbContext.IMGS_FILE.Where(u => u.IMGS02 == storecode && u.IMGS06 == barcode).FirstOrDefault();
                if (editmodel == null)
                {
                    return;//空
                }
                editmodel.IMGS08 = model.IMGS08;

                //提交修改
                dbContext.SaveChanges();
            }
        }


        public static DataTable getsfmreport(DateTime? t)
        {

            var list = SfmFiles.GetSfnList(t);

            DataTable dt = new DataTable();
            DataColumn dc = null;
            dc = dt.Columns.Add("1", Type.GetType("System.Int32"));
            dc = dt.Columns.Add("2", Type.GetType("System.String"));
            dc = dt.Columns.Add("3", Type.GetType("System.String"));
            dc = dt.Columns.Add("4", Type.GetType("System.String"));
            dc = dt.Columns.Add("5", Type.GetType("System.String"));
            dc = dt.Columns.Add("6", Type.GetType("System.String"));
            dc = dt.Columns.Add("7", Type.GetType("System.String"));
            dc = dt.Columns.Add("8", Type.GetType("System.String"));
            dc = dt.Columns.Add("9", Type.GetType("System.String"));

            if (list != null && list.Count > 0)
            {
                int sortno = 1;
                foreach (TC_SFN_FILE m in list)
                {
                    //根据编码再一次循环

                    var bmbList = BmbFiles.GetBmbListFor(ObkFiles.GetIma01(m.TC_SFN04));

                    foreach (BMB_FILE bmbModel in bmbList)
                    {
                        DataRow row = dt.NewRow();
                        row[0] = sortno.ToString();  //序号
                        row[1] = SflFiles.GetSflName(m.TC_SFN03);  //线别 。序号合并去除重复项
                        row[2] = m.TC_SFN04;  //编码
                        row[3] = ImaFiles.GetImaName(bmbModel.BMB03);  //名称  . 指BOM表 子料件编码 6、7 开头的名称
                        row[4] = ImaFiles.GetIma021(bmbModel.BMB03);  //图号
                        row[5] = bmbModel.BMB06 / bmbModel.BMB07;  //用量
                        row[6] = m.TC_SFN05 * (bmbModel.BMB06 / bmbModel.BMB07);  //计划数量
                        row[7] = "";  //实际备货数量
                        row[8] = "";  //备注

                        #region 
                        //row[0] = "0";  //序号
                        //row[1] = "1";  //线别 。序号合并去除重复项
                        //row[2] = "2";  //编码
                        //row[3] = "3";  //名称
                        //row[4] = "4";  //图号
                        //row[5] = "5";  //用量
                        //row[6] = "6";  //计划数量
                        //row[7] = "7";  //实际备货数量
                        //row[8] = "8";  //备注

                        #endregion

                        dt.Rows.Add(row);
                        sortno++;
                    }
                }
            }

            return dt;


        }




    }

    /// <summary>
    /// 生产排产
    /// </summary>
    public partial class SfmFiles
    {

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public static List<TC_SFM_FILE> GetSfmList(DateTime t)
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                return dbContext.TC_SFM_FILE.Where(u => u.TC_SFM02 == t).ToList();
            }

        }


        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public static List<TC_SFN_FILE> GetSfnList(DateTime? t)
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                string cmdStr = "";

                if (t == null)
                {
                    cmdStr = "select * from tc_sfn_file  where tc_sfn01 in ( select tc_sfm01  from tc_sfm_file  where tc_sfm02 = to_date('','yy/mm/dd') )";
                }

                else
                {
                    cmdStr = "select * from tc_sfn_file  where tc_sfn01 in ( select tc_sfm01  from tc_sfm_file  where tc_sfm02 = to_date('" + TypeHelper.StringToDateTime(t.ToString()).ToShortDateString() + "','yy/mm/dd') )";
                }



                var list = dbContext.Database.SqlQuery<TC_SFN_FILE>(cmdStr).ToList();

                return list;
            }

        }

    }


    public partial class SflFiles
    {
        //获取
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static TC_SFL_FILE GetModel(string sid)
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                var model = dbContext.TC_SFL_FILE.Where(u => u.TC_SFL01 == sid).FirstOrDefault();
                return model;
            }
        }

        public static string GetSflName(string sid)
        {
            //获取线别名称
            using (OraDBContext dbContext = new OraDBContext())
            {
                string returnValue = "";
                var model = dbContext.TC_SFL_FILE.Where(u => u.TC_SFL01 == sid).FirstOrDefault();
                if (model != null)
                {
                    returnValue = model.TC_SFL02;
                }

                return returnValue;

            }
        }


    }

    public partial class Brefiles
    {

        public static TC_BRE_FILE GetModel(string sid)
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                TC_BRE_FILE model = dbContext.TC_BRE_FILE.Where(u => u.TC_BRE01 == sid).FirstOrDefault();
                if (model == null)
                {
                    return null;//空
                }
                return model;
            }
        }


        public static void UpdateModel(TC_BRE_FILE model, string sid)
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                TC_BRE_FILE editmodel = dbContext.TC_BRE_FILE.Where(u => u.TC_BRE01 == sid).FirstOrDefault();
                if (editmodel == null)
                {
                    return;//空
                }

                //tc_breud05 = sysdate,tc_breuser = '" + p_tc_breuser + "'


                editmodel.TC_BREUD05 = model.TC_BREUD05;  //暂时只改到这两个字段。
                editmodel.TC_BREUSER = model.TC_BREUSER;

                //提交修改
                dbContext.SaveChanges();
            }
        }
    }


    public partial class XxuFiles
    {
        /// <summary>
        /// 获取tc_xxu001
        /// </summary>
        /// <param name="tc_xxu011"></param>
        /// <returns></returns>
        public static string Get_tc_xxu001(string tc_xxu011)
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                string returnValue = "";
                var model = dbContext.TC_XXU_FILE.Where(u => u.TC_XXU011 == tc_xxu011).OrderByDescending(u => u.TC_XXU001).FirstOrDefault(); //倒序 用于取最后一个。
                if (model != null)
                {
                    returnValue = model.TC_XXU001;
                }

                return returnValue;
            }
        }
    }


    public partial class bsfp620_manager
    {



        //public ArrayList<bsfp620> b620_getdata(String p_sfv01)
        //{       
        #region
        //DBConnect conn = new DBConnect();
        //ArrayList<bsfp620> p_list = new ArrayList<bsfp620>();
        //String l_sql = "SELECT SUM(rvbs06) FROM rvbs_file WHERE rvbs00='asft620' AND rvbs01='" + p_sfv01
        //                + "' AND rvbs021=?";
        ////修改  已过账单据不显示单身   by  cjq   20180206
        //String sql = "select sfv04,ima02,ima021,gfe02,SUM(sfv09) from sfv_file,gfe_file,ima_file,sfu_file"
        //             + " where sfv01='" + p_sfv01 + "' AND sfv08 = gfe01 AND sfv04 = ima01"
        //             + " and sfv01=sfu01 and sfupost='N' GROUP BY sfv04,ima02,ima021,gfe02";
        //ResultSet rs = conn.executeQuery(sql);
        //while (rs.next())
        //{
        //    bsfp620 g_bsfp620 = new bsfp620();
        //    g_bsfp620.setsfv04(rs.getString(1));
        //    g_bsfp620.setima02(rs.getString(2));
        //    g_bsfp620.setima021(rs.getString(3));
        //    l_sfv09 = rs.getFloat(5);
        //    try
        //    {
        //        conn.prepareStatement(l_sql);
        //        conn.setString(1, rs.getString(1));
        //        ResultSet rs1 = conn.executeQuery();
        //        while (rs1.next())
        //        {
        //            l_rvbs06 = rs1.getFloat(1);
        //            if (l_rvbs06 > 0)
        //            {
        //                l_sfv09 = l_sfv09 - l_rvbs06;
        //            }
        //            break;
        //        }
        //    }
        //    catch (SQLException e)
        //    {
        //        e.printStackTrace();
        //    }
        //    g_bsfp620.setsfv09(l_sfv09);
        //    g_bsfp620.setgfe02(rs.getString(4));

        //    p_list.add(g_bsfp620);
        //}
        //return p_list;
        #endregion
        //}

        public static DataTable b620_getdata(String p_sfv01)
        {

            float l_sfv09 = 0;
            float l_rvbs06 = 0;
            //DataTable dtValue = new DataTable();

            string l_sql = "SELECT nvl(SUM(rvbs06),0) FROM rvbs_file WHERE rvbs00='asft620' AND rvbs01='" + p_sfv01
                            + "' AND rvbs021='{0}'";
            //修改  已过账单据不显示单身   by  cjq   20180206 .. 去掉已经过账不显示
            string sql = "select sfv04,ima02,ima021,gfe02,SUM(sfv09) as sfv09 from sfv_file,gfe_file,ima_file,sfu_file"
                         + " where sfv01='" + p_sfv01 + "' AND sfv08 = gfe01 AND sfv04 = ima01"
                         + " and sfv01=sfu01  and sfupost='Y' GROUP BY sfv04,ima02,ima021,gfe02";  //  + " and sfv01=sfu01 and sfupost='N' GROUP BY sfv04,ima02,ima021,gfe02";

            //ResultSet rs = conn.executeQuery(sql);

            DataTable dt = OraRDBSHelper.ExecuateSql(sql);

            //DataRow newRow = new dt.DataRow();

            dt.Columns.Add("l_sfv09", typeof(string)); //数据类型为 文本

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {

                    l_sfv09 = TypeHelper.StringToInt(row[4].ToString());
                    l_sql = String.Format(l_sql, row[0].ToString());
                    l_rvbs06 = OraRDBSHelper.ExecuteScalarSql(l_sql);
                    l_sfv09 = l_sfv09 - l_rvbs06;

                    row["l_sfv09"] = l_sfv09;

                }
            }

            return dt;

            //while (rs.next())
            //{
            //    bsfp620 g_bsfp620 = new bsfp620();
            //    g_bsfp620.setsfv04(rs.getString(1));
            //    g_bsfp620.setima02(rs.getString(2));
            //    g_bsfp620.setima021(rs.getString(3));
            //    l_sfv09 = rs.getFloat(5);
            //    try
            //    {
            //        conn.prepareStatement(l_sql);
            //        conn.setString(1, rs.getString(1));
            //        ResultSet rs1 = conn.executeQuery();
            //        while (rs1.next())
            //        {
            //            l_rvbs06 = rs1.getFloat(1);
            //            if (l_rvbs06 > 0)
            //            {
            //                l_sfv09 = l_sfv09 - l_rvbs06;
            //            }
            //            break;
            //        }
            //    }
            //    catch (SQLException e)
            //    {
            //        e.printStackTrace();
            //    }
            //    g_bsfp620.setsfv09(l_sfv09);
            //    g_bsfp620.setgfe02(rs.getString(4));

            //    p_list.add(g_bsfp620);
            //}
            //return p_list;

        }
    }


    public partial class TcBrbFiles
    {
        public static List<String> get_tc_brb11(String p_tc_bre01, String p_type)
        {
            int l_tc_brb11 = 0;
            int l_qry = 0;
            int l_n = 0;
            int l_n1 = 0;
            int l_n2 = 0;
            //Date l_tc_bredate = new Date();
            string l_tc_bredate = "";
            String l_tc_bre04 = "";
            String l_tc_brb01 = "";
            String sql = "";
            String p_sql = "";
            String r_type = "Y";
            List<String> r_values = new List<String>();

            String l_sql = "select COUNT(*) from tc_brb_file where tc_brb03='"+ p_tc_bre01 + "'";

            l_qry = OraRDBSHelper.ExecuteScalarSql(l_sql);

            //ResultSet rs = conn.executeQuery(l_sql);
            //if (rs.next())
            //{
            //    l_qry = rs.getInt(1);
            //}

            if (l_qry > 0)
            {
                if (p_type.Equals("A"))
                {
                    l_sql = "select tc_bre04,tc_bredate,tc_brb18 from tc_bre_file,tc_brb_file where tc_bre01=tc_brb03 AND tc_bre01='"+ p_tc_bre01 + "'";
                    sql = "select tc_brb01,tc_brb11,count(*) from tc_brb_file,tc_brf_file where tc_brb03=tc_brf01 and tc_brf01='"+ p_tc_bre01 + "' group by tc_brb01,tc_brb11";
                    //p_sql = "SELECT COUNT(*) FROM tc_brh_file,tc_brb_file WHERE tc_brh01=tc_brb03 AND tc_brb01=? AND tc_brh04=? AND tc_brhdate=?";
                    p_sql = "SELECT COUNT(*) FROM tc_brh_file,tc_brb_file WHERE tc_brh01=tc_brb03 AND tc_brb01='{0}' AND tc_brh04='{1}' AND tc_brhdate={2}";
                }
                else if (p_type.Equals("O"))
                {
                    l_sql = "select tc_bre04,tc_bredate,count(*) from tc_bre_file,tc_brf_file where tc_bre01=tc_brf01 and tc_bre01='"+ p_tc_bre01 + "' GROUP BY tc_bre04,tc_bredate";
                }
                else if (p_type.Equals("N"))
                {
                    l_sql = "select tc_brb02,tc_brb27,tc_brb11 from tc_brb_file where tc_brb03='"+ p_tc_bre01 + "'";
                }
                else
                {
                    l_sql = "select tc_bre04,tc_bredate,tc_brb11 from tc_bre_file,tc_brb_file where tc_bre01=tc_brb03 AND tc_bre01='"+ p_tc_bre01 + "'";
                }

                DataTable dt = OraRDBSHelper.ExecuateSql(l_sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    l_tc_bre04 = dt.Rows[0][0].ToString();
                    l_tc_bredate = dt.Rows[0][1].ToString();
                    l_tc_brb11 = Convert.ToInt32(dt.Rows[0][2].ToString());
                }

                //ResultSet rs = conn.executeQuery(l_sql);
                //if (rs.next())
                //{
                //    l_tc_bre04 = rs.getString(1);
                //    l_tc_bredate = rs.getDate(2);
                //    l_tc_brb11 = rs.getInt(3);
                //}


                if (l_tc_brb11 == 0) { r_type = "B"; }
                if (p_type.Equals("A"))
                {
                    try
                    {
                       // ResultSet rs = conn.executeQuery(sql);
                         DataTable rs = OraRDBSHelper.ExecuateSql(sql);
                        if(rs!=null && rs.Rows.Count > 0)
                        {
                            l_tc_brb01 = rs.Rows[0][0].ToString(); // .getString(1);
                            l_n = Convert.ToInt32( rs.Rows[0][1].ToString());
                            l_n1 = Convert.ToInt32(rs.Rows[0][2].ToString());
                        }

                        //if (rs.next())
                        //{
                        //    l_tc_brb01 = rs.getString(1);
                        //    l_n = rs.getInt(2);
                        //    l_n1 = rs.getInt(3);
                        //}
                        //rs.close();
                        //conn.prepareStatement(p_sql);
                        //conn.setString(1, l_tc_brb01);
                        //conn.setString(2, l_tc_bre04);
                        //conn.setDate(3, l_tc_bredate);

                        DateTime dtnow = DateTime.Now;

                        if (l_tc_bredate.Length > 0)
                        {
                            dtnow = TypeHelper.StringToDateTime(l_tc_bredate); 
                        }

                       string dtnowStr  =  dtnow.ToString("yyyy/MM/dd");

                    
                       l_tc_bredate = "to_date('" + dtnowStr.Substring(0,10) + "', 'yyyy/mm/dd')";// "to_date('2015/10/15', 'yyyy/mm/dd')";

                        p_sql = String.Format(p_sql, l_tc_brb01, l_tc_bre04, l_tc_bredate);

                        l_n2 = OraRDBSHelper.ExecuteScalarSql(p_sql);

                        //rs = conn.executeQuery();
                        //if (rs.next())
                        //{
                        //    l_n2 = rs.getInt(1);
                        //}
                    }
                    catch (Exception  )
                    {
                        r_type = "B";
                        l_tc_brb11 = 0;
                    }
                    if (l_n > l_n1)
                    {
                        l_tc_brb11 = 0;
                        r_type = "C";
                    }
                }
            }
            else
            {
                r_type = "A";
                l_tc_brb11 = 0;
            }

            r_values.Add(r_type);
            r_values.Add(l_tc_brb11.ToString()); 
            return r_values;
        }


        public static  String get_tc_brb22(String p_tc_brb03)
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                string returnValue = "";
                var model = dbContext.TC_BRB_FILE.Where(u => u.TC_BRB03 == p_tc_brb03).FirstOrDefault();  
                if (model != null)
                {
                    returnValue = model.TC_BRB22;
                }

                return returnValue;
            }

        }

        //public String get_tc_brb22(String p_tc_brb03)
        //{
        //    String l_tc_brb22 = null;
        //    DBConnect conn = new DBConnect();

        //    // conn = new DBConnect();
        //    String l_sql = "select tc_brb22 from tc_brb_file where tc_brb03='"+ p_tc_brb03 + "'";

        //        ResultSet rs = conn.executeQuery(l_sql);
        //        if (rs.next())
        //        {
        //            l_tc_brb22 = rs.getString(1);
        //        }

        //    return l_tc_brb22;
        //}


        //public String ins_imn(String p_imm01, String p_imm16, String p_immplant, String p_imgs06, String p_imgs05_n)
        //{
        //    String l_success = "A";
        //    String l_immlegal = "";
        //    String l_imm14 = "";
        //    String l_imgs01 = "";
        //    String l_imgs02 = "";
        //    String l_imgs03 = "";
        //    String l_imgs04 = "";
        //    String l_imgs05 = "";
        //    String l_imgs06 = "";
        //    String l_imgs07 = "";
        //    float l_imgs08 = 0f;
        //    String l_imgs09 = "";
        //    String l_imgs10 = "";
        //    String l_imgs11 = "";
        //    String l_imgsplant = "";
        //    String l_imgslegal = "";
        //    String l_imgs02_n = "";
        //    String l_imgs03_n = "";
        //    int l_cnt = 0;
        //    int l_sum = 0;
        //    // int l_pacode_cnt = 0; //NO.20171207 by cjq 包装票数量
        //    int l_rpt_cnt = 0; // NO.20171207 by cjq 库存数量
          
        //    String g_date = df.format(new Date());
        //    String g_time = df1.format(new Date()).substring(8);
          
        //    // NO.20171207 by cjq begin 程序搬移
        //    l_sql = "SELECT imgs01,imgs02,imgs03,imgs04,imgs05,imgs06,imgs07,imgs08,TO_CHAR(imgs09,'yyyyMMdd'),imgs10,imgs11,imgsplant,imgslegal"
        //            + " FROM imgs_file WHERE imgs08>0 AND imgs06 = '" + p_imgs06 + "'";

        //    ResultSet rs = conn.executeQuery(l_sql);
        //    if (rs.next())
        //    {
        //        l_imgs01 = rs.getString(1);
        //        l_imgs02 = rs.getString(2);
        //        l_imgs03 = rs.getString(3);
        //        l_imgs04 = rs.getString(4);
        //        l_imgs05 = rs.getString(5);
        //        l_imgs06 = rs.getString(6);
        //        l_imgs07 = rs.getString(7);
        //        l_imgs08 = rs.getFloat(8);
        //        l_imgs09 = rs.getString(9);
        //        l_imgs10 = rs.getString(10);
        //        l_imgs11 = rs.getString(11);
        //        l_imgsplant = rs.getString(12);
        //        l_imgslegal = rs.getString(13);
        //    }
        //    // insert imn_file
        //    if (l_success.equals("A"))
        //    {
        //        // NO.20171207 by cjq begin 程序搬移
        //        if (l_cnt == 0)
        //        {
        //            l_sql = "INSERT INTO imm_file(imm01,imm02,imm03,imm04,imm10,immacti,immuser,immgrup,immmodu,"
        //                    + "immdate,immconf,imm14,immspc,immplant,immlegal,immoriu,immorig,imm15,imm16,immmksg)"
        //                    + " VALUES('" + p_imm01 + "',TO_DATE('" + g_date + "','yyyyMMdd'),'Y','Y','1','Y','"
        //                    + p_imm16 + "','" + l_imm14 + "','" + p_imm16 + "',TO_DATE('" + g_date + "','yyyyMMdd'),'Y','"
        //                    + l_imm14 + "','0','" + p_immplant + "','" + l_immlegal + "','" + p_imm16
        //                    + "','" + l_imm14 + "','1','" + p_imm16 + "','N')";
             
        //             conn.executeUpdate(l_sql);
        //        }

        //        l_sum = l_sum + 1;
        //        if (l_success.equals("A"))
        //        {
        //            l_sql = "INSERT INTO imn_file(imn01,imn02,imn03,imn04,imn05,imn06,imn09,imn10,"
        //                    + "imn15,imn16,imn17,imn20,imn21,imn22,imn28,imn29,imnplant,imnlegal)"
        //                    + " VALUES('" + p_imm01 + "'," + l_sum + ",'" + l_imgs01 + "','" + l_imgs02
        //                    + "','" + l_imgs03 + "','" + l_imgs04 + "','" + l_imgs07 + "'," + l_imgs08
        //                    + ",'" + l_imgs02_n + "','" + l_imgs03_n + "','" + l_imgs04 + "','" + l_imgs07
        //                    + "',1," + l_imgs08 + ",'BC AUTO','N','" + p_immplant + "','" + l_immlegal + "')";
        //            conn.executeUpdate(l_sql);
     
        //            // insert rvbs
        //            l_sql = "INSERT INTO rvbs_file(rvbs00,rvbs01,rvbs02,rvbs03,rvbs04,rvbs05,rvbs06,rvbs07,rvbs08,"
        //                    + "rvbs021,rvbs022,rvbs09,rvbs10,rvbs11,rvbs12,rvbs13,rvbsplant,rvbslegal)"
        //                    + " VALUES('aimt324','" + p_imm01 + "'," + l_sum + ",'" + l_imgs05 + "','" + l_imgs06
        //                    + "',TO_DATE('" + l_imgs09 + "','yyyyMMdd')," + l_imgs08 + ",'2',' '" + ",'"
        //                    + l_imgs01 + "',1,-1,0,0,0,0,'" + p_immplant + "','" + l_immlegal + "')";

          
        //            conn.executeUpdate(l_sql);
       

        //            l_sql = "INSERT INTO rvbs_file(rvbs00,rvbs01,rvbs02,rvbs03,rvbs04,rvbs05,rvbs06,rvbs07,rvbs08,"
        //                    + "rvbs021,rvbs022,rvbs09,rvbs10,rvbs11,rvbs12,rvbs13,rvbsplant,rvbslegal)"
        //                    + " VALUES('aimt324','" + p_imm01 + "'," + l_sum + ",'" + p_imgs05_n + "','" + p_imgs06
        //                    + "',TO_DATE('" + l_imgs09 + "','yyyyMMdd')," + l_imgs08 + ",'2',' '" + ",'"
        //                    + l_imgs01 + "',1,1,0,0,0,0,'" + p_immplant + "','" + l_immlegal + "')";

                
        //           conn.executeUpdate(l_sql);

        //        }


        //        if (l_success.equals("A"))
        //        {
        //            #region 


        //            // NO.20171211 by cjq begin 2139，3139, 8139, 8239仓不插入数据
        //            if (!("2139".equals(l_imgs02_n) || "3139".equals(l_imgs02_n)
        //                    || "8139".equals(l_imgs02_n) || "8239"
        //                        .equals(l_imgs02_n)))
        //            {
                      

        //                if (l_cnt == 0)
        //                {
        //                    l_sql = "INSERT INTO imgs_file VALUES('" + l_imgs01
        //                            + "','" + l_imgs02_n + "','" + l_imgs03_n
        //                            + "','" + l_imgs04 + "','" + p_imgs05_n + "','"
        //                            + p_imgs06 + "','" + l_imgs07 + "'," + l_imgs08
        //                            + ",TO_DATE('" + l_imgs09 + "','yyyyMMdd'),'"
        //                            + l_imgs10 + "','" + l_imgs11 + "','"
        //                            + p_immplant + "','" + l_immlegal + "')";
        //                }
        //                else
        //                {
        //                    l_sql = "UPDATE imgs_file SET imgs08 = imgs08 + "
        //                            + l_imgs08 + " WHERE imgs01='" + l_imgs01
        //                            + "' AND imgs02 = '" + l_imgs02_n
        //                            + "' AND imgs03 = '" + l_imgs03_n
        //                            + "' AND imgs04 = '" + l_imgs04
        //                            + "' AND imgs05 = '" + p_imgs05_n
        //                            + "' AND imgs06 = '" + l_imgs06 + "'";
        //                }

        //                try
        //                {
        //                    conn.executeUpdate(l_sql);
        //                }
           
                       
        //            }
        //            #endregion
        //            // NO.20171211 by cjq end 2139，3139, 8139, 8239仓不插入数据
        //        }

        //        #region img tlf
        //        l_tlf024 = 0f;
        //        l_tlf18 = 0f;
        //        l_sql = "SELECT img10 FROM img_file WHERE img01 = '" + l_imgs01
        //                + "' AND img02 = '" + l_imgs02_n + "' AND img03 = '"
        //                + l_imgs03_n + "' AND img04 = '" + l_imgs04 + "'";

        //        ResultSet rs = conn.executeQuery(l_sql);
        //        if (rs.next())
        //        {
        //            l_tlf024 = rs.getFloat(1);
        //        }
        //        rs.close();

        //        l_sql = "SELECT SUM(img10*img21) FROM img_file WHERE img01 = '"
        //                + l_imgs01 + "'";
          
        //        ResultSet rs = conn.executeQuery(l_sql);
        //        if (rs.next())
        //        {
        //            l_tlf18 = rs.getFloat(1);
        //        }
        //        rs.close();

        //        if (l_success.equals("A") || !l_imgs02.equals(l_imgs02_n))
        //        {
        //            #region tlf
        //            l_sql = "INSERT INTO tlf_file(tlf01,tlf02,tlf020,tlf021,tlf022,tlf023,tlf024,tlf025,tlf026,tlf027,"
        //                    // 1
        //                    + "tlf03,tlf030,tlf031,tlf032,tlf033,tlf034,tlf035,tlf036,tlf037,tlf04,"
        //                    // 2
        //                    + "tlf05,tlf06,tlf07,tlf08,tlf09,tlf10,tlf11,tlf12,tlf13,tlf14,"
        //                    // 3
        //                    + "tlf15,tlf16,tlf17,tlf18,tlf19,tlf20,tlf21,tlf211,tlf212,tlf2131,"
        //                    // 4
        //                    + "tlf2132,tlf214,tlf215,tlf2151,tlf216,tlf2171,tlf2172,tlf219,tlf218,tlf220,"
        //                    // 5
        //                    + "tlf221,tlf222,tlf2231,tlf2232,tlf224,tlf225,tlf2251,tlf226,tlf2271,tlf2272,"
        //                    // 6
        //                    + "tlf229,tlf230,tlf231,tlf60,tlf61,tlf62,tlf63,tlf64,tlf65,tlf66,"
        //                    // 7
        //                    + "tlf901,tlf902,tlf903,tlf904,tlf905,tlf906,tlf907,tlf908,tlf909,tlf910,"
        //                    // 8
        //                    + "tlf99,tlf930,tlf931,tlf151,tlf161,tlf2241,tlf2242,tlf2243,tlfcost,tlf41,"
        //                    // 9
        //                    + "tlf42,tlf43,tlf211x,tlf212x,tlf21x,tlf221x,tlf222x,tlf2231x,tlf2232x,tlf2241x,"
        //                    // 10
        //                    + "tlf2242x,tlf2243x,tlf224x,tlf65x,tlfplant,tlflegal,tlf27,tlf28,tlf012,tlf013)"
        //                    + " VALUES('" + l_imgs01 + "','99',' ',' ',' ',' ',0,'','" + p_imm01 + "'," + l_sum + ",'"
        //                    // 1
        //                    + "50','" + p_immplant + "','" + l_imgs02_n + "','" + l_imgs03_n + "','" + l_imgs04
        //                    + "'," + l_tlf024 + ",'" + l_imgs07 + "','" + p_imm01 + "'," + l_sum + ",'',"
        //                    // 2
        //                    + "'',TO_DATE('" + g_date + "','yyyyMMdd'),TO_DATE('" + g_date + "','yyyyMMdd'),'"
        //                    + g_time + "','" + p_imm16 + "'," + l_imgs08 + ",'" + l_imgs07 + "',1,'aimt324','6101',"
        //                    // 3
        //                    + "'','',''," + l_tlf18 + ",'" + l_imm14 + "','',0,'','',0,"
        //                    // 4
        //                    + "0,0,0,0,0,0,0,0,'','',"
        //                    // 5
        //                    + "0,0,0,0,0,0,0,0,0,0,"
        //                    // 6
        //                    + "0,0,0,1,'141','',0,'','','',"
        //                    // 7
        //                    + "'','" + l_imgs02_n + "','" + l_imgs03_n + "','" + l_imgs04 + "','" + p_imm01 + "',"
        //                    + l_sum + ",1,'','','',"
        //                    // 8
        //                    + "'','','','','',0,0,0,' ','',"
        //                    // 9
        //                    + "'','','','','','','','','','',"
        //                    // 10
        //                    + "'','','','','" + p_immplant + "','" + l_immlegal + "','','','',0)";
        //            #endregion

        //            String sql = "UPDATE gep_file SET gep11=? WHERE 1=1";
        //            try
        //            {
        //                conn.prepareStatement(sql);
        //                conn.setString(1, l_sql);
        //                conn.executeUpdate();
        //            }
        //        }
        //        #endregion
        //    }

        //    return l_success;

        //}


    }


    public partial class RvbsFiles
    {


        public static  String ins_rvbs(String p_rvbs00, String p_rvbs01, String p_rvbs021, String p_rvbs04, float p_rvbs06, String p_rvbs03, String p_tc_breuser)
        {
            String l_success = "A";
            //int l_tc_brb11 = 0;
            int l_cnt = 0;
            int l_num = 0;
            int l_sfv03 = 0;
            float l_sfv09 = 0.0F;
            float l_rvbs06 = 0.0F;
            float l_sum = 0.0F;
            String l_sfv11 = null;
            String l_sfuplant = null;
            String l_sfulegal = null;
            String l_tc_smd003 = null;
            int l_rvbs03 = 0;
            //String g_date = this.df.format(new Date());
            string g_date = DateTime.Today.ToString("yyyy/MM/dd");
            String l_sql = ("select count(*) from rvbs_file where rvbs04='" + p_rvbs04 + "'");
            try{l_num = OraRDBSHelper.ExecuteScalarSql(l_sql);}
            catch (Exception  ){l_success = "D";}
            // NO.20171208 by cjq begin 添加存储位置判断
            l_sql = "select count(*) from tc_ime_file where tc_ime03='" + p_rvbs03 + "'";
            try{l_rvbs03 = OraRDBSHelper.ExecuteScalarSql(l_sql);if (l_rvbs03 == 0){l_success = "E";}}
            catch (Exception ){l_success = "E";}
            // NO.20171208 by cjq begin 添加存储位置判断
            if (l_rvbs03 > 0)
            {
                if (l_num == 0)
                {
                    l_sql = "SELECT tc_smd003 FROM tc_smd_file WHERE 1=1";
                    try
                    {
                        l_tc_smd003 = OraRDBSHelper.ExecuteScalarSqlForString(l_sql);
                    }
                    catch (Exception  )
                    {
                        l_success = "Z";
                    }
                    if (l_tc_smd003.Equals("YYYY"))
                    {
                        // 这里似乎永远执行不到
                        #region 
                        l_sql = ("SELECT sum(sfv09) FROM sfv_file WHERE sfv01='" + p_rvbs01 + "' AND sfv04='" + p_rvbs021 + "'");
                        try
                        {
                            l_sum = OraRDBSHelper.ExecuteScalarSql(l_sql);
                        }
                        catch (Exception ){l_success = "B";}

                        if (l_sum < p_rvbs06)
                        {
                            l_success = "X";
                        }
                        else
                        {
                            l_sql = ("SELECT DISTINCT sfv03,sfv09,sfv11,sfuplant,sfulegal FROM sfv_file,sfu_file,tc_bre_file WHERE sfv01=sfu01 AND sfv11=tc_breud01 AND sfv01='"
                                    + p_rvbs01 + "' AND sfv04='" + p_rvbs021 + "' AND tc_bre01 ='" + p_rvbs04 + "' ORDER BY 1");
                            try
                            {
                                //ResultSet rs = conn.executeQuery(l_sql);
                                //OracleDataReader rs = OraRDBSHelper.ExecuteReaderSql(l_sql);
                                DataTable rs = OraRDBSHelper.ExecuateSql(l_sql);
                                #region 

                                if (rs != null && rs.Rows.Count > 0)
                                {
                                    foreach (DataRow row in rs.Rows)
                                    {
                                        #region 
                                        l_sfv03 = Convert.ToInt32(row[0].ToString()); // .getInt(1);
                                        l_sfv09 = Convert.ToSingle(row[1].ToString()); // .getFloat(2);
                                        l_sfv11 = row[2].ToString(); //.getString(3);
                                        l_sfuplant = row[3].ToString();
                                        l_sfulegal = row[4].ToString();

                                        if (l_sfv03 > 0)
                                        {
                                            l_sql = ("select max(rvbs022) from rvbs_file where rvbs01='" + p_rvbs01 + "' AND rvbs02='" + l_sfv03 + "'");
                                            try
                                            {
                                                l_cnt = OraRDBSHelper.ExecuteScalarSql(l_sql);

                                            }
                                            catch (Exception) { l_success = "D"; }
                                            if (l_cnt > 0)
                                            {
                                                l_sql = ("select sum(rvbs06) from rvbs_file where rvbs01='" + p_rvbs01 + "' AND rvbs02='" + l_sfv03 + "'");
                                                try
                                                {
                                                    l_rvbs06 = OraRDBSHelper.ExecuteScalarSql(l_sql);

                                                }
                                                catch (Exception) { l_success = "E"; }
                                                l_sfv09 -= l_rvbs06;
                                                l_sum -= l_rvbs06;
                                                if (l_sum < p_rvbs06) { l_success = "X"; break; }
                                            }
                                            else { l_cnt = 0; }

                                            if (l_sfv09 > 0.0F)
                                            {
                                                if (l_sfv09 >= p_rvbs06)
                                                {
                                                    l_cnt++;
                                                    l_sql = ("INSERT INTO rvbs_file VALUES('" + p_rvbs00 + "','" + p_rvbs01
                                                            + "'," + l_sfv03 + ",'" + p_rvbs03 + "','" + p_rvbs04 + "',to_date('"
                                                            + g_date + "','yyyy/MM/dd')," + p_rvbs06 + ",'2',' ','" + p_rvbs021
                                                            + "'," + l_cnt + ",1," + p_rvbs06 + ",0,0,0,'" + l_sfuplant + "','" + l_sfulegal + "')"); // + "'," + l_cnt + ",1,0,0,0,0,'" + l_sfuplant + "','" + l_sfulegal + "')");
                                                    try
                                                    {
                                                        //conn1.executeUpdate(l_sql);
                                                        OraRDBSHelper.ExecuteSqlNonQuery(l_sql);
                                                    }
                                                    catch (Exception)
                                                    {
                                                        l_success = "F";
                                                    }
                                                    break;
                                                }
                                                else
                                                {
                                                    l_cnt++;

                                                    l_sql = ("INSERT INTO rvbs_file VALUES('" + p_rvbs00 + "','" + p_rvbs01
                                                            + "'," + l_sfv03 + ",'" + p_rvbs03 + "','" + p_rvbs04 + "',to_date('"
                                                            + g_date + "','yyyy/MM/dd')," + l_sfv09 + ",'2',' ','" + p_rvbs021
                                                            + "'," + l_cnt + ",1," + l_sfv09 + ",0,0,0,'" + l_sfuplant + "','" + l_sfulegal + "')");// + "'," + l_cnt + ",1,0,0,0,0,'" + l_sfuplant + "','" + l_sfulegal + "')");
                                                    try
                                                    {
                                                        //conn1.executeUpdate(l_sql);
                                                        OraRDBSHelper.ExecuteSqlNonQuery(l_sql);

                                                    }
                                                    catch (Exception)
                                                    {

                                                        l_success = "F";
                                                    }
                                                    p_rvbs06 -= l_sfv09;
                                                    if (p_rvbs06 == 0.0F)
                                                    {
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            l_success = "C";
                                        }
                                        #endregion
                                    }


                                }

                            }
                            catch (Exception  ex)
                            {
 
                                l_success = "B";
                            }
                        }
                        #endregion
                        #endregion 
                    }
                    else
                    {
                        //让 Y ,N 都走这段
                        l_sql = ("SELECT SUM(sfv09) FROM sfv_file,tc_brb_file WHERE sfv01='"
                                + p_rvbs01 + "' AND sfv04=tc_brb22 AND tc_brb03 ='" + p_rvbs04 + "'");
                        try
                        {
                            //ResultSet rs = conn.executeQuery(l_sql);
                            //if (rs.next())
                            //{
                            //    l_sum = rs.getFloat(1);
                            //}
                            //rs.close();
                            l_sum = OraRDBSHelper.ExecuteScalarSql(l_sql);
                        }
                        catch (Exception ){l_success = "B";}
                        if (l_sum < p_rvbs06){l_success = "X";}
                        else
                        {
                            l_sql = ("SELECT DISTINCT sfv03,sfv09,sfv11,sfuplant,sfulegal FROM sfv_file,sfu_file,tc_brb_file WHERE sfv01=sfu01 AND sfv01='"
                                    + p_rvbs01 + "' AND sfv04=tc_brb22 AND tc_brb03 ='" + p_rvbs04 + "' ORDER BY 1");
                            try
                            {

                                DataTable rs = OraRDBSHelper.ExecuateSql(l_sql);
     
                                if(rs != null && rs.Rows.Count > 0)
                                {
                                    foreach (DataRow row in rs.Rows)
                                    {
                                        #region 
                                        l_sfv03 = Convert.ToInt32(row[0].ToString()); // .getInt(1);
                                        l_sfv09 = Convert.ToSingle(row[1].ToString()); // .getFloat(2);
                                        l_sfv11 = row[2].ToString(); //.getString(3);
                                        l_sfuplant = row[3].ToString();
                                        l_sfulegal = row[4].ToString();
                                        String str7 = "select count(*) from rvbs_file where rvbs01='" + p_rvbs01 + "' AND rvbs02='" + l_sfv03 + "'";
                                        try
                                        {
                                            l_cnt = OraRDBSHelper.ExecuteScalarSql(str7);
                                        }
                                        catch (Exception ) { l_success = "D"; }
                                        if (l_cnt > 0)
                                        {
                                            String str8 = "select max(rvbs022) from rvbs_file where rvbs01='" + p_rvbs01 + "' AND rvbs02='" + l_sfv03 + "'";
                                            try
                                            {
                                                l_cnt = OraRDBSHelper.ExecuteScalarSql(str8);
                                            }
                                            catch (Exception )
                                            {
                                                l_success = "D";
                                            }
                                            String str9 = "select sum(rvbs06) from rvbs_file where rvbs01='" + p_rvbs01 + "' AND rvbs02='" + l_sfv03 + "'";
                                            try
                                            {
                                                l_rvbs06 = OraRDBSHelper.ExecuteScalarSql(str9);
                                            }
                                            catch (Exception )
                                            {
                                                l_success = "E";
                                            }
                                            l_sfv09 -= l_rvbs06;
                                            l_sum -= l_rvbs06;
                                            if (l_sum < p_rvbs06)
                                            {
                                                l_success = "X";
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            l_cnt = 0;
                                        }
                                        if (l_sfv09 > 0.0F)
                                        {
                                            if (l_sfv09 >= p_rvbs06)
                                            {
                                                l_cnt++;
                                                l_sql = ("INSERT INTO rvbs_file VALUES('" + p_rvbs00 + "','" + p_rvbs01
                                                        + "'," + l_sfv03 + ",'" + p_rvbs03 + "','" + p_rvbs04 + "',to_date('"
                                                        + g_date + "','yyyy/MM/dd')," + p_rvbs06 + ",'2',' ','" + p_rvbs021
                                                        + "'," + l_cnt + ",1," + p_rvbs06 + ",0,0,0,'" + l_sfuplant + "','" + l_sfulegal + "')");// + "'," + l_cnt + ",1,0,0,0,0,'" + l_sfuplant + "','" + l_sfulegal + "')");
                                                try
                                                {
                                                    // conn1.executeUpdate(l_sql);
                                                    OraRDBSHelper.ExecuteSqlNonQuery(l_sql);
                                                }
                                                catch (Exception )
                                                {
                                                    l_success = "F";
                                                }
                                                break;
                                            }
                                            else
                                            {
                                                l_cnt++;

                                                l_sql = ("INSERT INTO rvbs_file VALUES('" + p_rvbs00 + "','" + p_rvbs01
                                                        + "'," + l_sfv03 + ",'" + p_rvbs03 + "','" + p_rvbs04 + "',to_date('"
                                                        + g_date + "','yyyy/MM/dd')," + l_sfv09 + ",'2',' ','" + p_rvbs021
                                                        + "'," + l_cnt + ",1," + l_sfv09 + ",0,0,0,'" + l_sfuplant + "','" + l_sfulegal + "')");//   + "'," + l_cnt + ",1,0,0,0,0,'" + l_sfuplant + "','" + l_sfulegal + "')");
                                                try
                                                {
                                                    // conn1.executeUpdate(l_sql);
                                                    OraRDBSHelper.ExecuteSqlNonQuery(l_sql);
                                                }
                                                catch (Exception)
                                                {
                                                    // e.printStackTrace();
                                                    l_success = "F";
                                                }
                                                p_rvbs06 -= l_sfv09;
                                                if (p_rvbs06 == 0.0F)
                                                {
                                                    break;
                                                }
                                            }
                                        }
                                        #endregion
                                    }
                                }
                            }
                            catch (Exception  ){l_success = "B";}
                        }
                    }
                }
                else
                {
                    l_success = "W";
                }
            }
            else
            {
                l_success = "E";
            }

            #region 更新bre
            if (l_success.Equals("A"))
            {
                //String str6 = "update tc_bre_file set tc_breud05 = sysdate,tc_breuser='" + p_tc_breuser + "'  where tc_bre01='" + p_rvbs04 + "'";
                try
                {
                    // conn1.executeUpdate(str6);
                    var bremodel = Brefiles.GetModel(p_rvbs04);
                    if (bremodel != null)
                    {
                        bremodel.TC_BREUSER = p_tc_breuser;
                        bremodel.TC_BREUD05 = DateTime.Now;
                        Brefiles.UpdateModel(bremodel, p_rvbs04);
                    }
                }
                catch (Exception  )
                {
                    //e.printStackTrace();
                    l_success = "F";
                }
            }
            #endregion

            return l_success;
        }



        public static  int get_rvbs_sum_1(String p_rvbs01, String p_rvbs04, int p_count)
        {
            int n_sum = 0;
            //DBConnect conn = new DBConnect();

            String l_sql = "select sum(rvbs06) from rvbs_file where rvbs01='" + p_rvbs01 + "' AND rvbs04='" + p_rvbs04 + "'";

            try
            {
                //ResultSet rs = conn.executeQuery(l_sql);
                //while (rs.next())
                //{
                //    n_sum = rs.getInt(1);
                //    break;
                //}
                //rs.close();

                n_sum = OraRDBSHelper.ExecuteScalarSql(l_sql);


            }
            catch (Exception )
            {              
                n_sum = 0;
            }

            return (p_count - n_sum);
        }




        //public String ins_rvbs(String p_rvbs00, String p_rvbs01, String p_rvbs021, String p_rvbs04, float p_rvbs06, String p_rvbs03, String p_tc_breuser)
        //{
        //    String l_success = "A";
        //    int l_tc_brb11 = 0;
        //    int l_cnt = 0;
        //    int l_num = 0;
        //    int l_sfv03 = 0;
        //    float l_sfv09 = 0.0F;
        //    float l_rvbs06 = 0.0F;
        //    float l_sum = 0.0F;
        //    String l_sfv11 = null;
        //    String l_sfuplant = null;
        //    String l_sfulegal = null;
        //    String l_tc_smd003 = null;
        //    int l_rvbs03 = 0;

        //    String g_date = this.df.format(new Date());
        //    DBConnect conn = new DBConnect();
        //    DBConnect conn1 = new DBConnect();

        //    String l_sql = ("select count(*) from rvbs_file where rvbs04='" + p_rvbs04 + "'");
        //    try
        //    {
        //        ResultSet rs = conn.executeQuery(l_sql);
        //        if (rs.next())
        //        {
        //            l_num = rs.getInt(1);
        //        }
        //        rs.close();
        //    }
        //    catch (SQLException e)
        //    {
        //        e.printStackTrace();
        //        l_success = "D";
        //    }

        //    // NO.20171208 by cjq begin 添加存储位置判断
        //    l_sql = "select count(*) from tc_ime_file where tc_ime03='" + p_rvbs03 + "'";
        //    try
        //    {
        //        ResultSet rs = conn.executeQuery(l_sql);
        //        if (rs.next())
        //        {
        //            l_rvbs03 = rs.getInt(1);
        //        }
        //        rs.close();
        //    }
        //    catch (SQLException e)
        //    {
        //        e.printStackTrace();
        //        l_success = "E";
        //    }
        //    // NO.20171208 by cjq begin 添加存储位置判断
        //    if (l_rvbs03 > 0)
        //    {
        //        if (l_num == 0)
        //        {
        //            l_sql = "SELECT tc_smd003 FROM tc_smd_file WHERE 1=1";
        //            try
        //            {
        //                ResultSet rs = conn.executeQuery(l_sql);
        //                while (rs.next())
        //                {
        //                    l_tc_smd003 = rs.getString(1);
        //                }
        //                rs.close();
        //            }
        //            catch (SQLException e)
        //            {
        //                e.printStackTrace();
        //                l_success = "Z";
        //            }
        //            if (l_tc_smd003.equals("Y"))
        //            {
        //                l_sql = ("SELECT sum(sfv09) FROM sfv_file WHERE sfv01='" + p_rvbs01 + "' AND sfv04='" + p_rvbs021 + "'");
        //                try
        //                {
        //                    ResultSet rs = conn.executeQuery(l_sql);
        //                    if (rs.next())
        //                    {
        //                        l_sum = rs.getFloat(1);
        //                    }
        //                    rs.close();
        //                }
        //                catch (SQLException e)
        //                {
        //                    e.printStackTrace();
        //                    l_success = "B";
        //                }

        //                if (l_sum < p_rvbs06)
        //                {
        //                    l_success = "X";
        //                }
        //                else
        //                {
        //                    l_sql = ("SELECT DISTINCT sfv03,sfv09,sfv11,sfuplant,sfulegal FROM sfv_file,sfu_file,tc_bre_file WHERE sfv01=sfu01 AND sfv11=tc_breud01 AND sfv01='"
        //                            + p_rvbs01 + "' AND sfv04='" + p_rvbs021 + "' AND tc_bre01 ='" + p_rvbs04 + "' ORDER BY 1");
        //                    try
        //                    {
        //                        ResultSet rs = conn.executeQuery(l_sql);
        //                        while (rs.next())
        //                        {
        //                            l_sfv03 = rs.getInt(1);
        //                            l_sfv09 = rs.getFloat(2);
        //                            l_sfv11 = rs.getString(3);
        //                            l_sfuplant = rs.getString(4);
        //                            l_sfulegal = rs.getString(5);
        //                            if (l_sfv03 > 0)
        //                            {
        //                                l_sql = ("select max(rvbs022) from rvbs_file where rvbs01='" + p_rvbs01 + "' AND rvbs02='" + l_sfv03 + "'");
        //                                try
        //                                {
        //                                    ResultSet rs1 = conn1.executeQuery(l_sql);
        //                                    if (rs1.next())
        //                                    {
        //                                        l_cnt = rs1.getInt(1);
        //                                    }
        //                                    rs1.close();
        //                                }
        //                                catch (SQLException e)
        //                                {
        //                                    e.printStackTrace();
        //                                    l_success = "D";
        //                                }
        //                                if (l_cnt > 0)
        //                                {
        //                                    l_sql = ("select sum(rvbs06) from rvbs_file where rvbs01='" + p_rvbs01 + "' AND rvbs02='" + l_sfv03 + "'");
        //                                    try
        //                                    {
        //                                        ResultSet rs1 = conn1.executeQuery(l_sql);
        //                                        if (rs1.next())
        //                                        {
        //                                            l_rvbs06 = rs1.getFloat(1);
        //                                        }
        //                                        rs1.close();
        //                                    }
        //                                    catch (SQLException e)
        //                                    {
        //                                        e.printStackTrace();
        //                                        l_success = "E";
        //                                    }
        //                                    l_sfv09 -= l_rvbs06;
        //                                    l_sum -= l_rvbs06;
        //                                    if (l_sum < p_rvbs06)
        //                                    {
        //                                        l_success = "X";
        //                                        break;
        //                                    }
        //                                }
        //                                else
        //                                {
        //                                    l_cnt = 0;
        //                                }

        //                                if (l_sfv09 > 0.0F)
        //                                {
        //                                    if (l_sfv09 >= p_rvbs06)
        //                                    {
        //                                        l_cnt++;
        //                                        l_sql = ("INSERT INTO rvbs_file VALUES('" + p_rvbs00 + "','" + p_rvbs01
        //                                                + "'," + l_sfv03 + ",'" + p_rvbs03 + "','" + p_rvbs04 + "',to_date('"
        //                                                + g_date + "','yyyyMMdd')," + p_rvbs06 + ",'2',' ','" + p_rvbs021
        //                                                + "'," + l_cnt + ",1,0,0,0,0,'" + l_sfuplant + "','" + l_sfulegal + "')");
        //                                        try
        //                                        {
        //                                            conn1.executeUpdate(l_sql);
        //                                        }
        //                                        catch (SQLException e)
        //                                        {
        //                                            e.printStackTrace();
        //                                            l_success = "F";
        //                                        }
        //                                        break;
        //                                    }
        //                                    else
        //                                    {
        //                                        l_cnt++;

        //                                        l_sql = ("INSERT INTO rvbs_file VALUES('" + p_rvbs00 + "','" + p_rvbs01
        //                                                + "'," + l_sfv03 + ",'" + p_rvbs03 + "','" + p_rvbs04 + "',to_date('"
        //                                                + g_date + "','yyyyMMdd')," + l_sfv09 + ",'2',' ','" + p_rvbs021
        //                                                + "'," + l_cnt + ",1,0,0,0,0,'" + l_sfuplant + "','" + l_sfulegal + "')");
        //                                        try
        //                                        {
        //                                            conn1.executeUpdate(l_sql);
        //                                        }
        //                                        catch (SQLException e)
        //                                        {
        //                                            e.printStackTrace();
        //                                            l_success = "F";
        //                                        }
        //                                        p_rvbs06 -= l_sfv09;
        //                                        if (p_rvbs06 == 0.0F)
        //                                        {
        //                                            break;
        //                                        }
        //                                    }
        //                                }
        //                            }
        //                            else
        //                            {
        //                                l_success = "C";
        //                            }
        //                        }
        //                        rs.close();
        //                    }
        //                    catch (SQLException e)
        //                    {
        //                        e.printStackTrace();
        //                        l_success = "B";
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                l_sql = ("SELECT SUM(sfv09) FROM sfv_file,tc_brb_file WHERE sfv01='"
        //                        + p_rvbs01 + "' AND sfv04=tc_brb22 AND tc_brb03 ='" + p_rvbs04 + "'");
        //                try
        //                {
        //                    ResultSet rs = conn.executeQuery(l_sql);
        //                    if (rs.next())
        //                    {
        //                        l_sum = rs.getFloat(1);
        //                    }
        //                    rs.close();
        //                }
        //                catch (SQLException e)
        //                {
        //                    e.printStackTrace();
        //                    l_success = "B";
        //                }
        //                if (l_sum < p_rvbs06)
        //                {
        //                    l_success = "X";
        //                }
        //                else
        //                {
        //                    l_sql = ("SELECT DISTINCT sfv03,sfv09,sfv11,sfuplant,sfulegal FROM sfv_file,sfu_file,tc_brb_file WHERE sfv01=sfu01 AND sfv01='"
        //                            + p_rvbs01 + "' AND sfv04=tc_brb22 AND tc_brb03 ='" + p_rvbs04 + "' ORDER BY 1");
        //                    try
        //                    {
        //                        ResultSet rs = conn.executeQuery(l_sql);
        //                        while (rs.next())
        //                        {
        //                            l_sfv03 = rs.getInt(1);
        //                            l_sfv09 = rs.getFloat(2);
        //                            l_sfv11 = rs.getString(3);
        //                            l_sfuplant = rs.getString(4);
        //                            l_sfulegal = rs.getString(5);


        //                            String str7 = "select count(*) from rvbs_file where rvbs01='" + p_rvbs01 + "' AND rvbs02='" + l_sfv03 + "'";
        //                            try
        //                            {
        //                                ResultSet rs1 = conn1.executeQuery(str7);
        //                                if (rs1.next())
        //                                {
        //                                    l_cnt = rs1.getInt(1);
        //                                }
        //                                rs1.close();
        //                            }
        //                            catch (SQLException e)
        //                            {
        //                                e.printStackTrace();
        //                                l_success = "D";
        //                            }
        //                            if (l_cnt > 0)
        //                            {
        //                                String str8 = "select max(rvbs022) from rvbs_file where rvbs01='" + p_rvbs01 + "' AND rvbs02='" + l_sfv03 + "'";
        //                                try
        //                                {
        //                                    ResultSet rs1 = conn1.executeQuery(str8);
        //                                    if (rs1.next())
        //                                    {
        //                                        l_cnt = rs1.getInt(1);
        //                                    }
        //                                    rs1.close();
        //                                }
        //                                catch (SQLException e)
        //                                {
        //                                    e.printStackTrace();
        //                                    l_success = "D";
        //                                }
        //                                String str9 = "select sum(rvbs06) from rvbs_file where rvbs01='" + p_rvbs01 + "' AND rvbs02='" + l_sfv03 + "'";
        //                                try
        //                                {
        //                                    ResultSet rs1 = conn1.executeQuery(str9);
        //                                    if (rs1.next())
        //                                    {
        //                                        l_rvbs06 = rs1.getFloat(1);
        //                                    }
        //                                    rs1.close();
        //                                }
        //                                catch (SQLException e)
        //                                {
        //                                    e.printStackTrace();
        //                                    l_success = "E";
        //                                }
        //                                l_sfv09 -= l_rvbs06;
        //                                l_sum -= l_rvbs06;
        //                                if (l_sum < p_rvbs06)
        //                                {
        //                                    l_success = "X";
        //                                    break;
        //                                }
        //                            }
        //                            else
        //                            {
        //                                l_cnt = 0;
        //                            }

        //                            if (l_sfv09 > 0.0F)
        //                            {
        //                                if (l_sfv09 >= p_rvbs06)
        //                                {
        //                                    l_cnt++;

        //                                    l_sql = ("INSERT INTO rvbs_file VALUES('" + p_rvbs00 + "','" + p_rvbs01
        //                                            + "'," + l_sfv03 + ",'" + p_rvbs03 + "','" + p_rvbs04 + "',to_date('"
        //                                            + g_date + "','yyyyMMdd')," + p_rvbs06 + ",'2',' ','" + p_rvbs021
        //                                            + "'," + l_cnt + ",1,0,0,0,0,'" + l_sfuplant + "','" + l_sfulegal + "')");
        //                                    try
        //                                    {
        //                                        conn1.executeUpdate(l_sql);
        //                                    }
        //                                    catch (SQLException e)
        //                                    {
        //                                        e.printStackTrace();
        //                                        l_success = "F";
        //                                    }
        //                                    break;
        //                                }
        //                                else
        //                                {
        //                                    l_cnt++;

        //                                    l_sql = ("INSERT INTO rvbs_file VALUES('" + p_rvbs00 + "','" + p_rvbs01
        //                                            + "'," + l_sfv03 + ",'" + p_rvbs03 + "','" + p_rvbs04 + "',to_date('"
        //                                            + g_date + "','yyyyMMdd')," + l_sfv09 + ",'2',' ','" + p_rvbs021
        //                                            + "'," + l_cnt + ",1,0,0,0,0,'" + l_sfuplant + "','" + l_sfulegal + "')");
        //                                    try
        //                                    {
        //                                        conn1.executeUpdate(l_sql);
        //                                    }
        //                                    catch (SQLException e)
        //                                    {
        //                                        e.printStackTrace();
        //                                        l_success = "F";
        //                                    }
        //                                    p_rvbs06 -= l_sfv09;
        //                                    if (p_rvbs06 == 0.0F)
        //                                    {
        //                                        break;
        //                                    }
        //                                }
        //                            }
        //                        }
        //                        rs.close();
        //                    }
        //                    catch (SQLException e)
        //                    {
        //                        e.printStackTrace();
        //                        l_success = "B";
        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            l_success = "W";
        //        }
        //    }
        //    else
        //    {
        //        l_success = "E";
        //    }

        //    if (l_success.equals("A"))
        //    {
        //        String str6 = "update tc_bre_file set tc_breud05 = sysdate,tc_breuser='" + p_tc_breuser + "'  where tc_bre01='" + p_rvbs04 + "'";
        //        try
        //        {
        //            conn1.executeUpdate(str6);
        //        }
        //        catch (SQLException e)
        //        {
        //            e.printStackTrace();
        //            l_success = "F";
        //        }
        //    }
        //    try
        //    {
        //        conn.close();
        //        conn1.close();
        //    }
        //    catch (Exception e)
        //    {
        //        e.printStackTrace();
        //    }
        //    return l_success;
        //}


    }


    /// <summary>
    /// 仓库信息
    /// </summary>
    public partial class TcimeFiles
    {

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static bool IsExists(string ime03)
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                //精确匹配名称
                var model = dbContext.TC_IME_FILE.Where(u => u.TC_IME03 == ime03).FirstOrDefault();
                if (model != null)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static TC_IME_FILE GetModel(string ime03)
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                //精确匹配名称
                var model = dbContext.TC_IME_FILE.Where(u => u.TC_IME03 == ime03).FirstOrDefault();
                return model;
            }
        }
    }

    public partial class ImgsFiles
    {

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static bool IsExists(string imgs01, string imgs02, string imgs05, string imgs06)
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                //精确匹配名称
                var model = dbContext.IMGS_FILE.Where(u => u.IMGS01 == imgs01 && u.IMGS02 == imgs02 && u.IMGS05 == imgs05 && u.IMGS06 == imgs06).FirstOrDefault();
                if (model != null)
                    return true;
                else
                    return false;
            }
        }
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static IMGS_FILE GetModel(string imgs01, string imgs02, string imgs05, string imgs06)
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                var model = dbContext.IMGS_FILE.Where(u => u.IMGS01 == imgs01 && u.IMGS02 == imgs02 && u.IMGS05 == imgs05 && u.IMGS06 == imgs06).FirstOrDefault();
                return model;
            }
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool AddModel(IMGS_FILE model)
        {
            bool returnFlag = false;
            using (OraDBContext dbContext = new OraDBContext())
            {
                //增加
                dbContext.IMGS_FILE.Add(model);

                dbContext.SaveChanges();

                returnFlag = true;
            }
            return returnFlag;
        }

        /// <summary>
        /// 修改imgs08
        /// </summary>
        /// <param name="model"></param>
        /// <param name="sid"></param>
        public static void UpdateModel(IMGS_FILE model)
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                var editmodel = dbContext.IMGS_FILE.Where(u => u.IMGS01 == model.IMGS01 && u.IMGS02 == model.IMGS02 && u.IMGS05 == model.IMGS05 && u.IMGS06 == model.IMGS06).FirstOrDefault();
                if (editmodel == null)
                {
                    return;//空
                }
                editmodel.IMGS08 = model.IMGS08;
                
                //提交修改
                dbContext.SaveChanges();
            }
        }

        public static void ClearImgs(string imgs06)
        {
            using (OraDBContext dbContext = new OraDBContext())
            {
                var imgs = dbContext.IMGS_FILE.Where(u => u.IMGS06 == imgs06).ToList();
                foreach (IMGS_FILE model in imgs)
                {
                    model.IMGS08 = 0;
                    dbContext.SaveChanges();
                }
            }
        }

        /// <summary>
        /// 计算包装票imgs_file 数量
        /// </summary>
        /// <param name="imgs06"></param>
        public static void Save(string imgs06)
        {

            //0. 先把 imgs06 的 imgs 全部更新为0 ，再重新计算 ？？


            ClearImgs(imgs06);


            //1.获取rvbs表关于包装票的信息
            DataTable dt =GetRvbsInfoGroup(imgs06);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    IMGS_FILE imgsNewModel = new IMGS_FILE();

                    imgsNewModel.IMGS01 = row["RVBS021"].ToString();
                    imgsNewModel.IMGS03 = " ";
                    imgsNewModel.IMGS04 = " ";
                    imgsNewModel.IMGS05 = row["RVBS03"].ToString();
                    imgsNewModel.IMGS06 = row["RVBS04"].ToString();
                    imgsNewModel.IMGS08 = Convert.ToDecimal(row["RVBS10"].ToString());
                    imgsNewModel.IMGS09 = Convert.ToDateTime(row["RVBS05"].ToString());
                    imgsNewModel.IMGS10 = "2";
                    imgsNewModel.IMGS11 = " ";
                    imgsNewModel.IMGSPLANT = row["RVBSPLANT"].ToString();
                    imgsNewModel.IMGSLEGAL = row["RVBSLEGAL"].ToString();

                    //获取仓库
                    TC_IME_FILE imeModel = TcimeFiles.GetModel(row["RVBS03"].ToString());
                    if (imeModel != null) { imgsNewModel.IMGS02 = imeModel.TC_IME01; }
                    else { break; } //没找到仓库，退出本次循环

                    //获取料件单位
                    IMA_FILE imaModel = ImaFiles.GetModel(row["RVBS021"].ToString());
                    if (imaModel != null) { imgsNewModel.IMGS07 = imaModel.IMA25; } else { break; }//没找到料件，退出本次循环

                    //判断imgs是否存在
                    if (!IsExists(imgsNewModel.IMGS01, imgsNewModel.IMGS02, imgsNewModel.IMGS05, imgsNewModel.IMGS06))
                    {
                        //add
                        AddModel(imgsNewModel);
                    }
                    else
                    {
                        //update
                        UpdateModel(imgsNewModel);
                    }
                }
            }
        }


        /// <summary>
        /// 获取rvbs信息 
        /// </summary>
        /// <param name="rvbs04"></param>
        /// <returns></returns>
        public static DataTable GetRvbsInfoGroup(string rvbs04)
        {
            DataTable dt = null;
            try
            {
                //string cmdStr = "select RVBS021,RVBS03,RVBS04,sum(rvbs10 * rvbs09) as RVBS10,min(RVBS05) RVBS05,RVBSPLANT,RVBSLEGAL from rvbs_file where rvbs04 = '" + rvbs04 + "' group by RVBS021,RVBS03,RVBS04,RVBSPLANT,RVBSLEGAL";

                //改为过账才计算
                string cmdStr = @"select RVBS021,RVBS03,RVBS04,sum(rvbs10 * rvbs09) as RVBS10,min(RVBS05) RVBS05,RVBSPLANT,RVBSLEGAL from (select  RVBS00 , RVBS01 , RVBS02,  RVBS03 , RVBS04 , RVBS05 , RVBS06 , RVBS07 , RVBS08,  RVBS021, RVBS022, RVBS09 ,RVBS06 as RVBS10,  RVBS11 , RVBS12 , RVBS13 , RVBSPLANT, RVBSLEGAL  from rvbs_file where rvbs00 = 'asft620' and rvbs04 = '" + rvbs04 + "' and exists(select * from sfu_file where sfu01 = rvbs_file.rvbs01 and sfupost = 'Y') union all select * from rvbs_file where rvbs00 = 'axmt820' and rvbs04 = '" + rvbs04 + "' and exists(select * from oga_file where oga01 = rvbs_file.rvbs01 and ogapost = 'Y') union all select  RVBS00 , RVBS01 , RVBS02,  RVBS03 , RVBS04 , RVBS05 , RVBS06 , RVBS07 , RVBS08,  RVBS021, RVBS022, RVBS09 ,RVBS06 as RVBS10,  RVBS11 , RVBS12 , RVBS13 , RVBSPLANT, RVBSLEGAL  from rvbs_file where rvbs00 = 'aimt324' and rvbs04 = '" + rvbs04 + "' and exists(select * from imm_file where imm01 = rvbs_file.rvbs01 and imm03 = 'Y') ) rvbs_file group by RVBS021, RVBS03, RVBS04, RVBSPLANT, RVBSLEGAL";


                //sql示例。 只统计  asft620，axmt820，aimt324 (asft620,aimt324 入库 调拨使用 rvbs06 进行统计)
                //select RVBS021, RVBS03, RVBS04, sum(rvbs10 * rvbs09) as RVBS10, min(RVBS05) RVBS05, RVBSPLANT, RVBSLEGAL
                //from
                //(
                //select  RVBS00 , RVBS01 , RVBS02,  RVBS03 , RVBS04 , RVBS05 , RVBS06 , RVBS07 , RVBS08,  RVBS021, RVBS022, RVBS09 ,RVBS06 as RVBS10,  RVBS11 , RVBS12 , RVBS13 , RVBSPLANT, RVBSLEGAL  from rvbs_file
                //where rvbs00 = 'asft620' and rvbs04 = '03212150051904030001'
                //and exists(select * from sfu_file where sfu01 = rvbs_file.rvbs01 and sfupost = 'Y')
                //union all
                //select * from rvbs_file
                //where rvbs00 = 'axmt820' and rvbs04 = '03212150051904030001'
                //and exists(select * from oga_file where oga01 = rvbs_file.rvbs01 and ogapost = 'Y')
                //union all
                //select  RVBS00 , RVBS01 , RVBS02,  RVBS03 , RVBS04 , RVBS05 , RVBS06 , RVBS07 , RVBS08,  RVBS021, RVBS022, RVBS09 ,RVBS06 as RVBS10,  RVBS11 , RVBS12 , RVBS13 , RVBSPLANT, RVBSLEGAL  from rvbs_file
                //where rvbs00 = 'aimt324' and rvbs04 = '03212150051904030001'
                //and exists(select * from imm_file where imm01 = rvbs_file.rvbs01 and imm03 = 'Y')
                //)
                //rvbs_file
                //group by RVBS021, RVBS03, RVBS04, RVBSPLANT, RVBSLEGAL

                dt = OraRDBSHelper.ExecuateSql(cmdStr);
            }
            catch
            { //有错误就算了
            }

            return dt;
        }








    }

}
