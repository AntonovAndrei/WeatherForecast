

using System;

namespace WeatherForecast.ViewModels
{
    public class PagingInfo
    {
        //years or month
        public string PageSearch {get; set; }
        public static int PageSize { get { return 24; } }
        public DateTime SearchDate { get; set; }
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }

        public int TotalPages =>
            (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
    }
}
