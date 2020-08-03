using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using MyTiptop.Web.Framework;
using MyTiptop.Web.MallAdmin.Models;
using MyTiptop.Data;
using MyTiptop.Services;
using MyTiptop.Core;
//using MyTiptop.OraCore;
//using Oracle.ManagedDataAccess;
//using Oracle.ManagedDataAccess.EntityFramework;

namespace MyTiptop.Web.MallAdmin.Controllers
{
    /// <summary>
    /// 角色管理Controller
    /// </summary>
    public class RoleController : BaseMallAdminController
    {
        #region Role Part
        public ActionResult List(string roleName, int pageNumber = 1, int pageSize = 15)
        {
            //try
            //{
            //    OraDBContext dbContext = new OraDBContext();
            //    var modelList = dbContext.TC_BRB_FILE.ToList().FirstOrDefault();
            //    //test
            //    Class1.UserIsExists("", "");
            //}
            //catch (Exception Ex)
            //{
            //    int aa = 0;
            //}

            if (pageSize <= 0)
                //防止除0操作
                pageSize = 1;
            //查询条件
            string filter = String.Empty;
            if (roleName != null && roleName != "")
            {
                filter += " roleName  like '%" + roleName + "%'";
            }
            //返回总页数、总记录数
            int totalPage;
            int totalRecord;
            //分页查询
            List<xpGrid_Role> RoleList = new RDBSHelper().ExecutePaging<xpGrid_Role>("xpGrid_Role", "*", "RoleId", filter, pageSize, pageNumber, out totalPage, out totalRecord);
            //页脚Model
            PageModel pageModel = new PageModel(pageSize, pageNumber, totalRecord);
            //初始化ViewModel
            RoleListViewModel model = new RoleListViewModel()
            {
                PageModel = pageModel,
                Roles = RoleList,
                RoleName = roleName
            };
            //记录本次访问地址， 用于返回上一层。
            MallUtils.SetAdminRefererCookie(string.Format("{0}?pageNumber={1}&pageSize={2}&roleName={3}",
                                  Url.Action("list"), pageModel.PageNumber, pageModel.PageSize,
                                  roleName));
            //返回View
            return View(model);
        }

