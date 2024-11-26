using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace QLNHxMVC.Models;

public partial class LatMvcQlnhContext : DbContext
{
    public LatMvcQlnhContext()
    {
    }

    public LatMvcQlnhContext(DbContextOptions<LatMvcQlnhContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TbAccount> TbAccounts { get; set; }

    public virtual DbSet<TbBillDetail> TbBillDetails { get; set; }

    public virtual DbSet<TbBillHistory> TbBillHistories { get; set; }

    public virtual DbSet<TbDmfood> TbDmfoods { get; set; }

    public virtual DbSet<TbDstable> TbDstables { get; set; }

    public virtual DbSet<TbFood> TbFoods { get; set; }

    public virtual DbSet<TbReport> TbReports { get; set; }

    public virtual DbSet<TbRevenu> TbRevenus { get; set; }

    public virtual DbSet<TbUserInfo> TbUserInfos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-5RGHKJ2R\\SQLEXPRESS01;Initial Catalog=LAT_MvcQLNH;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TbAccount>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__tbAccoun__349DA5863BD441DF");

            entity.ToTable("tbAccount");

            entity.HasIndex(e => e.Username, "UQ__tbAccoun__536C85E4BFA98E7D").IsUnique();

            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.AccountType).HasMaxLength(20);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        modelBuilder.Entity<TbBillDetail>(entity =>
        {
            entity.HasKey(e => e.BillId).HasName("PK__tbBillDe__11F2FC4A80529F41");

            entity.ToTable("tbBillDetails");

            entity.Property(e => e.BillId).HasColumnName("BillID");
            entity.Property(e => e.CustomerName).HasMaxLength(100);
            entity.Property(e => e.FoodName).HasMaxLength(100);
            entity.Property(e => e.Sdt).HasColumnName("SDT");
            entity.Property(e => e.TableName).HasMaxLength(50);
        });

        modelBuilder.Entity<TbBillHistory>(entity =>
        {
            entity.HasKey(e => e.BillId).HasName("PK__tbBillHi__11F2FC4AFB82997D");

            entity.ToTable("tbBillHistory");

            entity.Property(e => e.BillId).HasColumnName("BillID");
            entity.Property(e => e.CustomerName).HasMaxLength(100);
            entity.Property(e => e.FoodName).HasMaxLength(100);
            entity.Property(e => e.Sdt).HasColumnName("SDT");
            entity.Property(e => e.TableName).HasMaxLength(50);
            entity.Property(e => e.UserInfoId).HasColumnName("UserInfoID");

            entity.HasOne(d => d.UserInfo).WithMany(p => p.TbBillHistories)
                .HasForeignKey(d => d.UserInfoId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__tbBillHis__UserI__440B1D61");
        });

        modelBuilder.Entity<TbDmfood>(entity =>
        {
            entity.HasKey(e => e.DmfoodId).HasName("PK__tbDMFood__3DFF7D6B6DC27877");

            entity.ToTable("tbDMFood");

            entity.Property(e => e.DmfoodId).HasColumnName("DMFoodID");
            entity.Property(e => e.CategoryName).HasMaxLength(100);
        });

        modelBuilder.Entity<TbDstable>(entity =>
        {
            entity.HasKey(e => e.TableId).HasName("PK__tbDSTabl__7D5F018EC58D2215");

            entity.ToTable("tbDSTable");

            entity.Property(e => e.TableId).HasColumnName("TableID");
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.TableName).HasMaxLength(50);
        });

        modelBuilder.Entity<TbFood>(entity =>
        {
            entity.HasKey(e => e.FoodId).HasName("PK__tbFood__856DB3CB72C69D39");

            entity.ToTable("tbFood");

            entity.Property(e => e.FoodId).HasColumnName("FoodID");
            entity.Property(e => e.AvtFood)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.DmfoodId).HasColumnName("DMFoodID");
            entity.Property(e => e.FoodName).HasMaxLength(100);

            entity.HasOne(d => d.Dmfood).WithMany(p => p.TbFoods)
                .HasForeignKey(d => d.DmfoodId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__tbFood__DMFoodID__3F466844");
        });

        modelBuilder.Entity<TbReport>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("PK__tbReport__D5BD48E54D2E58DB");

            entity.ToTable("tbReport");

            entity.Property(e => e.ReportId).HasColumnName("ReportID");
            entity.Property(e => e.DateCheckin)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DateCheckout).HasColumnType("datetime");
            entity.Property(e => e.Idbill).HasColumnName("IDBill");
            entity.Property(e => e.Slban).HasColumnName("SLBan");

            entity.HasOne(d => d.IdbillNavigation).WithMany(p => p.TbReports)
                .HasForeignKey(d => d.Idbill)
                .HasConstraintName("FK__tbReport__IDBill__4CA06362");

            entity.HasOne(d => d.SlbanNavigation).WithMany(p => p.TbReports)
                .HasForeignKey(d => d.Slban)
                .HasConstraintName("FK__tbReport__SLBan__4D94879B");
        });

        modelBuilder.Entity<TbRevenu>(entity =>
        {
            entity.HasKey(e => e.RevenuId).HasName("PK__tbRevenu__FBB5DE1D9EE7BADC");

            entity.ToTable("tbRevenu");

            entity.Property(e => e.RevenuId).HasColumnName("RevenuID");

            entity.HasOne(d => d.SlTableNavigation).WithMany(p => p.TbRevenus)
                .HasForeignKey(d => d.SlTable)
                .HasConstraintName("FK__tbRevenu__SlTabl__48CFD27E");
        });

        modelBuilder.Entity<TbUserInfo>(entity =>
        {
            entity.HasKey(e => e.UserInfoId).HasName("PK__tbUserIn__D07EF2C4BDBB886F");

            entity.ToTable("tbUserInfo");

            entity.Property(e => e.UserInfoId).HasColumnName("UserInfoID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);

            entity.HasOne(d => d.Account).WithMany(p => p.TbUserInfos)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__tbUserInf__Accou__3A81B327");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
