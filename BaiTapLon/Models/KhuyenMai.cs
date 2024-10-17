using System;
using System.Collections.Generic;

namespace baitaplon.Models;

public partial class KhuyenMai
{
    public string MaKhuyenMai { get; set; } = null!;

    public string? MaNongSan { get; set; }

    public string? MoTa { get; set; }

    public DateTime? NgayBatDau { get; set; }

    public DateTime? NgayKetThuc { get; set; }

    public virtual NongSan? MaNongSanNavigation { get; set; }
}
