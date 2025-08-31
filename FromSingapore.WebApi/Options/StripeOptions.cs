namespace FromSingapore.WebApi.Options;

public class StripeOptions
{
    /// <summary>
    /// Uri of the server that should be used for the Stripe callbacks and webhooks
    /// </summary>
    public string ApiUri { get; set; }
}