﻿using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CMAESnet.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            double[] initial = new double[] { 2, 3 };
            CMAESOptimizer cmaoptimizer = new CMAESOptimizer(TestFunctions, initial, 1.5, 3221);

            cmaoptimizer.Optimize();

            Console.WriteLine(cmaoptimizer.ResultVector);
            Console.WriteLine(cmaoptimizer.ResultValue);
        }

        private static void NewMethod()
        {
            CMA cma_es = new CMA(Vector<double>.Build.Dense(2, 0), 1.3);

            for (int generation = 0; generation < 50; generation++)
            {
                var solutions = new List<Tuple<Vector<double>, double>>();
                for (int i = 0; i < cma_es.PopulationSize; i++)
                {
                    var x = cma_es.Ask();
                    double value = TestFunctions(x);
                    solutions.Add(new Tuple<Vector<double>, double>(x, value));
                    //Console.WriteLine("#{0} {1} (x1={2}, x2={3})", generation, value, x[0], x[1]);
                }
                cma_es.Tell(solutions);
                double yBest = solutions.Min(x => x.Item2);
                Vector<double> xBest = solutions.Where(x => x.Item2 == yBest).FirstOrDefault().Item1;
                Console.WriteLine("dd");
            }
            Console.WriteLine("Hello World!");
        }

        private static double TestFunctions(IList<double> x)
        {
            return Math.Pow(x[0] - 3, 2) + Math.Pow(10 * (x[1] + 2), 2);
        }
    }
}
