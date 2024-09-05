using System;
using System.Collections.Generic;

namespace PaySlipMVC.Models;

public partial class Ticket
{
    public int TicketId { get; set; }

    public string? Designation { get; set; }

    public string? Raiseto { get; set; }

    public string? Raiseby { get; set; }

    public string? Description { get; set; }

    public string? Attachment { get; set; }

    public string? Solution { get; set; }

    public DateTime? Raiseddate { get; set; }

    public DateTime? Closeddate { get; set; }
}
