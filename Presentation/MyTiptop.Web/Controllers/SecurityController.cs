using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyTiptop.Web.Models;
using MyTiptop.Web.Framework;
using MyTiptop.MysqlData;
using MyTiptop.Core;
using MyTiptop.Data;
using System.IO;
using MyTiptop.Services;
using System.Collections;
using System.Text;


using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.Net.Security;

namespace MyTiptop.Web.Controllers
{
    public class SecurityController : BaseWebController
    {
        public SecurityController()
        {
            //后台调用一下 方法
            //MyTiptop.OraData.OraQuery.ThreadMethod();

        }

        // GET: Security
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ComplaintIndex()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ComplaintShow()
        {
            string type = "1";
            if (Request["type"] != null)
            {
                type = Request["type"];
            }
            ComplaintShowViewModel model = new ComplaintShowViewModel()
            {
                m = new QA_Complaint(),
                Message = "",
                annexList = new List<QA_Complaint_annex>(),
                flag = "0",
                type = type
            };
            return View(model);
        }

        [HttpPost] 
        public ActionResult ComplaintShow(QA_Complaint m,int sid)
        {
            ComplaintShowViewModel model = new ComplaintShowViewModel();

            string type = "1";
            if (Request["type"] != null)
            {
                type = Request["type"];
            }

            if (sid == 0)
            {
                m.data11 = DateTime.Now; //更新时间
                m.data06 = TypeHelper.StringToInt(type, 1);  //投诉类别
                sid = Complaints.AddModel(m);
                m.id = sid;  //直接把id赋值，省的再查一遍
                model.Message = "";
                model.m = m;
                model.annexList = new List<QA_Complaint_annex>();
                //model.flag = "1";
                model.type = type;
            }
            else 
            { 
                m.data11 = DateTime.Now; //更新时间 
                m.data06 = TypeHelper.StringToInt(type, 1);  //投诉类别 
                m.id = sid; 
                //先更新 
                Complaints.UpdateModel(m, sid); 
                 
                model.Message = ""; 
                model.m = Complaints.GetModel(sid); 
                model.annexList = ComplaintAnnexs.GetList(sid);
               
                model.type = type;  
            }

            //食堂投诉最少2张图片,其它投诉最少1张图片..暂时取消图片限制
            if ((type == "1" && model.annexList.Count >= 0) || ((type == "2" || type == "3" || type == "4" || type == "5" || type == "6" || type == "7" || type == "8") && model.annexList.Count >= 0))
            {
                model.flag = "2";
            }
            else
            {
                model.flag = "1";
            }

            return View(model);  
        }  

        [HttpGet]
        public ActionResult Complaint()
        {
            ComplaintViewModel model = new ComplaintViewModel();
            return View(model);
        }

        public ActionResult Complaintajax(string user = "", string code = "")
        {
            string status = "0";
            //保存表单。
            if (code != "")
            {
                //假设保存成功
                status = "1";
            }
            return AjaxResult("success", status, true);
        }

        [HttpPost]
        public ActionResult Complaint(HttpPostedFileBase txt_file, string user, string code,string phone)
        {
            //1.表单： flow_data_375
            //2.流程： flow_run_prcs 
            //3.       flow_run
            //4.       flow_run_log
            //select max(run_id)from flow_run
            //select max(run_id) from flow_run_log 取最大值
            //流程实例基本信息（flow_run）
            //流程实例步骤信息（flow_run_prcs）
            //5.flow_process
            //附件
            //6.ATTACHMENT
            //7.flow_run_attach  : ATTACHMENT_ID =  AID "@" YM "_" ATTACH_ID

            if (txt_file != null)
            {
                var oStream = txt_file.InputStream;
            }
            string d = user;
            string dd = code;



            ComplaintViewModel model = new ComplaintViewModel();
            return View(model);

            //处理完 无误 转向显示页面 ComplaintShow ，有错误则返回当前页面并提示错误。


        }

