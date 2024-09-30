using FromSingapore.Core.StaticStore.Plans;

namespace FromSingapore.Core.StaticStore;

public static class StaticStore
{
    public static IReadOnlyList<Plan> Plans =
    [
        new BasicPlan()
    ];
}