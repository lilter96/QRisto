namespace QRisto.Application.Models.Request.Service;

public class CreateServiceRequestModel
{
    public string Code { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public string Description { get; set; }

    public Guid ProviderId { get; set; }
}