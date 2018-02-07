using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Dendrite
    {
        public double Weight { get; set; }

        public Dendrite()
        {
            this.Weight = new RandomValue().RandomNumber;
        }
    }
}
