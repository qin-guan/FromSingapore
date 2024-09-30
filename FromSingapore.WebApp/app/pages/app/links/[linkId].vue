<script setup lang="ts">
  const route = useRoute()
  // const { data: link, isPending: linkIsPending } = useLink(route.params.linkId)
</script>

<template>
  <div>
    <slot/>
  </div>
</template>
