using Neural_Networks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medical_System
{
    class SystemController
    {
        public NeuralNetwork DataNetwork { get; }
        public NeuralNetwork ImageNetwork { get; }

        public SystemController()
        {
            var dataTopology = new Topology(14, 1, 0.1, 7);//1-вход,2-выход,3-скорость обучения,4-промежуточный слой
            DataNetwork = new NeuralNetwork(dataTopology);

            var imageTopology = new Topology(400, 1, 0.1, 200);
            ImageNetwork = new NeuralNetwork(imageTopology);
        }
    }
}
