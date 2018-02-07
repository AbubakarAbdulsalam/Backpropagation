using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Network NN = new Network(21.5, new int[] { 2, 4, 2 });

            IList<double> inputs = new List<double>
            {
                1.0,
                0.0
            };

            IList<double> outtput = new List<double>
            {
                0.0,
                1.0
            };

            for(int i = 0; i < 1000000; i++)
            {
                NN.StohasticGradient(inputs, outtput);
            }


            double[] check = NN.StohasticGradient(inputs, outtput);
        }
    }
}
