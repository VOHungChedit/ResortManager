using Microsoft.EntityFrameworkCore;
using ResortMan.Data;
using ResortMan.Entities;

namespace ResortMan.Services
{
	public class AccomodationPackagesService
	{
		private readonly ApplicationDbContext context;
		public AccomodationPackagesService(ApplicationDbContext context)
		{
			this.context = context;
		}
		public IEnumerable<AccomodationPackage> GetAccomodationPackages()
		{
			return context.AccomodationsPackages
				.Include(ap => ap.AccomodationType)
				.Include(ap => ap.Pictures)
				.Select(ap => new AccomodationPackage()
				{
					Id = ap.Id,
					Name = ap.Name,
					AccomodationTypeId = ap.AccomodationTypeId,
					NoOfRoom = ap.NoOfRoom,
					FeePerNight = ap.FeePerNight,
					Pictures = ap.Pictures.Select(p => new AccomodationPackagePicture()
					{
						Id = p.Id,
						ContentType = p.ContentType
					}).ToList(),
					AccomodationType = ap.AccomodationType,
				})
				.ToList();
		}

		public IEnumerable<AccomodationPackage> SearchAccomodationPackages(string searchTerm)
		{
			var source = context.AccomodationsPackages.AsQueryable();
			if (!string.IsNullOrEmpty(searchTerm))
			{
				source = source.Where((AccomodationPackage a) => a.Name.ToLower().Contains(searchTerm.ToLower()));
			}

			return source.ToList();
		}
		public AccomodationPackage? GetAccomodationPackageById(int Id)
		{
			return context.AccomodationsPackages
				.Include(ap => ap.AccomodationType)
				.Include(ap => ap.Pictures)
				.Select(ap => new AccomodationPackage()
				{
					Id = ap.Id,
					Name = ap.Name,
					AccomodationTypeId = ap.AccomodationTypeId,
					NoOfRoom = ap.NoOfRoom,
					FeePerNight = ap.FeePerNight,
					Pictures = ap.Pictures.Select(p => new AccomodationPackagePicture()
					{
						Id = p.Id,
						ContentType = p.ContentType
					}).ToList(),
					AccomodationType = ap.AccomodationType,
					Accomodations = ap.Accomodations
				}).FirstOrDefault(e => e.Id == Id);
		}

		public AccomodationPackage? GetAccomodationPackageByIdWithReadyAccomodation(int id)
		{
			return context.AccomodationsPackages
				.Include(ap => ap.AccomodationType)
				.Include(ap => ap.Pictures)
				.Include(ap => ap.Accomodations.Where(a => a.IsReady))
				.Select(ap => new AccomodationPackage()
				{
					Id = ap.Id,
					Name = ap.Name,
					AccomodationTypeId = ap.AccomodationTypeId,
					NoOfRoom = ap.NoOfRoom,
					FeePerNight = ap.FeePerNight,
					Pictures = ap.Pictures.Select(p => new AccomodationPackagePicture()
					{
						Id = p.Id,
						ContentType = p.ContentType
					}).ToList(),
					AccomodationType = ap.AccomodationType,
					Accomodations = ap.Accomodations
				}).FirstOrDefault(e => e.Id == id);
		}
		public bool UpdateAccomodationPackage(AccomodationPackage accomodationPackage)
		{
			context.AccomodationsPackages.Update(accomodationPackage);
			return context.SaveChanges() > 0;
		}
		public bool DeleteAccomodationPackage(AccomodationPackage accomodationPackage)
		{

			context.AccomodationsPackages.Remove(accomodationPackage);
			return context.SaveChanges() > 0;
		}
		public bool SaveAccomodationPackage(AccomodationPackage accomodationPackage)
		{
			context.AccomodationsPackages.Add(accomodationPackage);
			var row = context.SaveChanges();
			return row > 0;

		}

		public IEnumerable<AccomodationPackage> GetPromote(int count)
		{
			var data = context.AccomodationsPackages
			   .Include(ap => ap.AccomodationType)
			   .Include(ap => ap.Pictures.Take(1))
			   .Select(ap => new AccomodationPackage()
			   {
				   Id = ap.Id,
				   Name = ap.Name,
				   AccomodationTypeId = ap.AccomodationTypeId,
				   NoOfRoom = ap.NoOfRoom,
				   FeePerNight = ap.FeePerNight,
				   Pictures = ap.Pictures.Select(p => new AccomodationPackagePicture()
				   {
					   Id = p.Id,
					   ContentType = p.ContentType
				   }).ToList(),
				   AccomodationType = ap.AccomodationType,
			   })
			   .Take(count)
			   .ToList();
			return data;
		}
	}
}
