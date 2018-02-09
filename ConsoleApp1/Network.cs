using System;
using System.Collections.Generic;
using System.Linq;
namespace ConsoleApp1
{
    class Network
    {
        private IList<Layer> layers;

        public double LearningRate { get; set; }

        public int LayerCount { get {return layers.Count; }}

        public double Lamda { get; set; }

        public Network(double learningRate, int[] layerss)
        {
            this.LearningRate = learningRate;
            this.layers = new List<Layer>();

            for(int i=0; i < layerss.Length; i++)
            {
                Layer layer = new Layer(layerss[i]);
                this.layers.Add(layer);

                for(int neurons =0; neurons < layerss[i]; neurons++)
                {
                    layer.Neurons.Add(new Neuron());
                }
                foreach (Neuron n in layer.Neurons)
                {
                    if(i!=0)
                    {
                        for(int d =0; d < layerss[i-1]; d++)
                        {
                            n.dendrites.Add(new Dendrite());
                        }
                    }
                }

            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private double Sigmoid (double input)
        {
            return 1 / (1 + Math.Exp(-input));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public double[] FeedForward(IList<double> input)
        {
            bool isInputSet = SetInputLayer(input);
            if(!isInputSet)
            { return null; }

            for(int i = 1; i < this.layers.Count; i++) // every layer
            {
                Layer currentLayer = this.layers[i];
                for(int numNeuron = 0; numNeuron < currentLayer.NeuronCount; numNeuron++) //for every neuron in that layer
                {
                    Neuron currentNeuron = currentLayer.Neurons[numNeuron];

                    currentNeuron.NeuronValue = 0.0;
                    for (int numInput = 0; numInput < this.layers[i - 1].NeuronCount; numInput++) //for every neuron in the last layer (which is the numbr of inputs into every neuron in this layer)
                    {
                        currentNeuron.NeuronValue = currentNeuron.NeuronValue + this.layers[i - 1].Neurons[numInput].NeuronValue * currentNeuron.dendrites[numInput].Weight;
                    }
                    currentNeuron.NeuronValue = Sigmoid(currentNeuron.NeuronValue + currentNeuron.Bias);

                }
            }

            //create list out of output layer neurons
            double[] output = new double[this.layers[this.layers.Count - 1].NeuronCount];
            Layer last = this.layers[this.layers.Count - 1];
            for (int i = 0; i < last.NeuronCount; i++)
            {
                output[i] = last.Neurons[i].NeuronValue;
            }

            return output;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="expectedOutput"></param>
        /// <param name="outputLayerActivation"></param>
        public void CalculateLastLayerError(double[]expectedOutput, double[]outputLayerActivation)
        {
            //####contract check of list sizes
            Layer last = this.layers[this.layers.Count - 1];
            for(int i=0; i < last.NeuronCount; i++)
            {
                Neuron current = last.Neurons[i];
                current.Delta = current.NeuronValue * (1 - current.NeuronValue) * (current.NeuronValue - expectedOutput[i]);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void BackPropagate()
        {
            for (int i = this.layers.Count - 2; i > 0; i--)
            {
                Layer current = this.layers[i];
                for(int j =0; j < current.NeuronCount; j++)
                {
                    Neuron currentNeuron = current.Neurons[j];
                    double sumWeightDelta =0.0;
                    for (int k = 0; k < this.layers[i + 1].NeuronCount; k++)
                    {
                        sumWeightDelta = sumWeightDelta + this.layers[i + 1].Neurons[k].dendrites[j].Weight * this.layers[i + 1].Neurons[k].Delta;
                    }
                    currentNeuron.Delta = currentNeuron.NeuronValue * (1 - currentNeuron.NeuronValue) * sumWeightDelta;
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputs"></param>
        /// <param name="expectedOutput"></param>
        public double[] StohasticGradient(IList<double>inputs, IList<double>expectedOutput)
        {
            double[] outputActiviation = FeedForward(inputs);
            if (outputActiviation == null) //better error notification and checking
                return null;
            
            CalculateLastLayerError(expectedOutput.ToArray(), outputActiviation); //calculate initial deltas

            BackPropagate(); 

            //update weights and bias
            for (int i = this.layers.Count - 1; i > 0; i--)  //start from output layer
            {
                for (int j = 0; j < this.layers[i].NeuronCount; j++)
                {
                    Neuron currentN = this.layers[i].Neurons[j];
                    currentN.Bias = currentN.Bias - this.LearningRate * currentN.Delta;

                    for (int k = 0; k < currentN.DendritesCount; k++)
                    {
                        currentN.dendrites[k].Weight = currentN.dendrites[k].Weight - this.LearningRate * this.layers[i - 1].Neurons[k].NeuronValue * currentN.Delta;
                    }
                }
            }
            return outputActiviation;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool SetInputLayer(IList<double>input)
        {
            if (input.Count != this.layers[0].NeuronCount)
            {
                return false;
            }
            int count = this.layers[0].NeuronCount;
            Layer inputLayer = this.layers[0];
            for(int i=0; i < count; i++)
            {
                this.layers[0].Neurons[i].NeuronValue = input[i];
               
            }
            return true;

        }

        private void Shuffle(IList<double> list)
        {
            var count = list.Count;
            var last = count - 1;
            for (int i = 0; i < last; i++)
            {
                Random n = new Random();
                var r = n.Next(i, count);
                var tmp = list[i];
                list[i] = list[r];
                list[r] = tmp;
            }
        }

        private void InLineShuffle(IList<double>s)
        {
          //  var shuffledList = s.OrderBy(x => Random.value).ToList(0);
        }
    }
}
