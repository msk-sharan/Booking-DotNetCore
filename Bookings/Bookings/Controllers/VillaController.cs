using Bookings.Application.Common.Interfaces;
using Bookings.Domain.Entities;
using Bookings.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace Bookings.Controllers
{
    public class VillaController : Controller
    {
        //Impoting the application db context from the Data section
        // private readonly ApplicationDbContext _db;
        
        //Implementing Ivilla Repository
        private readonly IVillaRepository _villaRepo;
        
        //Creating a constructor to use the db context as its parameter and as a reference
        public VillaController(IVillaRepository villaRepo)
        {
            _villaRepo = villaRepo;
        }
        // GET: VillaController
        //This is teh index view of teh villa controller that returns all teh villas available
        public IActionResult Index()
        {
            //The toList method gets all teh villas from teh db and arrange it as a list .
            var villas = _villaRepo.GetAll();
            return View(villas);
        }

        public IActionResult Create()
        {
            return View(); 
        }

        [HttpPost]
        public IActionResult Create(Villa obj)
        {
            if (obj.Name == obj.Description)
            {
                ModelState.AddModelError("name","The Name cannot exactly match he Description. ");
            }
            if (ModelState.IsValid)
            {
                _villaRepo.Add(obj);
                _villaRepo.Save();
                TempData["success"] = "Villa Created successfully";
                return RedirectToAction("Index","Villa");
            }
            TempData["error"] = "Villa could not be Created";

            return View();
        }

        
        // This is the get method for teh update method this gets teh id to be updated and forward it to a view action 
        
        public IActionResult Update(int villaId)
        {
            // The u method is known as teh link expression-refering u as  the mode and getting 
            //     the specified value from it and checking it with something.
            // While checking if the db has multiple values for that id the FirstOrDefault method will give the first value
            
            // Villa? obj = _db.Villas.FirstOrDefault(u => u.Id == villaId);
            //We can do this instead of this
            Villa? obj = _villaRepo.Get(u => u.Id == villaId);
            
            
            // Villa? obj = _db.Villas.Find(villaId);
            //We can do many things using the link expression.
            // var VillaList = _db.Villas.Where(u => u.Price > 50 && u.Occupancy > 5);
            if (obj is null)
            {
                return RedirectToAction("Error","Home");
            }

            return View(obj);
        }
        
        //Update is teh same as teh create 
        [HttpPost]
        public IActionResult Update(Villa obj)
        {
            //This gets the id nad updates that specific field
           
            if (ModelState.IsValid && obj.Id>0)
            {
                //We can use update keyword instead of teh add keyword
                _villaRepo.Update(obj);
                _villaRepo.Save();
                TempData["success"] = "Villa Updated successfully";
                return RedirectToAction("Index","Villa");
            }
            TempData["error"] = "Villa could not be Updated";


            return View();
        }
        
        public IActionResult Delete(int villaId)
        {
            Villa? obj = _villaRepo.Get(u => u.Id == villaId);
            if (obj is null)
            {
                return RedirectToAction("Error","Home");
            }

            return View(obj);
        }
        
         //Delete is the same as teh create 
        [HttpPost]
        public IActionResult Delete(Villa obj)
        {
            Villa? objFromDb = _villaRepo.Get(u => u.Id == obj.Id);          
            if (objFromDb is not null)
            {
                //We can use Remove keyword to delete a file
                _villaRepo.Remove(objFromDb);
                _villaRepo.Save();
                TempData["success"] = "Villa deleted successfully";
                return RedirectToAction("Index","Villa");
            }

            TempData["error"] = "Villa could not be Deleted";
            return View();
        }

    }
}
