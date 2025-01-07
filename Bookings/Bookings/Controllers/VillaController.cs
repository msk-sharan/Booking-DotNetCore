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
        // private readonly IVillaRepository _villaRepo;
        //We are using unit of work here so we dont need to create the repository 
        private readonly IUnitOfWork _unitOfWork;
        //It is a inbuilt method in the .net so that we can save the uploaded image in teh wwwroot folder
        private readonly IWebHostEnvironment _webHostEnvironment;
        
        //Creating a constructor to use the db context as its parameter and as a reference
        public VillaController(IUnitOfWork unitOfWork,IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        // GET: VillaController
        //This is teh index view of teh villa controller that returns all teh villas available
        public IActionResult Index()
        {
            //The toList method gets all teh villas from teh db and arrange it as a list .
            var villas = _unitOfWork.Villa.GetAll();
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
                if (obj.Image != null)
                {
                    //the guid command is used for changing the file name and teh path .get extension is used to get the image type
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(obj.Image.FileName);
                    //this image path is telling teh file to sVED IN TEH WWWROOT in the villaimage folder
                    string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, @"Images/VillaImage");
                    //this line is used to get the file from teh filename and save the image to teh eeeroot folder by combining the two paths
                    //teh file mode is set to teh required api
                    using var fileStram = new FileStream(Path.Combine(imagePath, fileName), FileMode.Create);
                    //then teh image is copied to teh file stream where it has teh path to teh folder that it has to be saved
                    obj.Image.CopyTo(fileStram);

                    obj.ImageUrl = @"Images/VillaImage" + fileName;

                }
                else 
                {
                    obj.ImageUrl = "http://placehold.co/600x400";
                }
                //since unit of work contains all teh repositories we can generally use unit of work along
                //with that we should mention what repository are we using
                _unitOfWork.Villa.Add(obj);
                _unitOfWork.Save();
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
            Villa? obj = _unitOfWork.Villa.Get(u => u.Id == villaId);
            
            
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
                
                if (obj.Image != null)
                {
                    //the guid command is used for changing the file name and teh path .get extension is used to get the image type
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(obj.Image.FileName);
                    //this image path is telling teh file to sVED IN TEH WWWROOT in the villaimage folder
                    string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, @"Images/VillaImage");

                    //this command checks if the image exists
                    if (!string.IsNullOrEmpty(obj.ImageUrl))
                    {
                        //when the image is saved the name will 1st start at // so we should trim it
                        var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    
                    
                    //this line is used to get the file from teh filename and save the image to teh eeeroot folder by combining the two paths
                    //teh file mode is set to teh required api
                    using var fileStram = new FileStream(Path.Combine(imagePath, fileName), FileMode.Create);
                    //then teh image is copied to teh file stream where it has teh path to teh folder that it has to be saved
                    obj.Image.CopyTo(fileStram);

                    obj.ImageUrl = @"Images/VillaImage" + fileName;

                }
                else 
                {
                    obj.ImageUrl = "http://placehold.co/600x400";
                }
                //We can use update keyword instead of teh add keyword
                _unitOfWork.Villa.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Villa Updated successfully";
                return RedirectToAction("Index","Villa");
            }
            TempData["error"] = "Villa could not be Updated";


            return View();
        }
        
        public IActionResult Delete(int villaId)
        {
            Villa? obj = _unitOfWork.Villa.Get(u => u.Id == villaId);
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
            Villa? objFromDb = _unitOfWork.Villa.Get(u => u.Id == obj.Id);          
            if (objFromDb is not null)
            {
                if (!string.IsNullOrEmpty(objFromDb.ImageUrl))
                {
                    var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                
                //We can use Remove keyword to delete a file
                _unitOfWork.Villa.Remove(objFromDb);
                _unitOfWork.Save();
                TempData["success"] = "Villa deleted successfully";
                return RedirectToAction("Index","Villa");
            }

            TempData["error"] = "Villa could not be Deleted";
            return View();
        }

    }
}
