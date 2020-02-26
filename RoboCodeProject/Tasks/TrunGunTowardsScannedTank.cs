using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robocode;

namespace RoboCodeProject
{
    class TurnGunTowardsScannedTank:BTNode
    {
        public TurnGunTowardsScannedTank(BlackBoard blackBoard)
        {
            this.blackBoard = blackBoard;
        }
        public double GetRotationToScannedRobot()
        {
            return blackBoard.lastScannedRobotEvent.Bearing -
                    (blackBoard.robot.GunHeading - blackBoard.robot.Heading);
        }
        public double GetRotationToScannedRobotFuture()
        {
            ScannedRobotEvent enemy = blackBoard.lastScannedRobotEvent;

            double angleToEnemy = enemy.Bearing;

            // Calculate the angle to the scanned robot
            double angle = (blackBoard.robot.Heading + angleToEnemy % 360);

            // Calculate the coordinates of the robot
            double enemyX = (blackBoard.robot.X + Math.Sin(angle) * enemy.Distance);
            double enemyY = (blackBoard.robot.Y + Math.Cos(angle) * enemy.Distance);
            blackBoard.robot.Out.WriteLine("enemy x: " + enemyX + " | enemy y: " + enemyY);
            return 0;
        }
        public override BTNodeStatus Tick()
        {

            if (blackBoard.lastScannedRobotEvent != null)
            {
                GetRotationToScannedRobotFuture();
                double rotation = GetRotationToScannedRobot();

                if (rotation < 180)
                {
                    blackBoard.robot.TurnGunRight(rotation);
                }
                else
                {
                    blackBoard.robot.TurnGunLeft( 360 - rotation);
                }


                if (Math.Abs(rotation) < 10)
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
