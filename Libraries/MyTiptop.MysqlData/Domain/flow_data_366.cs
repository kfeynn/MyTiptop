namespace MyTiptop.MysqlData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
 
    /// <summary>
    /// OA 开模申请单
    /// </summary>
    public partial class flow_data_366
    {
        public int id { get; set; }

        public int run_id { get; set; }
        [StringLength(200)]
        public string run_name { get; set; }
        [StringLength(20)]
        public string begin_user { get; set; }
        public DateTime begin_time { get; set; }
        public int flow_auto_num { get; set; }
        public int flow_auto_num_year { get; set; }
        public int flow_auto_num_month { get; set; }
        public string data_3 { get; set; }
        public string data_4 { get; set; }
        public string data_5 { get; set; }
        public string data_6 { get; set; }
        public string data_8 { get; set; }
        public string data_9 { get; set; }
        public string data_10 { get; set; }
        public string data_11 { get; set; }
        public string data_12 { get; set; }
        public string data_13 { get; set; }
        public string data_14 { get; set; }
        public string data_15 { get; set; }
        public string data_16 { get; set; }
        public string data_122 { get; set; }
        public string data_206 { get; set; }
        public string data_235 { get; set; }
        public string diff { get; set; }


        //data_6	data_206	data_11	data_12	 data_235	now -  data_235

        //图号data_6 模具编号data_206 供应商data_11 联系人（电话）data_12	 T0计划试模时间data_235   data_122 是实际试模时间

        //75	176251	压铸模具（开模）电子流（2019年11月04日18:52）沈晶金	GT25068	2019-11-04 18:52:37	1	5	0		沈晶金	2019-11-04	DKBA80240227			尹志华	2019-11-05	恒隆	钟玉婷13226788228
        //覃莫轩 压铸部-制造科 苏树仁	2019-11-26	1	2019-11-05			2019-12-06						交付需求 陈勇	2019-11-26		魏宏超	2019-11-05			同意 是           景双林	2019-11-06	王伟(通信）	2019-11-06		同意 DKBA80240227-GLDMP1121.rar* 38153@1911_1986730734,                  6万  2019-11-08						同意          211-S311911060003 dkba80240227.drw* dkba80240227.prt* 压铸模具(DKBA80240227_A)开模技术交流纪要_模板.docx* 新项目开模.msg*	37398@1911_789156561,37399@1911_693132803,37400@1911_135379416,37401@1911_1502569822,   99025BCR 模具-压铸铝-DKBA80240227.PRT-AISU-AISU屏蔽盖-130mm*80mm*15mm 周蕾	2019-11-05	MISC-AFA-80240227			GLD-0245	400T 简单




    }
}
