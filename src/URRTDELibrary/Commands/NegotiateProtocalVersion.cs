using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URRTDELibrary
{
    public class NegotiateProtocolVersion
    {
        public byte Accepted { get; set; }

        public NegotiateProtocolVersion(IURRTDE urRTDE)
        {
            byte[] payload = IToBytes.FromUshort(2);
            var resp = urRTDE.SendReceive(IPackageType.RTDE_REQUEST_PROTOCOL_VERSION, payload);
            Accepted = resp[0];         
        }
    }
}
