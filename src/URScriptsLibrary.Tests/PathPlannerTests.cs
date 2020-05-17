using System;
using System.Windows.Media.Media3D;
using URScritpsLibrary;
using Xunit;

namespace URScriptsLibrary.Tests
{
    public class PathPlannerTests
    {
        public RectangularBoundary Boundary { get; set; }
        public double BrushThickness { get; set; }
        public double Overlay { get; set; }

        public PathPlannerTests()
        {
            Point3D rotation = new Point3D(-0.5, 0, 1);
            var lt = new URPose(new Point3D(-1, +1, 0), rotation);
            var rt = new URPose(new Point3D(+1, +1, 0), rotation);
            var lb = new URPose(new Point3D(-1, -1, 0), rotation);
            var rb = new URPose(new Point3D(+1, -1, 0), rotation);
            Boundary = new RectangularBoundary(lt, rt, rb, lb);

            BrushThickness = 0.5;

            Overlay = 0.2;
        }

        [Theory]
        [InlineData(-0.1, 0)]
        [InlineData(1, 0)]
        public void SStyle_WrongOverlayRateHasNoMovement(double overlay, int expected)
        {
            var moves = PathPlanner.SStyle(Boundary, BrushThickness, overlay);

            var actual = moves.Count;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(-0.1, 0)]
        [InlineData(0, 0)]
        [InlineData(5, 1)]
        public void SStyle_WrongBrushThicknessHasNoMovement(double thickness, int expected)
        {
            var moves = PathPlanner.SStyle(Boundary, thickness, Overlay);

            var actual = moves.Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SStyle_ShouldHaveEnoughMoves()
        {
            var moves = PathPlanner.SStyle(Boundary, BrushThickness, Overlay);

            var indexOffset = BrushThickness * (1 - Overlay);

            int expected = (int)Math.Ceiling(Boundary.IndexMovement.PoseVector.Length / indexOffset);

            var actual = moves.Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SStyle_ShouldHaveCorrectScanMoves()
        {
            var moves = PathPlanner.SStyle(Boundary, BrushThickness, Overlay);

            var expected = Boundary.ScanMovement.PoseVector;

            for (int i = 0; i < moves.Count; i++)
            {
                var actual = moves[i].Movement.PoseVector;
                if (i % 2 == 1)
                {
                    actual.Negate();
                }
                Assert.True(Vector3D.Equals(expected, actual));
                Assert.True(Vector3D.Equals(new Vector3D(0, 0, 0), moves[i].Movement.RotationVector));
            }
        }

        [Fact]
        public void SStyle_IndexMovesShouldHaveCorrectDirection()
        {
            var moves = PathPlanner.SStyle(Boundary, BrushThickness, Overlay);

            double expected = 0;

            for (int i = 0; i < moves.Count; i++)
            {
                if (i < moves.Count - 1)
                {
                    var indexVector = (moves[i + 1].Start - moves[i].End).PoseVector;
                    var actual = Vector3D.AngleBetween(indexVector, Boundary.IndexMovement.PoseVector);
                    Assert.Equal(expected, actual, 4);
                }
            }
        }

        [Fact]
        public void SStyle_IndexMovesShouldHaveCorrectLength()
        {
            var moves = PathPlanner.SStyle(Boundary, BrushThickness, Overlay);

            double expected = BrushThickness * ( 1 - Overlay );

            for (int i = 0; i < moves.Count; i++)
            {
                if (i < moves.Count - 1)
                {
                    var indexVector = (moves[i + 1].Start - moves[i].End).PoseVector;
                    var actual = indexVector.Length;
                    Assert.Equal(expected, actual, 4);
                }
            }
        }
    }
}
