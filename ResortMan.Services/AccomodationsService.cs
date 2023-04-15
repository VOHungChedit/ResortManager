using Microsoft.EntityFrameworkCore;
using ResortMan.Data;
using ResortMan.Entities;

namespace ResortMan.Services;

public class AccomodationsService
{
    private readonly ApplicationDbContext context;

    public AccomodationsService(ApplicationDbContext context)
    {
        this.context = context;
    }
    public IEnumerable<Accomodation> GetAccomodations()
    {
        return context.Accomodations
                .Include(ap => ap.AccomodationPackage)
                .ToList();
    }
	public IEnumerable<Accomodation> SearchAccomodations(string searchTerm)
	{
		var source = context.Accomodations.AsQueryable();
		if (!string.IsNullOrEmpty(searchTerm))
		{
			source = source.Where((Accomodation a) => a.Name.ToLower().Contains(searchTerm.ToLower()));
		}

		return source.ToList();
	}

	public Accomodation? GetAccomodationById(int Id)
    {
        return context.Accomodations.Find(Id);
    }
    public bool UpdateAccomodation(Accomodation accomodation)
    {
        context.Accomodations.Update(accomodation);
        return context.SaveChanges() > 0;
    }

	public bool DeleteAccomodation(Accomodation Accomodation)
	{

		context.Accomodations.Remove(Accomodation);
		return context.SaveChanges() > 0;
	}
	public bool SaveAccomodation(Accomodation Accomodation)
    {
        context.Accomodations.Add(Accomodation);
        var row = context.SaveChanges();
        return row > 0;

    }


}
