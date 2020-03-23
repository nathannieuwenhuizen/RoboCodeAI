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
        public BTNode AimBehaviour;
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
                        blackBoard.attackInfo.consecutiveMisses = 0;
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
                        return blackBoard.attackInfo.consecutiveMisses < 20;
                    })


                );

            CornerBehaviour =
                    new Sequence(blackBoard,
                        new TurnHeading(blackBoard, false, Orientation.left),
                        new TurnGun(blackBoard, false, Orientation.right),
                        new GenericSimpleTask(blackBoard, () =>
                        {
                            blackBoard.attackInfo.consecutiveMisses = 0;
                            IsAdjustGunForRobotTurn = false;
                            IsAdjustRadarForGunTurn = true;
                            blackBoard.behaviour = Behaviour.corner;
                            return true;
                        }),
                        new ChangeColor(blackBoard, Color.Blue),

                    new TurnScan(blackBoard, 45, Orientation.none),
                    new TurnScan(blackBoard, -45, Orientation.none),
                    new TurnScan(blackBoard, 0, Orientation.right),

                    //if at wall
                    new Selector(blackBoard, new IsAtWall(blackBoard, 150),
                    //set turn 90 degreees
                    new TurnHeading(blackBoard, false, Orientation.none)
                    ),

                    //if at shot range then fire
                    new Selector(blackBoard, new InShotRange(blackBoard, 10000),
                    new Shoot(blackBoard, 5)
                    ),
                    //do a set(async) move
                    new MoveAhead(blackBoard, 100, true)
                    );

            AimBehaviour =
                    new Sequence(blackBoard,
                        //only for the first time
                        new TurnHeading(blackBoard, false, Orientation.bottom),
                        new TurnGun(blackBoard, true, Orientation.none),
                        new GenericSimpleTask(blackBoard, () =>
                        {
                            blackBoard.aimInfo.consecutiveMisses = 0;
                            IsAdjustGunForRobotTurn = true;
                            IsAdjustRadarForGunTurn = false;
                            blackBoard.behaviour = Behaviour.aim;
                            return true;
                        }),
                        new ChangeColor(blackBoard, Color.Yellow),
                    //if not at wall
                    new CheckNode(blackBoard, () => { return blackBoard.headingOrientation != Orientation.forth; },
                        //do a set(async) move
                        new MoveAhead(blackBoard, 50, true),
                        //if at wall
                        new Selector(blackBoard, new IsAtWall(blackBoard, 150),
                        //set turn 90 degreees
                        new TurnHeading(blackBoard, false, Orientation.none)
                        )
                    ),
                    new CheckNode(blackBoard, () => { return blackBoard.headingOrientation == Orientation.forth; },
                        //aim at enemy
                        new TurnScan(blackBoard, 360, Orientation.none, true),
                        new TurnGun(blackBoard, true, Orientation.none),

                        //if at shot range then fire
                        new Selector(blackBoard, new InShotRange(blackBoard, 10000, 5),
                        new Shoot(blackBoard, 5, true)
                    )
                    ));


            WinBehaviour =
            new Sequence(blackBoard,
                new CheckNode(blackBoard, () =>
                {
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

                //otherwise it would crash/disabled
                new TurnScan(blackBoard, 0),

                //do dance if won
                new CheckNode(blackBoard, () =>
                {
                    return blackBoard.wonRound == true;
                },
                WinBehaviour
               ),

                //if won in previousround, start off with that behaviour.
                new CheckNode(blackBoard, () => { return Global.WonBehaviour == Behaviour.aim; },
                    new CheckNode(blackBoard, () => { return AimViable(); },
                        AimBehaviour
                    )
                ),
                new CheckNode(blackBoard, () => { return Global.WonBehaviour == Behaviour.attack; },
                    new CheckNode(blackBoard, () => { return AttackViable(); },
                        AttackBehaviour
                    )
                ),


                //do the three strategies. if the first one doesn't succeed, than do the second and so forth.
                new CheckNode(blackBoard, () => { return CornerViable() && !WinningBehaviourViable(Behaviour.corner); },
                    CornerBehaviour
                ),
                new CheckNode(blackBoard, () => { return !CornerViable() && AimViable(); },
                    AimBehaviour
                ),
                new CheckNode(blackBoard, () => { return !CornerViable() && !AimViable() && AttackViable(); },
                    AttackBehaviour
                ),
                
                new CheckNode(blackBoard, () => { return !AttackViable(); },
                    new GenericSimpleTask(blackBoard, () => {
                        // I give up, try other tactics again
                        blackBoard.cornerInfo = new BehaviourInfo();
                        blackBoard.aimInfo = new BehaviourInfo();
                        blackBoard.attackInfo = new BehaviourInfo();
                        return true;
                    })
                )
               );

            new TurnScan(blackBoard, 360).Tick();

            while (true)
            {
                BeahaviorTree.Tick();
                blackBoard.robot.Out.WriteLine("New round " + Global.LostBehaviour);
                
            }
        }

        public bool WinningBehaviourViable(Behaviour otherBehaviour)
        {
            if (otherBehaviour == Global.WonBehaviour || Global.WonBehaviour == Behaviour.none) { return false; }

            switch (Global.WonBehaviour)
            {
                case Behaviour.aim:
                    return AimViable();
                case Behaviour.attack:
                    return AttackViable();
                default:
                    return CornerViable();
            }
        }

        public bool CornerViable()
        {
            return blackBoard.cornerInfo.consecutiveMisses < 5 && blackBoard.cornerInfo.aimAttempts < 20 && Global.LostBehaviour != Behaviour.corner;
        }
        public bool AimViable()
        {
            return blackBoard.aimInfo.consecutiveMisses < 10 && blackBoard.aimInfo.gotHit < 8 && Global.LostBehaviour != Behaviour.aim;
        }

        public bool AttackViable()
        {
            return blackBoard.attackInfo.consecutiveMisses < 3 && blackBoard.attackInfo.aimAttempts < 30 && Global.LostBehaviour != Behaviour.attack;
        }


        public override void OnScannedRobot(ScannedRobotEvent evnt)
        {
            blackBoard.lastScannedRobotEvent = evnt;
        }
        public override void OnBulletHit(BulletHitEvent evnt)
        {
            blackBoard.CurentBehaviourInfo.consecutiveMisses = 0;
            blackBoard.totalHittedBullets++;
        }
        
        public override void OnHitByBullet(HitByBulletEvent evnt)
        {
            blackBoard.CurentBehaviourInfo.gotHit++;
        }

        public override void OnBulletMissed(BulletMissedEvent evnt)
        {
            blackBoard.CurentBehaviourInfo.consecutiveMisses++;
            blackBoard.totalAmountMissedBullets++;
        }


        public override void OnWin(WinEvent evnt)
        {
            Global.LostBehaviour = Behaviour.none;
            Global.WonBehaviour = blackBoard.behaviour;

            blackBoard.wonRound = true;
            ResetData();

        }
        public void ResetData()
        {
            blackBoard.attackInfo = new BehaviourInfo();
            blackBoard.cornerInfo = new BehaviourInfo();
            blackBoard.aimInfo = new BehaviourInfo();

            blackBoard.wonRound = false;
            blackBoard.behaviour = Behaviour.none;

            blackBoard.robot.Out.WriteLine("New round " + Global.LostBehaviour);
        }
        //
        public override void OnDeath(DeathEvent evnt)
        {
            Global.WonBehaviour = Behaviour.none;
            Global.LostBehaviour = blackBoard.behaviour;

            blackBoard.robot.Out.WriteLine("Lost... by behavior: " + Global.LostBehaviour);
            ResetData();
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