        //public ActionResult Upload(HttpPostedFileBase file)
        public string UploadFile(HttpPostedFileBase file, string ufile,int sid)
        {
            ////////////   上传文件            
            if (file != null && file.ContentLength > 0)
            {

                //准备上传路径 
                QA_Complaint com = Complaints.GetModel(sid);
                if (com != null)
                {
                    DateTime dt = (DateTime)com.data11;
                    string filepath = ufile + @"\" + dt.ToString("yyyyMM");

                    string path = UpLoadFile.initFilePath(filepath, sid);  //上传路径
                    string logicPath = @"upload";

                    path = filepath + @"\" + sid;

                    var fileName = Path.GetFileName(file.FileName);
                    var fullpath = Path.Combine(path, fileName);   //完整路径
                    logicPath = logicPath + @"\" + dt.ToString("yyyyMM") + @"\" + sid + @"\" + fileName;

                    if (!UpLoadFile.IsAllowedExtension(fileName))
                    {
                        return "此文件不允许上传!";
                    }
                    //保存
                    file.SaveAs(fullpath);

                    //有记录则更新。

                    QA_Complaint_annex annexModel = new QA_Complaint_annex()
                    {
                        data01 = sid,        //主表id
                        data02 = fileName,   //文件名称 。 
                        data03 = logicPath, //虚拟路径    
                        data04 = fullpath    //完整路径 
                    };

                    int annexId = ComplaintAnnexs.IsExists(sid, fileName);
                    if (annexId > 0)
                    {
                        //无记录 添加到附件表
                        ComplaintAnnexs.UpdateModel(annexModel, annexId);
                    }
                    else
                    {
                        //无记录 添加到附件表
                        ComplaintAnnexs.AddModel(annexModel);
                    }

                }
            }
            return ""; 
        }


        /// <summary>
        /// 创建邮件正文
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public string CreateMailBody(int sid = 0, int type = 1) 
        { 
            string mailbody = "";

            
            QA_Complaint model = Complaints.GetModel(sid);

            if (model != null)
            {
                mailbody += "<p>";
                mailbody += "姓名：" + model.data01 ;
                mailbody += "</p>";
                mailbody += "<p>";
                mailbody += "工号：" + model.data02 ;
                mailbody += "</p>";
                mailbody += "<p>";
                mailbody += "联系方式：" + model.data03 ;
                mailbody += "</p>";
                mailbody += "<p>";
                mailbody += "地点：" + model.data04 ;
                mailbody += "</p>";
                mailbody += "<p></p>";
                mailbody += "<p>";
                mailbody += "投诉内容：" + model.data05 ;
                mailbody += "</p>";


                mailbody += "<br><br><p> 系统邮件，请勿回复！</p>";

            }

            return mailbody; 
        }

        /// <summary>
        /// 记录已发邮件
        /// </summary>
        /// <param name="sid"></param>
        public void updateComplaint(int sid)
        {
            var model = Complaints.GetModel(sid);
            if (model != null)
            {
                model.data07 = 1;

                Complaints.UpdateModel(model, sid);
            }
        }
     
