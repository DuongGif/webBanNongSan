using System;
using System.Collections.Generic;

namespace Btl_web.Models
{
    public partial class NhaCungCap
    {
        public NhaCungCap()
        {
            NongSans = new HashSet<NongSan>();
        }

        public string MaNhaCungCap { get; set; } = null!;
        public string? TenNhaCungCap { get; set; }
        public string? DiaChi { get; set; }
        public string? SoDienThoai { get; set; }

        public virtual ICollection<NongSan> NongSans { get; set; }
    }
}
