using QRisto.Application.Utils;

namespace QRisto.Application.Errors;

public class ServiceErrors
{
    public static readonly Error NotFound = new(
        "Service.NotFind", "Can't find the service.");
}