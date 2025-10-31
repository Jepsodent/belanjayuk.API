using System;
using System.Collections.Generic;

namespace belanjayuk.API.Models.Entities;

public partial class MsUserPassword
{
    public string IdUserPassword { get; set; } = null!;

    public string? IdUser { get; set; }

    public string? PasswordHashed { get; set; }

    public DateTime? DateIn { get; set; }

    public DateTime? DateUp { get; set; }

    public string? UserIn { get; set; }

    public string? UserUp { get; set; }

    public bool? IsActive { get; set; }

    public virtual MsUser? IdUserNavigation { get; set; }
}
