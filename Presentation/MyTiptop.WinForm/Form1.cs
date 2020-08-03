using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyTiptop.OraCore;
using MyTiptop.OraCore.Data;
using MyTiptop.OraData;
using LabelManager2;
using System.Configuration;
using System.Drawing.Printing;        //这是必备
using System.Printing;
using System.Management;
 

namespace MyTiptop.WinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //设置默认打印机
            SetPrinter();
            //MessageBox.Show("useless!");

            //PrintDocument fPrintDocument = new PrintDocument();    //获取默认打印机的方法

            //string printerName = fPrintDocument.PrinterSettings.PrinterName;

            ////aaa();

            //MessageBox.Show(printerName);
            //MessageBox.Show(Printer.GetPrinterStatus(printerName));
            //MessageBox.Show(Printer.GetPrinterStatusInt(printerName).ToString());

            if (this.txtqcs01.Text.Trim().Length > 5)   //按钮只能打印具体单据。
            {
                Print(this.txtqcs01.Text.Trim());
            }
            else
            {
                lblMessage.Text = "请出入单号";
            }




            //PrintQueue pq = LocalPrintServer.GetDefaultPrintQueue();
            //switch (pq.QueueStatus)
            //{
            //    case PrintQueueStatus.Offline:
            //        MessageBox.Show("脱机！..");
            //        break;
            //    default:
            //        break;
            //}

        }

       


        //设置 默认打印机
        private void SetPrinter()
        {
            ////结构件固定用此打印机  .打印机名称 要改写到配置文件。 
            string printer = ConfigurationManager.AppSettings["Printer"];
            PrintDocument fPrintDocument = new PrintDocument();    //获取默认打印机的方法
            if (printer != fPrintDocument.PrinterSettings.PrinterName)
            {
                if (Externs.SetDefaultPrinter(printer)) //设置默认打印机
                {
                    //MessageBox.Show(printer + "设置为默认打印机成功！");
                }
                else
                {
                    MessageBox.Show(printer + "设置为默认打印机失败！");
                }
            }
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

        /// <summary>
        /// 测试获取打印机状态
        /// </summary>
        private void aaa()
        {
            string query = string.Format("SELECT * from Win32_Printer ");
            var searcher = new ManagementObjectSearcher(query);
            var printers = searcher.Get();
            string printerName = ConfigurationManager.AppSettings["Printer"];
            string asdf = "";
            foreach (var printer in printers)
            { 
                Console.WriteLine(printer.Properties["Name"].Value); 
                if (printer.ToString().Contains(printerName))
                { 
                    MessageBox.Show(printer.Properties["WorkOffline"].Value.ToString()); //是否脱机 
                    foreach (var property in printer.Properties)
                    { 
                        asdf += string.Format("\t{0}: {1}", property.Name, property.Value); 
                        //MessageBox.Show(property.Name + "___" +  property.Value);
                    } 
                }
                this.lblMessage.Text = asdf;
            }
            //如此可以得到许多信息，包括状态、联机、脱机以及错误(-- 只能探测到是否脱机。状态不准确！！)
            //其中(bool)printer.Properties["WorkOffline"].Value 指示打印机是否脱机 
        }

        //打印入口
        private void Print(string qcs01 = "")
        {
            //设置默认打印机
            SetPrinter();

            string DBS = ConfigurationManager.AppSettings["DBS"];  //打印起始时间
            string plant = ConfigurationManager.AppSettings["plant"];  //打印截止时间
            string outsourcing = ConfigurationManager.AppSettings["outsourcing"];  //外协标记

            string[] plants = new string[] { };

            plants = plant.Split(',');
             
            List<QCS_FILE> modelList = new List<QCS_FILE>();

            List<string> qcsList = new List<string>();

            //1.获取List<QCS01> 

             
            if (qcs01 != "")  
            {
                //根据单号进行打印  
                //
                qcsList.Add(qcs01);
            } 
            else 
            {
                //modelList = QcsFiles.GetList(DBS, plants, 0); //获取所以未打印的单据

                string startDate = ConfigurationManager.AppSettings["startDate"];  //打印起始日期。

                qcsList = QcsFiles.GetList(DBS, plants, startDate, 0, outsourcing);
            }
            for (int n = 0; n < qcsList.Count; n++)
            {
                modelList = QcsFiles.GetList(qcsList[n].ToString(), DBS);

                for (int i = 0; i < modelList.Count; i++)
                {
                    string pageIndex = (i + 1).ToString() + "/" + modelList.Count.ToString();
                    IMA_FILE imaModel = ImaFiles.GetModel(modelList[i].QCS021, DBS);
                    PMC_FILE pmcModel = PmcFiles.GetModel(modelList[i].QCS03, DBS);
                    RVB_FILE rvbModel = RvbFiles.GetModel(modelList[i].QCS01, modelList[i].QCS02, DBS);

                    PrintDocument fPrintDocument = new PrintDocument();    //获取默认打印机的方法 
                    string printerName = fPrintDocument.PrinterSettings.PrinterName;

                    if (Printer.GetPrinterStatusInt(printerName) == 0) //准备就绪状态！（是否正确？）
                    {
                        //1.调用打印
                        //PrintDoc(pageIndex, imaModel, modelList[i], pmcModel, rvbModel);

                        //2.记录打印标记
                        //QcsFiles.UpadteModelUD10(modelList[i], 1, DBS);
                    }
                    else
                    {
                        MessageBox.Show("打印机缺纸，请处理！");
                    }
                }
            }
           

            this.lblMessage.Text = "打印完成！";
        }

        /// <summary>
        /// 打印qcs_file信息
        /// </summary>
        private void PrintDoc(string PageIndex,IMA_FILE imaModel,QCS_FILE qcsModel,PMC_FILE pmcModel,RVB_FILE rvbModel)
        {
            LabelManager2.ApplicationClass lbl = null;
            Document doc = null;
            try
            {
                lbl = new ApplicationClass();
                string  l_docname = @"\Lab\printdoc.Lab";  //打印模板
                lbl.Documents.Open(System.Windows.Forms.Application.StartupPath + l_docname, false);// 调用设计好的label文件

                doc = lbl.ActiveDocument;
                //int j = 3  ;//打印数量

                string ima100 = imaModel.IMA100;

                if (ima100.Equals("N"))
                {
                    ima100 += "    正常检验";
                }
                else if (ima100.Equals("T"))
                {
                    ima100 += "    加严检验";
                }
                else if (ima100.Equals("R"))
                {
                    ima100 += "    减量检验";
                }

                //for (int i = 0; i < j; i++)
                //{
                doc.Variables.FreeVariables.Item("Var_Page").Value = PageIndex;
                doc.Variables.FreeVariables.Item("Var_QCS01").Value = qcsModel.QCS01;
                doc.Variables.FreeVariables.Item("Var_QCS02").Value = qcsModel.QCS02.ToString();
                doc.Variables.FreeVariables.Item("Var_QCS021").Value = qcsModel.QCS021;
                doc.Variables.FreeVariables.Item("Var_IMA021").Value = imaModel.IMA021;
                doc.Variables.FreeVariables.Item("Var_IMA02").Value = imaModel.IMA02;
                doc.Variables.FreeVariables.Item("Var_IMA100").Value = ima100;
                doc.Variables.FreeVariables.Item("Var_PMC0103").Value = pmcModel.PMC01 + "    " + pmcModel.PMC03;
                doc.Variables.FreeVariables.Item("Var_QCS06").Value = qcsModel.QCS06.ToString() + "     " + imaModel.IMA25;
                doc.Variables.FreeVariables.Item("Var_QCSUD13").Value = qcsModel.QCSUD13 == null? "":((DateTime)qcsModel.QCSUD13).ToString("yyyy-MM-dd");  //入库日期
                //doc.Variables.FreeVariables.Item("Var_QCS04").Value = qcsModel.QCS04==null?"": ((DateTime)qcsModel.QCS04).ToString("yyyy-MM-dd");    //送检日期
                doc.Variables.FreeVariables.Item("Var_QCS04").Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");    //送检日期
                doc.Variables.FreeVariables.Item("Var_RVB04").Value = rvbModel.RVB04;
                doc.Variables.FreeVariables.Item("Var_RVBUD01").Value = rvbModel.RVBUD01 == null ? "" : rvbModel.RVBUD01;
                doc.Variables.FreeVariables.Item("Var_QCSUD05").Value = qcsModel.QCSUD05 == null ? "" : qcsModel.QCSUD05;

                doc.PrintLabel(1);//按lab文件中的设置打印标签，即几行几列等等，这个方法还有其它参数可以自己试
                //}
                doc.FormFeed();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                lbl.Documents.CloseAll();
                lbl.Quit();//退出
                lbl = null;
                doc = null;
                GC.Collect(0);
            }
        }


        /// <summary>
        /// 测试打印页，当检测到缺纸状态时，调用？
        /// </summary>
        private void PrintTest()
        {
            LabelManager2.ApplicationClass lbl = null;
            Document doc = null;
            try
            {
                lbl = new ApplicationClass();
                string l_docname = @"\Lab\printtest.Lab";  //打印模板
                lbl.Documents.Open(System.Windows.Forms.Application.StartupPath + l_docname, false);// 调用设计好的label文件

                doc = lbl.ActiveDocument;
                //int j = 3  ;//打印数量

                //for (int i = 0; i < j; i++)
                //{
                //doc.Variables.FreeVariables.Item("Var_Page").Value = PageIndex;
                //doc.Variables.FreeVariables.Item("Var_QCS01").Value = qcsModel.QCS01;
                //doc.Variables.FreeVariables.Item("Var_QCS02").Value = qcsModel.QCS02.ToString();
                //doc.Variables.FreeVariables.Item("Var_QCS021").Value = qcsModel.QCS021;
                //doc.Variables.FreeVariables.Item("Var_IMA021").Value = imaModel.IMA021;
                //doc.Variables.FreeVariables.Item("Var_IMA02").Value = imaModel.IMA02;
                //doc.Variables.FreeVariables.Item("Var_PMC0103").Value = pmcModel.PMC01 + "    " + pmcModel.PMC03;
                //doc.Variables.FreeVariables.Item("Var_QCS06").Value = qcsModel.QCS06.ToString() + "     " + imaModel.IMA25;
                //doc.Variables.FreeVariables.Item("Var_QCSUD13").Value = qcsModel.QCSUD13 == null ? "" : ((DateTime)qcsModel.QCSUD13).ToString("yyyy-MM-dd");  //入库日期
                //doc.Variables.FreeVariables.Item("Var_QCS04").Value = qcsModel.QCS04 == null ? "" : ((DateTime)qcsModel.QCS04).ToString("yyyy-MM-dd");    //送检日期
                //doc.Variables.FreeVariables.Item("Var_RVB04").Value = rvbModel.RVB04;
                //doc.Variables.FreeVariables.Item("Var_RVBUD01").Value = rvbModel.RVBUD01 == null ? "" : rvbModel.RVBUD01;

                doc.PrintLabel(1);//按lab文件中的设置打印标签，即几行几列等等，这个方法还有其它参数可以自己试
                //}
                doc.FormFeed();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                lbl.Documents.CloseAll();
                lbl.Quit();//退出
                lbl = null;
                doc = null;
                GC.Collect(0);
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            this.lblMessage.Text = DateTime.Now.ToShortTimeString();
            //时钟事件 
            try
            {
                //时间控件方法
                //this.lblClock.Text = DateTime.Now.ToShortTimeString();
                //排出周日
                if (DateTime.Now.DayOfWeek != DayOfWeek.Sunday)
                {
                    string ThisTime = DateTime.Now.ToShortTimeString();//得到现在的时间
                    if (isPrintTime(ThisTime))
                    {
                        this.lblMessage.Text = "进入自动打印程序！";
                        Print();//调用打印方法
                        this.lblMessage.Text = "打印完成";
                    }
                }
            }
            catch (Exception Ex)
            {
                this.lblMessage.Text = Ex.Message;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private bool isPrintTime(string time)
        {
            bool flag = false;
            string timerStart = ConfigurationManager.AppSettings["timerStart"];  //打印起始时间
            string timerend = ConfigurationManager.AppSettings["timerend"];  //打印截止时间
            string timer = ConfigurationManager.AppSettings["timer"]; //打印间隔
            string sendTime = DateTime.Parse(timerStart).ToString("HH:mm");

            while (DateTime.Parse(time) > DateTime.Parse(sendTime) )
            {
                sendTime = DateTime.Parse(sendTime).AddMinutes(double.Parse(timer)).ToString("HH:mm");
            }
            if (time == sendTime && DateTime.Parse(sendTime) > DateTime.Parse(timerStart) && DateTime.Parse(sendTime) < DateTime.Parse(timerend))
            {
                flag = true;
            }
            return flag;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //一分钟查看一次
            timer1.Interval = 60000;
            timer1.Start();
        }


        //手动触发，立即打印
        private void button2_Click(object sender, EventArgs e) 
        {
            this.lblMessage.Text = DateTime.Now.ToShortTimeString();
            //时钟事件 
            try
            {
                string ThisTime = DateTime.Now.ToShortTimeString();//得到现在的时间
                this.lblMessage.Text = "进入打印程序！";
                Print();//调用打印方法
                this.lblMessage.Text = "打印完成";
            }
            catch (Exception Ex)
            {
                this.lblMessage.Text = Ex.Message;
            }
        }
    } 
} 
