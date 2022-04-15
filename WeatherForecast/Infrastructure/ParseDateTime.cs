using System;

namespace WeatherForecast.Infrastructure
{
    public static class ParseDateTime
    {
        public static DateTime Parse(string date, string time)
        {
            string dateTime = date.Replace(".", "/") + " " + time + ":00";
            DateTime myDate = DateTime.Parse(dateTime);
            return myDate;
        }
    }
}
