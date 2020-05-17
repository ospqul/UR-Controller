using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using URScritpsLibrary;
using Xunit;

namespace URScriptsLibrary.Tests
{
    public class URScriptTests
    {
        [Theory]
        [InlineData("Script")]
        [InlineData("Test")]
        public void FunctionHeader_ReturnFunctionNameDeclaration(string name)
        {
            var expected = $"def { name }():\n";

            var actual = URScript.FunctionHeader(name);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FunctionHeader_EmptyNameStringShouldReturnUnnamed()
        {
            string name = "";
            
            var expected = $"def unnamed():\n";

            var actual = URScript.FunctionHeader(name);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FunctionEnd_ShouldReturnEndWithoutIndent()
        {
            var expected = $"end\n";

            var actual = URScript.FunctionEnd();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1,2,3,4,5,6,7,8)]
        public void MoveL_ShouldReturnCorrectCommand(double x, double y, double z, double rx, double ry, double rz, double a, double v)
        {
            var expected = $"movel(p[{ x }, { y }, { z }, { rx }, { ry }, { rz }], a={ a }, v={ v })\n";

            URPose pose = new URPose(new Point3D(x, y, z), new Point3D(rx, ry, rz));

            var actual = URScript.MoveL(pose, a, v);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(4)]
        public void ForceMode_ShouleHaveCorrectIndent(uint level)
        {
            URPose start = new URPose(new Point3D(0, 1, 2), new Point3D(0, 0, 0));
            URPose end = new URPose(new Point3D(1, 2, 3), new Point3D(0, 0, 0));
            URMovement movement = new URMovement(start, end);

            var cmds = URScript.ForceMode(level, movement).Split('\n');

            //skip last line since it is empty line
            for (int i=0; i<cmds.Length-1; i++)
            {
                Assert.StartsWith(URScript.Indent(level), cmds[i]);
                Assert.NotEqual(' ', cmds[i][(int)level * 2]);
            }
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(4)]
        public void Indent_IndentStringLengthShouldBeCorrect(uint level)
        {
            var indent = URScript.Indent(level);

            var expected = (int)level * 2;

            var actual = indent.Length;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(4)]
        public void Indent_IndentStringShouldOnlyIncludeSpaceChar(uint level)
        {
            var indent = URScript.Indent(level);

            for (int i = 0; i < indent.Length; i++)
            {
                Assert.Equal(' ', indent[i]);
            }
        }

        [Fact]
        public void Generate_ShouldGenerateCorrectScript()
        {
            string name = "test";
            URPose pose1 = new URPose(new Point3D(-1, 1, 0), new Point3D(0, 0, 0));
            URPose pose2 = new URPose(new Point3D(1, 1, 0), new Point3D(0, 0, 0));
            URPose pose3 = new URPose(new Point3D(1, -1, 0), new Point3D(0, 0, 0));
            URPose pose4 = new URPose(new Point3D(-1, -1, 0), new Point3D(0, 0, 0));
            List<IURMovement> moves = new List<IURMovement>();
            moves.Add(new URMovement(pose1, pose2));
            moves.Add(new URMovement(pose3, pose4));


            URVector safe = new URVector(new Vector3D(0, 0, 1), new Vector3D(0, 0, 0));

            var actual = URScript.Generate(name, moves, safe);

            var expected =
                "def test():\n" +
                "  movel(p[-1, 1, 1, 0, 0, 0], a=0.5, v=0.3)\n" +
                "  force_mode(p[0.0,0.0,0.0,0.0,0.0,0.0], [0, 0, 1, 0, 0, 0], [0.0, 0.0, -20.0, 0.0, 0.0, 0.0], 2, [0.1, 0.1, 0.15, 0.3490658503988659, 0.3490658503988659, 0.3490658503988659])\n" +
                "  movel(p[-1, 1, 0, 0, 0, 0], a=0.1, v=0.02)\n" +
                "  movel(p[1, 1, 0, 0, 0, 0], a=0.1, v=0.02)\n" +
                "  end_force_mode()\n" +
                "  movel(p[1, 1, 1, 0, 0, 0], a=0.1, v=0.1)\n" +
                "  movel(p[1, -1, 1, 0, 0, 0], a=0.5, v=0.3)\n" +
                "  force_mode(p[0.0,0.0,0.0,0.0,0.0,0.0], [0, 0, 1, 0, 0, 0], [0.0, 0.0, -20.0, 0.0, 0.0, 0.0], 2, [0.1, 0.1, 0.15, 0.3490658503988659, 0.3490658503988659, 0.3490658503988659])\n" +
                "  movel(p[1, -1, 0, 0, 0, 0], a=0.1, v=0.02)\n" +
                "  movel(p[-1, -1, 0, 0, 0, 0], a=0.1, v=0.02)\n" +
                "  end_force_mode()\n" +
                "  movel(p[-1, -1, 1, 0, 0, 0], a=0.1, v=0.1)\n" +
                "end\n";

            Assert.Equal(expected, actual);
        }


    }
}
