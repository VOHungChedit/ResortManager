namespace ResortMan.Entities;

public class AccomodationPackage
{
    public int Id { get; set; }

    public int AccomodationTypeId { get; set; }
    public AccomodationType AccomodationType { get; set; } = null!;

    public string Name { get; set; } = null!;
    public int NoOfRoom { get; set; }
    public decimal FeePerNight { get; set; }

    public IList<AccomodationPackagePicture> Pictures { get; set; } = null!;

    public ICollection<Accomodation> Accomodations { get; set; } = null!;
}