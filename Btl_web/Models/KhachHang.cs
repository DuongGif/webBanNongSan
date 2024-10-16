using System;
using System.Collections.Generic;

namespace Btl_web.Models
{
    public partial class KhachHang
    {
        public KhachHang()
        {
            TaiKhoans = new HashSet<TaiKhoan>();
        }

        public string MaKhachHang { get; set; } = null!;
        public string? TenKhachHang { get; set; }
        public string? DiaChi { get; set; }
        public string? SoDienThoai { get; set; }
        public string? Email { get; set; }

        public virtual ICollection<TaiKhoan> TaiKhoans { get; set; }
    }
}
