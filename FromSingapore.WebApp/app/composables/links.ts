import { useMutation, useQuery, useQueryClient } from "@tanstack/vue-query";

export function useLinks() {
  const { $api } = useNuxtApp()

  return useQuery({
    queryKey: ['links'],
    queryFn: async () => await $api.link.get()
  })
}

export function useLink(id: MaybeRef<string>) {
  const { $api } = useNuxtApp()

  // return useQuery({
  //   queryKey: ['links'],
  //   queryFn: async () => await $api.link[toValue(id)]
  // })
}

export function useCreateLinkMutation() {
  const { $api } = useNuxtApp()
  const queryClient = useQueryClient()

  return useMutation({
    mutationFn: $api.link.post,
    onSuccess() {
      queryClient.invalidateQueries({
        queryKey: ['links']
      })
    }
  })
}
