using QRisto.Application.Utils;

namespace QRisto.Application.Errors;

public class ProviderErrors
{
    public static readonly Error UnableCreateProvider = new(
        "Providers.UnableCreate", "Can't create provider");
}