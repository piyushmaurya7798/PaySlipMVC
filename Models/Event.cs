using System;
using System.Collections.Generic;

namespace PaySlipMVC.Models;

public partial class Event
{
    public int Id { get; set; }

    public DateOnly? Eventdate { get; set; }

    public string? Eventdesc { get; set; }

    public string? Vartype { get; set; }

    public int? Flag { get; set; }
}
