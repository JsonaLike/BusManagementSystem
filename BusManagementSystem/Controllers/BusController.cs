using BusManagementSystem.Models;
using BusManagementSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;

namespace BusManagementSystem.Controllers
{
    public class BusController : Controller
    {
        private readonly ILogger<CityController> _logger;
        private IBusRepository BusRepository;
        private IBusTypeMappingRepository BusTypeMappingRepository;

        public BusController(ILogger<CityController> logger,IBusRepository BusRepository,IBusTypeMappingRepository BusTypeMappingRepository)
        {
            _logger = logger;
            this.BusRepository = BusRepository;
            this.BusTypeMappingRepository = BusTypeMappingRepository;
        }
        public IActionResult Index()
        {
            var buses = BusRepository.Buses;
            ViewBag.Message = TempData["message"]?.ToString();
            return View(buses);
        }
        public IActionResult AddBus()
        {
            var EditView = new EditBusViewModel();
            EditView.Bus = new Bus();
            EditView.BusTypeMapping = BusTypeMappingRepository.BusTypes;
            return View(EditView);
        }

        [HttpPost]
        public IActionResult AddBus(EditBusViewModel EditBusViewModel)
        {
            _logger.LogInformation("EditBusViewModel.BusTypeChoice" + EditBusViewModel.BusTypeChoice);
            if(!EditBusViewModel.BusTypeChoice)
            BusRepository.Add(EditBusViewModel.Bus);
            else
            {
                BusTypeMapping busTypeMapping = new BusTypeMapping();//create a new bus type and add it
                busTypeMapping.BusType = EditBusViewModel.BusType;
                BusTypeMappingRepository.Add(busTypeMapping);
                EditBusViewModel.Bus.BusMapping = busTypeMapping;
                BusRepository.Add(EditBusViewModel.Bus);
            }
            TempData["message"] = "<strong>Bus Has been added successfully.</strong>";
            return RedirectToAction("Index");
        }
        [HttpPost("/EditBus")]
        public IActionResult EditBus([FromBody] Bus Bus)
        {
            BusRepository.Update(Bus);
            return Ok(Bus);
        }
        /*
        [HttpPost]
        public IActionResult EditBus(EditBusViewModel editBusViewModel)
        {
            var bus = BusRepository.Find(editBusViewModel.Bus.BusId);
            bus.PlateNumber = editBusViewModel.Bus.PlateNumber;
            bus.Model = editBusViewModel.Bus.Model;
            bus.Capacity = editBusViewModel.Bus.Capacity;
            bus.BusType = editBusViewModel.Bus.BusType;
            BusRepository.Update(bus);
            TempData["message"] = "<strong>Bus Has been modified successfully.</strong>";

            return RedirectToAction("Index");
        }*/
        /*
        public IActionResult EditBus([FromQuery] int busid)
        {
            var EditView = new EditBusViewModel();
            EditView.Bus = BusRepository.Find(busid);
            EditView.BusTypeMapping = BusTypeMappingRepository.BusTypes;
            return View(EditView);
        }*/
        public IActionResult RemoveBus([FromQuery] int busId)
        {
            BusRepository.Delete(BusRepository.Find(busId));
            TempData["message"] = "<strong>Bus Has been Removed successfully.</strong>";
            return RedirectToAction("Index");
        }
    }
}
