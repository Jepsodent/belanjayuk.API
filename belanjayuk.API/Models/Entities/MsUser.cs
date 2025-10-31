using System;
using System.Collections.Generic;

namespace belanjayuk.API.Models.Entities;

public partial class MsUser
{
    public string IdUser { get; set; } = null!;

    public string? UserName { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateTime? Dob { get; set; }

    public DateTime? DateIn { get; set; }

    public string? UserIn { get; set; }

    public DateTime? DateUp { get; set; }

    public string? UserUp { get; set; }

    public bool? IsActive { get; set; }

    public string? IdGender { get; set; }

    public virtual LtGender? IdGenderNavigation { get; set; }

    public virtual ICollection<MsUserPassword> MsUserPasswords { get; set; } = new List<MsUserPassword>();

    public virtual ICollection<MsUserSeller> MsUserSellers { get; set; } = new List<MsUserSeller>();

    public virtual ICollection<TrBuyerCart> TrBuyerCarts { get; set; } = new List<TrBuyerCart>();

    public virtual ICollection<TrBuyerTransaction> TrBuyerTransactions { get; set; } = new List<TrBuyerTransaction>();

    public virtual ICollection<TrHomeAddress> TrHomeAddresses { get; set; } = new List<TrHomeAddress>();
}
