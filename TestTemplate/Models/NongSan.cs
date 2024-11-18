using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; // Thêm thư viện để sử dụng Data Annotations

namespace TestTemplate.Models;

public partial class NongSan
{
    [Required(ErrorMessage = "Mã nông sản là bắt buộc.")]
    [StringLength(10, ErrorMessage = "Mã nông sản không thể dài quá 10 ký tự.")]
    public string MaNongSan { get; set; } = null!;

    [Required(ErrorMessage = "Tên nông sản là bắt buộc.")]
    [StringLength(255, ErrorMessage = "Tên nông sản không thể dài quá 255 ký tự.")]
    public string TenNongSan { get; set; } = null!;

    [Required(ErrorMessage = "Mã loại là bắt buộc.")]
    public string? MaLoai { get; set; }

    [Required(ErrorMessage = "Giá bán là bắt buộc.")]
    [Range(0, double.MaxValue, ErrorMessage = "Giá bán phải lớn hơn hoặc bằng 0.")]
    public decimal? GiaBan { get; set; }

    [Required(ErrorMessage = "Số lượng tồn kho là bắt buộc.")]
    [Range(0, int.MaxValue, ErrorMessage = "Số lượng tồn kho phải lớn hơn hoặc bằng 0.")]
    public int? SoLuongTonKho { get; set; }

    [Required(ErrorMessage = "Đơn vị tính là bắt buộc.")]
    [StringLength(50, ErrorMessage = "Đơn vị tính không thể dài quá 50 ký tự.")]
    [RegularExpression(@"^(kg|g|l|ml|m|cm|mm|in|ft)$", ErrorMessage = "Đơn vị tính không hợp lệ. Chỉ cho phép các đơn vị đo lường như kg, g, l, ml, m, cm, mm, in, ft.")]
    public string? DonViTinh { get; set; }


    [Required(ErrorMessage = "Mã nhà cung cấp là bắt buộc.")]
    public string? MaNhaCungCap { get; set; }

    [StringLength(255, ErrorMessage = "Đường dẫn ảnh không thể dài quá 255 ký tự.")]
    public string? DuongDanAnh { get; set; }

    public virtual Kho? Kho { get; set; }

    public virtual ICollection<KhuyenMai> KhuyenMais { get; set; } = new List<KhuyenMai>();

    public virtual LoaiNongSan? MaLoaiNavigation { get; set; }

    public virtual NhaCungCap? MaNhaCungCapNavigation { get; set; }

    public virtual NguonGoc? NguonGoc { get; set; }
}
