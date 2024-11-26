using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QLNHxMVC.Models;

public partial class TbUserInfo
{
    public int UserInfoId { get; set; }

    public int? AccountId { get; set; }

    public string FullName { get; set; } = null!;

    public DateOnly BirthDay { get; set; }

    public int Sex { get; set; }

    public string? Email { get; set; }

    [RegularExpression(@"^\d+$")]
    public string? PhoneNumber { get; set; }

    public virtual TbAccount? Account { get; set; }

    public virtual ICollection<TbBillHistory> TbBillHistories { get; set; } = new List<TbBillHistory>();
}
