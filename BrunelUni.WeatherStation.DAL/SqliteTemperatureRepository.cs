using System;
using System.Collections.Generic;
using System.Linq;
using Aidan.Common.Core;
using Aidan.Common.Core.Enum;
using Aidan.Common.Core.Interfaces.Contract;
using BrunelUni.WeatherStation.Core.Interfaces.Contract;
using BrunelUni.WeatherStation.Core.Models;

namespace BrunelUni.WeatherStation.DAL
{
    public class SqliteTemperatureRepository : ITemperatureRepository
    {
        private readonly WeatherContext _weatherContext;
        private readonly ILoggerAdapter<ITemperatureRepository> _loggingAdapter;

        public SqliteTemperatureRepository( WeatherContext weatherContext, ILoggerAdapter<ITemperatureRepository> loggingAdapter )
        {
            _weatherContext = weatherContext;
            _loggingAdapter = loggingAdapter;
        }

        public ObjectResult<IEnumerable<Temperature>> GetAll( ) => new( )
        {
            Status = OperationResultEnum.Success,
            Value = _weatherContext.TemperatureReadings
        };

        public ObjectResult<Temperature> GetLatest( )
        {
            Temperature value;
            try
            {
                value = _weatherContext
                    .TemperatureReadings
                    .OrderByDescending( x => x.ReadingAt )
                    .FirstOrDefault( );
            }
            catch( ArgumentException )
            {
                value = null;
            }

            return new ObjectResult<Temperature>
            {
                Value = value,
                Status = OperationResultEnum.Success
            };
        }

        public Result Create( Temperature temperature )
        {
            _weatherContext.TemperatureReadings.Add( temperature );
            _weatherContext.SaveChanges( );
            _loggingAdapter.LogInfo( $"temperature record created" );
            return Result.Success( );
        }
    }
}