<script setup lang="ts">
const links = ref([
  {
    label: 'Docs',
    to: '/docs'
  },
  {
    label: 'Pricing',
    to: '/pricing'
  },
  {
    label: 'Get started',
    to: '/register'
  }
])
</script>
<template>
  <UHeader :links="links" title="from.sg"></UHeader>

  <UMain class="flex flex-col">
    <slot/>
  </UMain>
</template>
