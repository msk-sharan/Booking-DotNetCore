using Bookings.Domain.Entities;
using Bookings.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bookings.Controllers;

public class VillaNumberController : Controller
{
    private readonly ApplicationDbContext _db;

    public VillaNumberController(ApplicationDbContext db)
    {
        _db = db;
    }
    public IActionResult Index()
    {
        var villaNumber = _db.VillaNumberss.ToList();
        return View(villaNumber);
    }

    public IActionResult Create()
    {
        //IEnumarabe is used to manage the collection of items
        //SelectListItem is a inbuilt method that is used for a dropdown
        //SelectListItem is a default method we should mention what should be shown
        //so we are not creating a new selectlistitem the text indicates what should be shown in teh dropdown
        //all teh values should be string in teh dropdown there will be id along the name so we should convert it to a string
        IEnumerable<SelectListItem> list = _db.Villas.ToList().Select(u => new SelectListItem
        {
            Text = u.Name,
            Value = u.Id.ToString()
        });
        //ViewData is used to pass data from controller to view
        //The reference is villalist and we can use teh list that is a reference
        //View data is a dictionary
        ViewData["VillaList"] = list;
        return View();
    }

    [HttpPost]
    public IActionResult Create(VillaNumber obj)
    {
        //We can do this so that it will not validate villa as it is just a navigator
        ModelState.Remove("Villa");
        if (ModelState.IsValid)
        {
            _db.VillaNumberss.Add(obj);
            _db.SaveChanges();
            TempData["success"] = "Villa NUmber Created successfully ";
            return RedirectToAction("Index", "VillaNumber");
        }

        return View();
    }
}