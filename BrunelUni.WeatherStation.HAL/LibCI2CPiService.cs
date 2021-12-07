using Aidan.Common.Core;
using Aidan.Common.Core.Enum;
using BrunelUni.WeatherStation.Core.Interfaces.Contract;

namespace BrunelUni.WeatherStation.HAL
{
    public class LibCI2CPiService : I2CPiService
    {
        private readonly ILibcAdapter _libcAdapter;
        private readonly int _handle;

        public LibCI2CPiService( int address, ILibcAdapter libcAdapter )
        {
            _libcAdapter = libcAdapter;
            _handle = _libcAdapter.Open( "/dev/i2c-1", 2 );
            _libcAdapter.Ioctl( _handle, 1795, address );
        }

        public Result WriteBytes( byte [ ] data )
        {
            _libcAdapter.Write( _handle, data, data.Length );
            return Result.Success( );
        }

        public ObjectResult<byte [ ]> ReadBytes( int length )
        {
            var array = new byte[ length ];
            _libcAdapter.Read( _handle, array, length );
            return new ObjectResult<byte [ ]>
            {
                Status = OperationResultEnum.Success,
                Value = array
            };
        }
    }
}