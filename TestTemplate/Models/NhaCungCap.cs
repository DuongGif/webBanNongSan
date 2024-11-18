using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestTemplate.Models
{
    public partial class NhaCungCap
    {
        [Required(ErrorMessage = "Mã nhà cung cấp là bắt buộc.")]
        public string MaNhaCungCap { get; set; } = null!;

        [Required(ErrorMessage = "Tên nhà cung cấp là bắt buộc.")]
        [StringLength(100, ErrorMessage = "Tên nhà cung cấp không được vượt quá 100 ký tự.")]
        public string? TenNhaCungCap { get; set; }

        [StringLength(200, ErrorMessage = "Địa chỉ không được vượt quá 200 ký tự.")]
        public string? DiaChi { get; set; }

        [Phone(ErrorMessage = "Số điện thoại không hợp lệ.")]
        [StringLength(10, ErrorMessage = "Số điện thoại phải có đúng 10 ký tự.")]
        [RegularExpression(@"^0\d{9}$", ErrorMessage = "Số điện thoại phải bắt đầu bằng số 0 và có 10 chữ số.")]
        public string? SoDienThoai { get; set; }


        public virtual ICollection<NongSan> NongSans { get; set; } = new List<NongSan>();
    }
}
