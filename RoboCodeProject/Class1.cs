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

        public BTNode CornerBehaviour;
        public BTNode BeahaviorTree;
        public BlackBoard blackBoard = new BlackBoard();

        public override void Run()
        {
            blackBoard.robot = this;

            CornerBehaviour = 
                    new Sequence(blackBoard,
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
                CornerBehaviour
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

        public override void OnHitWall(HitWallEvent e)
        {
            blackBoard.hitsWall = true;
            //blackBoard.movesForward = !blackBoard.movesForward;
        }

        public override void OnScannedRobot(ScannedRobotEvent evnt)
        {
            //Out.WriteLine("I scanned robot: " + evnt.Name);
            blackBoard.lastScannedRobotEvent = evnt;
            
        }
    }
    public class Crazy : AdvancedRobot
    {
        bool movingForward;

        /**
         * run: Crazy's main run function
         */
        public void run()
        {
            while (true)
            {
                // Tell the game we will want to move ahead 40000 -- some large number
                SetAhead(40000);
                movingForward = true;
                // Tell the game we will want to turn right 90
                SetTurnRight(90);
                // At this point, we have indicated to the game that *when we do something*,
                // we will want to move ahead and turn right.  That's what "set" means.
                // It is important to realize we have not done anything yet!
                // In order to actually move, we'll want to call a method that
                // takes real time, such as waitFor.
                // waitFor actually starts the action -- we start moving and turning.
                // It will not return until we have finished turning.
                WaitFor(new TurnCompleteCondition(this));
                // Note:  We are still moving ahead now, but the turn is complete.
                // Now we'll turn the other way...
                SetTurnLeft(180);
                // ... and wait for the turn to finish ...
                WaitFor(new TurnCompleteCondition(this));
                // ... then the other way ...
                SetTurnRight(180);
                // .. and wait for that turn to finish.
                WaitFor(new TurnCompleteCondition(this));
                // then back to the top to do it all again
            }
        }

        /**
         * onHitWall:  Handle collision with wall.
         */
        public void onHitWall(HitWallEvent e)
        {
            // Bounce off!
            reverseDirection();
        }

        /**
         * reverseDirection:  Switch from ahead to back & vice versa
         */
        public void reverseDirection()
        {
            if (movingForward)
            {
                SetBack(40000);
                movingForward = false;
            }
            else
            {
                SetAhead(40000);
                movingForward = true;
            }
        }

        /**
         * onScannedRobot:  Fire!
         */
        public void onScannedRobot(ScannedRobotEvent e)
        {
            Fire(1);
        }

        /**
         * onHitRobot:  Back up!
         */
        public void onHitRobot(HitRobotEvent e)
        {
            // If we're moving the other robot, reverse!
            if (e.IsMyFault)
            {
                reverseDirection();
            }
        }
    }
}
