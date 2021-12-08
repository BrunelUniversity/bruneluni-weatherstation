using System;
using Aidan.Common.Utils.EventDriven;
using BrunelUni.WeatherStation.Core.Interfaces.Contract;

namespace BrunelUni.WeatherStation.HAL;

public class TemperatureChangesCondition : BaseEventCondition, ITemperatureChangesCondition
{
    public TemperatureChangesCondition( ITemperatureEventState temperatureEventState,
        ITemperatureRepository temperatureRepository ) : base( ( ) =>
    {
        var latestValue = temperatureRepository.GetLatest( ).Value.Celsius;
        return latestValue == null ? Math.Abs( temperatureEventState.Value - latestValue ) > 1 : true;
    } )
    {
    }
}