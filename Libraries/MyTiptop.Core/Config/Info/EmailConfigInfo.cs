using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTiptop.Core
{
    /// <summary>
    /// 邮件配置信息类
    /// </summary>
    [Serializable]
    public class EmailConfigInfo : IConfigInfo
    {
        private string _host;//服务器地址
        private int _port;//服务器端口
        private string _username;//邮箱账号
        private string _password;//邮箱密码
        private string _from;//发送邮箱
        private string _fromname;//发送邮箱的昵称
        private string _findpwdbody;//找回密码内容
        private string _scverifybody;//安全中心验证邮箱内容
        private string _scupdatebody;//安全中心确认更新邮箱内容
        private string _webcomebody;//注册欢迎信息

        private string _receiver1;  //邮件接收人员
        private string _receiver2;
        private string _receiver3;
        private string _receiver4;
        private string _receiver5;
        private string _receiver6;
        private string _receiver7;
        private string _receiver8;

        public string Receiver1
        {
            get { return _receiver1; }
            set { _receiver1 = value;}
        }

        public string Receiver2
        {
            get { return _receiver2; }
            set { _receiver2 = value; }
        }

        public string Receiver3
        {
            get { return _receiver3; }
            set { _receiver3 = value; }
        }

        public string Receiver4
        {
            get { return _receiver4; }
            set { _receiver4 = value; }
        }

        public string Receiver5
        {
            get { return _receiver5; }
            set { _receiver5 = value; }
        }
        public string Receiver6
        {
            get { return _receiver6; }
            set { _receiver6 = value; }
        }
        public string Receiver7
        {
            get { return _receiver7; }
            set { _receiver7 = value; }
        }
        public string Receiver8
        {
            get { return _receiver8; }
            set { _receiver8 = value; }
        }


        /// <summary>
        /// 服务器地址
        /// </summary>
        public string Host
        {
            get { return _host; }
            set { _host = value; }
        }

        /// <summary>
        /// 服务器端口
        /// </summary>
        public int Port
        {
            get { return _port; }
            set { _port = value; }
        }

        /// <summary>
        /// 邮箱账号
        /// </summary>
        public string UserName
        {
            get { return _username; }
            set { _username = value; }
        }

        /// <summary>
        /// 邮箱密码
        /// </summary>
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        /// <summary>
        /// 发送邮箱
        /// </summary>
        public string From
        {
            get { return _from; }
            set { _from = value; }
        }

        /// <summary>
        /// 发送邮箱的昵称
        /// </summary>
        public string FromName
        {
            get { return _fromname; }
            set { _fromname = value; }
        }

        /// <summary>
        /// 找回密码内容
        /// </summary>
        public string FindPwdBody
        {
            get { return _findpwdbody; }
            set { _findpwdbody = value; }
        }

        /// <summary>
        /// 安全中心验证邮箱内容
        /// </summary>
        public string SCVerifyBody
        {
            get { return _scverifybody; }
            set { _scverifybody = value; }
        }

        /// <summary>
        /// 安全中心确认更新邮箱内容
        /// </summary>
        public string SCUpdateBody
        {
            get { return _scupdatebody; }
            set { _scupdatebody = value; }
        }

        /// <summary>
        /// 注册欢迎信息
        /// </summary>
        public string WebcomeBody
        {
            get { return _webcomebody; }
            set { _webcomebody = value; }
        }
    }
}
