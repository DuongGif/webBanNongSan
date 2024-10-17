using System;
using System.Collections.Generic;

namespace baitaplon.Models;

public partial class LoaiNongSan
{
    public string MaLoai { get; set; } = null!;

    public string? TenLoai { get; set; }

    public virtual ICollection<NongSan> NongSans { get; set; } = new List<NongSan>();
}
