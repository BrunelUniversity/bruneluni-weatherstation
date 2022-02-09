using System.Collections.Generic;
using Aidan.Common.Core;
using BrunelUni.WeatherStation.Core.Models;

namespace BrunelUni.WeatherStation.Core.Interfaces.Contract;

public interface IHumidityRepository
{
    ObjectResult<IEnumerable<Humidity>> GetAll( );
    ObjectResult<Humidity> GetLatest( );
    Result Create( Humidity humidity );
}