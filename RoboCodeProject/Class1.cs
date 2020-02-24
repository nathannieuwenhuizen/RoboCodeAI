using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robocode;

namespace RoboCodeProject
{
    public class Class1 : AdvancedRobot
    {
        public BTNode BeahaviorTree;
        public BlackBoard blackBoard = new BlackBoard();

        public override void Run()
        {
            blackBoard.robot = this;
            BeahaviorTree = new Sequence(blackBoard, 
                new ScanRobot(blackBoard, 360), 
                new MoveAhead(blackBoard, 100),
                new MoveAhead(blackBoard, -100)
                );
            IsAdjustGunForRobotTurn = true;
            IsAdjustRadarForGunTurn = true;

            while (true)
            {
                BeahaviorTree.Tick();
            }

        }
        public override void OnScannedRobot(ScannedRobotEvent evnt)
        {
            Out.WriteLine("I scanned robot: " + evnt.Name);
            blackBoard.lastScannedRobotEvent = evnt;
            
        }
    }
}
