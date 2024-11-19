using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestTemplate.Models;

public partial class TaiKhoan
{
    public string MaTaiKhoan { get; set; } = null!;

    public string? MaKhachHang { get; set; }

    public string MaKieuTaiKhoan { get; set; } = "KT02"; // Mặc định là KT02

   
    public string? TenTaiKhoan { get; set; }
   
    public string? MatKhau { get; set; }


    public DateTime NgayTao { get; set; } = DateTime.Now; // Mặc định là ngày hiện tại

    public virtual KhachHang? MaKhachHangNavigation { get; set; }

    public virtual KieuTaiKhoan? MaKieuTaiKhoanNavigation { get; set; }

    // Trường này chỉ dùng cho xác nhận mật khẩu trong quá trình đăng ký, không lưu vào DB
    [NotMapped]  // Thêm annotation này để Entity Framework bỏ qua cột này khi tạo bảng
    public string ConfirmMatKhau { get; set; }
}