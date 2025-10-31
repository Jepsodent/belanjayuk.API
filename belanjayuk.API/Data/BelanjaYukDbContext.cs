using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using belanjayuk.API.Models.Entities;

namespace belanjayuk.API.Data;

public partial class BelanjaYukDbContext : DbContext
{
    public BelanjaYukDbContext()
    {
    }

    public BelanjaYukDbContext(DbContextOptions<BelanjaYukDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DwmonthlySale> DwmonthlySales { get; set; }

    public virtual DbSet<LtCategory> LtCategories { get; set; }

    public virtual DbSet<LtGender> LtGenders { get; set; }

    public virtual DbSet<LtPayment> LtPayments { get; set; }

    public virtual DbSet<MsProduct> MsProducts { get; set; }

    public virtual DbSet<MsUser> MsUsers { get; set; }

    public virtual DbSet<MsUserPassword> MsUserPasswords { get; set; }

    public virtual DbSet<MsUserSeller> MsUserSellers { get; set; }

    public virtual DbSet<TrBuyerCart> TrBuyerCarts { get; set; }

    public virtual DbSet<TrBuyerTransaction> TrBuyerTransactions { get; set; }

    public virtual DbSet<TrBuyerTransactionDetail> TrBuyerTransactionDetails { get; set; }

    public virtual DbSet<TrHomeAddress> TrHomeAddresses { get; set; }

    public virtual DbSet<TrProductImage> TrProductImages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=belanjayuk;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DwmonthlySale>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DWMonthl__3214EC079D96CF55");

            entity.ToTable("DWMonthlySales");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.AvgRating).HasColumnType("decimal(3, 2)");
            entity.Property(e => e.LoadDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.MonthYear)
                .HasMaxLength(7)
                .IsUnicode(false);
            entity.Property(e => e.SellerId).HasMaxLength(36);
            entity.Property(e => e.TotalSales).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<LtCategory>(entity =>
        {
            entity.HasKey(e => e.IdCategory);

            entity.ToTable("LtCategory");

            entity.Property(e => e.IdCategory).HasMaxLength(36);
            entity.Property(e => e.CategoryName).HasMaxLength(100);
            entity.Property(e => e.DateIn).HasColumnType("datetime");
            entity.Property(e => e.DateUp).HasColumnType("datetime");
            entity.Property(e => e.UserIn).HasMaxLength(36);
            entity.Property(e => e.UserUp).HasMaxLength(36);
        });

        modelBuilder.Entity<LtGender>(entity =>
        {
            entity.HasKey(e => e.IdGender);

            entity.ToTable("LtGender");

            entity.Property(e => e.IdGender).HasMaxLength(36);
            entity.Property(e => e.DateIn).HasColumnType("datetime");
            entity.Property(e => e.DateUp).HasColumnType("datetime");
            entity.Property(e => e.GenderName).HasMaxLength(50);
            entity.Property(e => e.UserIn).HasMaxLength(36);
            entity.Property(e => e.UserUp).HasMaxLength(36);
        });

        modelBuilder.Entity<LtPayment>(entity =>
        {
            entity.HasKey(e => e.IdPayment);

            entity.ToTable("LtPayment");

            entity.Property(e => e.IdPayment).HasMaxLength(36);
            entity.Property(e => e.DateIn).HasColumnType("datetime");
            entity.Property(e => e.DateUp).HasColumnType("datetime");
            entity.Property(e => e.PaymentName).HasMaxLength(100);
            entity.Property(e => e.UserIn).HasMaxLength(36);
            entity.Property(e => e.UserUp).HasMaxLength(36);
        });

