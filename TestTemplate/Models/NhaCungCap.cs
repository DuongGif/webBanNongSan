using System;
using System.Collections.Generic;

namespace TestTemplate.Models;

public partial class NhaCungCap
{
    public string MaNhaCungCap { get; set; } = null!;

    public string? TenNhaCungCap { get; set; }

    public string? DiaChi { get; set; }

    public string? SoDienThoai { get; set; }

    public virtual ICollection<NongSan> NongSans { get; set; } = new List<NongSan>();
}
