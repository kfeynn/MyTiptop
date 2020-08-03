using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MyTiptop.SupplierData
{
    public partial class Query
    {

        /// <summary>
        /// 新开线程，后台运行
        /// </summary>
        public static void ThreadMethod()
        {
            try
            {
                //加载邮件主体
                ThreadStart myThreadDelegate = new ThreadStart(DoWork);
                Thread myThread = null;
                myThread = new Thread(myThreadDelegate);
                myThread.Start();
            }
            catch
            {
                //有错不管
            }
        }

        private static void DoWork()
        {
            using (DBContext db = new DBContext())
            {
                //随便执行一个查询，使保持映射
                var model = db.PN.Where(u => u.DNNUM == "GDS32-90010818083101").FirstOrDefault();
                
            }
        }
    }
}
