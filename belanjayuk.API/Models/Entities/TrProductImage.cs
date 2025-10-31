using System;
using System.Collections.Generic;

namespace belanjayuk.API.Models.Entities;

public partial class TrProductImage
{
    public string IdProductImages { get; set; } = null!;

    public string? IdProduct { get; set; }

    public string? ProductImage { get; set; }

    public DateTime? DateIn { get; set; }

    public DateTime? DateUp { get; set; }

    public string? UserIn { get; set; }

    public string? UserUp { get; set; }

    public bool? IsActive { get; set; }

    public virtual MsProduct? IdProductNavigation { get; set; }
}
