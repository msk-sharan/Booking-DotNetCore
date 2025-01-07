using Bookings.Application.Common.Interfaces;
using Bookings.Domain.Entities;
using Bookings.Infrastructure.Data;
using Bookings.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Bookings.Controllers;

public class VillaNumberController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public VillaNumberController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public IActionResult Index()
    {
        var villaNumber = _unitOfWork.VillaNumber.GetAll(includeProperties:"Villa");
        return View(villaNumber);
    }

    public IActionResult Create()
    {
        //IEnumarabe is used to manage the collection of items
        //SelectListItem is a inbuilt method that is used for a dropdown
        //SelectListItem is a default method we should mention what should be shown
        //so we are not creating a new selectlistitem the text indicates what should be shown in teh dropdown
        //all teh values should be string in teh dropdown there will be id along the name so we should convert it to a string
      
        
        // IEnumerable<SelectListItem> list = _db.Villas.ToList().Select(u => new SelectListItem
        // {
        //     Text = u.Name,
        //     Value = u.Id.ToString()
        // });
      
        
        //ViewData is used to pass data from controller to view
        //The reference is villalist and we can use teh list that is a reference
        //View data is a dictionary
        // ViewData["VillaList"] = list;
        //View bag is a dynamic type property
        
        // ViewBag.VillaList = list;

        //we can use this method instead of the old method os that we can pass teh method to teh view as normal
        VillaNumberVM villaNumberVm = new()
        {
            VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            })
        };
        return View(villaNumberVm);
    }

    [HttpPost]
    public IActionResult Create(VillaNumberVM obj)
    {
        //This checks if the villa number already exists or not
        bool roomNumberExists = _unitOfWork.VillaNumber.Any(u => u.Villa_Number == obj.VillaNumber.Villa_Number);
        
        //We can do this so that it will not validate villa as it is just a navigator or we can just use validate never
        // ModelState.Remove("Villa");
        if (ModelState.IsValid && !roomNumberExists)
        {
            _unitOfWork.VillaNumber.Add(obj.VillaNumber);
            _unitOfWork.Save();
            TempData["success"] = "Villa NUmber Created successfully ";
            return RedirectToAction(nameof(Index));
        }

        if (roomNumberExists)
        {
            TempData["error"] = "The Villa Number already Exists.";
        }

        obj.VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
        {
            Text = u.Name,
            Value = u.Id.ToString()
        });
        return View(obj);
    }

    public IActionResult Update(int villaNumberId)
    {
        VillaNumberVM villaNumberVm = new()
        {
            VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            }),
            VillaNumber = _unitOfWork.VillaNumber.Get(u=>u.Villa_Number==villaNumberId)
        };
        if (villaNumberVm.VillaNumber == null)
        {
            return RedirectToAction("Error", "Home");
        }
        return View(villaNumberVm);
        
    }
    
    [HttpPost]
    public IActionResult Update(VillaNumberVM villaNumberVm)
    {
       
        //We can do this so that it will not validate villa as it is just a navigator or we can just use validate never
        // ModelState.Remove("Villa");
        if (ModelState.IsValid )
        {
            _unitOfWork.VillaNumber.Update(villaNumberVm.VillaNumber);
            _unitOfWork.Save();
            TempData["success"] = "Villa Number Updated successfully ";
            return RedirectToAction(nameof(Index));
        }

        villaNumberVm.VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
        {
            Text = u.Name,
            Value = u.Id.ToString()
        });
        return View(villaNumberVm);
    }
    
    public IActionResult Delete(int villaNumberId)
    {
        VillaNumberVM villaNumberVm = new()
        {
            VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            }),
            VillaNumber = _unitOfWork.VillaNumber.Get(u=>u.Villa_Number==villaNumberId)
        };
        if (villaNumberVm.VillaNumber == null)
        {
            return RedirectToAction("Error", "Home");
        }
        return View(villaNumberVm);
        
    }
    
    [HttpPost]
    public IActionResult Delete(VillaNumberVM villaNumberVm)
    {
        VillaNumber? objFromDb = _unitOfWork.VillaNumber
            .Get(u => u.Villa_Number == villaNumberVm.VillaNumber.Villa_Number);          
        if (objFromDb is not null)
        {
            //We can use Remove keyword to delete a file
            _unitOfWork.VillaNumber.Remove(objFromDb);
            _unitOfWork.Save();
            TempData["success"] = "Villa Number deleted successfully";
            //Instead of simply specifying the index name we can mention that in a name of method because 
            // when we simply specify teh index in teh double quotes it will not show any error for spelling
            // mistakes so we cannot find out what is teh problem but when we use name of it will automatically
            // show teh error so that we can change that.
            //Name of will not work for different controllers it will only work for teh same controllers
            return RedirectToAction(nameof(Index));
        }

        TempData["error"] = "Villa Number could not be Deleted";
        return View();
    }
}