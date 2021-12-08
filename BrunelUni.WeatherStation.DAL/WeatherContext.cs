using Aidan.Common.Core.Interfaces.Contract;
using BrunelUni.WeatherStation.Core;
using BrunelUni.WeatherStation.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BrunelUni.WeatherStation.DAL
{
    public class WeatherContext : DbContext
    {
        private readonly string _fileName;

        public WeatherContext( IConfigurationAdapter configurationAdapter )
        {
            _fileName = configurationAdapter.Get<AppOptions>( ).DbSource;
        }

        protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder ) =>
            optionsBuilder.UseSqlite( $"Data Source={_fileName};" );

        public DbSet<Temperature> TemperatureReadings { get; set; }
        public DbSet<Humidity> HumidityReadings { get; set; }
    }
}