namespace MyTiptop.OraCore
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using MyTiptop.Web.Framework;
    using MyTiptop.Core;

    public partial class OraDBContext : DbContext
    {
        //条码打印程序不需要配置。为什么这个需要 ？
        // https://www.cnblogs.com/wendj/archive/2017/11/27/7905735.html
        //C:\Windows\Microsoft.NET\Framework\v4.0.30319\Config
        //C:\Windows\Microsoft.NET\Framework64\v4.0.30319\Config

        //当前数据中心
        private static string _dbs;

        //默认构造函数
        //public OraDBContext() : base("name=s10") { }

        //添加构造函数，用于传入 "name=DataBaseConStr" 选择数据库链接。可用于多个数据 但结构相同。可实现数据库之间切换。
        public OraDBContext(string dbs) : base(CtorExt2(dbs) ) { }

        private static string CtorExt2(string dbs)
        {
            _dbs = dbs;

            string connStr = "name=" + _dbs;
            return connStr;
        }

        //以下写法可以省去默认构造函数的传参步骤 
        private static string CtorExt()
        {
            //由此传入全局变量参数实例化数据库。 // globals.g_dbs 全局变量 ，营运中心            
            WebWorkContext WorkContext = new WebWorkContext();
            _dbs = WorkContext.MallConfig.Dbs;

            #region
            // 将 s31、s32 等写法指定到 s30   WebWorkContext                                    
            //_dbs = WorkContext.MallConfig.Plant.Substring(0, 2) + "0";    // 将 s31、s32 等写法指定到 s30   WebWorkContext
            //_dbs =  "S30"; // 将 s31、s32 等写法指定到 s30  
            #endregion

            string connStr = "name=" + _dbs; 
            return connStr; 
        }
        public OraDBContext() : base(CtorExt()) { }

        public virtual DbSet<TC_SFB_FILE> TC_SFB_FILE { get; set; }
        public virtual DbSet<TC_SFC_FILE> TC_SFC_FILE { get; set; }
        public virtual DbSet<TC_SFD_FILE> TC_SFD_FILE { get; set; }
        public virtual DbSet<TC_SHB_FILE> TC_SHB_FILE { get; set; }
        public virtual DbSet<ZO_FILE> ZO_FILE { get; set; }
        public virtual DbSet<ZXV_FILE> ZXV_FILE { get; set; }
        public virtual DbSet<ZYW_FILE> ZYW_FILE { get; set; }
        public virtual DbSet<ZZD_FILE> ZZD_FILE { get; set; }
        public virtual DbSet<IMGS_FILE> IMGS_FILE { get; set; }
        public virtual DbSet<TC_BRB_FILE> TC_BRB_FILE { get; set; }
        public virtual DbSet<TC_BRD_FILE> TC_BRD_FILE { get; set; }
        public virtual DbSet<TC_BRG_FILE> TC_BRG_FILE { get; set; }
        public virtual DbSet<TC_BRS_FILE> TC_BRS_FILE { get; set; }
        public virtual DbSet<IMD_FILE> IMD_FILE { get; set; }
        public virtual DbSet<TC_SFL_FILE> TC_SFL_FILE { get; set; }
        public virtual DbSet<TC_SFM_FILE> TC_SFM_FILE { get; set; }
        public virtual DbSet<TC_SFN_FILE> TC_SFN_FILE { get; set; }
        public virtual DbSet<BMB_FILE> BMB_FILE { get; set; }
        public virtual DbSet<IMA_FILE> IMA_FILE { get; set; }
        public virtual DbSet<OBK_FILE> OBK_FILE { get; set; }

        public virtual DbSet<SFU_FILE> SFU_FILE { get; set; }
        public virtual DbSet<SFV_FILE> SFV_FILE { get; set; }
        public virtual DbSet<TC_BRE_FILE> TC_BRE_FILE { get; set; }
        public virtual DbSet<TC_XXU_FILE> TC_XXU_FILE { get; set; }
        public virtual DbSet<TC_IME_FILE> TC_IME_FILE { get; set; }

        public virtual DbSet<TC_QCX_FILE> TC_QCX_FILE { get; set; }
        public virtual DbSet<TC_QCXX_FILE> TC_QCXX_FILE { get; set; }
        public virtual DbSet<TC_QCY_FILE> TC_QCY_FILE { get; set; }
        public virtual DbSet<TC_QCZ_FILE> TC_QCZ_FILE { get; set; }

        public virtual DbSet<TC_QCYY_FILE> TC_QCYY_FILE { get; set; }
        public virtual DbSet<TC_QCZZ_FILE> TC_QCZZ_FILE { get; set; }


        public virtual DbSet<RVBS_FILE> RVBS_FILE { get; set; }
        public virtual DbSet<QCS_FILE> QCS_FILE { get; set; }
        public virtual DbSet<PMC_FILE> PMC_FILE { get; set; }

        public virtual DbSet<RVB_FILE> RVB_FILE { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //指定当前Schema 或者说 用户  但还是做不到切换 用户。
            modelBuilder.HasDefaultSchema(_dbs.ToUpper());


            modelBuilder.Entity<TC_SFB_FILE>()
                .Property(e => e.TC_SFB01)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFB_FILE>()
                .Property(e => e.TC_SFB02)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFB_FILE>()
                .Property(e => e.TC_SFB03)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFB_FILE>()
                .Property(e => e.TC_SFB04)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFB_FILE>()
                .Property(e => e.TC_SFB05)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFB_FILE>()
                .Property(e => e.TC_SFB06)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFB_FILE>()
                .Property(e => e.TC_SFB07)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFB_FILE>()
                .Property(e => e.TC_SFB08)
                .HasPrecision(15, 3);

            modelBuilder.Entity<TC_SFB_FILE>()
                .Property(e => e.TC_SFB09)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFB_FILE>()
                .Property(e => e.TC_SFB10)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFB_FILE>()
                .Property(e => e.TC_SFB12)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFB_FILE>()
                .Property(e => e.TC_SFB13)
                .HasPrecision(15, 3);

            modelBuilder.Entity<TC_SFB_FILE>()
                .Property(e => e.TC_SFB14)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFB_FILE>()
                .Property(e => e.TC_SFB16)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFB_FILE>()
                .Property(e => e.TC_SFBUSER)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFB_FILE>()
                .Property(e => e.TC_SFBGRUP)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFB_FILE>()
                .Property(e => e.TC_SFBMODU)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFB_FILE>()
                .Property(e => e.TC_SFBUD01)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFB_FILE>()
                .Property(e => e.TC_SFBUD02)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFB_FILE>()
                .Property(e => e.TC_SFBUD03)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFB_FILE>()
                .Property(e => e.TC_SFBUD04)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFB_FILE>()
                .Property(e => e.TC_SFBUD05)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFB_FILE>()
                .Property(e => e.TC_SFBUD06)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFB_FILE>()
                .Property(e => e.TC_SFBUD07)
                .HasPrecision(15, 3);

            modelBuilder.Entity<TC_SFB_FILE>()
                .Property(e => e.TC_SFBUD08)
                .HasPrecision(15, 3);

            modelBuilder.Entity<TC_SFB_FILE>()
                .Property(e => e.TC_SFBUD09)
                .HasPrecision(15, 3);

            modelBuilder.Entity<TC_SFC_FILE>()
                .Property(e => e.TC_SFC01)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFC_FILE>()
                .Property(e => e.TC_SFC02)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFC_FILE>()
                .Property(e => e.TC_SFC03)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFC_FILE>()
                .Property(e => e.TC_SFC05)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFC_FILE>()
                .Property(e => e.TC_SFC06)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFC_FILE>()
                .Property(e => e.TC_SFC07)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFC_FILE>()
                .Property(e => e.TC_SFC08)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFC_FILE>()
                .Property(e => e.TC_SFC09)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFC_FILE>()
                .Property(e => e.TC_SFC11)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFC_FILE>()
                .Property(e => e.TC_SFC12)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFC_FILE>()
                .Property(e => e.TC_SFCACTI)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFC_FILE>()
                .Property(e => e.TC_SFCUSER)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFC_FILE>()
                .Property(e => e.TC_SFCGRUP)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFC_FILE>()
                .Property(e => e.TC_SFCMODU)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFC_FILE>()
                .Property(e => e.TC_SFCUD01)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFC_FILE>()
                .Property(e => e.TC_SFCUD02)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFC_FILE>()
                .Property(e => e.TC_SFCUD03)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFC_FILE>()
                .Property(e => e.TC_SFCUD04)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFC_FILE>()
                .Property(e => e.TC_SFCUD05)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFC_FILE>()
                .Property(e => e.TC_SFCUD06)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFC_FILE>()
                .Property(e => e.TC_SFCUD07)
                .HasPrecision(15, 3);

            modelBuilder.Entity<TC_SFC_FILE>()
                .Property(e => e.TC_SFCUD08)
                .HasPrecision(15, 3);

            modelBuilder.Entity<TC_SFC_FILE>()
                .Property(e => e.TC_SFCUD09)
                .HasPrecision(15, 3);

            modelBuilder.Entity<TC_SFC_FILE>()
                .Property(e => e.TC_SFCORIU)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFC_FILE>()
                .Property(e => e.TC_SFCORIG)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFD_FILE>()
                .Property(e => e.TC_SFD01)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFD_FILE>()
                .Property(e => e.TC_SFD03)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFD_FILE>()
                .Property(e => e.TC_SFD05)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFD_FILE>()
                .Property(e => e.TC_SFD06)
                .HasPrecision(15, 3);

            modelBuilder.Entity<TC_SFD_FILE>()
                .Property(e => e.TC_SFD07)
                .HasPrecision(20, 6);

            modelBuilder.Entity<TC_SFD_FILE>()
                .Property(e => e.TC_SFD09)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFD_FILE>()
                .Property(e => e.TC_SFDUD01)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFD_FILE>()
                .Property(e => e.TC_SFDUD02)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFD_FILE>()
                .Property(e => e.TC_SFDUD03)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFD_FILE>()
                .Property(e => e.TC_SFDUD04)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFD_FILE>()
                .Property(e => e.TC_SFDUD05)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFD_FILE>()
                .Property(e => e.TC_SFDUD06)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFD_FILE>()
                .Property(e => e.TC_SFDUD07)
                .HasPrecision(15, 3);

            modelBuilder.Entity<TC_SFD_FILE>()
                .Property(e => e.TC_SFDUD08)
                .HasPrecision(15, 3);

            modelBuilder.Entity<TC_SFD_FILE>()
                .Property(e => e.TC_SFDUD09)
                .HasPrecision(15, 3);

            modelBuilder.Entity<TC_SFD_FILE>()
                .Property(e => e.TC_SFDORIU)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFD_FILE>()
                .Property(e => e.TC_SFDORIG)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SHB_FILE>()
                .Property(e => e.TC_SHB01)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SHB_FILE>()
                .Property(e => e.TC_SHB02)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SHB_FILE>()
                .Property(e => e.TC_SHB04)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SHB_FILE>()
                .Property(e => e.TC_SHB06)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SHB_FILE>()
                .Property(e => e.TC_SHB07)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SHB_FILE>()
                .Property(e => e.TC_SHBPLANT)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SHB_FILE>()
                .Property(e => e.TC_SHBLEGAL)
                .IsUnicode(false);

            modelBuilder.Entity<ZO_FILE>()
                .Property(e => e.ZO01)
                .IsUnicode(false);

            modelBuilder.Entity<ZO_FILE>()
                .Property(e => e.ZO02)
                .IsUnicode(false);

            modelBuilder.Entity<ZO_FILE>()
                .Property(e => e.ZO041)
                .IsUnicode(false);

            modelBuilder.Entity<ZO_FILE>()
                .Property(e => e.ZO042)
                .IsUnicode(false);

            modelBuilder.Entity<ZO_FILE>()
                .Property(e => e.ZO05)
                .IsUnicode(false);

            modelBuilder.Entity<ZO_FILE>()
                .Property(e => e.ZO06)
                .IsUnicode(false);

            modelBuilder.Entity<ZO_FILE>()
                .Property(e => e.ZO07)
                .IsUnicode(false);

            modelBuilder.Entity<ZO_FILE>()
                .Property(e => e.ZO08)
                .IsUnicode(false);

            modelBuilder.Entity<ZO_FILE>()
                .Property(e => e.ZO09)
                .IsUnicode(false);

            modelBuilder.Entity<ZO_FILE>()
                .Property(e => e.ZO10)
                .IsUnicode(false);

            modelBuilder.Entity<ZO_FILE>()
                .Property(e => e.ZO11)
                .IsUnicode(false);

            modelBuilder.Entity<ZO_FILE>()
                .Property(e => e.ZO12)
                .IsUnicode(false);

            modelBuilder.Entity<ZO_FILE>()
                .Property(e => e.ZO13)
                .IsUnicode(false);

            modelBuilder.Entity<ZO_FILE>()
                .Property(e => e.ZO14)
                .IsUnicode(false);

            modelBuilder.Entity<ZO_FILE>()
                .Property(e => e.ZO15)
                .IsUnicode(false);

            modelBuilder.Entity<ZO_FILE>()
                .Property(e => e.ZOUSER)
                .IsUnicode(false);

            modelBuilder.Entity<ZO_FILE>()
                .Property(e => e.ZOGRUP)
                .IsUnicode(false);

            modelBuilder.Entity<ZO_FILE>()
                .Property(e => e.ZOMODU)
                .IsUnicode(false);

            modelBuilder.Entity<ZO_FILE>()
                .Property(e => e.ZOORIU)
                .IsUnicode(false);

            modelBuilder.Entity<ZO_FILE>()
                .Property(e => e.ZOORIG)
                .IsUnicode(false);

            modelBuilder.Entity<ZO_FILE>()
                .Property(e => e.ZO16)
                .IsUnicode(false);

            modelBuilder.Entity<ZXV_FILE>()
                .Property(e => e.ZXV01)
                .IsUnicode(false);

            modelBuilder.Entity<ZXV_FILE>()
                .Property(e => e.ZXV02)
                .IsUnicode(false);

            modelBuilder.Entity<ZXV_FILE>()
                .Property(e => e.ZXV05)
                .IsUnicode(false);

            modelBuilder.Entity<ZYW_FILE>()
                .Property(e => e.ZYW01)
                .IsUnicode(false);

            modelBuilder.Entity<ZYW_FILE>()
                .Property(e => e.ZYW02)
                .IsUnicode(false);

            modelBuilder.Entity<ZYW_FILE>()
                .Property(e => e.ZYW03)
                .IsUnicode(false);

            modelBuilder.Entity<ZYW_FILE>()
                .Property(e => e.ZYW04)
                .IsUnicode(false);

            modelBuilder.Entity<ZYW_FILE>()
                .Property(e => e.ZYW05)
                .IsUnicode(false);

            modelBuilder.Entity<ZYW_FILE>()
                .Property(e => e.ZYW06)
                .IsUnicode(false);

            modelBuilder.Entity<ZYW_FILE>()
                .Property(e => e.ZYW07)
                .IsUnicode(false);

            modelBuilder.Entity<ZYW_FILE>()
                .Property(e => e.ZYW08)
                .IsUnicode(false);

            modelBuilder.Entity<ZYW_FILE>()
                .Property(e => e.ZYWACTI)
                .IsUnicode(false);

            modelBuilder.Entity<ZYW_FILE>()
                .Property(e => e.ZYWUSER)
                .IsUnicode(false);

            modelBuilder.Entity<ZYW_FILE>()
                .Property(e => e.ZYWGRUP)
                .IsUnicode(false);

            modelBuilder.Entity<ZYW_FILE>()
                .Property(e => e.ZYWMODU)
                .IsUnicode(false);

            modelBuilder.Entity<ZYW_FILE>()
                .Property(e => e.ZYWORIG)
                .IsUnicode(false);

            modelBuilder.Entity<ZYW_FILE>()
                .Property(e => e.ZYWORIU)
                .IsUnicode(false);

            modelBuilder.Entity<ZZD_FILE>()
                .Property(e => e.ZZD01)
                .IsUnicode(false);

            modelBuilder.Entity<ZZD_FILE>()
                .Property(e => e.ZZD02)
                .IsUnicode(false);

            modelBuilder.Entity<ZZD_FILE>()
                .Property(e => e.ZZDACTI)
                .IsUnicode(false);

            modelBuilder.Entity<IMGS_FILE>()
      .Property(e => e.IMGS01)
      .IsUnicode(false);

            modelBuilder.Entity<IMGS_FILE>()
                .Property(e => e.IMGS02)
                .IsUnicode(false);

            modelBuilder.Entity<IMGS_FILE>()
                .Property(e => e.IMGS03)
                .IsUnicode(false);

            modelBuilder.Entity<IMGS_FILE>()
                .Property(e => e.IMGS04)
                .IsUnicode(false);

            modelBuilder.Entity<IMGS_FILE>()
                .Property(e => e.IMGS05)
                .IsUnicode(false);

            modelBuilder.Entity<IMGS_FILE>()
                .Property(e => e.IMGS06)
                .IsUnicode(false);

            modelBuilder.Entity<IMGS_FILE>()
                .Property(e => e.IMGS07)
                .IsUnicode(false);

            modelBuilder.Entity<IMGS_FILE>()
                .Property(e => e.IMGS08)
                .HasPrecision(15, 3);

            modelBuilder.Entity<IMGS_FILE>()
                .Property(e => e.IMGS10)
                .IsUnicode(false);

            modelBuilder.Entity<IMGS_FILE>()
                .Property(e => e.IMGS11)
                .IsUnicode(false);

            modelBuilder.Entity<IMGS_FILE>()
                .Property(e => e.IMGSPLANT)
                .IsUnicode(false);

            modelBuilder.Entity<IMGS_FILE>()
                .Property(e => e.IMGSLEGAL)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRB_FILE>()
                .Property(e => e.TC_BRB01)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRB_FILE>()
                .Property(e => e.TC_BRB02)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRB_FILE>()
                .Property(e => e.TC_BRB03)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRB_FILE>()
                .Property(e => e.TC_BRB06)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRB_FILE>()
                .Property(e => e.TC_BRB07)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRB_FILE>()
                .Property(e => e.TC_BRB09)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRB_FILE>()
                .Property(e => e.TC_BRB10)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRB_FILE>()
                .Property(e => e.TC_BRB11)
                .HasPrecision(15, 3);

            modelBuilder.Entity<TC_BRB_FILE>()
                .Property(e => e.TC_BRB12)
                .HasPrecision(15, 3);

            modelBuilder.Entity<TC_BRB_FILE>()
                .Property(e => e.TC_BRB13)
                .HasPrecision(15, 3);

            modelBuilder.Entity<TC_BRB_FILE>()
                .Property(e => e.TC_BRB14)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRB_FILE>()
                .Property(e => e.TC_BRB15)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRB_FILE>()
                .Property(e => e.TC_BRB18)
                .HasPrecision(15, 3);

            modelBuilder.Entity<TC_BRB_FILE>()
                .Property(e => e.TC_BRB19)
                .HasPrecision(15, 3);

            modelBuilder.Entity<TC_BRB_FILE>()
                .Property(e => e.TC_BRB20)
                .HasPrecision(15, 3);

            modelBuilder.Entity<TC_BRB_FILE>()
                .Property(e => e.TC_BRB21)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRB_FILE>()
                .Property(e => e.TC_BRB22)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRB_FILE>()
                .Property(e => e.TC_BRB23)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRB_FILE>()
                .Property(e => e.TC_BRB24)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRB_FILE>()
                .Property(e => e.TC_BRB25)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRB_FILE>()
                .Property(e => e.TC_BRBPLANT)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRB_FILE>()
                .Property(e => e.TC_BRBLEGAL)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRD_FILE>()
                .Property(e => e.TC_BRD01)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRD_FILE>()
                .Property(e => e.TC_BRD02)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRD_FILE>()
                .Property(e => e.TC_BRD03)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRD_FILE>()
                .Property(e => e.TC_BRD04)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRD_FILE>()
                .Property(e => e.TC_BRD05)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRD_FILE>()
                .Property(e => e.TC_BRD06)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRD_FILE>()
                .Property(e => e.TC_BRD07)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRD_FILE>()
                .Property(e => e.TC_BRD09)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRD_FILE>()
                .Property(e => e.TC_BRD10)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRD_FILE>()
                .Property(e => e.TC_BRD11)
                .HasPrecision(15, 3);

            modelBuilder.Entity<TC_BRD_FILE>()
                .Property(e => e.TC_BRD12)
                .HasPrecision(15, 3);

            modelBuilder.Entity<TC_BRD_FILE>()
                .Property(e => e.TC_BRD13)
                .HasPrecision(15, 3);

            modelBuilder.Entity<TC_BRD_FILE>()
                .Property(e => e.TC_BRD14)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRD_FILE>()
                .Property(e => e.TC_BRD15)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRD_FILE>()
                .Property(e => e.TC_BRD18)
                .HasPrecision(15, 3);

            modelBuilder.Entity<TC_BRD_FILE>()
                .Property(e => e.TC_BRD19)
                .HasPrecision(15, 3);

            modelBuilder.Entity<TC_BRD_FILE>()
                .Property(e => e.TC_BRD20)
                .HasPrecision(15, 3);

            modelBuilder.Entity<TC_BRD_FILE>()
                .Property(e => e.TC_BRD21)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRD_FILE>()
                .Property(e => e.TC_BRD22)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRD_FILE>()
                .Property(e => e.TC_BRD23)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRD_FILE>()
                .Property(e => e.TC_BRD24)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRD_FILE>()
                .Property(e => e.TC_BRDACTI)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRD_FILE>()
                .Property(e => e.TC_BRDPLANT)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRD_FILE>()
                .Property(e => e.TC_BRDLEGAL)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRG_FILE>()
                .Property(e => e.TC_BRG01)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRG_FILE>()
                .Property(e => e.TC_BRG02)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRG_FILE>()
                .Property(e => e.TC_BRG03)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRG_FILE>()
                .Property(e => e.TC_BRG05)
                .HasPrecision(15, 6);

            modelBuilder.Entity<TC_BRG_FILE>()
                .Property(e => e.TC_BRG06)
                .HasPrecision(15, 6);

            modelBuilder.Entity<TC_BRG_FILE>()
                .Property(e => e.TC_BRG07)
                .HasPrecision(15, 6);

            modelBuilder.Entity<TC_BRG_FILE>()
                .Property(e => e.TC_BRG08)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRG_FILE>()
                .Property(e => e.TC_BRG09)
                .HasPrecision(15, 3);

            modelBuilder.Entity<TC_BRG_FILE>()
                .Property(e => e.TC_BRG10)
                .HasPrecision(15, 3);

            modelBuilder.Entity<TC_BRG_FILE>()
                .Property(e => e.TC_BRG11)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRG_FILE>()
                .Property(e => e.TC_BRG12)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRG_FILE>()
                .Property(e => e.TC_BRG13)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRG_FILE>()
                .Property(e => e.TC_BRG14)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRG_FILE>()
                .Property(e => e.TC_BRG15)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRG_FILE>()
                .Property(e => e.TC_BRG16)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRG_FILE>()
                .Property(e => e.TC_BRG17)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRG_FILE>()
                .Property(e => e.TC_BRG20)
                .HasPrecision(15, 3);

            modelBuilder.Entity<TC_BRG_FILE>()
                .Property(e => e.TC_BRG21)
                .HasPrecision(15, 3);

            modelBuilder.Entity<TC_BRG_FILE>()
                .Property(e => e.TC_BRG22)
                .HasPrecision(15, 3);

            modelBuilder.Entity<TC_BRG_FILE>()
                .Property(e => e.TC_BRGPLANT)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRG_FILE>()
                .Property(e => e.TC_BRGLEGAL)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRG_FILE>()
                .Property(e => e.TC_BRG23)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRG_FILE>()
                .Property(e => e.TC_BRG24)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRG_FILE>()
                .Property(e => e.TC_BRG25)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRG_FILE>()
                .Property(e => e.TC_BRG26)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRG_FILE>()
                .Property(e => e.TC_BRG27)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRG_FILE>()
                .Property(e => e.TC_BRG28)
                .HasPrecision(15, 3);

            modelBuilder.Entity<TC_BRG_FILE>()
                .Property(e => e.TC_BRG29)
                .HasPrecision(15, 3);

            modelBuilder.Entity<TC_BRG_FILE>()
                .Property(e => e.TC_BRGNO)
                .HasPrecision(15, 3);

            modelBuilder.Entity<TC_BRS_FILE>()
                .Property(e => e.TC_BRS01)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRS_FILE>()
                .Property(e => e.TC_BRS02)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRS_FILE>()
                .Property(e => e.TC_BRS03)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRS_FILE>()
                .Property(e => e.TC_BRS05)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRS_FILE>()
                .Property(e => e.TC_BRS06)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRS_FILE>()
                .Property(e => e.TC_BRS07)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRS_FILE>()
                .Property(e => e.TC_BRS08)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRS_FILE>()
                .Property(e => e.TC_BRS09)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRS_FILE>()
                .Property(e => e.TC_BRS10)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRS_FILE>()
                .Property(e => e.TC_BRS11)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRS_FILE>()
                .Property(e => e.TC_BRS12)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRS_FILE>()
                .Property(e => e.TC_BRS14)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRS_FILE>()
                .Property(e => e.TC_BRS15)
                .IsUnicode(false);

            modelBuilder.Entity<IMD_FILE>()
                .Property(e => e.IMD01)
                .IsUnicode(false);

            modelBuilder.Entity<IMD_FILE>()
                .Property(e => e.IMD02)
                .IsUnicode(false);

            modelBuilder.Entity<IMD_FILE>()
                .Property(e => e.IMD03)
                .IsUnicode(false);

            modelBuilder.Entity<IMD_FILE>()
                .Property(e => e.IMD04)
                .IsUnicode(false);

            modelBuilder.Entity<IMD_FILE>()
                .Property(e => e.IMD05)
                .IsUnicode(false);

            modelBuilder.Entity<IMD_FILE>()
                .Property(e => e.IMD06)
                .IsUnicode(false);

            modelBuilder.Entity<IMD_FILE>()
                .Property(e => e.IMD07)
                .IsUnicode(false);

            modelBuilder.Entity<IMD_FILE>()
                .Property(e => e.IMD08)
                .IsUnicode(false);

            modelBuilder.Entity<IMD_FILE>()
                .Property(e => e.IMD09)
                .IsUnicode(false);

            modelBuilder.Entity<IMD_FILE>()
                .Property(e => e.IMD10)
                .IsUnicode(false);

            modelBuilder.Entity<IMD_FILE>()
                .Property(e => e.IMD11)
                .IsUnicode(false);

            modelBuilder.Entity<IMD_FILE>()
                .Property(e => e.IMD12)
                .IsUnicode(false);

            modelBuilder.Entity<IMD_FILE>()
                .Property(e => e.IMD13)
                .IsUnicode(false);

            modelBuilder.Entity<IMD_FILE>()
                .Property(e => e.IMDACTI)
                .IsUnicode(false);

            modelBuilder.Entity<IMD_FILE>()
                .Property(e => e.IMDUSER)
                .IsUnicode(false);

            modelBuilder.Entity<IMD_FILE>()
                .Property(e => e.IMDGRUP)
                .IsUnicode(false);

            modelBuilder.Entity<IMD_FILE>()
                .Property(e => e.IMDMODU)
                .IsUnicode(false);

            modelBuilder.Entity<IMD_FILE>()
                .Property(e => e.IMD16)
                .IsUnicode(false);

            modelBuilder.Entity<IMD_FILE>()
                .Property(e => e.IMD081)
                .IsUnicode(false);

            modelBuilder.Entity<IMD_FILE>()
                .Property(e => e.IMD17)
                .IsUnicode(false);

            modelBuilder.Entity<IMD_FILE>()
                .Property(e => e.IMD18)
                .IsUnicode(false);

            modelBuilder.Entity<IMD_FILE>()
                .Property(e => e.IMD19)
                .IsUnicode(false);

            modelBuilder.Entity<IMD_FILE>()
                .Property(e => e.IMD20)
                .IsUnicode(false);

            modelBuilder.Entity<IMD_FILE>()
                .Property(e => e.IMD21)
                .IsUnicode(false);

            modelBuilder.Entity<IMD_FILE>()
                .Property(e => e.IMD211)
                .IsUnicode(false);

            modelBuilder.Entity<IMD_FILE>()
                .Property(e => e.IMDPOS)
                .IsUnicode(false);

            modelBuilder.Entity<IMD_FILE>()
                .Property(e => e.IMDORIU)
                .IsUnicode(false);

            modelBuilder.Entity<IMD_FILE>()
                .Property(e => e.IMDORIG)
                .IsUnicode(false);

            modelBuilder.Entity<IMD_FILE>()
                .Property(e => e.IMD22)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFL_FILE>()
                 .Property(e => e.TC_SFL01)
                 .IsUnicode(false);

            modelBuilder.Entity<TC_SFL_FILE>()
                .Property(e => e.TC_SFL02)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFL_FILE>()
                .Property(e => e.TC_SFL03)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFL_FILE>()
                .Property(e => e.TC_SFL04)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFL_FILE>()
                .Property(e => e.TC_SFL05)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFL_FILE>()
                .Property(e => e.TC_SFL06)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFL_FILE>()
                .Property(e => e.TC_SFL07)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFL_FILE>()
                .Property(e => e.TC_SFL08)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFL_FILE>()
                .Property(e => e.TC_SFL09)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFL_FILE>()
                .Property(e => e.TC_SFLPLANT)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFL_FILE>()
                .Property(e => e.TC_SFLLEGAL)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFM_FILE>()
                .Property(e => e.TC_SFM01)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFM_FILE>()
                .Property(e => e.TC_SFM04)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFM_FILE>()
                .Property(e => e.TC_SFM06)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFM_FILE>()
                .Property(e => e.TC_SFM07)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFM_FILE>()
                .Property(e => e.TC_SFM08)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFM_FILE>()
                .Property(e => e.TC_SFM09)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFM_FILE>()
                .Property(e => e.TC_SFMPLANT)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFM_FILE>()
                .Property(e => e.TC_SFMLEGAL)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFM_FILE>()
                .Property(e => e.TC_SFMCONF)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFN_FILE>()
                .Property(e => e.TC_SFN01)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFN_FILE>()
                .Property(e => e.TC_SFN03)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFN_FILE>()
                .Property(e => e.TC_SFN04)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFN_FILE>()
                .Property(e => e.TC_SFN05)
                .HasPrecision(15, 3);

            modelBuilder.Entity<TC_SFN_FILE>()
                .Property(e => e.TC_SFN06)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFN_FILE>()
                .Property(e => e.TC_SFN07)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFN_FILE>()
                .Property(e => e.TC_SFN08)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFN_FILE>()
                .Property(e => e.TC_SFN09)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFN_FILE>()
                .Property(e => e.TC_SFNPLANT)
                .IsUnicode(false);

            modelBuilder.Entity<TC_SFN_FILE>()
                .Property(e => e.TC_SFNLEGAL)
                .IsUnicode(false);

            modelBuilder.Entity<BMB_FILE>()
                .Property(e => e.BMB01)
                .IsUnicode(false);

            modelBuilder.Entity<BMB_FILE>()
                .Property(e => e.BMB03)
                .IsUnicode(false);

            modelBuilder.Entity<BMB_FILE>()
                .Property(e => e.BMB06)
                .HasPrecision(16, 8);

            modelBuilder.Entity<BMB_FILE>()
                .Property(e => e.BMB07)
                .HasPrecision(16, 8);

            modelBuilder.Entity<BMB_FILE>()
                .Property(e => e.BMB08)
                .HasPrecision(9, 4);

            modelBuilder.Entity<BMB_FILE>()
                .Property(e => e.BMB09)
                .IsUnicode(false);

            modelBuilder.Entity<BMB_FILE>()
                .Property(e => e.BMB10)
                .IsUnicode(false);

            modelBuilder.Entity<BMB_FILE>()
                .Property(e => e.BMB10_FAC)
                .HasPrecision(20, 8);

            modelBuilder.Entity<BMB_FILE>()
                .Property(e => e.BMB10_FAC2)
                .HasPrecision(20, 8);

            modelBuilder.Entity<BMB_FILE>()
                .Property(e => e.BMB11)
                .IsUnicode(false);

            modelBuilder.Entity<BMB_FILE>()
                .Property(e => e.BMB13)
                .IsUnicode(false);

            modelBuilder.Entity<BMB_FILE>()
                .Property(e => e.BMB14)
                .IsUnicode(false);

            modelBuilder.Entity<BMB_FILE>()
                .Property(e => e.BMB15)
                .IsUnicode(false);

            modelBuilder.Entity<BMB_FILE>()
                .Property(e => e.BMB16)
                .IsUnicode(false);

            modelBuilder.Entity<BMB_FILE>()
                .Property(e => e.BMB17)
                .IsUnicode(false);

            modelBuilder.Entity<BMB_FILE>()
                .Property(e => e.BMB19)
                .IsUnicode(false);

            modelBuilder.Entity<BMB_FILE>()
                .Property(e => e.BMB21)
                .IsUnicode(false);

            modelBuilder.Entity<BMB_FILE>()
                .Property(e => e.BMB22)
                .IsUnicode(false);

            modelBuilder.Entity<BMB_FILE>()
                .Property(e => e.BMB23)
                .HasPrecision(9, 4);

            modelBuilder.Entity<BMB_FILE>()
                .Property(e => e.BMB24)
                .IsUnicode(false);

            modelBuilder.Entity<BMB_FILE>()
                .Property(e => e.BMB25)
                .IsUnicode(false);

            modelBuilder.Entity<BMB_FILE>()
                .Property(e => e.BMB26)
                .IsUnicode(false);

            modelBuilder.Entity<BMB_FILE>()
                .Property(e => e.BMB27)
                .IsUnicode(false);

            modelBuilder.Entity<BMB_FILE>()
                .Property(e => e.BMB28)
                .HasPrecision(9, 4);

            modelBuilder.Entity<BMB_FILE>()
                .Property(e => e.BMBMODU)
                .IsUnicode(false);

            modelBuilder.Entity<BMB_FILE>()
                .Property(e => e.BMBCOMM)
                .IsUnicode(false);

            modelBuilder.Entity<BMB_FILE>()
                .Property(e => e.BMB29)
                .IsUnicode(false);

            modelBuilder.Entity<BMB_FILE>()
                .Property(e => e.BMB30)
                .IsUnicode(false);

            modelBuilder.Entity<BMB_FILE>()
                .Property(e => e.BMB31)
                .IsUnicode(false);

            modelBuilder.Entity<BMB_FILE>()
                .Property(e => e.BMBUD01)
                .IsUnicode(false);

            modelBuilder.Entity<BMB_FILE>()
                .Property(e => e.BMBUD02)
                .IsUnicode(false);

            modelBuilder.Entity<BMB_FILE>()
                .Property(e => e.BMBUD03)
                .IsUnicode(false);

            modelBuilder.Entity<BMB_FILE>()
                .Property(e => e.BMBUD04)
                .IsUnicode(false);

            modelBuilder.Entity<BMB_FILE>()
                .Property(e => e.BMBUD05)
                .IsUnicode(false);

            modelBuilder.Entity<BMB_FILE>()
                .Property(e => e.BMBUD06)
                .IsUnicode(false);

            modelBuilder.Entity<BMB_FILE>()
                .Property(e => e.BMBUD07)
                .HasPrecision(15, 3);

            modelBuilder.Entity<BMB_FILE>()
                .Property(e => e.BMBUD08)
                .HasPrecision(15, 3);

            modelBuilder.Entity<BMB_FILE>()
                .Property(e => e.BMBUD09)
                .HasPrecision(15, 3);

            modelBuilder.Entity<BMB_FILE>()
                .Property(e => e.BMB081)
                .HasPrecision(15, 3);

            modelBuilder.Entity<BMB_FILE>()
                .Property(e => e.BMB082)
                .HasPrecision(9, 4);

            modelBuilder.Entity<BMB_FILE>()
                .Property(e => e.BMB36)
                .IsUnicode(false);

            modelBuilder.Entity<BMB_FILE>()
                .Property(e => e.BMB37)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA01)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA02)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA021)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA03)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA04)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA05)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA06)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA07)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA08)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA09)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA10)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA11)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA12)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA13)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA14)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA15)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA17)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA17_FAC)
                .HasPrecision(16, 8);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA18)
                .HasPrecision(15, 3);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA19)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA20)
                .HasPrecision(9, 3);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA21)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA22)
                .HasPrecision(9, 3);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA23)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA24)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA25)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA26)
                .HasPrecision(15, 3);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA261)
                .HasPrecision(15, 3);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA262)
                .HasPrecision(15, 3);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA27)
                .HasPrecision(15, 3);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA271)
                .HasPrecision(15, 3);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA28)
                .HasPrecision(15, 3);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA31)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA31_FAC)
                .HasPrecision(20, 8);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA32)
                .HasPrecision(20, 6);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA33)
                .HasPrecision(20, 6);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA34)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA35)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA36)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA37)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA38)
                .HasPrecision(15, 3);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA39)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA40)
                .HasPrecision(15, 3);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA41)
                .HasPrecision(15, 3);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA42)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA43)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA44)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA44_FAC)
                .HasPrecision(20, 8);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA45)
                .HasPrecision(15, 3);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA46)
                .HasPrecision(15, 3);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA47)
                .HasPrecision(9, 4);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA48)
                .HasPrecision(15, 3);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA49)
                .HasPrecision(15, 3);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA491)
                .HasPrecision(15, 3);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA50)
                .HasPrecision(15, 3);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA51)
                .HasPrecision(15, 3);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA52)
                .HasPrecision(15, 3);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA53)
                .HasPrecision(20, 6);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA531)
                .HasPrecision(20, 6);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA54)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA55)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA55_FAC)
                .HasPrecision(20, 8);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA56)
                .HasPrecision(15, 3);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA561)
                .HasPrecision(15, 3);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA562)
                .HasPrecision(9, 4);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA571)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA58)
                .HasPrecision(15, 3);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA59)
                .HasPrecision(15, 3);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA60)
                .HasPrecision(15, 3);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA61)
                .HasPrecision(15, 3);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA62)
                .HasPrecision(15, 3);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA63)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA63_FAC)
                .HasPrecision(20, 8);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA64)
                .HasPrecision(15, 3);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA641)
                .HasPrecision(15, 3);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA65)
                .HasPrecision(15, 3);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA66)
                .HasPrecision(15, 3);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA67)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA68)
                .HasPrecision(15, 3);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA69)
                .HasPrecision(15, 3);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA70)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA72)
                .HasPrecision(15, 3);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA86)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA86_FAC)
                .HasPrecision(20, 8);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA87)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA871)
                .HasPrecision(9, 4);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA872)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA873)
                .HasPrecision(9, 4);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA874)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA88)
                .HasPrecision(15, 3);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA91)
                .HasPrecision(20, 6);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA92)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA93)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA94)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA95)
                .HasPrecision(20, 6);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA75)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA76)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA77)
                .HasPrecision(16, 8);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA851)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA852)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA853)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA98)
                .HasPrecision(20, 6);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA99)
                .HasPrecision(15, 3);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA100)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA101)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA102)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA103)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA104)
                .HasPrecision(15, 3);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA105)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA106)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA107)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA108)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA109)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA110)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA111)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA121)
                .HasPrecision(20, 6);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA122)
                .HasPrecision(20, 6);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA123)
                .HasPrecision(20, 6);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA124)
                .HasPrecision(20, 6);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA125)
                .HasPrecision(20, 6);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA126)
                .HasPrecision(15, 3);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA127)
                .HasPrecision(20, 6);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA128)
                .HasPrecision(20, 6);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA129)
                .HasPrecision(15, 3);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA130)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA131)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA132)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA133)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA134)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA135)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA136)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA137)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA138)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA139)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA140)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA141)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA144)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA145)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA146)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA147)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA903)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA904)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA905)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA906)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA907)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA908)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA910)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMAACTI)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMAUSER)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMAGRUP)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMAMODU)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMAAG)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMAAG1)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMAUD01)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMAUD02)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMAUD03)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMAUD04)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMAUD05)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMAUD06)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMAUD07)
                .HasPrecision(15, 3);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMAUD08)
                .HasPrecision(15, 3);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMAUD09)
                .HasPrecision(15, 3);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA1001)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA1002)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA1003)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA1004)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA1005)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA1006)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA1007)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA1008)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA1009)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA1010)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA1011)
                .HasPrecision(15, 3);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA1014)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA1016)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA1017)
                .HasPrecision(20, 6);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA1018)
                .HasPrecision(20, 6);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA1019)
                .HasPrecision(20, 6);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA1020)
                .HasPrecision(20, 6);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA1021)
                .HasPrecision(20, 6);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA1022)
                .HasPrecision(15, 3);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA1023)
                .HasPrecision(15, 3);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA1024)
                .HasPrecision(20, 6);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA1025)
                .HasPrecision(20, 6);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA1026)
                .HasPrecision(20, 6);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA1027)
                .HasPrecision(15, 3);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA1028)
                .HasPrecision(15, 3);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA1029)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA911)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA912)
                .HasPrecision(15, 3);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA913)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA914)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA391)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA1321)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA1911)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA1912)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA1913)
                .HasPrecision(20, 6);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA1914)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA1915)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA1916)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA1919)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA915)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA916)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA150)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA151)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA152)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA918)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA919)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA920)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA921)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA922)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA923)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA924)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA925)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA601)
                .HasPrecision(15, 3);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA153)
                .HasPrecision(9, 4);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA926)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA154)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA155)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA149)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA1491)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMAORIU)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMAORIG)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA022)
                .HasPrecision(15, 3);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA251)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA940)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA941)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA156)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA157)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA158)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA927)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA120)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA159)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA930)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA931)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA932)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA933)
                .IsUnicode(false);

            modelBuilder.Entity<IMA_FILE>()
                .Property(e => e.IMA934)
                .IsUnicode(false);

            modelBuilder.Entity<OBK_FILE>()
                .Property(e => e.OBK01)
                .IsUnicode(false);

            modelBuilder.Entity<OBK_FILE>()
                .Property(e => e.OBK02)
                .IsUnicode(false);

            modelBuilder.Entity<OBK_FILE>()
                .Property(e => e.OBK03)
                .IsUnicode(false);

            modelBuilder.Entity<OBK_FILE>()
                .Property(e => e.OBK05)
                .IsUnicode(false);

            modelBuilder.Entity<OBK_FILE>()
                .Property(e => e.OBK06)
                .IsUnicode(false);

            modelBuilder.Entity<OBK_FILE>()
                .Property(e => e.OBK07)
                .IsUnicode(false);

            modelBuilder.Entity<OBK_FILE>()
                .Property(e => e.OBK08)
                .HasPrecision(20, 6);

            modelBuilder.Entity<OBK_FILE>()
                .Property(e => e.OBK09)
                .HasPrecision(15, 3);

            modelBuilder.Entity<OBK_FILE>()
                .Property(e => e.OBK10)
                .HasPrecision(20, 6);

            modelBuilder.Entity<OBK_FILE>()
                .Property(e => e.OBK11)
                .IsUnicode(false);

            modelBuilder.Entity<OBK_FILE>()
                .Property(e => e.OBK12)
                .IsUnicode(false);

            modelBuilder.Entity<OBK_FILE>()
                .Property(e => e.OBK13)
                .IsUnicode(false);

            modelBuilder.Entity<OBK_FILE>()
                .Property(e => e.OBK14)
                .IsUnicode(false);

            modelBuilder.Entity<OBK_FILE>()
                .Property(e => e.OBKACTI)
                .IsUnicode(false);

            modelBuilder.Entity<OBK_FILE>()
                .Property(e => e.OBKGRUP)
                .IsUnicode(false);

            modelBuilder.Entity<OBK_FILE>()
                .Property(e => e.OBKUSER)
                .IsUnicode(false);

            modelBuilder.Entity<OBK_FILE>()
                .Property(e => e.OBKMODU)
                .IsUnicode(false);

            modelBuilder.Entity<OBK_FILE>()
                .Property(e => e.OBKORIG)
                .IsUnicode(false);

            modelBuilder.Entity<OBK_FILE>()
                .Property(e => e.OBKORIU)
                .IsUnicode(false);

            modelBuilder.Entity<SFU_FILE>()
    .Property(e => e.SFU00)
    .IsUnicode(false);

            modelBuilder.Entity<SFU_FILE>()
                .Property(e => e.SFU01)
                .IsUnicode(false);

            modelBuilder.Entity<SFU_FILE>()
                .Property(e => e.SFU03)
                .IsUnicode(false);

            modelBuilder.Entity<SFU_FILE>()
                .Property(e => e.SFU04)
                .IsUnicode(false);

            modelBuilder.Entity<SFU_FILE>()
                .Property(e => e.SFU05)
                .IsUnicode(false);

            modelBuilder.Entity<SFU_FILE>()
                .Property(e => e.SFU06)
                .IsUnicode(false);

            modelBuilder.Entity<SFU_FILE>()
                .Property(e => e.SFU07)
                .IsUnicode(false);

            modelBuilder.Entity<SFU_FILE>()
                .Property(e => e.SFU08)
                .IsUnicode(false);

            modelBuilder.Entity<SFU_FILE>()
                .Property(e => e.SFU09)
                .IsUnicode(false);

            modelBuilder.Entity<SFU_FILE>()
                .Property(e => e.SFU10)
                .IsUnicode(false);

            modelBuilder.Entity<SFU_FILE>()
                .Property(e => e.SFU11)
                .IsUnicode(false);

            modelBuilder.Entity<SFU_FILE>()
                .Property(e => e.SFU12)
                .IsUnicode(false);

            modelBuilder.Entity<SFU_FILE>()
                .Property(e => e.SFU13)
                .IsUnicode(false);

            modelBuilder.Entity<SFU_FILE>()
                .Property(e => e.SFUPOST)
                .IsUnicode(false);

            modelBuilder.Entity<SFU_FILE>()
                .Property(e => e.SFUUSER)
                .IsUnicode(false);

            modelBuilder.Entity<SFU_FILE>()
                .Property(e => e.SFUGRUP)
                .IsUnicode(false);

            modelBuilder.Entity<SFU_FILE>()
                .Property(e => e.SFUMODU)
                .IsUnicode(false);

            modelBuilder.Entity<SFU_FILE>()
                .Property(e => e.SFUCONF)
                .IsUnicode(false);

            modelBuilder.Entity<SFU_FILE>()
                .Property(e => e.SFUUD01)
                .IsUnicode(false);

            modelBuilder.Entity<SFU_FILE>()
                .Property(e => e.SFUUD02)
                .IsUnicode(false);

            modelBuilder.Entity<SFU_FILE>()
                .Property(e => e.SFUUD03)
                .IsUnicode(false);

            modelBuilder.Entity<SFU_FILE>()
                .Property(e => e.SFUUD04)
                .IsUnicode(false);

            modelBuilder.Entity<SFU_FILE>()
                .Property(e => e.SFUUD05)
                .IsUnicode(false);

            modelBuilder.Entity<SFU_FILE>()
                .Property(e => e.SFUUD06)
                .IsUnicode(false);

            modelBuilder.Entity<SFU_FILE>()
                .Property(e => e.SFUUD07)
                .HasPrecision(15, 3);

            modelBuilder.Entity<SFU_FILE>()
                .Property(e => e.SFUUD08)
                .HasPrecision(15, 3);

            modelBuilder.Entity<SFU_FILE>()
                .Property(e => e.SFUUD09)
                .HasPrecision(15, 3);

            modelBuilder.Entity<SFU_FILE>()
                .Property(e => e.SFUPLANT)
                .IsUnicode(false);

            modelBuilder.Entity<SFU_FILE>()
                .Property(e => e.SFULEGAL)
                .IsUnicode(false);

            modelBuilder.Entity<SFU_FILE>()
                .Property(e => e.SFUORIU)
                .IsUnicode(false);

            modelBuilder.Entity<SFU_FILE>()
                .Property(e => e.SFUORIG)
                .IsUnicode(false);

            modelBuilder.Entity<SFU_FILE>()
                .Property(e => e.SFU15)
                .IsUnicode(false);

            modelBuilder.Entity<SFU_FILE>()
                .Property(e => e.SFU16)
                .IsUnicode(false);

            modelBuilder.Entity<SFU_FILE>()
                .Property(e => e.SFUMKSG)
                .IsUnicode(false);

            modelBuilder.Entity<SFV_FILE>()
                .Property(e => e.SFV01)
                .IsUnicode(false);

            modelBuilder.Entity<SFV_FILE>()
                .Property(e => e.SFV04)
                .IsUnicode(false);

            modelBuilder.Entity<SFV_FILE>()
                .Property(e => e.SFV05)
                .IsUnicode(false);

            modelBuilder.Entity<SFV_FILE>()
                .Property(e => e.SFV06)
                .IsUnicode(false);

            modelBuilder.Entity<SFV_FILE>()
                .Property(e => e.SFV07)
                .IsUnicode(false);

            modelBuilder.Entity<SFV_FILE>()
                .Property(e => e.SFV08)
                .IsUnicode(false);

            modelBuilder.Entity<SFV_FILE>()
                .Property(e => e.SFV09)
                .HasPrecision(15, 3);

            modelBuilder.Entity<SFV_FILE>()
                .Property(e => e.SFV11)
                .IsUnicode(false);

            modelBuilder.Entity<SFV_FILE>()
                .Property(e => e.SFV12)
                .IsUnicode(false);

            modelBuilder.Entity<SFV_FILE>()
                .Property(e => e.SFV13)
                .HasPrecision(15, 3);

            modelBuilder.Entity<SFV_FILE>()
                .Property(e => e.SFV16)
                .IsUnicode(false);

            modelBuilder.Entity<SFV_FILE>()
                .Property(e => e.SFV17)
                .IsUnicode(false);

            modelBuilder.Entity<SFV_FILE>()
                .Property(e => e.SFV18)
                .IsUnicode(false);

            modelBuilder.Entity<SFV_FILE>()
                .Property(e => e.SFV19)
                .IsUnicode(false);

            modelBuilder.Entity<SFV_FILE>()
                .Property(e => e.SFV20)
                .IsUnicode(false);

            modelBuilder.Entity<SFV_FILE>()
                .Property(e => e.SFV30)
                .IsUnicode(false);

            modelBuilder.Entity<SFV_FILE>()
                .Property(e => e.SFV31)
                .HasPrecision(20, 8);

            modelBuilder.Entity<SFV_FILE>()
                .Property(e => e.SFV32)
                .HasPrecision(15, 3);

            modelBuilder.Entity<SFV_FILE>()
                .Property(e => e.SFV33)
                .IsUnicode(false);

            modelBuilder.Entity<SFV_FILE>()
                .Property(e => e.SFV34)
                .HasPrecision(20, 8);

            modelBuilder.Entity<SFV_FILE>()
                .Property(e => e.SFV35)
                .HasPrecision(15, 3);

            modelBuilder.Entity<SFV_FILE>()
                .Property(e => e.SFV930)
                .IsUnicode(false);

            modelBuilder.Entity<SFV_FILE>()
                .Property(e => e.SFV41)
                .IsUnicode(false);

            modelBuilder.Entity<SFV_FILE>()
                .Property(e => e.SFV42)
                .IsUnicode(false);

            modelBuilder.Entity<SFV_FILE>()
                .Property(e => e.SFV43)
                .IsUnicode(false);

            modelBuilder.Entity<SFV_FILE>()
                .Property(e => e.SFV44)
                .IsUnicode(false);

            modelBuilder.Entity<SFV_FILE>()
                .Property(e => e.SFVUD01)
                .IsUnicode(false);

            modelBuilder.Entity<SFV_FILE>()
                .Property(e => e.SFVUD02)
                .IsUnicode(false);

            modelBuilder.Entity<SFV_FILE>()
                .Property(e => e.SFVUD03)
                .IsUnicode(false);

            modelBuilder.Entity<SFV_FILE>()
                .Property(e => e.SFVUD04)
                .IsUnicode(false);

            modelBuilder.Entity<SFV_FILE>()
                .Property(e => e.SFVUD05)
                .IsUnicode(false);

            modelBuilder.Entity<SFV_FILE>()
                .Property(e => e.SFVUD06)
                .IsUnicode(false);

            modelBuilder.Entity<SFV_FILE>()
                .Property(e => e.SFVUD07)
                .HasPrecision(15, 3);

            modelBuilder.Entity<SFV_FILE>()
                .Property(e => e.SFVUD08)
                .HasPrecision(15, 3);

            modelBuilder.Entity<SFV_FILE>()
                .Property(e => e.SFVUD09)
                .HasPrecision(15, 3);

            modelBuilder.Entity<SFV_FILE>()
                .Property(e => e.SFVPLANT)
                .IsUnicode(false);

            modelBuilder.Entity<SFV_FILE>()
                .Property(e => e.SFVLEGAL)
                .IsUnicode(false);

            modelBuilder.Entity<SFV_FILE>()
                .Property(e => e.SFV45)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRE_FILE>()
                .Property(e => e.TC_BRE01)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRE_FILE>()
                .Property(e => e.TC_BRE02)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRE_FILE>()
                .Property(e => e.TC_BRE03)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRE_FILE>()
                .Property(e => e.TC_BRE04)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRE_FILE>()
                .Property(e => e.TC_BRE05)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRE_FILE>()
                .Property(e => e.TC_BREUSER)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRE_FILE>()
                .Property(e => e.TC_BREGRUP)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRE_FILE>()
                .Property(e => e.TC_BREMODU)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRE_FILE>()
                .Property(e => e.TC_BREUD01)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRE_FILE>()
                .Property(e => e.TC_BREUD02)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRE_FILE>()
                .Property(e => e.TC_BREUD03)
                .HasPrecision(15, 3);

            modelBuilder.Entity<TC_BRE_FILE>()
                .Property(e => e.TC_BREUD04)
                .HasPrecision(15, 3);

            modelBuilder.Entity<TC_BRE_FILE>()
                .Property(e => e.TC_BREPLANT)
                .IsUnicode(false);

            modelBuilder.Entity<TC_BRE_FILE>()
                .Property(e => e.TC_BRELEGAL)
                .IsUnicode(false);

            modelBuilder.Entity<TC_XXU_FILE>()
                .Property(e => e.TC_XXU001)
                .IsUnicode(false);

            modelBuilder.Entity<TC_XXU_FILE>()
                .Property(e => e.TC_XXU002)
                .IsUnicode(false);

            modelBuilder.Entity<TC_XXU_FILE>()
                .Property(e => e.TC_XXU003)
                .IsUnicode(false);

            modelBuilder.Entity<TC_XXU_FILE>()
                .Property(e => e.TC_XXU004)
                .IsUnicode(false);

            modelBuilder.Entity<TC_XXU_FILE>()
                .Property(e => e.TC_XXU005)
                .IsUnicode(false);

            modelBuilder.Entity<TC_XXU_FILE>()
                .Property(e => e.TC_XXU006)
                .IsUnicode(false);

            modelBuilder.Entity<TC_XXU_FILE>()
                .Property(e => e.TC_XXU007)
                .IsUnicode(false);

            modelBuilder.Entity<TC_XXU_FILE>()
                .Property(e => e.TC_XXU009)
                .IsUnicode(false);

            modelBuilder.Entity<TC_XXU_FILE>()
                .Property(e => e.TC_XXU010)
                .IsUnicode(false);

            modelBuilder.Entity<TC_XXU_FILE>()
                .Property(e => e.TC_XXU011)
                .IsUnicode(false);

            modelBuilder.Entity<TC_IME_FILE>()
                .Property(e => e.TC_IME01)
                .IsUnicode(false);

            modelBuilder.Entity<TC_IME_FILE>()
                .Property(e => e.TC_IME02)
                .IsUnicode(false);

            modelBuilder.Entity<TC_IME_FILE>()
                .Property(e => e.TC_IME03)
                .IsUnicode(false);


            modelBuilder.Entity<TC_QCX_FILE>()
                .Property(e => e.TC_QCX02)
                .IsUnicode(false);

            modelBuilder.Entity<TC_QCX_FILE>()
                .Property(e => e.TC_QCX06)
                .IsUnicode(false);

            modelBuilder.Entity<TC_QCX_FILE>()
                .Property(e => e.TC_QCX07)
                .IsUnicode(false);

            modelBuilder.Entity<TC_QCXX_FILE>()
                .Property(e => e.TC_QCXX02)
                .IsUnicode(false);

            modelBuilder.Entity<TC_QCXX_FILE>()
                .Property(e => e.TC_QCXX06)
                .IsUnicode(false);

            modelBuilder.Entity<TC_QCXX_FILE>()
                .Property(e => e.TC_QCXX07)
                .IsUnicode(false);

            modelBuilder.Entity<TC_QCXX_FILE>()
                .Property(e => e.TC_QCXX09)
                .IsUnicode(false);

            modelBuilder.Entity<TC_QCXX_FILE>()
                .Property(e => e.TC_QCXX14)
                .IsUnicode(false);

            modelBuilder.Entity<TC_QCXX_FILE>()
                .Property(e => e.TC_QCXX15)
                .IsUnicode(false);


            modelBuilder.Entity<TC_QCY_FILE>()
        .Property(e => e.TC_QCY01)
        .IsUnicode(false);

            modelBuilder.Entity<TC_QCY_FILE>()
                .Property(e => e.TC_QCY02)
                .IsUnicode(false);

            modelBuilder.Entity<TC_QCY_FILE>()
                .Property(e => e.TC_QCY03)
                .IsUnicode(false);

            modelBuilder.Entity<TC_QCY_FILE>()
                .Property(e => e.TC_QCY04)
                .IsUnicode(false);

            modelBuilder.Entity<TC_QCY_FILE>()
                .Property(e => e.TC_QCY05)
                .IsUnicode(false);

            modelBuilder.Entity<TC_QCY_FILE>()
                .Property(e => e.TC_QCY06)
                .IsUnicode(false);

            modelBuilder.Entity<TC_QCY_FILE>()
                .Property(e => e.TC_QCY07)
                .IsUnicode(false);

            modelBuilder.Entity<TC_QCY_FILE>()
                .Property(e => e.TC_QCY09)
                .IsUnicode(false);

            modelBuilder.Entity<TC_QCY_FILE>()
                .Property(e => e.TC_QCY11)
                .IsUnicode(false);

            modelBuilder.Entity<TC_QCY_FILE>()
                .Property(e => e.TC_QCY13)
                .IsUnicode(false);

            modelBuilder.Entity<TC_QCY_FILE>()
                .Property(e => e.TC_QCY17)
                .IsUnicode(false);

            modelBuilder.Entity<TC_QCY_FILE>()
                .Property(e => e.TC_QCY18)
                .IsUnicode(false);


            modelBuilder.Entity<TC_QCZ_FILE>()
                .Property(e => e.TC_QCZ01)
                .IsUnicode(false);

            modelBuilder.Entity<TC_QCZ_FILE>()
                .Property(e => e.TC_QCZ02)
                .IsUnicode(false);

            modelBuilder.Entity<TC_QCZ_FILE>()
                .Property(e => e.TC_QCZ03)
                .IsUnicode(false);

            modelBuilder.Entity<TC_QCZ_FILE>()
                .Property(e => e.TC_QCZ04)
                .IsUnicode(false);

            modelBuilder.Entity<TC_QCZ_FILE>()
                .Property(e => e.TC_QCZ07)
                .IsUnicode(false);

            modelBuilder.Entity<TC_QCZ_FILE>()
                .Property(e => e.TC_QCZ08)
                .IsUnicode(false);

            modelBuilder.Entity<TC_QCZ_FILE>()
                .Property(e => e.TC_QCZ11)
                .HasPrecision(15, 3);

            modelBuilder.Entity<RVBS_FILE>()
      .Property(e => e.RVBS00)
      .IsUnicode(false);

            modelBuilder.Entity<RVBS_FILE>()
                .Property(e => e.RVBS01)
                .IsUnicode(false);

            modelBuilder.Entity<RVBS_FILE>()
                .Property(e => e.RVBS03)
                .IsUnicode(false);

            modelBuilder.Entity<RVBS_FILE>()
                .Property(e => e.RVBS04)
                .IsUnicode(false);

            modelBuilder.Entity<RVBS_FILE>()
                .Property(e => e.RVBS06)
                .HasPrecision(15, 3);

            modelBuilder.Entity<RVBS_FILE>()
                .Property(e => e.RVBS07)
                .IsUnicode(false);

            modelBuilder.Entity<RVBS_FILE>()
                .Property(e => e.RVBS08)
                .IsUnicode(false);

            modelBuilder.Entity<RVBS_FILE>()
                .Property(e => e.RVBS021)
                .IsUnicode(false);

            modelBuilder.Entity<RVBS_FILE>()
                .Property(e => e.RVBS10)
                .HasPrecision(15, 3);

            modelBuilder.Entity<RVBS_FILE>()
                .Property(e => e.RVBS11)
                .HasPrecision(15, 3);

            modelBuilder.Entity<RVBS_FILE>()
                .Property(e => e.RVBS12)
                .HasPrecision(15, 3);

            modelBuilder.Entity<RVBS_FILE>()
                .Property(e => e.RVBSPLANT)
                .IsUnicode(false);

            modelBuilder.Entity<RVBS_FILE>()
                .Property(e => e.RVBSLEGAL)
                .IsUnicode(false);

            modelBuilder.Entity<QCS_FILE>()
     .Property(e => e.QCS00)
     .IsUnicode(false);

            modelBuilder.Entity<QCS_FILE>()
                .Property(e => e.QCS01)
                .IsUnicode(false);

            modelBuilder.Entity<QCS_FILE>()
                .Property(e => e.QCS021)
                .IsUnicode(false);

            modelBuilder.Entity<QCS_FILE>()
                .Property(e => e.QCS03)
                .IsUnicode(false);

            modelBuilder.Entity<QCS_FILE>()
                .Property(e => e.QCS041)
                .IsUnicode(false);

            modelBuilder.Entity<QCS_FILE>()
                .Property(e => e.QCS06)
                .HasPrecision(15, 3);

            modelBuilder.Entity<QCS_FILE>()
                .Property(e => e.QCS061)
                .HasPrecision(12, 3);

            modelBuilder.Entity<QCS_FILE>()
                .Property(e => e.QCS062)
                .IsUnicode(false);

            modelBuilder.Entity<QCS_FILE>()
                .Property(e => e.QCS071)
                .HasPrecision(12, 3);

            modelBuilder.Entity<QCS_FILE>()
                .Property(e => e.QCS072)
                .IsUnicode(false);

            modelBuilder.Entity<QCS_FILE>()
                .Property(e => e.QCS081)
                .HasPrecision(12, 3);

            modelBuilder.Entity<QCS_FILE>()
                .Property(e => e.QCS082)
                .IsUnicode(false);

            modelBuilder.Entity<QCS_FILE>()
                .Property(e => e.QCS09)
                .IsUnicode(false);

            modelBuilder.Entity<QCS_FILE>()
                .Property(e => e.QCS091)
                .HasPrecision(15, 3);

            modelBuilder.Entity<QCS_FILE>()
                .Property(e => e.QCS10)
                .IsUnicode(false);

            modelBuilder.Entity<QCS_FILE>()
                .Property(e => e.QCS11)
                .IsUnicode(false);

            modelBuilder.Entity<QCS_FILE>()
                .Property(e => e.QCS12)
                .IsUnicode(false);

            modelBuilder.Entity<QCS_FILE>()
                .Property(e => e.QCS13)
                .IsUnicode(false);

            modelBuilder.Entity<QCS_FILE>()
                .Property(e => e.QCS14)
                .IsUnicode(false);

            modelBuilder.Entity<QCS_FILE>()
                .Property(e => e.QCS16)
                .IsUnicode(false);

            modelBuilder.Entity<QCS_FILE>()
                .Property(e => e.QCS17)
                .IsUnicode(false);

            modelBuilder.Entity<QCS_FILE>()
                .Property(e => e.QCS19)
                .IsUnicode(false);

            modelBuilder.Entity<QCS_FILE>()
                .Property(e => e.QCS20)
                .IsUnicode(false);

            modelBuilder.Entity<QCS_FILE>()
                .Property(e => e.QCS21)
                .IsUnicode(false);

            modelBuilder.Entity<QCS_FILE>()
                .Property(e => e.QCS22)
                .HasPrecision(15, 3);

            modelBuilder.Entity<QCS_FILE>()
                .Property(e => e.QCSACTI)
                .IsUnicode(false);

            modelBuilder.Entity<QCS_FILE>()
                .Property(e => e.QCSUSER)
                .IsUnicode(false);

            modelBuilder.Entity<QCS_FILE>()
                .Property(e => e.QCSGRUP)
                .IsUnicode(false);

            modelBuilder.Entity<QCS_FILE>()
                .Property(e => e.QCSMODU)
                .IsUnicode(false);

            modelBuilder.Entity<QCS_FILE>()
                .Property(e => e.QCS30)
                .IsUnicode(false);

            modelBuilder.Entity<QCS_FILE>()
                .Property(e => e.QCS31)
                .HasPrecision(20, 8);

            modelBuilder.Entity<QCS_FILE>()
                .Property(e => e.QCS32)
                .HasPrecision(15, 3);

            modelBuilder.Entity<QCS_FILE>()
                .Property(e => e.QCS33)
                .IsUnicode(false);

            modelBuilder.Entity<QCS_FILE>()
                .Property(e => e.QCS34)
                .HasPrecision(20, 8);

            modelBuilder.Entity<QCS_FILE>()
                .Property(e => e.QCS35)
                .HasPrecision(15, 3);

            modelBuilder.Entity<QCS_FILE>()
                .Property(e => e.QCS36)
                .IsUnicode(false);

            modelBuilder.Entity<QCS_FILE>()
                .Property(e => e.QCS37)
                .HasPrecision(20, 8);

            modelBuilder.Entity<QCS_FILE>()
                .Property(e => e.QCS38)
                .HasPrecision(15, 3);

            modelBuilder.Entity<QCS_FILE>()
                .Property(e => e.QCS39)
                .IsUnicode(false);

            modelBuilder.Entity<QCS_FILE>()
                .Property(e => e.QCS40)
                .HasPrecision(20, 8);

            modelBuilder.Entity<QCS_FILE>()
                .Property(e => e.QCS41)
                .HasPrecision(15, 3);

            modelBuilder.Entity<QCS_FILE>()
                .Property(e => e.QCSSPC)
                .IsUnicode(false);

            modelBuilder.Entity<QCS_FILE>()
                .Property(e => e.QCSUD01)
                .IsUnicode(false);

            modelBuilder.Entity<QCS_FILE>()
                .Property(e => e.QCSUD02)
                .IsUnicode(false);

            modelBuilder.Entity<QCS_FILE>()
                .Property(e => e.QCSUD03)
                .IsUnicode(false);

            modelBuilder.Entity<QCS_FILE>()
                .Property(e => e.QCSUD04)
                .IsUnicode(false);

            modelBuilder.Entity<QCS_FILE>()
                .Property(e => e.QCSUD05)
                .IsUnicode(false);

            modelBuilder.Entity<QCS_FILE>()
                .Property(e => e.QCSUD06)
                .IsUnicode(false);

            modelBuilder.Entity<QCS_FILE>()
                .Property(e => e.QCSUD07)
                .HasPrecision(15, 3);

            modelBuilder.Entity<QCS_FILE>()
                .Property(e => e.QCSUD08)
                .HasPrecision(15, 3);

            modelBuilder.Entity<QCS_FILE>()
                .Property(e => e.QCSUD09)
                .HasPrecision(15, 3);

            modelBuilder.Entity<QCS_FILE>()
                .Property(e => e.QCSPLANT)
                .IsUnicode(false);

            modelBuilder.Entity<QCS_FILE>()
                .Property(e => e.QCSLEGAL)
                .IsUnicode(false);

            modelBuilder.Entity<QCS_FILE>()
                .Property(e => e.QCSORIU)
                .IsUnicode(false);

            modelBuilder.Entity<QCS_FILE>()
                .Property(e => e.QCSORIG)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
    .Property(e => e.PMC01)
    .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC02)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC03)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC04)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC05)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC06)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC07)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC081)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC082)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC091)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC092)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC093)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC094)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC095)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC10)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC11)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC12)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC13)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC14)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC15)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC16)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC17)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC18)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC19)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC20)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC21)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC22)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC23)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC24)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC25)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC26)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC27)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC30)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC45)
                .HasPrecision(13, 2);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC46)
                .HasPrecision(13, 2);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC47)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC48)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC49)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC52)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC53)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC54)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC55)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC56)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC901)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC902)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC903)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC904)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC905)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC906)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC907)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC908)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC909)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC910)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC911)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMCACTI)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMCUSER)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMCGRUP)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMCMODU)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMCUD01)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMCUD02)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMCUD03)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMCUD04)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMCUD05)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMCUD06)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMCUD07)
                .HasPrecision(15, 3);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMCUD08)
                .HasPrecision(15, 3);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMCUD09)
                .HasPrecision(15, 3);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC912)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC1912)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC1913)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC1914)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC1915)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC1916)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC1917)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC1918)
                .HasPrecision(15, 3);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC1919)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC1920)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC913)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC281)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC914)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC915)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC916)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC917)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC918)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC919)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC920)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC921)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC922)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC923)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC57)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC59)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC60)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMC930)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMCORIU)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMCORIG)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMCUD16)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMCUD17)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMCUD18)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMCUD19)
                .IsUnicode(false);

            modelBuilder.Entity<PMC_FILE>()
                .Property(e => e.PMCUD20)
                .IsUnicode(false);

            modelBuilder.Entity<RVB_FILE>()
    .Property(e => e.RVB01)
    .IsUnicode(false);

            modelBuilder.Entity<RVB_FILE>()
                .Property(e => e.RVB04)
                .IsUnicode(false);

            modelBuilder.Entity<RVB_FILE>()
                .Property(e => e.RVB05)
                .IsUnicode(false);

            modelBuilder.Entity<RVB_FILE>()
                .Property(e => e.RVB06)
                .HasPrecision(15, 3);

            modelBuilder.Entity<RVB_FILE>()
                .Property(e => e.RVB07)
                .HasPrecision(15, 3);

            modelBuilder.Entity<RVB_FILE>()
                .Property(e => e.RVB08)
                .HasPrecision(15, 3);

            modelBuilder.Entity<RVB_FILE>()
                .Property(e => e.RVB09)
                .HasPrecision(15, 3);

            modelBuilder.Entity<RVB_FILE>()
                .Property(e => e.RVB10)
                .HasPrecision(20, 6);

            modelBuilder.Entity<RVB_FILE>()
                .Property(e => e.RVB13)
                .IsUnicode(false);

            modelBuilder.Entity<RVB_FILE>()
                .Property(e => e.RVB14)
                .IsUnicode(false);

            modelBuilder.Entity<RVB_FILE>()
                .Property(e => e.RVB15)
                .HasPrecision(15, 3);

            modelBuilder.Entity<RVB_FILE>()
                .Property(e => e.RVB16)
                .HasPrecision(15, 3);

            modelBuilder.Entity<RVB_FILE>()
                .Property(e => e.RVB17)
                .IsUnicode(false);

            modelBuilder.Entity<RVB_FILE>()
                .Property(e => e.RVB18)
                .IsUnicode(false);

            modelBuilder.Entity<RVB_FILE>()
                .Property(e => e.RVB19)
                .IsUnicode(false);

            modelBuilder.Entity<RVB_FILE>()
                .Property(e => e.RVB20)
                .IsUnicode(false);

            modelBuilder.Entity<RVB_FILE>()
                .Property(e => e.RVB22)
                .IsUnicode(false);

            modelBuilder.Entity<RVB_FILE>()
                .Property(e => e.RVB25)
                .IsUnicode(false);

            modelBuilder.Entity<RVB_FILE>()
                .Property(e => e.RVB27)
                .HasPrecision(13, 3);

            modelBuilder.Entity<RVB_FILE>()
                .Property(e => e.RVB28)
                .HasPrecision(13, 3);

            modelBuilder.Entity<RVB_FILE>()
                .Property(e => e.RVB29)
                .HasPrecision(15, 3);

            modelBuilder.Entity<RVB_FILE>()
                .Property(e => e.RVB30)
                .HasPrecision(15, 3);

            modelBuilder.Entity<RVB_FILE>()
                .Property(e => e.RVB31)
                .HasPrecision(15, 3);

            modelBuilder.Entity<RVB_FILE>()
                .Property(e => e.RVB32)
                .HasPrecision(15, 3);

            modelBuilder.Entity<RVB_FILE>()
                .Property(e => e.RVB33)
                .HasPrecision(15, 3);

            modelBuilder.Entity<RVB_FILE>()
                .Property(e => e.RVB34)
                .IsUnicode(false);

            modelBuilder.Entity<RVB_FILE>()
                .Property(e => e.RVB35)
                .IsUnicode(false);

            modelBuilder.Entity<RVB_FILE>()
                .Property(e => e.RVB36)
                .IsUnicode(false);

            modelBuilder.Entity<RVB_FILE>()
                .Property(e => e.RVB37)
                .IsUnicode(false);

            modelBuilder.Entity<RVB_FILE>()
                .Property(e => e.RVB38)
                .IsUnicode(false);

            modelBuilder.Entity<RVB_FILE>()
                .Property(e => e.RVB39)
                .IsUnicode(false);

            modelBuilder.Entity<RVB_FILE>()
                .Property(e => e.RVB41)
                .IsUnicode(false);

            modelBuilder.Entity<RVB_FILE>()
                .Property(e => e.RVB80)
                .IsUnicode(false);

            modelBuilder.Entity<RVB_FILE>()
                .Property(e => e.RVB81)
                .HasPrecision(20, 8);

            modelBuilder.Entity<RVB_FILE>()
                .Property(e => e.RVB82)
                .HasPrecision(15, 3);

            modelBuilder.Entity<RVB_FILE>()
                .Property(e => e.RVB83)
                .IsUnicode(false);

            modelBuilder.Entity<RVB_FILE>()
                .Property(e => e.RVB84)
                .HasPrecision(20, 8);

            modelBuilder.Entity<RVB_FILE>()
                .Property(e => e.RVB85)
                .HasPrecision(15, 3);

            modelBuilder.Entity<RVB_FILE>()
                .Property(e => e.RVB86)
                .IsUnicode(false);

            modelBuilder.Entity<RVB_FILE>()
                .Property(e => e.RVB87)
                .HasPrecision(15, 3);

            modelBuilder.Entity<RVB_FILE>()
                .Property(e => e.RVB88)
                .HasPrecision(20, 6);

            modelBuilder.Entity<RVB_FILE>()
                .Property(e => e.RVB88T)
                .HasPrecision(20, 6);

            modelBuilder.Entity<RVB_FILE>()
                .Property(e => e.RVB10T)
                .HasPrecision(20, 6);

            modelBuilder.Entity<RVB_FILE>()
                .Property(e => e.RVB331)
                .HasPrecision(15, 3);

            modelBuilder.Entity<RVB_FILE>()
                .Property(e => e.RVB332)
                .HasPrecision(15, 3);

            modelBuilder.Entity<RVB_FILE>()
                .Property(e => e.RVB930)
                .IsUnicode(false);

            modelBuilder.Entity<RVB_FILE>()
                .Property(e => e.RVBUD01)
                .IsUnicode(false);

            modelBuilder.Entity<RVB_FILE>()
                .Property(e => e.RVBUD02)
                .IsUnicode(false);

            modelBuilder.Entity<RVB_FILE>()
                .Property(e => e.RVBUD03)
                .IsUnicode(false);

            modelBuilder.Entity<RVB_FILE>()
                .Property(e => e.RVBUD04)
                .IsUnicode(false);

            modelBuilder.Entity<RVB_FILE>()
                .Property(e => e.RVBUD05)
                .IsUnicode(false);

            modelBuilder.Entity<RVB_FILE>()
                .Property(e => e.RVBUD06)
                .IsUnicode(false);

            modelBuilder.Entity<RVB_FILE>()
                .Property(e => e.RVBUD07)
                .HasPrecision(15, 3);

            modelBuilder.Entity<RVB_FILE>()
                .Property(e => e.RVBUD08)
                .HasPrecision(15, 3);

            modelBuilder.Entity<RVB_FILE>()
                .Property(e => e.RVBUD09)
                .HasPrecision(15, 3);

            modelBuilder.Entity<RVB_FILE>()
                .Property(e => e.RVB051)
                .IsUnicode(false);

            modelBuilder.Entity<RVB_FILE>()
                .Property(e => e.RVB89)
                .IsUnicode(false);

            modelBuilder.Entity<RVB_FILE>()
                .Property(e => e.RVB90)
                .IsUnicode(false);

            modelBuilder.Entity<RVB_FILE>()
                .Property(e => e.RVB90_FAC)
                .HasPrecision(20, 8);

            modelBuilder.Entity<RVB_FILE>()
                .Property(e => e.RVB42)
                .IsUnicode(false);

            modelBuilder.Entity<RVB_FILE>()
                .Property(e => e.RVB43)
                .IsUnicode(false);

            modelBuilder.Entity<RVB_FILE>()
                .Property(e => e.RVB44)
                .IsUnicode(false);

            modelBuilder.Entity<RVB_FILE>()
                .Property(e => e.RVBPLANT)
                .IsUnicode(false);

            modelBuilder.Entity<RVB_FILE>()
                .Property(e => e.RVBLEGAL)
                .IsUnicode(false);

            modelBuilder.Entity<RVB_FILE>()
                .Property(e => e.RVB93)
                .IsUnicode(false);

            modelBuilder.Entity<RVB_FILE>()
                .Property(e => e.RVB919)
                .IsUnicode(false);

            modelBuilder.Entity<TC_QCYY_FILE>()
                 .Property(e => e.TC_QCYY01)
                 .IsUnicode(false);

            modelBuilder.Entity<TC_QCYY_FILE>()
                .Property(e => e.TC_QCYY02)
                .IsUnicode(false);

            modelBuilder.Entity<TC_QCYY_FILE>()
                .Property(e => e.TC_QCYY03)
                .IsUnicode(false);

            modelBuilder.Entity<TC_QCYY_FILE>()
                .Property(e => e.TC_QCYY04)
                .IsUnicode(false);

            modelBuilder.Entity<TC_QCYY_FILE>()
                .Property(e => e.TC_QCYY05)
                .IsUnicode(false);

            modelBuilder.Entity<TC_QCYY_FILE>()
                .Property(e => e.TC_QCYY06)
                .IsUnicode(false);

            modelBuilder.Entity<TC_QCYY_FILE>()
                .Property(e => e.TC_QCYY07)
                .IsUnicode(false);

            modelBuilder.Entity<TC_QCYY_FILE>()
                .Property(e => e.TC_QCYY09)
                .IsUnicode(false);

            modelBuilder.Entity<TC_QCYY_FILE>()
                .Property(e => e.TC_QCYY11)
                .IsUnicode(false);

            modelBuilder.Entity<TC_QCYY_FILE>()
                .Property(e => e.TC_QCYY13)
                .IsUnicode(false);

            modelBuilder.Entity<TC_QCYY_FILE>()
                .Property(e => e.TC_QCYY17)
                .IsUnicode(false);

            modelBuilder.Entity<TC_QCYY_FILE>()
                .Property(e => e.TC_QCYY18)
                .IsUnicode(false);

            modelBuilder.Entity<TC_QCZZ_FILE>()
                .Property(e => e.TC_QCZZ01)
                .IsUnicode(false);

            modelBuilder.Entity<TC_QCZZ_FILE>()
                .Property(e => e.TC_QCZZ02)
                .IsUnicode(false);

            modelBuilder.Entity<TC_QCZZ_FILE>()
                .Property(e => e.TC_QCZZ03)
                .IsUnicode(false);

            modelBuilder.Entity<TC_QCZZ_FILE>()
                .Property(e => e.TC_QCZZ04)
                .IsUnicode(false);

            modelBuilder.Entity<TC_QCZZ_FILE>()
                .Property(e => e.TC_QCZZ05)
                .IsUnicode(false);

            modelBuilder.Entity<TC_QCZZ_FILE>()
                .Property(e => e.TC_QCZZ08)
                .IsUnicode(false);

            modelBuilder.Entity<TC_QCZZ_FILE>()
                .Property(e => e.TC_QCZZ09)
                .IsUnicode(false);

            modelBuilder.Entity<TC_QCZZ_FILE>()
                .Property(e => e.TC_QCZZ12)
                .HasPrecision(15, 3);

        }
    }
}
