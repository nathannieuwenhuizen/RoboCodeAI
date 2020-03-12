using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboCodeProject
{
    class Shoot:BTNode
    {
        private int damage;
        public Shoot(BlackBoard blackBoard, int _damage = 1)
        {
            this.blackBoard = blackBoard;
            damage = _damage;
        }
        public override BTNodeStatus Tick()
        {
            blackBoard.robot.Fire(damage);
            return BTNodeStatus.succes;
        }


    }
}
