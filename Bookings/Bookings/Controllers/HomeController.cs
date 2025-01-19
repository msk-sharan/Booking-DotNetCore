using System.Diagnostics;
using Bookings.Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Bookings.Models;
using Bookings.ViewModels;

namespace Bookings.Controllers;

public class HomeController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public HomeController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        HomeVM homeVM = new()
        {
            VillaList = _unitOfWork.Villa.GetAll(includeProperties:"VillaAmenity"),
            Nights = 1,
            CheckInDate = DateOnly.FromDateTime(DateTime.Now)
        };
        return View(homeVM);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Error()
    {
        return View();
    }
}