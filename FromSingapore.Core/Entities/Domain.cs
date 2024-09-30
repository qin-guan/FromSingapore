namespace FromSingapore.Core.Entities;

public abstract class Domain
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    
    public ICollection<Link> Links { get; set; } = [];
}