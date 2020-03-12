using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robocode;

namespace RoboCodeProject
{
    class TurnHeadingToEnemy : TurnTask
    {
        public TurnHeadingToEnemy(BlackBoard blackBoard)
        {
            this.blackBoard = blackBoard;
        }

        public override BTNodeStatus Tick()
        {

            if (blackBoard.lastScannedRobotEvent != null)
            {
                //GetRotationToScannedRobotFuture();
                double rotation = GetAngleOfHeading();
                blackBoard.robot.Out.WriteLine("Angle: " + rotation);

                blackBoard.robot.TurnRight(rotation);
                blackBoard.robot.Ahead(10);

                if (Math.Abs(rotation) < 10)
                {
                    return BTNodeStatus.succes;
                }
                else
                {
                    return BTNodeStatus.running;
                }

            }
            return BTNodeStatus.failed;
        }


    }
}
