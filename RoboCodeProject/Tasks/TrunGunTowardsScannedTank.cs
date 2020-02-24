using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboCodeProject
{
    class TurnGunTowardsScannedTank:BTNode
    {
        public TurnGunTowardsScannedTank(BlackBoard blackBoard)
        {
            this.blackBoard = blackBoard;
        }
        public override BTNodeStatus Tick()
        {

            if (blackBoard.lastScannedRobotEvent != null)
            {
                double rotation = blackBoard.lastScannedRobotEvent.Bearing -
                    Math.Abs(blackBoard.robot.GunHeading - blackBoard.robot.Heading);
                blackBoard.robot.TurnGunRight(rotation);
                if (Math.Abs(rotation) < 1)
                {
                    return BTNodeStatus.succes;
                } else
                {
                    return BTNodeStatus.running;
                }

            }
            return BTNodeStatus.failed;
        }


    }
}
