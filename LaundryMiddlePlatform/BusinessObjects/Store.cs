using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects;

public class Store
{
    [Display(Name = "ID")]
    public int Id { get; set; }

    [Display(Name = "Tên cửa hàng")]
    [Required(ErrorMessage = "Tên cửa hàng không được để trống")]
    [StringLength(100, ErrorMessage = "Tên cửa hàng không được vượt quá 100 ký tự")]
    public string Name { get; set; } = null!;

    [Display(Name = "Địa chỉ")]
    [Required(ErrorMessage = "Địa chỉ không được để trống")]
    [StringLength(200, ErrorMessage = "Địa chỉ không được vượt quá 200 ký tự")]
    public string Address { get; set; } = null!;

    [Display(Name = "Số điện thoại")]
    [Required(ErrorMessage = "Số điện thoại không được để trống")]
    [RegularExpression(@"^(\d{7,14})$", ErrorMessage = "Số điện thoại phải từ 7 đến 14 chữ số")]
    public string Phone { get; set; } = null!;

    [Display(Name = "Email")]
    [Required(ErrorMessage = "Email không được để trống")]
    [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){1,3})+)$", ErrorMessage = "Email không hợp lệ")]
    public string? Email { get; set; } = null!;

    [Display(Name = "Mô tả")]
    [Required(ErrorMessage = "Mô tả không được để trống")]
    [StringLength(200, ErrorMessage = "Mô tả không được vượt quá 200 ký tự")]
    public string Description { get; set; } = null!;

    [Display(Name = "Ảnh đại diện")]
    public string? AvatarUrl { get; set; } = null!;

    [Display(Name = "Ảnh bìa")]
    public string? CoverUrl { get; set; } = null!;

    [Display(Name = "Facebook")]
    [RegularExpression(@"^(http(s)?:\/\/)?((w){3}.)?facebook.com\/[a-zA-Z0-9_\-\.]+$", ErrorMessage = "Địa chỉ Facebook không hợp lệ")]
    public string? FacebookUrl { get; set; }

    [Display(Name = "Mật khẩu")]
    [Required(ErrorMessage = "Mật khẩu không được để trống")]
    [StringLength(30, MinimumLength = 6, ErrorMessage = "Mật khẩu phải từ 6 kí tự và không được quá 30 ký tự")]
    public string Password { get; set; } = null!;

    [Display(Name = "Giờ mở cửa")]
    [Required(ErrorMessage = "Giờ mở cửa không được để trống")]
    [DataType(DataType.Time)]
    public TimeSpan OpenTime { get; set; }

    [Display(Name = "Giờ đóng cửa")]
    [Required(ErrorMessage = "Giờ đóng cửa không được để trống")]
    [DataType(DataType.Time)]
    public TimeSpan CloseTime { get; set; }

    [Display(Name = "Đang mở cửa")]
    public bool IsOpening { get; set; } = false;

    [Display(Name = "Ngày tạo")]
    [DataType(DataType.DateTime)]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [Display(Name = "Bị chặn")]
    public bool IsBanned { get; set; } = false;

    public ICollection<Machine> Machines { get; set; } = new List<Machine>();

    public ICollection<Service> Services { get; set; } = new List<Service>();
}
