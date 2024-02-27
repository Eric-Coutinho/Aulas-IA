using System.Diagnostics;
using AIContinuous;

double Rosenbrock(double[] x)
{
    var n = x.Length - 1;
    double sum = 0.0;

    for (int i = 0; i < n; i++)
    {
        sum += 100.0 * Math.Pow(x[i + 1] - x[i] * x[i], 2) + Math.Pow(1.0 - x[i], 2);
    }

    return sum;
}

double Restriction(double[] x)
{
    return (Math.Pow(x[0] - 1.0, 3.0) - x[1] + 1.0) + (x[0] + x[0] - 2.0);
}

List<double[]> bounds = new()
{
    new double[] { -10.0, 10.0 },
    new double[] { -10.0, 10.0 },
};

var sw = new Stopwatch();

sw.Restart();
var diffEvolution = new DiffEvolution(Rosenbrock, bounds, 200, Restriction);
var res = diffEvolution.Optimize(10000);

sw.Stop();
Console.WriteLine($"Res: {res[0]} {res[1]} | Time: {sw.ElapsedMilliseconds}");