import { useMutation, useQuery, useQueryClient } from "@tanstack/vue-query";

export function useDomains() {
  const { $api } = useNuxtApp()

  return useQuery({
    queryKey: ['domains'],
    queryFn: async () => await $api.domain.get()
  })
}

export function useDomainQuery(name: MaybeRef<string>) {
  const { $api } = useNuxtApp()

  return useQuery({
    queryKey: computed(() => ['domains', toValue(name)]),
    queryFn: async () => await $api.domain.byName(toValue(name)).get()
  })
}
