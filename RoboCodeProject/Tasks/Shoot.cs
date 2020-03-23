using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboCodeProject
{
    class Shoot:BTNode
    {
        private double damage;
        private bool basedOnDistance;
        public Shoot(BlackBoard blackBoard, double _damage = 1, bool _basedOnDistance = false)
        {
            this.blackBoard = blackBoard;
            damage = _damage;
            basedOnDistance = _basedOnDistance;
        }
        public override BTNodeStatus Tick()
        {
            double finalDamage = damage;
            if (basedOnDistance)
            {
                finalDamage = damage * (1 - (blackBoard.lastScannedRobotEvent.Distance / Global.BattleFieldDiagonal(blackBoard.robot)));
                blackBoard.robot.Out.WriteLine("Damage: " + finalDamage );
                //blackBoard.robot.Out.WriteLine("Damage: " + (blackBoard.lastScannedRobotEvent.Distance / Global.BattleFieldDiagonal(blackBoard.robot)));
            }
            blackBoard.robot.Fire(finalDamage);
            return BTNodeStatus.succes;
        }


    }
}
