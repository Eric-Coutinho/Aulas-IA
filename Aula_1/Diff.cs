using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aula_1
{
    public static class Diff
    {
        public static double Differentiate(Func<double, double> function, double x, double h = 1e-2)
            => (function(x + h) - function(x)) / h;
            // x1 = x + h
            // x0 = x

        public static double Differentiate3P(Func<double, double> function, double x, double h = 1e-2)
            => (function(x + h) - function(x - h)) / (2.0 * h);
            // x1 = x + h
            // x0 = x
            
        public static double Differentiate5P(Func<double, double> function, double x, double h = 1e-2)
            => function(x - 2.0 * h) - 8.0 * function(x - h) + 8.0 * function(x + h) - function(x + 2.0 * h) / (12.0 * h);
    }
}