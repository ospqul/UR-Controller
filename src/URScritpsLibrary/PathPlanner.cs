using System;
using System.Collections.Generic;
using System.Windows.Media.Media3D;

namespace URScritpsLibrary
{
    public static class PathPlanner
    {
        // O-->-->-->-->-->-->-->O
        //                       |
        // O--<--<--<--<--<--<--<O
        // |
        // O-->-->-->-->-->-->-->O
        public static List<IURMovement> SStyle(RectangularBoundary boundary, double brushThickness, double overlap)
        {
            List<IURMovement> movements = new List<IURMovement>();

            if ((overlap >= 1) || (overlap < 0))
            {
                return movements;
            }

            var offset = brushThickness * (1 - overlap);
            var indexVector = boundary.IndexMovement.PoseVector;
            int number = (int)Math.Ceiling(indexVector.Length / offset);
            indexVector.Normalize();
            if (number > 1)
            {                
                for (int i = 0; i < number; i++)
                {
                    var deltaMove = Vector3D.Multiply(indexVector, i* offset);
                    var rotationMove = new Vector3D(0, 0, 0);
                    if (i % 2 == 0)
                    {
                        URVector delta = new URVector(deltaMove, rotationMove);
                        movements.Add(new URMovement(boundary.LeftTop + delta, boundary.RightTop + delta));
                    }
                    else
                    {
                        URVector delta = new URVector(deltaMove, rotationMove);
                        movements.Add(new URMovement(boundary.RightTop + delta, boundary.LeftTop + delta));
                    }
                }
            }
            else if (number == 1)
            {
                movements.Add(new URMovement(boundary.LeftTop, boundary.RightTop));
            }    

            return movements;
        }
    }
}
