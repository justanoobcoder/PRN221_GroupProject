using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects;

public class Service
{
    [Display(Name = "Mã dịch vụ")]
    public int Id { get; set; }

    [Display(Name = "Tên dịch vụ")]
    [Required(ErrorMessage = "Tên dịch vụ không được để trống")]
    [StringLength(100, ErrorMessage = "Tên dịch vụ không được vượt quá 100 ký tự")]
    public string Name { get; set; } = null!;

    [Display(Name = "Mô tả")]
    [Required(ErrorMessage = "Mô tả không được để trống")]
    [StringLength(200, ErrorMessage = "Mô tả không được vượt quá 200 ký tự")]
    public string Description { get; set; } = null!;

    [Display(Name = "Giá mỗi kg")]
    [Required(ErrorMessage = "Giá mỗi kg không được để trống")]
    [Range(1, 1_000_000_000, ErrorMessage = "Giá mỗi kg phải lớn hơn 0")]
    [DataType(DataType.Currency)]
    public decimal PricePerKg { get; set; }

    [Display(Name = "Thời gian hoàn thành")]
    [Required(ErrorMessage = "Thời gian hoàn thành không được để trống")]
    [Range(0.001, 24, ErrorMessage = "Thời gian hoàn thành phải lớn hơn 0")]
    public float ServiceTimeInHour { get; set; }

    public int StoreId { get; set; }

    public DateTime DeletedAt { get; set; }
}
