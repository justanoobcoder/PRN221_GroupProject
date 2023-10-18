using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects;

public class Order
{
    [Display(Name = "Mã đơn hàng")]
    public string Id { get; set; } = null!;

    public int CustomerId { get; set; }

    public int ServiceId { get; set; }

    [Display(Name = "Ngày tạo")]
    [DataType(DataType.DateTime)]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [Display(Name = "Ngày nhận")]
    [DataType(DataType.DateTime)]
    public DateTime? PickUpAt { get; set; }

    [Display(Name = "Ngày trả")]
    [DataType(DataType.DateTime)]
    public DateTime? DropOffAt { get; set; }

    [Display(Name = "Khối lượng")]
    [Required(ErrorMessage = "Khối lượng không được để trống")]
    [Range(1, 20, ErrorMessage = "Khối lượng phải nằm trong khoảng từ 1 đến 20")]
    public float Weight { get; set; }

    [Display(Name = "Tổng tiền")]
    [DataType(DataType.Currency)]
    public decimal TotalCost { get; set; }

    [Display(Name = "Trạng thái")]
    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    [Display(Name = "Ghi chú")]
    [StringLength(200, ErrorMessage = "Ghi chú không được vượt quá 200 ký tự")]
    public string? Note { get; set; }

    public Customer Customer { get; set; } = default!;

    public Service Service { get; set; } = default!;
}
