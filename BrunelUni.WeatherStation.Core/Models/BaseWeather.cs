using System;
using System.ComponentModel.DataAnnotations;

namespace BrunelUni.WeatherStation.Core.Models;

public abstract class BaseWeather
{
    [ Key ]
    public Guid Id { set; get; }

    public DateTime ReadingAt { get; set; }

    public BaseWeather( )
    {
        Id = Guid.NewGuid( );
        ReadingAt = DateTime.UtcNow;
    }
}