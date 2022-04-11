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
using WeatherForecast.Services;

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
                var weathers = new List<WeatherDto>();
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
                                    int row = 5;

                                    while (true)
                                    {
                                        try {
                                            string date = ws.Cell($"A{row}").Value.ToString().Trim();
                                            string time = ws.Cell($"B{row}").Value.ToString().Trim();
                                            DateTime dateTime = ParseDateTime.Parse(date, time);

                                            double temperature = Convert.ToDouble(ws.Cell($"C{row}").Value.ToString().Trim());
                                            double relativeHumidity = Convert.ToDouble(ws.Cell($"D{row}").Value.ToString().Trim());
                                            double dewPoint = Convert.ToDouble(ws.Cell($"E{row}").Value.ToString().Trim());
                                            int atmosphericPressure = Convert.ToInt32(ws.Cell($"F{row}").Value.ToString().Trim());

                                            string wind = ws.Cell($"G{row}").Value.ToString().Trim();
                                            string windDirection = wind == "" ? null : wind;

                                            string speed = ws.Cell($"H{row}").Value.ToString().Trim();
                                            int? windSpeed = speed == "" ? null : Convert.ToInt32(speed);

                                            string cover = ws.Cell($"I{row}").Value.ToString().Trim();
                                            int? cloudCover = cover == "" ? null : Convert.ToInt32(cover);

                                            string lowerLimit = ws.Cell($"J{row}").Value.ToString().Trim();
                                            int? cloudLowerLimit = lowerLimit == "" ? null : Convert.ToInt32(lowerLimit);

                                            string visibility = ws.Cell($"K{row}").Value.ToString().Trim();
                                            string horizontalVisibility = visibility == "" ? null : visibility;

                                            string phenomena = ws.Cell($"L{row}").Value.ToString().Trim();
                                            string weatherPhenomena = phenomena == "" ? null : phenomena;


                                            weathers.Add(new WeatherDto
                                            {
                                                Date = dateTime,
                                                Temperature = temperature,
                                                RelativeHumidity = relativeHumidity,
                                                DewPoint = dewPoint,
                                                AtmosphericPressure = atmosphericPressure,
                                                WindDirection = windDirection,
                                                WindSpeed = windSpeed,
                                                CloudCover = cloudCover,
                                                CloudLowerLimit = cloudLowerLimit,
                                                HorizontalVisibility = horizontalVisibility,
                                                WeatherPhenomena = weatherPhenomena
                                            });

                                        }
                                        catch (Exception ex)
                                        {
                                            _logger.LogError(ex.Message + "  " +
                                               "InnerException:" + ex.InnerException);
                                        }

                                        string nextDate = ws.Cell($"A{row + 1}").Value.ToString().Trim();
                                        if(nextDate == "")
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            row++;
                                        }
                                    }
                                    try {
                                        await _weatherRepository.AddWeatherAsync(weathers);
                                    } 
                                    catch (Exception ex) 
                                    {
                                        _logger.LogError(ex.Message);
                                    }
                                    

                                    weathers.Clear();
                                }

                                
                            }
                        }
                    }
                }
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
