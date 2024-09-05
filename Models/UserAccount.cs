﻿using System;
using System.Collections.Generic;

namespace PaySlipMVC.Models;

public partial class UserAccount
{
    public int Id { get; set; }

    public string? UserFname { get; set; }

    public string? UserLname { get; set; }

    public long? UserMobile { get; set; }

    public string? UserEmail { get; set; }

    public string? UserPass { get; set; }

    public string? UserRole { get; set; }

    public string? UserProfile { get; set; }
}
