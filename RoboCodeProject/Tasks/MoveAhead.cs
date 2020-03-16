namespace RoboCodeProject
{
    class MoveAhead : BTNode
    {
        private int moveDistance;
        private bool set;
        private bool resetsCollission;
        public MoveAhead(BlackBoard blackBoard, int _movePixels, bool _set = false, bool _resetsCollission = false)
        {
            this.blackBoard = blackBoard;
            moveDistance = _movePixels;
            set = _set;
            resetsCollission = _resetsCollission;
        }
        public override BTNodeStatus Tick()
        {
            if (resetsCollission)
            {
                blackBoard.hitsWall = false;
                blackBoard.hitsRobot = false;
            }

            if (set)
            {
                blackBoard.robot.SetAhead(moveDistance);
                blackBoard.robot.Execute();
            }
            else {
                blackBoard.robot.Ahead(moveDistance);
            }
            return BTNodeStatus.succes;
        }
    }
}
