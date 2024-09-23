namespace FromSingapore.Core.Context;

public abstract class SubscriptionPlan
{
    public Guid Id { get; set; }
    
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required bool BuiltIn { get; set; }
    public required TimeSpan Duration { get; set; }
    public required decimal Price { get; set; }
}