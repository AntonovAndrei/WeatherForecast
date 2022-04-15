using System.Collections.Generic;

namespace WeatherForecast.ViewModels
{
    public class WeatherListViewModel
    {
        public IEnumerable<WeatherViewModel> Weathers { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
