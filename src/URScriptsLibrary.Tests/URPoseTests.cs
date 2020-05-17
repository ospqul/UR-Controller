using System.Windows.Media.Media3D;
using URScritpsLibrary;
using Xunit;

namespace URScriptsLibrary.Tests
{
    public class URPoseTests
    {
        #region Methods Tests

        [Theory]
        [InlineData(1, 2, 3, 10, 20, 30, 11, 22, 33)]
        [InlineData(1, 2, 3, -10, -20, -30, -9, -18, -27)]
        public void AddMethod_PoseShouldAdd(
            double x, double y, double z,    // initial position
            double dx, double dy, double dz, // vector
            double expectedX, double expectedY, double expectedZ)    // expected position
        {
            Point3D position = new Point3D(x, y, z);
            Point3D rotation = new Point3D(0, 0, 0);
            URPose pose = new URPose(position, rotation);

            Vector3D poseVector = new Vector3D(dx, dy, dz);
            Vector3D rotationVector = new Vector3D(0, 0, 0);
            URVector vector = new URVector(poseVector, rotationVector);

            var finalPose = URPose.Add(pose, vector);

            var actualX = finalPose.Position.X;
            var actualY = finalPose.Position.Y;
            var actualZ = finalPose.Position.Z;
            
            Assert.Equal(expectedX, actualX);
            Assert.Equal(expectedY, actualY);
            Assert.Equal(expectedZ, actualZ);
        }

        [Theory]
        [InlineData(1, 2, 3, 10, 20, 30, 11, 22, 33)]
        [InlineData(1, 2, 3, -10, -20, -30, -9, -18, -27)]
        public void AddMethod_RotationShouldAdd(
            double x, double y, double z,    // initial rotation
            double dx, double dy, double dz, // rotation vector
            double expectedX, double expectedY, double expectedZ)    // expected rotation
        {
            Point3D position = new Point3D(0, 0, 0);
            Point3D rotation = new Point3D(x, y, z);
            URPose pose = new URPose(position, rotation);

            Vector3D poseVector = new Vector3D(0, 0, 0);
            Vector3D rotationVector = new Vector3D(dx, dy, dz);
            URVector vector = new URVector(poseVector, rotationVector);

            var finalPose = URPose.Add(pose, vector);

            var actualX = finalPose.Rotation.X;
            var actualY = finalPose.Rotation.Y;
            var actualZ = finalPose.Rotation.Z;

            Assert.Equal(expectedX, actualX);
            Assert.Equal(expectedY, actualY);
            Assert.Equal(expectedZ, actualZ);
        }

        [Theory]
        [InlineData(1, 2, 3, 10, 20, 30, -9, -18, -27)]
        [InlineData(1, 2, 3, -10, -20, -30, 11, 22, 33)]
        public void SubtractMethodPosePose_PoseShouldSubtract(
            double x1, double y1, double z1, // first position
            double x2, double y2, double z2, // second position
            double expectedX, double expectedY, double expectedZ)    // expected vector
        {
            Point3D position1 = new Point3D(x1, y1, z1);
            Point3D rotation1 = new Point3D(0, 0, 0);
            URPose pose1 = new URPose(position1, rotation1);

            Point3D position2 = new Point3D(x2, y2, z2);
            Point3D rotation2 = new Point3D(0, 0, 0);
            URPose pose2 = new URPose(position2, rotation2);

            var finalVector = URPose.Subtract(pose1, pose2);

            var actualX = finalVector.PoseVector.X;
            var actualY = finalVector.PoseVector.Y;
            var actualZ = finalVector.PoseVector.Z;

            Assert.Equal(expectedX, actualX);
            Assert.Equal(expectedY, actualY);
            Assert.Equal(expectedZ, actualZ);
        }

        [Theory]
        [InlineData(1, 2, 3, 10, 20, 30, -9, -18, -27)]
        [InlineData(1, 2, 3, -10, -20, -30, 11, 22, 33)]
        public void SubtractMethodPosePose_RotationShouldSubtract(
            double x1, double y1, double z1, // first rotation
            double x2, double y2, double z2, // second rotation
            double expectedX, double expectedY, double expectedZ)    // expected vector
        {
            Point3D position1 = new Point3D(0, 0, 0);
            Point3D rotation1 = new Point3D(x1, y1, z1);
            URPose pose1 = new URPose(position1, rotation1);

            Point3D position2 = new Point3D(0, 0, 0);
            Point3D rotation2 = new Point3D(x2, y2, z2);
            URPose pose2 = new URPose(position2, rotation2);

            var finalVector = URPose.Subtract(pose1, pose2);

            var actualX = finalVector.RotationVector.X;
            var actualY = finalVector.RotationVector.Y;
            var actualZ = finalVector.RotationVector.Z;

            Assert.Equal(expectedX, actualX);
            Assert.Equal(expectedY, actualY);
            Assert.Equal(expectedZ, actualZ);
        }

