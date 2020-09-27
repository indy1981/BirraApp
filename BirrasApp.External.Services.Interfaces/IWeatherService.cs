using BirrasApp.External.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BirrasApp.External.Services.Interfaces
{
    public interface IWeatherService
    {
        Task<float?> GetWeatherByDay(DateTimeOffset date);
    }
}
