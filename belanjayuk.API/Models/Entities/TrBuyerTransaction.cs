using System;
using System.Collections.Generic;

namespace belanjayuk.API.Models.Entities;

public partial class TrBuyerTransaction
{
    public string IdBuyerTransaction { get; set; } = null!;

    public string? IdUser { get; set; }

    public string? IdPayment { get; set; }

    public decimal? FinalPrice { get; set; }

    public int? Rating { get; set; }

    public string? RatingComment { get; set; }

    public DateTime? DateIn { get; set; }

    public DateTime? DateUp { get; set; }

    public string? UserIn { get; set; }

    public string? UserUp { get; set; }

    public bool? IsActive { get; set; }

    public virtual LtPayment? IdPaymentNavigation { get; set; }

    public virtual MsUser? IdUserNavigation { get; set; }

    public virtual ICollection<TrBuyerTransactionDetail> TrBuyerTransactionDetails { get; set; } = new List<TrBuyerTransactionDetail>();
}