        //发送邮件
        public ActionResult sendEmailAjax(int sid = 0 ,int type = 1)
        {
            string status = "0";

            string mailto = "yuanqiang.zheng@grand-tec.com";


            if (type == 1)
            {
                mailto = BMAConfig.EmailConfig.Receiver1;  //多个接收人 一 ； 分隔
            }
            else if (type == 2)
            {
                mailto = BMAConfig.EmailConfig.Receiver2;  //多个接收人 一 ； 分隔
            }
            else if (type == 3)
            {
                mailto = BMAConfig.EmailConfig.Receiver3;  //多个接收人 一 ； 分隔
            }
            else if (type == 4)
            {
                mailto = BMAConfig.EmailConfig.Receiver4;  //多个接收人 一 ； 分隔
            }
            else if (type == 5)
            {
                mailto = BMAConfig.EmailConfig.Receiver5;  //多个接收人 一 ； 分隔
            }
            else if (type == 6)
            {
                mailto = BMAConfig.EmailConfig.Receiver6;  //多个接收人 一 ； 分隔
            }
            else if (type == 7)
            {
                mailto = BMAConfig.EmailConfig.Receiver7;  //多个接收人 一 ； 分隔
            }
            else if (type == 8)
            {
                mailto = BMAConfig.EmailConfig.Receiver8;  //多个接收人 一 ； 分隔
            }

            /////////////////////////
            string mailTitle = "";

            if (type == 1)
                mailTitle = "-食堂投诉";
            else if (type == 2)
                mailTitle = "-宿舍投诉";
            else if (type == 3)
                mailTitle = "-清洁投诉";
            else if (type == 4)
                mailTitle = "-工程维修投诉";
            else if (type == 5)
                mailTitle = "-车队投诉";
            else if (type == 6)
                mailTitle = "-其它投诉";
            else if (type == 7)
                mailTitle = "-违规违纪投诉";
            else if (type == 8)
                mailTitle = "-诚信廉洁投诉";



            //加这段之前用公司邮箱发送报错：根据验证过程，远程证书无效.加上后解决问题
            ServicePointManager.ServerCertificateValidationCallback = delegate (Object obj, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors) { return true; };
            //邮件服务器配置信息 
            ConfigHost host = new ConfigHost();
            host.Server = BMAConfig.EmailConfig.Host;  //发送邮件服务器
            host.Port = Convert.ToInt16(BMAConfig.EmailConfig.Port);  //邮件服务器端口   //配置文件不是数字就会出错
            host.Username = BMAConfig.EmailConfig.UserName;  //邮箱账户
            host.Password = BMAConfig.EmailConfig.Password;  //邮箱密码
            host.EnableSsl = true;

            //邮件信息
            ConfigMail mail = new ConfigMail();
            mail.From = BMAConfig.EmailConfig.From;  //从哪个邮箱发出去
            mail.FromName = BMAConfig.EmailConfig.FromName + mailTitle;  //发送名字
            mail.Subject = BMAConfig.EmailConfig.FromName;  //标题

            
            //发送地址 
            ArrayList list = new ArrayList();
            //接收人
            string[] s = mailto.Split(';');
            foreach (string m in s)
            {
                //粗略排除掉空值
                if (m.Length > 5)
                {
                    //添加收件人
                    list.Add(m);
                }
            }
            //list.Add("yuanqiang.zheng@grand-tec.com");            //list.Add("bbb");  //其它收件人
            string[] mailTo = (string[])list.ToArray(typeof(string));
            mail.To = mailTo;   //收件人

           
            ////抄送、密送人
            //ArrayList listCC = new ArrayList();
            //listCC.Add("13652942624@163.com");
            //string[] mailCC = (string[])listCC.ToArray(typeof(string));
            //mail.CC = mailCC;   //抄送人

         
            //以下部分可以根据业务循环发送多封邮件 
            mail.Body = CreateMailBody(sid,type);

            //添加附件
            ArrayList AttachmentsList = new ArrayList();
            //附件列表

            List<QA_Complaint_annex> annexList = ComplaintAnnexs.GetList(sid);

            foreach (QA_Complaint_annex m in annexList)
            {
                //粗略排除掉空值
                if (m.data04.Length > 5)
                {
                    //添加收件人
                    AttachmentsList.Add(m.data04.ToString());
                }
            }
            //list.Add("yuanqiang.zheng@grand-tec.com");            //list.Add("bbb");  //其它收件人
            string[] attachments = (string[])AttachmentsList.ToArray(typeof(string));
           
            mail.Attachments = attachments;

            //新开线程发送邮件
            UseNetMail sendmailsss = new UseNetMail(mail, host);
            try
            {
                //新开线程发送邮件
                sendmailsss.ThreadSendMail();
                status = "1";
            }
            catch (Exception Ex)
            {
                status = "2";
            }

            //记录已经发邮件。
            updateComplaint(sid);


            //同步发送邮件。耗时、取消
            //UseNetMail sendmailsss = new UseNetMail();
            //sendmailsss.CreateHost(host);
            //sendmailsss.CreateMultiMail(mail);
            //try
            //{
            //    //发送邮件。 
            //    sendmailsss.SendMail();
            //    status = "1";
            //}
            //catch (Exception Ex)
            //{
            //    status = "2";
            //}

            ////////////////////////////

            return AjaxResult("success", status, true);
        }



        public ActionResult deleteAnnexAjax(int sid =0)
        {
            string status = "0";

            //int id = TypeHelper.StringToInt(sid);

            if (sid > 0)
            {
                
                QA_Complaint_annex model = ComplaintAnnexs.GetModel(sid);
                if (model != null)
                {
                    string fullpath = model.data04;
                    //删除
                    ComplaintAnnexs.DeleteModel(sid);
                    //删除文件
                    DeleteFile(fullpath);
                    status = "1";
                }
            }

            return AjaxResult("success", status, true);
        }


        /// <summary>
        /// 删除文件
        /// </summary>
        public void DeleteFile(string filepath)
        {
            //存在文件 
            if (System.IO.File.Exists(filepath)) //Server.MapPath("~/upimg/Data.html")))
            {
                System.IO.File.Delete(filepath);//删除文件
            }
        }

        /// <summary>
        /// 上传附件（图片）
        /// </summary>
        /// <param name="sid">表单ID</param>
        /// <returns></returns>
        public ActionResult UpLoad(string sid = "0")
        {
            string status = "0";

            int key = TypeHelper.StringToInt(sid);

            if (key != 0)
            {
                try
                {

                    var colletion = HttpContext.Request.Files;

                    HttpPostedFileBase file = colletion[0];

                    SecurityController filehelper = new Controllers.SecurityController();

                    string ufile = Server.MapPath("~/upload");  //固定上传路径

                    string path = filehelper.UploadFile(file, ufile, key);

                    status = "1";


                    //int i = 0;
                    //int ss = 5 / i;
                }
                catch (Exception Ex)
                {
                    //记录错误信息

                    Log log = new Log()
                    {
                        Date = DateTime.Now,
                        Exception = Ex.Message,
                        ErrLevel = "Error",
                        Logger = "SecurityController.UpLoad",
                        Context = WorkContext.UserName
                    };
                    Logs.AddModel(log);
                }
            }


            return AjaxResult("success", status, true);
        }

