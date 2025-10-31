using System;
using System.Collections.Generic;

namespace belanjayuk.API.Models.Entities;

public partial class TrBuyerCart
{
    public string IdBuyerCart { get; set; } = null!;

    public string? IdUser { get; set; }

    public string? IdProduct { get; set; }

    public int? Qty { get; set; }

    public DateTime? DateIn { get; set; }

    public DateTime? DateUp { get; set; }

    public string? UserIn { get; set; }

    public string? UserUp { get; set; }

    public bool? IsActive { get; set; }

    public virtual MsProduct? IdProductNavigation { get; set; }

    public virtual MsUser? IdUserNavigation { get; set; }
}
