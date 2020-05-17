using System.Windows.Media.Media3D;
using URScritpsLibrary;
using Xunit;

namespace URScriptsLibrary.Tests
{
    public class URVectorTests
    {
        #region Methods Tests

        [Theory]
        [InlineData(1, 2, 3, 10, 20, 30, 11, 22, 33)]
        [InlineData(1, 2, 3, -10, -20, -30, -9, -18, -27)]
        public void AddMethodVectorPose_PoseShouldAdd(
            double x, double y, double z,    // initial position
            double dx, double dy, double dz, // vector
            double expectedX, double expectedY, double expectedZ)    // expected position
        {
            Vector3D poseVector = new Vector3D(dx, dy, dz);
            Vector3D rotationVector = new Vector3D(0, 0, 0);
            URVector vector = new URVector(poseVector, rotationVector);

            Point3D position = new Point3D(x, y, z);
            Point3D rotation = new Point3D(0, 0, 0);
            URPose pose = new URPose(position, rotation);            

            var finalPose = URVector.Add(vector, pose);

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
        public void AddMethodVectorPose_RotationShouldAdd(
            double x, double y, double z,    // initial rotation
            double dx, double dy, double dz, // rotation vector
            double expectedX, double expectedY, double expectedZ)    // expected rotation
        {
            Vector3D poseVector = new Vector3D(0, 0, 0);
            Vector3D rotationVector = new Vector3D(dx, dy, dz);
            URVector vector = new URVector(poseVector, rotationVector);

            Point3D position = new Point3D(0, 0, 0);
            Point3D rotation = new Point3D(x, y, z);
            URPose pose = new URPose(position, rotation);            

            var finalPose = URVector.Add(vector, pose);

            var actualX = finalPose.Rotation.X;
            var actualY = finalPose.Rotation.Y;
            var actualZ = finalPose.Rotation.Z;

            Assert.Equal(expectedX, actualX);
            Assert.Equal(expectedY, actualY);
            Assert.Equal(expectedZ, actualZ);
        }

        [Theory]
        [InlineData(1, 2, 3, 10, 20, 30, 9, 18, 27)]
        [InlineData(1, 2, 3, -10, -20, -30, -11, -22, -33)]
        public void SubtractMethodVectorPose_PoseShouldSubtract(
            double x, double y, double z,    // initial position
            double dx, double dy, double dz, // vector
            double expectedX, double expectedY, double expectedZ)    // expected position
        {
            Vector3D poseVector = new Vector3D(dx, dy, dz);
            Vector3D rotationVector = new Vector3D(0, 0, 0);
            URVector vector = new URVector(poseVector, rotationVector);

            Point3D position = new Point3D(x, y, z);
            Point3D rotation = new Point3D(0, 0, 0);
            URPose pose = new URPose(position, rotation);

            var finalPose = URVector.Subtract(vector, pose);

            var actualX = finalPose.Position.X;
            var actualY = finalPose.Position.Y;
            var actualZ = finalPose.Position.Z;

            Assert.Equal(expectedX, actualX);
            Assert.Equal(expectedY, actualY);
            Assert.Equal(expectedZ, actualZ);
        }

        [Theory]
        [InlineData(1, 2, 3, 10, 20, 30, 9, 18, 27)]
        [InlineData(1, 2, 3, -10, -20, -30, -11, -22, -33)]
        public void SubtractMethodVectorPose_RotationShouldSubtract(
            double x, double y, double z,    // initial rotation
            double dx, double dy, double dz, // ratation vector
            double expectedX, double expectedY, double expectedZ)    // expected position
        {
            Vector3D poseVector = new Vector3D(0, 0, 0);
            Vector3D rotationVector = new Vector3D(dx, dy, dz);
            URVector vector = new URVector(poseVector, rotationVector);

            Point3D position = new Point3D(0, 0, 0);
            Point3D rotation = new Point3D(x, y, z);
            URPose pose = new URPose(position, rotation);

            var finalPose = URVector.Subtract(vector, pose);

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
        public void SubtractMethodVectorVector_PoseVectorShouldSubtract(
            double x1, double y1, double z1, // first position vector
            double x2, double y2, double z2, // second position vector
            double expectedX, double expectedY, double expectedZ)    // expected position vector
        {
            Vector3D poseVector1 = new Vector3D(x1, y1, z1);
            Vector3D rotationVector1 = new Vector3D(0, 0, 0);
            URVector vector1 = new URVector(poseVector1, rotationVector1);

            Vector3D poseVector2 = new Vector3D(x2, y2, z2);
            Vector3D rotationVector2 = new Vector3D(0, 0, 0);
            URVector vector2 = new URVector(poseVector2, rotationVector2);

            var finalVector = URVector.Subtract(vector1, vector2);

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
        public void SubtractMethodVectorVector_RotationVectorShouldSubtract(
            double x1, double y1, double z1, // first rotation vector
            double x2, double y2, double z2, // second rotation vector
            double expectedX, double expectedY, double expectedZ)    // expected rotation vector
        {
            Vector3D poseVector1 = new Vector3D(0, 0, 0);
            Vector3D rotationVector1 = new Vector3D(x1, y1, z1);
            URVector vector1 = new URVector(poseVector1, rotationVector1);

            Vector3D poseVector2 = new Vector3D(0, 0, 0);
            Vector3D rotationVector2 = new Vector3D(x2, y2, z2);
            URVector vector2 = new URVector(poseVector2, rotationVector2);

            var finalVector = URVector.Subtract(vector1, vector2);

            var actualX = finalVector.RotationVector.X;
            var actualY = finalVector.RotationVector.Y;
            var actualZ = finalVector.RotationVector.Z;

            Assert.Equal(expectedX, actualX);
            Assert.Equal(expectedY, actualY);
            Assert.Equal(expectedZ, actualZ);
        }

        #endregion

        #region Operators Tests

        [Theory]
        [InlineData(1, 2, 3, 10, 20, 30, 11, 22, 33)]
        [InlineData(1, 2, 3, -10, -20, -30, -9, -18, -27)]
        public void AddittionOperatorVectorPose_PoseShouldAdd(
            double x, double y, double z,    // initial position
            double dx, double dy, double dz, // vector
            double expectedX, double expectedY, double expectedZ)    // expected position
        {
            Vector3D poseVector = new Vector3D(dx, dy, dz);
            Vector3D rotationVector = new Vector3D(0, 0, 0);
            URVector vector = new URVector(poseVector, rotationVector);

            Point3D position = new Point3D(x, y, z);
            Point3D rotation = new Point3D(0, 0, 0);
            URPose pose = new URPose(position, rotation);

            var finalPose = vector + pose;

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
        public void AddittionOperatorVectorPose_RotationShouldAdd(
            double x, double y, double z,    // initial rotation
            double dx, double dy, double dz, // rotation vector
            double expectedX, double expectedY, double expectedZ)    // expected rotation
        {
            Vector3D poseVector = new Vector3D(0, 0, 0);
            Vector3D rotationVector = new Vector3D(dx, dy, dz);
            URVector vector = new URVector(poseVector, rotationVector);

            Point3D position = new Point3D(0, 0, 0);
            Point3D rotation = new Point3D(x, y, z);
            URPose pose = new URPose(position, rotation);

            var finalPose = vector + pose;

            var actualX = finalPose.Rotation.X;
            var actualY = finalPose.Rotation.Y;
            var actualZ = finalPose.Rotation.Z;

            Assert.Equal(expectedX, actualX);
            Assert.Equal(expectedY, actualY);
            Assert.Equal(expectedZ, actualZ);
        }

        [Theory]
        [InlineData(1, 2, 3, 10, 20, 30, 9, 18, 27)]
        [InlineData(1, 2, 3, -10, -20, -30, -11, -22, -33)]
        public void SubtractionOperatorVectorPose_PoseShouldSubtract(
            double x, double y, double z,    // initial position
            double dx, double dy, double dz, // vector
            double expectedX, double expectedY, double expectedZ)    // expected position
        {
            Vector3D poseVector = new Vector3D(dx, dy, dz);
            Vector3D rotationVector = new Vector3D(0, 0, 0);
            URVector vector = new URVector(poseVector, rotationVector);

            Point3D position = new Point3D(x, y, z);
            Point3D rotation = new Point3D(0, 0, 0);
            URPose pose = new URPose(position, rotation);

            var finalPose = vector - pose;

            var actualX = finalPose.Position.X;
            var actualY = finalPose.Position.Y;
            var actualZ = finalPose.Position.Z;

            Assert.Equal(expectedX, actualX);
            Assert.Equal(expectedY, actualY);
            Assert.Equal(expectedZ, actualZ);
        }

        [Theory]
        [InlineData(1, 2, 3, 10, 20, 30, 9, 18, 27)]
        [InlineData(1, 2, 3, -10, -20, -30, -11, -22, -33)]
        public void SubtractionOperatorVectorPose_RotationShouldSubtract(
            double x, double y, double z,    // initial rotation
            double dx, double dy, double dz, // ratation vector
            double expectedX, double expectedY, double expectedZ)    // expected position
        {
            Vector3D poseVector = new Vector3D(0, 0, 0);
            Vector3D rotationVector = new Vector3D(dx, dy, dz);
            URVector vector = new URVector(poseVector, rotationVector);

            Point3D position = new Point3D(0, 0, 0);
            Point3D rotation = new Point3D(x, y, z);
            URPose pose = new URPose(position, rotation);

            var finalPose = vector - pose;

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
        public void SubtractionOperatorVectorVector_PoseVectorShouldSubtract(
            double x1, double y1, double z1, // first position vector
            double x2, double y2, double z2, // second position vector
            double expectedX, double expectedY, double expectedZ)    // expected position vector
        {
            Vector3D poseVector1 = new Vector3D(x1, y1, z1);
            Vector3D rotationVector1 = new Vector3D(0, 0, 0);
            URVector vector1 = new URVector(poseVector1, rotationVector1);

            Vector3D poseVector2 = new Vector3D(x2, y2, z2);
            Vector3D rotationVector2 = new Vector3D(0, 0, 0);
            URVector vector2 = new URVector(poseVector2, rotationVector2);

            var finalVector = vector1 - vector2;

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
        public void SubtractionOperatorVectorVector_RotationVectorShouldSubtract(
            double x1, double y1, double z1, // first rotation vector
            double x2, double y2, double z2, // second rotation vector
            double expectedX, double expectedY, double expectedZ)    // expected rotation vector
        {
            Vector3D poseVector1 = new Vector3D(0, 0, 0);
            Vector3D rotationVector1 = new Vector3D(x1, y1, z1);
            URVector vector1 = new URVector(poseVector1, rotationVector1);

            Vector3D poseVector2 = new Vector3D(0, 0, 0);
            Vector3D rotationVector2 = new Vector3D(x2, y2, z2);
            URVector vector2 = new URVector(poseVector2, rotationVector2);

            var finalVector = vector1 - vector2;

            var actualX = finalVector.RotationVector.X;
            var actualY = finalVector.RotationVector.Y;
            var actualZ = finalVector.RotationVector.Z;

            Assert.Equal(expectedX, actualX);
            Assert.Equal(expectedY, actualY);
            Assert.Equal(expectedZ, actualZ);
        }

        #endregion
    }
}
