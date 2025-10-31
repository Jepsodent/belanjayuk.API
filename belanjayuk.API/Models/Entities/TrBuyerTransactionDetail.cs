using System;
using System.Collections.Generic;

namespace belanjayuk.API.Models.Entities;

public partial class TrBuyerTransactionDetail
{
    public string IdBuyerTransactionDetail { get; set; } = null!;

    public string? IdBuyerTransaction { get; set; }

    public string? IdProduct { get; set; }

    public int? Qty { get; set; }

    public decimal? PriceOfProduct { get; set; }

    public decimal? DiscountProduct { get; set; }

    public int? Rating { get; set; }

    public string? RatingComment { get; set; }

    public string? UserIn { get; set; }

    public string? UserUp { get; set; }

    public DateTime? DateIn { get; set; }

    public DateTime? DateUp { get; set; }

    public bool? IsActive { get; set; }

    public virtual TrBuyerTransaction? IdBuyerTransactionNavigation { get; set; }

    public virtual MsProduct? IdProductNavigation { get; set; }
}
