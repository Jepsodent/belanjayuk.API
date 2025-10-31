using System;
using System.Collections.Generic;

namespace belanjayuk.API.Models.Entities;

public partial class MsProduct
{
    public string IdProduct { get; set; } = null!;

    public string? IdUserSeller { get; set; }

    public string? ProductName { get; set; }

    public string? ProductDesc { get; set; }

    public decimal? Price { get; set; }

    public decimal? Discount { get; set; }

    public string? IdCategory { get; set; }

    public DateTime? DateIn { get; set; }

    public DateTime? DateUp { get; set; }

    public string? UserIn { get; set; }

    public string? UserUp { get; set; }

    public bool? IsActive { get; set; }

    public int? Stock { get; set; }

    public virtual LtCategory? IdCategoryNavigation { get; set; }

    public virtual MsUserSeller? IdUserSellerNavigation { get; set; }

    public virtual ICollection<TrBuyerCart> TrBuyerCarts { get; set; } = new List<TrBuyerCart>();

    public virtual ICollection<TrBuyerTransactionDetail> TrBuyerTransactionDetails { get; set; } = new List<TrBuyerTransactionDetail>();

    public virtual ICollection<TrProductImage> TrProductImages { get; set; } = new List<TrProductImage>();
}