        public ActionResult Update(string fishId)     //HttpPostedFileBase[] txt_file
        {
            //获取邮件配置信息
            //string aa =BMAConfig.EmailConfig.From;

            var colletion = HttpContext.Request.Files;

            //HttpPostedFileBase file = Request.Files[0];

            var oFile = Request.Files["txt_file"];
            var oStream = oFile.InputStream;
            //得到了文件的流对象，我们不管是用NPOI、GDI还是直接保存文件都不是问题了吧。。。。
            //后台TODO

            //保存文件后，跳转到 展示页面。

            string status = "1";

            return AjaxResult("success", status, true);

        }




        #region  来访登记

        public ActionResult Registration(string idNumber = "", string idNumberBak = "")
        {
            //记录错误提示
            string message = "";
            string card_no = "";//来访登记卡
            string id_no = "";

            //判断输入的值。 证件号码 or 来访登记卡 

            //  1:来访登记，步骤1  ，查询来访单 
            //  2:来访登记，步骤2  ，办理来访卡、入厂 
            //  3:离厂 
            string switchFlag = "1";
            id_no = idNumber;

            string f = "";

            if (idNumber != "" && idNumber.Length > 1)
            {
                f = idNumber.Substring(0, 1);
            }
            if (f == "K" || f == "G")  //来访登记卡
            {
                if (idNumberBak.Length > 1)
                {
                    switchFlag = "2";
                    card_no = idNumber;
                    id_no = idNumberBak;
                }
                else
                {
                    switchFlag = "3";
                    card_no = idNumber;
                    id_no = "";
                }
            }

            flow_data_362 model = MyTiptop.MysqlData.flow_data_362dao.findByIdNumber(id_no);

            if (model != null && model.data_33 != "")
            {
                if (switchFlag != "1")
                {
                    message = "来访单已经是离厂状态，不能更改";
                    switchFlag = "1";
                }
            }
            switch (switchFlag)
            {
                case "1":
                    //没有更新动作
                    break;
                case "2":
                    //执行更新动作
                    guest_card cardModel = guest_carddao.GetModel(card_no);
                    if (cardModel == null)
                    {
                        message = "来访卡不存在！";
                    }
                    else
                    {
                        flow_data_362bll.In(idNumberBak, card_no);
                    }
                    model = MyTiptop.MysqlData.flow_data_362dao.findByIdNumber(id_no);
                    break;
                case "3":
                    //执行更新动作
                    guest_card cardModel1 = guest_carddao.GetModel(card_no);
                    if (cardModel1 == null)
                    {
                        message = "来访卡不存在！";
                    }
                    else
                    {
                        flow_data_362bll.Out(card_no);
                    }
                    //model 由 card_no 来获取
                    model = MyTiptop.MysqlData.flow_data_362dao.findByCardAndDate(card_no);
                    if (model != null)
                    {
                        id_no = model.data_11;
                    }
                    break;
            }

            //初始化ViewModel
            RegistrationViewModel viewModel = new RegistrationViewModel()
            {
                idNumber = idNumber,
                idNumberBak = id_no,
                message = message,
                model = model
            };
            //返回View
            return View(viewModel);
        }

        //ajax 调用 更新实际来访人数
        public ActionResult updateData_43(string idNumber,int data_43)
        {
            int date43 = flow_data_362dao.updateData43(idNumber, data_43);
            return AjaxResult("success", "\"" + date43.ToString() + "\"", true);
        }


        //ajax 调用 更新车牌
        public ActionResult updateData_45(string idNumber, string data_45)
        {
            string date45 = flow_data_362dao.updateData45(idNumber, data_45);
            return AjaxResult("success", "\"" + date45.ToString() + "\"", true);
        }

        //ajax 调用 判断是否已经离场
        public ActionResult isLeave(string idNumber)
        {
            //当天来访是否已经离场。
            string returnValue = "0";
            flow_data_362 model = MyTiptop.MysqlData.flow_data_362dao.findByIdNumber(idNumber);
            if (model != null && model.data_33 != "")
            {
                returnValue = "1";
            }
            return AjaxResult("success", "\"" + returnValue + "\"", true);
        }


        /// <summary>
        /// 查找匹配车牌 AJAX 
        /// </summary>
        /// <param name="prdtName"></param>
        /// <returns></returns>
        public ActionResult getPartIn(string p)
        {
            string prdtList = tccdao.getParkInCodeList(); 

            //json
            //[{"label": "博客园", "value": "cnblogs"}, {"label": "囧月", "value": "囧月"}]

            return AjaxResult("success", prdtList, true);
        }


        #endregion 

    }
}