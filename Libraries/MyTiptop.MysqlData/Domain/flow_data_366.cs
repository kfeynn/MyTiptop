namespace MyTiptop.MysqlData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
 
    /// <summary>
    /// OA ��ģ���뵥
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

        //ͼ��data_6 ģ�߱��data_206 ��Ӧ��data_11 ��ϵ�ˣ��绰��data_12	 T0�ƻ���ģʱ��data_235   data_122 ��ʵ����ģʱ��

        //75	176251	ѹ��ģ�ߣ���ģ����������2019��11��04��18:52���򾧽�	GT25068	2019-11-04 18:52:37	1	5	0		�򾧽�	2019-11-04	DKBA80240227			��־��	2019-11-05	��¡	������13226788228
        //��Ī�� ѹ����-����� ������	2019-11-26	1	2019-11-05			2019-12-06						�������� ����	2019-11-26		κ�곬	2019-11-05			ͬ�� ��           ��˫��	2019-11-06	��ΰ(ͨ�ţ�	2019-11-06		ͬ�� DKBA80240227-GLDMP1121.rar* 38153@1911_1986730734,                  6��  2019-11-08						ͬ��          211-S311911060003 dkba80240227.drw* dkba80240227.prt* ѹ��ģ��(DKBA80240227_A)��ģ����������Ҫ_ģ��.docx* ����Ŀ��ģ.msg*	37398@1911_789156561,37399@1911_693132803,37400@1911_135379416,37401@1911_1502569822,   99025BCR ģ��-ѹ����-DKBA80240227.PRT-AISU-AISU���θ�-130mm*80mm*15mm ����	2019-11-05	MISC-AFA-80240227			GLD-0245	400T ��




    }
}
