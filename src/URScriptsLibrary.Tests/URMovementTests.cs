using System.Windows.Media.Media3D;
using URScritpsLibrary;
using Xunit;

namespace URScriptsLibrary.Tests
{
    public class URMovementTests
    {
        public URPose pose1 { get; set; }
        public URPose pose2 { get; set; }

        public URMovementTests()
        {
            Point3D position1 = new Point3D(1, 2, 3);
            Point3D rotation1 = new Point3D(4, 5, 6);
            pose1 = new URPose(position1, rotation1);

            Point3D position2 = new Point3D(10, 20, 30);
            Point3D rotation2 = new Point3D(-4, -5, -6);
            pose2 = new URPose(position2, rotation2);
        }

        [Fact]
        public void Ctor_MustSetStartEndCorrectly()
        {
            URMovement movement = new URMovement(pose1, pose2);

            Assert.True(Vector3D.Equals(pose1.Position, movement.Start.Position));
            Assert.True(Vector3D.Equals(pose1.Rotation, movement.Start.Rotation));
            Assert.True(Vector3D.Equals(pose2.Position, movement.End.Position));
            Assert.True(Vector3D.Equals(pose2.Rotation, movement.End.Rotation));
        }

        [Fact]
        public void Ctor_MustSetMovementCorrectly()
        {
            var expected = pose2;

            URMovement movement = new URMovement(pose1, expected);
            
            URPose actual = pose1 + movement.Movement;

            Assert.True(Vector3D.Equals(actual.Position, expected.Position));
            Assert.True(Vector3D.Equals(actual.Rotation, expected.Rotation));
        }
    }
}
