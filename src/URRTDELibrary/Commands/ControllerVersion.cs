using System;

namespace URRTDELibrary
{
    public class ControllerVersion
    {
        public uint Major { get; set; }
        public uint Minor { get; set; }
        public uint Bugfix { get; set; }
        public uint Build { get; set; }

        public ControllerVersion(IURRTDE urRTDE)
        {            
            byte[] payload = new byte[0];
            var resp = urRTDE.SendReceive(IPackageType.RTDE_GET_URCONTROL_VERSION, payload);
            byte[] temp = new byte[4];
            Buffer.BlockCopy(resp, 0, temp, 0, 4);
            Major = IFromBytes.ToUInt32(temp);
            Buffer.BlockCopy(resp, 4, temp, 0, 4);
            Minor = IFromBytes.ToUInt32(temp);
            Buffer.BlockCopy(resp, 8, temp, 0, 4);
            Bugfix = IFromBytes.ToUInt32(temp);
            Buffer.BlockCopy(resp, 12, temp, 0, 4);
            Build = IFromBytes.ToUInt32(temp);         
        }
    }
}
