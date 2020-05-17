namespace URScritpsLibrary
{
    public class URMovement : IURMovement
    {
        public URPose Start { get; set; }
        public URPose End { get; set; }
        public URVector Movement { get; set; }

        public URMovement(URPose start, URPose end)
        {
            Start = start;
            End = end;
            Movement = End - Start;
        }
    }
}
