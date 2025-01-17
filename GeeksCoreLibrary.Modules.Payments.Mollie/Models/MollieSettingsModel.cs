using GeeksCoreLibrary.Components.OrderProcess.Models;

namespace GeeksCoreLibrary.Modules.Payments.Mollie.Models;

public class MollieSettingsModel : PaymentServiceProviderSettingsModel
{
    /// <summary>
    /// Gets or sets the API key for the current environment.
    /// </summary>
    public string? ApiKey { get; set; }
}