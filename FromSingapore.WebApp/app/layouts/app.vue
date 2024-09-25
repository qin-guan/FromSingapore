<script setup lang="ts">
const links = ref([
  {
    id: 'links',
    label: 'Links',
    to: '/app'
  },
  {
    id: 'domains',
    label: 'Domains',
    to: '/app/domains'
  },
])
</script>

<template>
  <UDashboardLayout>
    <UDashboardPanel :width="250" :resizable="{ min: 200, max: 300 }" collapsible>
      <UDashboardNavbar :ui="{ left: 'flex-1', wrapper: 'border-0' }">
        <template #left>
          <span class="font-semibold">from.sg</span>
        </template>
      </UDashboardNavbar>

      <UDashboardSidebar>
        <UDashboardSidebarLinks :links="links" />
      </UDashboardSidebar>
    </UDashboardPanel>

    <UDashboardPage>
      <UDashboardPanel grow>
        <UDashboardPanelContent>
          <slot />
        </UDashboardPanelContent>
      </UDashboardPanel>
    </UDashboardPage>
  </UDashboardLayout>
</template>
