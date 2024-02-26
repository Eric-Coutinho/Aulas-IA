using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aula_1
{
    public class DiffEvolution
    {
        protected Func<double[], double> Fitness { get; }
        protected int Dimension { get; }
        protected int Npop { get; }
        protected List<double[]> Individuals { get; set; }
        protected int BestIndividualIndex { get; set; }
        protected double BestIndividualFitness { get; set; } = double.MaxValue;
        protected double Mutation { get; set; }
        protected List<double[]> Bounds { get; }

        public DiffEvolution(Func<double[], double> fitness, List<double[]> bounds, int npop, double mutation = 0.7)
        {
            this.Fitness = fitness;
            this.Dimension = bounds.Count;
            this.Npop = npop;
            Individuals = new(Npop);
            this.Bounds = bounds;
            this.Mutation = mutation;
        }

        private void GeneratePopulation()
        {
            int dimension = Dimension;

            for (int i = 0; i < Npop; i++)
            {
                Individuals.Add(new double[dimension]);

                for (int j = 0; j < dimension; j++)
                {
                    Individuals[i][j] = Utils.Rescale(Random.Shared.NextDouble(), Bounds[j][0], Bounds[j][1]);
                }
            }

            FindBestIndividual();
        }

        private void FindBestIndividual()
        {
            var fitnessBest = BestIndividualFitness;

            for (int i = 0; i < Npop; i++)
            {
                var fitnessCurrent = Fitness(Individuals[i]);

                if (fitnessCurrent < fitnessBest)
                {
                    BestIndividualIndex = i;
                    fitnessBest = fitnessCurrent;
                }
            }

            BestIndividualFitness = fitnessBest;
        }

        private double[] Mutate(double[] individual)
        {
            var newIndividual = new double[Dimension];

            var individualRand1 = Random.Shared.Next(Npop);
            var individualRand2 = Random.Shared.Next(Npop);

            newIndividual = Individuals[BestIndividualIndex];

            for (int i = 0; i < Dimension; i++)
            {
                newIndividual[i] += Mutation * (Individuals[individualRand1][i] - Individuals[individualRand2][i]);
            }

            return newIndividual;
        }

        protected void Iterate()
        {
            for (int i = 0; i < Npop; i++)
            {
                var trial = Mutate(Individuals[i]);

                if (Fitness(trial) > Fitness(Individuals[i]))
                    Individuals[i] = trial;
            }

            FindBestIndividual();
        }

        public double[] Optimize(int n)
        {
            GeneratePopulation();

            for (int i = 0; i < n; i++)
            {

                Iterate();
            }

            return Individuals[BestIndividualIndex];
        }
    }
}