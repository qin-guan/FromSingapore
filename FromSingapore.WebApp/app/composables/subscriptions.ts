import { useMutation, useQuery, useQueryClient } from "@tanstack/vue-query";

export function useSubscription() {
  const { $api } = useNuxtApp()
  const { data: user } = useWhoAmI()

  return useQuery({
    queryKey: ['subscriptions'],
    queryFn: async () => await $api.user.byUserId(user.value?.id!).subscription.get(),
    enabled: computed(() => !!user?.value?.id),
    retry: false
  })
}

export function useCreateSubscription() {
  const { $api } = useNuxtApp()
  const { data: user } = useWhoAmI()

  return useMutation({
    mutationFn: async (data: { planId: MaybeRef<string> }) => await $api.user.byUserId(user.value?.id!).subscription.byPlanId(toValue(data.planId)).post(),
  })
}
