using System;
using System.Collections.Generic;

namespace belanjayuk.API.Models.Entities;

public partial class LtGender
{
    public string IdGender { get; set; } = null!;

    public string? GenderName { get; set; }

    public DateTime? DateIn { get; set; }

    public string? UserIn { get; set; }

    public DateTime? DateUp { get; set; }

    public string? UserUp { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<MsUser> MsUsers { get; set; } = new List<MsUser>();
}
