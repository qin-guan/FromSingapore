<script setup lang="ts">
const { data: domains, isPending: domainsIsPending } = useDomains()
const { mutate, isPending } = useCreateLinkMutation()

const state = reactive({
  title: '',
  description: '',
  shortCode: '',
  originalUri: '',
  domainId: ''
})

function createLink() {
  mutate({
    title: state.title,
    description: state.description,
    shortCode: state.shortCode,
    originalUri: state.originalUri,
    domainId: state.domainId
  })
}
</script>

<template>
  <div>
    <UInput v-model="state.originalUri" label="URL" placeholder="Enter the URL of the link" />
    <UInput v-model="state.shortCode" label="Code" placeholder="Enter the code of the link" />
    <UInput v-model="state.title" label="Title" placeholder="Enter the name of the link" />
    <USelect v-model="state.domainId" :options="domains?.domains" option-attribute="name" value-attribute="id" />

    <UButton @click="createLink" :isPending="isPending">
      Create Link
    </UButton>
  </div>
</template>
