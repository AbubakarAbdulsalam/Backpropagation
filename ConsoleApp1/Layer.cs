using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Layer
    {
        public IList<Neuron> Neurons { get; set; }

        public int NeuronCount { get { return Neurons.Count; } }

        public Layer(int neuronsContained)
        {
            Neurons = new List<Neuron>(neuronsContained);
        }
    }
}
