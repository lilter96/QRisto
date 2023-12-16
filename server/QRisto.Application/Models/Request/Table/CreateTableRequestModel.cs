namespace QRisto.Application.Models.Request.Table;

public class CreateTableRequestModel
{
    public Guid ServiceId { get; set; }

    public string Name { get; set; }

    public int Seats { get; set; }

    public double LocationX { get; set; }

    public double LocationY { get; set; }

    public double Rotation { get; set; }
}