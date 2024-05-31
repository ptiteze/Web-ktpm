using System;
using System.Collections.Generic;

namespace Kitchen_MVC.Models;

public partial class Bill
{
    public int OrderId { get; set; }

    public DateTime PaymentTime { get; set; }

    public decimal Total { get; set; }

    public virtual Order Order { get; set; } = null!;
}
