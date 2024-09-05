using System;
using System.Collections.Generic;

namespace PaySlipMVC.Models;

public partial class Leave
{
    public int Id { get; set; }

    public string? Sitem { get; set; }

    public int? Pl { get; set; }

    public int? Cl { get; set; }

    public int? Sl { get; set; }

    public int? EmpId { get; set; }

    public string? Fromdate { get; set; }

    public string? Todate { get; set; }

    public string? Reason { get; set; }

    public int? Extra { get; set; }
}
