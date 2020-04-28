using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URRTDELibrary
{
    public class SetupOutput
    {
        public byte OutputRecipeId { get; set; }
        public string VariableTypes { get; set; }

        public SetupOutput(IURRTDE urRTDE, double frequency, string variables)
        {
            var bFreq = IToBytes.FromDouble(frequency);
            var bVar = IToBytes.FromString(variables);
            byte[] payload = new byte[bFreq.Length + bVar.Length];
            Buffer.BlockCopy(bFreq, 0, payload, 0, bFreq.Length);
            Buffer.BlockCopy(bVar, 0, payload, bFreq.Length, bVar.Length);

            var resp = urRTDE.SendReceive(IPackageType.RTDE_CONTROL_PACKAGE_SETUP_OUTPUTS, payload);

            OutputRecipeId = resp[0];

            byte[] temp = new byte[resp.Length - 1];
            Buffer.BlockCopy(resp, 1, temp, 0, temp.Length);
            VariableTypes = IFromBytes.ToString(temp);
        }
    }
}
