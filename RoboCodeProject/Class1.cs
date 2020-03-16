using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Robocode;

namespace RoboCodeProject
{
    public class Class1 : AdvancedRobot
    {

        public BTNode CornerBehaviour;
        public BTNode AttackBehaviour;
        public BTNode BeahaviorTree;
        public BlackBoard blackBoard = new BlackBoard();

        public override void Run()
        {
            blackBoard.robot = this;

            AttackBehaviour = 
                new Sequence(blackBoard,
                //new ChangeColor(blackBoard, Color.Red),
                                    new MoveAhead(blackBoard, 300, true),

                    new TurnGunToEnemy(blackBoard, false, Orientation.forth),

                    new ScanRobot(blackBoard, 360, Orientation.none),
                    new TurnHeadingToEnemy(blackBoard, true, Orientation.forth),

                    
                    new CheckNode(blackBoard, () => { return blackBoard.hitsWall || blackBoard.hitsRobot; }, 
                        //new ChangeColor(blackBoard, Color.Blue),
                   
                        new CheckNode(blackBoard, () => { return new InShotRange(blackBoard, 100).Tick() == BTNodeStatus.failed; },
                            new ChangeColor(blackBoard, Color.Green),
                            new Shoot(blackBoard, 50)
                        ),
                         new MoveAhead(blackBoard, -100, false, true)
                    )

                //new Selector(blackBoard, new InShotRange(blackBoard, 150),
                //new Shoot(blackBoard, 5)
                //)


                );
            CornerBehaviour = 
                    new Sequence(blackBoard,
                    new ChangeColor(blackBoard, Color.Blue),
                    new TurnGunToEnemy(blackBoard, false, Orientation.right),
                    new ScanRobot(blackBoard, 0, Orientation.right),
                    

                    new Selector(blackBoard, new IsAtWall(blackBoard, 150),
                    new TurnHeadingToEnemy(blackBoard, false, Orientation.none)
                    ),

                    new Selector(blackBoard, new InShotRange(blackBoard, 10000),
                    new Shoot(blackBoard, 5)
                    ),

                    new MoveAhead(blackBoard, 100, true)
                    );



            BeahaviorTree = new Sequence(blackBoard,
                new ScanRobot(blackBoard, 0),
                AttackBehaviour
               ); 
            //IsAdjustGunForRobotTurn = true;
            IsAdjustRadarForGunTurn = true;

            new ScanRobot(blackBoard, 360).Tick();
            new TurnHeadingToEnemy(blackBoard, false, Orientation.left).Tick();
            while (true)
            {
                //blackBoard.robot.Out.WriteLine("Orientation: " + blackBoard.desiredOrientation + " | robot X : " + X + " | robot Y: " + Y);

                BeahaviorTree.Tick();
            }
        }


        public override void OnScannedRobot(ScannedRobotEvent evnt)
        {
            blackBoard.lastScannedRobotEvent = evnt;
        }
        public override void OnBulletHitBullet(BulletHitBulletEvent evnt)
        {
            blackBoard.currentAmountBulletsMissed = 0;
            blackBoard.totalHittedBullets++;
        }
        public override void OnBulletMissed(BulletMissedEvent evnt)
        {
            blackBoard.totalAmountMissedBullets++;
            blackBoard.currentAmountBulletsMissed++;
        }

        public override void OnHitWall(HitWallEvent evnt)
        {
            Out.WriteLine("I hit wall");

            blackBoard.hitsWall = true;
        }
        public override void OnHitRobot(HitRobotEvent e)
        {
            Out.WriteLine("I hit robot");

            blackBoard.hitsRobot = true;
        }

    }
}
