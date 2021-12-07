using System.Collections.Generic;
using Aidan.Common.Core;
using BrunelUni.WeatherStation.Core.Models;

namespace BrunelUni.WeatherStation.Core.Interfaces.Contract
{
    public interface ITemperatureRepository
    {
        ObjectResult<IEnumerable<Temperature>> GetAll( );
        Result Create( Temperature temperature );
    }
}