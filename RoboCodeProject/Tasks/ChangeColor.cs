using Robocode;
using System.Drawing;
using System.IO;
using System.Reflection;
using Robocode.RobotInterfaces;
using System;

namespace RoboCodeProject
{
    class ChangeColor : BTNode
    {
        private Color color;
        private bool randomValues;
        public ChangeColor(BlackBoard blackBoard, Color _color, bool _randomValues = false)
        {
            this.blackBoard = blackBoard;
            color = _color;
            randomValues = _randomValues;
        }
        public override BTNodeStatus Tick()
        {
            if (randomValues)
            {
                Random rnd = new Random();
                blackBoard.robot.SetAllColors(Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255)));
            } else
            {
                blackBoard.robot.SetAllColors(color);
            }
            return BTNodeStatus.succes;
        }
    }
}
