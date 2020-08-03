namespace MyTiptop.SupplierData
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DBContext : DbContext
    {
        public DBContext(): base("name=supplierConstr"){}

        public virtual DbSet<PN> PN { get; set; }
        public virtual DbSet<PNSUB> PNSUB { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PN>()
                .Property(e => e.DNNUM)
                .IsUnicode(false);

            modelBuilder.Entity<PN>()
                .Property(e => e.PMN33)
                .IsUnicode(false);

            modelBuilder.Entity<PN>()
                .Property(e => e.SUPID)
                .IsUnicode(false);

            modelBuilder.Entity<PN>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<PN>()
                .Property(e => e.CHANGE_USER)
                .IsUnicode(false);

            modelBuilder.Entity<PN>()
                .Property(e => e.CHANGE_TIME)
                .IsUnicode(false);

            modelBuilder.Entity<PN>()
                .Property(e => e.PLANT)
                .IsUnicode(false);


            modelBuilder.Entity<PNSUB>()
                .Property(e => e.PMM01)
                .IsUnicode(false);

            modelBuilder.Entity<PNSUB>()
                .Property(e => e.PMN04)
                .IsUnicode(false);

            modelBuilder.Entity<PNSUB>()
                .Property(e => e.PMN041)
                .IsUnicode(false);

            modelBuilder.Entity<PNSUB>()
                .Property(e => e.IMA021)
                .IsUnicode(false);

            modelBuilder.Entity<PNSUB>()
                .Property(e => e.PMN07)
                .IsUnicode(false);

            modelBuilder.Entity<PNSUB>()
                .Property(e => e.PMN20)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PNSUB>()
                .Property(e => e.PMN86)
                .IsUnicode(false);

            modelBuilder.Entity<PNSUB>()
                .Property(e => e.PMN87)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PNSUB>()
                .Property(e => e.SDNNUM)
                .IsUnicode(false);
        }
    }
}
