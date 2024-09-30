import { useMutation, useQuery, useQueryClient } from "@tanstack/vue-query";

export function usePlans() {
  const { $api } = useNuxtApp()

  return useQuery({
    queryKey: ['plans'],
    queryFn: async () => await $api.plan.get()
  })
}
