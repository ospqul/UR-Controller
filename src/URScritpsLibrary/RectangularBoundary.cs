using System;
using System.Windows.Media.Media3D;

namespace URScritpsLibrary
{
    public class RectangularBoundary : IRectangularBoundary
    {
        public URPose LeftTop { get; set; }
        public URPose RightTop { get; set; }
        public URPose RightBottom { get; set; }
        public URPose LeftBottom { get; set; }

        public URVector ScanMovement { get; set; }
        public URVector IndexMovement { get; set; }

        public RectangularBoundary(URPose lefttop, URPose righttop, URPose rightbottom, URPose leftbottom)
        {
            LeftTop = lefttop;
            RightTop = righttop;
            RightBottom = rightbottom;
            LeftBottom = leftbottom;

            ScanMovement = RightTop - LeftTop;
            IndexMovement = LeftBottom - LeftTop;
        }

        public RectangularBoundary(IURMovement scanMove, URPose pose)
        {
            // define LeftTop corner and RightTop corner positions with URMove
            LeftTop = scanMove.Start;
            RightTop = scanMove.End;
            ScanMovement = scanMove.Movement;

            IndexMovement = GetIndexMoveVector(pose);

            LeftBottom = LeftTop + IndexMovement;
            RightBottom = RightTop + IndexMovement;
        }

        private URVector GetIndexMoveVector(URPose pose)
        {
            Vector3D vScanMove = ScanMovement.PoseVector;
            Vector3D vector = (pose - LeftTop).PoseVector;

            double angle = Vector3D.AngleBetween(vScanMove, vector); // 0-180 degree
            // turn vScanMove into unit vector
            vScanMove.Normalize();
            var projection = Vector3D.Multiply(vScanMove, vector.Length * Math.Cos(angle * Math.PI / 180));
            var vIndexMove = vector - projection;
            return new URVector(vIndexMove, new Vector3D(0, 0, 0));
        }
    }
}