        [Theory]
        [InlineData(1, 2, 3, 10, 20, 30, -9, -18, -27)]
        [InlineData(1, 2, 3, -10, -20, -30, 11, 22, 33)]
        public void SubtractMethodPoseVector_PoseShouldSubtract(
            double x, double y, double z,    // initial position
            double dx, double dy, double dz, // vector
            double expectedX, double expectedY, double expectedZ)    // expected position
        {
            Point3D position = new Point3D(x, y, z);
            Point3D rotation = new Point3D(0, 0, 0);
            URPose pose = new URPose(position, rotation);

            Vector3D poseVector = new Vector3D(dx, dy, dz);
            Vector3D rotationVector = new Vector3D(0, 0, 0);
            URVector vector = new URVector(poseVector, rotationVector);

            var finalPose = URPose.Subtract(pose, vector);

            var actualX = finalPose.Position.X;
            var actualY = finalPose.Position.Y;
            var actualZ = finalPose.Position.Z;

            Assert.Equal(expectedX, actualX);
            Assert.Equal(expectedY, actualY);
            Assert.Equal(expectedZ, actualZ);
        }

        [Theory]
        [InlineData(1, 2, 3, 10, 20, 30, -9, -18, -27)]
        [InlineData(1, 2, 3, -10, -20, -30, 11, 22, 33)]
        public void SubtractMethodPoseVector_RotationShouldSubtract(
            double x, double y, double z,    // initial rotation
            double dx, double dy, double dz, // ratation vector
            double expectedX, double expectedY, double expectedZ)    // expected position
        {
            Point3D position = new Point3D(0, 0, 0);
            Point3D rotation = new Point3D(x, y, z);
            URPose pose = new URPose(position, rotation);

            Vector3D poseVector = new Vector3D(0, 0, 0);
            Vector3D rotationVector = new Vector3D(dx, dy, dz);
            URVector vector = new URVector(poseVector, rotationVector);

            var finalPose = URPose.Subtract(pose, vector);

            var actualX = finalPose.Rotation.X;
            var actualY = finalPose.Rotation.Y;
            var actualZ = finalPose.Rotation.Z;

            Assert.Equal(expectedX, actualX);
            Assert.Equal(expectedY, actualY);
            Assert.Equal(expectedZ, actualZ);
        }

        #endregion

        #region Operators Tests

        [Theory]
        [InlineData(1, 2, 3, 10, 20, 30, 11, 22, 33)]
        [InlineData(1, 2, 3, -10, -20, -30, -9, -18, -27)]
        public void AddittionOperator_PoseShouldAdd(
            double x, double y, double z,    // initial position
            double dx, double dy, double dz, // vector
            double expectedX, double expectedY, double expectedZ)    // expected position
        {
            Point3D position = new Point3D(x, y, z);
            Point3D rotation = new Point3D(0, 0, 0);
            URPose pose = new URPose(position, rotation);

            Vector3D poseVector = new Vector3D(dx, dy, dz);
            Vector3D rotationVector = new Vector3D(0, 0, 0);
            URVector vector = new URVector(poseVector, rotationVector);

            var finalPose = pose + vector;

            var actualX = finalPose.Position.X;
            var actualY = finalPose.Position.Y;
            var actualZ = finalPose.Position.Z;

            Assert.Equal(expectedX, actualX);
            Assert.Equal(expectedY, actualY);
            Assert.Equal(expectedZ, actualZ);
        }

        [Theory]
        [InlineData(1, 2, 3, 10, 20, 30, 11, 22, 33)]
        [InlineData(1, 2, 3, -10, -20, -30, -9, -18, -27)]
        public void AddittionOperator_RotationShouldAdd(
            double x, double y, double z,    // initial rotation
            double dx, double dy, double dz, // rotation vector
            double expectedX, double expectedY, double expectedZ)    // expected rotation
        {
            Point3D position = new Point3D(0, 0, 0);
            Point3D rotation = new Point3D(x, y, z);
            URPose pose = new URPose(position, rotation);

            Vector3D poseVector = new Vector3D(0, 0, 0);
            Vector3D rotationVector = new Vector3D(dx, dy, dz);
            URVector vector = new URVector(poseVector, rotationVector);

            var finalPose = pose + vector;

            var actualX = finalPose.Rotation.X;
            var actualY = finalPose.Rotation.Y;
            var actualZ = finalPose.Rotation.Z;

            Assert.Equal(expectedX, actualX);
            Assert.Equal(expectedY, actualY);
            Assert.Equal(expectedZ, actualZ);
        }

