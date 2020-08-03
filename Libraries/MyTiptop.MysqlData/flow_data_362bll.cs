using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTiptop.MysqlData
{
    public partial class flow_data_362bll
    {
        /// <summary>
        /// 来访登记
        /// </summary>
        /// <param name="idNumber">证件号码</param>
        /// <param name="card_no">来访登记卡</param>
        /// <returns></returns>
        public static int In(string idNumber, string card_no)
        {
            int returnValue = 1;

            //1.更新362表
            flow_data_362dao.updateInTime(idNumber, card_no);

            //2.更新登记卡状态
            guest_carddao.updateFlag(card_no, 1);   // 0 未使用 ，1 正在使用 ，2 注销状态

            return returnValue;
            
        }

        /// <summary>
        /// 离厂登记（扫描来访卡）
        /// </summary>
        /// <param name="card_no"></param>
        public static int Out(string card_no)
        {
            int returnValue = 1;

            //1.更新362表
            flow_data_362dao.updateOutTime(card_no);

            //2.更新登记卡状态
            guest_carddao.updateFlag(card_no,0); // 0 未使用 ，1 正在使用 ，2 注销状态

            return returnValue;
        }


    }
}
