using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboCodeProject
{
    public static class Global
    {
        public static double DegToRad(double degrees)
        {
            double radians = (Math.PI / 180) * degrees;
            return (radians);
        }

    }
}
