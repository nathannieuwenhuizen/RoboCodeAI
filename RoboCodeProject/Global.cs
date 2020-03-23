using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robocode;

namespace RoboCodeProject
{
    public static class Global
    {
        public static double DegToRad(double degrees)
        {
            double radians = (Math.PI / 180) * degrees;
            return (radians);
        }

        public static double BattleFieldDiagonal(Robot robot)
        {
            return Math.Sqrt(Math.Pow(robot.BattleFieldHeight, 2) + Math.Pow(robot.BattleFieldWidth, 2));
        }
        public static Behaviour LostBehaviour = Behaviour.none ;
        public static Behaviour WonBehaviour = Behaviour.none ;
    }
}
