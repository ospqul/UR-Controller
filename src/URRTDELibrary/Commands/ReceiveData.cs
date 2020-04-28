using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URRTDELibrary
{
    public class ReceiveData
    {
        public class RecieveData
        {
            private IURRTDE _urRTDE { get; set; }
            private string[] _variableTypes { get; set; }

            public ITCPPosition ActualTCPPose { get; set; }
            public byte RecipeId { get; set; }

            public RecieveData(IURRTDE urRTDE, string variableTypes)
            {
                _urRTDE = urRTDE;
                _variableTypes = variableTypes.Split(',');
            }

            public void Receive()
            {
                var resp = _urRTDE.Receive(IPackageType.RTDE_DATA_PACKAGE);
                int offset = 0;
                RecipeId = resp[0];
                offset += 1;
                for (int i = 0; i < _variableTypes.Length; i++)
                {
                    switch (_variableTypes[i])
                    {
                        case "INT32":
                            byte[] tempInt32 = new byte[4];
                            Buffer.BlockCopy(resp, offset, tempInt32, 0, 4);
                            offset += 4;
                            break;

                        case "UINT32":
                            byte[] tempUInt32 = new byte[4];
                            Buffer.BlockCopy(resp, offset, tempUInt32, 0, 4);
                            offset += 4;
                            break;

                        case "VECTOR6D":
                            byte[] tempV6D = new byte[48];
                            Buffer.BlockCopy(resp, offset, tempV6D, 0, 48);
                            var pose = IFromBytes.To6Double(tempV6D);
                            ActualTCPPose = new ITCPPosition
                            {
                                X = pose[0],
                                Y = pose[1],
                                Z = pose[2],
                                RX = pose[3],
                                RY = pose[4],
                                RZ = pose[5]
                            };
                            offset += 48;
                            break;

                        case "VECTOR3D":
                            break;

                        case "VECTOR6INT32":
                            break;

                        case "VECTOR6UINT32":
                            break;

                        case "DOUBLE":
                            byte[] tempDouble = new byte[8];
                            Buffer.BlockCopy(resp, offset, tempDouble, 0, 8);
                            offset += 8;
                            break;

                        case "UINT64":
                            break;

                        case "UINT8":
                            break;

                        case "BOOL":
                            break;
                    }
                }
            }
        }
    }
}
