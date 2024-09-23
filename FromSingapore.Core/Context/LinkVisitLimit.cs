using Microsoft.EntityFrameworkCore;

namespace FromSingapore.Core.Context;

public class LinkVisitLimit
{
    public Guid Id { get; set; }

    public required int Max { get; set; }
}