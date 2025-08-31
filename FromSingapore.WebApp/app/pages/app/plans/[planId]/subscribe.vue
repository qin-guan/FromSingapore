<script setup lang="ts">
const route = useRoute()
const planId = computed(() => route.params.planId as string)

const { mutate } = useCreateSubscription()

function subscribe() {
  mutate({
    planId
  }, {
    onSuccess(data) {
      window.open(data.checkoutUri, "_blank")
    }
  })
}
</script>

<template>
  <div>
    <UButton @click="subscribe">Subscribe</UButton>
  </div>
</template>
