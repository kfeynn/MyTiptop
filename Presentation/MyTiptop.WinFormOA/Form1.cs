using System; 
using System.Collections.Generic; 
using System.ComponentModel; 
using System.Data; 
using System.Drawing; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
using System.Windows.Forms;
using System.Net.Mail;
using System.IO;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.Net.Security;
using System.Collections;
using MyTiptop.Services;
using System.Data.SqlClient;
using MyTiptop.MysqlData;

namespace MyTiptop.WinFormOA
{
    public partial class Form1 : Form 
    {
        public Form1()
        { 
            InitializeComponent(); 
        }

        private static string sendtime = "10:00";
         
        private void Form1_Load(object sender, EventArgs e)
        {
            //获取邮件发送日期
            string ConfigUrl = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Config.txt"; //文件地址

            StreamReader sr = new StreamReader(ConfigUrl);
            this.txtSendTime.Text = sr.ReadLine();
             

            sendtime = this.txtSendTime.Text;



            sr.Close();
            //一分钟查看一次
            timer1.Interval = 60000;
            timer1.Start();
        }

        private void button1_Click(object sender, EventArgs e) 
        { 
            this.lblMessage.Text = ""; 
            sendEmail(); 
            //this.lblMessage.Text = "发送完成"; 
        } 
         
        private string MailBody(List<flow_data_366> dt)
        {
            string mailStr = "<html><head><meta http-equiv='content-type' content='text/html;charset=UTF-8'><title></title></head> <body> <div>您好:</div><div></div>";

            string listStr = "";
            //获取应发送email的mould列表
            //DataTable dt = GetSendList();
            listStr += "<div><br></div>";
            listStr += "<div>";
            //序号	图号	模具编号	供应商	联系人（电话）	T0计划试模时间	距离试模日期天数
            listStr += "<table class=MsoNormalTable border=1 cellpadding=0>";

            listStr += "<tr>";

            listStr += "<th>序号</th>";
            listStr += "<th>图号</th>";
            listStr += "<th>模具编号</th>";
            listStr += "<th>供应商</th>";
            listStr += "<th>联系人(电话)</th>";
            listStr += "<th>计划试模时间</th>";
            listStr += "<th>距离试模日期天数</th>";

            listStr += "</tr>";

            int i = 1;

            foreach (flow_data_366 model in dt)
            {
                listStr += "<tr>";
                listStr += "<td>" + i.ToString() + "</td>";
                listStr += "<td>" + model.data_6 + "</td>";
                listStr += "<td>" + model.data_206 + "</td>";
                listStr += "<td>" + model.data_11 + "</td>";
                listStr += "<td>" + model.data_12 + "</td>";
                listStr += "<td>" + model.data_235 + "</td>";
                listStr += "<td>" + model.diff + "</td>";
                listStr += "</tr>";
                i++;
            }

            listStr += "</table>";

            listStr += "</div>";

            listStr += "<br>";


            mailStr += listStr;

            mailStr += "<br><br><div>  系统邮件，请勿回复！</div>";


            mailStr += "</body></html>";

            //拼接邮件正文

            //将列表记录到已经发送表。（不考虑邮件发送失败）

            //SaveEmailRecord(dt);

          

            return mailStr; 
        }


        private void SaveEmailRecord(List<flow_data_366> dt)
        {
            ////保存邮件发送记录。
            //using (DBContext dbContext = new DBContext())
            //{
            //    foreach (DataRow row in dt.Rows)
            //    {
            //        //用于三元表达式赋空值
            //        int? Null = null;

            //        b_mailRecord NewModel = new b_mailRecord()
            //        {
            //            mouldId = row["id"] == null ? Null : TypeHelper.StringToInt(row["id"].ToString()),
            //            idate = DateTime.Now,
            //            ruleType = TypeHelper.StringToInt(row["ruleType"].ToString()),
            //            usedCount = row["usedCount"] == null ? Null : TypeHelper.StringToInt(row["usedCount"].ToString())
            //        };
            //        //增加
            //        dbContext.b_mailRecord.Add(NewModel);
            //    }

            //    //提交保存
            //    dbContext.SaveChanges();
            //}
        }

        /// <summary>
        /// 获取发送邮件的mould清单
        /// </summary>
        /// <returns></returns>
        private DataTable GetSendList()
        {
            //using (DBContext dbContext = new DBContext())
            //{
            //    //准备查询sql语句 ， xpGrid_Functions  + checked  ： 功能权限 + 是否选中
            //    // ADO.NET 方式执行SQL 。 返回DataTable
            //    SqlConnection conn = new System.Data.SqlClient.SqlConnection();
            //    conn = (SqlConnection)dbContext.Database.Connection;
            //    if (conn.State != ConnectionState.Open)
            //    {
            //        conn.Open();
            //    }
            //    SqlCommand cmd = new SqlCommand();
            //    cmd.Connection = conn;
            //    cmd.CommandType = CommandType.Text;

            //    string cmdStr = "exec p_GetEmailSendList ";

            //    cmd.CommandText = cmdStr;
            //    //执行填充Table
            //    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            //    DataTable dt = new DataTable();
            //    adapter.Fill(dt);

            //    return dt;
            //}
            return null;
        }

