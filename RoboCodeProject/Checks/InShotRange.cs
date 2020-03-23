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
        private float checklAngle;
        public InShotRange(BlackBoard blackBoard, float maxDistance = 300, float _checkAngle = 10)
        {
            this.blackBoard = blackBoard;
            this.maxDistance = maxDistance;
            this.checklAngle = _checkAngle;
        }
        public override BTNodeStatus Tick()
        {
            if (blackBoard.lastScannedRobotEvent != null)
            {
                if (blackBoard.lastScannedRobotEvent.Distance < maxDistance)
                {
                    if (Math.Abs(GetAngleOfGunHeading()) < checklAngle)
                    {
                        blackBoard.CurentBehaviourInfo.aimAttempts = 0;
                        return BTNodeStatus.failed;
                    } 
                }
            }
            blackBoard.CurentBehaviourInfo.aimAttempts++;
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
