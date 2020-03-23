using System;
using Robocode;
namespace RoboCodeProject
{
    public enum Behaviour
    {
        none,
        corner,
        attack,
        win
    }

    public class BlackBoard
    {

        //tank orientations
        public bool movesForward = true;
        public Orientation desiredOrientation = Orientation.none;
        public Orientation gunOrientation = Orientation.none;
        public Orientation sensorOrientation = Orientation.none;

        //objects
        public AdvancedRobot robot;
        public ScannedRobotEvent lastScannedRobotEvent = null;

        //round data
        public int totalAmountMissedBullets = 0;
        public int totalHittedBullets = 0;

        public int ticksBetweenSuccesfulShots = 0;
        public int currentAmountBulletsMissedInAttack = 0;
        public int currentAmountBulletsMissedInCorner = 0;

        public bool wonRound = false;

        public int aimAttemptsCorner = 0;
        public int aimAttemptsAttack = 0;

        //state
        public Behaviour behaviour = Behaviour.none;
        public bool hitsWall = false;
        public bool hitsRobot = false;
    }
}