using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboCodeProject
{
    class InShotRange: BTNode
    {
        private float maxDistance;
        public InShotRange(BlackBoard blackBoard, float maxDistance = 300)
        {
            this.blackBoard = blackBoard;
            this.maxDistance = maxDistance;
        }
        public override BTNodeStatus Tick()
        {
            if (blackBoard.lastScannedRobotEvent != null)
            {
                if (blackBoard.lastScannedRobotEvent.Distance < maxDistance)
                {
                    if (Math.Abs(GetAngleOfGunHeading()) < 10)
                    {
                        return BTNodeStatus.failed;
                    }
                }
            }

            return BTNodeStatus.succes;
        }

        public double GetAngleOfGunHeading()
        {
            if (blackBoard.lastScannedRobotEvent == null)
            {
                return 0;
            }

            double angle = blackBoard.lastScannedRobotEvent.Bearing -
                    (blackBoard.robot.GunHeading - blackBoard.robot.Heading);

            if (angle < -180)
            {
                angle += 360;
            }
            return angle;

        }


    }
}
