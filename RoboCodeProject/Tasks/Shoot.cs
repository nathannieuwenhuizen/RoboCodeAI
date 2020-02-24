using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboCodeProject
{
    class Shoot:BTNode
    {

        public Shoot(BlackBoard blackBoard)
        {
            this.blackBoard = blackBoard;
        }
        public override BTNodeStatus Tick()
        {
            blackBoard.robot.Fire(1);
            return BTNodeStatus.succes;
        }


    }
}
