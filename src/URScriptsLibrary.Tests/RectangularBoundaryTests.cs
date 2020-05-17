using System.Windows.Media.Media3D;
using URScritpsLibrary;
using Xunit;

namespace URScriptsLibrary.Tests
{
    public class RectangularBoundaryTests
    {
        public URPose lt { get; set; }
        public URPose rt { get; set; }
        public URPose lb { get; set; }
        public URPose rb { get; set; }

        public Point3D rotation = new Point3D(-0.5, 0, 1);

        public const int precision = 4;

        public RectangularBoundaryTests()
        {
            lt = new URPose(new Point3D(-1, +1, 0), rotation);
            rt = new URPose(new Point3D(+1, +1, 0), rotation);
            lb = new URPose(new Point3D(-1, -1, 0), rotation);
            rb = new URPose(new Point3D(+1, -1, 0), rotation);
        }

        [Fact]
        public void CtorSetWithFourCornerPositions_SetCornerPositionsCorretly()
        {
            var boundary = new RectangularBoundary(lt, rt, rb, lb);

            Assert.True(Vector3D.Equals(boundary.LeftTop.Position, lt.Position));
            Assert.True(Vector3D.Equals(boundary.LeftTop.Rotation, lt.Rotation));

            Assert.True(Vector3D.Equals(boundary.RightTop.Position, rt.Position));
            Assert.True(Vector3D.Equals(boundary.RightTop.Rotation, rt.Rotation));

            Assert.True(Vector3D.Equals(boundary.RightBottom.Position, rb.Position));
            Assert.True(Vector3D.Equals(boundary.RightBottom.Rotation, rb.Rotation));

            Assert.True(Vector3D.Equals(boundary.LeftBottom.Position, lb.Position));
            Assert.True(Vector3D.Equals(boundary.LeftBottom.Rotation, lb.Rotation));
        }

        [Fact]
        public void CtorSetWithFourCornerPositions_SetScanMovementCorretly()
        {
            var boundary = new RectangularBoundary(lt, rt, rb, lb);

            var actual = lt + boundary.ScanMovement;

            var expected = rt;

            Assert.True(Vector3D.Equals(expected.Position, actual.Position));
            Assert.True(Vector3D.Equals(expected.Rotation, actual.Rotation));
        }

        [Fact]
        public void CtorSetWithFourCornerPositions_SetIndexMovementCorretly()
        {
            var boundary = new RectangularBoundary(lt, rt, rb, lb);

            var actual = lt + boundary.IndexMovement;

            var expected = lb;

            Assert.True(Vector3D.Equals(expected.Position, actual.Position));
            Assert.True(Vector3D.Equals(expected.Rotation, actual.Rotation));
        }
        
        [Fact]
        public void CtorSetWithScanMovement_SetCornerPositionsCorretly()
        {
            var scanMove = new URMovement(lt, rt);
            var point = new Point3D(-0.5, -1, 0);
            var pose = new URPose(point, rotation);

            var boundary = new RectangularBoundary(scanMove, pose);

            Assert.True(Vector3D.Equals(boundary.LeftTop.Position, lt.Position));
            Assert.True(Vector3D.Equals(boundary.LeftTop.Rotation, lt.Rotation));

            Assert.True(Vector3D.Equals(boundary.RightTop.Position, rt.Position));
            Assert.True(Vector3D.Equals(boundary.RightTop.Rotation, rt.Rotation));

            Assert.Equal(rb.Position.X, boundary.RightBottom.Position.X, precision);
            Assert.Equal(rb.Position.Y, boundary.RightBottom.Position.Y, precision);
            Assert.Equal(rb.Position.Z, boundary.RightBottom.Position.Z, precision);
            Assert.True(Vector3D.Equals(boundary.RightBottom.Rotation, rb.Rotation));

            Assert.Equal(lb.Position.X, boundary.LeftBottom.Position.X, precision);
            Assert.Equal(lb.Position.Y, boundary.LeftBottom.Position.Y, precision);
            Assert.Equal(lb.Position.Z, boundary.LeftBottom.Position.Z, precision);
            Assert.True(Vector3D.Equals(boundary.LeftBottom.Rotation, lb.Rotation));
        }

        [Fact]
        public void CtorSetWithScanMovement_SetScanMovementCorretly()
        {
            var scanMove = new URMovement(lt, rt);
            var point = new Point3D(-0.5, -1, 0);
            var pose = new URPose(point, rotation);

            var boundary = new RectangularBoundary(scanMove, pose);

            var actual = lt + boundary.ScanMovement;

            var expected = rt;

            Assert.True(Vector3D.Equals(expected.Position, actual.Position));
            Assert.True(Vector3D.Equals(expected.Rotation, actual.Rotation));
        }

        [Fact]
        public void CtorSetWithScanMovement_SetIndexMovementCorretly()
        {
            var scanMove = new URMovement(lt, rt);
            var point = new Point3D(-0.5, -1, 0);
            var pose = new URPose(point, rotation);

            var boundary = new RectangularBoundary(scanMove, pose);

            var actual = lt + boundary.IndexMovement;

            var expected = lb;

            Assert.Equal(expected.Position.X, actual.Position.X, precision);
            Assert.Equal(expected.Position.Y, actual.Position.Y, precision);
            Assert.Equal(expected.Position.Z, actual.Position.Z, precision);
            Assert.True(Vector3D.Equals(expected.Rotation, actual.Rotation));
        }
    }
}
