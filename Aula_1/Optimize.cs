using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aula1;

namespace Aula_1
{
    public static class Optimize
    {
        public static double Newton(Func<double, double> function, double x0, double h = 1e-2, double atol = 1e-4, int maxIter = 10000)
        {
            Func<double, double> diffFunction = x => Diff.Differentiate3P(function, x, h * 2);
            Func<double, double> diffSecondFunction = x => Diff.Differentiate3P(diffFunction, x, h);

            return Root.Newton(diffFunction, diffSecondFunction, x0, atol, maxIter);
        }
        public static double GradientDescent(Func<double, double> function, double x0, double learningRate = 1e-2, double atol = 1e-4)
        {
            double xp = x0;
            double diff = Diff.Differentiate3P(function, xp);

            while (Math.Abs(diff) > atol)
            {
                diff = Diff.Differentiate3P(function, xp);
                xp -= learningRate * diff;
            }
            return xp;
        }

        public static double[] GradientDescent(Func<double[], double> function, double[] x0, double learningRate = 1e-2, double atol = 1e-4)
        {
            var dim = x0.Length;
            var xp = (double[])x0.Clone();
            var diff = Diff.Gradient(function, xp);
            double diffNorm;

            do
            {
                diffNorm = 0.0;
                diff = Diff.Gradient(function, xp);

                for (int i = 0; i < dim; i++)
                {
                    xp[i] -= learningRate * diff[i];
                    diffNorm += Math.Abs(diff[i]);
                }
            }
            while (diffNorm > dim *  atol);
            
            return xp;
        }
    }
}