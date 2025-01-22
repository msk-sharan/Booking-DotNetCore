using Bookings.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bookings.ViewModels;

public class VillaNumberVM
{
    public VillaNumber? VillaNumber { get; set; }
    [ValidateNever]
    public IEnumerable<SelectListItem>? VillaList { get; set; }
}

//We are using this data as a seperate model because we dont have to use viewdata and viewbag we can pass
//teh data from tha controller to teh view through this method it will be the cleaner approch.