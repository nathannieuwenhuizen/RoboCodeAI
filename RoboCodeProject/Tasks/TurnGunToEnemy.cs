using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robocode;

namespace RoboCodeProject
{
    public class TurnGun: TurnTask
    {
        public TurnGun(BlackBoard blackBoard, bool _toEnemy = true, Orientation _orientation = Orientation.none)
        {
            toEnemy = _toEnemy;
            orientation = _orientation;

            this.blackBoard = blackBoard;
        }

        public override BTNodeStatus Tick()
        {
            if (orientation != Orientation.none)
            {
                blackBoard.gunOrientation = orientation;
            }
            double rotation = 0;
            double accuracy = 5;

                            //blackBoard.robot.Out.WriteLine("Orientation: " + blackBoard.gunOrientation + " | rotation " + rotation);

            if (toEnemy)
            {
                if (blackBoard.lastScannedRobotEvent != null)
                {
                    rotation = GetAngleOfGunHeading(); 
                    double distance = blackBoard.lastScannedRobotEvent.Distance;
                    accuracy = 180 / (distance * 0.1f);

                    blackBoard.robot.SetTurnGunRight(rotation);

                    return BTNodeStatus.succes;


                    if (Math.Abs(rotation) < accuracy)
                    {
                        return BTNodeStatus.succes;
                    }
                    else
                    {
                        return BTNodeStatus.running;
                    }

                }
            } else
            {
                double localRotation = blackBoard.robot.GunHeading - blackBoard.robot.Heading;

                switch (blackBoard.gunOrientation)
                {
                    case Orientation.left:
                        rotation = 270 - localRotation;
                        break;
                    case Orientation.right:
                        rotation = 90 - localRotation;
                        break;
                    case Orientation.forth:
                        rotation = -localRotation;
                        break;
                    case Orientation.bottom:
                        rotation = 180 - localRotation;
                        break;
                    case Orientation.none:
                        break;

                }

                rotation = (360 + rotation) % 360;
                if (rotation < 180)
                {
                    blackBoard.robot.SetTurnGunRight(rotation);
                }
                else
                {
                    blackBoard.robot.SetTurnGunLeft(360 - rotation);
                }

                if (Math.Abs(rotation) < accuracy)
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
