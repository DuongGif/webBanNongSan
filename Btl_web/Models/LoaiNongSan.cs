using System;
using System.Collections.Generic;

namespace Btl_web.Models
{
    public partial class LoaiNongSan
    {
        public LoaiNongSan()
        {
            NongSans = new HashSet<NongSan>();
        }

        public string MaLoai { get; set; } = null!;
        public string? TenLoai { get; set; }

        public virtual ICollection<NongSan> NongSans { get; set; }
    }
}
