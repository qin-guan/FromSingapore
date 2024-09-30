/* tslint:disable */
/* eslint-disable */
// Generated by Microsoft Kiota
// @ts-ignore
import { ConfirmEmailRequestBuilderRequestsMetadata, type ConfirmEmailRequestBuilder } from './confirmEmail/index.js';
// @ts-ignore
import { DomainRequestBuilderNavigationMetadata, DomainRequestBuilderRequestsMetadata, type DomainRequestBuilder } from './domain/index.js';
// @ts-ignore
import { ForgotPasswordRequestBuilderRequestsMetadata, type ForgotPasswordRequestBuilder } from './forgotPassword/index.js';
// @ts-ignore
import { type WithShortCodeItemRequestBuilder, WithShortCodeItemRequestBuilderRequestsMetadata } from './item/index.js';
// @ts-ignore
import { LinkRequestBuilderRequestsMetadata, type LinkRequestBuilder } from './link/index.js';
// @ts-ignore
import { LoginRequestBuilderRequestsMetadata, type LoginRequestBuilder } from './login/index.js';
// @ts-ignore
import { ManageRequestBuilderNavigationMetadata, type ManageRequestBuilder } from './manage/index.js';
// @ts-ignore
import { PlanRequestBuilderRequestsMetadata, type PlanRequestBuilder } from './plan/index.js';
// @ts-ignore
import { RefreshRequestBuilderRequestsMetadata, type RefreshRequestBuilder } from './refresh/index.js';
// @ts-ignore
import { RegisterRequestBuilderRequestsMetadata, type RegisterRequestBuilder } from './register/index.js';
// @ts-ignore
import { ResendConfirmationEmailRequestBuilderRequestsMetadata, type ResendConfirmationEmailRequestBuilder } from './resendConfirmationEmail/index.js';
// @ts-ignore
import { ResetPasswordRequestBuilderRequestsMetadata, type ResetPasswordRequestBuilder } from './resetPassword/index.js';
// @ts-ignore
import { type UserRequestBuilder, UserRequestBuilderNavigationMetadata } from './user/index.js';
// @ts-ignore
import { apiClientProxifier, registerDefaultDeserializer, registerDefaultSerializer, type BaseRequestBuilder, type KeysToExcludeForNavigationMetadata, type NavigationMetadata, type RequestAdapter } from '@microsoft/kiota-abstractions';
// @ts-ignore
import { FormParseNodeFactory, FormSerializationWriterFactory } from '@microsoft/kiota-serialization-form';
// @ts-ignore
import { JsonParseNodeFactory, JsonSerializationWriterFactory } from '@microsoft/kiota-serialization-json';
// @ts-ignore
import { MultipartSerializationWriterFactory } from '@microsoft/kiota-serialization-multipart';
// @ts-ignore
import { TextParseNodeFactory, TextSerializationWriterFactory } from '@microsoft/kiota-serialization-text';

/**
 * The main entry point of the SDK, exposes the configuration and the fluent API.
 */
export interface ApiClient extends BaseRequestBuilder<ApiClient> {
    /**
     * The confirmEmail property
     */
    get confirmEmail(): ConfirmEmailRequestBuilder;
    /**
     * The Domain property
     */
    get domain(): DomainRequestBuilder;
    /**
     * The forgotPassword property
     */
    get forgotPassword(): ForgotPasswordRequestBuilder;
    /**
     * The Link property
     */
    get link(): LinkRequestBuilder;
    /**
     * The login property
     */
    get login(): LoginRequestBuilder;
    /**
     * The manage property
     */
    get manage(): ManageRequestBuilder;
    /**
     * The Plan property
     */
    get plan(): PlanRequestBuilder;
    /**
     * The refresh property
     */
    get refresh(): RefreshRequestBuilder;
    /**
     * The register property
     */
    get register(): RegisterRequestBuilder;
    /**
     * The resendConfirmationEmail property
     */
    get resendConfirmationEmail(): ResendConfirmationEmailRequestBuilder;
    /**
     * The resetPassword property
     */
    get resetPassword(): ResetPasswordRequestBuilder;
    /**
     * The User property
     */
    get user(): UserRequestBuilder;
    /**
     * Gets an item from the ApiSdk.item collection
     * @param shortCode Unique identifier of the item
     * @returns {WithShortCodeItemRequestBuilder}
     */
     byShortCode(shortCode: string) : WithShortCodeItemRequestBuilder;
}
/**
 * Instantiates a new {@link ApiClient} and sets the default values.
 * @param requestAdapter The request adapter to use to execute the requests.
 */
// @ts-ignore
export function createApiClient(requestAdapter: RequestAdapter) {
    registerDefaultSerializer(JsonSerializationWriterFactory);
    registerDefaultSerializer(TextSerializationWriterFactory);
    registerDefaultSerializer(FormSerializationWriterFactory);
    registerDefaultSerializer(MultipartSerializationWriterFactory);
    registerDefaultDeserializer(JsonParseNodeFactory);
    registerDefaultDeserializer(TextParseNodeFactory);
    registerDefaultDeserializer(FormParseNodeFactory);
    const pathParameters: Record<string, unknown> = {
        "baseurl": requestAdapter.baseUrl,
    };
    return apiClientProxifier<ApiClient>(requestAdapter, pathParameters, ApiClientNavigationMetadata, undefined);
}
/**
 * Uri template for the request builder.
 */
export const ApiClientUriTemplate = "{+baseurl}";
/**
 * Metadata for all the navigation properties in the request builder.
 */
export const ApiClientNavigationMetadata: Record<Exclude<keyof ApiClient, KeysToExcludeForNavigationMetadata>, NavigationMetadata> = {
    byShortCode: {
        requestsMetadata: WithShortCodeItemRequestBuilderRequestsMetadata,
        pathParametersMappings: ["ShortCode"],
    },
    confirmEmail: {
        requestsMetadata: ConfirmEmailRequestBuilderRequestsMetadata,
    },
    domain: {
        requestsMetadata: DomainRequestBuilderRequestsMetadata,
        navigationMetadata: DomainRequestBuilderNavigationMetadata,
    },
    forgotPassword: {
        requestsMetadata: ForgotPasswordRequestBuilderRequestsMetadata,
    },
    link: {
        requestsMetadata: LinkRequestBuilderRequestsMetadata,
    },
    login: {
        requestsMetadata: LoginRequestBuilderRequestsMetadata,
    },
    manage: {
        navigationMetadata: ManageRequestBuilderNavigationMetadata,
    },
    plan: {
        requestsMetadata: PlanRequestBuilderRequestsMetadata,
    },
    refresh: {
        requestsMetadata: RefreshRequestBuilderRequestsMetadata,
    },
    register: {
        requestsMetadata: RegisterRequestBuilderRequestsMetadata,
    },
    resendConfirmationEmail: {
        requestsMetadata: ResendConfirmationEmailRequestBuilderRequestsMetadata,
    },
    resetPassword: {
        requestsMetadata: ResetPasswordRequestBuilderRequestsMetadata,
    },
    user: {
        navigationMetadata: UserRequestBuilderNavigationMetadata,
    },
};
/* tslint:enable */
/* eslint-enable */
