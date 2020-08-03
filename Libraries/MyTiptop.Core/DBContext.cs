namespace MyTiptop.Core
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DBContext : DbContext
    {
        public DBContext(): base("name=DataBaseConStr") { }
        public virtual DbSet<xpGrid_FuncsInRoles> xpGrid_FuncsInRoles { get; set; } 
        public virtual DbSet<xpGrid_Functions> xpGrid_Functions { get; set; } 
        public virtual DbSet<xpGrid_Role> xpGrid_Role { get; set; } 
        public virtual DbSet<xpGrid_User> xpGrid_User { get; set; } 
        public virtual DbSet<xpGrid_UsersInRoles> xpGrid_UsersInRoles { get; set; } 
        public virtual DbSet<Base_Dept> Base_Dept { get; set; } 
        public virtual DbSet<Base_Sort> Base_Sort { get; set; } 
        public virtual DbSet<Base_Status> Base_Status { get; set; } 
        public virtual DbSet<Equipment> Equipment { get; set; }
        public virtual DbSet<xpGrid_FunctionsForPublic> xpGrid_FunctionsForPublic { get; set; }
        public virtual DbSet<Base_RunGridView> Base_RunGridView { get; set; }
        public virtual DbSet<Base_RunGridView_Condition> Base_RunGridView_Condition { get; set; }
        public virtual DbSet<Base_Qry> Base_Qry { get; set; }
        public virtual DbSet<QA_Complaint> QA_Complaint { get; set; }
        public virtual DbSet<QA_Complaint_annex> QA_Complaint_annex { get; set; }

        public virtual DbSet<rvvList> rvvList { get; set; }

        public virtual DbSet<Log> Log { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<xpGrid_FuncsInRoles>()
                .Property(e => e.FuncCode)
                .IsUnicode(false);

            modelBuilder.Entity<xpGrid_Functions>()
                .Property(e => e.FuncCode)
                .IsUnicode(false);

            modelBuilder.Entity<xpGrid_Functions>()
                .Property(e => e.FuncName)
                .IsUnicode(false);

            modelBuilder.Entity<xpGrid_Functions>()
                .Property(e => e.FuncUrl)
                .IsUnicode(false);

            modelBuilder.Entity<xpGrid_Functions>()
                .Property(e => e.FuncParent)
                .IsUnicode(false);

            modelBuilder.Entity<xpGrid_Functions>()
                .Property(e => e.FuncImg)
                .IsUnicode(false);

            modelBuilder.Entity<xpGrid_Role>()
                .Property(e => e.RoleName)
                .IsUnicode(false);

            modelBuilder.Entity<xpGrid_Role>()
                .Property(e => e.RoleDes)
                .IsUnicode(false);

            modelBuilder.Entity<xpGrid_User>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<xpGrid_User>()
                .Property(e => e.UserCName)
                .IsUnicode(false);

            modelBuilder.Entity<xpGrid_User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<xpGrid_User>()
                .HasMany(e => e.xpGrid_UsersInRoles)
                .WithRequired(e => e.xpGrid_User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Base_Dept>()
                .Property(e => e.DeptName)
                .IsUnicode(false);

            modelBuilder.Entity<Base_Dept>()
                .Property(e => e.Remark)
                .IsUnicode(false);

            modelBuilder.Entity<Base_Sort>()
                .Property(e => e.SortName)
                .IsUnicode(false);

            modelBuilder.Entity<Base_Sort>()
                .Property(e => e.Remark)
                .IsUnicode(false);

            modelBuilder.Entity<Base_Status>()
                .Property(e => e.StatusName)
                .IsUnicode(false);

            modelBuilder.Entity<Base_Status>()
                .Property(e => e.Remark)
                .IsUnicode(false);

            modelBuilder.Entity<Base_Status>()
                .HasMany(e => e.Equipment)
                .WithRequired(e => e.Base_Status)
                .HasForeignKey(e => e.StatusID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Equipment>()
                .Property(e => e.Ecode)
                .IsUnicode(false);

            modelBuilder.Entity<Equipment>()
                .Property(e => e.IP)
                .IsUnicode(false);

            modelBuilder.Entity<Equipment>()
                .Property(e => e.Place)
                .IsUnicode(false);

            modelBuilder.Entity<Equipment>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<Equipment>()
                .Property(e => e.Brand)
                .IsUnicode(false);

            modelBuilder.Entity<Equipment>()
                .Property(e => e.Version)
                .IsUnicode(false);

            modelBuilder.Entity<Equipment>()
                .Property(e => e.Remark)
                .IsUnicode(false);

            modelBuilder.Entity<xpGrid_FunctionsForPublic>()
              .Property(e => e.FuncCode)
              .IsUnicode(false);

            modelBuilder.Entity<xpGrid_FunctionsForPublic>()
                .Property(e => e.FuncName)
                .IsUnicode(false);

            modelBuilder.Entity<xpGrid_FunctionsForPublic>()
                .Property(e => e.FuncUrl)
                .IsUnicode(false);

            modelBuilder.Entity<xpGrid_FunctionsForPublic>()
                .Property(e => e.FuncParent)
                .IsUnicode(false);

            modelBuilder.Entity<xpGrid_FunctionsForPublic>()
                .Property(e => e.FuncImg)
                .IsUnicode(false);

            modelBuilder.Entity<Base_RunGridView>()
                .Property(e => e.GridViewName)
                .IsUnicode(false);

            modelBuilder.Entity<Base_RunGridView>()
                .Property(e => e.strSelect)
                .IsUnicode(false);

            modelBuilder.Entity<Base_RunGridView>()
                .Property(e => e.Type)
                .IsUnicode(false);

            modelBuilder.Entity<Base_RunGridView>()
                .Property(e => e.DBType)
                .IsUnicode(false);

            modelBuilder.Entity<Base_RunGridView>()
                .Property(e => e.SqlType)
                .IsUnicode(false);

            modelBuilder.Entity<Base_RunGridView>()
                .Property(e => e.keyWord)
                .IsUnicode(false);

            modelBuilder.Entity<Base_RunGridView_Condition>()
                .Property(e => e.field)
                .IsUnicode(false);

            modelBuilder.Entity<Base_RunGridView_Condition>()
                .Property(e => e.fieldName)
                .IsUnicode(false);

            modelBuilder.Entity<Base_RunGridView_Condition>()
                .Property(e => e.formName)
                .IsUnicode(false);

            modelBuilder.Entity<Base_RunGridView_Condition>()
                .Property(e => e.iOperator)
                .IsUnicode(false);

            modelBuilder.Entity<Base_RunGridView_Condition>()
                .Property(e => e.currOperator)
                .IsUnicode(false);

            modelBuilder.Entity<Base_RunGridView_Condition>()
                .Property(e => e.currValue)
                .IsUnicode(false);

            modelBuilder.Entity<Base_RunGridView_Condition>()
                .Property(e => e.datetype)
                .IsUnicode(false);

            modelBuilder.Entity<Base_RunGridView_Condition>()
                .Property(e => e.editFormat)
                .IsUnicode(false);

            modelBuilder.Entity<Base_RunGridView_Condition>()
                .Property(e => e.Remark)
                .IsUnicode(false);

            modelBuilder.Entity<Base_Qry>()
                .Property(e => e.keyVal)
                .IsUnicode(false);

            modelBuilder.Entity<Base_Qry>()
                .Property(e => e.keyText)
                .IsUnicode(false);

            modelBuilder.Entity<Base_Qry>()
                .Property(e => e.viewType)
                .IsUnicode(false);

            modelBuilder.Entity<Base_Qry>()
                .Property(e => e.Remark)
                .IsUnicode(false);

            modelBuilder.Entity<rvvList>()
                .Property(e => e.rvv32)
                .IsUnicode(false);

            modelBuilder.Entity<rvvList>()
                .Property(e => e.rvv33)
                .IsUnicode(false);

            modelBuilder.Entity<rvvList>()
                .Property(e => e.qrcode)
                .IsUnicode(false);

            modelBuilder.Entity<rvvList>()
                .Property(e => e.qrcode2)
                .IsUnicode(false);


            modelBuilder.Entity<QA_Complaint>()
               .Property(e => e.data01)
               .IsUnicode(false);

            modelBuilder.Entity<QA_Complaint>()
                .Property(e => e.data02)
                .IsUnicode(false);

            modelBuilder.Entity<QA_Complaint>()
                .Property(e => e.data03)
                .IsUnicode(false);

            modelBuilder.Entity<QA_Complaint>()
                .Property(e => e.data04)
                .IsUnicode(false);

            modelBuilder.Entity<QA_Complaint>()
                .Property(e => e.data05)
                .IsUnicode(false);

            modelBuilder.Entity<QA_Complaint>()
                .Property(e => e.data13)
                .IsUnicode(false);

            modelBuilder.Entity<QA_Complaint>()
                .Property(e => e.data14)
                .IsUnicode(false);

            modelBuilder.Entity<QA_Complaint>()
                .Property(e => e.data15)
                .IsUnicode(false);

            modelBuilder.Entity<QA_Complaint_annex>()
               .Property(e => e.data02)
               .IsUnicode(false);

            modelBuilder.Entity<QA_Complaint_annex>()
                .Property(e => e.data03)
                .IsUnicode(false);

            modelBuilder.Entity<QA_Complaint_annex>()
                .Property(e => e.data04)
                .IsUnicode(false);

            modelBuilder.Entity<Log>()
                .Property(e => e.Thread)
                .IsUnicode(false);

            modelBuilder.Entity<Log>()
                .Property(e => e.ErrLevel)
                .IsUnicode(false);

            modelBuilder.Entity<Log>()
                .Property(e => e.Logger)
                .IsUnicode(false);

            modelBuilder.Entity<Log>()
                .Property(e => e.Message)
                .IsUnicode(false);

            modelBuilder.Entity<Log>()
                .Property(e => e.Exception)
                .IsUnicode(false);

            modelBuilder.Entity<Log>()
                .Property(e => e.Context)
                .IsUnicode(false);

        }
    }
}
