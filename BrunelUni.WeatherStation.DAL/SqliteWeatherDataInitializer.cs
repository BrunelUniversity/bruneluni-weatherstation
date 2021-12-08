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
        private readonly ITemperatureChangesCondition _temperatureChangesCondition;
        private readonly IHumidityEventState _humidityEventState;
        private readonly IHumidityRepository _humidityRepository;
        private readonly IHumidityChangesCondition _humidityChangesCondition;
        
        public SqliteWeatherDataInitializer( WeatherContext weatherContext,
            ITemperatureEventState temperatureEventState,
            ITemperatureRepository temperatureRepository,
            ITemperatureChangesCondition temperatureChangesCondition,
            IHumidityEventState humidityEventState,
            IHumidityRepository humidityRepository,
            IHumidityChangesCondition humidityChangesCondition )
        {
            _weatherContext = weatherContext;
            _temperatureEventState = temperatureEventState;
            _temperatureRepository = temperatureRepository;
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
                RelativeHumidity = _humidityEventState.Value
            } );
        }

        private void CreateTemperatureReading( )
        {
            _temperatureRepository.Create( new Temperature
            {
                Celsius = _temperatureEventState.Value
            } );
        }
    }
}