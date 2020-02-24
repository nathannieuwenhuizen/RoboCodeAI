namespace RoboCodeProject
{
    class MoveAhead : BTNode
    {
        private int moveDistance;
        public MoveAhead(BlackBoard blackBoard, int movePixels)
        {
            this.blackBoard = blackBoard;
            moveDistance = movePixels;
        }
        public override BTNodeStatus Tick()
        {
            blackBoard.robot.Ahead(moveDistance);

            return BTNodeStatus.succes;
        }
    }
}
