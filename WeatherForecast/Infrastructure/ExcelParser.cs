using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using WeatherForecast.Domain.Dto;

namespace WeatherForecast.Infrastructure
{
    public static class ExcelParser
    {
        public static List<WeatherDto> ParseSheet(IXLWorksheet ws)
        {
            int row = 5;

            List<WeatherDto> weathers = new List<WeatherDto>();

            while (true)
            {
                try
                {
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
                catch
                {
                    throw new Exception("Error when parsing data!");
                }

                string nextDate = ws.Cell($"A{row + 1}").Value.ToString().Trim();
                if (nextDate == "")
                {
                    break;
                }
                else
                {
                    row++;
                }
            }

            return weathers;
        }
    }
}
