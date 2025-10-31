using System;
using System.Collections.Generic;

namespace belanjayuk.API.Models.Entities;

public partial class LtPayment
{
    public string IdPayment { get; set; } = null!;

    public string? PaymentName { get; set; }

    public DateTime? DateIn { get; set; }

    public DateTime? DateUp { get; set; }

    public string? UserIn { get; set; }

    public string? UserUp { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<TrBuyerTransaction> TrBuyerTransactions { get; set; } = new List<TrBuyerTransaction>();
}
