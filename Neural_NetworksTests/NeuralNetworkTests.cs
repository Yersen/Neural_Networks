using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neural_Networks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neural_Networks.Tests
{
    [TestClass()]
    public class NeuralNetworkTests
    {
        [TestMethod()]
        public void FeedForwardTest()
        {
            var dataset = new List<Tuple<double, double[]>>
            {
                //Результат - болеет - 1
                //            здоров - 0
                //Неправильная температура T
                //Хороший возраст A
                //Курит S
                //Правильно питается F
                //                                           T A S F
                new Tuple<double, double[]> (0,new double[] {0,0,0,0 }),
                new Tuple<double, double[]> (0,new double[] {0,0,0,1 }),
                new Tuple<double, double[]> (1,new double[] {0,0,1,0 }),
                new Tuple<double, double[]> (0,new double[] {0,0,1,1 }),
                new Tuple<double, double[]> (0,new double[] {0,1,0,0 }),
                new Tuple<double, double[]> (0,new double[] {0,1,0,1 }),
                new Tuple<double, double[]> (1,new double[] {0,1,1,0 }),
                new Tuple<double, double[]> (0,new double[] {0,1,1,1 }),
                new Tuple<double, double[]> (1,new double[] {1,0,0,0 }),
                new Tuple<double, double[]> (1,new double[] {1,0,0,1 }),
                new Tuple<double, double[]> (1,new double[] {1,0,1,0 }),
                new Tuple<double, double[]> (1,new double[] {1,0,0,1 }),
                new Tuple<double, double[]> (1,new double[] {1,0,1,0 }),
                new Tuple<double, double[]> (0,new double[] {1,1,0,1 }),
                new Tuple<double, double[]> (1,new double[] {1,1,1,0 }),
                new Tuple<double, double[]> (1,new double[] {1,1,1,1 }),
            };
            var topology = new Topology(4, 1, 0.1, 16,8);
            var neuralNetwork = new NeuralNetwork(topology);

            var difference = neuralNetwork.Learn(dataset, 100000);//обучение 1000 прогон
            var results = new List<double>();
            foreach (var data in dataset) 
            {
                var res = neuralNetwork.FeedForward(data.Item2).Output;//использование
                results.Add(res);
            }
            for (int i = 0; i < results.Count; i++)
            {
                var expected = Math.Round(dataset[i].Item1,3);
                var actual = Math.Round(results[i], 3);
                Assert.AreEqual(expected, actual);
            }
        }
    }
}