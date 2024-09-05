using System;
using System.Collections.Generic;

namespace PaySlipMVC.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string? Name { get; set; }

    public string? Contact { get; set; }

    public string? Email { get; set; }

    public decimal? Salary { get; set; }

    public DateOnly? DateOfJoining { get; set; }

    public string? Password { get; set; }

    public string? Role { get; set; }

    public virtual ICollection<Payslip> Payslips { get; set; } = new List<Payslip>();
}
