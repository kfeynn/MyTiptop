using System;
using System.Collections;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyTiptop.Web.Framework;
using MyTiptop.Web.MallAdmin.Models;
using MyTiptop.Services;
using MyTiptop.Data;
using MyTiptop.Core;

namespace MyTiptop.Web.MallAdmin.Controllers
{
    public class BaseController : BaseMallAdminController
    {

        #region 类型表

        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="PrcsName">用户（查询用）</param>
        /// <param name="pageNumber">当前页码</param>
        /// <param name="pageSize">页尺寸</param>
        /// <returns></returns>
        public ActionResult SortList(string SortName, int pageNumber = 1, int pageSize = 15)
        {
            // 获取参数、分页信息 （Post 传参数传递约定大于配置）
            // 查询所有用户信息
            if (pageSize <= 0)
                //防止除0操作
                pageSize = 1;
            //查询条件
            string filter = String.Empty;
            if (SortName != null && SortName != "")
            {
                filter += " SortName  like '%" + SortName + "%'";
            }
            //返回总页数、总记录数
            int totalPage;
            int totalRecord;
            //分页查询
            List<Base_Sort> List = new RDBSHelper().ExecutePaging<Base_Sort>("Base_Sort", "*", "  id asc", filter, pageSize, pageNumber, out totalPage, out totalRecord);
            //页脚Model
            PageModel pageModel = new PageModel(pageSize, pageNumber, totalRecord);
            //初始化ViewModel
            SortListViewModel model = new SortListViewModel()
            {
                PageModel = pageModel,
                List = List,
                SortName = SortName
            };
            //记录本次访问地址， 用于返回上一层。
            MallUtils.SetAdminRefererCookie(string.Format("{0}?pageNumber={1}&pageSize={2}&SortName={3}",
                                  Url.Action("SortList"), pageModel.PageNumber, pageModel.PageSize,
                                  SortName));
            //返回View
            return View(model);
        }

        /// <summary>
        /// 编辑检查模式
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SortEdit(int sid)
        {
            //准备编辑数据
            Base_Sort model = BaseSorts.GetModel(sid);
            if (model == null)
            {
                return PromptView("不存在");
            }
            SortEditViewModel ViewModel = new SortEditViewModel()
            {
                Single = model
            };
            //获取返回上一页链接信息 
            ViewBag.referer = MallUtils.GetMallAdminRefererCookie();
            return View(ViewModel);
        }

        /// <summary>
        /// 编辑检查模式（确认修改）
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SortEdit(SortEditViewModel Model, int sid = -1)
        {
            Base_Sort editmodel = BaseSorts.GetModel(sid);
            if (editmodel == null)
            {
                return PromptView("不存在");
            }
            //模型验证
            if (ModelState.IsValid)
            {
                BaseSorts.UpdateModel(Model.Single, sid);
                //获取返回上一页链接信息，比 history.go(-1) 多了刷新功能
                ViewBag.referer = MallUtils.GetMallAdminRefererCookie();

                return PromptView("修改成功!");
            }
            return View(Model);
            //return PromptView("模型验证出错！");
        }

        /// <summary>
        /// 添加编辑检查
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SortAdd()
        {
            SortAddViewModel model = new SortAddViewModel()
            {
                AddFlag = false,
                Message = "",
                Single = new  Base_Sort()
            };
            //返回上一级目录
            ViewBag.referer = MallUtils.GetMallAdminRefererCookie();
            return View(model);
        }

