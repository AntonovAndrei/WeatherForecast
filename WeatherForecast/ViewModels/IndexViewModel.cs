using System.Collections.Generic;

namespace WeatherForecast.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<WeatherViewModel> Weathers { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
