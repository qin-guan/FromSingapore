import {
    AnonymousAuthenticationProvider,
} from '@microsoft/kiota-abstractions';
import {FetchRequestAdapter, HttpClient} from '@microsoft/kiota-http-fetchlibrary';
import {createApiClient} from "~~/api/apiClient";

export default defineNuxtPlugin((nuxtApp) => {
    const authProvider = new AnonymousAuthenticationProvider();
    const httpClient = new HttpClient((req, init) => {
        return fetch(req, {
            credentials: 'include',
            ...init
        }) 
    });
    const adapter = new FetchRequestAdapter(authProvider, undefined, undefined, httpClient);
    adapter.baseUrl = nuxtApp.$config.public.api

    const api = createApiClient(adapter)

    return {
        provide: {
            api
        }
    }
})
