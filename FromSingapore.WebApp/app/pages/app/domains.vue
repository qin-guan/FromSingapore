<script setup lang="ts">
definePageMeta({
  layout: 'app'
})

const name = ref('')
const debouncedName = useDebounce(name)
const { data: query, isPending: queryIsPending } = useDomainQuery(debouncedName)
const { data: subscription, isPending: subscriptionIsPending, error: subscriptionError } = useSubscription()
const { data: plans, isPending: plansIsPending } = usePlans()

const basePlan = computed(() => plans.value?.plans?.
  filter(p => p.features?.domainsAvailable !== undefined &&
    p.priceAmount !== undefined &&
    p.features.domainsAvailable > 0
  )
  .sort((a, b) => a.priceAmount! - b.priceAmount!)
  .at(-1)
)

function buyDomain() {
  if (!query?.value?.available) {
    return
  }


}
</script>

<template>
  <div>
    <h2 class="text-xl font-semibold">Domains</h2>

    <form class="my-3" @submit.prevent="buyDomain">
      <UInput v-model="name" placeholder="Search for a subdomain!" />
    </form>

    <template v-if="name && !queryIsPending">
      <div v-if="query?.available" class="flex justify-between items-center">
        <span v-if="!subscription?.subscription?.id">
          Buy
          <UKbd size="md">
            {{ debouncedName }}.from.sg
          </UKbd>
          for ${{ basePlan?.priceAmount }}!
        </span>

        <UButton>Buy</UButton>
      </div>

      <div v-else>
        <UKbd size="md">
          {{ debouncedName }}.from.sg
        </UKbd>
        is not available =(
      </div>
    </template>
  </div>
</template>
