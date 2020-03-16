using Robocode;
using System.Drawing;
using System.IO;
using System.Reflection;
using Robocode.RobotInterfaces;

namespace RoboCodeProject
{
    class ChangeColor : BTNode
    {
        private Color color;
        public ChangeColor(BlackBoard blackBoard, Color _color)
        {
            this.blackBoard = blackBoard;
            color = _color;
        }
        public override BTNodeStatus Tick()
        {
            blackBoard.robot.SetAllColors(color);
            return BTNodeStatus.succes;
        }
    }
}
