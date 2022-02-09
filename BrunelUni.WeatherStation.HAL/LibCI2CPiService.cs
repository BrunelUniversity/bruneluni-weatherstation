using System.Linq;
using Aidan.Common.Core;
using Aidan.Common.Core.Enum;
using Aidan.Common.Core.Interfaces.Contract;
using BrunelUni.WeatherStation.Core.Interfaces.Contract;

namespace BrunelUni.WeatherStation.HAL
{
    public class LibCI2CPiService : I2CPiService
    {
        private readonly ILibcAdapter _libcAdapter;
        private readonly ILoggerAdapter<I2CPiService> _loggerAdapter;
        private readonly int _handle;

        public LibCI2CPiService( int address, ILibcAdapter libcAdapter, ILoggerAdapter<I2CPiService> loggerAdapter )
        {
            _libcAdapter = libcAdapter;
            _loggerAdapter = loggerAdapter;
            var file = "/dev/i2c-1";
            _handle = _libcAdapter.Open( file, 2 );
            _loggerAdapter.LogDebug( $"opened i2c bus {file} to read" );
            _libcAdapter.Ioctl( _handle, 1795, address );
            _loggerAdapter.LogDebug( $"using i2c at 0x{address:X} address" );
        }

        public Result WriteBytes( byte [ ] data )
        {
            _libcAdapter.Write( _handle, data, data.Length );
            _loggerAdapter.LogDebug( $"writing {data.Aggregate( "", ( current, b ) => current + $"0x{b:X} " )}" );
            return Result.Success( );
        }

        public ObjectResult<byte [ ]> ReadBytes( int length )
        {
            var array = new byte[ length ];
            _libcAdapter.Read( _handle, array, length );
            _loggerAdapter.LogDebug( $"read {array.Aggregate( "", ( current, b ) => current + $"0x{b:X} " )}" );
            return new ObjectResult<byte [ ]>
            {
                Status = OperationResultEnum.Success,
                Value = array
            };
        }
    }
}