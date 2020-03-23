using System;
using Robocode;
namespace RoboCodeProject
{
    public enum Behaviour
    {
        none,
        corner,
        attack,
        aim,
        win
    }

    public class BehaviourInfo
    {
        public int consecutiveMisses = 0;
        public int aimAttempts = 0;
        public int gotHit = 0;
    }

    public class BlackBoard
    {

        //tank orientations
        public bool movesForward = true;
        public Orientation headingOrientation = Orientation.none;
        public Orientation gunOrientation = Orientation.none;
        public Orientation sensorOrientation = Orientation.none;

        //objects
        public AdvancedRobot robot;
        public ScannedRobotEvent lastScannedRobotEvent = null;

        //round data
        public int totalAmountMissedBullets = 0;
        public int totalHittedBullets = 0;

        public int ticksBetweenSuccesfulShots = 0;

        public bool wonRound = false;

        //state
        public Behaviour behaviour = Behaviour.none;

        public BehaviourInfo CurentBehaviourInfo
        {
            get {
                switch(behaviour)
                {
                    case Behaviour.attack:
                        return attackInfo;
                    case Behaviour.corner:
                        return cornerInfo;
                    case Behaviour.aim:
                        return aimInfo;
                    default:
                        return attackInfo;
                }
            }
            set {
                switch (behaviour)
                {
                    case Behaviour.attack:
                        attackInfo = value;
                        break;
                    case Behaviour.corner:
                        cornerInfo = value;
                        break;
                    case Behaviour.aim:
                        aimInfo = value;
                        break;

                }
            }
        }
        public BehaviourInfo attackInfo = new BehaviourInfo();
        public BehaviourInfo cornerInfo = new BehaviourInfo();
        public BehaviourInfo aimInfo = new BehaviourInfo();

        public bool hitsWall = false;
        public bool hitsRobot = false;
    }
}