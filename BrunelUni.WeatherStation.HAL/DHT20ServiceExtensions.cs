using BrunelUni.WeatherStation.Core.Interfaces.Contract;

namespace BrunelUni.WeatherStation.HAL
{
    public static class DHT20ServiceExtensions
    {
        public static double GetTemperature( this IDHT20Service dht20Service ) => dht20Service.ReadTemperature( ).Value;
        public static double GetHumidity( this IDHT20Service dht20Service ) => dht20Service.ReadHumidity( ).Value;
    }
}