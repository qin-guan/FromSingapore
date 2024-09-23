namespace FromSingapore.Core.Context;

public abstract class Domain
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public ICollection<Link> Links { get; set; } = [];
}