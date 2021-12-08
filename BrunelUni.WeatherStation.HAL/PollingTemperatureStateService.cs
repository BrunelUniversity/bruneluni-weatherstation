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
            ITaskService taskService ) :
            base( temperatureEventState, dht20Service.GetTemperature, taskService )
        {
        }
    }
}