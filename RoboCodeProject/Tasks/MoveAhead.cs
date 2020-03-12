namespace RoboCodeProject
{
    class MoveAhead : BTNode
    {
        private int moveDistance;
        private bool set;
        public MoveAhead(BlackBoard blackBoard, int _movePixels, bool _set = false)
        {
            this.blackBoard = blackBoard;
            moveDistance = _movePixels;
            set = _set;
        }
        public override BTNodeStatus Tick()
        {

            if (blackBoard.movesForward)
            {
                if (set)
                {
                    blackBoard.robot.SetAhead(moveDistance);
                    blackBoard.robot.Execute();
                }
                else {
                    blackBoard.robot.Ahead(moveDistance);
                }
            } else
            {
                if (set)
                {
                    blackBoard.robot.SetBack(moveDistance);
                    blackBoard.robot.Execute();
                }
                else
                {
                    blackBoard.robot.Back(moveDistance);
                }
            }

            return BTNodeStatus.succes;
        }
    }
}
