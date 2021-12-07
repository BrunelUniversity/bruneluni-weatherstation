using System;
using System.ComponentModel.DataAnnotations;

namespace BrunelUni.WeatherStation.Core.Models;

public abstract class BaseModel
{
    [ Key ]
    public Guid Id { set; get; }

    public DateTime Created { get; set; }

    public BaseModel( )
    {
        Id = Guid.NewGuid( );
        Created = DateTime.UtcNow;
    }
}