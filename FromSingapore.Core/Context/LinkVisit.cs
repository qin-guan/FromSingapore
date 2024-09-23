namespace FromSingapore.Core.Context;

public class LinkVisit
{
    public Guid Id { get; set; }
    
    public DateTime VisitedAt { get; set; }
    
    public Guid LinkId { get; set; }
    public Link Link { get; set; }
}