using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Neuron
    {
        
        public double NeuronValue { get ; set; }

        public double Delta { get; set; }
        public double Bias { get; set; }
        public IList<Dendrite> dendrites { get; set; }

        public int DendritesCount {  get { return this.dendrites.Count; } }
        
        public Neuron()
        {
            this.dendrites = new List<Dendrite>();

            this.Bias = new RandomValue().RandomNumber;
            
        }
    }
}
