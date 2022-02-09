using System.Collections.Generic;
using System.Linq;
using Aidan.Common.Core;
using Aidan.Common.Core.Enum;
using Aidan.Common.Core.Interfaces.Contract;
using BrunelUni.WeatherStation.Core.Interfaces.Contract;
using BrunelUni.WeatherStation.Core.Models;

namespace BrunelUni.WeatherStation.DAL;

public class HumidityRepository : IHumidityRepository
{
    private readonly WeatherContext _weatherContext;
    private readonly ILoggerAdapter<HumidityRepository> _loggerAdapter;

    public HumidityRepository( WeatherContext weatherContext, ILoggerAdapter<HumidityRepository> loggerAdapter )
    {
        _weatherContext = weatherContext;
        _loggerAdapter = loggerAdapter;
    }

    public ObjectResult<IEnumerable<Humidity>> GetAll( )
    {
        return new ObjectResult<IEnumerable<Humidity>>
        {
            Status = OperationResultEnum.Success,
            Value = _weatherContext.HumidityReadings
        };
    }

    public ObjectResult<Humidity> GetLatest( )
    {
        return new ObjectResult<Humidity>
        {
            Status = OperationResultEnum.Success,
            Value = _weatherContext.HumidityReadings.OrderByDescending( x => x.ReadingAt ).FirstOrDefault( )
        };
    }

    public Result Create( Humidity humidity )
    {
        _weatherContext.HumidityReadings.Add( humidity );
        _loggerAdapter.LogInfo( $"humidity record created {humidity.Id} of {humidity.RelativeHumidity} relative humidity" );
        _weatherContext.SaveChanges( );
        return Result.Success( );
    }
}