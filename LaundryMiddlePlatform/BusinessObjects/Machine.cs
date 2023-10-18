using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects;

public class Machine
{
    [Display(Name = "ID")]
    public int Id { get; set; }

    [Display(Name = "Tên máy")]
    [Required(ErrorMessage = "Tên máy không được để trống")]
    [StringLength(100, ErrorMessage = "Tên máy không được vượt quá 100 ký tự")]
    [RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "Tên máy không được chứa ký tự đặc biệt")]
    public string Name { get; set; } = null!;

    [Display(Name = "Mô tả")]
    [StringLength(200, ErrorMessage = "Mô tả không được vượt quá 200 ký tự")]
    public string? Description { get; set; }

    [Display(Name = "Hãng")]
    [Required(ErrorMessage = "Hãng không được để trống")]
    [StringLength(100, ErrorMessage = "Hãng không được vượt quá 100 ký tự")]
    public string Brand { get; set; } = null!;

    [Display(Name = "Mẫu máy")]
    [Required(ErrorMessage = "Mẫu máy không được để trống")]
    [StringLength(100, ErrorMessage = "Mẫu máy không được vượt quá 100 ký tự")]
    public string Model { get; set; } = null!;

    [Display(Name = "Dung tích")]
    [Required(ErrorMessage = "Dung tích không được để trống")]
    [Range(1, 20, ErrorMessage = "Dung tích phải nằm trong khoảng từ 1 đến 20")]
    public float Capacity { get; set; }

    [Display(Name = "Trạng thái")]
    public bool IsAvailable { get; set; } = true;

    [Display(Name = "Thời gian giặt")]
    [Required(ErrorMessage = "Thời gian giặt không được để trống")]
    [Range(10, 30, ErrorMessage = "Thời gian giặt phải nằm trong khoảng từ 10 đến 30")]
    public float WashTimeInMinute { get; set; }

    public int StoreId { get; set; }
}
