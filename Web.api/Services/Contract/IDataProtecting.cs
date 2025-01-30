using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contract
{
    public interface IDataProtecting
    {
        public string ProtectData(int value);

        public string ProtectData(string value);

        public byte[] UnprotectData(string protectedValue);
    }
}
