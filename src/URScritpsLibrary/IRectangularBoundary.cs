namespace URScritpsLibrary
{
    public interface IRectangularBoundary
    {
        URVector IndexMovement { get; set; }
        URPose LeftBottom { get; set; }
        URPose LeftTop { get; set; }
        URPose RightBottom { get; set; }
        URPose RightTop { get; set; }
        URVector ScanMovement { get; set; }
    }
}