using System;
using System.Collections.Generic;

namespace PaySlipMVC.Models;

public partial class Payslip
{
    public int Id { get; set; }

    public int? Userid { get; set; }

    public int? Present { get; set; }

    public int? Absent { get; set; }

    public int? Tdays { get; set; }

    public decimal? Salary { get; set; }

    public int Remain { get; set; }

    public virtual Employee? User { get; set; }
}
