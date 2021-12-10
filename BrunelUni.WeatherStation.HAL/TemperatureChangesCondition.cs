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
        var latestValue = temperatureRepository.GetLatest( ).Value;
        if( latestValue == null )
        {
            loggerAdapter.LogInfo( $"latest temperature value is null" );
        }
        else
        {
            loggerAdapter.LogInfo( $"latest temperature value is {latestValue.Celsius}" );
            loggerAdapter.LogInfo( $"difference in changes in temperature are {Math.Abs( temperatureEventState.Value - latestValue.Celsius )}" );
        }
        loggerAdapter.LogInfo( $"current temperature value is {temperatureEventState.Value}" );
        if( temperatureRepository.GetLatest( ).Value == null )
        {
            return true;
        }
        return Math.Abs( temperatureEventState.Value - latestValue.Celsius ) > 0.175;
    } )
    {
    }
}