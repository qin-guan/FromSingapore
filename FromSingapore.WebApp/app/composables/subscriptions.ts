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
