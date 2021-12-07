using Aidan.Common.Utils.EventDriven;
using BrunelUni.WeatherStation.Core.Interfaces.Contract;

namespace BrunelUni.WeatherStation.HAL
{
    public class TemperatureState : BaseEventState<double>, ITemperatureEventState
    {
    }
}