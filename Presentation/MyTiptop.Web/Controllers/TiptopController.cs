using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Text;
using System.Collections.Generic;
using System.Data;

using MyTiptop.Web.Models;
using MyTiptop.Web.Framework;
using MyTiptop.Core;
using MyTiptop.Data;
using MyTiptop.Services;
using MyTiptop.OraCore;
using MyTiptop.OraData;
using MyTiptop.OraCore.Data;


namespace MyTiptop.Web.Controllers
{
    
    public class TiptopController : BaseWebController
    {
        //private ApplicationSignInManager _signInManager;
        //private ApplicationUserManager _userManager;

        public TiptopController()
        {
            //后台调用一下 方法
            MyTiptop.OraData.OraQuery.ThreadMethod();

        }

        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="PrcsName">用户（查询用）</param>
        /// <param name="pageNumber">当前页码</param>
        /// <param name="pageSize">页尺寸</param>
        /// <returns></returns>
        public ActionResult bsfp625(string barcode="", string requisition="", string employee="", string packno="", string position="")
        {
            if (barcode == "")
            {
                //初始化ViewModel
                Bsfp625ViewModel model0 = new Bsfp625ViewModel()
                {
                    //requisition = barcode,
                    List = new DataTable(),
                    message = ""
                };
                //返回View
                return View(model0);
            }

            //记录错误提示
            string message = "";
            DataTable dt = new DataTable();

            string g_barcode = barcode;
            string p_fchar = g_barcode.Substring(0, 1);
            int l_fnum = 1;
            if (p_fchar == "0")
            {
                p_fchar = g_barcode.Substring(1,1);
                l_fnum = 2;
            }
            string l_barcode = g_barcode.Substring(l_fnum);
            //1.获取tc_xxu001 java里是取最后一个   select tc_xxu001 from tc_xxu_file where tc_xxu011='" + p_type      Azz_Get.getdata(p_fchar, replay0);  
            string g_type = XxuFiles.Get_tc_xxu001(p_fchar);
            //2.

            switch (g_type)
            {
                case "L":
                    #region
                    //    dwr.util.setValue("requisition", l_barcode);
                    //    var p620_show = function(bsfp620) {
                    //        data1 = bsfp620;
                    //        if (data1.length == 0)
                    //        {
                    //            alert("未找到此入库单信息或已过账，请检查！");
                    //            dwr.util.setValue("requisition", "");
                    //            dwr.util.setValue("barcode", '');
                    //            return;
                    //        }
                    //    $(function() { $('#dg').datagrid('loadData', data1); })
                    //}
                    //    bsfp620_manager.b620_getdata(l_barcode, p620_show);
                    //    //document.getElementById('barcode').value = '';
                    //    dwr.util.setValue("barcode", '');
                    //    break;
                    #endregion
                    //1.
                    requisition = l_barcode;
                    //2.showlist ,放到后面
                    #region 
                    //dt = bsfp620_manager.b620_getdata(l_barcode); 
                    //if (dt != null && dt.Rows.Count > 0)
                    //{
                    //}
                    //else
                    //{
                    //    message = "未找到此入库单信息或已过账，请检查！";
                    //    requisition = "";
                    //}
                    #endregion
                    break;
                case "A":
                    //window.alert(g_type + "未能识别之条码，请确认！");
                    message = "未能识别之条码，请确认！";
                    break;
                case "C":
                    //判断单号值，如果为空，则提醒先扫描单号
                    if (requisition == ""){message = "请先扫描单号条码！";break;}
                    if (employee == ""){message = "请先扫描工号条码！";break;}
                    if (position == ""){message = "请先扫描储位条码！";break;}
                    //料号
                    l_fnum = 2;
                    int l_enum = g_barcode.Length - 10;
                    string l_sfv04 = g_barcode.Substring(l_fnum, l_enum);

                    List<string> p_number = TcBrbFiles.get_tc_brb11(g_barcode, "A");

                    if (p_number[0] == "A"){message="该包装票不存在";break;}
                    if (p_number[0] == "B"){message="该包装票未装箱";break;}
                    if (p_number[0] == "C"){message="该包装票未装满箱";break;}
                    if (p_number[0] == "D"){message="该料号当班次未QC检验";break;}
                    if (p_number[0] == "E"){message="该包装票下的结构件条码前缀重复，请检查！";break;}

                    int p_sfv09 = Convert.ToInt32(p_number[1]);
                    if (p_sfv09 > 0)
                    {
                        //获取到物料编号910221211858
                        l_sfv04 = TcBrbFiles.get_tc_brb22(g_barcode);
                        //更新储位及数量
                        string ins_boolean = RvbsFiles.ins_rvbs("asft620", requisition, l_sfv04, g_barcode, p_sfv09, position, employee);
                        //判断 ins_boolean 
                        string g_success = ins_boolean;
                        if (g_success == "A")
                        {
                            #region 成功, 在这里更新imgs_file? g_barcode .不用考虑用户取消过账等情况？


                            //更新包装票对应的imgs信息
                            ImgsFiles.Save(g_barcode);

                            ////取得 index
                            //var l_rows = $('#dg').datagrid('getRows');
                            //for (i = 0; i < l_rows.length; i++)
                            //{
                            //    if (l_rows[i]['sfv04'] == l_sfv04)
                            //    {
                            //        var l_sfv09 = l_rows[i]['sfv09'];
                            //        l_rows[i]['sfv09'] = l_sfv09 - p_sfv09;
                            //        if (l_sfv09 <= 0)
                            //        {
                            //              $('#dg').datagrid('deleteRow', i);
                            //        }
                            //        $('#dg').datagrid('refreshRow', i);
                            //    }
                            //}
                            //dwr.util.setValue("packno", g_barcode);
                            //dwr.util.setValue("barcode", '');
                            #endregion
                        }
                        else
                        {
                            if (g_success == "W") { message = "该包装票已经上架，请检查！"; }
                            else if (g_success == "X") { message = "该包装票的数量超出本单数量"; }
                            else if (g_success == "U")
                            {
                                int p620_get_rvbs_sum = RvbsFiles.get_rvbs_sum_1(requisition, g_barcode, p_sfv09);

                                if (p620_get_rvbs_sum != 0)
                                {
                                    message = "该包装票还有" + p620_get_rvbs_sum.ToString() + "(数量) 未入库，请检查！'";
                                }
                            }
                            else if (g_success == "E") { message = "未维护对应存储位置信息，请检查！"; }
                            else { message = "更新不成功！"; }
                        }

                    }
                    else { message = "包装票不存在或者未装满"; }

                    break;
                case "H":
                    employee = l_barcode;
                    break;
                case "M":
                    position = l_barcode;
                    break;
                default:
                    message = "未能识别之条码，请确认！";
                    break;
            }

            //showlist
            if (requisition != "")
            {
                dt = bsfp620_manager.b620_getdata(requisition);
                if (dt != null && dt.Rows.Count > 0)
                {
                }
                else
                {
                    message = "未找到此入库单信息或未过账，请检查！";
                    requisition = "";
                }
            }

            //初始化ViewModel
            Bsfp625ViewModel model = new Bsfp625ViewModel()
            {
                employee = employee,
                requisition = requisition,
                packno = packno,
                position = position,
                message = message,
                List = dt
            };
            //返回View
            return View(model);

        }


