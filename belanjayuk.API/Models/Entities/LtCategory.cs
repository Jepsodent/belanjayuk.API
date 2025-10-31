using System;
using System.Collections.Generic;

namespace belanjayuk.API.Models.Entities;

public partial class LtCategory
{
    public string IdCategory { get; set; } = null!;

    public string? CategoryName { get; set; }

    public DateTime? DateIn { get; set; }

    public DateTime? DateUp { get; set; }

    public string? UserIn { get; set; }

    public string? UserUp { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<MsProduct> MsProducts { get; set; } = new List<MsProduct>();
}
