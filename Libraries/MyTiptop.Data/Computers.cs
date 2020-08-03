using System;
using System.Collections;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyTiptop.Core;
using System.Data.SqlClient;


namespace MyTiptop.Data 
{
    /// <summary>
    /// 设备
    /// </summary>
    public partial class Equipments
    {
        /// <summary>
        /// 判断编码是否存在
        /// </summary>
        /// <param name="Ecode"></param>
        /// <returns></returns>
        public static bool IsExist(string ecode)
        {
            bool returnFlag = false;
            using (DBContext dbContext = new DBContext())
            {
                Equipment model = dbContext.Equipment.Where(u => u.Ecode == ecode).FirstOrDefault();
                if (model != null)
                {
                    returnFlag = true;
                }
            }
            return returnFlag;
        }

        /// <summary>
        /// 获取ID
        /// </summary>
        /// <param name="ecode"></param>
        /// <returns></returns>
        public static int GetEquipmentId(string ecode)
        {
            int returnFlag = 0;
            using (DBContext dbContext = new DBContext())
            {
                Equipment model = dbContext.Equipment.Where(u => u.Ecode == ecode).FirstOrDefault();
                if (model != null)
                {
                    returnFlag = model.id;
                }
            }
            return returnFlag;

        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool AddModel(Equipment model)
        {
            bool returnFlag = false;
            using (DBContext dbContext = new DBContext())
            {
                Equipment NewModel = new Equipment()
                {
                    Ecode = model.Ecode,
                    DeptID = model.DeptID,
                    Brand = model.Brand,
                    InputUserID = model.InputUserID,
                    IP = model.IP,
                    Place = model.Place,
                    Remark = model.Remark,
                    SortID = model.SortID,
                    StatusID = model.StatusID,
                    UpdateTime = model.UpdateTime,
                    UserName = model.UserName,
                    Version = model.Version
                };
                //增加
                dbContext.Equipment.Add(NewModel);

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
            using (DBContext dbContext = new DBContext())
            {
                Equipment model = dbContext.Equipment.Where(u => u.id == sid).FirstOrDefault();
                if (model != null)
                {
                    dbContext.Equipment.Remove(model);

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
        public static void UpdateModel(Equipment model, int sid)
        {
            using (DBContext dbContext = new DBContext())
            {
                Equipment editmodel = dbContext.Equipment.Where(u => u.id == sid).FirstOrDefault();
                if (editmodel == null)
                {
                    return;//空
                }
                editmodel.Ecode = model.Ecode;
                editmodel.DeptID = model.DeptID;
                editmodel.Brand = model.Brand;
                editmodel.Base_Status = model.Base_Status;
                editmodel.InputUserID = model.InputUserID;
                editmodel.IP = model.IP;
                editmodel.Place = model.Place;
                editmodel.Remark = model.Remark;
                editmodel.SortID = model.SortID;
                editmodel.StatusID = model.StatusID;
                editmodel.UpdateTime = model.UpdateTime;
                editmodel.UserName = model.UserName;
                editmodel.Version = model.Version;
                //提交修改
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static List<Equipment> GetList(string filter)
        {
            using (DBContext db = new DBContext())
            {
                string cmdStr = "select * from  Equipment where 1=1 and " + filter;

                //执行存储过程.  返回类型用视图实体类 。
                var list = db.Database.SqlQuery<Equipment>(cmdStr).ToList();

                return list;
            }
        }

    }


    /// <summary>
    /// 类型
    /// </summary>
    public partial class BaseSorts
    {
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="PqName"></param>
        /// <returns></returns>
        public static bool IsExists(string sortName)
        {
            using (DBContext dbContext = new DBContext())
            {
                //精确匹配名称
                var model = dbContext.Base_Sort.Where(u => u.SortName == sortName).FirstOrDefault();
                if (model != null)
                    return true;
                else
                    return false;
            }
        }
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static bool IsExists(int sid)
        {
            using (DBContext dbContext = new DBContext())
            {
                //精确匹配Id
                var model = dbContext.Base_Sort.Where(u => u.id == sid).FirstOrDefault();
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
        public static Base_Sort GetModel(int sid)
        {
            using (DBContext dbContext = new DBContext())
            {
                var model = dbContext.Base_Sort.Where(u => u.id == sid).FirstOrDefault();
                return model;
            }
        }
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static Base_Sort GetModel(string sortName)
        {
            using (DBContext dbContext = new DBContext())
            {
                var model = dbContext.Base_Sort.Where(u => u.SortName == sortName).FirstOrDefault();
                return model;
            }
        }
        /// <summary>
        /// 获取ID
        /// </summary>
        /// <param name="sortName"></param>
        /// <returns></returns>
        public static int? GetSortId(string sortName)
        {
            using (DBContext dbContext = new DBContext())
            {
                var model = dbContext.Base_Sort.Where(u => u.SortName == sortName).FirstOrDefault();
                if (model != null)
                    return model.id;
                else
                    return null;
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool AddModel(Base_Sort model)
        {
            bool returnFlag = false;
            using (DBContext dbContext = new DBContext())
            {
                Base_Sort NewModel = new Base_Sort()
                {
                    SortName = model.SortName,
                    Remark = model.Remark
                };
                //增加
                dbContext.Base_Sort.Add(NewModel);

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
            using (DBContext dbContext = new DBContext())
            {
                Base_Sort model = dbContext.Base_Sort.Where(u => u.id == sid).FirstOrDefault();
                if (model != null)
                {
                    dbContext.Base_Sort.Remove(model);

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
        public static void UpdateModel(Base_Sort model, int sid)
        {
            using (DBContext dbContext = new DBContext())
            {
                Base_Sort editmodel = dbContext.Base_Sort.Where(u => u.id == sid).FirstOrDefault();
                if (editmodel == null)
                {
                    return;//空
                }
                editmodel.SortName = model.SortName;
                editmodel.Remark = model.Remark;
                
                //提交修改
                dbContext.SaveChanges();
            }
        }


        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public static List<Base_Sort> GetList()
        {
            using (DBContext dbContext = new DBContext())
            {
                return dbContext.Base_Sort.ToList();
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static string GetSortName(int? sid)
        {
            string returnFlag = "";
            using (DBContext dbContext = new DBContext())
            {
                //if (sid == null)
                //{
                //    return "";
                //}
                Base_Sort model = dbContext.Base_Sort.Where(u => u.id == sid).FirstOrDefault();
                if (model != null)
                {
                    returnFlag = model.SortName;
                }
            }
            return returnFlag;
        }

    }

    /// <summary>
    /// 部门
    /// </summary>
    public partial class BaseDepts
    {

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="PqName"></param>
        /// <returns></returns>
        public static bool IsExists(string deptName)
        {
            using (DBContext dbContext = new DBContext())
            {
                //精确匹配名称
                var model = dbContext.Base_Dept.Where(u => u.DeptName == deptName).FirstOrDefault();
                if (model != null)
                    return true;
                else
                    return false;
            }
        }
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static bool IsExists(int sid)
        {
            using (DBContext dbContext = new DBContext())
            {
                //精确匹配Id
                var model = dbContext.Base_Dept.Where(u => u.id == sid).FirstOrDefault();
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
        public static Base_Dept GetModel(int sid)
        {
            using (DBContext dbContext = new DBContext())
            {
                var model = dbContext.Base_Dept.Where(u => u.id == sid).FirstOrDefault();
                return model;
            }
        }
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static Base_Dept GetModel(string deptName)
        {
            using (DBContext dbContext = new DBContext())
            {
                var model = dbContext.Base_Dept.Where(u => u.DeptName == deptName).FirstOrDefault();
                return model;
            }
        }

        /// <summary>
        /// 获取ID
        /// </summary>
        /// <param name="sortName"></param>
        /// <returns></returns>
        public static int? GetDeptId(string deptName)
        {
            using (DBContext dbContext = new DBContext())
            {
                var model = dbContext.Base_Dept.Where(u => u.DeptName == deptName).FirstOrDefault();
                if (model != null)
                    return model.id;
                else
                    return null;
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool AddModel(Base_Dept model)
        {
            bool returnFlag = false;
            using (DBContext dbContext = new DBContext())
            {
                Base_Dept NewModel = new Base_Dept()
                {
                    DeptName = model.DeptName,
                    Remark = model.Remark
                };
                //增加
                dbContext.Base_Dept.Add(NewModel);

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
            using (DBContext dbContext = new DBContext())
            {
                Base_Dept model = dbContext.Base_Dept.Where(u => u.id == sid).FirstOrDefault();
                if (model != null)
                {
                    dbContext.Base_Dept.Remove(model);

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
        public static void UpdateModel(Base_Dept model, int sid)
        {
            using (DBContext dbContext = new DBContext())
            {
                Base_Dept editmodel = dbContext.Base_Dept.Where(u => u.id == sid).FirstOrDefault();
                if (editmodel == null)
                {
                    return;//空
                }
                editmodel.DeptName = model.DeptName;
                editmodel.Remark = model.Remark;

                //提交修改
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public static List<Base_Dept> GetList()
        {
            using (DBContext dbContext = new DBContext())
            {
                return dbContext.Base_Dept.ToList();
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static string GetDeptName(int? sid)
        {
            string returnFlag = "";
            using (DBContext dbContext = new DBContext())
            {
                //if (sid == null)
                //{
                //    return "";
                //}
                Base_Dept model = dbContext.Base_Dept.Where(u => u.id == sid).FirstOrDefault();
                if (model != null)
                {
                    returnFlag = model.DeptName;
                }
            }
            return returnFlag;
        }


    }


    /// <summary>
    /// 部门
    /// </summary>
    public partial class BaseStatuss
    {

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="PqName"></param>
        /// <returns></returns>
        public static bool IsExists(string statusName)
        {
            using (DBContext dbContext = new DBContext())
            {
                //精确匹配名称
                var model = dbContext.Base_Status.Where(u => u.StatusName == statusName).FirstOrDefault();
                if (model != null)
                    return true;
                else
                    return false;
            }
        }
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static bool IsExists(int sid)
        {
            using (DBContext dbContext = new DBContext())
            {
                //精确匹配Id
                var model = dbContext.Base_Status.Where(u => u.id == sid).FirstOrDefault();
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
        public static Base_Status GetModel(int sid)
        {
            using (DBContext dbContext = new DBContext())
            {
                var model = dbContext.Base_Status.Where(u => u.id == sid).FirstOrDefault();
                return model;
            }
        }
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static Base_Status GetModel(string statusName)
        {
            using (DBContext dbContext = new DBContext())
            {
                var model = dbContext.Base_Status.Where(u => u.StatusName == statusName).FirstOrDefault();
                return model;
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool AddModel(Base_Status model)
        {
            bool returnFlag = false;
            using (DBContext dbContext = new DBContext())
            {
                Base_Status NewModel = new Base_Status()
                {
                    StatusName = model.StatusName,
                    Remark = model.Remark
                };
                //增加
                dbContext.Base_Status.Add(NewModel);
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
            using (DBContext dbContext = new DBContext())
            {
                Base_Status model = dbContext.Base_Status.Where(u => u.id == sid).FirstOrDefault();
                if (model != null)
                {
                    dbContext.Base_Status.Remove(model);

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
        public static void UpdateModel(Base_Status model, int sid)
        {
            using (DBContext dbContext = new DBContext())
            {
                Base_Status editmodel = dbContext.Base_Status.Where(u => u.id == sid).FirstOrDefault();
                if (editmodel == null)
                {
                    return;//空
                }
                editmodel.StatusName = model.StatusName;
                editmodel.Remark = model.Remark;

                //提交修改
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// 获取状态列表。JSon 格式
        /// </summary>
        /// <param name="top">最多取多少位</param>
        /// <returns></returns>
        public static string GetStatusList(int top)
        {
            using (DBContext dbContext = new DBContext())
            {
                var List = from m in dbContext.Base_Status select m;
                string ret = "";
                if (List != null)
                {
                    int i = 0;
                    ret = "[";
                    foreach (Base_Status s in List)
                    {
                        if (i < top)
                        {
                            if (ret.Length > 1) ret += ",";
                            ret += string.Format("{{\"id\":\"{0}\",\"text\":\"{1}\"}}",s.id, s.StatusName.TrimEnd());
                            //计数 
                            i++;
                        }
                    }
                    ret += "]";
                }
                
                /*
                    [
                        { "id": "usa", "text": "美国" },
                        { "id": "cn", "text": "中国" },
                        { "id": "jp", "text": "日本" }
                    ]
                */
                return ret;
            }
        }

        /// <summary>
        /// 获取状态列表
        /// </summary>
        /// <returns></returns>
        public static List<Base_Status> GetList()
        {
            using (DBContext dbContext = new DBContext())
            {
                return dbContext.Base_Status.ToList();
             }

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static string GetStatusName(int? sid)
        {
            string returnFlag = "";
            using (DBContext dbContext = new DBContext())
            {
                //if (sid == null)
                //{
                //    return "";
                //}
                Base_Status model = dbContext.Base_Status.Where(u => u.id == sid).FirstOrDefault();
                if (model != null)
                {
                    returnFlag = model.StatusName;
                }
            }
            return returnFlag;
        }

        /// <summary>
        /// 获取ID
        /// </summary>
        /// <param name="sortName"></param>
        /// <returns></returns>
        public static int GetStatusId(string statusName)
        {
            using (DBContext dbContext = new DBContext())
            {
                var model = dbContext.Base_Status.Where(u => u.StatusName == statusName).FirstOrDefault();
                if (model != null)
                    return model.id;
                else
                    return 0;
            }
        }






    }


}
