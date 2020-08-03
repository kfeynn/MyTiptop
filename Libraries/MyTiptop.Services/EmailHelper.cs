using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.IO;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.Net.Security;
using MyTiptop.Core;
using MyTiptop.Data;
using System.Threading;

namespace MyTiptop.Services
{

    #region  邮件发送

    //发送邮件至少需要发送邮件服务器信息和邮件信息，因此我们建立Host和Mail两个配置类。
    public class ConfigHost
    {
        public string Server { get; set; }  // 邮件服务器地址
        public int Port { get; set; } // 端口 
        public string Username { get; set; }  //登录名
        public string Password { get; set; }  //密码
        public bool EnableSsl { get; set; } // true
    }

    public class ConfigMail
    {
        public string From { get; set; }  //从哪里发送
        public string FromName { get; set; } //以什么名字发送
        public string[] To { get; set; }   //目的地址
        public string[] CC { get; set; }  //抄送地址
        public string[] Bcc { get; set; }  //密送地址
        public string Subject { get; set; } // 主题
        public string Body { get; set; }   //邮件正文 
        public string[] Attachments { get; set; } //附件
        public string[] Resources { get; set; }
    }
    //同时定义一个统一的接口ISendMail，以方便测试和比较。
    public interface ISendMail
    {
        void CreateHost(ConfigHost host);
        void CreateMail(ConfigMail mail);
        //void CreateMultiMail(ConfigMail mail);
        void SendMail();
    }

    //使用System.Net.Mail
    public class UseNetMail : ISendMail
    {
        private MailMessage Mail { get; set; }
        private SmtpClient Host { get; set; }

        //从外面传进来的参数
        private ConfigMail cMail { get; set; }
        private ConfigHost cHost { get; set; }

        public UseNetMail()
        {
            //构造函数，初始化对象
        }

        public UseNetMail(ConfigMail mail, ConfigHost host )
        {
            //构造函数，初始化对象
            cMail = mail;
            cHost = host;
        }



        public void CreateHost(ConfigHost host)
        {

            Host = new SmtpClient(host.Server, host.Port);
            Host.UseDefaultCredentials = false;
            Host.Credentials = new System.Net.NetworkCredential(host.Username, host.Password);
            Host.EnableSsl = host.EnableSsl;
        }

        public void CreateMail(ConfigMail mail)
        {
            Mail = new MailMessage();
            Mail.From = new MailAddress(mail.From, mail.FromName, System.Text.Encoding.UTF8);

            //Mail.From = new MailAddress("yuanqiang.zheng@grand-tec.com", "来自模具系统的邮件2", System.Text.Encoding.UTF8);

            if (mail.To != null)
            {
                //收件人
                foreach (var t in mail.To) { Mail.To.Add(t); }
            }
            if (mail.CC != null)
            {
                //抄送人
                foreach (var t in mail.CC) { Mail.CC.Add(t); }
            }
            if (mail.Bcc != null)
            {
                //密送人
                foreach (var t in mail.Bcc) { Mail.Bcc.Add(t); }
            }
            Mail.Subject = mail.Subject;
            Mail.Body = mail.Body;
            Mail.IsBodyHtml = true; // true;
            Mail.BodyEncoding = System.Text.Encoding.UTF8;
            Mail.SubjectEncoding = System.Text.Encoding.UTF8;

            //msg.IsBodyHtml = true;
            //msg.Priority = MailPriority.High;//优先级

        }

        public void CreateMultiMail(ConfigMail mail)
        {
            CreateMail(mail);

            if (mail.Resources != null && mail.Resources.Length > 0)  //不启用这段
            {
                Mail.AlternateViews.Add(AlternateView.CreateAlternateViewFromString("If you see this message, it means that your mail client does not support html.", Encoding.UTF8, "text/plain"));
                var html = AlternateView.CreateAlternateViewFromString(mail.Body, Encoding.UTF8, "text/html");
                foreach (string resource in mail.Resources)
                {
                    var image = new LinkedResource(resource, "image/jpeg");
                    image.ContentId = Convert.ToBase64String(Encoding.Default.GetBytes(Path.GetFileName(resource)));
                    html.LinkedResources.Add(image);
                }
                Mail.AlternateViews.Add(html);
            }

            foreach (var attachment in mail.Attachments)
            {
                Mail.Attachments.Add(new Attachment(attachment));
            }
        }

        public void SendMail()
        {
            if (Host != null && Mail != null)
            {
                try
                {
                    //发送邮件
                    Host.Send(Mail);
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
            }
            else
            {
                throw new Exception("These is not a host to send mail or there is not a mail need to be sent.");
            }
        }

        /// <summary>
        /// 新开线程，后台运行
        /// </summary>
        public void ThreadSendMail()
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

        private void DoWork()
        {
            try
            {
                //发送邮件
                UseNetMail email = new UseNetMail(cMail, cHost);

                email.CreateHost(cHost);

                email.CreateMultiMail(cMail);

                email.SendMail();
            }
            catch (Exception Ex){

                //记录错误信息

                Log log = new Log()
                {
                    Date = DateTime.Now,
                    Exception = Ex.Message,
                    ErrLevel = "Error",
                    Logger = "MyTiptop.Services.ThreadSendMail"
                    //Context = WorkContext.UserName
                };
                Logs.AddModel(log);

            }
        }



    }
    #endregion

}