        public ActionResult imgssave(string axmtcode = "",string barcode="")
        {
            string message = "";
            if (axmtcode != "")
            {
                //优先出货单
                List<RVBS_FILE> list = Rvbss.GetList(axmtcode);
                foreach (RVBS_FILE m in list)
                {
                    //更新包装票对应的imgs信息
                    ImgsFiles.Save(m.RVBS04);
                }
            }
            else if (barcode != "")
            {
                //更新包装票对应的imgs信息
                ImgsFiles.Save(barcode);
                message = "更新完成";
            }
            //初始化ViewModel
            ImgsSaveViewModel model = new ImgsSaveViewModel()
            {
                axmtcode = axmtcode,
                barcode = barcode,
                message = message
            };
            //返回View
            return View(model);
        }

        //private string ins_boolean(string p_boolean)
        //{
        //    string message = "";

        //    string g_success = p_boolean;
        //    if (g_success == "A")
        //    {
        //        #region 成功

        //        ////取得 index
        //        //var l_rows = $('#dg').datagrid('getRows');
        //        //for (i = 0; i < l_rows.length; i++)
        //        //{
        //        //    if (l_rows[i]['sfv04'] == l_sfv04)
        //        //    {
        //        //        var l_sfv09 = l_rows[i]['sfv09'];
        //        //        l_rows[i]['sfv09'] = l_sfv09 - p_sfv09;
        //        //        if (l_sfv09 <= 0)
        //        //        {
        //        //              $('#dg').datagrid('deleteRow', i);
        //        //        }
        //        //        $('#dg').datagrid('refreshRow', i);
        //        //    }
        //        //}
        //        //dwr.util.setValue("packno", g_barcode);
        //        //dwr.util.setValue("barcode", '');
        //        #endregion
        //    }
        //    else
        //    {
        //        if (g_success == "W"){message ="该包装票已经上架，请检查！";}
        //        else if (g_success == "X"){message="该包装票的数量超出本单数量";}
        //        else if (g_success == "U")
        //        {
        //            int p620_get_rvbs_sum = RvbsFiles.get_rvbs_sum_1(l_requisition, g_barcode, p_sfv09);

        //            if (p620_get_rvbs_sum != 0)
        //            {
        //                message = "该包装票还有" + p620_get_rvbs_sum.ToString() + "(数量) 未入库，请检查！'";
        //            }
        //        }
        //        else if (g_success == "E"){message="未维护对应存储位置信息，请检查！";}
        //        else{message="更新不成功！";}
        //    }
        //    //
        //    return message;

        //}



    } 






} 