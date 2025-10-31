using System;
using System.Collections.Generic;

namespace belanjayuk.API.Models.Entities;

public partial class DwmonthlySale
{
    public Guid Id { get; set; }

    public string SellerId { get; set; } = null!;

    public string MonthYear { get; set; } = null!;

    public decimal? TotalSales { get; set; }

    public int? TotalQty { get; set; }

    public decimal? AvgRating { get; set; }

    public DateTime? LoadDate { get; set; }
}
