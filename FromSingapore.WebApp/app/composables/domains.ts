import { useMutation, useQuery, useQueryClient } from "@tanstack/vue-query";

export function useDomains() {
  const { $api } = useNuxtApp()

  return useQuery({
    queryKey: ['domains'],
    queryFn: async () => await $api.domain.get()
  })
}
