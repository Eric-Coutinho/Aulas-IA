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
        protected List<double[]> Bounds { get; }

        public DiffEvolution(Func<double[], double> fitness, List<double[]> bounds, int npop)
        {
            this.Fitness = fitness;
            this.Dimension = bounds.Count;
            this.Npop = npop;
            this.Bounds = bounds;
            Individuals = new(npop);
        }

        private void GeneratePopulation()
        {
            int dimension = Dimension;

            for (int i = 0; i < Npop; i++)
            {
                Individuals[i] = new double[dimension];

                for (int j = 0; j < dimension; j++)
                {
                    Individuals[i][j] = Utils.Rescale(Random.Shared.NextDouble(), Bounds[j][0], Bounds[j][1]);
                }
            }
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

            newIndividual = Individuals[BestIndividualIndex];

            for (int i = 0; i < Dimension; i++)
            {
                newIndividual[i] += Individuals[Random.Shared.Next(Npop)][i] - Individuals[Random.Shared.Next(Npop)][i];
            }

            return newIndividual;
        }

        public double[] Optimize()
        {
            GeneratePopulation();
            FindBestIndividual();

            return Individuals[BestIndividualIndex];
        }
    }
}