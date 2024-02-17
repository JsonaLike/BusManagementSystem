using BusManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Text.Encodings.Web;

namespace BusManagementSystem.Controllers
{
    public class CityController : Controller
    {
        private readonly ReservationSystemContext _ReservationSystemContext;
        private readonly ILogger<CityController> _logger;
        private readonly ICityRepository _cityRepository;

        public CityController(ReservationSystemContext reservationSystemContext, ILogger<CityController> logger,ICityRepository cityRepository)
        {
            _ReservationSystemContext = reservationSystemContext;
            _logger = logger;
            _cityRepository = cityRepository;
        }
        public IActionResult Index()
        {
            var cities = _cityRepository.Cities;
            //ViewBag.Message = TempData["message"]?.ToString();
            return View(cities);
        }
        public IActionResult GetCities()
        {
            var cities = _cityRepository.Cities;
            ViewBag.Message = TempData["message"]?.ToString();

            return Json(cities);
        }
        /*public IActionResult AddaNewCity()
        {

        }*/
        public IActionResult AddCity()
        {
            City city = new City();
            return View(city);
        }

        [HttpPost]
        public IActionResult AddCity(City c)
        {
            try
            {
                _cityRepository.Add(c);
                /*_ReservationSystemContext.Cities.Add(c);
                _ReservationSystemContext.SaveChanges();*/
                TempData["message"] = "<strong>City has been added successfully</strong>";
            }
            catch (Exception ex)
            {
                TempData["message"] = "<strong>Cannot insert dublicated cities</strong> ";
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        public IActionResult EditCity([FromQuery] int CityId)
        {
            City city = _cityRepository.Find(CityId);
            return View(city);
        }
        [HttpPost]
        public IActionResult EditCity(City c)
        {
            try
            {
                _cityRepository.Update(c);

                TempData["message"] = "<strong>City has been edited successfully</strong>";
            }
            catch (Exception ex)
            {
                TempData["message"] = "<strong>Cannot Insert dublicated cities</strong>";
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        public IActionResult RemoveCity([FromQuery] int CityId)
        {
            string name = _ReservationSystemContext.Cities.Find(CityId).Name; 
            try
            {
            _cityRepository.Delete(_cityRepository.Find(CityId));
           //     _ReservationSystemContext.SaveChanges();
            }catch(Exception ex)
            {
                TempData["message"] = "<strong>Cannot remove the city because it is assigned as a departure or arrival for one or more trips.</strong>";
                return RedirectToAction("Index");
            }
            TempData["message"] = "<strong>City " + name + "has been removed sucessfully.</strong>";
            return RedirectToAction("Index");
        }
    }
}
