﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robocode;

namespace RoboCodeProject
{
    public class TurnHeading : TurnTask
    {
        public bool set = false;
        public TurnHeading(BlackBoard blackBoard, bool _toEnemy = true, Orientation _orientation = Orientation.none, bool _set = true)
        {
            toEnemy = _toEnemy;
            if (_orientation != Orientation.none)
            {
                blackBoard.headingOrientation = _orientation;
            }
            set = _set;
            this.blackBoard = blackBoard;
        }

        public override BTNodeStatus Tick()
        {
            double rotation = 0;
            double accuracy = 1;

            if (toEnemy)
            {
                if (blackBoard.lastScannedRobotEvent != null)
                {

                    //heading angle to enemy
                    rotation = GetAngleOfHeading();

                    //distance to enemy
                    double distance = blackBoard.lastScannedRobotEvent.Distance;

                    //be more accurate when the enmy is more far away
                    accuracy = 180 / (distance * 0.5f);

                    blackBoard.robot.SetTurnRight(rotation);
                    if (rotation > 180)
                    {
                        if (set)
                        {
                            blackBoard.robot.SetTurnLeft(360 - rotation);
                            blackBoard.robot.Execute();
                            return BTNodeStatus.succes;

                        }
                        else
                        {
                            blackBoard.robot.TurnLeft(360 - rotation);
                        }
                    }
                    else
                    {
                        if (set)
                        {
                            blackBoard.robot.SetTurnRight(rotation);
                            blackBoard.robot.Execute();
                            return BTNodeStatus.succes;

                        }
                        else
                        {
                            blackBoard.robot.TurnRight(rotation);
                        }
                    }
                }

            } else
            {
                switch (blackBoard.headingOrientation)
                {
                    case Orientation.left:
                        rotation = 270 - blackBoard.robot.Heading;
                        break;
                    case Orientation.right:
                        rotation = 90 -blackBoard.robot.Heading;
                        break;
                    case Orientation.forth:
                        rotation = -blackBoard.robot.Heading;
                        break;
                    case Orientation.bottom:
                        rotation = 180 -blackBoard.robot.Heading;
                        break;
                    case Orientation.none:
                        break;
                   
                }
                rotation = (360 + rotation) % 360;
                if (rotation < 180)
                {
                    blackBoard.robot.SetTurnRight(rotation);
                }
                else
                {
                    blackBoard.robot.SetTurnLeft(360 - rotation);
                }


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


    }
}
