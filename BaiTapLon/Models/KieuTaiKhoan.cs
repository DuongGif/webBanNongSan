using System;
using System.Collections.Generic;

namespace baitaplon.Models;

public partial class KieuTaiKhoan
{
    public string MaKieuTaiKhoan { get; set; } = null!;

    public string? TenKieuTaiKhoan { get; set; }

    public virtual ICollection<TaiKhoan> TaiKhoans { get; set; } = new List<TaiKhoan>();
}
