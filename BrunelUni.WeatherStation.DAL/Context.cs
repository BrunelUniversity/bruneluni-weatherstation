using System.Reflection;
using BrunelUni.WeatherStation.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BrunelUni.WeatherStation.DAL;

public class Context : DbContext
{
    protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder )
    {
        optionsBuilder.UseSqlite( @"FileName=mydatabase.db;", builder =>
        {
            builder.MigrationsAssembly( Assembly.GetExecutingAssembly( ).FullName );
        } );
    }

    public DbSet<Temperature> TemperatureReadings { get; set; }
    public DbSet<Humidity> HumidityReadings { get; set; }
}