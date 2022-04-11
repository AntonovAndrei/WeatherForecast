using System;

namespace WeatherForecast.Services
{
    public static class ParseDateTime
    {
        public static DateTime Parse(string date, string time)
        {
            string dateTime = date.Replace(".", "/") + " " + time + ":00";
            Console.WriteLine(dateTime);
            DateTime myDate = DateTime.Parse(dateTime);
            return myDate;
        }
    }
}
