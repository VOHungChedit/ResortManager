namespace ResortMan.Entities;

public class Accomodation
{
	public int Id { get; set; }

	public int AccomodationPackageId { get; set; }
	public AccomodationPackage AccomodationPackage { get; set; } = null!;

	public string Name { get; set; } = null!;
	public string Description { get; set; } = null!;

	public AccomodationStatus Status { get; set; } = AccomodationStatus.Ready;

	public bool IsReady => Status == AccomodationStatus.Ready;
}