        private void sendEmail()
        {

            //加这段之前用公司邮箱发送报错：根据验证过程，远程证书无效.加上后解决问题
            ServicePointManager.ServerCertificateValidationCallback = delegate (Object obj, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors) { return true; };
            //邮件服务器配置信息 
            ConfigHost host = new ConfigHost();
            host.Server = ConfigurationManager.AppSettings["System_MailServerSmtp"].ToString();  //发送邮件服务器
            host.Port = Convert.ToInt16(ConfigurationManager.AppSettings["System_MailPort"].ToString());  //邮件服务器端口   //配置文件不是数字就会出错
            host.Username = ConfigurationManager.AppSettings["System_MailServerUserName"].ToString();  //邮箱账户
            host.Password = ConfigurationManager.AppSettings["System_MailServerPassWord"].ToString();  //邮箱密码
            host.EnableSsl = true;

            //邮件信息
            ConfigMail mail = new ConfigMail();
            mail.From = ConfigurationManager.AppSettings["System_FromEMail"].ToString();  //从哪个邮箱发出去
            mail.FromName = ConfigurationManager.AppSettings["System_FromName"].ToString();  //发送名字
            mail.Subject = ConfigurationManager.AppSettings["System_Subject"].ToString();  //标题

            //发送地址 
            ArrayList list = new ArrayList();
            //接收人
            string receivers = ConfigurationManager.AppSettings["System_Receiver"].ToString();
            string[] s = receivers.Split(';');
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
            //抄送、密送人
            ArrayList listBCC = new ArrayList();
            listBCC.Add("yuanqiang.zheng@grand-tec.com");
            string[] mailBCC = (string[])listBCC.ToArray(typeof(string));
            mail.Bcc = mailBCC;   //密送人



            //DataTable dt = GetSendList();
            List<flow_data_366> dtlist = flow_data_366dao.findBy();
            if (dtlist != null && dtlist.Count  > 0) 
            {
                //以下部分可以根据业务循环发送多封邮件 
                mail.Body = MailBody(dtlist);

                UseNetMail sendmailsss = new UseNetMail();
                sendmailsss.CreateHost(host);
                sendmailsss.CreateMail(mail);
                try
                {
                    //发送邮件。 
                    sendmailsss.SendMail();
                }
                catch (Exception Ex)
                {
                    this.lblMessage.Text = Ex.Message;

                    //MessageBox.Show(Ex.Message );
                }
            }
        } 
      
        private void timer1_Tick(object sender, EventArgs e) 
        {
            //时钟事件 
            try
            {
                //时间控件方法
                this.lblClock.Text = DateTime.Now.ToShortTimeString();
                //排出周六周日
                //if (DateTime.Now.DayOfWeek != DayOfWeek.Saturday && DateTime.Now.DayOfWeek != DayOfWeek.Sunday)
                //{
                    string ThisTime = DateTime.Now.ToShortTimeString();//得到现在的时间

                    if (ThisTime.Equals(sendtime))
                    {
                        this.lblMessage.Text = "进入自动执行发送程序！";

                        //单线程发送邮件，需不需要开多线程后台发送，前台不管？Thread 
                        sendEmail();

                        this.lblMessage.Text = "发送完成";

                        //this.lblMessage.Text = "进入自动执行发送程序！";

                    }
                //}
            }
            catch (Exception Ex) 
            { 
                this.lblMessage.Text = Ex.Message; 
            } 
        }
          
        private void btnchangetime_Click(object sender, EventArgs e)   
        {  
            //修改配置文件：发送时间。
            //Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //cfa.AppSettings.Settings["sendTime"].Value = this.txtSendTime.Text.Trim();
            //cfa.Save();
            //ConfigurationManager.RefreshSection("appSettings");
            
            //保存设置的时间
            string text = this.txtSendTime.Text;
            string ConfigUrl = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Config.txt"; //文件地址
            StreamWriter sw = File.CreateText(ConfigUrl);
            sw.WriteLine(text);
            sw.Close();
            this.lblClock.Text = "设置完成";

            sendtime = text;

        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.MinimizedToNormal();
        }

        private void MinimizedToNormal()
        {
            this.Visible = true;
            this.WindowState = FormWindowState.Normal;
            this.notifyIcon1.Visible = false;
        }

        private void NormalToMinimized()
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                this.notifyIcon1.Visible = true;
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            NormalToMinimized();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            //最小化到托盘
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                this.notifyIcon1.Visible = true;
            }
        }

  
    } 
} 
