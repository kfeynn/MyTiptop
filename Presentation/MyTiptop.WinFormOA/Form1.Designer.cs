namespace MyTiptop.WinFormOA
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.txtSendTime = new System.Windows.Forms.TextBox();
            this.btnchangetime = new System.Windows.Forms.Button();
            this.lblClock = new System.Windows.Forms.Label();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(433, 41);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(135, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "send email";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(574, 46);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(65, 12);
            this.lblMessage.TabIndex = 1;
            this.lblMessage.Text = "lblMessage";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // txtSendTime
            // 
            this.txtSendTime.Location = new System.Drawing.Point(77, 43);
            this.txtSendTime.Name = "txtSendTime";
            this.txtSendTime.Size = new System.Drawing.Size(100, 21);
            this.txtSendTime.TabIndex = 2;
            // 
            // btnchangetime
            // 
            this.btnchangetime.Location = new System.Drawing.Point(208, 41);
            this.btnchangetime.Name = "btnchangetime";
            this.btnchangetime.Size = new System.Drawing.Size(107, 23);
            this.btnchangetime.TabIndex = 3;
            this.btnchangetime.Text = "定时发送时间";
            this.btnchangetime.UseVisualStyleBackColor = true;
            this.btnchangetime.Click += new System.EventHandler(this.btnchangetime_Click);
            // 
            // lblClock
            // 
            this.lblClock.AutoSize = true;
            this.lblClock.Location = new System.Drawing.Point(321, 46);
            this.lblClock.Name = "lblClock";
            this.lblClock.Size = new System.Drawing.Size(41, 12);
            this.lblClock.TabIndex = 4;
            this.lblClock.Text = "label1";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "邮件提醒";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(807, 378);
            this.Controls.Add(this.lblClock);
            this.Controls.Add(this.btnchangetime);
            this.Controls.Add(this.txtSendTime);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "自动提醒邮件发送器";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox txtSendTime;
        private System.Windows.Forms.Button btnchangetime;
        private System.Windows.Forms.Label lblClock;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
    }
}

