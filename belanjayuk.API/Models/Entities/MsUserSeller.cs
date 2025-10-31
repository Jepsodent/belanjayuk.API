using System;
using System.Collections.Generic;

namespace belanjayuk.API.Models.Entities;

public partial class MsUserSeller
{
    public string IdUserSeller { get; set; } = null!;

    public string? IdUser { get; set; }

    public string? StoreName { get; set; }

    public string? SellerDesc { get; set; }

    public string? Address { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public DateTime? DateIn { get; set; }

    public DateTime? DateUp { get; set; }

    public string? UserIn { get; set; }

    public string? UserUp { get; set; }

    public bool? IsActive { get; set; }

    public virtual MsUser? IdUserNavigation { get; set; }

    public virtual ICollection<MsProduct> MsProducts { get; set; } = new List<MsProduct>();
}
