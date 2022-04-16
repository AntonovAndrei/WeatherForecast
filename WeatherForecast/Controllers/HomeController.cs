using AutoMapper;
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
using WeatherForecast.Infrastructure;
using WeatherForecast.ViewModels;

namespace WeatherForecast.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWeatherRepository _weatherRepository;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, IWeatherRepository weatherRepository, IMapper mapper)
        {
            _mapper = mapper;
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
                List<WeatherDto> weathers = new List<WeatherDto>();

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
                                    try
                                    {
                                        weathers = ExcelParser.ParseSheet(ws);
                                    }
                                    catch (Exception ex)
                                    {
                                        _logger.LogError(ex.Message);
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


        public async Task<IActionResult> Weathers(DateTime startDate, int pageNumber = 1)
        {

            WeatherListModel weatherList = await _weatherRepository.GetWeatherByMonthWithPagingAsync(startDate, pageNumber);

            return View(weatherList);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
