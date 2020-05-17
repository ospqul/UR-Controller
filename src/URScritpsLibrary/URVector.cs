using System.Windows.Media.Media3D;

namespace URScritpsLibrary
{
    public class URVector
    {
        public Vector3D PoseVector { get; set; }
        public Vector3D RotationVector { get; set; }

        public URVector(Vector3D poseVector, Vector3D rotationVector)
        {
            PoseVector = poseVector;
            RotationVector = rotationVector;
        }

        #region methods

        public static URPose Add(URVector vector, URPose pose)
        {
            var newPosition = Vector3D.Add(vector.PoseVector, pose.Position);
            var newRotation = Vector3D.Add(vector.RotationVector, pose.Rotation);
            return new URPose(newPosition, newRotation);
        }

        public static URVector Add(URVector vector1, URVector vector2)
        {
            var newPoseVector = Vector3D.Add(vector1.PoseVector, vector2.PoseVector);
            var newRotationVector = Vector3D.Add(vector1.RotationVector, vector2.RotationVector);
            return new URVector(newPoseVector, newRotationVector);
        }

        public static URPose Subtract(URVector vector, URPose pose)
        {
            var newPosition = Vector3D.Subtract(vector.PoseVector, pose.Position);
            var newRotation = Vector3D.Subtract(vector.RotationVector, pose.Rotation);
            return new URPose(newPosition, newRotation);
        }

        public static URVector Subtract(URVector vector1, URVector vector2)
        {
            var newPoseVector = Vector3D.Subtract(vector1.PoseVector, vector2.PoseVector);
            var newRotationVector = Vector3D.Subtract(vector1.RotationVector, vector2.RotationVector);
            return new URVector(newPoseVector, newRotationVector);
        }

        #endregion

        #region operators

        public static URPose operator +(URVector vector, URPose pose)
        {
            return new URPose(vector.PoseVector + pose.Position, vector.RotationVector + pose.Rotation);
        }

        public static URVector operator +(URVector vector1, URVector vector2)
        {
            return new URVector(vector1.PoseVector + vector2.PoseVector, vector1.RotationVector + vector2.RotationVector);
        }

        public static URPose operator -(URVector vector, URPose pose)
        {
            return new URPose(vector.PoseVector - pose.Position, vector.RotationVector - pose.Rotation);
        }

        public static URVector operator -(URVector vector1, URVector vector2)
        {
            return new URVector(vector1.PoseVector - vector2.PoseVector, vector1.RotationVector - vector2.RotationVector);
        }

        #endregion

    }
}
