using System;
using System.Collections.Generic;

namespace Btl_web.Models
{
    public partial class TaiKhoan
    {
        public string MaTaiKhoan { get; set; } = null!;
        public string? MaKhachHang { get; set; }
        public string? MaKieuTaiKhoan { get; set; }
        public string? TenTaiKhoan { get; set; }
        public string? MatKhau { get; set; }
        public DateTime? NgayTao { get; set; }

        public virtual KhachHang? MaKhachHangNavigation { get; set; }
        public virtual KieuTaiKhoan? MaKieuTaiKhoanNavigation { get; set; }
    }
}
