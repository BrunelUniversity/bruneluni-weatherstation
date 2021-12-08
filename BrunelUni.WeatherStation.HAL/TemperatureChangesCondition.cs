using System;
using Aidan.Common.Core.Interfaces.Contract;
using Aidan.Common.Utils.EventDriven;
using BrunelUni.WeatherStation.Core.Interfaces.Contract;

namespace BrunelUni.WeatherStation.HAL;

public class TemperatureChangesCondition : BaseEventCondition, ITemperatureChangesCondition
{
    public TemperatureChangesCondition( ITemperatureEventState temperatureEventState,
        ITemperatureRepository temperatureRepository,
        ILoggerAdapter<ITemperatureChangesCondition> loggerAdapter ) : base( ( ) =>
    {
        var latestValue = temperatureRepository.GetLatest( ).Value.Celsius;
        loggerAdapter.LogInfo( $"latest value is {latestValue}" );
        loggerAdapter.LogInfo( $"current value is {temperatureEventState.Value}" );
        return latestValue == null ? true : Math.Abs( temperatureEventState.Value - latestValue ) > 1;
    } )
    {
    }
}