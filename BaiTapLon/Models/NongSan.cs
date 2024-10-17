using System;
using System.Collections.Generic;

namespace baitaplon.Models;

public partial class NongSan
{
    public string MaNongSan { get; set; } = null!;

    public string TenNongSan { get; set; } = null!;

    public string? MaLoai { get; set; }

    public decimal? GiaBan { get; set; }

    public int? SoLuongTonKho { get; set; }

    public string? DonViTinh { get; set; }

    public string? MaNhaCungCap { get; set; }

    public string? DuongDanAnh { get; set; }

    public virtual Kho? Kho { get; set; }

    public virtual ICollection<KhuyenMai> KhuyenMais { get; set; } = new List<KhuyenMai>();

    public virtual LoaiNongSan? MaLoaiNavigation { get; set; }

    public virtual NhaCungCap? MaNhaCungCapNavigation { get; set; }

    public virtual NguonGoc? NguonGoc { get; set; }
}
