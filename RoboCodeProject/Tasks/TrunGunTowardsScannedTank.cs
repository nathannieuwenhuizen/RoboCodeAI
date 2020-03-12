﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robocode;

namespace RoboCodeProject
{
    class TurnGunTowardsScannedTank: TurnTask
    {
        public TurnGunTowardsScannedTank(BlackBoard blackBoard)
        {
            this.blackBoard = blackBoard;
        }

        public override BTNodeStatus Tick()
        {

            if (blackBoard.lastScannedRobotEvent != null)
            {
                //GetRotationToScannedRobotFuture();
                double rotation = GetAngleOfGunHeading();
                blackBoard.robot.Out.WriteLine("Angle: " + rotation);

                    blackBoard.robot.TurnGunRight(rotation);


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
