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
        protected double MutationMin { get; set; }
        protected double MutationMax { get; set; }
        protected double Recombination { get; set; }
        protected List<double[]> Bounds { get; }
        protected Func<double[], double> Restriction { get; }
        private double[] IndividualsRestrictions { get; set; }
        private double[] IndividualsFitness { get; set; }

        public DiffEvolution(
        Func<double[], double> fitness, 
        List<double[]> bounds, 
        int npop,
        Func<double[], double> restriction,
        double mutationMin = 0.5,
        double mutationMax = 0.9,
        double recombination = 0.8
        )
        {
            this.Fitness = fitness;
            this.Dimension = bounds.Count;
            this.Bounds = bounds;
            this.Npop = npop;
            this.Restriction = restriction;
            Individuals = new(Npop);
            this.MutationMin = mutationMin;
            this.MutationMax = mutationMax;
            this.Recombination = recombination;
            this.IndividualsRestrictions = new double[Npop];
            this.IndividualsFitness = new double[Npop];
        }

        private void GeneratePopulation()
        {
            int dimension = Dimension;

            for (int i = 0; i < Npop; i++)
            {
                Individuals.Add(new double[dimension]);

                for (int j = 0; j < dimension; j++)
                    Individuals[i][j] = Utils.Rescale(Random.Shared.NextDouble(), Bounds[j][0], Bounds[j][1]);

                IndividualsRestrictions[i] = Restriction(Individuals[i]);
                IndividualsFitness[i] = IndividualsRestrictions[i] <= 0.0
                                        ? Fitness(Individuals[i])
                                        : double.MaxValue;
            }

            FindBestIndividual();
        }

        private void FindBestIndividual()
        {
            var fitnessBest = IndividualsFitness[BestIndividualIndex];

            for (int i = 0; i < Npop; i++)
            {
                var fitnessCurrent = Fitness(Individuals[i]);

                if (fitnessCurrent < fitnessBest)
                {
                    BestIndividualIndex = i;
                    fitnessBest = fitnessCurrent;
                }
            }

            IndividualsFitness[BestIndividualIndex] = fitnessBest;
        }

        private double[] Mutate(int index)
        {

            int individualRand1;
            int individualRand2;

            do individualRand1 = Random.Shared.Next(Npop);
            while (individualRand1 == index);
            
            do individualRand2 = Random.Shared.Next(Npop);
            while (individualRand1 == individualRand2);

            var newIndividual = (double[])Individuals[BestIndividualIndex].Clone();

            for (int i = 0; i < Dimension; i++)
                newIndividual[i] += Utils.Rescale(Random.Shared.NextDouble(), MutationMin, MutationMax) * (Individuals[individualRand1][i] - Individuals[individualRand2][i]);

            return newIndividual;
        }

        protected double[] Crossover(int index)
        {
            var trial = Mutate(index);
            var trial2 = (double[])Individuals[index].Clone();

            for (int i = 0; i < Dimension; i++)
            {
                if (!(Random.Shared.NextDouble() < Recombination) || (i == Random.Shared.Next(Dimension)))
                    trial2[i] = trial[i];
            }

            return trial2;
        }

        protected void Iterate()
        {
            for (int i = 0; i < Npop; i++)
            {
                var trial = Crossover(i);

                var restTrial = Restriction(trial);
                var fitnessTrial = restTrial <= 0.0 ? Fitness(trial) : double.MaxValue;

                var restIndividual = IndividualsRestrictions[i];

                if (
                (restIndividual > 0.0 && (restTrial < restIndividual))
                || (restTrial <= 0.0 && restIndividual > 0.0)
                || (restTrial <= 0.0 && Fitness(trial) < fitnessTrial)
                )
                {
                    Individuals[i] = trial;
                    IndividualsRestrictions[i] = restTrial;
                }

            }

            FindBestIndividual();
        }

        public double[] Optimize(int n)
        {
            GeneratePopulation();

            for (int i = 0; i < n; i++)
                Iterate();


            return Individuals[BestIndividualIndex];
        }
    }
}