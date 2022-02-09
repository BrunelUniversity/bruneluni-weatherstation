using System;
using Aidan.Common.Core.Interfaces.Contract;
using Aidan.Common.Utils.EventDriven;
using BrunelUni.WeatherStation.Core.Interfaces.Contract;

namespace BrunelUni.WeatherStation.HAL;

public class HumidityChangesCondition : BaseEventCondition, IHumidityChangesCondition
{
    public HumidityChangesCondition( IHumidityEventState humidityEventState,
        IHumidityRepository humidityRepository,
        ILoggerAdapter<IHumidityChangesCondition> loggerAdapter ) : base( ( ) =>
    {
        var latestValue = humidityRepository.GetLatest( ).Value;
        if( latestValue == null )
        {
            loggerAdapter.LogError( $"latest humidity value is null" );
        }
        else
        {
            loggerAdapter.LogDebug( $"latest humidity value is {latestValue.RelativeHumidity}" );
            loggerAdapter.LogDebug( $"difference in changes in humidity are {Math.Abs( humidityEventState.Value - latestValue.RelativeHumidity )}" );
        }
        loggerAdapter.LogDebug( $"current humidity value is {humidityRepository.GetLatest( )}" );
        if( humidityRepository.GetLatest( ).Value == null )
        {
            return true;
        }
        return Math.Abs( humidityEventState.Value - latestValue.RelativeHumidity ) > 2.25;
    } )
    {
    }
}