        modelBuilder.Entity<MsProduct>(entity =>
        {
            entity.HasKey(e => e.IdProduct);

            entity.ToTable("MsProduct");

            entity.HasIndex(e => e.IdCategory, "IX_MsProduct_IdCategory").HasFillFactor(100);

            entity.HasIndex(e => e.IdUserSeller, "IX_MsProduct_IdUserSeller").HasFillFactor(100);

            entity.Property(e => e.IdProduct).HasMaxLength(36);
            entity.Property(e => e.DateIn).HasColumnType("datetime");
            entity.Property(e => e.DateUp).HasColumnType("datetime");
            entity.Property(e => e.Discount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.IdCategory).HasMaxLength(36);
            entity.Property(e => e.IdUserSeller).HasMaxLength(36);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ProductDesc).HasMaxLength(2000);
            entity.Property(e => e.ProductName).HasMaxLength(100);
            entity.Property(e => e.Stock).HasDefaultValue(0);
            entity.Property(e => e.UserIn).HasMaxLength(36);
            entity.Property(e => e.UserUp).HasMaxLength(36);

            entity.HasOne(d => d.IdCategoryNavigation).WithMany(p => p.MsProducts)
                .HasForeignKey(d => d.IdCategory)
                .HasConstraintName("FK_MsProduct_LtCategory");

            entity.HasOne(d => d.IdUserSellerNavigation).WithMany(p => p.MsProducts)
                .HasForeignKey(d => d.IdUserSeller)
                .HasConstraintName("FK_MsProduct_MsUserSeller");
        });

        modelBuilder.Entity<MsUser>(entity =>
        {
            entity.HasKey(e => e.IdUser);

            entity.ToTable("MsUser");

            entity.HasIndex(e => e.IdGender, "IX_MsUser_IdGender").HasFillFactor(100);

            entity.Property(e => e.IdUser).HasMaxLength(36);
            entity.Property(e => e.DateIn).HasColumnType("datetime");
            entity.Property(e => e.DateUp).HasColumnType("datetime");
            entity.Property(e => e.Dob)
                .HasColumnType("datetime")
                .HasColumnName("DOB");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.IdGender).HasMaxLength(36);
            entity.Property(e => e.LastName).HasMaxLength(200);
            entity.Property(e => e.PhoneNumber).HasMaxLength(50);
            entity.Property(e => e.UserIn).HasMaxLength(36);
            entity.Property(e => e.UserName).HasMaxLength(100);
            entity.Property(e => e.UserUp).HasMaxLength(36);

            entity.HasOne(d => d.IdGenderNavigation).WithMany(p => p.MsUsers)
                .HasForeignKey(d => d.IdGender)
                .HasConstraintName("FK_MsUser_LtGender");
        });

        modelBuilder.Entity<MsUserPassword>(entity =>
        {
            entity.HasKey(e => e.IdUserPassword);

            entity.ToTable("MsUserPassword");

            entity.HasIndex(e => e.IdUser, "IX_MsUserPassword_IdUser").HasFillFactor(100);

            entity.Property(e => e.IdUserPassword).HasMaxLength(36);
            entity.Property(e => e.DateIn).HasColumnType("datetime");
            entity.Property(e => e.DateUp).HasColumnType("datetime");
            entity.Property(e => e.IdUser).HasMaxLength(36);
            entity.Property(e => e.PasswordHashed).HasMaxLength(200);
            entity.Property(e => e.UserIn).HasMaxLength(36);
            entity.Property(e => e.UserUp).HasMaxLength(36);

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.MsUserPasswords)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK_MsUserPassword_MsUser");
        });

        modelBuilder.Entity<MsUserSeller>(entity =>
        {
            entity.HasKey(e => e.IdUserSeller);

            entity.ToTable("MsUserSeller");

            entity.HasIndex(e => e.IdUser, "IX_MsUserSeller_IdUser").HasFillFactor(100);

            entity.Property(e => e.IdUserSeller).HasMaxLength(36);
            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.DateIn).HasColumnType("datetime");
            entity.Property(e => e.DateUp).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.IdUser).HasMaxLength(36);
            entity.Property(e => e.PhoneNumber).HasMaxLength(50);
            entity.Property(e => e.SellerDesc).HasMaxLength(200);
            entity.Property(e => e.StoreName).HasMaxLength(100);
            entity.Property(e => e.UserIn).HasMaxLength(36);
            entity.Property(e => e.UserUp).HasMaxLength(36);

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.MsUserSellers)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK_MsUserSeller_MsUser");
        });

        modelBuilder.Entity<TrBuyerCart>(entity =>
        {
            entity.HasKey(e => e.IdBuyerCart);

            entity.ToTable("TrBuyerCart");

            entity.HasIndex(e => e.IdProduct, "IX_TrBuyerCart_IdProduct").HasFillFactor(100);

            entity.HasIndex(e => e.IdUser, "IX_TrBuyerCart_IdUser").HasFillFactor(100);

            entity.Property(e => e.IdBuyerCart).HasMaxLength(36);
            entity.Property(e => e.DateIn).HasColumnType("datetime");
            entity.Property(e => e.DateUp).HasColumnType("datetime");
            entity.Property(e => e.IdProduct).HasMaxLength(36);
            entity.Property(e => e.IdUser).HasMaxLength(36);
            entity.Property(e => e.UserIn).HasMaxLength(36);
            entity.Property(e => e.UserUp).HasMaxLength(36);

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.TrBuyerCarts)
                .HasForeignKey(d => d.IdProduct)
                .HasConstraintName("FK_TrBuyerCart_MsProduct");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.TrBuyerCarts)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK_TrBuyerCart_MsUser");
        });

        modelBuilder.Entity<TrBuyerTransaction>(entity =>
        {
            entity.HasKey(e => e.IdBuyerTransaction);

            entity.ToTable("TrBuyerTransaction");

            entity.HasIndex(e => e.IdPayment, "IX_TrBuyerTransaction_IdPayment").HasFillFactor(100);

            entity.HasIndex(e => e.IdUser, "IX_TrBuyerTransaction_IdUser").HasFillFactor(100);

            entity.Property(e => e.IdBuyerTransaction).HasMaxLength(36);
            entity.Property(e => e.DateIn).HasColumnType("datetime");
            entity.Property(e => e.DateUp).HasColumnType("datetime");
            entity.Property(e => e.FinalPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.IdPayment).HasMaxLength(36);
            entity.Property(e => e.IdUser).HasMaxLength(36);
            entity.Property(e => e.RatingComment).HasMaxLength(1000);
            entity.Property(e => e.UserIn).HasMaxLength(36);
            entity.Property(e => e.UserUp).HasMaxLength(36);

            entity.HasOne(d => d.IdPaymentNavigation).WithMany(p => p.TrBuyerTransactions)
                .HasForeignKey(d => d.IdPayment)
                .HasConstraintName("FK_TrBuyerTransaction_LtPayment");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.TrBuyerTransactions)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK_TrBuyerTransaction_MsUser");
        });

        modelBuilder.Entity<TrBuyerTransactionDetail>(entity =>
        {
            entity.HasKey(e => e.IdBuyerTransactionDetail);

            entity.ToTable("TrBuyerTransactionDetail");

            entity.HasIndex(e => e.IdBuyerTransaction, "IX_TrBTD_IdBuyerTransaction").HasFillFactor(100);

            entity.HasIndex(e => e.IdProduct, "IX_TrBTD_IdProduct").HasFillFactor(100);

            entity.Property(e => e.IdBuyerTransactionDetail).HasMaxLength(36);
            entity.Property(e => e.DateIn).HasColumnType("datetime");
            entity.Property(e => e.DateUp).HasColumnType("datetime");
            entity.Property(e => e.DiscountProduct).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.IdBuyerTransaction).HasMaxLength(36);
            entity.Property(e => e.IdProduct).HasMaxLength(36);
            entity.Property(e => e.PriceOfProduct).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.RatingComment).HasMaxLength(1000);
            entity.Property(e => e.UserIn).HasMaxLength(36);
            entity.Property(e => e.UserUp).HasMaxLength(36);

            entity.HasOne(d => d.IdBuyerTransactionNavigation).WithMany(p => p.TrBuyerTransactionDetails)
                .HasForeignKey(d => d.IdBuyerTransaction)
                .HasConstraintName("FK_TrBTD_TrBuyerTransaction");

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.TrBuyerTransactionDetails)
                .HasForeignKey(d => d.IdProduct)
                .HasConstraintName("FK_TrBTD_MsProduct");
        });

        modelBuilder.Entity<TrHomeAddress>(entity =>
        {
            entity.HasKey(e => e.IdHomeAddress);

            entity.ToTable("TrHomeAddress");

            entity.HasIndex(e => e.IdUser, "IX_TrHomeAddress_IdUser").HasFillFactor(100);

            entity.Property(e => e.IdHomeAddress).HasMaxLength(36);
            entity.Property(e => e.DateIn).HasColumnType("datetime");
            entity.Property(e => e.DateUp).HasColumnType("datetime");
            entity.Property(e => e.HomeAddressDesc).HasMaxLength(2000);
            entity.Property(e => e.IdUser).HasMaxLength(36);
            entity.Property(e => e.Kecamatan).HasMaxLength(100);
            entity.Property(e => e.KodePos).HasMaxLength(20);
            entity.Property(e => e.KotaKabupaten).HasMaxLength(100);
            entity.Property(e => e.Provinsi).HasMaxLength(100);
            entity.Property(e => e.UserIn).HasMaxLength(36);
            entity.Property(e => e.UserUp).HasMaxLength(36);

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.TrHomeAddresses)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK_TrHomeAddress_MsUser");
        });

        modelBuilder.Entity<TrProductImage>(entity =>
        {
            entity.HasKey(e => e.IdProductImages);

            entity.HasIndex(e => e.IdProduct, "IX_TrProductImages_IdProduct").HasFillFactor(100);

            entity.Property(e => e.IdProductImages).HasMaxLength(36);
            entity.Property(e => e.DateIn).HasColumnType("datetime");
            entity.Property(e => e.DateUp).HasColumnType("datetime");
            entity.Property(e => e.IdProduct).HasMaxLength(36);
            entity.Property(e => e.UserIn).HasMaxLength(36);
            entity.Property(e => e.UserUp).HasMaxLength(36);

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.TrProductImages)
                .HasForeignKey(d => d.IdProduct)
                .HasConstraintName("FK_TrProductImages_MsProduct");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