        /// <summary>
        /// 添加编辑检查
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SortAdd(SortAddViewModel model)
        {
            if (BaseSorts.IsExists(model.Single.SortName))
            {
                return PromptView("已经存在！");
            }
            //获取上一级目录
            string referer = MallUtils.GetMallAdminRefererCookie();
            ViewBag.referer = referer;
            //启用模型验证 eg: @Html.ValidationMessageFor 
            if (ModelState.IsValid)
            {
                //添加
                BaseSorts.AddModel(model.Single);
                if (model.AddFlag)
                {
                    model.Message = "添加成功！";
                    //连续添加
                    return View(model);
                }
                //整理跳转链接，到上一级目录    malladmin/user
                //string subreferer = GetAdminRefererStr("shoplist", ViewBag.referer);
                return Redirect(referer);
            }
            return View(model);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public ActionResult Sortdelete(int sid = -1)
        {
            if (!BaseSorts.IsExists(sid))
            {
                return PromptView("不存在！");
            }
            try
            {
                BaseSorts.DeleteModel(sid);
            }
            catch (Exception Ex)
            {
                return PromptView("删除错误：" + Ex.Message);
            }
            return PromptView("已经删除！");
        }

        #endregion


        #region 部门表

        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="PrcsName">用户（查询用）</param>
        /// <param name="pageNumber">当前页码</param>
        /// <param name="pageSize">页尺寸</param>
        /// <returns></returns>
        public ActionResult DeptList(string DeptName, int pageNumber = 1, int pageSize = 15)
        {
            // 获取参数、分页信息 （Post 传参数传递约定大于配置）
            // 查询所有用户信息
            if (pageSize <= 0)
                //防止除0操作
                pageSize = 1;
            //查询条件
            string filter = String.Empty;
            if (DeptName != null && DeptName != "")
            {
                filter += " DeptName  like '%" + DeptName + "%'";
            }
            //返回总页数、总记录数
            int totalPage;
            int totalRecord;
            //分页查询
            List<Base_Dept> List = new RDBSHelper().ExecutePaging<Base_Dept>("Base_Dept", "*", "  id asc", filter, pageSize, pageNumber, out totalPage, out totalRecord);
            //页脚Model
            PageModel pageModel = new PageModel(pageSize, pageNumber, totalRecord);
            //初始化ViewModel
            DeptListViewModel model = new DeptListViewModel()
            {
                PageModel = pageModel,
                List = List,
                DeptName = DeptName
            };
            //记录本次访问地址， 用于返回上一层。
            MallUtils.SetAdminRefererCookie(string.Format("{0}?pageNumber={1}&pageSize={2}&DeptName={3}",
                                  Url.Action("DeptList"), pageModel.PageNumber, pageModel.PageSize,
                                  DeptName));
            //返回View
            return View(model);
        }

        /// <summary>
        /// 编辑检查模式
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DeptEdit(int sid)
        {
            //准备编辑数据
            Base_Dept model = BaseDepts.GetModel(sid);
            if (model == null)
            {
                return PromptView("不存在");
            }
            DeptEditViewModel ViewModel = new DeptEditViewModel()
            {
                Single = model
            };
            //获取返回上一页链接信息 
            ViewBag.referer = MallUtils.GetMallAdminRefererCookie();
            return View(ViewModel);
        }

        /// <summary>
        /// 编辑检查模式（确认修改）
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeptEdit(DeptEditViewModel Model, int sid = -1)
        {
            Base_Dept editmodel = BaseDepts.GetModel(sid);
            if (editmodel == null)
            {
                return PromptView("不存在");
            }
            //模型验证
            if (ModelState.IsValid)
            {
                BaseDepts.UpdateModel(Model.Single, sid);
                //获取返回上一页链接信息，比 history.go(-1) 多了刷新功能
                ViewBag.referer = MallUtils.GetMallAdminRefererCookie();

                return PromptView("修改成功!");
            }
            return View(Model);
            //return PromptView("模型验证出错！");
        }

        /// <summary>
        /// 添加编辑检查
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DeptAdd()
        {
            DeptAddViewModel model = new DeptAddViewModel()
            {
                AddFlag = false,
                Message = "",
                Single = new Base_Dept()
            };
            //返回上一级目录
            ViewBag.referer = MallUtils.GetMallAdminRefererCookie();
            return View(model);
        }

        /// <summary>
        /// 添加编辑检查
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeptAdd(DeptAddViewModel model)
        {
            if (BaseDepts.IsExists(model.Single.DeptName))
            {
                return PromptView("已经存在！");
            }
            //获取上一级目录
            string referer = MallUtils.GetMallAdminRefererCookie();
            ViewBag.referer = referer;
            //启用模型验证 eg: @Html.ValidationMessageFor 
            if (ModelState.IsValid)
            {
                //添加
                BaseDepts.AddModel(model.Single);
                if (model.AddFlag)
                {
                    model.Message = "添加成功！";
                    //连续添加
                    return View(model);
                }
                //整理跳转链接，到上一级目录    malladmin/user
                //string subreferer = GetAdminRefererStr("shoplist", ViewBag.referer);
                return Redirect(referer);
            }
            return View(model);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public ActionResult Deptdelete(int sid = -1)
        {
            if (!BaseDepts.IsExists(sid))
            {
                return PromptView("不存在！");
            }
            try
            {
                BaseDepts.DeleteModel(sid);
            }
            catch (Exception Ex)
            {
                return PromptView("删除错误：" + Ex.Message);
            }
            return PromptView("已经删除！");
        }

        #endregion


        #region 状态表

        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="PrcsName">用户（查询用）</param>
        /// <param name="pageNumber">当前页码</param>
        /// <param name="pageSize">页尺寸</param>
        /// <returns></returns>
        public ActionResult StatusList(string StatusName, int pageNumber = 1, int pageSize = 15)
        {
            // 获取参数、分页信息 （Post 传参数传递约定大于配置）
            // 查询所有用户信息
            if (pageSize <= 0)
                //防止除0操作
                pageSize = 1;
            //查询条件
            string filter = String.Empty;
            if (StatusName != null && StatusName != "")
            {
                filter += " StatusName  like '%" + StatusName + "%'";
            }
            //返回总页数、总记录数
            int totalPage;
            int totalRecord;
            //分页查询
            List<Base_Status> List = new RDBSHelper().ExecutePaging<Base_Status>("Base_Status", "*", "  id asc", filter, pageSize, pageNumber, out totalPage, out totalRecord);
            //页脚Model
            PageModel pageModel = new PageModel(pageSize, pageNumber, totalRecord);
            //初始化ViewModel
            StatusListViewModel model = new StatusListViewModel()
            {
                PageModel = pageModel,
                List = List,
                StatusName = StatusName
            };
            //记录本次访问地址， 用于返回上一层。
            MallUtils.SetAdminRefererCookie(string.Format("{0}?pageNumber={1}&pageSize={2}&StatusName={3}",
                                  Url.Action("StatusList"), pageModel.PageNumber, pageModel.PageSize,
                                  StatusName));
            //返回View
            return View(model);
        }

        /// <summary>
        /// 编辑检查模式
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult StatusEdit(int sid)
        {
            //准备编辑数据
            Base_Status model = BaseStatuss.GetModel(sid);
            if (model == null)
            {
                return PromptView("不存在");
            }
            StatusEditViewModel ViewModel = new StatusEditViewModel()
            {
                Single = model
            };
            //获取返回上一页链接信息 
            ViewBag.referer = MallUtils.GetMallAdminRefererCookie();
            return View(ViewModel);
        }

        /// <summary>
        /// 编辑检查模式（确认修改）
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult StatusEdit(StatusEditViewModel Model, int sid = -1)
        {
            Base_Status editmodel = BaseStatuss.GetModel(sid);
            if (editmodel == null)
            {
                return PromptView("不存在");
            }
            //模型验证
            if (ModelState.IsValid)
            {
                BaseStatuss.UpdateModel(Model.Single, sid);
                //获取返回上一页链接信息，比 history.go(-1) 多了刷新功能
                ViewBag.referer = MallUtils.GetMallAdminRefererCookie();

                return PromptView("修改成功!");
            }
            return View(Model);
            //return PromptView("模型验证出错！");
        }

        /// <summary>
        /// 添加编辑检查
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult StatusAdd()
        {
            StatusAddViewModel model = new StatusAddViewModel()
            {
                AddFlag = false,
                Message = "",
                Single = new  Base_Status()
            };
            //返回上一级目录
            ViewBag.referer = MallUtils.GetMallAdminRefererCookie();
            return View(model);
        }

        /// <summary>
        /// 添加编辑检查
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult StatusAdd(StatusAddViewModel model)
        {
            if (BaseStatuss.IsExists(model.Single.StatusName))
            {
                return PromptView("已经存在！");
            }
            //获取上一级目录
            string referer = MallUtils.GetMallAdminRefererCookie();
            ViewBag.referer = referer;
            //启用模型验证 eg: @Html.ValidationMessageFor 
            if (ModelState.IsValid)
            {
                //添加
                BaseStatuss.AddModel(model.Single);
                if (model.AddFlag)
                {
                    model.Message = "添加成功！";
                    //连续添加
                    return View(model);
                }
                //整理跳转链接，到上一级目录    malladmin/user
                //string subreferer = GetAdminRefererStr("shoplist", ViewBag.referer);
                return Redirect(referer);
            }
            return View(model);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public ActionResult Statusdelete(int sid = -1)
        {
            if (!BaseStatuss.IsExists(sid))
            {
                return PromptView("不存在！");
            }
            try
            {
                BaseStatuss.DeleteModel(sid);
            }
            catch (Exception Ex)
            {
                return PromptView("删除错误：" + Ex.Message);
            }
            return PromptView("已经删除！");
        }

        #endregion

    }
}