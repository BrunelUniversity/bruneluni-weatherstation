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

        public SqliteWeatherDataInitializer( WeatherContext weatherContext,
            ITemperatureEventState temperatureEventState,
            ITemperatureRepository temperatureRepository,
            IDHT20Service dht20Service,
            ITemperatureChangesCondition temperatureChangesCondition )
        {
            _weatherContext = weatherContext;
            _temperatureEventState = temperatureEventState;
            _temperatureRepository = temperatureRepository;
            _dht20Service = dht20Service;
            _temperatureChangesCondition = temperatureChangesCondition;
        }

        public Result Initialize( )
        {
            _weatherContext.Database.EnsureCreated( );
            _temperatureEventState.ValueChangedEvent += () => _temperatureChangesCondition.Evaluate( );
            _temperatureChangesCondition.Valid += CreateReading;
            return Result.Success( );
        }

        private void CreateReading( )
        {
            _temperatureRepository.Create( new Temperature
            {
                Celsius = _dht20Service.ReadHumidity( ).Value
            } );
        }
    }
}