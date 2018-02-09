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

            for (int i = 0; i < 10; i++)
            {
                //better than function call. cos of speed
                NN.StohasticGradient(inputs, outtput);
                double temp = inputs[0];
                inputs[0] = inputs[1];
                inputs[1] = temp;


                double tempp = outtput[0];

                outtput[0] = outtput[1];
                outtput[1] = tempp;
            }

            double[] check = NN.StohasticGradient(inputs, outtput);

            double[,] tempppp = new double[3,3];
            int size = tempppp.Length;
        }

     
        
    }
}
