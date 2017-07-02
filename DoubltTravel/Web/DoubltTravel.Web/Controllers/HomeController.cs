using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoubltTravel.Data.Countrues;
using Microsoft.AspNetCore.Mvc;

namespace DoubltTravel.Web.Controllers
{
    public class HomeController : Controller
    {
        private ICountryRepository countryRepository;

        public HomeController(ICountryRepository countryRepository)
        {
            this.countryRepository = countryRepository;
        }

        public async Task<IActionResult> Index()
        {
            var countries = await countryRepository.CountriesAsync();

            return View(countries);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Здравейте, добре дошли ви BoubltTravel, тук можете да намерите повече информация за държавата която смятате да посетите.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Може да се свържете с нас на посочените email-и за връзка.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
