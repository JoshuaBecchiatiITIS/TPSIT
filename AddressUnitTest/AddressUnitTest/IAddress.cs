using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressUnitTest
{
    internal interface IAddress
    {
        string generateIPv4();
        string generateSubnet();
    }
}
