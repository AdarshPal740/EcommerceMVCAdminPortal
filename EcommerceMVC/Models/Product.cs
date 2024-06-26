﻿using System;
using System.Collections.Generic;

namespace EcommerceMVC.Models;

public partial class Product
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public decimal? Price { get; set; }

    public string? Image { get; set; }

    public string? Category { get; set; }

    public string? Brand { get; set; }

    public DateTime? CreatedAt { get; set; }
}
