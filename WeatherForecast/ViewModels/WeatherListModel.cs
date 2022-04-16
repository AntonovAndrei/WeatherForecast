using System.Collections.Generic;

namespace WeatherForecast.ViewModels
{
    public class WeatherListModel
    {
        public IEnumerable<WeatherViewModel> Weathers { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
