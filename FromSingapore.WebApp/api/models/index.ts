/* tslint:disable */
/* eslint-disable */
// Generated by Microsoft Kiota
// @ts-ignore
import { type Parsable, type ParseNode, type SerializationWriter } from '@microsoft/kiota-abstractions';

/**
 * Creates a new instance of the appropriate class based on discriminator value
 * @param parseNode The parse node to use to read the discriminator value and create the object
 * @returns {FromSingaporeWebApiEndpointsLinkListLinksResponse}
 */
// @ts-ignore
export function createFromSingaporeWebApiEndpointsLinkListLinksResponseFromDiscriminatorValue(parseNode: ParseNode | undefined) : ((instance?: Parsable) => Record<string, (node: ParseNode) => void>) {
    return deserializeIntoFromSingaporeWebApiEndpointsLinkListLinksResponse;
}
/**
 * The deserialization information for the current model
 * @returns {Record<string, (node: ParseNode) => void>}
 */
// @ts-ignore
export function deserializeIntoFromSingaporeWebApiEndpointsLinkListLinksResponse(fromSingaporeWebApiEndpointsLinkListLinksResponse: Partial<FromSingaporeWebApiEndpointsLinkListLinksResponse> | undefined = {}) : Record<string, (node: ParseNode) => void> {
    return {
    }
}
export interface FromSingaporeWebApiEndpointsLinkListLinksResponse extends Parsable {
}
/**
 * Serializes information the current object
 * @param writer Serialization writer to use to serialize this model
 */
// @ts-ignore
export function serializeFromSingaporeWebApiEndpointsLinkListLinksResponse(writer: SerializationWriter, fromSingaporeWebApiEndpointsLinkListLinksResponse: Partial<FromSingaporeWebApiEndpointsLinkListLinksResponse> | undefined = {}) : void {
}
/* tslint:enable */
/* eslint-enable */
