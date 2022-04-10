using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using WeatherForecast.Domain.Dto;
using WeatherForecast.Domain.Repositories.Abstract;
using WeatherForecast.Models;

namespace WeatherForecast.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWeatherRepository _weatherRepository;

        public HomeController(ILogger<HomeController> logger, IWeatherRepository weatherRepository)
        {
            _logger = logger;
            _weatherRepository = weatherRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Import()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Import(List<IFormFile> filesExcel)
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine("Poshlo delo");
                var weathers = new List<WeatherDto>
                {
                    new WeatherDto
                    {
                        Date = new DateTime(2008, 3, 1, 7, 0, 0),
                        Temperature = -1.2,
                        RelativeHumidity = 95.2,
                        DewPoint = -1.1,
                        AtmosphericPressure = 762,
                        WindDirection = "Юг",
                        WindSpeed = 1,
                        CloudCover = 60,
                        CloudLowerLimit = 2500,
                        HorizontalVisibility = 10,
                        WeatherPhenomena = "Дымка"
                    },
                    new WeatherDto
                    {
                        Date = new DateTime(2008, 6, 1, 7, 30, 0),
                        Temperature = -4,
                        RelativeHumidity = 91,
                        DewPoint = -10,
                        AtmosphericPressure = 762,
                        WindDirection = "Ю",
                        WindSpeed = 1,
                        CloudCover = null,
                        CloudLowerLimit = 2500,
                        HorizontalVisibility = null,
                        WeatherPhenomena = null
                    }
                };

                await _weatherRepository.AddWeatherAsync(weathers);




                /*var weathers = new List<WeatherDto>();
                foreach (var excel in filesExcel)
                {
                    if (excel.Length > 0)
                    {
                        var filePath = Path.GetTempFileName();

                        using (var stream = System.IO.File.Create(filePath))
                        {
                            await excel.CopyToAsync(stream);
                            using (XLWorkbook workBook = new XLWorkbook(stream))
                            {
                                foreach (IXLWorksheet ws in workBook.Worksheets)
                                {

                                }
                            }
                        }
                    }
                }*/
            }

            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
