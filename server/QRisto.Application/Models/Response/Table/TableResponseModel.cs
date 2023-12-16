namespace QRisto.Application.Models.Response.Table;

public class TableResponseModel
{
    public Guid Id { get; set; }

    public Guid ServiceId { get; set; }

    public string Name { get; set; }

    public int Seats { get; set; }

    public double LocationX { get; set; }

    public double LocationY { get; set; }

    public double Rotation { get; set; }
}