using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboCodeProject
{
    class HitSomething : BTNode
    {
        private bool checkRobot;
        private bool checkWall;
        public HitSomething(BlackBoard _blackboard, bool _checkRobot, bool _checkWall)
        {
            blackBoard = _blackboard;
            checkRobot = _checkRobot;
            checkWall = _checkWall;
        }
        public override BTNodeStatus Tick()
        {
            if (checkRobot == blackBoard.hitsRobot == true)
            {
                return BTNodeStatus.failed;
            } else if (checkWall == blackBoard.hitsWall == true)
            {
                return BTNodeStatus.failed;
            }
            
            return BTNodeStatus.succes;
        }
    }
}
