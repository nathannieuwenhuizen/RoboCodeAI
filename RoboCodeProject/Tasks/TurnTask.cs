using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robocode;

namespace RoboCodeProject
{
    public enum Orientation
    {
        left,
        right,
        forth,
        bottom,
        none
    }

    public class TurnTask : BTNode
    {
        public bool toEnemy;
        public Orientation orientation = Orientation.none;

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
        public double GetAngleOfHeading()
        {
            if (blackBoard.lastScannedRobotEvent == null)
            {
                return 0;
            }
            double angle = blackBoard.lastScannedRobotEvent.Bearing;

            //blackBoard.robot.Out.WriteLine("Bearing: " + blackBoard.lastScannedRobotEvent.Bearing);
            //blackBoard.robot.Out.WriteLine("Heading: " + blackBoard.robot.Heading);

            if (angle < -180)
            {
                //angle += 360;
            }

            return angle;

        }

        public double GetRotationToScannedRobotFuture()
        {

            ScannedRobotEvent enemy = blackBoard.lastScannedRobotEvent;
            if (enemy == null)
            {
                return 0;
            }

            double angleToEnemy = enemy.Bearing;

            // Calculate the angle to the scanned robot
            double angle = Global.DegToRad(blackBoard.robot.Heading + angleToEnemy % 360);

            // Calculate the coordinates of the robot
            double enemyX = (blackBoard.robot.X + Math.Sin(angle) * enemy.Distance);
            double enemyY = (blackBoard.robot.Y + Math.Cos(angle) * enemy.Distance);
            //double dirX = blackBoard.robot.be

            //blackBoard.robot.Out.WriteLine("Angle: " + angle + "enemy x: " + enemyX + " | enemy y: " + enemyY);
            return 0;
        }
        public override BTNodeStatus Tick()
        {
            return BTNodeStatus.failed;
        }


    }
}
