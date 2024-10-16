using System;
using System.Collections.Generic;

namespace Btl_web.Models
{
    public partial class Kho
    {
        public string MaNongSan { get; set; } = null!;
        public int? SoLuongTonKho { get; set; }
        public DateTime? NgayCapNhat { get; set; }

        public virtual NongSan MaNongSanNavigation { get; set; } = null!;
    }
}
