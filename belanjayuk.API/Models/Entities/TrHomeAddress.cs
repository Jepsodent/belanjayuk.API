using System;
using System.Collections.Generic;

namespace belanjayuk.API.Models.Entities;

public partial class TrHomeAddress
{
    public string IdHomeAddress { get; set; } = null!;

    public string? IdUser { get; set; }

    public string? Provinsi { get; set; }

    public string? KotaKabupaten { get; set; }

    public string? Kecamatan { get; set; }

    public string? KodePos { get; set; }

    public string? HomeAddressDesc { get; set; }

    public bool? IsPrimaryAddress { get; set; }

    public DateTime? DateIn { get; set; }

    public DateTime? DateUp { get; set; }

    public string? UserIn { get; set; }

    public string? UserUp { get; set; }

    public bool? IsActive { get; set; }

    public virtual MsUser? IdUserNavigation { get; set; }
}
