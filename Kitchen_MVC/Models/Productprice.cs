﻿using System;
using System.Collections.Generic;

namespace Kitchen_MVC.Models;

public partial class Productprice
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public DateTime AppliedDate { get; set; }

    public decimal Price { get; set; }

    public int EmployeeId { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
