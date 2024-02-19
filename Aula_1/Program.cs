using System;
using Aula_1;

double MyFunction(double x)
{
    return x * x * x + 4 * x * x - 10;
}

var solB = Root2.Bisection(MyFunction, -100, 100);
// var solFP = Root2.FalsePosition(MyFunction, -100, 100);

Console.WriteLine(solB);
// Console.WriteLine(solFP);
