using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using ResortMan.Entities;
using System.ComponentModel.DataAnnotations;

namespace ResortMan.MvcApp.Areas.Staff.ViewModels;

public class BookingActionModel
{
    public int Id { get; set; }
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;

	public int AccomodationId { get; set; }

    public Accomodation Accomodation { get; set; } = null!;

    public DateTime FromDate { get; set; }

    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; }

    public int Duration { get; set; }
    public string? Note { get; set; }

    [BindNever, ValidateNever]
    public IEnumerable<Accomodation> Accomodations { get; set; } = null!;
}
