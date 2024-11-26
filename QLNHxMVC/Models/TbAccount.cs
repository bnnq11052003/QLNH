using System;
using System.Collections.Generic;

namespace QLNHxMVC.Models;

public partial class TbAccount
{
    public int AccountId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string AccountType { get; set; } = null!;

    public virtual ICollection<TbUserInfo> TbUserInfos { get; set; } = new List<TbUserInfo>();
}
