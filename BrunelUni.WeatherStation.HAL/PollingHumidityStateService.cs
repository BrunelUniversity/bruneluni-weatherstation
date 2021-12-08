using Aidan.Common.Core.Interfaces.Contract;
using Aidan.Common.Utils.EventDriven;
using BrunelUni.WeatherStation.Core.Interfaces.Contract;

namespace BrunelUni.WeatherStation.HAL;

public class PollingHumidityStateService : BasePollingStateService<double>, IPollingHumidityStateService
{
    public PollingHumidityStateService( IHumidityEventState eventState,
        ITaskService taskService,
        IDHT20Service dht20Service ) : base( eventState, dht20Service.GetHumidity, taskService )
    {
    }
}