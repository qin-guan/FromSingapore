import {useMutation, useQuery} from "@tanstack/vue-query";

export function useWhoAmI() {
    const { $api } = useNuxtApp()

    return useQuery({
        queryKey: ['me'],
        queryFn: async () => await $api.manage.info.get(),
        retry: false
    })
}

export function useRegisterMutation() {
    const { $api } = useNuxtApp()

    return useMutation({
        mutationFn: $api.register.post
    })
}

export function useLoginMutation() {
    const { $api } = useNuxtApp()

    return useMutation({
        mutationFn: $api.login.post
    })
}
