// https://nuxt.com/docs/api/configuration/nuxt-config
export default defineNuxtConfig({
    devtools: { enabled: true },
    extends: ['@nuxt/ui-pro'],
    modules: ['@nuxt/ui', '@vueuse/nuxt'],

    compatibilityDate: '2024-04-03',
    future: {
        compatibilityVersion: 4
    },

    runtimeConfig: {
        public: {
            api: process.env['services__webapi__http__0'] || process.env['services__webapi__https__0'] || 'http://localhost:5213' || 'https://localhost:7092'
        }
    },
})
