using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aula_1
{
    public static class Utils
    {
        public static double Rescale(double x, double min, double max)
            => (max - min) * x + min;
    }
}