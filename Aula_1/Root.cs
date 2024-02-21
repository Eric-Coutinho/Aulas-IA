using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aula1
{
    public static class Root
    {
        public static double Bisection(Func<double, double> function, double a, double b, double atol = 1e-4, double rtol = 1e-4, int maxIter = 1000) //Func<recebe, retorna> , valor 0 , valor  1 , tol = erro máximo
        {
            var date = DateTime.Now;
            double c = 0.0;
            for (int i = 0; i < maxIter; i++)
            {
                c = (a + b) / 2.0;

                var fa = function(a);
                var fc = function(c);

                if (fa * fc < 0.0)
                    b = c;
                else
                    a = c;

                if (Math.Abs(fc) < atol)
                    break;

                if ((b - a) < 2.0 * rtol)
                    break;
            }
            Console.WriteLine((DateTime.Now - date).TotalMilliseconds);
            return c;
        }
        public static double FalsePosition(Func<double, double> function, double a, double b)
        {
            var fa = function(a);
            var fb = function(b);

            var c = -fb * (a - b) / (fa - fb) + b;

            double bis = Root.Bisection(function, c, b);

            return bis;
        }

        public static double Newton(Func<double, double> function, Func<double, double> der, double x0, double atol = 1e-4, int maxIter = 10000)
        {
            double xp = x0;


            for (var i = 0; i < maxIter; i++)
            {
                var fp = function(xp);
                xp -= fp / der(xp);

                if (Math.Abs(fp) < atol)
                    break;
            }
            return xp;
        }
    }
}