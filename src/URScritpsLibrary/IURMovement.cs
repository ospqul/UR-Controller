namespace URScritpsLibrary
{
    public interface IURMovement
    {
        URPose End { get; set; }
        URVector Movement { get; set; }
        URPose Start { get; set; }
    }
}