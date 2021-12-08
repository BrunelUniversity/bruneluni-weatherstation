using Aidan.Common.Core.Interfaces.Contract;
using Aidan.Common.Utils.EventDriven;
using BrunelUni.WeatherStation.Core.Interfaces.Contract;

namespace BrunelUni.WeatherStation.HAL
{
    public class PollingTemperatureStateService :
        BasePollingStateService<double>,
        IPollingTemperatureStateService
    {
        public PollingTemperatureStateService( ITemperatureEventState temperatureEventState,
            IDHT20Service dht20Service,
            ITaskService taskService,
            ILoggerAdapter<IPollingTemperatureStateService> pollingTemperatureStateService ) :
            base( temperatureEventState, () =>
        {
            var temp = dht20Service.GetTemperature( );
            pollingTemperatureStateService.LogInfo( $"current temperature {temp}" );
            return dht20Service.GetTemperature( );
        }, taskService )
        {
        }
    }
}