using System.Collections.Generic;
using System.Text;

namespace URScritpsLibrary
{
    public static class URScript
    {
        public static string Generate(string name, List<IURMovement> movements, URVector safeDistVector)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(FunctionHeader(name));

            // Start scan
            for (int i = 0; i < movements.Count; i++)
            {
                // movej to safe position above start point
                sb.Append(Indent(1) + MoveL(movements[i].Start + safeDistVector, 0.5, 0.3));
                // force mode
                sb.Append(ForceMode(1, movements[i]));
                // move to safe position after one line scan
                sb.Append(Indent(1) + MoveL(movements[i].End + safeDistVector, 0.1, 0.1));
            }

            sb.Append(FunctionEnd());
            return sb.ToString();
        }
        
        public static string FunctionHeader(string functionName)
        {
            if (functionName == "")
            {
                return $"def unnamed():\n";
            }
            else
            {
                return $"def { functionName }():\n";
            }
        }

        public static string FunctionEnd()
        {
            return $"end\n";
        }

        public static string MoveL(URPose p, double a = 0.1, double v = 0.02)
        {
            return $"movel(p[{ p.Position.X }, { p.Position.Y }, { p.Position.Z }, { p.Rotation.X }, { p.Rotation.Y }, { p.Rotation.Z }], a={ a }, v={ v })\n";
        }

        public static string ForceMode(uint indentLevel, IURMovement movement)
        {
            string cmd = "";            
            cmd += Indent(indentLevel) + "force_mode(p[0.0,0.0,0.0,0.0,0.0,0.0], [0, 0, 1, 0, 0, 0], [0.0, 0.0, -20.0, 0.0, 0.0, 0.0], 2, [0.1, 0.1, 0.15, 0.3490658503988659, 0.3490658503988659, 0.3490658503988659])\n";            
            cmd += Indent(indentLevel) + MoveL(movement.Start);            
            cmd += Indent(indentLevel) + MoveL(movement.End);            
            cmd += Indent(indentLevel) + "end_force_mode()\n";
            return cmd;
        }

        public static string Indent(uint level)
        {
            if (level == 0)
            {
                return "";
            }
            else
            {
                string indent = "";
                for (int i = 0; i < level; i++)
                {
                    indent += "  ";
                }
                return indent;
            }
        }

    }
}
