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
}
