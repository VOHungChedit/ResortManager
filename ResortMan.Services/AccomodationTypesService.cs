using Microsoft.EntityFrameworkCore;
using ResortMan.Data;
using ResortMan.Entities;

namespace ResortMan.Services;

public class AccomodationTypesService
{
    private readonly ApplicationDbContext context;

    public AccomodationTypesService(ApplicationDbContext context)
    {
        this.context = context;
    }
    public IEnumerable<AccomodationType> GetAccomodationTypes()
    {
        return context.AccomodationTypes.ToList();
    }
	public IEnumerable<AccomodationType> SearchAccomodationTypes(string searchTerm)
	{
		var source = context.AccomodationTypes.AsQueryable();
		if (!string.IsNullOrEmpty(searchTerm))
		{
			source = source.Where((AccomodationType a) => a.Name.ToLower().Contains(searchTerm.ToLower()));
		}

		return source.ToList();
	}
	public AccomodationType GetAccomodationTypeById(int Id)
    {
        return context.AccomodationTypes.Find(Id);
    }
    public bool UpdateAccomodationType(AccomodationType accomodationType)
    {

        context.AccomodationTypes.Update(accomodationType);
        return context.SaveChanges() > 0;
    }

	public bool DeleteAccomodationType(AccomodationType accomodationType)
	{

		context.AccomodationTypes.Remove(accomodationType);
		return context.SaveChanges() > 0;
	}
	public bool SaveAccomodationType(AccomodationType accomodationType)
    {
        context.AccomodationTypes.Add(accomodationType);
        var row = context.SaveChanges();
        return row > 0;

    }


}
