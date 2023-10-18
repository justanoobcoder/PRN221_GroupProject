using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects;

public class Customer
{
    [Display(Name = "ID")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập tên")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Tên phải từ 3 kí tự và không được quá 100 ký tự")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Tên không hợp lệ")]
    [Display(Name = "Tên")]
    public string FullName { get; set; } = null!;

    [Required(ErrorMessage = "Vui lòng nhập địa chỉ")]
    [StringLength(200, ErrorMessage = "Địa chỉ không được quá 200 ký tự")]
    [Display(Name = "Địa chỉ")]
    public string Address { get; set; } = null!;

    [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
    [RegularExpression(@"^(\d{7,14})$", ErrorMessage = "Số điện thoại phải từ 7 đến 14 chữ số")]
    [Display(Name = "Số điện thoại")]
    public string Phone { get; set; } = null!;

    [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
    [StringLength(30, MinimumLength = 6, ErrorMessage = "Mật khẩu phải từ 6 kí tự và không được quá 30 ký tự")]
    [Display(Name = "Mật khẩu")]
    public string Password { get; set; } = null!;

    [Required(ErrorMessage = "Vui lòng nhập email")]
    [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){1,3})+)$", ErrorMessage = "Email không hợp lệ")]
    [Display(Name = "Email")]
    public string? Email { get; set; } = null!;

    [Display(Name = "Ảnh đại diện")]
    public string? AvatarUrl { get; set; }

    [Display(Name = "Ngày tạo")]
    [DataType(DataType.DateTime)]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [Display(Name = "Bị chặn")]
    public bool IsBanned { get; set; } = false;
    
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
