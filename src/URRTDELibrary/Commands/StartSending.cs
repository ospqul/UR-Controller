using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URRTDELibrary
{
    public class StartSending
    {
        public byte Accepted { get; set; }

        public StartSending(IURRTDE urRTDE)
        {
            byte[] payload = new byte[0];
            var resp = urRTDE.SendReceive(IPackageType.RTDE_CONTROL_PACKAGE_START, payload);
            Accepted = resp[0];
        }
    }
}
