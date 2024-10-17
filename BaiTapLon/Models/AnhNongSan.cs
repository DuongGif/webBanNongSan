using System;
using System.Collections.Generic;

namespace baitaplon.Models;

public partial class AnhNongSan
{
    public string MaNongSan { get; set; } = null!;

    public string DuongDanAnh { get; set; } = null!;

    public virtual NongSan MaNongSanNavigation { get; set; } = null!;
}
