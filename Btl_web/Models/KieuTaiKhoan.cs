using System;
using System.Collections.Generic;

namespace Btl_web.Models
{
    public partial class KieuTaiKhoan
    {
        public KieuTaiKhoan()
        {
            TaiKhoans = new HashSet<TaiKhoan>();
        }

        public string MaKieuTaiKhoan { get; set; } = null!;
        public string? TenKieuTaiKhoan { get; set; }

        public virtual ICollection<TaiKhoan> TaiKhoans { get; set; }
    }
}
