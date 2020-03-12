using System;
using Robocode;
namespace RoboCodeProject
{
    public class BlackBoard
    {
        public bool hitsWall = false;
        public bool movesForward = true;
        public Orientation desiredOrientation = Orientation.none;
        public Orientation gunOrientation = Orientation.none;
        public Orientation sensorOrientation = Orientation.none;
        public AdvancedRobot robot;
        public ScannedRobotEvent lastScannedRobotEvent = null;
    }
}