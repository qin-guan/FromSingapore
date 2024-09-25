<script setup lang="ts">
import { useRegisterMutation, useWhoAmI } from "~/composables/auth";

definePageMeta({
  layout: 'landing'
})

const { $api } = useNuxtApp()
const { data: user, isPending: userIsPending } = useWhoAmI()
const { mutate: registerMutate, isPending: registerIsPending } = useRegisterMutation()

const error = ref<{ title: string, description?: string[] }>()

const fields = ref([
  {
    name: 'email',
    type: 'email',
    label: 'Email',
    placeholder: 'Enter your email',
  },
  {
    name: 'password',
    type: 'password',
    label: 'Password',
    placeholder: 'Enter your password',
  }
])

watchOnce(user, (value) => {
  if (!!value?.email) {
    navigateTo('/app')
  }
})

function register(event: { email: string, password: string }) {
  error.value = undefined
  registerMutate(event, {
    onError(err: any) {
      // Inaccurate types generated
      if (err.status === 400) {
        error.value = {
          title: err.title,
          description: Object.values(err.errors).flatMap(e => <string>e)
        }
      }
      else {
        error.value = {
          title: err.title
        }
      }
    },
    async onSuccess() {
      try {
        await $api.login.post(event, { queryParameters: { useCookies: true } })
      } catch (err: any) {
        if (err.message !== 'no response content type found for deserialization') {
          throw err
        }
      }
    }
  })
}
</script>
<template>
  <div class="flex flex-1 items-center justify-center">
    <UCard class="max-w-sm w-full bg-white/75 dark:bg-white/5 backdrop-blur">
      <UAuthForm title="Sign up" description="Get started with a free account right away!" align="top"
        icon="i-heroicons-user-circle" :fields="fields" :loading="registerIsPending" @submit="register">
        <template #validation>
          <UAlert v-if="error" color="red" icon="i-heroicons-information-circle-20-solid" :title="error.title">
            <template #description>
              <p v-for="line in error.description">{{ line }}</p>
            </template>
          </UAlert>
        </template>
      </UAuthForm>
    </UCard>
  </div>
</template>
