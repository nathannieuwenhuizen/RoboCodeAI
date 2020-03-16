using System;
using Robocode;
namespace RoboCodeProject
{
    public class BlackBoard
    {
        public bool movesForward = true;
        public Orientation desiredOrientation = Orientation.none;
        public Orientation gunOrientation = Orientation.none;
        public Orientation sensorOrientation = Orientation.none;
        public AdvancedRobot robot;
        public ScannedRobotEvent lastScannedRobotEvent = null;

        public int totalAmountMissedBullets = 0;
        public int totalHittedBullets = 0;
        public int ticksBetweenSuccesfulShots = 0;
        public int currentAmountBulletsMissed = 0;

        public bool hitsWall = false;
        public bool hitsRobot = false;
    }
}