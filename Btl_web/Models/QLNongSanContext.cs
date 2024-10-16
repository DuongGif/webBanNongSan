using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Btl_web.Models
{
    public partial class QLNongSanContext : DbContext
    {
        public QLNongSanContext()
        {
        }

        public QLNongSanContext(DbContextOptions<QLNongSanContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AnhNongSan> AnhNongSans { get; set; } = null!;
        public virtual DbSet<KhachHang> KhachHangs { get; set; } = null!;
        public virtual DbSet<Kho> Khos { get; set; } = null!;
        public virtual DbSet<KhuyenMai> KhuyenMais { get; set; } = null!;
        public virtual DbSet<KieuTaiKhoan> KieuTaiKhoans { get; set; } = null!;
        public virtual DbSet<LoaiNongSan> LoaiNongSans { get; set; } = null!;
        public virtual DbSet<NguonGoc> NguonGocs { get; set; } = null!;
        public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; } = null!;
        public virtual DbSet<NongSan> NongSans { get; set; } = null!;
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=MSI\\SQLEXPRESS;Initial Catalog=QLNongSan;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AnhNongSan>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("AnhNongSan");

                entity.Property(e => e.DuongDanAnh)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.MaNongSan)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.MaNongSanNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.MaNongSan)
                    .HasConstraintName("FK__AnhNongSa__MaNon__3E52440B");
            });

            modelBuilder.Entity<KhachHang>(entity =>
            {
                entity.HasKey(e => e.MaKhachHang)
                    .HasName("PK__KhachHan__88D2F0E54C205FE6");

                entity.ToTable("KhachHang");

                entity.Property(e => e.MaKhachHang)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DiaChi)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SoDienThoai)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.TenKhachHang)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Kho>(entity =>
            {
                entity.HasKey(e => e.MaNongSan)
                    .HasName("PK__Kho__948BD1BC8FBE2433");

                entity.ToTable("Kho");

                entity.Property(e => e.MaNongSan)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.NgayCapNhat).HasColumnType("date");

                entity.HasOne(d => d.MaNongSanNavigation)
                    .WithOne(p => p.Kho)
                    .HasForeignKey<Kho>(d => d.MaNongSan)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Kho__MaNongSan__46E78A0C");
            });

            modelBuilder.Entity<KhuyenMai>(entity =>
            {
                entity.HasKey(e => e.MaKhuyenMai)
                    .HasName("PK__KhuyenMa__6F56B3BDC26E949A");

                entity.ToTable("KhuyenMai");

                entity.Property(e => e.MaKhuyenMai)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.MaNongSan)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.MoTa)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.NgayBatDau).HasColumnType("date");

                entity.Property(e => e.NgayKetThuc).HasColumnType("date");

                entity.HasOne(d => d.MaNongSanNavigation)
                    .WithMany(p => p.KhuyenMais)
                    .HasForeignKey(d => d.MaNongSan)
                    .HasConstraintName("FK__KhuyenMai__MaNon__440B1D61");
            });

            modelBuilder.Entity<KieuTaiKhoan>(entity =>
            {
                entity.HasKey(e => e.MaKieuTaiKhoan)
                    .HasName("PK__KieuTaiK__50D01A6F93EBA406");

                entity.ToTable("KieuTaiKhoan");

                entity.Property(e => e.MaKieuTaiKhoan)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TenKieuTaiKhoan)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LoaiNongSan>(entity =>
            {
                entity.HasKey(e => e.MaLoai)
                    .HasName("PK__LoaiNong__730A57598652720B");

                entity.ToTable("LoaiNongSan");

                entity.Property(e => e.MaLoai)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TenLoai).HasMaxLength(50);
            });

            modelBuilder.Entity<NguonGoc>(entity =>
            {
                entity.HasKey(e => e.MaNongSan)
                    .HasName("PK__NguonGoc__948BD1BC6CDCAE8C");

                entity.ToTable("NguonGoc");

                entity.Property(e => e.MaNongSan)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.KhuVuc)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PhuongPhap)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.MaNongSanNavigation)
                    .WithOne(p => p.NguonGoc)
                    .HasForeignKey<NguonGoc>(d => d.MaNongSan)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__NguonGoc__MaNong__412EB0B6");
            });

            modelBuilder.Entity<NhaCungCap>(entity =>
            {
                entity.HasKey(e => e.MaNhaCungCap)
                    .HasName("PK__NhaCungC__53DA920524520C9B");

                entity.ToTable("NhaCungCap");

                entity.Property(e => e.MaNhaCungCap)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DiaChi).HasMaxLength(255);

                entity.Property(e => e.SoDienThoai)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.TenNhaCungCap).HasMaxLength(100);
            });

            modelBuilder.Entity<NongSan>(entity =>
            {
                entity.HasKey(e => e.MaNongSan)
                    .HasName("PK__NongSan__948BD1BC654C4823");

                entity.ToTable("NongSan");

                entity.Property(e => e.MaNongSan)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DonViTinh).HasMaxLength(50);

                entity.Property(e => e.GiaBan).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MaLoai)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.MaNhaCungCap)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TenNongSan).HasMaxLength(255);

                entity.HasOne(d => d.MaLoaiNavigation)
                    .WithMany(p => p.NongSans)
                    .HasForeignKey(d => d.MaLoai)
                    .HasConstraintName("FK__NongSan__MaLoai__3B75D760");

                entity.HasOne(d => d.MaNhaCungCapNavigation)
                    .WithMany(p => p.NongSans)
                    .HasForeignKey(d => d.MaNhaCungCap)
                    .HasConstraintName("FK__NongSan__MaNhaCu__3C69FB99");
            });

            modelBuilder.Entity<TaiKhoan>(entity =>
            {
                entity.HasKey(e => e.MaTaiKhoan)
                    .HasName("PK__TaiKhoan__AD7C6529AA9E75F8");

                entity.ToTable("TaiKhoan");

                entity.Property(e => e.MaTaiKhoan)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.MaKhachHang)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.MaKieuTaiKhoan)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.MatKhau)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NgayTao)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TenTaiKhoan)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.MaKhachHangNavigation)
                    .WithMany(p => p.TaiKhoans)
                    .HasForeignKey(d => d.MaKhachHang)
                    .HasConstraintName("FK__TaiKhoan__MaKhac__4E88ABD4");

                entity.HasOne(d => d.MaKieuTaiKhoanNavigation)
                    .WithMany(p => p.TaiKhoans)
                    .HasForeignKey(d => d.MaKieuTaiKhoan)
                    .HasConstraintName("FK__TaiKhoan__MaKieu__4F7CD00D");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
