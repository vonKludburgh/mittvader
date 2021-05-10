using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorComponentDemo.Services
{
    interface IDAL
    {
        IEnumerable<Models.Measurement> GetWeatherData();
    }
}
