using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robocode;

namespace RoboCodeProject
{
    public class IsAtWall : BTNode
    {
        private float offset;
        public IsAtWall(BlackBoard blackBoard, float offset = 0)
        {
            this.blackBoard = blackBoard;
            this.offset = offset;
        }
        public override BTNodeStatus Tick()
        {
            Orientation currentHeading;
            double heading = blackBoard.robot.Heading;
            if (heading < 45 || heading > 315)
            {
                currentHeading = Orientation.forth;
            }
            else if (heading > 45 && heading < 135)
            {
                currentHeading = Orientation.right;
            }
            else if (heading > 135 && heading < 225)
            {
                currentHeading = Orientation.bottom;
            }
            else
            {
                currentHeading = Orientation.left;
            }

            switch (currentHeading)
            {
                case Orientation.forth:
                    if (blackBoard.robot.Y > blackBoard.robot.BattleFieldHeight - offset)
                    {
                        blackBoard.headingOrientation = Orientation.right;
                    }
                    
                    break;
                case Orientation.left:
                    if (blackBoard.robot.X < offset)
                    {
                        blackBoard.headingOrientation = Orientation.forth;
                    }
                    break;
                case Orientation.right:
                    if (blackBoard.robot.X > blackBoard.robot.BattleFieldWidth - offset)
                    {
                        blackBoard.headingOrientation = Orientation.bottom;
                    }
                    break;
                case Orientation.bottom:
                    if (blackBoard.robot.Y < offset)
                    {
                        blackBoard.headingOrientation = Orientation.left;
                    }
                    break;
            }
            
            if (currentHeading != blackBoard.headingOrientation)
            {
                return BTNodeStatus.failed;
            }

            //if (blackBoard.hitsWall)
            //{
            //    blackBoard.hitsWall = false;
            //    blackBoard.robot.Out.Write("wall");
            //    //blackBoard.desiredOrientation
            //    return BTNodeStatus.failed;
            //}
            return BTNodeStatus.succes;
        }

    }
}