        [Theory]
        [InlineData(1, 2, 3, 10, 20, 30, -9, -18, -27)]
        [InlineData(1, 2, 3, -10, -20, -30, 11, 22, 33)]
        public void SubtractionOperatorPosePose_PoseShouldSubtract(
            double x1, double y1, double z1, // first position
            double x2, double y2, double z2, // second position
            double expectedX, double expectedY, double expectedZ)    // expected vector
        {
            Point3D position1 = new Point3D(x1, y1, z1);
            Point3D rotation1 = new Point3D(0, 0, 0);
            URPose pose1 = new URPose(position1, rotation1);

            Point3D position2 = new Point3D(x2, y2, z2);
            Point3D rotation2 = new Point3D(0, 0, 0);
            URPose pose2 = new URPose(position2, rotation2);

            var finalVector = pose1 - pose2;

            var actualX = finalVector.PoseVector.X;
            var actualY = finalVector.PoseVector.Y;
            var actualZ = finalVector.PoseVector.Z;

            Assert.Equal(expectedX, actualX);
            Assert.Equal(expectedY, actualY);
            Assert.Equal(expectedZ, actualZ);
        }

        [Theory]
        [InlineData(1, 2, 3, 10, 20, 30, -9, -18, -27)]
        [InlineData(1, 2, 3, -10, -20, -30, 11, 22, 33)]
        public void SubtractionOperatorPosePose_RotationShouldSubtract(
            double x1, double y1, double z1, // first rotation
            double x2, double y2, double z2, // second rotation
            double expectedX, double expectedY, double expectedZ)    // expected vector
        {
            Point3D position1 = new Point3D(0, 0, 0);
            Point3D rotation1 = new Point3D(x1, y1, z1);
            URPose pose1 = new URPose(position1, rotation1);

            Point3D position2 = new Point3D(0, 0, 0);
            Point3D rotation2 = new Point3D(x2, y2, z2);
            URPose pose2 = new URPose(position2, rotation2);

            var finalVector = pose1 - pose2;

            var actualX = finalVector.RotationVector.X;
            var actualY = finalVector.RotationVector.Y;
            var actualZ = finalVector.RotationVector.Z;

            Assert.Equal(expectedX, actualX);
            Assert.Equal(expectedY, actualY);
            Assert.Equal(expectedZ, actualZ);
        }

        [Theory]
        [InlineData(1, 2, 3, 10, 20, 30, -9, -18, -27)]
        [InlineData(1, 2, 3, -10, -20, -30, 11, 22, 33)]
        public void SubtractionOperatorPoseVector_PoseShouldSubtract(
            double x, double y, double z,    // initial position
            double dx, double dy, double dz, // vector
            double expectedX, double expectedY, double expectedZ)    // expected position
        {
            Point3D position = new Point3D(x, y, z);
            Point3D rotation = new Point3D(0, 0, 0);
            URPose pose = new URPose(position, rotation);

            Vector3D poseVector = new Vector3D(dx, dy, dz);
            Vector3D rotationVector = new Vector3D(0, 0, 0);
            URVector vector = new URVector(poseVector, rotationVector);

            var finalPose = pose - vector;

            var actualX = finalPose.Position.X;
            var actualY = finalPose.Position.Y;
            var actualZ = finalPose.Position.Z;

            Assert.Equal(expectedX, actualX);
            Assert.Equal(expectedY, actualY);
            Assert.Equal(expectedZ, actualZ);
        }

        [Theory]
        [InlineData(1, 2, 3, 10, 20, 30, -9, -18, -27)]
        [InlineData(1, 2, 3, -10, -20, -30, 11, 22, 33)]
        public void SubtractionOperatorPoseVector_RotationShouldSubtract(
            double x, double y, double z,    // initial rotation
            double dx, double dy, double dz, // ratation vector
            double expectedX, double expectedY, double expectedZ)    // expected position
        {
            Point3D position = new Point3D(0, 0, 0);
            Point3D rotation = new Point3D(x, y, z);
            URPose pose = new URPose(position, rotation);

            Vector3D poseVector = new Vector3D(0, 0, 0);
            Vector3D rotationVector = new Vector3D(dx, dy, dz);
            URVector vector = new URVector(poseVector, rotationVector);

            var finalPose = pose - vector;

            var actualX = finalPose.Rotation.X;
            var actualY = finalPose.Rotation.Y;
            var actualZ = finalPose.Rotation.Z;

            Assert.Equal(expectedX, actualX);
            Assert.Equal(expectedY, actualY);
            Assert.Equal(expectedZ, actualZ);
        }

        #endregion
    }
}
