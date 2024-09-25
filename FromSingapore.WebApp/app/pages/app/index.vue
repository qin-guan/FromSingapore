<script setup lang="ts">
definePageMeta({
  layout: 'app'
})

const { data, isPending: linksIsPending } = useLinks()
</script>

<template>
  <div>
    <h2 class="text-xl font-semibold">Links</h2>

    <AppCreateLink />

    <UTable :rows="data?.links" />
  </div>
</template>
