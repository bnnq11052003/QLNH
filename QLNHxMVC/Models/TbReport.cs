using System;
using System.Collections.Generic;

namespace QLNHxMVC.Models;

public partial class TbReport
{
    public int ReportId { get; set; }

    public int Idbill { get; set; }

    public int Slban { get; set; }

    public DateTime? DateCheckin { get; set; }

    public DateTime? DateCheckout { get; set; }

    public int TotalPrice { get; set; }

    public virtual TbDstable IdbillNavigation { get; set; } = null!;

    public virtual TbFood SlbanNavigation { get; set; } = null!;
}
