using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Robocode;

namespace RoboCodeProject
{
    public class RealFinalFinalRobot : AdvancedRobot
    {

        public BTNode CornerBehaviour;
        public BTNode AttackBehaviour;
        public BTNode WinBehaviour;
        public BTNode BeahaviorTree;
        public BlackBoard blackBoard;

        public override void Run()
        {
            blackBoard = new BlackBoard();
            blackBoard.robot = this;

            AttackBehaviour =
                new Sequence(blackBoard,

                    new GenericSimpleTask(blackBoard, () =>
                    {
                        blackBoard.currentAmountBulletsMissedInAttack = 0;
                        IsAdjustGunForRobotTurn = false;
                        IsAdjustRadarForGunTurn = true;
                        blackBoard.behaviour = Behaviour.attack;
                        return true;
                    }),
                    new TurnGun(blackBoard, false, Orientation.forth),
                    new ChangeColor(blackBoard, Color.Red),

                    new MoveAhead(blackBoard, 200, true),
                    new TurnScan(blackBoard, 360, Orientation.none, true),
                    new TurnHeading(blackBoard, true, Orientation.none),


                    new Selector(blackBoard, new InShotRange(blackBoard, 400, 10),
                    new Shoot(blackBoard, 2)
                    ),

                    new CheckNode(blackBoard, () => {
                        return blackBoard.hitsRobot;
                    },
                         new Shoot(blackBoard, 5),
                         new MoveAhead(blackBoard, -50, false, true)
                    ),

                    new CheckNode(blackBoard, () => {
                        return blackBoard.currentAmountBulletsMissedInAttack < 20;
                    })


                );

            CornerBehaviour =
                    new Sequence(blackBoard,
                    new CheckNode(blackBoard, () =>
                    {
                        return blackBoard.behaviour != Behaviour.corner;
                    },
                    //only for the first time
                        new TurnHeading(blackBoard, false, Orientation.left),
                        new TurnGun(blackBoard, false, Orientation.right),
                        new GenericSimpleTask(blackBoard, () =>
                        {
                            blackBoard.currentAmountBulletsMissedInAttack = 0;
                            IsAdjustGunForRobotTurn = false;
                            IsAdjustRadarForGunTurn = true;
                            blackBoard.behaviour = Behaviour.corner;
                            return true;
                        }),
                        new ChangeColor(blackBoard, Color.Blue)

                    ),

                    new TurnScan(blackBoard, 45, Orientation.none),
                    new TurnScan(blackBoard, -45, Orientation.none),
                    new TurnScan(blackBoard, 0, Orientation.right),
                    

                    new Selector(blackBoard, new IsAtWall(blackBoard, 150),
                    new TurnHeading(blackBoard, false, Orientation.none)
                    ),

                    new Selector(blackBoard, new InShotRange(blackBoard, 10000),
                    new Shoot(blackBoard, 5)
                    ),

                    new MoveAhead(blackBoard, 100, true)
                    );


            WinBehaviour =
            new Sequence(blackBoard,
                new CheckNode(blackBoard, () =>
                {
                    Out.WriteLine("I won");
                    return blackBoard.behaviour != Behaviour.win;
                },
                new MoveAhead(blackBoard, 0, true),
                new TurnGun(blackBoard, false, Orientation.forth),
                new GenericSimpleTask(blackBoard, () =>
                {
                    blackBoard.behaviour = Behaviour.win;
                    return true;
                })
                ),
                new ChangeColor(blackBoard, Color.Green, false),
                new TurnScan(blackBoard, 90, Orientation.none)

            );


            BeahaviorTree = new Sequence(blackBoard,

                new TurnScan(blackBoard, 0),
                //do dance if win
                new CheckNode(blackBoard, () =>
                {
                    return blackBoard.wonRound == true;
                },
                WinBehaviour
               ),

                //do corner if amount of consecutivem isses isnt too much
                new CheckNode(blackBoard, () => {
                    blackBoard.robot.Out.WriteLine("Check corner: " + blackBoard.currentAmountBulletsMissedInCorner);
                    return blackBoard.currentAmountBulletsMissedInCorner < 10 && blackBoard.aimAttemptsCorner < 20;
                },
                    CornerBehaviour
                ),

               AttackBehaviour,

               new CheckNode(blackBoard, () => { return blackBoard.currentAmountBulletsMissedInAttack > 2 || blackBoard.aimAttemptsAttack > 10; },
                   new GenericSimpleTask(blackBoard, () => {
                       blackBoard.currentAmountBulletsMissedInCorner = 0; 
                       blackBoard.aimAttemptsCorner = 0;
                       return true;
                   })
               )
               );

            new TurnScan(blackBoard, 360).Tick();

            while (true)
            {
                BeahaviorTree.Tick();
            }
        }


        public override void OnScannedRobot(ScannedRobotEvent evnt)
        {
            blackBoard.lastScannedRobotEvent = evnt;
        }
        public override void OnBulletHitBullet(BulletHitBulletEvent evnt)
        {
            if (blackBoard.behaviour == Behaviour.attack)
            {
                blackBoard.currentAmountBulletsMissedInAttack = 0;
            } else
            {
                blackBoard.currentAmountBulletsMissedInCorner = 0;
            }
            blackBoard.totalHittedBullets++;
        }
        public override void OnBulletMissed(BulletMissedEvent evnt)
        {
            if (blackBoard.behaviour == Behaviour.attack)
            {
                blackBoard.currentAmountBulletsMissedInAttack++;
            }
            else
            {
                blackBoard.currentAmountBulletsMissedInCorner++;
            }
            blackBoard.totalAmountMissedBullets++;
        }

        public override void OnWin(WinEvent evnt)
        {
            blackBoard.wonRound = true;
        }
        public override void OnRoundEnded(RoundEndedEvent evnt)
        {
            blackBoard.currentAmountBulletsMissedInAttack = 0;
            blackBoard.wonRound = false;
            blackBoard.behaviour = Behaviour.none;
        }


        public override void OnHitWall(HitWallEvent evnt)
        {

            blackBoard.hitsWall = true;
        }
        public override void OnHitRobot(HitRobotEvent e)
        {
            blackBoard.hitsRobot = true;
        }

    }
}
