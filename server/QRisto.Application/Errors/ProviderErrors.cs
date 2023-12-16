using QRisto.Application.Utils;

namespace QRisto.Application.Errors;

public class ProviderErrors
{
    public static readonly Error UnableCreateProvider = new(
        "Providers.UnableCreate", "Can't create provider");
    
    public static readonly Error UnableGetProviders = new(
        "Providers.UnableGetList", "Can't get providers list");
    
    public static readonly Error UnableGetProvider = new(
        "Providers.UnableGetProvider", "Can't get provider by id");
    
    public static readonly Error UnableDeleteProvider = new(
        "Providers.UnableDeleteProvider", "Can't delete provider");
    
    public static readonly Error UnableUpdateProvider = new(
        "Providers.UnableUpdateProvider", "Can't update provider");
}