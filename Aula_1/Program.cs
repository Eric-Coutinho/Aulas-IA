using System;
using Aula_1;
using Aula1;

double MyFunction(double x)
{
    return Math.Pow(x - 1, 2) + Math.Sin(Math.Pow(x, 3));
}

double MyDer(double x)
{
    return 1 / 2.0 * Math.Sqrt(Math.Abs(x)) * x + (Math.Sqrt(Math.Abs(x)) - 5);
}

double sol;
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

date = DateTime.Now;
sol = Optimize.Newton(MyFunction, 1.0);
Console.WriteLine($"Optimize Newton Solution: {sol} | Time: {(DateTime.Now - date).TotalMilliseconds}");

date = DateTime.Now;
sol = Optimize.GradientDescent(MyFunction, 1.0);
Console.WriteLine($"Optimize Gradient Descendent Solution: {sol} | Time: {(DateTime.Now - date).TotalMilliseconds}");
