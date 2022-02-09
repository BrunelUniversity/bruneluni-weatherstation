using Aidan.Common.Core.Interfaces.Contract;
using Aidan.Common.Utils.EventDriven;
using BrunelUni.WeatherStation.Core.Interfaces.Contract;

namespace BrunelUni.WeatherStation.HAL;

public class CurrentWeatherLogger : BasePollingService, ICurrentWeatherLogger
{
    private readonly ILoggerAdapter<ICurrentWeatherLogger> _loggerAdapter;

    public CurrentWeatherLogger( ITaskService taskService,
        ILoggerAdapter<ICurrentWeatherLogger> loggerAdapter,
        ITemperatureEventState temperatureEventState,
        IHumidityEventState humidityEventState )
        : base( ( ) =>
        {
            loggerAdapter.LogInfo( $"current temperature value is {temperatureEventState.Value}" );
            loggerAdapter.LogInfo( $"current humidity value is {humidityEventState.Value}" );
        }, 20000, taskService )
    {
    }
}