using System.ComponentModel;
using Aidan.Common.Core;

namespace BrunelUni.WeatherStation.Core.Interfaces.Contract;

public interface IDHT20Service
{
    [ Description( "temperature ( c )" ) ]
    ObjectResult<double> ReadTemperature( );
    
    [ Description( "humidity ( % )" ) ]
    ObjectResult<double> ReadHumidity( );
}