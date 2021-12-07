using System;
using System.Linq;
using System.Threading.Tasks;
using Aidan.Common.Core;
using Aidan.Common.Core.Enum;
using BrunelUni.WeatherStation.Core.Interfaces.Contract;

namespace BrunelUni.WeatherStation.HAL
{
    public class DHT20Service : IDHT20Service
    {
        private readonly I2CPiServiceFactory _i2CPiServiceFactory;

        public DHT20Service( I2CPiServiceFactory i2CPiServiceFactory ) { _i2CPiServiceFactory = i2CPiServiceFactory; }

        public ObjectResult<double> ReadTemperature( )
        {
            var value = GetReading( );
            var tempRaw = new [ ]
            {
                ( value[ 3 ] & 0x0F ) << 16,
                value[ 4 ] << 8,
                value[ 5 ]
            }.Sum( );
            return new ObjectResult<double>
            {
                Status = OperationResultEnum.Success,
                Value = ( ( tempRaw / Math.Pow( 2, 20 ) ) * ( 200 ) ) - 50
            };
        }

        public ObjectResult<double> ReadHumidity( )
        {
            var value = GetReading( );
            var humRaw = new [ ]
            {
                value[ 1 ] << 12,
                value[ 2 ] << 4,
                ( value[ 3 ] & 0xF0 ) >> 4
            }.Sum( );
            return new ObjectResult<double>
            {
                Status = OperationResultEnum.Success,
                Value = ( humRaw / Math.Pow( 2, 20 ) ) * ( 100 )
            };
        }

        private T Get<T>( Func<I2CPiService, T> handler )
        {
            var service = _i2CPiServiceFactory.Factory( 0x38 );
            Task.Delay( 500 );
            return handler( service );
        }

        private byte [ ] GetReading( ) => Get( x =>
        {
            x.WriteBytes( new byte [ ] { 0xac, 0x33, 0x00 } );
            Task.Delay( 100 );
            return x.ReadBytes( 6 ).Value;
        } );
    }
}