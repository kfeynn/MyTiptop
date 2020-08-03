using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;


namespace MyTiptop.SupplierData
{
    /// <summary>
    ///  
    /// </summary>
    public partial class Pns
    {

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name=" "></param>
        /// <returns></returns>
        public static bool IsExists(string sid)
        {
            using (DBContext dbContext = new DBContext())
            {
                //精确匹配名称
                var model = dbContext.PN.Where(u => u.DNNUM == sid).FirstOrDefault();
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
        public static PN GetModel(string sid)
        {
            using (DBContext dbContext = new DBContext())
            {
                var model = dbContext.PN.Where(u => u.DNNUM == sid).FirstOrDefault();
                return model;
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool AddModel(PN model)
        {
            bool returnFlag = false;
            using (DBContext dbContext = new DBContext())
            {
                //增加
                dbContext.PN.Add(model);
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
            using (DBContext dbContext = new DBContext())
            {
                var model = dbContext.PN.Where(u => u.DNNUM == sid).FirstOrDefault();
                if (model != null)
                {
                    dbContext.PN.Remove(model);

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
        //public static void UpdateModel(PN model, int sid)
        //{
        //    using (OraDBContext dbContext = new OraDBContext())
        //    {
        //        var editmodel = dbContext.TC_QCX_FILE.Where(u => u.TC_QCX01 == sid).FirstOrDefault();
        //        if (editmodel == null)
        //        {
        //            return;//空
        //        }
        //        editmodel.TC_QCX02 = model.TC_QCX02;
        //        editmodel.TC_QCX03 = model.TC_QCX03;
        //        editmodel.TC_QCX04 = model.TC_QCX04;
        //        editmodel.TC_QCX05 = model.TC_QCX05;
        //        editmodel.TC_QCX06 = model.TC_QCX06;
        //        editmodel.TC_QCX07 = model.TC_QCX07;

        //        //提交修改 
        //        dbContext.SaveChanges();
        //    }
        //}

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public static List<PN> GetList()
        {
            using (DBContext dbContext = new DBContext())
            {
                return dbContext.PN.OrderByDescending(u =>u.CREATE_TIME ).ToList();  //按时间倒序
            }
        }

        /// <summary>
        /// 获取列表 (条件：状态)
        /// </summary>
        /// <returns></returns>
        public static List<PN> GetList(int status)
        {
            using (DBContext dbContext = new DBContext())
            {
                return dbContext.PN.Where(u => u.STATUS == status).OrderByDescending(u => u.CREATE_TIME).ToList();
            }
        }

    }


}
