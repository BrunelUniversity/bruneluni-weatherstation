using System.Collections.Generic;
using BrunelUni.WeatherStation.Core.Interfaces.Excluded;
using BrunelUni.WeatherStation.Core.Models;

namespace BrunelUni.WeatherStation.Core.Interfaces.Contract;

public interface IGetTemperatureReadingsCommand : ICommandWithReturn<IEnumerable<Temperature>>
{
}