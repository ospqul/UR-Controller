using System.Windows.Media.Media3D;

namespace URScritpsLibrary
{
    public class URPose
    {
        public Point3D Position { get; set; }
        public Point3D Rotation { get; set; }

        public URPose(Point3D position, Point3D rotation)
        {
            Position = position;
            Rotation = rotation;
        }

        #region methods

        public static URPose Add(URPose pose, URVector vector)
        {
            var newPosition = Point3D.Add(pose.Position, vector.PoseVector);
            var newRotation = Point3D.Add(pose.Rotation, vector.RotationVector);
            return new URPose(newPosition, newRotation);
        }

        public static URVector Subtract(URPose pose1, URPose pose2)
        {
            var newPoseVector = Point3D.Subtract(pose1.Position, pose2.Position);
            var newRotationVector = Point3D.Subtract(pose1.Rotation, pose2.Rotation);
            return new URVector(newPoseVector, newRotationVector);
        }

        public static URPose Subtract(URPose pose, URVector vector)
        {
            var newPosition = Point3D.Subtract(pose.Position, vector.PoseVector);
            var newRotation = Point3D.Subtract(pose.Rotation, vector.RotationVector);
            return new URPose(newPosition, newRotation);
        }

        #endregion

        #region operators

        public static URPose operator +(URPose pose, URVector vector)
        {
            return new URPose(pose.Position + vector.PoseVector, pose.Rotation + vector.RotationVector);
        }

        public static URVector operator -(URPose pose1, URPose pose2)
        {
            return new URVector(pose1.Position - pose2.Position, pose1.Rotation - pose2.Rotation);
        }

        public static URPose operator -(URPose pose, URVector vector)
        {
            return new URPose(pose.Position - vector.PoseVector, pose.Rotation - vector.RotationVector);
        }

        #endregion
    }
}
