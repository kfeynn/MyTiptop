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
    public partial class QcCheck
    {
        public static void addQcCheckHead(TC_QCY_FILE model,string qcx06)
        {
            //开启事务管理1.添加记录。2.添加 tc_qcz_file 。
            using (TransactionScope sc = new TransactionScope())
            {
                try
                {
                    //1.添加 
                    Tcqcys.AddModel(model);
                    //2.添加 tc_qcz_file

                    List<TC_QCX_FILE> qcxlist = Tcqcxs.GetList(qcx06, 0);

                    foreach (TC_QCX_FILE qcx in qcxlist)
                    {
                        TC_QCZ_FILE qcz = new TC_QCZ_FILE();

                        qcz.TC_QCZ01 = model.TC_QCY01;
                        qcz.TC_QCZ02 = qcx.TC_QCX02;
                        qcz.TC_QCZ05 = qcx.TC_QCX01;
                        qcz.TC_QCZ07 = qcx.TC_QCX07;
                        qcz.TC_QCZ06 = qcx.TC_QCX03;
                        qcz.TC_QCZ09 = qcx.TC_QCX04;

                        Tcqczs.AddModel(qcz);
                    }
                    //事务提交
                    sc.Complete();
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
            }
        }

        public static void addQcOutCheckHead(TC_QCYY_FILE model, string qcx06)
        {
            //开启事务管理1.添加记录。2.添加 tc_qcz_file 。
            using (TransactionScope sc = new TransactionScope())
            {
                try
                {
                    //1.添加 
                    Tcqcyys.AddModel(model);
                    //2.添加 tc_qcz_file

                    List<TC_QCXX_FILE> qcxlist = Tcqcxxs.GetList(qcx06, 1);

                    foreach (TC_QCXX_FILE qcx in qcxlist)
                    {
                        TC_QCZZ_FILE qcz = new TC_QCZZ_FILE();

                        qcz.TC_QCZZ01 = model.TC_QCYY01;
                        qcz.TC_QCZZ02 = qcx.TC_QCXX02;
                        qcz.TC_QCZZ06 = qcx.TC_QCXX01;
                        qcz.TC_QCZZ07 = qcx.TC_QCXX03;
                        qcz.TC_QCZZ08 = qcx.TC_QCXX07;
                        qcz.TC_QCZZ10 = qcx.TC_QCXX04;
                        qcz.TC_QCZZ11 = qcx.TC_QCXX08;
                        qcz.TC_QCZZ09 = qcx.TC_QCXX09;

                        Tcqczzs.AddModel(qcz);
                    }
                    //事务提交
                    sc.Complete();
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
            }
        }


        public static void reloadqcz(string qcz01)
        {
            //开启事务管理1.添加记录。2.添加 tc_qcz_file 。
            using (TransactionScope sc = new TransactionScope())
            {
                try
                {
                    //1.删除记录
                    Tcqczs.DeleteModel(qcz01);

                    //2.添加记录
                    string qcx06 = qcz01.Substring(0, qcz01.IndexOf('-'));
                    List<TC_QCX_FILE> qcxlist = Tcqcxs.GetList(qcx06, 0);

                    foreach (TC_QCX_FILE qcx in qcxlist)
                    {
                        TC_QCZ_FILE qcz = new TC_QCZ_FILE();

                        qcz.TC_QCZ01 = qcz01;
                        qcz.TC_QCZ02 = qcx.TC_QCX02;
                        qcz.TC_QCZ05 = qcx.TC_QCX01;
                        qcz.TC_QCZ06 = qcx.TC_QCX03;
                        qcz.TC_QCZ07 = qcx.TC_QCX07;
                        qcz.TC_QCZ09 = qcx.TC_QCX04;

                        Tcqczs.AddModel(qcz);
                    }
                    //事务提交
                    sc.Complete();
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
            }
        }

        public static bool deleteQcy(string sid)
        {
            bool returnValue = false;
            //开启事务管理1.添加记录。2.添加 tc_qcz_file 。
            using (TransactionScope sc = new TransactionScope())
            {
                try
                {
                    //1.删除记录
                    Tcqcys.DeleteModel(sid);
                    //2.删除子记录
                    Tcqczs.DeleteModel(sid);
                    //事务提交
                    sc.Complete();
                    returnValue = true;
                }
                catch (Exception )
                {
                    returnValue = false;
                }
            }
            return returnValue;
        }

        /// <summary>
        /// 获取编号
        /// </summary>
        /// <param name="prefix">前缀</param>
        /// <returns></returns>
        public static string getPN(string prefix)
        {
            //prefix = "187132";
            String y = DateTime.Now.Year.ToString().Substring(2);
            String m = DateTime.Now.Month.ToString();
            if (m.Length < 2){m = "0" + m;}
            prefix = prefix+"-" + y + m;

            int maxNumber = getQcCheckHeadMaxNumber(prefix);
            maxNumber = maxNumber + 1;

            string flowNumber = maxNumber.ToString();
            int o = 3 - flowNumber.Length;
            //流水号位数为3。不足补0
            for (int i = 0; i < o; i++)
            {
                flowNumber = "0" + flowNumber;
            }

            string returnValue = prefix + flowNumber; 

            return returnValue; 
        }

        /// <summary>
        /// 获取编号 出货单
        /// </summary>
        /// <param name="prefix">前缀</param>
        /// <returns></returns>
        public static string getPNOut(string prefix)
        {
            //prefix = "187132";
            String y = DateTime.Now.Year.ToString().Substring(2);
            String m = DateTime.Now.Month.ToString();
            if (m.Length < 2) { m = "0" + m; }
            prefix = prefix + "-" + y + m;

            int maxNumber = getQcOutCheckHeadMaxNumber(prefix);
            maxNumber = maxNumber + 1;

            string flowNumber = maxNumber.ToString();
            int o = 3 - flowNumber.Length;
            //流水号位数为3。不足补0
            for (int i = 0; i < o; i++)
            {
                flowNumber = "0" + flowNumber;
            }

            string returnValue = prefix + flowNumber;

            return returnValue;
        }

        /// <summary>
        /// 获取最大流水号
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static int getQcCheckHeadMaxNumber(string prefix)
        {
            int returnNumber = 0;

            TC_QCY_FILE model = Tcqcys.GetMaxModel(prefix);

            if (model != null)
            {
                //取最后三位流水号
                string flow =  model.TC_QCY01.Substring(model.TC_QCY01.Length-3);

                returnNumber = TypeHelper.StringToInt(flow) ;
            }

            return returnNumber;
        }


        /// <summary>
        /// 获取最大流水号 出货单
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static int getQcOutCheckHeadMaxNumber(string prefix)
        {
            int returnNumber = 0;

            TC_QCYY_FILE model = Tcqcyys.GetMaxModel(prefix);

            if (model != null)
            {
                //取最后三位流水号
                string flow = model.TC_QCYY01.Substring(model.TC_QCYY01.Length - 3);

                returnNumber = TypeHelper.StringToInt(flow);
            }

            return returnNumber;
        }






    }
}
