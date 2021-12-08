using Aidan.Common.Core;
using BrunelUni.WeatherStation.Core.Interfaces.Contract;
using BrunelUni.WeatherStation.Core.Models;

namespace BrunelUni.WeatherStation.DAL
{
    public class SqliteWeatherDataInitializer : IWeatherDataInitializer
    {
        private readonly WeatherContext _weatherContext;
        private readonly ITemperatureEventState _temperatureEventState;
        private readonly ITemperatureRepository _temperatureRepository;
        private readonly IDHT20Service _dht20Service;
        private readonly ITemperatureChangesCondition _temperatureChangesCondition;
        private readonly IHumidityEventState _humidityEventState;
        private readonly IHumidityRepository _humidityRepository;
        private readonly IHumidityChangesCondition _humidityChangesCondition;
        
        public SqliteWeatherDataInitializer( WeatherContext weatherContext,
            ITemperatureEventState temperatureEventState,
            ITemperatureRepository temperatureRepository,
            IDHT20Service dht20Service,
            ITemperatureChangesCondition temperatureChangesCondition, IHumidityEventState humidityEventState, IHumidityRepository humidityRepository, IHumidityChangesCondition humidityChangesCondition )
        {
            _weatherContext = weatherContext;
            _temperatureEventState = temperatureEventState;
            _temperatureRepository = temperatureRepository;
            _dht20Service = dht20Service;
            _temperatureChangesCondition = temperatureChangesCondition;
            _humidityEventState = humidityEventState;
            _humidityRepository = humidityRepository;
            _humidityChangesCondition = humidityChangesCondition;
        }

        public Result Initialize( )
        {
            _weatherContext.Database.EnsureCreated( );
            _temperatureEventState.ValueChangedEvent += () => _temperatureChangesCondition.Evaluate( );
            _temperatureChangesCondition.Valid += CreateTemperatureReading;
            _humidityEventState.ValueChangedEvent += () => _humidityChangesCondition.Evaluate( );
            _humidityChangesCondition.Valid += CreateHumidityReading;
            return Result.Success( );
        }

        private void CreateHumidityReading( )
        {
            _humidityRepository.Create( new Humidity
            {
                RelativeHumidity = _dht20Service.ReadHumidity( ).Value
            } );
        }

        private void CreateTemperatureReading( )
        {
            _temperatureRepository.Create( new Temperature
            {
                Celsius = _dht20Service.ReadTemperature( ).Value
            } );
        }
    }
}