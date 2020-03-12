﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboCodeProject
{
    public class ScanRobot : TurnTask
    {
        private float scanDegrees;
        public ScanRobot(BlackBoard blackBoard, float scanDegrees = 0, Orientation _orientation = Orientation.none)
        {
            this.blackBoard = blackBoard;
            this.scanDegrees = scanDegrees;
            orientation = _orientation;
        }
        public override BTNodeStatus Tick()
        {
            if (orientation == Orientation.none)
            {
                blackBoard.robot.TurnRadarLeft(scanDegrees);
            } else
            {
                double rotation = 0;
                double localRotation = blackBoard.robot.RadarHeading - blackBoard.robot.Heading;
                double accuracy = 1;
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
                blackBoard.robot.Out.WriteLine("Orientation: " + blackBoard.gunOrientation + " | rotation " + rotation);

                rotation = (360 + rotation) % 360;
                if (rotation < 180)
                {
                    blackBoard.robot.TurnRadarRight(rotation);
                }
                else
                {
                    blackBoard.robot.TurnRadarLeft(360 - rotation);
                }


                if (Math.Abs(rotation) < accuracy)
                {
                    blackBoard.robot.TurnRadarLeft(5);
                    blackBoard.robot.TurnRadarRight(5);

                    return BTNodeStatus.succes;
                }
                else
                {
                    return BTNodeStatus.running;
                }
            }
            return BTNodeStatus.succes;
        }

    }
}
