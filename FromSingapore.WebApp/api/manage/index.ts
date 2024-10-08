/* tslint:disable */
/* eslint-disable */
// Generated by Microsoft Kiota
// @ts-ignore
import { InfoRequestBuilderRequestsMetadata, type InfoRequestBuilder } from './info/index.js';
// @ts-ignore
import { TwofaRequestBuilderRequestsMetadata, type TwofaRequestBuilder } from './twofa/index.js';
// @ts-ignore
import { type BaseRequestBuilder, type KeysToExcludeForNavigationMetadata, type NavigationMetadata } from '@microsoft/kiota-abstractions';

/**
 * Builds and executes requests for operations under /manage
 */
export interface ManageRequestBuilder extends BaseRequestBuilder<ManageRequestBuilder> {
    /**
     * The info property
     */
    get info(): InfoRequestBuilder;
    /**
     * The Twofa property
     */
    get twofa(): TwofaRequestBuilder;
}
/**
 * Uri template for the request builder.
 */
export const ManageRequestBuilderUriTemplate = "{+baseurl}/manage";
/**
 * Metadata for all the navigation properties in the request builder.
 */
export const ManageRequestBuilderNavigationMetadata: Record<Exclude<keyof ManageRequestBuilder, KeysToExcludeForNavigationMetadata>, NavigationMetadata> = {
    info: {
        requestsMetadata: InfoRequestBuilderRequestsMetadata,
    },
    twofa: {
        requestsMetadata: TwofaRequestBuilderRequestsMetadata,
    },
};
/* tslint:enable */
/* eslint-enable */
