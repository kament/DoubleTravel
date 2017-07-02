using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoubltTravel.Data.Countrues;
using Microsoft.AspNetCore.Mvc;

namespace DoubltTravel.Web.Controllers
{
    public class CountriesController : Controller
    {
        private ICountryRepository countryRepository;

        public CountriesController(ICountryRepository countryRepository)
        {
            this.countryRepository = countryRepository;
        }

        public async Task<IActionResult> Index(int id)
        {
            var country = await countryRepository.CountryByIdAsync(id);

            return View(country);
        }
    }
}
