using System;
using Aula_1;
using Aula1;

double Rosenbrock(double[] x)
{
    var n = x.Length - 1.0;
    var sum = 0.0;

    for (var i = 0; i < n; i++)
        sum += 100 * Math.Pow(x[i+1] - x[i] * x[i], 2) + Math.Pow(1 - x[i], 2);

    return sum;
}

// double MyDer(double x)
// {
//     return 1 / 2.0 * Math.Sqrt(Math.Abs(x)) * x + (Math.Sqrt(Math.Abs(x)) - 5);
// }

List<double[]> bounds = new()
{
    new double[] { -10.0, 10.0 },
    new double[] { -10.0, 10.0 }
};

// double[] x = {1, 1};
// double[] sol;
var date = DateTime.Now;

// date = DateTime.Now;
// sol = Root.Bisection(MyFunction, -100, 100);
// Console.WriteLine($"Bisection Solution: {sol} | Time: {(DateTime.Now - date).TotalMilliseconds}");

// date = DateTime.Now;
// sol = Root.FalsePosition(MyFunction, -100, 100);'
// Console.WriteLine($"False Position Solution: {sol} | Time: {(DateTime.Now - date).TotalMilliseconds}");

// date = DateTime.Now;
// sol = Root.Newton(MyFunction, MyDer, 10.0);
// Console.WriteLine($"Newton Solution: {sol} | Time: {(DateTime.Now - date).TotalMilliseconds}");

// date = DateTime.Now;
// sol = Root.Newton(MyFunction, double (double x) => Diff.Differentiate3P(MyFunction, x), 10.0);
// Console.WriteLine($"Newton Differentiate Solution: {sol} | Time: {(DateTime.Now - date).TotalMilliseconds}");

// date = DateTime.Now;
// sol = Optimize.Newton(MyFunction, 1.0);
// Console.WriteLine($"Optimize Newton Solution: {sol} | Time: {(DateTime.Now - date).TotalMilliseconds}");

// date = DateTime.Now;
// sol = Optimize.GradientDescent(MyFunction, 1.0);
// Console.WriteLine($"Optimize Gradient Descendent Solution: {sol} | Time: {(DateTime.Now - date).TotalMilliseconds}");

// date = DateTime.Now;
// sol = Optimize.GradientDescent(MyFunction, x, 1e-5, 1e-90);
// Console.WriteLine($"Optimize Gradient Descendent 2D Solution: {sol[0]}, {sol[1]} | Time: {(DateTime.Now - date).TotalMilliseconds}");

date = DateTime.Now;
var diffEvolution = new DiffEvolution(Rosenbrock, bounds, 1000);
var res = diffEvolution.Optimize(10000);
Console.WriteLine($"Res: {res[0]}, {res[1]} | Time: {(DateTime.Now - date).TotalMilliseconds}");