        [HttpGet]
        public ActionResult Add()
        {
            RoleAddViewModel model = new RoleAddViewModel();
            //返回上一级目录
            ViewBag.referer = MallUtils.GetMallAdminRefererCookie();
            return View(model);
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(RoleAddViewModel model)
        {
            string roleName = model.Role.RoleName;
            xpGrid_Role roleInfo =  Roles.GetRoleByRoleName(roleName);
            if (roleInfo != null)
            {
                return PromptView("角色已经存在");
            }
            ViewBag.referer = MallUtils.GetMallAdminRefererCookie();
            //启用模型验证 eg: Html.ValidationMessageFor 
            if (ModelState.IsValid)
            {
                Roles.AddRole(model.Role.RoleName, model.Role.RoleDes);
                if (model.AddFlag)
                {
                    //连续添加
                    model.Message = "添加成功！";
                    return View(model);
                }
                //整理跳转链接，到上一级目录    malladmin/user
                string subreferer = GetAdminRefererStr(ViewBag.referer);
                return Redirect(subreferer);
            }
            return View(model);
        }

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="rid"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit( int rid=-1)
        {
            xpGrid_Role roleInfo = Roles.GetRoleById(rid);
            if (roleInfo == null)
            {
                return PromptView("用户不存在");
            }
            RoleEditViewModel model = new RoleEditViewModel()
            {
                 Role  = roleInfo
            };
            //获取返回上一页链接信息，比 history.go(-1) 多了刷新功能
            ViewBag.referer = MallUtils.GetMallAdminRefererCookie();
            return View(model);

        }

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="rid"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(RoleEditViewModel model, int rid = -1)
        {
            xpGrid_Role roleInfo = Roles.GetRoleById(rid);
            if (roleInfo == null)
            {
                return PromptView("用户不存在");
            }
            if (ModelState.IsValid)
            {
                Roles.UpdateRole(rid, model.Role);
                //获取返回上一页链接信息，比 history.go(-1) 多了刷新功能
                ViewBag.referer = MallUtils.GetMallAdminRefererCookie();

                return PromptView("用户修改成功");
            }

            return View(model);

        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="rid">角色ID</param>
        /// <returns></returns>
        public ActionResult delete(int rid = -1)
        {
            xpGrid_Role roleInfo = Roles.GetRoleById(rid);
            if (roleInfo == null)
            {
                return PromptView("角色不存在");
            }
            try
            {
                Roles.DeleteRole(rid);
            }
            catch (Exception Ex)
            {
                return PromptView("删除错误:" + Ex.Message);
            }
            return PromptView("角色已经删除！");
        }

        /// <summary>
        /// 获取上一级访问地址，用于转向Redirect
        /// </summary>
        /// <param name="referer"></param>
        /// <returns></returns>
        public string GetAdminRefererStr(string referer)
        {
            string subreferer = referer.Substring(referer.LastIndexOf("?") + 1);

            subreferer = string.Format("{0}?{1}", Url.Action("list"), subreferer);

            return subreferer;
        }

        #endregion


        #region Func Part New
        public ActionResult FuncListe()
        {
            return View();
        }

        /// <summary>
        /// 查询
        /// </summary>
        public ActionResult MyDataBing(string key, string sortField = "", string sortOrder = "", int pageIndex = 0, int pageSize = 20)
        {
            //分页
            if (pageSize <= 0)
            {
                pageSize = 1;
            }
            //脚标从 1 开始
            pageIndex += 1;

            if (sortField == "")
            {
                sortField = " FuncCode ";
            }
            if (sortOrder == "")
            {
                sortOrder = " asc ";
            }
            sortField = sortField + " " + sortOrder;

            //查询条件
            string filter = String.Empty;

            //filter = " where 1=1 ";

            if (key != null && key != "")
            {
                if (filter.Length > 1)
                {
                    filter += " and ";
                }
                filter += " FuncCode like '%" + key + "%' ";
            }

            //返回总页数、总记录数 
            //int totalPage;
            int totalRecord;
            //分页查询 
            int totalpage;

            var dt = new RDBSHelper().ExecutePaging<xpGrid_Functions>("xpGrid_Functions","*",sortField, filter, pageSize, pageIndex,out totalpage, out totalRecord);
            //页脚Model 

            Hashtable result = new Hashtable();
            result["data"] = dt;
            result["total"] = totalRecord;

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 是否已经存在
        /// </summary>
        /// <param name="mouldId"></param>
        /// <returns></returns>
        public ActionResult IsExistsFuncCodeAjax(string sid = "")
        {
            string status = "0";
            if (Roles.IsExistFunc(sid))
            {
                //存在
                status = "1";
            }
            return AjaxResult("success", status, true);
        }


        /// <summary>
        /// 保存修改
        /// </summary>
        public void SaveChanged(string data)
        {
            String json = Request["data"];
            ArrayList rows = (ArrayList)JSON.Decode(json);
            foreach (Hashtable row in rows)
            {
                //根据记录状态，进行不同的增加、删除、修改操作
                String state = row["_state"] != null ? row["_state"].ToString() : "";
                if (state == "added")
                {
                    //添加
                    addNewRow(row);
                }
                else if (state == "removed" || state == "deleted")
                {
                    //删除
                    string sid = row["FuncCode"].ToString();
                    Roles.DeleteFunc(sid);
                }
                else if (state == "modified")
                {
                    //修改
                    editRow(row);
                }
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="row"></param>
        private void addNewRow(Hashtable row)
        {
            var model = new xpGrid_Functions();

            int? INUll = null;

            model.FuncCode = row["FuncCode"].ToString();
            model.FuncName = row["FuncName"] == null ? null : row["FuncName"].ToString();
            model.FuncUrl = row["FuncUrl"] == null ? null : row["FuncUrl"].ToString();
            model.FuncParent = row["FuncParent"] == null ? null : row["FuncParent"].ToString();
            model.FuncImg = row["FuncImg"] == null ? null : row["FuncImg"].ToString(); 
            model.Enable = 0;
            model.DisplayOrder = row["DisplayOrder"] == null || row["DisplayOrder"].ToString() =="" ? INUll : Convert.ToInt32(row["DisplayOrder"].ToString());

            Roles.AddFunctions(model);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="row"></param>
        private void editRow(Hashtable row)
        {
            string sid = row["FuncCode"].ToString();
            var model = new xpGrid_Functions();

            int? INUll = null;

            model.FuncCode = row["FuncCode"].ToString();
            model.FuncName = row["FuncName"] == null ? null : row["FuncName"].ToString();
            model.FuncUrl = row["FuncUrl"] == null ? null : row["FuncUrl"].ToString();
            model.FuncParent = row["FuncParent"] == null ? null : row["FuncParent"].ToString();
            model.FuncImg = row["FuncImg"] == null ? null : row["FuncImg"].ToString();
            model.Enable = 0;
            model.DisplayOrder = row["DisplayOrder"] == null || row["DisplayOrder"].ToString() == "" ? INUll : Convert.ToInt32(row["DisplayOrder"].ToString());

            Roles.UpdateFunc(sid, model);
        }

        #endregion 



        #region Func Part

        /// <summary>
        /// 系统功能列表
        /// </summary>
        /// <param name="funcCode"></param>
        /// <param name="funcName"></param>
        /// <param name="funcUrl"></param>
        /// <param name="funcParent"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ActionResult FuncList(string funcCode,string funcName,string funcUrl,string funcParent,int pageNumber= 1,int pageSize=15)
        {
            if (pageSize <= 0)
                //防止除0操作
                pageSize = 1;
            //查询条件
            string filter = String.Empty;

            #region 整理查询条件

            if (funcCode!=null && funcCode != "")
            {
                filter += " FuncCode  like '%" + funcCode + "%'";
            }
            if (funcName !=null && funcName != "")
            {
                if (filter.Length > 0)
                {
                    filter += " and ";
                }
                filter += " FuncName  like '%" + funcName + "%'";
            }
            if (funcUrl!=null && funcUrl != "")
            {
                if (filter.Length > 0)
                {
                    filter += " and ";
                }
                filter += " FuncUrl  like '%" + funcUrl + "%'";
            }
            if (funcParent!=null && funcParent != "")
            {
                if (filter.Length > 0)
                {
                    filter += " and ";
                }
                filter += " FuncParent  like '%" + funcParent + "%'";
            }
            #endregion 
 
            //返回总页数、总记录数
            int totalPage;
            int totalRecord;
            //分页查询
            List<xpGrid_Functions> funcList = new RDBSHelper().ExecutePaging<xpGrid_Functions>("xpGrid_Functions", "*", "FuncCode", filter, pageSize, pageNumber, out totalPage, out totalRecord);
            //页脚Model
            PageModel pageModel = new PageModel(pageSize, pageNumber, totalRecord);
            //初始化ViewModel
            FuncListViewModel model = new FuncListViewModel()
            {
                PageModel = pageModel,
                FuncList = funcList,
                FuncCode = funcCode,
                FuncName = funcName,
                FuncParent = funcParent,
                FuncUrl = funcUrl
            };
            //记录本次访问地址， 用于返回上一层。
            MallUtils.SetAdminRefererCookie(string.Format("{0}?pageNumber={1}&pageSize={2}&funcCode={3}&funcName={4}&funcUrl={5}&funcParent={6}",
                                  Url.Action("FuncList"), pageModel.PageNumber, pageModel.PageSize, funcCode, funcName, funcUrl, funcParent));
            //返回View
            return View(model);

        }

        [HttpGet]
        public ActionResult FuncAdd()
        {
            FuncAddViewModel model = new FuncAddViewModel();
            //返回上一级目录
            ViewBag.referer = MallUtils.GetMallAdminRefererCookie();
            return View(model);
        }

        [HttpPost]
        public ActionResult FuncAdd(FuncAddViewModel model)
        {
            if (Roles.IsExistFunc(model.Func.FuncCode))
            {
                return PromptView("已经存在!");
            }
            ViewBag.referer = MallUtils.GetMallAdminRefererCookie();
            //启用模型验证 eg: Html.ValidationMessageFor 
            if (ModelState.IsValid)
            {
                Roles.AddFunctions(model.Func);
                if (model.AddFlag)
                {
                    //连续添加
                    model.Message = "添加成功！";
                    return View(model);
                }
                //整理跳转链接，到上一级目录    malladmin/user
                string subreferer = FuncRefererStr(ViewBag.referer);
                return Redirect(subreferer);
            }
            return View(model);
        }


        [HttpGet]
        public ActionResult FuncEdit(string fid)
        {
            xpGrid_Functions func = Roles.GetFunctionByFuncCode(fid);
            if (func == null)
            {
                return PromptView("纪录不存在");
            }
            FuncEditViewModel model = new FuncEditViewModel()
            {
                 Func = func 
            };
            //获取返回上一页链接信息，比 history.go(-1) 多了刷新功能
            ViewBag.referer = MallUtils.GetMallAdminRefererCookie();
            return View(model);
        }



        public ActionResult FuncEdit(string fid, FuncEditViewModel model)
        {
  

            if (!Roles.IsExistFunc(fid))
            {
                return PromptView("纪录不存在");
            }


            if (ModelState.IsValid)
            {
                Roles.UpdateFunc(fid, model.Func );
                //获取返回上一页链接信息，比 history.go(-1) 多了刷新功能
                ViewBag.referer = MallUtils.GetMallAdminRefererCookie();

                return PromptView("用户修改成功");
            }

            return View(model);

        }


        /// <summary>
        /// 删除功能菜单
        /// </summary>
        /// <param name="fid"></param>
        /// <returns></returns>
        public ActionResult Funcdelete(string  fid )
        {
            if (!Roles.IsExistFunc(fid))
            {
                return PromptView("纪录不存在!");
            }
            try
            {
                Roles.DeleteFunc(fid);
            }
            catch (Exception Ex)
            {
                return PromptView("删除错误:" + Ex.Message);
            }
            return PromptView("功能菜单已经删除！");
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pmIdList"></param>
        public ActionResult Delfunclist(string[] pmIdList)
        {
            try
            {
                Roles.Delfunclist(pmIdList);
            }
            catch (Exception Ex)
            {
                return PromptView("批量删除错误:" + Ex.Message);
            }

            return PromptView("批量删除成功");

        }

        /// <summary>
        /// 获取Func上一级访问地址，用于转向Redirect
        /// </summary>
        /// <param name="referer"></param>
        /// <returns></returns>
        public string FuncRefererStr(string referer)
        {
            string subreferer = referer.Substring(referer.LastIndexOf("?") + 1);
            subreferer = string.Format("{0}?{1}", Url.Action("funclist"), subreferer);
            return subreferer;
        }

        #endregion


        //[HttpGet]
        public ActionResult RoleAuthorization(int rid = -1)
        {
            xpGrid_Role roleInfo = Roles.GetRoleById(rid);
            if (roleInfo == null)
            {
                return PromptView("角色不存在");
            }
            //返回上一级目录
            ViewBag.referer = MallUtils.GetMallAdminRefererCookie();

            DataTable funcList = Roles.RoleAuthorizationFunc(rid);
            
            RoleAuthorizationViewModel Model = new RoleAuthorizationViewModel
            {
                role = roleInfo,
                Message = "",
                FuncList = funcList
            };

            return View(Model);
        }


        public ActionResult AuthorizationChange(string[] pmIdList, int rid = -1)
        {
            xpGrid_Role roleInfo = Roles.GetRoleById(rid);
            if (roleInfo == null)
            {
                return PromptView("角色不存在");
            }
            string Message = "";
            //修改授权
            if (Roles.RoleAuthorizationChange(rid, pmIdList))
                Message = "修改成功";
            //返回上一级目录
            ViewBag.referer = MallUtils.GetMallAdminRefererCookie();

            DataTable funcList = Roles.RoleAuthorizationFunc(rid);

            RoleAuthorizationViewModel Model = new RoleAuthorizationViewModel
            {
                role = roleInfo,
                Message = Message,
                FuncList = funcList
            };
            return View("RoleAuthorization", Model);
        }

    }
}
