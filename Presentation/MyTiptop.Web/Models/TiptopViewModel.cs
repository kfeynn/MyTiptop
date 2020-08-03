using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using MyTiptop.Web.Models;
using MyTiptop.Web.Framework;
using MyTiptop.Core;
using MyTiptop.Data;
using MyTiptop.Services;
using System.Data;
using MyTiptop.MysqlData;

namespace MyTiptop.Web.Models
{
    public class Bsfp625ViewModel
    {
        /// <summary>
        /// 入库单号
        /// </summary>
        public string requisition { get; set; }
        /// <summary>
        /// 操作人员 
        /// </summary>
        public string employee { get; set; }
        /// <summary>
        /// 包装票号
        /// </summary>
        public string packno { get; set; }
        /// <summary>
        /// 存储位置
        /// </summary>
        public string position { get; set; }

        public string message { get; set; }

        public DataTable List { get; set; }

    }

    public class ImgsSaveViewModel
    {
        /// <summary>
        /// 出过单号
        /// </summary>
        public string axmtcode { get; set; }
        /// <summary>
        /// 包装票号
        /// </summary>
        public string barcode { get; set; }

        /// <summary>
        /// 提示消息
        /// </summary>
        public string message { get; set; }
    }

    public class RegistrationViewModel
    {
        public string idNumber { get; set; }

        // 
        public string idNumberBak { get; set; }

        public string message { get; set; }

        public flow_data_362 model { get; set; }
         
    }

    /// <summary> 
    /// 投诉
    /// </summary> 
    public class ComplaintViewModel 
    { 
        //通知信息 
        public string Message { get; set; }

        public string UserName { get; set; }

        public string UserCode { get; set; }

        public string Phone { get; set; }

        public string Adress { get; set; }


    }

    //
    public class ComplaintShowViewModel
    {
        //通知信息 
        public string Message { get; set; }

        public string flag { get; set; }

        public string type { get; set; }

        public QA_Complaint m { get; set; }

        public List<QA_Complaint_annex> annexList { get; set; }

    }

}
