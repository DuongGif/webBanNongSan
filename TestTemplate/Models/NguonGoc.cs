using System;
using System.Collections.Generic;

namespace TestTemplate.Models;

public partial class NguonGoc
{
    public string MaNongSan { get; set; } = null!;

    public string? KhuVuc { get; set; }

    public string? PhuongPhap { get; set; }

    public virtual NongSan MaNongSanNavigation { get; set; } = null!;
}
