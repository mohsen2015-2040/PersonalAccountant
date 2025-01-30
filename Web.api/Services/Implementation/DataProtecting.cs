using Microsoft.AspNetCore.DataProtection;
using Services.Contract;
using System.Runtime.InteropServices;
using System.Text;

namespace Services.Implementation
{
    public class DataProtecting(IDataProtectionProvider provider) : IDataProtecting
    {
        private readonly IDataProtector _protector = provider.CreateProtector("Services.DataProtectionI");
        
        public string ProtectData(int rawValue)
        {
            var valueBytes = BitConverter.GetBytes(rawValue);

            return _protector.Protect(Encoding.UTF8.GetString(valueBytes));
        }

        public string ProtectData(string rawValue)
        {
            var valueBytes = Encoding.ASCII.GetBytes(rawValue);

            return _protector.Protect(Encoding.UTF8.GetString(valueBytes));
        }

        public byte[] UnprotectData(string protectedValue)
        {
            var unprotected = _protector.Unprotect(protectedValue);
            var unprotectedBytes = Encoding.UTF8.GetBytes(unprotected);

            return unprotectedBytes;
        }

    }
}
