using BusManagementSystem.Models;
using BusManagementSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data;

namespace BusManagementSystem.Controllers
{
    public class DriverController : Controller
    {
        private readonly ReservationSystemContext _ReservationSystemContext;
        private readonly ILogger<DriverController> _logger;
        private IDriverRepository DriverRepository;
        private IBusRepository BusRepository;
        private ILicenseTypeMappingRepository LicenseTypeMappingRepository;
        public DriverController(ReservationSystemContext reservationSystemContext, ILogger<DriverController> logger,IDriverRepository DriverRepository,IBusRepository BusRepository,ILicenseTypeMappingRepository LicenseTypeMappingRepository)
        {
            _ReservationSystemContext = reservationSystemContext;
            _logger = logger;
            this.DriverRepository = DriverRepository;
            this.BusRepository = BusRepository;
            this.LicenseTypeMappingRepository = LicenseTypeMappingRepository;
        }
        [Authorize(Roles = "Admin")]
        public IActionResult RemoveDriver([FromQuery] int DriverId)
        {
            try
            {
                DriverRepository.Delete(DriverRepository.Find(DriverId));
            }catch (DbUpdateException ex)
            {
                TempData["message"] = "<strong>The Driver cannot be removed because he is assigned to a trip.</strong>";
                return RedirectToAction("Index");
            }
            TempData["message"] = "<strong>Driver has been removed successfully.</strong>";
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AddDriver() {
            EditDriverViewModel editDriver = new EditDriverViewModel();
            Driver newDriver = new Driver
            {
                Name = "",
                Phone = 0,
                Email = "",
                BirthDate = new DateTime(1990, 1, 1),
                LicenseType = 1,
                LicenseExpiry = new DateTime(2023, 12, 31)
            };
            
            editDriver.Driver = newDriver;
            editDriver.Drivers  = DriverRepository.Drivers;
            editDriver.Buses = BusRepository.Buses;
            editDriver.LicenseTypes = LicenseTypeMappingRepository.LicenseTypes;

            return View(editDriver);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddDriver(EditDriverViewModel addDriver)
        {

            DriverRepository.Add(addDriver.Driver);
            TempData["message"] = "<strong>Driver Has been added successfully.</strong>";
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult EditDriver([FromQuery]int DriverId)
        {
            EditDriverViewModel editDriverViewModel = new EditDriverViewModel();
            editDriverViewModel.Driver = DriverRepository.Drivers.FirstOrDefault(d => d.DriverId == DriverId);
            editDriverViewModel.LicenseTypes = LicenseTypeMappingRepository.LicenseTypes;
            return View(editDriverViewModel);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult EditDriver(EditDriverViewModel EditDriverViewModel)
        {

            DriverRepository.Update(EditDriverViewModel.Driver);
            TempData["message"] = "<strong>Driver Has been modified successfully.</strong>";
            return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            var Drivers = DriverRepository.Drivers;
            ViewBag.Message = TempData["message"]?.ToString();
            return View(Drivers);
        }
    }
}