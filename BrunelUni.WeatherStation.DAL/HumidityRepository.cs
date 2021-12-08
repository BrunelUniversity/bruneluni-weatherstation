using System.Collections.Generic;
using System.Linq;
using Aidan.Common.Core;
using Aidan.Common.Core.Enum;
using BrunelUni.WeatherStation.Core.Interfaces.Contract;
using BrunelUni.WeatherStation.Core.Models;

namespace BrunelUni.WeatherStation.DAL;

public class HumidityRepository : IHumidityRepository
{
    private readonly WeatherContext _weatherContext;

    public HumidityRepository( WeatherContext weatherContext ) { _weatherContext = weatherContext; }

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

    public Result Create( Humidity temperature )
    {
        _weatherContext.HumidityReadings.Add( temperature );
        _weatherContext.SaveChanges( );
        return Result.Success( );
    }
}